using GameBox.Events;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace GameBox.Graphics.Scenes
{
    public class Scene : INamerable, IRenderizable
    {
        private Camera cam;
        private string name;
        private SceneManager parent;

        internal Scene(SceneManager parent_)
        {
            parent = parent_;
            cam = new Camera(this);
        }

        internal Window window
        {
            get { return parent.window; }
        }

        internal void Resize()
        {
            cam.Resize();
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public Camera camera
        {
            get { return cam; }
        }

        public void UpdateFrame(UpdateFrameEvent ufe)
        {
            cam.UpdateFrameInternal(ufe);
        }

        public void Render()
        {
            GL.ClearColor(System.Drawing.Color.MidnightBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            cam.Render();
        }
    }
}
