using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RTS
{
    public class Army
    {
        public List<Unit> units = new List<Unit>();
        List<Unit> selectedUnits = new List<Unit>();
        SelectionCircle selectionCircle;
        public Army()
        {
            selectionCircle = new SelectionCircle();
        }

        public void Draw()
        {
            foreach (Unit unit in units)
            {
                if (unit.goals.Count > 0)
                {
                    LineModel line = new LineModel(unit.goals, unit.Position,new Vector3(0, 0.5f, 0));
                    line.Draw();
                }
                if (selectedUnits.Contains(unit))
                    selectionCircle.Draw(unit.Position, unit.Scale);
                unit.Draw();
            }
        }

        public void Update()
        {
            foreach (Unit unit in units)
                unit.Update();
        }

        public void AddUnit(Unit unit)
        {
            units.Add(unit);
        }

        public void AddUnit(Model3D model, Vector3 position)
        {
            Unit u = new Unit(model, position);
            units.Add(u);
        }

        public void RemoveUnit(Unit unit)
        {
            units.Remove(unit);
        }

        public void SelectUnits(Rect rect)
        {
            selectedUnits.Clear();
            foreach (Unit unit in units)
            {
                if (rect.Contains(unit.Position))
                    selectedUnits.Add(unit);
            }
        }
        
        public void MoveSelectedUnits(Vector3 goal)
        {
            foreach (Unit unit in selectedUnits)
                unit.SetGoal(goal);
        }

        public void QueueMoveSelectedUnits(Vector3 goal)
        {
            foreach (Unit unit in selectedUnits)
                unit.QueueGoal(goal);
        }
    }
}
