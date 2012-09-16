using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameBox.Graphics
{
	internal static class VBOCreator
	{
		private static VertexPositionColor[] CubeVertices = new VertexPositionColor[]
		{
			new VertexPositionColor(-1.0f, -1.0f,  1.0f, Color.DarkRed),
			new VertexPositionColor( 1.0f, -1.0f,  1.0f, Color.DarkRed),
			new VertexPositionColor( 1.0f,  1.0f,  1.0f, Color.Gold),
			new VertexPositionColor(-1.0f,  1.0f,  1.0f, Color.Gold),
			new VertexPositionColor(-1.0f, -1.0f, -1.0f, Color.DarkRed),
			new VertexPositionColor( 1.0f, -1.0f, -1.0f, Color.DarkRed), 
			new VertexPositionColor( 1.0f,  1.0f, -1.0f, Color.Gold),
			new VertexPositionColor(-1.0f,  1.0f, -1.0f, Color.Gold) 
		};
		
		private static readonly short[] CubeElements = new short[]
		{
			0, 1, 2, 2, 3, 0, // front face
			3, 2, 6, 6, 7, 3, // top face
			7, 6, 5, 5, 4, 7, // back face
			4, 0, 3, 3, 7, 4, // left face
			0, 1, 5, 5, 4, 0, // bottom face
			1, 5, 6, 6, 2, 1, // right face
		};

		internal enum VBOType
		{
			Cube = 0
		}

		private const int numTypes = 1;
		private static Vbo[] vbos = null;

		internal static void Init()
		{
			vbos = new Vbo[numTypes];

			vbos[0].LoadVBO(CubeVertices, CubeVertices, CubeElements);
		}

		internal static void Destroy()
		{
			foreach (Vbo vbo in vbos)
			{
				// Destroy?
			}
		}

		internal static Vbo getVbo(VBOType type)
		{
			return vbos[(int)type];
		}
	}
}

