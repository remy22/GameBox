using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace GameBox.Graphics.VBODefinitions
{
	[Serializable]
	public class VBOData
	{
		public float[] VertexData;
		public float[] NormalData;
		public float[] TextureData;
		public uint[] IndicesData;
		
		public VBOData() {
			VertexData = null;
			NormalData = null;
			TextureData = null;
			IndicesData = null;
		}

		public static void CreateVBO(ref VBOData vboData, ref VBO vbo) {
			int bufferSize;
			
			// Normal Array Buffer
			if (vboData.NormalData != null) {
				// Generate Array Buffer Id
				GL.GenBuffers (1, out vbo.normalBufferID);
				
				// Bind current context to Array Buffer ID
				GL.BindBuffer (BufferTarget.ArrayBuffer, vbo.normalBufferID);
				
				// Send data to buffer
				GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(vboData.NormalData.Length * sizeof(float)), vboData.NormalData, BufferUsageHint.StaticDraw);
				
				// Validate that the buffer is the correct size
				GL.GetBufferParameter (BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (vboData.NormalData.Length * sizeof(float)!= bufferSize)
					throw new ApplicationException ("Normal array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			}
			
			// TexCoord Array Buffer
			if (vboData.TextureData != null)
			{
				// Generate Array Buffer Id
				GL.GenBuffers(1, out vbo.textureBufferID);
				
				// Bind current context to Array Buffer ID
				GL.BindBuffer(BufferTarget.ArrayBuffer, vbo.textureBufferID);
				
				// Send data to buffer
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vboData.TextureData.Length * sizeof(float)), vboData.TextureData, BufferUsageHint.StaticDraw);
				
				// Validate that the buffer is the correct size
				GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (vboData.TextureData.Length * sizeof(float) != bufferSize)
					throw new ApplicationException("TexCoord array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			}
			
			// Vertex Array Buffer
			{
				// Generate Array Buffer Id
				GL.GenBuffers (1, out vbo.vertexBufferID);
				
				// Bind current context to Array Buffer ID
				GL.BindBuffer (BufferTarget.ArrayBuffer, vbo.vertexBufferID);
				
				// Send data to buffer
				GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(vboData.VertexData.Length * sizeof(float)), vboData.VertexData, BufferUsageHint.DynamicDraw);
				
				// Validate that the buffer is the correct size
				GL.GetBufferParameter (BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (vboData.VertexData.Length * sizeof(float) != bufferSize)
					throw new ApplicationException ("Vertex array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			}
			
			// Element Array Buffer
			{
				// Generate Array Buffer Id
				GL.GenBuffers (1, out vbo.indiciesBufferID);
				
				// Bind current context to Array Buffer ID
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, vbo.indiciesBufferID);
				
				// Send data to buffer
				GL.BufferData (BufferTarget.ElementArrayBuffer, (IntPtr)(vboData.IndicesData.Length * sizeof(int)), vboData.IndicesData, BufferUsageHint.StaticDraw);
				
				// Validate that the buffer is the correct size
				GL.GetBufferParameter (BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (vboData.IndicesData.Length * sizeof(int) != bufferSize)
					throw new ApplicationException ("Element array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, 0);
			}
			
			vbo.elementCount = vboData.IndicesData.Length;
		}

		public void DrawVBO (ref VBO vbo, int textureID) {
			if(vbo.vertexBufferID == 0)
				return;
			if(vbo.indiciesBufferID == 0)
				return;
			
			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, textureID);
			
			// Texture Data Buffer Binding
			if(vbo.textureBufferID != 0)
			{
				// Bind to the Array Buffer ID
				GL.BindBuffer(BufferTarget.ArrayBuffer, vbo.textureBufferID);
				
				// Set the Pointer to the current bound array describing how the data ia stored
				GL.TexCoordPointer(2, TexCoordPointerType.Float, sizeof(float)*2, IntPtr.Zero);
				
				// Enable the client state so it will use this array buffer pointer
				GL.EnableClientState(ArrayCap.TextureCoordArray);
			}
			
			// Vertex Array Buffer
			{
				// Bind to the Array Buffer ID
				GL.BindBuffer (BufferTarget.ArrayBuffer, vbo.vertexBufferID);
				
				// Set the Pointer to the current bound array describing how the data ia stored
				GL.VertexPointer (3, VertexPointerType.Float, sizeof(float)*3, IntPtr.Zero);
				
				// Enable the client state so it will use this array buffer pointer
				GL.EnableClientState (ArrayCap.VertexArray);
			}
			
			// Element Array Buffer
			{
				// Bind to the Array Buffer ID
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, vbo.indiciesBufferID);
				
				// Draw the elements in the element array buffer
				// Draws up items in the Color, Vertex, TexCoordinate, and Normal Buffers using indices in the ElementArrayBuffer
				GL.DrawElements (BeginMode.Triangles, vbo.elementCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
			}
			GL.Disable(EnableCap.Texture2D);
		}
	}
}

