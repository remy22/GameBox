using System;
using GameBox.Processes;
using OpenTK;
using GameBox.Events;
using System.Collections.Generic;

namespace GameBox.Graphics
{
    public static class RenderingContext
    {
        public static Scene RenderingScene = null;
        public static Process RenderingProcess = null;

        public static bool DrawBoundingBox = false;
        public static FrameEventArgs e;
		public static List<GBEvent> currentEvents;

    }
}
