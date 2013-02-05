using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBox.Processes;
using GameBox.XMLSerialization;

namespace GameBox.Resources
{
    public class Resource
    {
        private string name = string.Empty;
        protected string fileName;
        protected bool loaded = false;
        protected GBXMLContainer initData = new GBXMLContainer();
        protected bool disposed = false;

        protected Resource()
        {
        }

		protected Resource (string name_, string fileName_)
		{
            name = name_;
            fileName = fileName_;
		}

        public GBXMLContainer InitData
        {
            internal set { initData = value; }
            get { return initData; }
        }

        public string Name
        {
            get { return name; }
        }

        internal string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public bool Loaded
        {
            get { return loaded; }
        }


        public bool Load(Process loadProcess)
        {
            string completeDir = loadProcess == null ? string.Empty : loadProcess.BaseDir + "/";
            completeDir += fileName;
            if (GBFileSystem.FileExists(completeDir))
            {
                loaded = LoadImplement(completeDir);
                return loaded;
            }
            return false;
        }

        public virtual bool LoadImplement(string fileName)
        {
            return true;
        }

        ~Resource()
        {
            if (loaded && !disposed)
            {
                GBDebug.WriteLine("Resource " + Name + " is leaking memory");
            }
        }
    }
}
