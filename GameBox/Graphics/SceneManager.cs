using System;
using System.Collections.Generic;

namespace GameBox.Graphics
{
	public class SceneManager
	{
		private List<Scene> scenes = new List<Scene>();
		private Scene activeScene = null;

		public SceneManager ()
		{
		}

		public void AddScene(Scene newScene)
		{
			scenes.Add(newScene);
		}

		public Scene ActiveScene
		{
			get { return activeScene; }
			set { activeScene = value; }
		}
	}
}

