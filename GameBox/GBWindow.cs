using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using GameBox.Graphics;

namespace GameBox
{
	public class GBWindow : GameWindow
	{
		public static GBWindow CreateFromProperties()
		{
			int w = Convert.ToInt32(GBProperties.Properties["window"]["screenResolution"]["x","320"].Text);
			int h = Convert.ToInt32(GBProperties.Properties["window"]["screenResolution"]["y","200"].Text);
			int bpp = Convert.ToInt32(GBProperties.Properties["window"]["bpp","16"].Text);

			GraphicsMode gMode = new GraphicsMode(new ColorFormat(bpp));

			GBInfo.WriteLine("Creating window with: ");
			GBInfo.WriteLine("w: "+w+" h: "+h);
			GBInfo.WriteLine("Graphics Mode: "+gMode);
			GBWindow ret = new GBWindow(w, h, gMode, "GameBox");
			PostProperties(ret);
			return ret;
		}

		private static void PostProperties (GBWindow ret)
		{
			GBInfo.WriteLine("Created window with: ");
			GBInfo.WriteLine("w: "+ret.Width+" h: "+ret.Height);
			GBInfo.WriteLine("WindowInfo: "+ret.WindowInfo);
			GBInfo.WriteLine("GraphicsMode: "+ret.Context.GraphicsMode);
		}

		private string baseTitle;
        internal bool ShowBoundingBox = false;

		private GBWindow (int w, int h, GraphicsMode gMode, string baseTitle) : base(w, h, gMode, baseTitle)
		{
			this.baseTitle = baseTitle;
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GBSystem.OnLoadWindow(e);
        }

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

            GBSystem.OnUpdateFrame(e);
			
			if (Keyboard[OpenTK.Input.Key.Escape])
				this.Exit();

			if (Keyboard[OpenTK.Input.Key.Number1])
				this.VSync = VSyncMode.Off;

			if (Keyboard[OpenTK.Input.Key.Number2])
				this.VSync = VSyncMode.On;

			if (Keyboard[OpenTK.Input.Key.Number3])
				this.VSync = VSyncMode.Adaptive;

            if (Keyboard[OpenTK.Input.Key.Number4])
                RenderingContext.DrawBoundingBox = true;

            if (Keyboard[OpenTK.Input.Key.Number5])
                RenderingContext.DrawBoundingBox = false;
		}

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            GBSystem.OnResize(e);
        }

		private double ellapsed = 0.0;
		
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);
			ellapsed += e.Time;
			if (ellapsed > 1)
			{
				Title = baseTitle + " Ellapsed now: " + e.Time + "FPS: " + 1.0 / e.Time;
                ellapsed = 0;
			}

            GBSystem.OnRenderFrame(e);
			
			SwapBuffers();
		}
	}
}
