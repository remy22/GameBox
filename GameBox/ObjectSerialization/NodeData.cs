using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using GameBox.Services;
using GameBox.Graphics.Types.Wrappers;
using GameBox.Graphics.Types;

namespace GameBox.ObjectSerialization
{
    internal class NodeData
    {
        private string name;
        private string textValue;
        private List<NodeData> children = new List<NodeData>();
        private Dictionary<string,string> attributes = new Dictionary<string,string>();
        private string[] childSeparators = { "/" };

        internal NodeData getChildFromAddress(string childName)
        {
            string[] childrenAddress = childName.Split(childSeparators, StringSplitOptions.RemoveEmptyEntries);
            NodeData nd = this;

            foreach (String str in childrenAddress)
            {
                if (nd != null)
                {
                    NodeData down = nd.getChild(str);
                    nd = down;
                }
            }
            return nd;
        }

        internal Dictionary<string, string> Attributes
        {
            get { return attributes; }
        }

        internal IConvertibleFromGBProperties getAsObject(string address,IConvertibleFromGBProperties obj)
        {
            NodeData nd = getChildFromAddress(address);
            GBProperties prop = new GBProperties(nd);
            obj.CreateFromProperties(prop);
            return obj;
        }

        internal GBProperties getAsProperties(string address)
        {
            NodeData nd = getChildFromAddress(address);
            GBProperties prop = new GBProperties(nd);
            return prop;
        }

        internal string getProperty(string propName, string defValue)
        {
            string val;
            bool ok = attributes.TryGetValue(propName, out val);
            if (!ok)
            {
                if (defValue != null)
                {
                    attributes[propName] = defValue;
                    Logger.debug("Setting property " + propName + " to its default value " + defValue);
                    val = defValue;
                }
            }

            return val;
        }

        internal int getPropertyInt(string propName, string defValue)
        {
            return int.Parse(getProperty(propName, defValue));
        }

        internal NodeData getChild(string childName)
        {
            foreach (NodeData nd in children)
            {
                if (nd.name == childName)
                    return nd;
            }

            return null;
        }

        internal void ReadNode(XmlTextReader reader)
        {
            name = reader.Name;
            bool EndReached = false;
            while (!EndReached && reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        NodeData newChild = new NodeData();
                        // check if the element has any attributes
                        if (reader.HasAttributes)
                        {
                            // move to the first attribute
                            reader.MoveToFirstAttribute();
                            for (int i = 0; i < reader.AttributeCount; i++)
                            {
                                // read the current attribute
                                reader.MoveToAttribute(i);
                                newChild.attributes[reader.Name] = reader.Value;
                            }
                        }
                        reader.MoveToElement();
                        newChild.ReadNode(reader);
                        children.Add(newChild);
                        break;
                    case XmlNodeType.Text:
                        textValue = reader.Value;
                        break;
                    case XmlNodeType.XmlDeclaration:
                    case XmlNodeType.ProcessingInstruction:
                        break;
                    case XmlNodeType.Comment:
                        break;
                    case XmlNodeType.EndElement:
                        EndReached = true;
                        break;
                }
            }
        }

        internal static NodeData ReadFile(GBFile file)
        {
            return ReadFile(file.getStream());
        }

        internal static NodeData ReadFile(StreamReader stream)
        {
            try
            {
                NodeData nData = null;
                using (XmlTextReader reader = new XmlTextReader(stream))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                nData = new NodeData();
                                nData.ReadNode(reader);
                                break;
                            case XmlNodeType.EndElement:
                                break;
                        }
                    }
                }

                return nData;
            }
            catch (Exception e)
            {
                Logger.debug(e.Message);
            }

            return null;
        }

        internal static NodeData ReadFile(string fileName)
        {
            return ReadFile(new StreamReader(fileName));
        }

        private static string spaces(int level = 0)
        {
            string result = "";

            for (int i = 0; i < level; i++)
            {
                result += "\t";
            }

            return result;
        }

        private static string lineJump = "\n";
        internal string SerializeToString(int level = 0)
        {
            string result = spaces(level) + "<" + name;
            foreach (KeyValuePair<string,string> attribute in attributes)
            {
                result += " " + attribute.Key + "=\"" + attribute.Value + "\"";
            }

            result += ">" + lineJump;
            foreach (NodeData node in children)
            {
                result += node.SerializeToString(level + 1);
            }

            result += spaces(level) + textValue;
            result += spaces(level) + "</" + name + ">" + lineJump;
            return result;
        }
    }
}
