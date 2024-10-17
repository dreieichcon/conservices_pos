using System;
using System.IO;
using System.Management.Automation;
using Innkeep.Strings;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{

	[Nuke.Common.Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
	readonly Configuration Configuration = Configuration.Release;

	/// Support plugins are available for:
	/// - JetBrains ReSharper        https://nuke.build/resharper
	/// - JetBrains Rider            https://nuke.build/rider
	/// - Microsoft VisualStudio     https://nuke.build/visualstudio
	/// - Microsoft VSCode           https://nuke.build/vscode
	[Solution(GenerateProjects = true)]
	readonly Solution Solution;

	Project ServerProject => Solution.Ui.Server.Innkeep_Server;

	Project ClientProject => Solution.Ui.Client.Innkeep_Client;

	static AbsolutePath SourceDirectory => RootDirectory / "source";

	static AbsolutePath TestsDirectory => RootDirectory / "tests";

	static AbsolutePath OutputDirectory => RootDirectory / "release";

	static AbsolutePath OutputDirectoryServer => OutputDirectory / "server";

	static AbsolutePath OutputDirectoryClient => OutputDirectory / "client";

	static AbsolutePath BuildDirectoryClient => OutputDirectoryClient / "build";

	static AbsolutePath BuildDirectoryServer => OutputDirectoryServer / "build";

	static AbsolutePath PackDirectoryClient => OutputDirectoryClient / "pack";

	static AbsolutePath PackDirectoryServer => OutputDirectoryServer / "pack";

	Target Clean => definition => definition
								.Before(Restore)
								.Executes(() =>
									{
										foreach (var path in SourceDirectory.GlobDirectories("**/bin", "**/obj"))
											path.DeleteDirectory();

										foreach (var path in TestsDirectory.GlobDirectories("**/bin", "**/obj"))
											path.DeleteDirectory();

										BuildDirectoryClient.CreateOrCleanDirectory();
										BuildDirectoryServer.CreateOrCleanDirectory();
									}
								);

	Target Restore => definition => definition
									.DependsOn(Clean)
									.Executes(() =>
										{
											DotNetRestore(s => s
												.SetProjectFile(ServerProject)
											);

											DotNetRestore(s => s
												.SetProjectFile(ClientProject)
											);
										}
									);

	Target Compile => definition => definition
									.DependsOn(Restore)
									.Executes(() =>
										{
											DotNetBuild(s => s
															.SetProjectFile(ServerProject)
															.SetConfiguration(Configuration)
															.EnableNoRestore()
											);

											DotNetBuild(s => s
															.SetProjectFile(ClientProject)
															.SetConfiguration(Configuration)
															.EnableNoRestore()
											);
										}
									);

	Target Publish => definition => definition
									.DependsOn(Compile)
									.Executes(() =>
										{
											DotNetPublish(s => s
																.SetProject(ServerProject)
																.SetOutput(BuildDirectoryServer)
																.SetConfiguration(Configuration.Release)
																.SetPublishSingleFile(true)
																.SetSelfContained(true)
																.SetPublishTrimmed(false)
											);

											Directory.CreateDirectory(Path.Combine(BuildDirectoryServer, "log"));
											Directory.CreateDirectory(Path.Combine(BuildDirectoryServer, "cert"));

											DotNetPublish(s => s
																.SetProject(ClientProject)
																.SetOutput(BuildDirectoryClient)
																.SetConfiguration(Configuration.Release)
																.SetPublishSingleFile(true)
																.SetSelfContained(true)
																.SetPublishTrimmed(false)
											);

											Directory.CreateDirectory(Path.Combine(BuildDirectoryClient, "log"));
											Directory.CreateDirectory(Path.Combine(BuildDirectoryClient, "cert"));
										}
									);

	Target Pack => definition => definition
								.DependsOn(Publish)
								.Executes(() =>
									{
										Console.WriteLine("Packing Client");
										PackClient();

										Console.WriteLine("Packing Server");
										PackServer();
									}
								);

	static void PackClient()
		=> PackInternal(OutputDirectoryClient, "Innkeep.Client");

	static void PackServer()
		=> PackInternal(OutputDirectoryServer, "Innkeep.Server");

	static void PackInternal(AbsolutePath outputDirectory, string packId)
	{
		using var ps = PowerShell.Create();

		var cdCommand = $"cd \"{outputDirectory}\"";

		var packCommand =
			$@"vpk pack --packId {packId} --packVersion {AppVersion.Version} --packDir .\build --mainExe {packId}.exe --outputDir .\pack";

		ps.AddScript(cdCommand);
		ps.AddScript(packCommand);

		var results = ps.Invoke();

		foreach (var result in results)
			Console.WriteLine(result);
	}

	public static int Main() => Execute<Build>(x => x.Pack);
}