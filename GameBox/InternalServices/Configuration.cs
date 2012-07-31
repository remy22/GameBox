using System;
using System.Collections.Generic;
using System.IO;
using GameBox.Model;

namespace GameBox.InternalServices
{
    internal static class Configuration
    {
        private static char[] sectionAddressSeparators = { '/' };
        private static char[] leafAddressSeparators = { '.' };
        private static TreeNode mainConfig = new TreeNode();
        private static Stack<TreeNode> childrenSections = new Stack<TreeNode>();

        internal static void SaveConfig()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("config2.cfg"))
                {
                    mainConfig.SaveSection(sw);
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be written:");
                Console.WriteLine(e.Message);
            }
        }

        internal static void Init()
        {
            mainConfig = new TreeNode { Name = "SystemConfig" };
            try
            {
                using (StreamReader sr = new StreamReader("config.cfg"))
                {
                    mainConfig.LoadSection(sr);
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        internal static void Destroy()
        {
            SaveConfig();
        }
    }
}
