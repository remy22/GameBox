using System;
using System.IO;

namespace GameBox.Graphics.Types.Wrappers
{
    internal class GBFile
    {
        string fileName;
        FileInfo info = null;

        internal GBFile(string fileName)
        {
            this.fileName = fileName;
            info = new FileInfo(fileName);
        }

        internal StreamReader getStream()
        {
            return new StreamReader(fileName);
        }

        internal bool FileExists()
        {
            return info.Exists;
        }

        internal static GBFile CreateObject(string fileName)
        {
            return new GBFile(fileName);
        }

        internal static bool FileExists(string fileName)
        {
            return new GBFile(fileName).FileExists();
        }
    }
}
