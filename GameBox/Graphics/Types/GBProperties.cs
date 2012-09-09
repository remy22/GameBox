using System;
using System.Collections.Generic;
using GameBox.ObjectSerialization;

namespace GameBox.Graphics.Types
{
    public class GBProperties
    {
        private Dictionary<string, string> properties = new Dictionary<string, string>();

        internal GBProperties(Dictionary<string, string> prop)
        {
            foreach (KeyValuePair<string, string> p in prop)
            {
                properties[p.Key] = p.Value;
            }
        }

        internal GBProperties(NodeData node)
        {
            foreach (KeyValuePair<string, string> p in node.Attributes)
            {
                properties[p.Key] = p.Value;
            }
        }
    }
}
