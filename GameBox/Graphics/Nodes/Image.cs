using System;
using System.Collections.Generic;
using GameBox.XMLSerialization;
using GameBox.Resources;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using GameBox.Processes;

namespace GameBox.Graphics.Nodes
{
    public class Image : RenderNode
    {
        private List<Texture> textures = new List<Texture>();
        protected Texture activeTexture = null;
        protected bool enableTransparency = false;
        protected SizeF size;

        public Image(GBXMLContainer initData, RenderNode parent)
            : base(initData, parent)
        {
            string textureName = InitData["Image",string.Empty].Text;
            size = GBXMLContainer.ReadSizeF(InitData);

            if (textureName != string.Empty)
                textures.Add(ProcessManager.ActiveProcess.rManager.GetOrCreateTexture(textureName));
        }

        public virtual void setActiveTexture(int index = 0)
        {
            if (textures.Count < index)
            {
                activeTexture = ProcessManager.ActiveProcess.rManager.GetTexture("DummyTexture");
            }
            else
            {
                activeTexture = textures[index];
                size.Width = activeTexture.Width;
                size.Height = activeTexture.Height;
            }
        }

        protected void RenderActiveTexture()
        {
            if (enableTransparency)
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            }

            activeTexture.BindTexture();

            SizeF halfSize = new SizeF(size.Width / 2, size.Height / 2);
            RectangleF rect = new RectangleF(position.X - halfSize.Width, position.Y - halfSize.Height, size.Width, size.Height);
            GL.Color3(1.0, 1.0, 1.0);

            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex2(rect.Left, rect.Top);
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex2(rect.Right, rect.Top);
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex2(rect.Right, rect.Bottom);
            GL.TexCoord2(0.0f, 0.0f);
            GL.Vertex2(rect.Left, rect.Bottom);

            GL.End();

            if (enableTransparency)
            {
                GL.Disable(EnableCap.Blend);
            }

            activeTexture.UnbindTexture();
            if (RenderingContext.DrawBoundingBox)
            {
                GL.Begin(BeginMode.Lines);

                GL.Vertex2(rect.Left, rect.Bottom);
                GL.Vertex2(rect.Right, rect.Bottom);
                GL.Vertex2(rect.Right, rect.Top);
                GL.Vertex2(rect.Left, rect.Top);
                GL.Vertex2(rect.Left, rect.Bottom);
                GL.Vertex2(rect.Left, rect.Top);
                GL.Vertex2(rect.Right, rect.Bottom);
                GL.Vertex2(rect.Right, rect.Top);

                GL.End();
            }
        }

        public override void RenderImpl()
        {
            base.RenderImpl();

            if (activeTexture == null)
                setActiveTexture(0);
            RenderActiveTexture();
        }
    }
}
