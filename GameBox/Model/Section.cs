using System;
using System.Collections.Generic;

namespace GameBox.Model
{
    internal class Section
    {
        private Dictionary<string, Section> childrenSections = new Dictionary<string, Section>();
        private Dictionary<string, string> childrenLeaf = new Dictionary<string, string>();

        internal Section()
        {
        }

        internal void AddChildSection(string name_)
        {
            AddChildSection(name_, new Section());
        }

        internal void AddChildSection(string name_, Section newSection)
        {
            childrenSections.Add(name_, newSection);
        }

        internal void AddLeaf(string name_)
        {
            AddLeaf(name_, "");
        }

        internal void AddLeaf(string name_, string value_)
        {
            childrenLeaf.Add(name_, value_);
        }

        internal string GetLeaf(string name_)
        {
            return childrenLeaf[name_];
        }

        internal Section GetSection(string name_)
        {
            return childrenSections[name_];
        }

        internal Dictionary<string, Section> Sections
        {
            get { return childrenSections; }
        }

        internal Dictionary<string, string> Leafs
        {
            get { return childrenLeaf; }
        }
    }
}
