using CommandLine;
using CommandLine.Text;
using Magicodes.CmdTools.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Magicodes.CmdTools.CmdOptions
{
    [Verb("order", HelpText = "排序（支持对文本的排序）")]
    public class OrderOptions: ICmdAction
    {
        [Option('s', "source", Required = true,
          HelpText = "源路径")]
        public string Source { get; set; }

        [Option('t', "target", HelpText = "目标路径，为空则使用源路径")]
        public string Target { get; set; }

        public void Execute(object agrs)
        {
            var option = (OrderOptions)agrs;
            var sourcePath = IoHelper.GetAbsolutePath(option.Source);
            if (!File.Exists(sourcePath))
            {
                Console.WriteLine("未找到文件：" + sourcePath);
                return;
            }
            var lines = File.ReadAllLines(sourcePath);
            var orders = lines.Select(p => p.Trim().Trim('"').Replace("\\", "/")).Where(p => !string.IsNullOrWhiteSpace(p)).Distinct().OrderBy(p => p).ToArray();
            if (string.IsNullOrWhiteSpace(option.Target))
            {
                File.WriteAllLines(sourcePath, orders);
            }
            else
            {
                var targetPath = IoHelper.GetAbsolutePath(option.Target);
                File.WriteAllLines(targetPath, orders);
            }
        }
    }
}
