﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBox.Graphics
{
    interface INodeDefinition
    {
        VertexPositionColor[] Vertexs;
        short[] Elements;
    }
}
