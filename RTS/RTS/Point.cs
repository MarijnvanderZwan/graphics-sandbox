using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class Point
    {
        public int X, Y;

        public Point(float x, float y)
        {
            X = (int)x;
            Y = (int)y;
        }
        
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public int Distance(Point other)
        {
            return (int)(Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y)) * 10);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, 0, Y);
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }

        public override bool Equals(object obj)
        {
            Point p = obj as Point;
            return p == this;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
    }
}
