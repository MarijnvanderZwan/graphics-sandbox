using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    public class Unit
    {
        public List<Vector3> goals = new List<Vector3>();
        float positionTreshold = 0.001f;
        Model3D model;
        public Vector3 Position;

        public Unit(String path, Vector3 position)
        {
            Position = position;
            model = new Model3D(path);
        }

        public Unit(Model3D model, Vector3 position)
        {
            Position = position;
            model.Transformation.Translation = position;
            this.model = model;
        }

        public void Update()
        {
            if (!GoalReached())
                Position += Vector3.Normalize(CurrentGoal - Position) * 0.01f;
            else if (goals.Count > 0)
                goals.Remove(CurrentGoal);

            model.Transformation.Translation = Position;
        }

        public void SetGoal(Vector3 goal)
        {
            goals = new List<Vector3>();
            goals.Add(goal);
        }

        public void QueueGoal(Vector3 goal)
        {
            goals.Add(goal);
        }

        public void Draw()
        {
            model.Draw();
        }
        
        public bool GoalReached()
        {
            return (CurrentGoal - Position).LengthSquared() < positionTreshold;
        }

        public Vector3 CurrentGoal
        {
            get
            {
                if (goals.Count > 0) return goals[0];
                return Position;
            }
        }
    }
}
