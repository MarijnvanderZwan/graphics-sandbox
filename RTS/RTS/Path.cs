using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace RTS
{
    class Path
    {
        int[,] obstacles;
        List<Point> possibleMoves;
        Node[,] nodes;
        int width, height;
        Point currentGoal;

        public Path() { }

        public Path(int[,] obstacles)
        {
            this.obstacles = obstacles;

            width = obstacles.GetLength(0);
            height = obstacles.GetLength(1);

            InitializePossibleMoves();
        }

        public List<Vector3> AStar2D(Vector3 start, Vector3 goal)
        {
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
                foreach (Point move in possibleMoves)
                {
                    Point p = current + move;

                    if (!OutOfBounds(p) && obstacles[p.X, p.Y] == 0 && nodes[p.X, p.Y].Status != NodeStatus.Closed)
                    {
                        if (nodes[p.X, p.Y].Status == NodeStatus.None)
                        {
                            nodes[p.X, p.Y].G = nodes[current.X, current.Y].G + current.Distance(p);
                            nodes[p.X, p.Y].Parent = current;
                            open.Add(p);
                            nodes[p.X, p.Y].Status = NodeStatus.Open;
                        }
                        else if (nodes[p.X, p.Y].G > nodes[current.X, current.Y].G + current.Distance(p))
                        {
                            nodes[p.X, p.Y].G = nodes[current.X, current.Y].G + current.Distance(p);
                            nodes[p.X, p.Y].Parent = current;
                        }
                    }
                }
                open.Remove(current);
                nodes[current.X, current.Y].Status = NodeStatus.Closed;
            }
            Console.WriteLine(GetPath().Count);
            return GetPath();
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

        public List<Vector3> GetPath()
        {
            List<Vector3> path = new List<Vector3>();
            path.Add(currentGoal.ToVector3());

            Point p1 = currentGoal;
            Point p2 = nodes[p1.X, p1.Y].Parent;
            Point p3 = p2;                          // Testing initial three points

            while (nodes[p3.X, p3.Y].Parent.X != -1)
            {
                p3 = nodes[p3.X, p3.Y].Parent;
                if (p3 - p2 != p2 - p1)
                    path.Add(p2.ToVector3());
                p1 = p2;
                p2 = p3;
            }
            path.Reverse();
            return path;
        }

        public void InitializePossibleMoves()
        {
            possibleMoves = new List<Point>();

            possibleMoves.Add(new Point(1, 0));
            possibleMoves.Add(new Point(-1, 0));
            possibleMoves.Add(new Point(0, 1));
            possibleMoves.Add(new Point(0, -1));

            possibleMoves.Add(new Point(1, 1));
            possibleMoves.Add(new Point(-1, 1));
            possibleMoves.Add(new Point(1, -1));
            possibleMoves.Add(new Point(-1, -1));
        }

        public bool OutOfBounds(Point p)
        {
            return p.X >= width || p.Y >= height || p.X < 0 || p.Y < 0;
        }
    }
}
