using System;
using System.Collections.Generic;
using System.IO;

namespace GameBox.Model
{
    internal class TreeNode
    {
        private static char[] separators = { '=' };
        private List<TreeNode> childrenSections = new List<TreeNode>();
        private Dictionary<string, string> childrenLeaf = new Dictionary<string, string>();
        private const string SectionHeader = "section::";
        private const string SectionClose = "::";

        private string name = "noNamed";

        internal string Name
        {
            get { return name; }
            set { name = value; }
        }

        internal TreeNode()
        {
        }

        internal TreeNode Node(string name_)
        {
            return childrenSections.Find(x => x.Name == name_);
        }

        internal string Property(string name_)
        {
            string val;
            childrenLeaf.TryGetValue(name_, out val);
            return val;
        }

        internal void SaveSection(StreamWriter sw, uint tab = 0)
        {
            string indent = "";
            for (int i = 0; i < tab; i++)
            {
                indent += "\t";
            }
            string indentProp = indent + "\t";
            sw.WriteLine(indent + SectionHeader + name);

            foreach (TreeNode s in Sections)
            {
                s.SaveSection(sw,tab+1);
            }

            foreach (KeyValuePair<string, string> s in Leafs)
            {
                sw.WriteLine(indentProp + s.Key + "=" + s.Value);
            }

            sw.WriteLine(indent + SectionClose);

        }

        internal void LoadSection(StreamReader sr)
        {
            string line = "";
            bool SectionClosed = false;

            while (line != null && !SectionClosed)
            {
                line = sr.ReadLine();
                if (line != null)
                {
                    Console.WriteLine(line);
                    line = line.Trim();
                    if (line.StartsWith(SectionHeader))
                    {
                        // Get the section name.
                        string sName = line.Substring(SectionHeader.Length);

                        TreeNode internalSection = new TreeNode { Name = sName };
                        AddChildSection(internalSection);
                        internalSection.LoadSection(sr);
                    }
                    else if (line.StartsWith(SectionClose))
                    {
                        SectionClosed = true;
                    }
                    else
                    {
                        string[] data = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        data[0] = data[0].Trim();
                        data[1] = data[1].Trim();
                        AddLeaf(data[0], data[1]);
                    }
                }
            }

        }

        internal TreeNode AddChildSection(TreeNode newSection)
        {
            childrenSections.Add(newSection);
            return newSection;
        }

        internal void AddLeaf(string name_)
        {
            AddLeaf(name_, default(string));
        }

        internal void AddLeaf(string name_, string value_)
        {
            childrenLeaf.Add(name_, value_);
        }

        internal string GetLeaf(string name_)
        {
            return childrenLeaf[name_];
        }

        internal TreeNode GetSection(string name_)
        {
            foreach (TreeNode s in childrenSections)
                if (s.name.Equals(name_))
                    return s;

            return null;
        }

        internal List<TreeNode> Sections
        {
            get { return childrenSections; }
        }

        internal Dictionary<string, string> Leafs
        {
            get { return childrenLeaf; }
        }
    }
}
