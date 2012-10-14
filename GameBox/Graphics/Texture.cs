using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.Collections.Generic;
using OpenTK.Graphics;

namespace GameBox.Graphics
{
	internal class Texture : IDisposable
	{
		protected int texture = -1;
        protected string idTexture;
        protected bool disposed = false;

		private static List<Texture> textures = null;

		internal static void Init()
		{
			textures  = new List<Texture>();
		}

		internal static void Destroy()
		{
			foreach (Texture t in textures)
			{
				t.Dispose(true);
			}

			textures.Clear();
			textures = null;
		}

        internal int TextureID
        {
            get { return texture; }
        }

        internal static void createTexture(string file, string id)
        {
            Texture t = new Texture(file, id);
            textures.Add(t);
        }

        internal Texture()
        {
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

        void Dispose(bool manual)
        {
            if (!disposed)
            {
                if (manual)
                {
                    textures.Remove(this);
                    if (GraphicsContext.CurrentContext != null)
                    {
               			if (isValid())
			            {
				            GL.DeleteTexture(texture);
			            }
                    }
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~Texture()
        {
            Console.WriteLine("[Warning] Resource leaked: {0}.", this);
        }
	}
}

