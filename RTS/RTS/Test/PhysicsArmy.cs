using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    public class PhysicsArmy : Army
    {

        public PhysicsArmy() : base()
        {
            Model3D model = new Model3D(Util.ModelPath + @"\townhouse.obj");
            AddUnit(model, new Vector3(0, 0, 0));
            //model = new Model3D(Util.ModelPath + @"\crate.obj");
            //AddUnit(model, new Vector3(5, 0, 0));
        }

        public new void AddUnit(Model3D model, Vector3 position)
        {
            PhysicsUnit u = new PhysicsUnit(model, position);
            units.Add(u);
        }
    }
}