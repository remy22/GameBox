using System;
using System.Drawing;
using System.Drawing.Text;

namespace GameBox.Resources
{
    public class GBFont : Resource, IDisposable
    {
        private Font fontData = null;

   		public GBFont (string name_,string fileName_):base(name_,fileName_)
		{

		}

        public Font FontData
        {
            get { return fontData; }
        }

        private FontFamily LoadFontFamily(string fileName, out PrivateFontCollection _myFonts)
        {
            _myFonts = new PrivateFontCollection();//here is where we assing memory space to myFonts
            _myFonts.AddFontFile(fileName);//we add the full path of the ttf file
            return _myFonts.Families[0];//returns the family object as usual.
        }

        public override bool LoadImplement(string fileName)
        {
            PrivateFontCollection myFonts;

            FontFamily family = LoadFontFamily(fileName, out myFonts);
            fontData = new Font(family, NumberConverter.ParseFloat(initData["Size", "20.0"].Text));
            return true;
        }

        public void Dispose()
        {
            GBDebug.WriteLine("Disposing font: " + Name);
            if (!disposed && loaded)
            {
                disposed = true;
                GC.SuppressFinalize(this);

                if (fontData != null)
                {
                    fontData.Dispose();
                    fontData = null;
                }
            }
        }
    }
}
