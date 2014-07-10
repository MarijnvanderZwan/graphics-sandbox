using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace RTS
{
    class Path
    {
        int[,] obstacles;
        List<Point> path;
        Node[,] nodes;
        int width, height;
        Point currentGoal;

        public Path() { }

        public Path(int[,] obstacles)
        {
            this.obstacles = obstacles;
            path = new List<Point>();

            width = obstacles.GetLength(0);
            height = obstacles.GetLength(1);
        }

        public List<Vector3> AStar2D(Vector3 start, Vector3 goal)
        {
            path = new List<Point>();
            currentGoal = new Point(goal.X, goal.Z);
            nodes = new Node[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    nodes[x, y] = new Node(x, y, currentGoal);

            List<Point> open = new List<Point>();
            open.Add(new Point(start.X, start.Z));
            while (open.Count > 0 && nodes[currentGoal.X, currentGoal.Y].Status != NodeStatus.Closed)
            {
                Point current = GetMinimum(open);
                foreach (Point move in GetPossibleMoves())
                {
                    Point p = move + current;

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
            path.Reverse();
            return PathToVector3();
        }

        public List<Vector3> PathToVector3()
        {
            List<Vector3> p = new List<Vector3>();
            foreach (Point v in path)
            {
                p.Add(new Vector3(v.X, 0, v.Y));
            }
            return p;
        }

        public int Distance(Point p1, Point p2)
        {
            return (int)(Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y)) * 10);
        }

        public Point GetMinimum(List<Point> points)
        {
            Point min = points[0];
            foreach (Point p in points)
            {
                if (nodes[p.X, p.Y].Value < nodes[min.X, min.Y].Value)
                    min = p;
            }
            return min;
        }

        public void GetPath()
        {
            Point p = currentGoal;
            path.Add(currentGoal);
            while (nodes[p.X, p.Y].Parent.X != -1)
            {
                p = nodes[p.X, p.Y].Parent;
                path.Add(p);
            }
        }

        public List<Point> GetPossibleMoves()
        {
            List<Point> moves = new List<Point>();

            moves.Add(new Point(1, 0));
            moves.Add(new Point(-1, 0));
            moves.Add(new Point(0, 1));
            moves.Add(new Point(0, -1));

            moves.Add(new Point(1, 1));
            moves.Add(new Point(-1, 1));
            moves.Add(new Point(1, -1));
            moves.Add(new Point(-1, -1));

            return moves;
        }

        public bool OutOfBounds(Point p)
        {
            return p.X >= width || p.Y >= height || p.X < 0 || p.Y < 0;
        }
    }
}
