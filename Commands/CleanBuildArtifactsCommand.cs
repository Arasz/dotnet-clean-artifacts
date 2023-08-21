using Spectre.Console;
using Spectre.Console.Cli;

namespace DotnetClean.Commands;

public sealed class CleanBuildArtifactsCommand : AsyncCommand<CleanBuildArtifactsSettings>
{
    private readonly string[] _artifactDirectories =
    {
        "bin",
        "obj"
    };

    public override async Task<int> ExecuteAsync(CommandContext context, CleanBuildArtifactsSettings settings)
    {
        var startPath = settings.StartPath ?? Directory.GetCurrentDirectory();

        try
        {
            return settings.IsFast ? await FastMode(startPath) : await UserFriendlyMode(startPath);
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return -1;
        }
    }

    private ValueTask<int> FastMode(string startPath)
    {
        var directories = FindBuildArtifactDirectories(startPath);

        foreach (var directory in directories)
        {
            Directory.Delete(directory, true);
        }

        AnsiConsole.MarkupLine($"All [bold blue]{directories.Count}[/] build artifacts directories <[yellow]{string.Join(',', _artifactDirectories)}[/]> removed from the root path [bold yellow]{startPath}[/]");

        return ValueTask.FromResult(0);
    }


    private readonly TimeSpan _statusDisplayTime = TimeSpan.FromSeconds(2);
    private readonly TimeSpan _progressDisplayTime = TimeSpan.FromMilliseconds(100);

    private Task<int> UserFriendlyMode(string startPath) => AnsiConsole.Status()
        .Spinner(Spinner.Known.Bounce)
        .StartAsync($"Searching for build artifacts directories [yellow]{string.Join(',', _artifactDirectories)}[/] in [bold yellow]{startPath}[/]",
            async statusContext =>
            {
                await Task.Delay(_statusDisplayTime);

                var directories = FindBuildArtifactDirectories(startPath);

                statusContext.Status($"Search finished. Found [bold blue]{directories.Count}[/] artifact directories");
                await Task.Delay(_statusDisplayTime);

                var step = 0;
                foreach (var directory in directories)
                {
                    step++;

                    statusContext.Status($"Removing ({step}/{directories.Count}) [yellow]{directory}[/] directory");
                    await Task.Delay(_progressDisplayTime);

                    Directory.Delete(directory, true);
                }

                statusContext.Status($"All build directories removed");
                await Task.Delay(_statusDisplayTime);

                return 0;
            });

    private List<string> FindBuildArtifactDirectories(string startPath)
    {
        var directories = _artifactDirectories.Select(artifactDirectory => Directory.EnumerateDirectories(startPath,
                artifactDirectory,
                new EnumerationOptions
                {
                    MatchCasing = MatchCasing.PlatformDefault,
                    RecurseSubdirectories = true,
                    ReturnSpecialDirectories = false,
                    MaxRecursionDepth = 10
                }))
            .SelectMany(enumerable => enumerable)
            .ToList();
        return directories;
    }
}