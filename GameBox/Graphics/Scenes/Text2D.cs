using System;
using System.Drawing;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GameBox.Graphics.Scenes
{
    public class Text2D : RenderNode
    {
        private string text = String.Empty;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
