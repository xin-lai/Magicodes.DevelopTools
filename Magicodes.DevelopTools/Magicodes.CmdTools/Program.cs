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
            var obj = Parser.Default.ParseArguments<CopyOptions, MoveOptions, OrderOptions>(args).Value;
            switch (obj.GetHashCode())
            {
                case 0:
                    {
                        var option = (CopyOptions)obj;
                        var sourcePath = IoHelper.GetAbsolutePath(option.Source);
                        var targetPath = IoHelper.GetAbsolutePath(option.Target);
                        Console.WriteLine("sourcePath：" + sourcePath);
                        Console.WriteLine("targetPath：" + targetPath);
                        if (string.IsNullOrWhiteSpace(option.Config))
                        {
                            Console.WriteLine("即将执行目录复制");
                            if (!option.Debug)
                            {
                                IoHelper.Copy(sourcePath, targetPath);
                            }
                            else
                            {
                                Console.WriteLine("sourcePath：" + sourcePath);
                                Console.WriteLine("targetPath：" + targetPath);
                                return;
                            }
                        }
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
                                Console.WriteLine("sourceFilePath：" + sourceFilePath);
                                Console.WriteLine("targetFilePath：" + targetFilePath);
                            }
                            else
                            {
                                IoHelper.Copy(sourceFilePath, targetFilePath);
                            }
                            Console.WriteLine("已复制：" + fileName);
                        }
                        break;
                    }

                case 2:
                    {
                        var option = (OrderOptions)obj;
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

                        break;
                    }
                default:
                    break;
            }

            Console.WriteLine("Magicodes.CmdTools执行完毕！");
            Console.ReadLine();
        }
    }
}
