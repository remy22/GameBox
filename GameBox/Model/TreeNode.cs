using System;
using System.Collections.Generic;

namespace GameBox.Model
{
    internal class TreeNode
    {
        private List<TreeNode> childrenSections = new List<TreeNode>();
        private Dictionary<string, string> childrenLeaf = new Dictionary<string, string>();
        private string name = "noNamed";

        internal string Name
        {
            get { return name; }
            set { name = value; }
        }

        internal TreeNode()
        {
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
