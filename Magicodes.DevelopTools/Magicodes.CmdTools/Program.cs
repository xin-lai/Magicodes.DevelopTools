using CommandLine;
using Magicodes.CmdTools.CmdOptions;
using Magicodes.CmdTools.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Magicodes.CmdTools
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = Parser.Default.ParseArguments<CopyOptions, OrderOptions>(args).Value;
            CmdActionsManager.Init();
            CmdActionsManager.ExecuteAction(obj);
            Console.WriteLine("Magicodes.CmdTools执行完毕！");
        }
    }
}
