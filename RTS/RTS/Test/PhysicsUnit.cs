using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RTS
{
    public class PhysicsUnit : Unit
    {

        public PhysicsUnit(Model3D model, Vector3 position) : base(model, position)
        {
            
        }

        public override void Update()
        {
            if (InputState.IsKeyDown(Keys.Left))
                model.Transformation.Translation += new Vector3(-1, 0, 0);
            if (InputState.IsKeyDown(Keys.Right))
                model.Transformation.Translation += new Vector3(1, 0, 0);
            if (InputState.IsKeyDown(Keys.Up))
                model.Transformation.Translation += new Vector3(0, 0, -1);
            if (InputState.IsKeyDown(Keys.Down))
                model.Transformation.Translation += new Vector3(0, 0, 1);
        }
    }
}
