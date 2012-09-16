using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace GameBox.Graphics
{
	internal class Texture : IDisposable
	{
		private int texture = -1;
		private string idTexture;

		private static List<Texture> textures = null;

		internal static void Init()
		{
			textures  = new List<Texture>();
		}

		internal static void Destroy()
		{
			foreach (Texture t in textures)
			{
				if (t.isValid())
				{
					t.Dispose();
				}
			}

			textures.Clear();
			textures = null;
		}

        internal static void createTexture(string file, string id)
        {
            Texture t = new Texture(file, id);
            textures.Add(t);
        }

		private Texture (string file, string id)
		{
			Bitmap bitmap = new Bitmap(file);
			GL.GenTextures(1, out texture);
			GL.BindTexture(TextureTarget.Texture2D, texture);
			
			BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
				ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
			              OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			
			bitmap.UnlockBits(data);
			
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			idTexture = id;
		}

		internal bool isValid()
		{
			return texture > -1;
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			if (isValid())
			{
				GL.DeleteTextures(1, ref texture);
			}
		}

		#endregion
	}
}

