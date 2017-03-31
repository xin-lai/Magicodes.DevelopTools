using CommandLine;
using Magicodes.CmdTools.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Magicodes.CmdTools.CmdOptions
{
    [Verb("copy", HelpText = "复制文件")]
    public class CopyOptions : ICmdAction
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

        [Option('i', "ignore",
         HelpText = "忽略的文件名&扩展名&目录名（多个请用分号分割）")]
        public string Ignore { get; set; }

        [Option('d', "debug",
          HelpText = "调试模式")]
        public bool Debug { get; set; }

       
        public void Execute(object agrs)
        {
            var option = agrs as CopyOptions;
            if (option == null)
            {
                Console.WriteLine("没有参数，无法执行！");
                return;
            }
            var sourcePath = IoHelper.GetAbsolutePath(option.Source);
            var targetPath = IoHelper.GetAbsolutePath(option.Target);
            Console.WriteLine("sourcePath：" + sourcePath);
            Console.WriteLine("targetPath：" + targetPath);
            if (string.IsNullOrEmpty(option.Ignore))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("没有设置忽略文件，所有文件都将被复制！");
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.WriteLine("即将忽略以下内容：" + option.Ignore);
            }
            if (string.IsNullOrWhiteSpace(option.Config))
            {
                Console.WriteLine("即将执行复制");
                if (!option.Debug)
                {

                    IoHelper.Copy(sourcePath, targetPath, option.Ignore);
                }
                else
                {
                    Console.WriteLine("已启用Debug模式，不会执行复制");
                    return;
                }
            }
            else
            {
                var configPath = IoHelper.GetAbsolutePath(option.Config);
                if (!File.Exists(configPath))
                {
                    Console.WriteLine("configPath不存在：" + configPath);
                    Console.ReadLine();
                    return;
                }
                var lines = File.ReadAllLines(configPath);
                foreach (var item in lines)
                {
                    var fileName = item.Trim();
                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        continue;
                    }
                    fileName = fileName.Replace("/", "\\");
                    var sourceFilePath = Path.Combine(sourcePath, fileName);
                    var targetFilePath = Path.Combine(targetPath, fileName);
                    if (option.Debug)
                    {
                        Console.WriteLine("已启用Debug模式，不会执行复制");
                    }
                    else
                    {
                        IoHelper.Copy(sourceFilePath, targetFilePath, option.Ignore);
                    }
                    Console.WriteLine("已复制：" + fileName);
                }
            }
        }

    }
}
