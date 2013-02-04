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
        protected GBColor[] borderColors = new GBColor[4];

        public Image(GBXMLContainer initData, RenderNode parent)
            : base(initData, parent)
        {
            string textureName = InitData["Image",string.Empty].Text;
            size = GBXMLContainer.ReadSizeF(InitData);

            borderColors[0] = new GBColor(InitData["BorderColors"]["TopLeft"]);
            borderColors[1] = new GBColor(InitData["BorderColors"]["TopRight"]);
            borderColors[2] = new GBColor(InitData["BorderColors"]["BottomLeft"]);
            borderColors[3] = new GBColor(InitData["BorderColors"]["BottomRight"]);

            if (InitData["BorderColors"].Exists("Top"))
            {
                borderColors[0] = new GBColor(InitData["BorderColors"]["Top"]);
                borderColors[1] = new GBColor(InitData["BorderColors"]["Top"]);
            }

            if (InitData["BorderColors"].Exists("Bottom"))
            {
                borderColors[2] = new GBColor(InitData["BorderColors"]["Bottom"]);
                borderColors[3] = new GBColor(InitData["BorderColors"]["Bottom"]);
            }

            if (InitData["BorderColors"].Exists("Left"))
            {
                borderColors[0] = new GBColor(InitData["BorderColors"]["Left"]);
                borderColors[2] = new GBColor(InitData["BorderColors"]["Left"]);
            }

            if (InitData["BorderColors"].Exists("Right"))
            {
                borderColors[1] = new GBColor(InitData["BorderColors"]["Right"]);
                borderColors[3] = new GBColor(InitData["BorderColors"]["Right"]);
            }

            borderColors[0].Multiply(color);
            borderColors[1].Multiply(color);
            borderColors[2].Multiply(color);
            borderColors[3].Multiply(color);

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

            GL.Begin(BeginMode.Quads);

            GL.Color4(borderColors[2].R, borderColors[2].G, borderColors[2].B, borderColors[2].A);
            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex2(rect.Left, rect.Top);
            GL.Color4(borderColors[3].R, borderColors[3].G, borderColors[3].B, borderColors[3].A);
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex2(rect.Right, rect.Top);
            GL.Color4(borderColors[1].R, borderColors[1].G, borderColors[1].B, borderColors[1].A);
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex2(rect.Right, rect.Bottom);
            GL.Color4(borderColors[0].R, borderColors[0].G, borderColors[0].B, borderColors[0].A);
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
