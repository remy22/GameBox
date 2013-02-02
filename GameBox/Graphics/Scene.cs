using System;
using GameBox.XMLSerialization;
using GameBox.Resources;
using System.Collections.Generic;
using GameBox.Processes;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK;

namespace GameBox.Graphics
{
	public class Scene : RenderNode
	{
        private bool isFirst = false;
        protected Process parentProcess = null;
        protected SizeF size;

		public Scene (GBXMLContainer data, Process parentProcess_) : base(data, null)
		{
            parentProcess = parentProcess_;
            size = GBXMLContainer.ReadSizeF(InitData);
            isFirst = bool.Parse(InitData["IsFirst", bool.FalseString].Text);

            LoadResourceInfo();
		}

        public bool IsFirst { get { return isFirst; } }

		internal void LoadNotLoadedResources()
        {
            foreach (string resString in XMLSerializeContext.resourceList)
            {
                Resource res = ProcessManager.ActiveProcess.rManager.GetResource(resString);
                GBDebug.WriteLineIf(res.Loaded, "Resource " + res + "(" + res.FileName + ") already loaded.");

                if (!res.Loaded)
                {
                    GBDebug.WriteLine("Loading resource " + res + "(" + res.FileName + ")...");
                    res.Load(parentProcess);
                }
            }
        }

        private void LoadResourceInfo()
        {
            GBXMLContainer resources = InitData["Resources"];
            foreach (GBXMLContainer resource in resources.Children)
            {
                string nodeName = resource.Name;
                string type = resource["Type"].Text;
                string fileName = resource["FileName"].Text;
                Type chType = Type.GetType("GameBox.Resources." + type);
                Resource newResource = ProcessManager.ActiveProcess.rManager.GetOrCreateResource(chType, nodeName, fileName);
                newResource.InitData = (GBXMLContainer)resource.Clone();
				XMLSerializeContext.resourceList.Add(nodeName);
            }
        }

        protected void LoadScene()
        {
            GBInfo.WriteLine("Loading scene " + name + "...");
            LoadNotLoadedResources();
        }

        public uint StartFrame()
        {
            return Render();
        }

        public override uint Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            RenderingContext.RenderingScene = this;

            uint cont = base.Render();

            RenderingContext.RenderingScene = null;
            return cont;

        }

        internal void SetProjection()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(
                position.X - (size.Width / 2),
                (size.Width / 2),
                position.Y - (size.Height / 2),
                (size.Height / 2),
                -1.0, 
                1.0);
            GL.MatrixMode(MatrixMode.Modelview);
        }
	}
}
