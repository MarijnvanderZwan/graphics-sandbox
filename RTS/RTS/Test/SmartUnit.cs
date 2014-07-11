using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class SmartUnit : Unit
    {
        int[,] obstacles;
        Path path;

        public SmartUnit(Model3D model, Vector3 position) : base(model, position)
        {
            obstacles = World.Instance.Map.obstacles;
            path = new Path(obstacles);
        }

        public override void SetGoal(Vector3 goal)
        {
            if (goal.X >= 0 && goal.X < obstacles.GetLength(0) &&
                goal.Z >= 0 && goal.Z < obstacles.GetLength(1) && 
                obstacles[(int)goal.X, (int)goal.Z] == 0)
                goals = path.AStar2D(Position, goal);
        }
    }
}
