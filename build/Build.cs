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
using static Nuke.Common.Tools.NuGet.NuGetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Pack);

    protected override void OnBuildInitialized()
    {
        var sdkVersion = XmlPeekSingle(
            Project.Path,
            "//PackageReference[@Include='JetBrains.ReSharper.SDK']/@Version");
        sdkVersion.NotNull("Unable to detect SDK version");

        ExtensionVersion = AppVeyor == null ? sdkVersion : $"{sdkVersion}.{AppVeyor.BuildNumber}";
        var sdkMatch = Regex.Match(sdkVersion, @"\d{2}(\d{2}).(\d).*");
        var waveMajorVersion = int.Parse(sdkMatch.Groups[1].Value + sdkMatch.Groups[2].Value);
        WaveVersionsRange = $"[{waveMajorVersion}.0, {waveMajorVersion + 1}.0)";

        base.OnBuildInitialized();
    }

    [CI]
    readonly AppVeyor AppVeyor;

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

    string ExtensionVersion { get; set; }

    string WaveVersionsRange { get; set; }

    Target UpdateBuildVersion => _ => _
        .Requires(() => AppVeyor)
        .Executes(() =>
        {
            AppVeyor.Instance.UpdateBuildVersion(ExtensionVersion);
        });

    [LocalExecutable("./rstc/rstc.exe")] readonly Tool Rstc;

    Target CompileTemplates => _ => _
        .Executes(() =>
        {
            var templatesPath = RootDirectory / "templates" / $"bs{BootstrapVersion}";
            var templatesInput = RootDirectory.GetRelativePathTo(templatesPath / "*.md");
            var templatesOutput = RootDirectory.GetRelativePathTo(OutputDirectory / "templates.dotSettings");
            var templatesReadme = RootDirectory.GetRelativePathTo(templatesPath / "README.md");
            Rstc($"compile -i {templatesInput} -o {templatesOutput} -r {templatesReadme}");
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(ProjectFilePath));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
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
            NuGetPack(s => s
                .SetTargetPath(BuildProjectDirectory / "Resharper.Bootstrap.Templates.nuspec")
                .SetVersion(ExtensionVersion)
                .SetBasePath(OutputDirectory)
                .AddProperty("bootstrapVersion", BootstrapVersion)
                .AddProperty("waveVersion", WaveVersionsRange)
                .SetOutputDirectory(RootDirectory));
        });

    Target UploadArtifact => _ => _
        .DependsOn(Pack)
        .Requires(() => AppVeyor)
        .Executes(() =>
        {
            AppVeyor.PushArtifact($"Bootstrap{BootstrapVersion}.LiveTemplates.{ExtensionVersion}.nupkg");
        });
}
