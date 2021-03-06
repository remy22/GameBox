using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using System.Drawing;

namespace GameBox.XMLSerialization
{
	public class GBXMLContainer : ICloneable
	{
		private Dictionary<string,string> attributes = new Dictionary<string,string>();
		private List<GBXMLContainer> children = new List<GBXMLContainer>();
		private string textValue;
		private string name = "";
		public static bool AttributesToChildren = true;

		public static GBXMLContainer LoadOrNull(string file)
		{
			if (GBFileSystem.FileExists(file))
			{
				return new GBXMLContainer(file);
			}
			return null;
		}

        public static GBXMLContainer LoadFromString(string data)
        {
            return new GBXMLContainer(new MemoryStream(new System.Text.UTF8Encoding().GetBytes(data)));
        }

        public GBXMLContainer getPropertiesStartingWith(string strStart)
        {
            GBXMLContainer cp = new GBXMLContainer();
            cp.name = name;
            foreach (GBXMLContainer ch in children)
            {
                if (ch.name.StartsWith(strStart))
                {
                    cp.AddChild((GBXMLContainer)ch.Clone());
                }
            }
            return cp;
        }

        public static GBXMLContainer LoadFromProperties(string[] data)
        {
            GBXMLContainer temp = new GBXMLContainer();
            if (data.Length > 0)
            {
                temp.name = data[0];
                int index = 1;
                while (data.Length > index)
                {
                    GBXMLContainer internalTemp = new GBXMLContainer();
                    internalTemp.name = data[index];
                    index++;
                    internalTemp.textValue = (data.Length > index) ? data[index] : "";
                    temp.children.Add(internalTemp);
                    index++;
                }
            }
            return temp;
        }

		public GBXMLContainer()
		{

		}

		public GBXMLContainer (GBXMLContainer other)
		{
			this.name = other.name;
			foreach (KeyValuePair<string, string> keypair in attributes)
			{
				this.attributes[keypair.Key] = keypair.Value;
			}

			textValue = other.textValue;

			foreach (GBXMLContainer ch in other.children)
			{
				children.Add(new GBXMLContainer(ch));
			}
		}

        public GBXMLContainer(MemoryStream data)
            : this(new StreamReader(data))
        {
        }

		public GBXMLContainer (string file):this(GBFileSystem.GetXMLTextReader(file))
		{
		}

		public GBXMLContainer (StreamReader stream):this(GBFileSystem.GetXMLTextReader(stream))
		{
		}

		public GBXMLContainer (XmlTextReader reader)
		{
			while (reader.NodeType != XmlNodeType.Element)
			{
				reader.Read();
			}
			name = reader.Name;
			// check if the element has any attributes
			if (reader.HasAttributes) {
				// move to the first attribute
				reader.MoveToFirstAttribute ();
				for (int i = 0; i < reader.AttributeCount; i++) {
					// read the current attribute
					reader.MoveToAttribute (i);
					if (AttributesToChildren)
					{
						GBXMLContainer newChild = new GBXMLContainer();
						newChild.name = reader.Name;
						newChild.textValue = reader.Value;
						children.Add(newChild);
					}
					else
					{
						attributes [reader.Name] = reader.Value;
					}
				}
			}
			reader.MoveToContent ();
			bool EndReached = reader.IsEmptyElement;
			while (!EndReached && reader.Read()) {
				switch (reader.NodeType) {
				case XmlNodeType.Element:
					GBXMLContainer newChild = new GBXMLContainer (reader);
					children.Add (newChild);
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

        public void Overwrite(GBXMLContainer data)
        {
            textValue = data.textValue;
            foreach (GBXMLContainer container in data.children)
            {
                if (Exists(container.name))
                {
                    this[container.name].Overwrite(data[container.name]);
                }
                else
                {
                    AddChild(new GBXMLContainer(data[container.name]));
                }
            }
        }

        public void AddChild(GBXMLContainer container)
        {
            children.Add(container);
        }

		public string Text
		{
			get { return textValue; }
		}

        public string Name
        {
            get { return name;  }
            set { name = value; }
        }

		public override string ToString ()
		{
			return GetAsString(0);
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

		public string GetAsString(int level)
		{
			string result = spaces(level) + "<" + name;
			foreach (KeyValuePair<string,string> attribute in attributes)
			{
				result += " " + attribute.Key + "=\"" + attribute.Value + "\"";
			}
			
			result += ">" + lineJump;
			foreach (GBXMLContainer node in children)
			{
				result += node.GetAsString(level + 1);
			}
			
			if (textValue != string.Empty)
				result += spaces(level) + textValue + lineJump;
			result += spaces(level) + "</" + name + ">" + lineJump;
			return result;
		}

        public IEnumerable<GBXMLContainer> Children
        {
            get { return children; }
        }

		#region ICloneable implementation

		public object Clone ()
		{
			GBXMLContainer cloned = new GBXMLContainer(this);
			return cloned;
		}

		#endregion

		public GBXMLContainer this[string field, string notFound = ""]
		{
			get {
				foreach (GBXMLContainer cont in children)
				{
					if (cont.name == field)
						return cont;
				}

				// Do not return null objects to avoid crashes.
//				GBDebug.Warning("Warning. Returning notFound (" + notFound + ") field for field: "+field);
				GBXMLContainer def = new GBXMLContainer();
				def.name = field;
				def.textValue = notFound;
				return def;
			}
		}

        public bool Exists(string field)
        {
            foreach (GBXMLContainer cont in children)
            {
                if (cont.name == field)
                    return true;
            }

            return false;
        }

        public static RectangleF ReadRectangleF(GBXMLContainer stream)
        {
            return new RectangleF(
                ReadPointF(stream),
                ReadSizeF(stream)
            );
        }

        public static PointF ReadPointF(GBXMLContainer stream)
        {
            return ReadPointFFrom(stream, "Position");
        }

        public static SizeF ReadSizeF(GBXMLContainer stream)
        {
            return new SizeF(
                NumberConverter.ParseFloat(stream["Length"]["Width", "-1.0"].Text),
                NumberConverter.ParseFloat(stream["Length"]["Height", "-1.0"].Text)
            );
        }

        public static PointF ReadPointFFrom(GBXMLContainer stream, string field)
        {
            return new PointF(
                NumberConverter.ParseFloat(stream[field]["X", "0.0"].Text),
                NumberConverter.ParseFloat(stream[field]["Y", "0.0"].Text)
            );
        }

        public static bool FieldExists(GBXMLContainer stream, string field)
        {
            return stream.Exists(field);
        }
    }
}

