using System;
using System.Collections.Generic;
using GameBox.Graphics.VBODefinitions;
using GameBox.Events;

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

        public virtual void UpdateFrame(UpdateFrameEvent ufe)
        {
        }

        internal void UpdateFrameInternal(UpdateFrameEvent ufe)
        {
            UpdateFrame(ufe);
            foreach (RenderNode node in children)
            {
                node.UpdateFrame(ufe);
            }
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

        internal virtual void PreInit()
        {
        }

        internal T createChild<T>(string name_ = "") where T : RenderNode, new()
        {
            T newNode = new T();
            newNode.name = name_;
            newNode.parent = this;
            newNode.cameraParent = this.cameraParent;
            children.Add(newNode);
            newNode.PreInit();
            return newNode;
        }

        internal Shape createCube(string name_ = "")
        {
            Shape shape = createChild<Shape>(name_);
            shape.defineAsCube();
            return shape;
        }

        internal Shape createPlane(string name_ = "")
        {
            Shape shape = createChild<Shape>(name_);
            shape.defineAsPlane();
            return shape;
        }
    }
}
