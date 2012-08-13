using System;
using System.Runtime.InteropServices;
using OpenTK;
using System.Drawing;

namespace GameBox.Graphics
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct VertexPositionColor
    {
        public Vector3 Position;
        public uint Color;

        public VertexPositionColor(float x, float y, float z, Color color)
        {
            Position = new Vector3(x, y, z);
            Color = ToRgba(color);
        }

        static uint ToRgba(Color color)
        {
            return (uint)color.A << 24 | (uint)color.B << 16 | (uint)color.G << 8 | (uint)color.R;
        }
    }
}
