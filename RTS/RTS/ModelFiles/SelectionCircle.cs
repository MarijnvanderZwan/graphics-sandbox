using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RTS
{
    class SelectionCircle
    {
        public Model3D model;

        public SelectionCircle()
        {
            model = new Model3D(Util.ModelPath + @"\selectionCircle.obj");
            model.Meshes[0].Texture = Util.TextureFromFile(Util.TexturePath + @"\green_texture.png");
        }

        public void Draw(Vector3 position, Vector3 scale)
        {
            model.Transformation.Translation = position;
            model.Transformation.Scale = scale;
            model.Draw();
        }
    }
}
