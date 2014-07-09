using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace RTS
{
    class Path
    {
        Vector start, goal;
        int[,] obstacles;
        List<Vector> path;
        public List<Vector3> pathVec3;
        Node[,] nodes;
        int size;

        public Path() { }

        public Path(Vector3 start, Vector3 goal, int[,] obstacles)
        {
            this.start = new Vector(start.X, start.Z);
            this.goal = new Vector(goal.X, goal.Z); ;
            this.obstacles = obstacles;
            path = new List<Vector>();
            AStar2D();
        }

        public void AStar2D()
        {
            size = obstacles.GetLength(0);
            nodes = new Node[size, size];
            List<Vector> moves = GetPossibleMoves();

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    nodes[x, y] = new Node(x, y, goal);

            List<Vector> open = new List<Vector>();
            open.Add(start);

            while (open.Count > 0 && nodes[goal.X, goal.Y].Status != NodeStatus.Closed)
            {
                Vector current = GetMinimum(open);
                foreach (Vector move in moves)
                {
                    Vector p = move + current;

                    if (!OutOfBounds(p) && obstacles[p.X, p.Y] == 0 && nodes[p.X, p.Y].Status != NodeStatus.Closed)
                    {
                        if (nodes[p.X, p.Y].Status == NodeStatus.None)
                        {
                            nodes[p.X, p.Y].G = nodes[current.X, current.Y].G + Distance(current, p);
                            nodes[p.X, p.Y].Parent = current;
                            open.Add(p);
                            nodes[p.X, p.Y].Status = NodeStatus.Open;
                        }
                        else if (nodes[p.X, p.Y].G > nodes[current.X, current.Y].G + Distance(current, p))
                        {
                            nodes[p.X, p.Y].G = nodes[current.X, current.Y].G + Distance(current, p);
                            nodes[p.X, p.Y].Parent = current;
                        }
                    }
                }
                open.Remove(current);
                nodes[current.X, current.Y].Status = NodeStatus.Closed;

            }
            GetPath();
            PathToVector3();
        }

        public void PathToVector3()
        {
            pathVec3 = new List<Vector3>();
            foreach (Vector v in path)
            {
                pathVec3.Add(new Vector3(v.X, 0, v.Y));
            }
        }

        public int Distance(Vector p1, Vector p2)
        {
            return (int)(Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y)) * 10);
        }

        public Vector GetMinimum(List<Vector> points)
        {
            Vector min = points[0];
            foreach (Vector p in points)
            {
                if (nodes[p.X, p.Y].Value < nodes[min.X, min.Y].Value)
                    min = p;
            }
            return min;
        }

        public void GetPath()
        {
            Vector p = goal;
            path.Add(goal);
            while (nodes[p.X, p.Y].Parent.X != -1)
            {
                p = nodes[p.X, p.Y].Parent;
                path.Add(p);
            }
        }

        public List<Vector> GetPossibleMoves()
        {
            List<Vector> moves = new List<Vector>();

            moves.Add(new Vector(1, 0));
            moves.Add(new Vector(-1, 0));
            moves.Add(new Vector(0, 1));
            moves.Add(new Vector(0, -1));

            moves.Add(new Vector(1, 1));
            moves.Add(new Vector(-1, 1));
            moves.Add(new Vector(1, -1));
            moves.Add(new Vector(-1, -1));

            return moves;
        }

        public bool OutOfBounds(Vector p)
        {
            return p.X >= size || p.Y >= size || p.X < 0 || p.Y < 0;
        }
    }
}
