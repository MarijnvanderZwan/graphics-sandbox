﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RTS
{

    public class Node
    {
        int g, h;
        Point parent = new Point(-1, -1);
        NodeStatus status = NodeStatus.None;

        public Node() { }

        public Node(int x, int y, Point goal)
        {
            g = int.MaxValue / 2;
            h = Math.Abs(goal.X - x) + Math.Abs(goal.Y - y);
        }

        public Point Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public int Value
        {
            get { return g + h * 10; }
        }

        public int G
        {
            get { return g; }
            set { g = value; }
        }

        public NodeStatus Status
        {
            get { return status; }
            set { status = value; }
        }
    }

    public enum NodeStatus
    {
        Open,
        Closed,
        None,
    };
}
