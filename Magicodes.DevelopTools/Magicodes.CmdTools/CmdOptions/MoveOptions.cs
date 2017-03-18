using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.CmdTools.CmdOptions
{
    [Verb("move", HelpText = "移动文件")]
    public class MoveOptions
    {

        public override int GetHashCode()
        {
            return 1;
        }
    }
}
