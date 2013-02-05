using System;
using System.Collections.Generic;
using GameBox.Processes;
using System.Drawing;

namespace GameBox.Resources
{
    public class ResourceManager
    {
        private List<Texture> createdTextures = new List<Texture>();
        private List<GBFont> createdFonts = new List<GBFont>();

        public void DeleteAllResources()
        {
            foreach (Texture texture in createdTextures)
            {
                texture.Dispose();
            }

            createdTextures.Clear();

            foreach (GBFont font in createdFonts)
            {
                font.Dispose();
            }

            createdFonts.Clear();
        }

        public Texture GetTexture(string Name2Search, string fileName = "")
        {
            foreach (Texture texture in createdTextures)
            {
                if (texture.Name.Equals(Name2Search))
                {
                    if (texture.FileName.Equals(string.Empty) && !fileName.Equals(string.Empty))
                    {
                        texture.FileName = fileName;
                    }
                    return texture;
                }
            }

            return null;

        }

        public GBFont GetFont(string Name2Search, string fileName = "")
        {
            foreach (GBFont font in createdFonts)
            {
                if (font.Name.Equals(Name2Search))
                {
                    if (font.FileName.Equals(string.Empty) && !fileName.Equals(string.Empty))
                    {
                        font.FileName = fileName;
                    }
                    return font;
                }
            }

            return null;
        }

        public Texture GetOrCreateTexture(string Name2Search, string fileName = "")
        {
            Texture text2Return = GetTexture(Name2Search, fileName);
            if (text2Return == null)
            {
                text2Return = new Texture(Name2Search, fileName);
                createdTextures.Add(text2Return);
            }
            return text2Return;
        }

        public Texture GetOrCreateTexture(string Name2Search, GBFont font, string text, SizeF size, bool autoText)
        {
            Texture text2Return = GetTexture(Name2Search, "");
            if (text2Return == null)
            {
                text2Return = new Texture(Name2Search, font, text, size, autoText);
                createdTextures.Add(text2Return);
            }
            return text2Return;
        }

        public GBFont GetOrCreateFont(string Name2Search, string fileName = "")
        {

            GBFont font2Return = GetFont(Name2Search, fileName);
            if (font2Return == null)
            {
                font2Return = new GBFont(Name2Search, fileName);
                createdFonts.Add(font2Return);
            }
            return font2Return;
        }

        public Resource GetResource(string Name2Search, string fileName = "")
        {
            Resource r = GetFont(Name2Search, fileName);
            if (r != null)
                return r;
            return GetTexture(Name2Search, fileName);
        }

        public Resource GetOrCreateResource(Type type, string Name2Search, string fileName = "")
        {
            if (type.Name.EndsWith("GBFont"))
            {
                return GetOrCreateFont(Name2Search, fileName);
            }
            return GetOrCreateTexture(Name2Search, fileName);
        }
    }
}
