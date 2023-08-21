using System.ComponentModel;
using Spectre.Console.Cli;

namespace DotnetClean.Commands;

public sealed class CleanBuildArtifactsSettings : CommandSettings
{
    [Description("A root directory with build artifacts. Defaults to current directory.")]
    [CommandArgument(0, "[artifactSearchRootPath]")]
    public string? StartPath { get; init; }
    
    [Description("Fast mode. Hides all messages displyed for a user")]
    [CommandOption("-f|--fast")]
    public bool IsFast { get; init; }
}