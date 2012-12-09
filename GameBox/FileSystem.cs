using System.IO;

namespace GameBox
{
	public static class FileSystem
	{
		internal static bool FileExists(string fileName)
		{
			return File.Exists(fileName);
		}

		internal static StreamReader GetReader(string fileName)
		{
			return new StreamReader(fileName);
		}

		internal static System.Xml.XmlTextReader GetXMLTextReader(string fileName)
		{
			return new System.Xml.XmlTextReader(GetReader(fileName));
		}

		internal static System.Xml.XmlTextReader GetXMLTextReader(StreamReader stream)
		{
			return new System.Xml.XmlTextReader(stream);
		}


	}
}

