using System.IO;
using System.Collections.Generic;

namespace GameBox
{
	public static class GBFileSystem
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

        internal static List<string> FilesInDirectory(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            return (List<string>)dirInfo.EnumerateFiles(dir);
        }

        private static FileInfo[] FileInfosInDirectory(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            return dirInfo.GetFiles();

        }

        internal static string CompleteFileNameForFile(string dir, string file)
        {
            FileInfo[] fInfos = FileInfosInDirectory(dir);
            foreach (FileInfo fInfo in fInfos)
            {
                if (fInfo.Name.Equals(file))
                    return fInfo.FullName;
            }

            return string.Empty;
        }
	}
}

