using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Rendering;

namespace SpectreConsoleTest
{
    internal class CommandRunner : Command<MyCommand>
    {
        public override int Execute(CommandContext context, MyCommand settings)
        {
            switch (settings.Module)
            {
                case ConsoleModule.Markup:
                    MarkupOutputTest();
                    return 0;

                case ConsoleModule.Table:
                    TableOutputTest();
                    return 0;

                case ConsoleModule.Calendar:
                    CalendarOutputTest();
                    return 0;

                case ConsoleModule.BarChart:
                    BarChartOutputTest();
                    return 0;

                case ConsoleModule.Layout:
                    LayoutOutputTest();
                    return 0;

                case ConsoleModule.RuleLine:
                    RuleLineOutputTest();
                    return 0;

                default:
                    return 0;
            }
        }
        private void RuleLineOutputTest()
        {
            var rule = new Rule("[red]Hello rule line[/]");
            AnsiConsole.Write(rule);

            var ruleLeft = new Rule("[blue]Hello rule left line[/]");
            ruleLeft.Justification = Justify.Left;
            AnsiConsole.Write(ruleLeft);

            var ruleRight = new Rule("[blue]Hello rule right line[/]");
            ruleRight.Justification = Justify.Right;
            AnsiConsole.Write(ruleRight);

        }

        private void LayoutOutputTest()
        {
            var layout = new Layout("Root")
                .SplitColumns(new Layout("left"), new Layout("right").SplitRows(new Layout("Top"), new Layout("Bottom")));

            layout["left"].Update(new Panel(Align.Center(new Markup("[green]Hello[/]"), VerticalAlignment.Middle)).Expand());

            layout["right"]["Top"].Update(new Panel(Align.Center(new Markup("[red]Hello right top[/]"), VerticalAlignment.Middle)).Expand());
            layout["right"]["Bottom"].Update(new Panel(Align.Center(new Markup("[yellow]Hello bottom top[/]"), VerticalAlignment.Middle)).Expand());

            AnsiConsole.Write(layout);

        }

        private void CalendarOutputTest()
        {
            var calendar = new Calendar(2024, 08);
            AnsiConsole.Write(calendar);
        }

        private void BarChartOutputTest()
        {
            var barChart = new BarChart();
            barChart.Width = 60;
            barChart.Label("[green]BarChart[/]");
            barChart.CenterLabel();
            barChart.AddItem("bar1", 10, Color.Yellow);
            barChart.AddItem("bar2", 50, Color.Green);
            barChart.AddItem("bar3", 29, Color.Blue);
            barChart.AddItem("bar4", 49, Color.Red);
            AnsiConsole.Write(barChart);
        }
        private void MarkupOutputTest()
        {
            AnsiConsole.MarkupLine("[underline red]Hello[/][Blue] Spectre.Console[/]");
        }

        private void TableOutputTest()
        {
            var table = new Table();

            //Column defination
            table.AddColumn("[red]Column1[/]");
            table.AddColumn("[green]Column2[/]");
            table.AddColumn("[blue]Column3[/]");

            //Column value

            table.AddRow("value1_1", "value1_2", "value1_3");
            table.AddRow("value2_1", "value2_2", "value2_3");
            table.AddRow("value3_1", "value3_2", "value3_3");
            table.AddRow("value4_1", "value4_2", "value4_3");

            AnsiConsole.Write(table);
        }




    }
}
