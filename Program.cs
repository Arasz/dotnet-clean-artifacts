using DotnetClean.Commands;
using Spectre.Console.Cli;

var app = new CommandApp<CleanBuildArtifactsCommand>();
app.Configure(configurator =>
{
    configurator.Settings.ApplicationName = "clean-artifacts";
    configurator.Settings.ApplicationVersion = "1.0.1";
});
await app.RunAsync(args);