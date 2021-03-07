using System.Text.RegularExpressions;

using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.AppVeyor;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.NuGet;

using static Nuke.Common.IO.XmlTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    public Build()
    {
        SdkVersion = XmlPeekSingle(
            SourceDirectory / $"Resharper.Bootstrap{BootstrapVersion}.Templates" / $"Resharper.Bootstrap{BootstrapVersion}.Templates.csproj",
            "//PackageReference[@Include='JetBrains.ReSharper.SDK']/@Version");
        SdkVersion.NotNull("Unable to detect SDK version");

        ExtensionVersion = SdkVersion;
        var sdkMatch = Regex.Match(SdkVersion, @"\d{2}(\d{2}).(\d).*");
        var waveMajorVersion = int.Parse(sdkMatch.Groups[1].Value + sdkMatch.Groups[2].Value);
        WaveVersionsRange = $"[{waveMajorVersion}.0, {waveMajorVersion + 1}.0)";
    }

    public static int Main () => Execute<Build>(x => x.Pack);

    [Parameter]
    readonly string Configuration = "Release";

    [Parameter]
    readonly string BootstrapVersion = "3";

    [Solution]
    readonly Solution Solution;

    Project Project => Solution.GetProject($"Resharper.Bootstrap{BootstrapVersion}.Templates");

    AbsolutePath ProjectFilePath => Project.Path;

    AbsolutePath OutputDirectory => Project.Directory / "bin" / Configuration;

    AbsolutePath SourceDirectory => RootDirectory / "src";

    string SdkVersion { get; }

    string ExtensionVersion { get; set; }

    string WaveVersionsRange { get; }

    Target AppendBuildNumber => _ => _
        .Requires(() => Host == HostType.AppVeyor)
        .Executes(() =>
        {
            ExtensionVersion = $"{ExtensionVersion}.{AppVeyor.Instance.BuildNumber}";
        });

    Target UpdateBuildVersion => _ => _
        .Requires(() => Host == HostType.AppVeyor)
        .DependsOn(AppendBuildNumber)
        .Executes(() =>
        {
            AppVeyor.Instance.UpdateBuildVersion(ExtensionVersion);
        });

    Target CompileTemplates => _ => _
        .Executes(() =>
        {
            var templatesPath = RootDirectory / "templates" / $"bs{BootstrapVersion}";
            var templatesInput = RootDirectory.GetRelativePathTo(templatesPath / "*.md");
            var templatesOutput = RootDirectory.GetRelativePathTo(OutputDirectory / "templates.dotSettings");
            var templatesReadme = RootDirectory.GetRelativePathTo(templatesPath / "README.md");
            var process = ProcessTasks.StartProcess(
                RootDirectory / "rstc" / "rstc.exe",
                $"compile -i {templatesInput} -o {templatesOutput} -r {templatesReadme}",
                RootDirectory);

            process.AssertZeroExitCode();
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(ProjectFilePath));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .After(AppendBuildNumber)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(ProjectFilePath)
                .SetConfiguration(Configuration)
                .SetVersionPrefix(ExtensionVersion)
                .SetOutputDirectory(OutputDirectory)
                .EnableNoRestore());
        });

    Target Pack => _ => _
        .DependsOn(Compile, CompileTemplates)
        .Executes(() =>
        {
            NuGetTasks.NuGetPack(s => s
                .SetTargetPath(BuildProjectDirectory / "Resharper.Bootstrap.Templates.nuspec")
                .SetVersion(ExtensionVersion)
                .SetBasePath(OutputDirectory)
                .AddProperty("bootstrapVersion", BootstrapVersion)
                .AddProperty("waveVersion", WaveVersionsRange)
                .SetOutputDirectory(RootDirectory));
        });

    Target UploadArtifact => _ => _
        .DependsOn(Pack)
        .Requires(() => Host == HostType.AppVeyor)
        .Executes(() =>
        {
            AppVeyor.Instance.PushArtifact($"Bootstrap{BootstrapVersion}.LiveTemplates.{ExtensionVersion}.nupkg");
        });
}