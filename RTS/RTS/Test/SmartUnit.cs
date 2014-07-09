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

        public SmartUnit()
            : base(new Model3D(Util.ModelPath + @"\barrel.obj"), new Vector3())
        {
            obstacles = World.Instance.Map.obstacles;
        }

        public override void SetGoal(Vector3 goal)
        {
            Path path = new Path(Position, goal, obstacles);
            goals = path.pathVec3;
            goals.Reverse();
        }
    }
}
