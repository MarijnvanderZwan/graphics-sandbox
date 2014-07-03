using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class Rect
    {
        Vector2 p1 = new Vector2();
        Vector2 p2 = new Vector2();
        public Rect(float x1, float y1, float x2, float y2)
        {
            p1 = new Vector2(Math.Min(x1, x2), Math.Min(y1, y2));
            p2 = new Vector2(Math.Max(x1, x2), Math.Max(y1, y2));
        }

        public bool Contains(Vector3 v)
        {
            return v.X >= p1.X && v.X <= p2.X && v.Z >= p1.Y && v.Z <= p2.Y;
        }
    }
}
