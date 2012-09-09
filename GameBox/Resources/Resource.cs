using System;
using System.Collections.Generic;
using System.IO;
using GameBox.Services;

namespace GameBox.Resources
{
    internal class Resource
    {
        private string originFile;
        private string name;
        private bool processed;
        private byte[] data;
        private FileInfo fInfo;

        internal Resource(string name_, string originFile_)
        {
            name = name_;
            originFile = originFile_;
            fInfo = new FileInfo(originFile);
            processed = false;
        }

        internal Resource(string name_, string originFile_,byte[] data_) : this(name_,originFile_)
        {
            data = data_;
        }

        private static readonly int readBlockSize = 4096;
        private long counterFile;

        internal void ReadResource()
        {
            if (fInfo.Exists)
            {
                Logger.debug("Loading file " + fInfo.FullName);
                FileStream f = fInfo.OpenRead();
                data = new byte[f.Length];
                counterFile = 0;
                do
                {
                    int result = f.Read(data, (int)counterFile, readBlockSize);
                    counterFile += readBlockSize;
                    if (result != readBlockSize)
                        counterFile = f.Length;
                } while (counterFile < f.Length);
                // Close the file ASAP.
                f.Close();
                counterFile = f.Length;
            }
        }

        internal long BytesRead
        {
            get { return counterFile; }
        }

        internal string Name
        {
            get { return name; }
        }

        internal string OriginFile
        {
            get { return originFile; }
        }

        internal FileInfo Info
        {
            get { return fInfo; }
        }

        internal byte[] Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
