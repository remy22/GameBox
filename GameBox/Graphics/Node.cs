using System;
using System.Collections.Generic;

namespace GameBox.Graphics
{
    internal class Node : IDisposable
    {
        internal enum NodeType
        {
            Container,
            Cube
        }

        NodeType nodeType;

        internal Node(NodeType nType)
        {
            nodeType = nType;
            Init();
        }

        private void Init()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
