using System;

namespace GameBox.Graphics.VBODefinitions
{
	internal class CubeVBO : VBOData
	{
		internal CubeVBO () : base()
		{
			// Vertex Data
			VertexData = new float[] {
				// Front face
				-1.0f, -1.0f, 1.0f, 
				1.0f, -1.0f, 1.0f, 
				1.0f, 1.0f, 1.0f, 
				-1.0f, 1.0f, 1.0f,
				// Right face
				1.0f, -1.0f, 1.0f, 
				1.0f, -1.0f, -1.0f, 
				1.0f, 1.0f, -1.0f, 
				1.0f, 1.0f, 1.0f,
				// Back face
				1.0f, -1.0f, -1.0f, 
				-1.0f, -1.0f, -1.0f, 
				-1.0f, 1.0f, -1.0f, 
				1.0f, 1.0f, -1.0f,
				// Left face
				-1.0f, -1.0f, -1.0f, 
				-1.0f, -1.0f, 1.0f, 
				-1.0f, 1.0f, 1.0f, 
				-1.0f, 1.0f, -1.0f,
				// Top Face	
				-1.0f, 1.0f, 1.0f, 
				1.0f, 1.0f, 1.0f,
				1.0f, 1.0f, -1.0f, 
				-1.0f, 1.0f, -1.0f,
				// Bottom Face
				1.0f, -1.0f, 1.0f, 
				-1.0f, -1.0f, 1.0f,
				-1.0f, -1.0f, -1.0f, 
				1.0f, -1.0f, -1.0f
			};
			
			// Normal Data for the Cube Verticies
			NormalData = new float[] {
				// Front face
				0f, 0f, 1f, 
				0f, 0f, 1f,
				0f, 0f, 1f,
				0f, 0f, 1f, 
				// Right face
				1f, 0f, 0f, 
				1f, 0f, 0f, 
				1f, 0f, 0f, 
				1f, 0f, 0f,
				// Back face
				0f, 0f, -1f, 
				0f, 0f, -1f, 
				0f, 0f, -1f,  
				0f, 0f, -1f, 
				// Left face
				-1f, 0f, 0f,  
				-1f, 0f, 0f, 
				-1f, 0f, 0f,  
				-1f, 0f, 0f,
				// Top Face	
				0f, 1f, 0f,  
				0f, 1f, 0f, 
				0f, 1f, 0f,  
				0f, 1f, 0f,
				// Bottom Face
				0f, -1f, 0f,  
				0f, -1f, 0f, 
				0f, -1f, 0f,  
				0f, -1f, 0f
			};
			
			// Texture Data for the Cube Verticies 
			TextureData = new float[] {
				// Font Face
				0, 1,
				1, 1,
				1, 0,
				0, 0,
				// Right Face
				0, 1,
				1, 1,
				1, 0,
				0, 0,
				// Back Face
				0, 1,
				1, 1,
				1, 0,
				0, 0,
				// Left Face
				0, 1,
				1, 1,
				1, 0,
				0, 0,
				// Top Face	
				0, 1,
				1, 1,
				1, 0,
				0, 0,
				// Bottom Face
				0, 1,
				1, 1,
				1, 0,
				0, 0
			};
			
			// Element Indices for the Cube
			IndicesData = new uint[] { 
				// Font face
				0, 1, 2, 2, 3, 0, 
				// Right face
				7, 6, 5, 5, 4, 7, 
				// Back face
				11, 10, 9, 9, 8, 11,
				// Left face
				15, 14, 13, 13, 12, 15, 
				// Top Face	
				19, 18, 17, 17, 16, 19,
				// Bottom Face
				23, 22, 21, 21, 20, 23,
			};

			// Element Indices for the Cube
			ColorData = new uint[] { 
				// Font face
				0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 
				// Right face
				0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 
				// Back face
				0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 
				// Left face
				0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 
				// Top Face	
				0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 
				// Bottom Face
				0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 
			};
		}
	}
}

