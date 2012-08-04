using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace GameBox.Graphics
{
    internal struct VertexData
    {
        public byte R, G, B, A;
        public Vector3 Position;

        public static int SizeInBytes = 16;

    }
}
