using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;
using GameBox.Input;
using GameBox.Services;
using GameBox.Events;

namespace GameBox.Graphics.Scenes
{
    public class Camera : RenderNode, IRenderizable
    {
        private Scene parentScene = null;
        private Vector3 eye;
        private Vector3 target;
        private Vector3 vUp;

        public Camera(Scene parentScene_)
        {
            parentScene = parentScene_;
            eye = new Vector3(0, 5, 5);
            target = new Vector3(0, 0, 0);
            vUp = new Vector3(0,1,0);
            cameraParent = this;
        }

        public Size ViewPort
        {
            get { return parentScene.window.Size; }
        }

        public void Resize()
        {
        }

        public override void UpdateFrame(UpdateFrameEvent ufe)
        {
            bool cameraEyeMoved = false;

            if (ufe.getKeyState(GBKey.A))
            {
                eye.X -= 0.05f;
                cameraEyeMoved = true;
            }
            else if (ufe.getKeyState(GBKey.D))
            {
                eye.X += 0.05f;
                cameraEyeMoved = true;
            }
            else if (ufe.getKeyState(GBKey.W))
            {
                eye.Y += 0.05f;
                cameraEyeMoved = true;
            }
            else if (ufe.getKeyState(GBKey.S))
            {
                eye.Y -= 0.05f;
                cameraEyeMoved = true;
            }

            if (cameraEyeMoved)
                Logger.debug("Eye:" + eye);
        }

        public override void RenderInternal()
        {
            GL.Enable(EnableCap.DepthTest);

            GL.Viewport(0, 0, parentScene.window.Size.Width, parentScene.window.Size.Height);

            float aspect_ratio = parentScene.window.Size.Width / (float)parentScene.window.Size.Height;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);

            GL.MatrixMode(MatrixMode.Modelview);

            Matrix4 lookat = Matrix4.LookAt(eye.X, eye.Y, eye.Z, target.X, target.Y, target.Z, vUp.X, vUp.Y, vUp.Z);
            GL.LoadMatrix(ref lookat);
        }
    }
}
