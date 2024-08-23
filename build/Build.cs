using System.IO;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    [Solution(GenerateProjects = true)] 
    readonly Solution Solution;

    Project ServerProject => Solution.Ui.Server.Innkeep_Server;
    
    Project ClientProject => Solution.Ui.Client.Innkeep_Client;

    static AbsolutePath SourceDirectory => RootDirectory / "source";

    static AbsolutePath TestsDirectory => RootDirectory / "tests";

    static AbsolutePath OutputDirectory => RootDirectory / "release";
    
    static AbsolutePath OutputDirectoryServer => OutputDirectory / "server";
    
    static AbsolutePath OutputDirectoryClient => OutputDirectory / "client";
    
    public static int Main () => Execute<Build>(x => x.Publish);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = Configuration.Release;

    Target Clean => definition => definition
        .Before(Restore)
        .Executes(() =>
        {
            foreach(var path in SourceDirectory.GlobDirectories("**/bin", "**/obj"))
            {
                path.DeleteDirectory();
            }
            
            foreach(var path in TestsDirectory.GlobDirectories("**/bin", "**/obj"))
            {
                path.DeleteDirectory();
            }

            OutputDirectory.CreateOrCleanDirectory();
        });

    Target Restore => definition => definition
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(ServerProject));
            
            DotNetRestore(s => s
                .SetProjectFile(ClientProject));
        });

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
        });

    Target Publish => definition => definition
         .DependsOn(Compile)
         .Executes(() =>
             {
                 DotNetPublish(s => s
                    .SetProject(ServerProject)
                    .SetOutput(OutputDirectoryServer)
                    .SetConfiguration(Configuration.Release)
                    .SetPublishSingleFile(true)
                    .SetSelfContained(true)
                    .SetPublishTrimmed(false)
                 );
                 
                 DotNetPublish(s => s
                    .SetProject(ClientProject)
                    .SetOutput(OutputDirectoryClient)
                    .SetConfiguration(Configuration.Release)
                    .SetPublishSingleFile(true)
                    .SetSelfContained(true)
                    .SetPublishTrimmed(false)
                 );
                 
             }
         );

}
