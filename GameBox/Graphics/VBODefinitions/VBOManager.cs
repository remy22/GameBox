using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBox.Graphics.VBODefinitions
{
    internal static class VBOManager
    {
        internal enum VBOType
        {
            Plane,
            Cube
        }

        private static Dictionary<VBOType, VBOData> vbos = new Dictionary<VBOType, VBOData>();

        internal static void Init()
        {
            vbos[VBOType.Plane] = new CubeVBO();
            vbos[VBOType.Plane].CreateVBO();

            vbos[VBOType.Cube] = new CubeVBO();
            vbos[VBOType.Cube].CreateVBO();
        }

        internal static VBOData getVBO(VBOType type_)
        {
            return vbos[type_];
        }
    }
}
