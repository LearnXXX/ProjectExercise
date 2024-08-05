using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace SpectreConsoleTest
{

    public class MyCommand : CommandSettings
    {
        [CommandOption("-m|--module")]
        [Description("Desctription test")]
        [CommandArgument(0,"[module]")]
        public ConsoleModule Module { get; set; }
    }

    public enum ConsoleModule
    {
        Markup,
        Table,
        BarChart,
        Calendar,
        Layout,
        RuleLine,
    }
}
