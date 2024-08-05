// See https://aka.ms/new-console-template for more information




using Spectre.Console;
using Spectre.Console.Cli;
using SpectreConsoleTest;


var app = new CommandApp<CommandRunner>();
//app.Configure(config =>
//{
//    config.AddBranch<MyCommand>("module", module =>
//    {
//        module.AddCommand<CommandRunner>("runner");
//    });

//    //config.AddCommand<CommandRunner>("module")
//    //.WithAlias("module-test")
//    //.WithDescription("This is a test for Spectre.Console command")
//    //.WithExample(new[] { "module", "--module" });
//});
var UnicodeBar = '█';
var AsciiBar = '█';
Console.WriteLine(UnicodeBar);
Console.WriteLine(AsciiBar);

while (true)
{
    AnsiConsole.MarkupLine("[green]Please input command[/]");
    var command = Console.ReadLine();
    if (command != null)
    {
        var arguments = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        app.Run(arguments);
    }
}