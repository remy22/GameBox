using GameBox.Events;
using GameBox.Input;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace GameBox.Graphics.Scenes
{
    internal class SceneManager : IRenderizable
    {
        private List<Scene> scenes = new List<Scene>();
        private Window parent = null;

        internal SceneManager(Window w)
        {
            parent = w;
        }

        internal Window window
        {
            get { return parent; }
        }

        internal void Resize()
        {
            foreach (Scene s in scenes)
            {
                s.Resize();
            }
        }

        internal void UpdateFrame(UpdateFrameEvent ufe_)
        {
            // TODO: Update only 1 active scene.
            foreach (Scene s in scenes)
            {
                s.UpdateFrame(ufe_);
            }

        }

        public void Render()
        {

            // TODO: Render only 1 active scene.
            foreach (Scene s in scenes)
            {
                s.Render();
            }
        }

        public Scene newScene(string name_)
        {
            Scene ns = new Scene(this);
            ns.Name = name_;
            scenes.Add(ns);
            return ns;
        }
    }
}
