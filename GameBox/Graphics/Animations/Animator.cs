using System;
using GameBox.XMLSerialization;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using GameBox.Events;
using System.Globalization;

namespace GameBox.Graphics.Animations
{
    public class Animator : GBEventReceiver
    {
        protected GBXMLContainer initData;
        private float time;
        private PointF startPosition;
        private PointF endPosition;
        private PointF currentPosition;
        private PointF deltaPosition;
        private bool translate = false;
        private string name = string.Empty;
        private bool isActive = false;
        private double ellapsed = 0;

        public Animator(GBXMLContainer initdata) : base(initdata)
        {
            initData = (GBXMLContainer)initdata.Clone();
            name = InitData.Name;
            Reload();
        }

        public void Reload()
        {
            time = NumberConverter.ParseFloat(initData["Time", "10"].Text);

            if (initData.Exists("Translate"))
            {
                GBXMLContainer transl = initData["Translate"];
                translate = true;
                startPosition = GBXMLContainer.ReadPointFFrom(transl, "StartPosition");
                endPosition = GBXMLContainer.ReadPointFFrom(transl, "EndPosition");
            }
        }

        public GBXMLContainer InitData
        {
            get { return initData; }
        }

        public void Start()
        {
            isActive = true;
            ellapsed = 0;
            if (translate)
            {
                currentPosition = startPosition;
                deltaPosition = new PointF(endPosition.X - startPosition.X, endPosition.Y - startPosition.Y);
            }
        }

        public void Render()
        {
            DispatchGBEvents(RenderingContext.currentEvents);

            if (isActive)
            {
                ellapsed += RenderingContext.e.Time;
                if (ellapsed >= time)
                {
                    isActive = false;
                }
                else
                {
                    if (translate)
                    {
                        currentPosition.X = startPosition.X + ( deltaPosition.X * (((float)ellapsed) / time) );
                        currentPosition.Y = startPosition.Y + ( deltaPosition.Y * (((float)ellapsed) / time) );

                        GL.Translate(currentPosition.X, currentPosition.Y, 0);
                    }
                }
            }
        }

        public override void DispatchAction(GBXMLContainer actionData)
        {
            if (actionData.Name.Equals("Start"))
            {
                Start();
            }
        }
    }
}
