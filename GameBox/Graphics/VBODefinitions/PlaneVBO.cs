using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBox.Graphics.VBODefinitions
{
    internal class PlaneVBO : VBOData
    {
        internal PlaneVBO()
            : base()
		{
			// Vertex Data
			VertexData = new float[] {
				// Front face
				-1.0f, -1.0f, 1.0f, 
				1.0f, -1.0f, 1.0f, 
				1.0f, 1.0f, 1.0f, 
				-1.0f, 1.0f, 1.0f
			};
			
			// Normal Data for the Cube Verticies
			NormalData = new float[] {
				// Front face
				0f, 0f, 1f, 
				0f, 0f, 1f,
				0f, 0f, 1f,
				0f, 0f, 1f
			};
			
			// Texture Data for the Cube Verticies 
			TextureData = new float[] {
				// Font Face
				0, 1,
				1, 1,
				1, 0,
				0, 0
			};
			
			// Element Indices for the Cube
			IndicesData = new uint[] { 
				// Font face
				0, 1, 2, 2, 3, 0
			};

			// Element Indices for the Cube
			ColorData = new uint[] { 
				// Font face
				0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff
			};
		}

        internal override int NumFaces
        {
            get { return 1; }
        }

        internal override int GetVertexInFace(int nFace)
        {
            return 4;
        }

    }
}
