using System;
using OpenTK;

namespace GameBox.Graphics.Scenes
{
    class Transformation
    {
        private Matrix4 matrix;
        public Vector3 position;
        public Vector3 scale;
        public Vector3 rotation;

        public Transformation()
        {
            matrix = Matrix4.Identity;
            position = new Vector3(0, 0, 0);
            scale = new Vector3(1, 1, 1);
            rotation = new Vector3(0, 0, 0);
        }

        public void UpdateMatrix()
        {
            matrix = Matrix4.Identity;
            matrix.M11 = scale.X;
            matrix.M22 = scale.Y;
            matrix.M33 = scale.Z;
            matrix.M14 = position.X;
            matrix.M24 = position.Y;
            matrix.M34 = position.Z;
            if (rotation.X != 0.0)
                matrix *= Matrix4.CreateRotationX(rotation.X);

            if (rotation.Y != 0.0)
                matrix *= Matrix4.CreateRotationX(rotation.Y);

            if (rotation.Z != 0.0)
                matrix *= Matrix4.CreateRotationX(rotation.Z);

        }
    }
}
