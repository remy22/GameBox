using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBox.Model;

namespace GameBox.InternalServices
{
    internal static class SystemData
    {
        static TreeNode rootNode = new TreeNode { Name = "SystemRootData" };

        internal static TreeNode RootData
        {
            get { return rootNode; }
        }
    }
}
