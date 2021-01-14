var target = Argument("target", "Default");
var bootstrapVersion = Argument("bootstrapVersion", "3");
var buildConfiguration = Argument("buildConfig", "Debug");
var projectPath = string.Format("src/Resharper.Bootstrap{0}.Templates/Resharper.Bootstrap{0}.Templates.csproj", bootstrapVersion);
var sdkVersion = XmlPeek(projectPath, "//PackageReference[@Include='JetBrains.ReSharper.SDK']/@Version");
Information($"Detected SDK {sdkVersion} version for project {projectPath}");

var extensionsVersion = sdkVersion;
var sdkMatch = System.Text.RegularExpressions.Regex.Match(sdkVersion, @"\d{2}(\d{2}).(\d).*");
var waveMajorVersion = int.Parse(sdkMatch.Groups[1].Value + sdkMatch.Groups[2].Value);
var waveVersion = Argument("wave", $"[{waveMajorVersion}.0, {waveMajorVersion + 1}.0)");

Task("AppendBuildNumber")
  .WithCriteria(BuildSystem.AppVeyor.IsRunningOnAppVeyor)
  .Does(() =>
{
	var buildNumber = BuildSystem.AppVeyor.Environment.Build.Number;
	extensionsVersion = string.Format("{0}.{1}", extensionsVersion, buildNumber);
});

Task("UpdateBuildVersion")
  .IsDependentOn("AppendBuildNumber")
  .WithCriteria(BuildSystem.AppVeyor.IsRunningOnAppVeyor)
  .Does(() =>
{
	BuildSystem.AppVeyor.UpdateBuildVersion(extensionsVersion);
});

Task("CompileTemplates")
  .Does(() =>
{
	var exitCodeWithArgument = 
		StartProcess("rstc/rstc.exe", string.Format("compile -i templates/bs{0}/*.md -o templates/bs{0}/templates.dotSettings -r templates/bs{0}/README.md", 
			bootstrapVersion));

    if (exitCodeWithArgument != 0) 
	{
		throw new Exception(string.Format("Template compilation fail. Code {0}", exitCodeWithArgument));
	}
});

Task("NugetRestore")
  .Does(() =>
{
	NuGetRestore("src/Resharper.BootstrapTemplates.sln");
});

Task("Build")
  .IsDependentOn("NugetRestore")
  .Does(() =>
{
	MSBuild(projectPath, new MSBuildSettings {
		Verbosity = Verbosity.Minimal,
		Configuration = buildConfiguration,
        ArgumentCustomization = args => args
                                    .Append($"/p:VersionPrefix={extensionsVersion}")
    });
});


Task("NugetPack")
  .IsDependentOn("CompileTemplates")
  .IsDependentOn("AppendBuildNumber")
  .IsDependentOn("Build")
  .Does(() =>
{
	 var buildPath = string.Format("./src/Resharper.Bootstrap{0}.Templates/bin/{1}", bootstrapVersion, buildConfiguration);

	 var files = new List<NuSpecContent>();
     files.Add(new NuSpecContent {Source = string.Format("{0}/Resharper.Bootstrap{1}.Templates.dll", buildPath, bootstrapVersion), Target = "dotFiles"});
     files.Add(new NuSpecContent {
		Source = string.Format("./templates/bs{0}/templates.dotSettings", bootstrapVersion), 
		Target = string.Format("DotFiles/Extensions/Bootstrap{0}.LiveTemplates/settings", bootstrapVersion)
	 });

     if (buildConfiguration == "Debug") 
     {
		files.Add(new NuSpecContent {Source = string.Format("{0}/Resharper.Bootstrap{1}.Templates.pdb", buildPath, bootstrapVersion), Target = "dotFiles"});
     }

     var nuGetPackSettings   = new NuGetPackSettings {
                                     Id                      = string.Format("Bootstrap{0}.LiveTemplates", bootstrapVersion),
                                     Version                 = extensionsVersion,
                                     Title                   = string.Format("Bootstrap {0} Live Templates", bootstrapVersion),
                                     Authors                 = new[] {"Oleg Shevchenko"},
                                     Owners                  = new[] {"Oleg Shevchenko"},
                                     Description             = string.Format("Bootstrap {0} Live Templates for quick markup generation", bootstrapVersion),
                                     ProjectUrl              = new Uri("https://github.com/olsh/resharper-bootstrap-templates"),
                                     IconUrl                 = new Uri("https://raw.githubusercontent.com/olsh/resharper-bootstrap-templates/master/images/bootstrap3-logo.png"),
                                     LicenseUrl              = new Uri("https://github.com/olsh/resharper-bootstrap-templates/raw/master/LICENSE"),
                                     Tags                    = new [] {"resharper", "bootstrap", string.Format("bootstrap{0}", bootstrapVersion), "templates"},
                                     RequireLicenseAcceptance= false,
                                     Symbols                 = false,
                                     NoPackageAnalysis       = true,
                                     Files                   = files,
                                     OutputDirectory         = ".",
									 Dependencies            = new [] { new NuSpecDependency() { Id = "Wave", Version = waveVersion } },
									 ReleaseNotes            = new [] { "https://github.com/olsh/resharper-bootstrap-templates/releases" }
                                 };

     NuGetPack(nuGetPackSettings);
});

Task("CreateArtifact")
  .IsDependentOn("NugetPack")
  .WithCriteria(BuildSystem.AppVeyor.IsRunningOnAppVeyor)
  .Does(() =>
{
	BuildSystem.AppVeyor.UploadArtifact(string.Format("Bootstrap{0}.LiveTemplates.{1}.nupkg", bootstrapVersion, extensionsVersion));
});

Task("Default")
	.IsDependentOn("NugetPack");

RunTarget(target);
