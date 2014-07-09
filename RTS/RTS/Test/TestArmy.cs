using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    public class TestArmy : Army
    {

        public TestArmy() : base()
        {
            Model3D model = new Model3D(Util.ModelPath + @"\townhouse.obj");
            AddUnit(model, new Vector3(0, 0, 0));
            model = new Model3D(Util.ModelPath + @"\crate.obj");
            AddUnit(model, new Vector3(5, 0, 0));
        }

        public new void AddUnit(Model3D model, Vector3 position)
        {
            SmartUnit u = new SmartUnit();
            units.Add(u);
        }
    }
}