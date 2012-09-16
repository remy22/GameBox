using System;
using System.Runtime.InteropServices;

namespace GameBox.Graphics.VBODefinitions
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VBO {
		public int vertexBufferID;
		public int normalBufferID;
		public int indiciesBufferID;
		public int textureBufferID;
		public int elementCount;
	}
}

