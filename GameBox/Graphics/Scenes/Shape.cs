using GameBox.Graphics.VBODefinitions;
using System;

namespace GameBox.Graphics.Scenes
{
    class Shape : RenderNode
    {
        private VBOData vbo = null;
        private VBODrawProperties vboDrawProperties = new VBODrawProperties();

        public override void RenderInternal()
        {
            if (vbo != null)
            {
                vbo.DrawVBO(vboDrawProperties);
            }
        }

        internal static RenderNode createCube(Camera parentCamera, IRenderizable parent, string name_ = "")
        {
            Shape shape = new Shape();
            shape.vbo = VBOManager.getVBO(VBOManager.VBOType.Cube);
            shape.name = name_;
            shape.parent = parent;
            shape.cameraParent = parentCamera;
            return shape;
        }

    }
}
