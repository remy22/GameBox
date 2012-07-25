using System;
using System.Collections.Generic;
using System.IO;
using GameBox.Model;

namespace GameBox.InternalServices
{
    internal class Configuration : Singleton<Configuration>
    {
        private const string SectionHeader = "section::";
        private const string SectionClose = "::";
        private char[] separators = { '=' };
        private char[] sectionAddressSeparators = { '/' };
        private char[] leafAddressSeparators = { '.' };
        private TreeNode mainConfig = new TreeNode();
        private Stack<TreeNode> childrenSections = new Stack<TreeNode>();

        public Configuration()
        {
        }

        internal void SaveSection(TreeNode section,StreamWriter sw)
        {
            sw.WriteLine(SectionHeader + section.Name);

            foreach (TreeNode s in section.Sections)
            {
                SaveSection(s, sw);
            }

            foreach (KeyValuePair<string, string> s in section.Leafs)
            {
                sw.WriteLine(s.Key + "=" + s.Value);
            }

        }

        internal void SaveConfig()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("config.cfg"))
                {
                    TreeNode currentSection = mainConfig;
                    SaveSection(currentSection, sw);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be written:");
                Console.WriteLine(e.Message);
            }
        }

        internal override void Init()
        {
            base.Init();
            try
            {
                using (StreamReader sr = new StreamReader("config.cfg"))
                {
                    string line;
                    TreeNode currentSection = null;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        if (line.StartsWith(SectionHeader))
                        {
                            // Get the section name.
                            string sName = line.Substring(SectionHeader.Length);

                            TreeNode internalSection = new TreeNode{ Name = sName };
                            currentSection.AddChildSection(internalSection);
                            childrenSections.Push(currentSection);
                            currentSection = internalSection;
                        }
                        else if (line.StartsWith(SectionClose))
                        {
                            if (childrenSections.Count > 1)
                                currentSection = childrenSections.Pop();
                            else if (childrenSections.Count == 1)
                                mainConfig = childrenSections.Pop();
                        }
                        else
                        {
                            string[] data = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            currentSection.AddLeaf(data[0], data[1]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        internal override void Destroy()
        {

            base.Destroy();
        }
    }
}
