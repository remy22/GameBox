using System;
using OpenTK;

namespace GameBox.Graphics
{
    class Mesh
    {
        private VertexData[] vertexData = null;

        private Mesh()
        {
        }

        private Mesh(uint numVertex)
        {
            vertexData = new VertexData[numVertex];
        }

        private Mesh(uint numVertex, VertexData[] vData)
        {
            vertexData = new VertexData[numVertex];

            for (int i = 0; i < vData.Length; ++i)
            {
                vertexData[i] = vData[i];
            }
        }

        private Mesh(VertexData[] vData)
        {
            vertexData = new VertexData[vData.Length];

            for (int i = 0; i < vData.Length; ++i)
            {
                vertexData[i] = vData[i];
            }
        }

        #region Static constructors

        internal static Mesh CreateTriangle(VertexData[] vData)
        {
            Mesh mesh = new Mesh(3, vData);
            return mesh;
        }
        #endregion
    }
}
