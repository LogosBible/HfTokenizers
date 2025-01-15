var platform = BuildEnvironment.IsWindows() ? "win-x64" :
	BuildEnvironment.IsLinux() ? "linux-x64" :
	BuildEnvironment.IsMacOS() ? "osx-x64" :
	throw new InvalidOperationException("Could not determine platform.");

return BuildRunner.Execute(args, build =>
{
	build.AddDotNetTargets(
		new DotNetBuildSettings
		{
			SolutionPlatform = platform,
			NuGetApiKey = Environment.GetEnvironmentVariable("NUGET_API_KEY"),
		});

	build.Target("build-rust").Does(() => RunCargo("build"));

	build.Target("test-rust").Does(() => RunCargo("test"));
});

void RunCargo(string command)
{
	RunApp("cargo",
		new AppRunnerSettings
		{
			Arguments = [command, "--release", "--target-dir", Path.Combine("target", platform)],
			WorkingDirectory = Path.Combine("rust", "logoshft"),
		});
}
