using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.CmdTools.CmdOptions
{
    [Verb("order", HelpText = "排序（支持对文本的排序）")]
    public class OrderOptions
    {
        [Option('s', "source", Required = true,
          HelpText = "源路径")]
        public string Source { get; set; }

        [Option('t', "target", HelpText = "目标路径，为空则使用源路径")]
        public string Target { get; set; }



        public override int GetHashCode()
        {
            return 2;
        }
    }
}
