using System;
using OpenTK.Graphics.OpenGL;

namespace GameBox.Graphics
{
    internal class VertexBufferObject
    {
        internal enum VBOType
        {
            PointsNoTexture = 0
        }

        private uint VBOHandle;
        private VertexData[] data = null;
        private VBOType vboType;
        private bool initialized = false;

        private int index = 0;

        internal VertexBufferObject(VBOType type)
        {
            vboType = type;
        }

        internal void Init()
        {
            GL.GenBuffers(1, out VBOHandle);
            initialized = true;

            switch (vboType)
            {
                case VBOType.PointsNoTexture:
                    GL.BindBuffer(BufferTarget.ArrayBuffer, VBOHandle);
                    GL.ColorPointer(4, ColorPointerType.UnsignedByte, VertexData.SizeInBytes, (IntPtr)0);
                    GL.VertexPointer(3, VertexPointerType.Float, VertexData.SizeInBytes, (IntPtr)(4 * sizeof(byte)));
                break;
            }

            index = 0;
            data = new VertexData[100];
        }

        internal void Destroy()
        {
            if (initialized)
                GL.DeleteBuffers(1, ref VBOHandle);
        }

        internal void ResizeArray()
        {
            VertexData[] newInstance = new VertexData[data.Length + 100];
            data.CopyTo(newInstance, 0);
            data = newInstance;
        }

        internal int GetNewIndex(VertexData[] data_)
        {
            while (data.Length <= index + data_.Length)
            {
                ResizeArray();
            }

            int val = index;
            index += data_.Length;
            return val;
        }

        internal VertexData this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }

        internal void Draw()
        {
            // Tell OpenGL to discard old VBO when done drawing it and reserve memory _now_ for a new buffer.
            // without this, GL would wait until draw operations on old VBO are complete before writing to it
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexData.SizeInBytes * data.Length), IntPtr.Zero, BufferUsageHint.StreamDraw);
            // Fill newly allocated buffer
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexData.SizeInBytes * index), data, BufferUsageHint.StreamDraw);
            // Only draw particles that are alive
            GL.DrawArrays(BeginMode.Points, 0, index);
        }
    }
}
