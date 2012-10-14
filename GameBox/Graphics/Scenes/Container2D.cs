using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace GameBox.Graphics.Scenes
{
    public class Container2D : RenderNode
    {
        private Rectangle rectangle;

        public Container2D()
        {
            rectangle.X = 0;
            rectangle.Y = 0;
        }

        internal override void PreInit()
        {
            rectangle.Width = this.cameraParent.ViewPort.Width;
            rectangle.Width = this.cameraParent.ViewPort.Height;
        }

        public override void RenderInternal()
        {
            base.RenderInternal();
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

        }

        internal Text2D createText(string name_ = "",string text_="")
        {
            Text2D t2d = createChild<Text2D>(name_);
            t2d.Text = text_;
            return t2d;
        }

    }
}
