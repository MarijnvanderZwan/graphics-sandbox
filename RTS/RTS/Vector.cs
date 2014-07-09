using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RTS
{
    public class Vector
    {
        public int X, Y;
        public Vector(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector(float x, float y)
        {
            this.X = (int) x;
            this.Y = (int) y;
        }

        public Vector(Point p)
        {
            this.X = p.X;
            this.Y = p.Y;
        }

        public static Vector operator +(Vector p1, Vector p2)
        {
            return new Vector(p1.X + p2.X, p1.Y + p2.Y);
        }

        public int LengthSquared()
        {
            return X * X + Y * Y;
        }

        public static Vector operator -(Vector p1, Vector p2)
        {
            return new Vector(p1.X - p2.X, p1.Y - p2.Y);
        }
    }
}
