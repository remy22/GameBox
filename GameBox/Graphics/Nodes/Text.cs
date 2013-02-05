using System;
using GameBox.XMLSerialization;
using GameBox.Resources;
using OpenTK.Graphics.OpenGL;
using GameBox.Processes;

namespace GameBox.Graphics.Nodes
{
    class Text : Image
    {
        protected string text = string.Empty;
        protected GBFont font = null;
        protected bool autoText = false;

        public Text(GBXMLContainer initData, RenderNode parent)
            : base(initData, parent)
        {
            text = InitData["Text"].Text;
            font = ProcessManager.ActiveProcess.rManager.GetOrCreateFont(InitData["Font"].Text);
            autoText = bool.Parse(InitData["AutoText","true"].Text);
        }

        public override void setActiveTexture(int index = 0)
        {
            activeTexture = ProcessManager.ActiveProcess.rManager.GetOrCreateTexture(this.name+"."+text,font, text, size, autoText);
            size = activeTexture.size;
            enableTransparency = true;
        }
    }
}
