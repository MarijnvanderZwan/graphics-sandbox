using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
