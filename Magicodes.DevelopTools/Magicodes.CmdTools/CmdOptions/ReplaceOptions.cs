using CommandLine;
using Magicodes.CmdTools.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Magicodes.CmdTools.CmdOptions
{
    [Verb("replace", HelpText = "替换文件或文本")]
    public class ReplaceOptions : ICmdAction
    {
        [Option('p', "path", Required = true,
          HelpText = "目录或文件路径")]
        public string FileOrDirPath { get; set; }

        [Option('s', "source", Required = true,
          HelpText = "源字符串（多个请以空格分隔）")]
        public string Source { get; set; }

        [Option('t', "target", Required = true,
          HelpText = "目标字符串（多个请以空格分隔）")]
        public string Target { get; set; }

        [Option('i', "replacetext", HelpText = "是否替换文本")]
        public bool IsReplaceText { get; set; }

        private static string[] SourceArray { get; set; }
        private static string[] TargetArray { get; set; }

        public void Execute(object agrs)
        {
            var option = agrs as ReplaceOptions;
            if (option == null)
            {
                Console.WriteLine("没有参数，无法执行！");
                return;
            }
            if (string.IsNullOrWhiteSpace(option.Source))
            {
                Console.WriteLine("请设置源！");
                return;
            }
            if (string.IsNullOrWhiteSpace(option.Target))
            {
                Console.WriteLine("请设置目标！");
                return;
            }
            SourceArray = option.Source.Split(' ');
            TargetArray = option.Target.Split(' ');
            if (SourceArray.Length != TargetArray.Length)
            {
                Console.WriteLine("目标和源不匹配！");
                return;
            }

            var path = IoHelper.GetAbsolutePath(option.FileOrDirPath);
            if (File.Exists(path))
            {
                ReplaceFile(option, path);
            }
            else
            {
                ReplaceDirectory(option, path);
            }
        }

        private void ReplaceDirectory(ReplaceOptions option, string path)
        {
            if (Directory.Exists(path))
            {
                var dirInfo = new DirectoryInfo(path);
                var files = dirInfo.GetFiles();
                //替换文件以及内容
                foreach (var item in files)
                {
                    ReplaceFile(option, item.FullName);
                }
                //替换子目录以及子目录文件
                var dirs = dirInfo.GetDirectories();
                foreach (var item in dirs)
                {
                    var target = Path.Combine(item.Parent.FullName, Replace(option, item.Name));
                    if (item.FullName != target)
                        item.MoveTo(target);
                    ReplaceDirectory(option, item.FullName);
                }
            }
            else
            {
                Console.WriteLine("路径：" + path + "不存在！");
            }
        }
        //重命名文件以及替换文件内容
        private void ReplaceFile(ReplaceOptions option, string path)
        {
            if (option.IsReplaceText)
            {
                var content = File.ReadAllText(path);
                content = Replace(option, content);
                File.WriteAllText(path, content);
            }
            var fileName = Path.GetFileName(path);
            fileName = Replace(option, fileName);
            var target = Path.Combine(Path.GetDirectoryName(path), fileName);
            if (path != target)
                File.Move(path, target);
        }
        /// <summary>
        /// 替换内容
        /// </summary>
        /// <param name="option"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private string Replace(ReplaceOptions option, string text)
        {

            for (int i = 0; i < SourceArray.Length; i++)
            {
                text = text.Replace(SourceArray[i], TargetArray[i]);
            }
            return text;
        }
    }
}
