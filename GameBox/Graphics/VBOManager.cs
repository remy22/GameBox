using System;

namespace GameBox.Graphics
{
    internal static class VBOManager
    {
        private static VertexBufferObject[] vbos = null;
        private const uint NumVBOs = 1;

        internal static void Init()
        {
            vbos = new VertexBufferObject[NumVBOs];

            for (int i = 0; i < NumVBOs; ++i)
            {
                switch (i)
                {
                    case 0:
                        vbos[i] = new VertexBufferObject(VertexBufferObject.VBOType.PointsNoTexture);
                    break;
                }
                vbos[i].Init();
            }
        }

        internal static void Draw()
        {
            for (int i = 0; i < NumVBOs; ++i)
            {
                vbos[i].Draw();
            }
        }

        internal static void Destroy()
        {
            for (int i = 0; i < NumVBOs; ++i)
            {
                vbos[i].Destroy();
            }
        }

        internal static VertexBufferObject GetVBO(int index)
        {
            return vbos[index];
        }

        internal static VertexBufferObject GetVBO(VertexBufferObject.VBOType type)
        {
            switch (type)
            {
                case VertexBufferObject.VBOType.PointsNoTexture:
                    return vbos[0];
            }

            return null;
        }

    }
}
