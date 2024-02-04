var target = Argument("target", "Build");
var configuration = Argument("configuration", "Release");

var solutionFolder = "./";

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
    {
        var binPattern = $"{solutionFolder}**/bin/{configuration}";
        var objPattern = $"{solutionFolder}**/obj/{configuration}";

        var outputDirs = GetDirectories(binPattern);
        outputDirs.Add(GetDirectories(objPattern));

        foreach(var dir in outputDirs)
        {
            CleanDirectory(dir.FullPath);
        }
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>{
        DotNetRestore(solutionFolder);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetBuild(solutionFolder, new DotNetBuildSettings
        {
            Configuration = configuration,
            NoRestore = true
        });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var settings = new DotNetTestSettings
        {
            Configuration = configuration,
            NoBuild = true,
            NoRestore = true
        };

        var projectFiles = GetFiles($"{solutionFolder}/*Tests.csproj");

        foreach(var file in projectFiles)
        {
            DotNetTest(file.FullPath, settings);
        }
    });

RunTarget(target);