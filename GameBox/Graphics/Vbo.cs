using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace GameBox.Graphics
{
	internal struct Vbo
	{
		internal int VboID, EboID, TboID, NumElements;
		private int strideVertices;
		private int verticesLen;

		internal void LoadVBO<TVertex>(TVertex[] vertices, TVertex[] textures, short[] elements) where TVertex : struct
		{
			int size;
			strideVertices = BlittableValueType.StrideOf(vertices);
			verticesLen = vertices.Length;
			NumElements = elements.Length;

			GL.GenBuffers(1, out VboID);
			GL.BindBuffer(BufferTarget.ArrayBuffer, VboID);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verticesLen * strideVertices), 
			              vertices, BufferUsageHint.StaticDraw);
			GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
			if (verticesLen * strideVertices != size)
				throw new ApplicationException("Vertex data not uploaded correctly");
			
			GL.GenBuffers(1, out EboID);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, EboID);
			GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(NumElements * sizeof(short)), 
                elements, BufferUsageHint.StaticDraw);
			GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
			if (elements.Length * sizeof(short) != size)
				throw new ApplicationException("Element data not uploaded correctly");

			GL.GenBuffers(1, out TboID);
			GL.BindBuffer(BufferTarget.TextureBuffer, TboID);
			GL.BufferData(BufferTarget.TextureBuffer, (IntPtr)(verticesLen * strideVertices), 
			              textures, BufferUsageHint.StaticDraw);
			GL.GetBufferParameter(BufferTarget.TextureBuffer, BufferParameterName.BufferSize, out size);
			if (verticesLen * strideVertices != size)
			    throw new ApplicationException("Vertex texture data not uploaded correctly");
		}

		internal void Draw()
		{
			GL.Enable(EnableCap.Texture2D);
            GL.BindBuffer(BufferTarget.TextureBuffer, 1);
			GL.EnableClientState(ArrayCap.ColorArray);
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.EnableClientState(ArrayCap.TextureCoordArray);

			GL.BindBuffer(BufferTarget.ArrayBuffer, VboID);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, EboID);
			GL.BindBuffer(BufferTarget.TextureBuffer, TboID);

			GL.VertexPointer(3, VertexPointerType.Float, strideVertices, new IntPtr(0));
			GL.ColorPointer(4, ColorPointerType.UnsignedByte, strideVertices, new IntPtr(12));
            GL.TexCoordPointer(3, TexCoordPointerType.Float, strideVertices, new IntPtr(16));
			
			GL.DrawElements(BeginMode.Triangles, NumElements, DrawElementsType.UnsignedShort, IntPtr.Zero);
		}


	}
}

