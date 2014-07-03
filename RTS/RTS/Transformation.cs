using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class Transformation
    {
        public Vector3 Rotation;
        public Vector3 Scale;
        public Vector3 Translation;

        public Transformation()
        {
            Rotation = new Vector3(0, 0, 0);
            Scale = new Vector3(1, 1, 1);
            Translation = new Vector3(0, 0, 0);
        }

        public Matrix TransformationMatrix
        {
            get
            {
                return Matrix.CreateScale(Scale) *
                       Matrix.CreateRotationX(Rotation.X) *
                       Matrix.CreateRotationY(Rotation.Y) *
                       Matrix.CreateRotationZ(Rotation.Z) *
                       Matrix.CreateTranslation(Translation);
            }
        }
    }
}
