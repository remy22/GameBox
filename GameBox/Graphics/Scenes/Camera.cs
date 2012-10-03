using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;

namespace GameBox.Graphics.Scenes
{
    internal class Camera : Shape, IRenderizable
    {
        private Scene parentScene_ = null;
        private Vector3 eye;
        private Vector3 target;
        private Vector3 vUp;
        private Size viewPort;

        public Camera(Scene parentScene)
        {
            parentScene_ = parentScene;
            eye = new Vector3(0, 5, 5);
            target = new Vector3(0, 0, 0);
            vUp = new Vector3(0,1,0);
        }

        public void Resize()
        {
        }

        public override void Render()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.Enable(EnableCap.DepthTest);
            Matrix4 lookat = Matrix4.LookAt(eye.X, eye.Y, eye.Z, target.X, target.Y, target.Z, vUp.X, vUp.Y, vUp.Z);
            GL.LoadMatrix(ref lookat);

            base.Render();
        }
    }
}
