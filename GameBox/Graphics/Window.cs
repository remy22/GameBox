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
using GameBox.Graphics.VBODefinitions;
using GameBox.Graphics.Scenes;

namespace GameBox.Graphics
{

    class Window : GameWindow
    {
        internal SceneManager sManager;

        public Window() : base(800, 600)
        {
            this.VSync = VSyncMode.Off;
            sManager = new SceneManager(this);

        }

        internal Scene scene;

        protected void InitGraphicsModules()
        {
            Texture.Init();
            //            Texture.createTexture("brick.jpg", "brick");
            VBOManager.Init();
            scene = sManager.newScene("mainExample");
            scene.camera.createCube("cube");
        }

        protected void DestroyGraphicsModules()
        {
//            VBOManager.Delete();
            Texture.Destroy();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitGraphicsModules();
        }
		
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            sManager.Resize();
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

            sManager.Render();
            SwapBuffers();
        }

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
            DestroyGraphicsModules();
		}

    }
}
