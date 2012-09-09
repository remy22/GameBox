using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GameBox.Graphics
{
    internal class VBODefinitions
    {
        internal enum VBOs
        {
            Cube
        }

        internal static Vbo GetNew(VBOs vboType)
        {
            Vbo result = new Vbo();
            VertexPositionColor[] Vertices;
            short[] Elements;

            switch (vboType)
            {
                case VBOs.Cube:
                    Vertices = new VertexPositionColor[]
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

                    Elements = new short[]
                    {
                        0, 1, 2, 2, 3, 0, // front face
                        3, 2, 6, 6, 7, 3, // top face
                        7, 6, 5, 5, 4, 7, // back face
                        4, 0, 3, 3, 7, 4, // left face
                        0, 1, 5, 5, 4, 0, // bottom face
                        1, 5, 6, 6, 2, 1, // right face
                    };

                    result.LoadVBO(Vertices, Elements);

                    break;
            }

            return result;
        }
    }
}
