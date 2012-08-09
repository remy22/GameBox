using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace GameBox.Graphics
{
    class DeviceDelegate : GameWindow
    {
        /// <summary>Creates a 800x600 window with the specified title.</summary>
        public DeviceDelegate() : base(800, 600)
        {
            this.VSync = VSyncMode.Off;
        }

        Node parentNode = null;
        int MaxParticleCount = 100;

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(.1f, 0f, .1f, 0f);
            GL.Enable(EnableCap.DepthTest);

            // Setup parameters for Points
//            GL.PointSize(5f);
            GL.Enable(EnableCap.PointSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);

            // Setup VBO state
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.VertexArray);

/*
            GL.GenBuffers(1, out VBOHandle);

            // Since there's only 1 VBO in the app, might aswell setup here.
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOHandle);
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, VertexData.SizeInBytes, (IntPtr)0);
            GL.VertexPointer(3, VertexPointerType.Float, VertexData.SizeInBytes, (IntPtr)(4 * sizeof(byte)));
*/
            VBOManager.Init();
            parentNode = new Node(Node.NodeType.Container);

            Random rnd = new Random();
            Vector3 temp = Vector3.Zero;

            // generate some random stuff for the particle system
            VertexData vData = new VertexData();
            for (uint i = 0; i < MaxParticleCount; i++)
            {
                Node child = parentNode.CreateChild(Node.NodeType.Point);
                vData.R = (byte)rnd.Next(0, 256);
                vData.G = (byte)rnd.Next(0, 256);
                vData.B = (byte)rnd.Next(0, 256);
                vData.A = (byte)rnd.Next(0, 256); // isn't actually used
                vData.Position = Vector3.Zero; // all particles are born at the origin
                child[0] = vData;
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            VBOManager.Destroy();
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 p = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 0.1f, 50.0f);
            GL.LoadMatrix(ref p);

            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 mv = Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            GL.LoadMatrix(ref mv);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[Key.Escape])
            {
                Exit();
            }

            VertexData vData = new VertexData();
            Random rnd = new Random();
            /*
            for (int i = 0; i < MaxParticleCount; i++)
            {
                Node child = parentNode.CreateChild(Node.NodeType.Point);
                vData.R = (byte)rnd.Next(0, 256);
                vData.G = (byte)rnd.Next(0, 256);
                vData.B = (byte)rnd.Next(0, 256);
                vData.A = (byte)rnd.Next(0, 256); // isn't actually used
                vData.Position = Vector3.Zero;
//                vData.Position = new Vector3((float)(rnd.NextDouble() * 2) - 0.5f,
//                    (float)(rnd.NextDouble() * 2) - 0.5f,
//                    (float)0.0);
//                parentNode.Child(i)[0] = vData;
            }
        */
            parentNode.Render();
        }

        private uint frames = 0;
        private double ellapsed = 0.0;
        private double ellapsedOld = 0.0;
        private double fps = 0.0;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            ellapsed += e.Time;
            frames++;
            if (ellapsed - ellapsedOld > 1)
            {
                fps = frames / ellapsed;
                ellapsedOld = ellapsed;
                this.Title = MaxParticleCount + " Points. Ellapsed now: " + e.Time + "FPS: " + 1.0 / e.Time;
            }

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushMatrix();

            GL.Translate(0f, 0f, 0);

            VBOManager.Draw();
            /*
            // Tell OpenGL to discard old VBO when done drawing it and reserve memory _now_ for a new buffer.
            // without this, GL would wait until draw operations on old VBO are complete before writing to it
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexData.SizeInBytes * MaxParticleCount), IntPtr.Zero, BufferUsageHint.StreamDraw);
            // Fill newly allocated buffer
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexData.SizeInBytes * MaxParticleCount), VBO, BufferUsageHint.StreamDraw);
            // Only draw particles that are alive
            GL.DrawArrays(BeginMode.Points, MaxParticleCount - VisibleParticleCount, VisibleParticleCount);
//            GL.DrawElements(BeginMode.Points, VisibleParticleCount, DrawElementsType.UnsignedShort, MaxParticleCount - VisibleParticleCount);
            */
//            tempMesh.Render();
            GL.PopMatrix();

            SwapBuffers();
        }
    }
}
