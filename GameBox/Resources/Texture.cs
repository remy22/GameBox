using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System;
using System.Collections.Generic;
using GameBox.Processes;

namespace GameBox.Resources
{
	public class Texture : Resource
	{
		private Bitmap bmp;
		private int texture = -1;
        private System.Drawing.Graphics gfx;

		public Texture (string name_,string fileName_):base(name_,fileName_)
		{
		}

		public override bool LoadImplement (string fileName)
		{
			try
			{
				bmp = new Bitmap(fileName);
				GL.GenTextures(1, out texture);
				GL.BindTexture(TextureTarget.Texture2D, texture);
				
				BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                                               ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
				              OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
				
				bmp.UnlockBits(data);
				
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
				return true;
			} catch (Exception e)
			{
				throw new GBException(e);
			}
		}

        internal Texture(GBFont font, string text, RectangleF boundingBox)
        {
            GBDebug.Assert(boundingBox.Width > 0, "Trying to create texture with width 0");
            GBDebug.Assert(boundingBox.Height > 0, "Trying to create texture with height 0");

            bmp = new Bitmap((int)boundingBox.Width, (int)boundingBox.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            gfx = System.Drawing.Graphics.FromImage(bmp);
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)boundingBox.Width, (int)boundingBox.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

            DrawString(text, font);
        }

        internal void DrawString(string text, GBFont font, PointF point = new PointF(), Brush brush = null)
        {
            if (brush == null)
                brush = Brushes.White;
            Color tr = Color.Transparent;
            tr = Color.FromArgb(128, 0, 0, 0);
            gfx.Clear(tr);
            gfx.DrawString(text, font.FontData, brush, point);

            SizeF size = gfx.MeasureString(text, font.FontData);

            Rectangle dirty_region = new Rectangle(new Point(), bmp.Size);
            System.Drawing.Imaging.BitmapData data = bmp.LockBits(dirty_region,
            System.Drawing.Imaging.ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0,
                dirty_region.X, dirty_region.Y, dirty_region.Width, dirty_region.Height,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

        }

        public int Width
        {
            get { return bmp != null ?bmp.Width : 0; }
        }

        public int Height
        {
            get { return bmp != null ? bmp.Height : 0; }
        }

        internal void BindTexture()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture);
        }

        internal void UnbindTexture()
        {
            GL.Disable(EnableCap.Texture2D);
        }
	}
}

