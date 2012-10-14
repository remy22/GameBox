using GameBox.Graphics.VBODefinitions;
using System;

namespace GameBox.Graphics.Scenes
{
    public class Shape : RenderNode
    {
        private VBOData vbo = null;

        public override void RenderInternal()
        {
            if (vbo != null)
            {
                vbo.DrawVBO(-1);
            }
        }

        public void defineAsCube()
        {
            vbo = new CubeVBO();
            vbo.CreateVBO();
        }

        public void defineAsPlane()
        {
            vbo = new PlaneVBO();
            vbo.CreateVBO();
        }
    }
}
