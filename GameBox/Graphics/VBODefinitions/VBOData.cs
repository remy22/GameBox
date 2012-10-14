using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace GameBox.Graphics.VBODefinitions
{
	[Serializable]
	public class VBOData
	{
		protected float[] VertexData;
		protected uint[]  ColorData;
		protected float[] NormalData;
		protected float[] TextureData;
		protected uint[] IndicesData;
		
		private int VertexBufferID;
		private int ColorBufferID;
		private int NormalBufferID;
		private int IndiciesBufferID;
		private int TextureBufferID;
		private int ElementCount;

		public VBOData() {
			VertexData = null;
			NormalData = null;
			TextureData = null;
			IndicesData = null;
			ColorData = null;
			VertexBufferID = 0;
			ColorBufferID = 0;
			NormalBufferID = 0;
			IndiciesBufferID = 0;
			TextureBufferID = 0;
			ElementCount = 0;

		}

		public void CreateVBO() {
			int bufferSize;
			
			// Normal Array Buffer
			if (NormalData != null) {
				GL.GenBuffers (1, out NormalBufferID);
				GL.BindBuffer (BufferTarget.ArrayBuffer, NormalBufferID);
				GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(NormalData.Length * sizeof(float)), NormalData, BufferUsageHint.StaticDraw);
				GL.GetBufferParameter (BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (NormalData.Length * sizeof(float)!= bufferSize)
					throw new ApplicationException ("Normal array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			}
			
			// TexCoord Array Buffer
			if (TextureData != null)
			{
				// Generate Array Buffer Id
				GL.GenBuffers(1, out TextureBufferID);
				GL.BindBuffer(BufferTarget.ArrayBuffer, TextureBufferID);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(TextureData.Length * sizeof(float)), TextureData, BufferUsageHint.StaticDraw);
				GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (TextureData.Length * sizeof(float) != bufferSize)
					throw new ApplicationException("TexCoord array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			}
			
			// Vertex Array Buffer
			{
				GL.GenBuffers (1, out VertexBufferID);
				GL.BindBuffer (BufferTarget.ArrayBuffer, VertexBufferID);
				GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(VertexData.Length * sizeof(float)), VertexData, 
				               BufferUsageHint.DynamicDraw);
				GL.GetBufferParameter (BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (VertexData.Length * sizeof(float) != bufferSize)
					throw new ApplicationException ("Vertex array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			}

			// Color Array Buffer
			if (ColorData != null)
			{
				GL.GenBuffers (1, out ColorBufferID);
				GL.BindBuffer (BufferTarget.ArrayBuffer, ColorBufferID);
				GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(ColorData.Length * sizeof(uint)), ColorData, BufferUsageHint.DynamicDraw);
				GL.GetBufferParameter (BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (ColorData.Length * sizeof(float) != bufferSize)
					throw new ApplicationException ("Color array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			}

			// Element Array Buffer
			{
				GL.GenBuffers (1, out IndiciesBufferID);
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, IndiciesBufferID);
				GL.BufferData (BufferTarget.ElementArrayBuffer, (IntPtr)(IndicesData.Length * sizeof(int)), IndicesData, BufferUsageHint.StaticDraw);
				GL.GetBufferParameter (BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (IndicesData.Length * sizeof(int) != bufferSize)
					throw new ApplicationException ("Element array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, 0);
			}
			
			ElementCount = IndicesData.Length;
		}

		internal void DrawVBO (int textureID)
		{
			if(VertexBufferID == 0 || IndiciesBufferID == 0)
				return;

            if (textureID > 0 && TextureBufferID > 0)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, textureID);

				GL.BindBuffer(BufferTarget.ArrayBuffer, TextureBufferID);
				GL.TexCoordPointer(2, TexCoordPointerType.Float, sizeof(float)*2, IntPtr.Zero);
				GL.EnableClientState(ArrayCap.TextureCoordArray);
			}

            // Vertex Array Buffer
            {
                GL.EnableClientState(ArrayCap.VertexArray);
                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
                GL.VertexPointer(3, VertexPointerType.Float, sizeof(float) * 3, IntPtr.Zero);
            }

            if (ColorBufferID > 0)
            {
                GL.EnableClientState(ArrayCap.ColorArray);
                GL.BindBuffer(BufferTarget.ArrayBuffer, ColorBufferID);
                GL.ColorPointer(4, ColorPointerType.UnsignedByte, sizeof(uint), IntPtr.Zero);
            }


            // Element Array Buffer
            {
                GL.EnableClientState(ArrayCap.IndexArray);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndiciesBufferID);
                GL.DrawElements(BeginMode.Triangles, ElementCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
            }

            GL.Disable(EnableCap.Texture2D);
        }

        internal virtual int NumFaces
        {
            get { return 0; }
        }

        internal virtual int GetVertexInFace(int nFace)
        {
            return 0;
        }

        internal void setColorToFace(int face, System.Drawing.Color color)
        {
            int index = 0;
            for (int i = 0; i < face; i++)
            {
                index += GetVertexInFace(i);
            }

            for (int i = 0; i < GetVertexInFace(face); i++)
            {
                ColorData[i] = (uint)color.ToArgb();
            }
        }
	}
}
