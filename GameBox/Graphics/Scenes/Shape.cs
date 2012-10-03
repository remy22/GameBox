using System;
using System.Collections.Generic;
using GameBox.Graphics.VBODefinitions;

namespace GameBox.Graphics.Scenes
{
    public class Shape : INamerable, IRenderizable
    {
        private string name;
        VBOData vbo = null;
        private List<Shape> children = new List<Shape>();
        private IRenderizable parent = null;
        private Camera cameraParent = null;
        private VBODrawProperties vboDrawProperties = new VBODrawProperties();

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual void Render()
        {
            if (vbo != null)
            {
                vbo.DrawVBO(vboDrawProperties);
            }

            foreach (Shape s in children)
            {
                s.Render();
            }
        }

        internal Shape createCube(string name_ = "")
        {
            Shape t = Shape.createCube(cameraParent, this, name_);
            children.Add(t);
            return t;
        }

        internal static Shape createCube(Camera parentCamera, IRenderizable parent, string name_ = "")
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
