using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RTS
{
    public class Unit
    {
        public List<Vector3> goals = new List<Vector3>();
        float positionTreshold = 0.05f;
        Model3D model;
        public Vector3 Position;
        public Vector3 Scale;

        public Unit(Model3D model, Vector3 position)
        {
            Position = position;
            model.Transformation.Translation = position;
            this.model = model;
            
            Vector3 min = model.BoundingBox.Min;
            Vector3 max = model.BoundingBox.Max;
            Scale = (max - min) / 2;
        }

        public void Update()
        {
            if (!GoalReached())
                Position += Vector3.Normalize(CurrentGoal - Position) * 0.25f;
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
