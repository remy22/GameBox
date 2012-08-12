using System;
using System.Collections.Generic;

namespace GameBox.Graphics
{
    internal class Node : IDisposable
    {
        internal enum NodeType
        {
            Container,
            Point
        }

        private VertexData[] vertexData = null;
        private int indexVBO = -1;
        private bool MustUpdate = true;
        private Node parent = null;
        private List<Node> children = new List<Node>();

        internal VertexData this[int index]
        {
            get { return vertexData[index]; }
            set { vertexData[index] = value; }
        }

        internal Node Child(int index)
        {
            return children[index];
        }

        internal int NumChildren
        {
            get { return children.Count; }
        }

        public Node CreateChild(NodeType type)
        {
            Node newNode = new Node(type);
            newNode.parent = this;
            children.Add(newNode);
            return newNode;
        }

        internal Node(NodeType type)
        {
            switch (type)
            {
                case NodeType.Container:
                    break;
                case NodeType.Point:
                    CreateParticle(VertexData.Reset);
                    break;
                default:
                    break;
            }
        }

        #region Static constructors

        internal void CreateParticle(VertexData vData)
        {
            Create(1);
            vertexData[0] = vData;
        }

        #endregion

        public void Dispose()
        {
        }

        internal void Render()
        {
            VertexBufferObject vbo = VBOManager.GetVBO(meshType);
            if (indexVBO < 0 && vertexData != null)
            {
                // I need a index array for the VBO.
                indexVBO = vbo.GetNewIndex(vertexData);
                MustUpdate = true;
            }

            if (MustUpdate && vertexData != null)
            {
                for (int i=0;i< vertexData.Length;++i)
                {
                    vbo[indexVBO + i] = vertexData[i];
                }
                MustUpdate = false;
            }

            foreach (Node c in children)
            {
                c.Render();
            }
        }

        private void Create(int numVertex)
        {
            vertexData = new VertexData[numVertex];
            SelectMeshType();
        }

        private void Create(int numVertex, VertexData[] vData)
        {
            Create(numVertex);
            vertexData = new VertexData[numVertex];
            int i = 0;

            for (; i < vData.Length; ++i)
            {
                vertexData[i] = vData[i];
            }

            if (numVertex > vData.Length)
            {
                for (; i < numVertex; ++i)
                {
                    vertexData[i] = VertexData.Reset;
                }
            }
        }

        private void Create(VertexData[] vData)
        {
            Create(vData.Length);
            for (int i = 0; i < vData.Length; ++i)
            {
                vertexData[i] = vData[i];
            }
        }

        private VertexBufferObject.VBOType meshType;

        private void SelectMeshType()
        {
            switch (vertexData.Length)
            {
                case 1:
                    meshType = VertexBufferObject.VBOType.PointsNoTexture;
                break;
            }
        }

    }
}
