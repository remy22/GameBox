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
        private const char[] separators = { '=' };
        private Section mainConfig = new Section();
        private Stack<Section> childrenSections = new Stack<Section>();

        public Configuration()
        {
        }

        internal void SaveSection(string key, Section section,StreamWriter sw)
        {
            sw.WriteLine(SectionHeader + key);

            foreach (KeyValuePair<string, Section> s in section.Sections)
            {
                SaveSection(s.Key, s.Value, sw);
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
                    Section currentSection = mainConfig;
                    SaveSection("main", currentSection, sw);
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
                    Section currentSection = mainConfig;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        if (line.StartsWith(SectionHeader))
                        {
                            // Get the section name.
                            string sName = line.Substring(SectionHeader.Length);

                            if (sName == "main" && currentSection == mainConfig)
                            {
                            }
                            else
                            {
                                Section internalSection = new Section();
                                currentSection.AddChildSection(sName, internalSection);
                                childrenSections.Push(currentSection);
                                currentSection = internalSection;
                            }
                        }
                        else if (line.StartsWith(SectionClose))
                        {
                            currentSection = childrenSections.Pop();
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
