using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System;

namespace GameBox
{
	public class Texture : Resource
	{
		private Bitmap bmp;
		private int texture = -1;

		public Texture ()
		{
		}

		#region implemented abstract members of Resource

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

		#endregion
	}
}

