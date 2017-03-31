using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Magicodes.CmdTools.Helper
{
    public static class IoHelper
    {
        /// <summary>  
        /// 路径分割符  
        /// </summary>  
        private const string PATH_SPLIT_CHAR = "\\";

        private static Dictionary<string, IQueryable<string>> ignoreDic = new Dictionary<string, IQueryable<string>>();

        /// <summary>
        /// 是否忽略此文件的复制
        /// </summary>
        /// <param name="ignore">忽略字符串，多个以“；”分割，支持目录忽略</param>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        public static bool IsIgnore(string ignore, string filePath)
        {
            var flag = false;
            Console.ForegroundColor = ConsoleColor.Red;
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("路径为空，已忽略！");
                flag = true;
            }
            else
            {
               
                if (!string.IsNullOrEmpty(ignore))
                {
                    var q = ignoreDic.ContainsKey(ignore) ? ignoreDic[ignore] : ignore.Split(';').Distinct().Where(p => !string.IsNullOrWhiteSpace(p)).AsQueryable();
                    if (!ignoreDic.ContainsKey(ignore))
                        ignoreDic.Add(ignore, q);
                    var ext = Path.GetExtension(filePath);
                    if (q.Any(p => p.Equals(ext, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        Console.WriteLine("已忽略：" + ext);
                        flag = true;
                    }
                    else if (q.Any(p => p.Equals(Path.GetFileName(filePath), StringComparison.CurrentCultureIgnoreCase)))
                    {
                        Console.WriteLine("已忽略：" + Path.GetFileName(filePath));
                        flag = true;
                    }
                    var dirs = q.Where(p => p.IndexOf(".") == -1);
                    if (dirs.Count() > 0)
                    {
                        foreach (var item in dirs)
                        {
                            if (filePath.Contains(item))
                            {
                                Console.WriteLine("已忽略：" + item);
                                flag = true;
                                break;
                            }
                        }
                    }
                }
               
            }
            Console.ForegroundColor = ConsoleColor.Black;
            return flag;
        }

        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetAbsolutePath(string path)
        {
            if (path.StartsWith("."))
            {
                return Path.GetFullPath(Path.Combine(System.Environment.CurrentDirectory, path));
            }
            return path;
        }

        public static void Copy(string sourceFilePath, string targetFilePath, string ignore)
        {
            if (Directory.Exists(sourceFilePath))
            {
                if (!Directory.Exists(targetFilePath))
                    Directory.CreateDirectory(targetFilePath);
                if (IsIgnore(ignore, targetFilePath))
                    return;
                CopyFiles(sourceFilePath, targetFilePath, true, true, ignore);
            }
            else if (File.Exists(sourceFilePath))
            {
                var path = Path.GetDirectoryName(targetFilePath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                targetFilePath = Path.Combine(targetFilePath, Path.GetFileName(sourceFilePath));
                if (IsIgnore(ignore, targetFilePath))
                    return;
                File.Copy(sourceFilePath, targetFilePath, true);
            }
            else
            {
                Console.WriteLine("路径不存在：" + sourceFilePath);
            }
        }

        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite, bool copySubDir, string ignore)
        {

            //复制当前目录文件  
            foreach (string sourceFileName in Directory.GetFiles(sourceDir))
            {
                string targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                if (IsIgnore(ignore, targetFileName))
                    continue;
                if (File.Exists(targetFileName))
                {
                    if (overWrite == true)
                    {
                        File.SetAttributes(targetFileName, FileAttributes.Normal);
                        File.Copy(sourceFileName, targetFileName, overWrite);
                    }
                }
                else
                {
                    File.Copy(sourceFileName, targetFileName, overWrite);
                }
            }
            //复制子目录  
            if (copySubDir)
            {
                foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    string targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                    if (IsIgnore(ignore, targetSubDir))
                        continue;
                    if (!Directory.Exists(targetSubDir))
                        Directory.CreateDirectory(targetSubDir);
                    CopyFiles(sourceSubDir, targetSubDir, overWrite, true, ignore);
                }
            }
        }


    }
}
