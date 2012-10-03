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
            Scene ns = new Scene();
            ns.Name = name_;
            scenes.Add(ns);
            return ns;
        }
    }
}
