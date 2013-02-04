using System;
using GameBox.XMLSerialization;
using System.Globalization;

namespace GameBox.Graphics
{
    public class GBColor
    {
        private byte[] b = new byte[4];
        private float[] f = new float[4];

        private const int Alpha = 0;
        private const int Red = 1;
        private const int Green = 2;
        private const int Blue = 3;

        public GBColor(GBXMLContainer initData)
        {
            if (initData.Name.Equals("ColorB"))
            {
                setColorValueFromByte(Alpha, byte.Parse(initData["Alpha", byte.MaxValue.ToString()].Text));
                setColorValueFromByte(Red, byte.Parse(initData["Red", byte.MaxValue.ToString()].Text));
                setColorValueFromByte(Green, byte.Parse(initData["Green", byte.MaxValue.ToString()].Text));
                setColorValueFromByte(Blue, byte.Parse(initData["Blue", byte.MaxValue.ToString()].Text));
            }
            else
            {
                setColorValueFromFloat(Alpha, NumberConverter.ParseFloat(initData["Alpha", "1.0"].Text));
                setColorValueFromFloat(Red, NumberConverter.ParseFloat(initData["Red", "1.0"].Text));
                setColorValueFromFloat(Green, NumberConverter.ParseFloat(initData["Green", "1.0"].Text));
                setColorValueFromFloat(Blue, NumberConverter.ParseFloat(initData["Blue", "1.0"].Text));
            }
        }

        public GBColor(GBColor other)
        {
            setColorValueFromFloat(Alpha, other.A);
            setColorValueFromFloat(Red, other.R);
            setColorValueFromFloat(Green, other.G);
            setColorValueFromFloat(Blue, other.B);
        }

        public static GBColor Multiply(GBColor first, GBColor second)
        {
            GBColor temp = new GBColor(first);
            temp.A *= second.A;
            temp.R *= second.R;
            temp.G *= second.G;
            temp.B *= second.B;
            return temp;
        }

        public GBColor Multiply(GBColor second)
        {
            A *= second.A;
            R *= second.R;
            G *= second.G;
            B *= second.B;
            return this;
        }

        private void setColorValueFromByte(int index, byte value)
        {
            b[index] = value;
            f[index] = (float)((float)value / (float)byte.MaxValue);
        }

        private void setColorValueFromFloat(int index, float value)
        {
            b[index] = (byte)(value * byte.MaxValue);
            f[index] = value;
        }

        public float A
        {
            get { return f[Alpha]; }
            set { setColorValueFromFloat(Alpha, value); }
        }

        public float R
        {
            get { return f[Red]; }
            set { setColorValueFromFloat(Red, value); }
        }

        public float G
        {
            get { return f[Green]; }
            set { setColorValueFromFloat(Green, value); }
        }

        public float B
        {
            get { return f[Blue]; }
            set { setColorValueFromFloat(Blue, value); }
        }

        public byte Ab
        {
            get { return b[Alpha]; }
            set { setColorValueFromByte(Alpha, value); }
        }

        public byte Rb
        {
            get { return b[Red]; }
            set { setColorValueFromByte(Red, value); }
        }

        public byte Gb
        {
            get { return b[Green]; }
            set { setColorValueFromByte(Green, value); }
        }

        public byte Bb
        {
            get { return b[Blue]; }
            set { setColorValueFromByte(Blue, value); }
        }
    }
}
