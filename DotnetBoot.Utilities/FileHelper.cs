using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotnetBoot.Utilities
{
    public static class FileHelper
    {
        public static bool CopyDirectory(string source, string target, bool overwrite, out string message)
        {
            try
            {
                message = null;
                return Copy(source, target, overwrite);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
        private static bool Copy(string source, string target, bool overwrite)
        {
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }
            string[] directories = Directory.GetDirectories(source);
            string[] files = Directory.GetFiles(source);
            foreach (var f in files)
            {
                string des = f.Replace(source, target);
                File.Copy(f, des, overwrite);
            }
            foreach (var d in directories)
            {
                Copy(d, d.Replace(source, target), overwrite);
            }
            return true;
        }
    }
}
