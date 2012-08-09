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

        public static VertexData Reset = new VertexData
        {
            A = 255,
            R = 255,
            G = 255,
            B = 255,
            Position = Vector3.Zero
        };

        public static VertexData WhitePoint = new VertexData
        {
            A = 255,
            R = 255,
            G = 255,
            B = 255,
            Position = Vector3.Zero
        };
    }
}
