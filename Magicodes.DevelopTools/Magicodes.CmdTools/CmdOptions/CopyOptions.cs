using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.CmdTools.CmdOptions
{
    [Verb("copy", HelpText = "复制文件")]
    public class CopyOptions
    {
        [Option('s', "source", Required = true,
          HelpText = "源文件夹路径")]
        public string Source { get; set; }

        [Option('t', "target", Required = true,
          HelpText = "目标文件夹路径")]
        public string Target { get; set; }

        [Option('c', "config",
          HelpText = "配置文件路径")]
        public string Config { get; set; }

        [Option('d', "debug",
          HelpText = "调试模式")]
        public bool Debug { get; set; }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
