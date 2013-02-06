using System;
using System.Collections.Generic;
using GameBox.XMLSerialization;
using OpenTK;
using System.Drawing;
using GameBox.Graphics.Nodes;
using GameBox.Graphics.Animations;
using GameBox.Events;
using OpenTK.Graphics.OpenGL;
using GameBox.Processes;

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
        protected GBColor color;
        protected string state = string.Empty;

        public RenderNode(GBXMLContainer initData, RenderNode parent) : base(initData)
        {
            Initdata = (GBXMLContainer)initData.Clone();
            Parent = parent;
            name = InitData.Name;

            LoadPatterns();

            state = InitData["InitialState"].Text;
            position = GBXMLContainer.ReadPointF(InitData);
            color = new GBColor(InitData["Color"]);

            zOrder = NumberConverter.ParseFloat(Initdata["ZOrder", "0.0"].Text);
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
				GBXMLContainer animatorParsed = ProcessManager.ActiveProcess.patternObjects.ParsePattern(animator);
				animators.Add(new Animator(animatorParsed));
            }
        }

        internal void LoadPatterns()
        {
            GBXMLContainer patternObjects = InitData["PatternObjects"];
            foreach (GBXMLContainer container in patternObjects.Children)
            {
                ProcessManager.ActiveProcess.patternObjects.AddObject(container);
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

            GL.PushMatrix();

            foreach (Animator anim in animators)
            {
                anim.Render();
            }
            
            GL.Translate(position.X, position.Y, 0);

            if (name.Equals("MenuScene") && RenderingContext.currentEvents.Count > 0)
            {
                int a = 0;
            }

            DispatchGBEvents(RenderingContext.currentEvents);

            RenderImpl();
            for (int i = 0; i < childrenNode.Count; i++) {
				RenderNode node = childrenNode [i];
				count += node.Render ();
			}

            GL.PopMatrix();
            return count;
        }

        public virtual GBXMLContainer getProperty(string property)
        {
            switch (property)
            {
                case "State":
                    return GBXMLContainer.LoadFromString("<"+property+">"+state+"</"+property+">");
                default:
                    return new GBXMLContainer();
            }
        }

        public virtual void RenderImpl()
        {
        }

        public float ZOrder
        {
            get { return zOrder; }
        }

        public override void DispatchAction(GBEvent evnt, string action, GBXMLContainer actionData)
        {
            switch (action)
            {
                case "ChangeState":
                    state = actionData["ActionParameter1"].Text;
                    GBDebug.WriteLine("New state (" + name + "):" + state);
                    break;
            }
        }

    }
}
