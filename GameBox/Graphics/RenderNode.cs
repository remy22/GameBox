using System;
using System.Collections.Generic;
using GameBox.XMLSerialization;
using OpenTK;
using System.Drawing;
using GameBox.Graphics.Nodes;
using GameBox.Graphics.Animations;
using GameBox.Events;

namespace GameBox.Graphics
{
    public class RenderNode : GBEventReceiver
    {
        private GBXMLContainer Initdata;
        private RenderNode Parent;
        protected string name;
        protected List<RenderNode> childrenNode = new List<RenderNode>();
        protected PointF position;
        protected bool visible = true;
        protected float zOrder;
        protected List<Animator> animators = new List<Animator>();

        public RenderNode(GBXMLContainer initData, RenderNode parent) : base(initData)
        {
            Initdata = (GBXMLContainer)initData.Clone();
            Parent = parent;
            name = InitData.Name;

            position = GBXMLContainer.ReadPointF(InitData);

            zOrder = float.Parse(Initdata["ZOrder", "0.0"].Text);
            GBXMLContainer ch = Initdata["Children"];
            foreach (GBXMLContainer container in ch.Children)
            {
                string type = container["Type"].Text;
                Type chType = Type.GetType("GameBox.Graphics.Nodes." + type);
                RenderNode newChild = (RenderNode)Activator.CreateInstance(chType, container, this);
                childrenNode.Add(newChild);
            }

            GBXMLContainer animXML = Initdata["Animators"];
            foreach (GBXMLContainer animator in animXML.Children)
            {
                animators.Add(new Animator(animator));
            }

        }

        protected GBXMLContainer InitData
        {
            get { return Initdata; }
        }

        public string Name
        {
            get { return name; }
        }

        public virtual uint Render()
        {
            uint count = 1;

            foreach (Animator anim in animators)
            {
                anim.Update(RenderingContext.e);
            }

            RenderImpl();
            foreach (RenderNode node in childrenNode)
            {
                count += node.Render();
            }
            return count;
        }

        public virtual void RenderImpl()
        {
        }

        public float ZOrder
        {
            get { return zOrder; }
        }

        public override void DispatchGBEvent(GBEvent evnt)
        {
            base.DispatchGBEvent(evnt);
            foreach (RenderNode node in childrenNode)
            {
                node.DispatchGBEvent(evnt);
            }
        }
    }
}
