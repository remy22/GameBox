using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using System.Drawing;

namespace GameBox.Graphics
{

    class DeviceDelegate : GameWindow
    {
        const float rotation_speed = 180.0f;
        float angle;
        
		public DeviceDelegate() : base(800, 600)
        {
            this.VSync = VSyncMode.Off;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string version = GL.GetString(StringName.Version);
            int major = (int)version[0];
            int minor = (int)version[2];
            if (major <= 1 && minor < 5)
            {
                this.Exit();
            }

			VBOCreator.Init();
			Texture.Init();
            Texture.createTexture("brick.jpg", "brick");
            GL.ClearColor(System.Drawing.Color.MidnightBlue);
            GL.Enable(EnableCap.DepthTest);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            float aspect_ratio = Width / (float)Height;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[OpenTK.Input.Key.Escape])
                this.Exit();
        }

        private uint frames = 0;
        private double ellapsed = 0.0;
        private double ellapsedOld = 0.0;
        private double fps = 0.0;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            ellapsed += e.Time;
            frames++;
            if (ellapsed - ellapsedOld > 1)
            {
                fps = frames / ellapsed;
                ellapsedOld = ellapsed;
                this.Title = "Ellapsed now: " + e.Time + "FPS: " + 1.0 / e.Time;
            }

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

//            Matrix4 lookat = Matrix4.LookAt(0, 5, 5, 0, 0, 0, 0, 1, 0);
            Matrix4 lookat = Matrix4.LookAt(0, 0, 5, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

//            angle += rotation_speed * (float)e.Time;
//            GL.Rotate(angle, 0.0f, 1.0f, 0.0f);

			VBOCreator.getVbo(VBOCreator.VBOType.Cube).Draw();

            SwapBuffers();
        }

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
			Texture.Destroy();
		}

    }
}
