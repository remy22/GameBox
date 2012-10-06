using System;
using System.Collections.Generic;
using GameBox.Graphics.VBODefinitions;

namespace GameBox.Graphics.Scenes
{
    public class RenderNode : INamerable, IRenderizable
    {
        protected string name;
        private List<RenderNode> children = new List<RenderNode>();
        internal protected IRenderizable parent = null;
        internal protected Camera cameraParent = null;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual void RenderInternal()
        {
        }

        public void Render()
        {
            RenderInternal();
            RenderChildren();
        }

        protected void RenderChildren()
        {
            foreach (RenderNode s in children)
            {
                s.Render();
            }
        }

        internal RenderNode createCube(string name_ = "")
        {
            RenderNode t = Shape.createCube(cameraParent, this, name_);
            children.Add(t);
            return t;
        }

    }
}
