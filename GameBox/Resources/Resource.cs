using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBox.Processes;

namespace GameBox.Resources
{
    public class Resource
    {
        private string name = string.Empty;
        protected string fileName;
        protected bool loaded = false;

        protected Resource()
        {
        }

		protected Resource (string name_,string fileName_)
		{
            name = name_;
            fileName = fileName_;
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

        private static List<Texture> createdTextures = new List<Texture>();
        private static List<GBFont> createdFonts = new List<GBFont>();

        public static Texture GetTexture(string Name2Search, string fileName = "")
        {
            foreach (Texture texture in createdTextures)
            {
                if (texture.Name.Equals(Name2Search))
                {
                    if (texture.fileName.Equals(string.Empty) && !fileName.Equals(string.Empty))
                    {
                        texture.FileName = fileName;
                    }
                    return texture;
                }
            }

            return null;

        }

        public static GBFont GetFont(string Name2Search, string fileName = "")
        {
            foreach (GBFont font in createdFonts)
            {
                if (font.Name.Equals(Name2Search))
                {
                    if (font.fileName.Equals(string.Empty) && !fileName.Equals(string.Empty))
                    {
                        font.FileName = fileName;
                    }
                    return font;
                }
            }

            return null;
        }

        public static Texture GetOrCreateTexture(string Name2Search, string fileName = "")
        {
            Texture text2Return = GetTexture(Name2Search, fileName);
            if (text2Return == null)
            {
                text2Return = new Texture(Name2Search, fileName);
                createdTextures.Add(text2Return);
            }
            return text2Return;
        }

        public static GBFont GetOrCreateFont(string Name2Search, string fileName = "")
        {

            GBFont font2Return = GetFont(Name2Search, fileName);
            if (font2Return == null)
            {
                font2Return = new GBFont(Name2Search, fileName);
                createdFonts.Add(font2Return);
            }
            return font2Return;
        }

        public static Resource GetResource(string Name2Search, string fileName = "")
        {
            Resource r = GetFont(Name2Search, fileName);
            if (r != null)
                return r;
            return GetTexture(Name2Search, fileName);
        }

        public static Resource GetOrCreateResource(Type type, string Name2Search, string fileName = "")
        {
            if (type.Name.EndsWith("GBFont"))
            {
                return GetOrCreateFont(Name2Search, fileName);
            }
            return GetOrCreateTexture(Name2Search, fileName);
        }

        public virtual bool LoadImplement(string fileName)
        {
            return true;
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

    }
}
