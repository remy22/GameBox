using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;

namespace GameBox.Graphics.Scenes
{
    public class Camera : RenderNode, IRenderizable
    {
        private Scene parentScene = null;
        private Vector3 eye;
        private Vector3 target;
        private Vector3 vUp;
        private Size viewPort;

        public Camera(Scene parentScene_)
        {
            parentScene = parentScene_;
            eye = new Vector3(0, 5, 5);
            target = new Vector3(0, 0, 0);
            vUp = new Vector3(0,1,0);
            viewPort = parentScene.window.Size;
        }

        public void Resize()
        {
            viewPort = parentScene.window.Size;
            GL.Viewport(0, 0, viewPort.Width, viewPort.Height);

            float aspect_ratio = viewPort.Width / (float)viewPort.Height;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);
        }

        public override void RenderInternal()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.Enable(EnableCap.DepthTest);
            Matrix4 lookat = Matrix4.LookAt(eye.X, eye.Y, eye.Z, target.X, target.Y, target.Z, vUp.X, vUp.Y, vUp.Z);
            GL.LoadMatrix(ref lookat);
        }
    }
}
