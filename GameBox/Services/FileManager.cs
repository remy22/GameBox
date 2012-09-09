using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GameBox.Services
{
    internal class FileManager
    {
        internal static FileStream get(string filename)
        {
            return File.OpenRead(filename);
        }
    }
}
