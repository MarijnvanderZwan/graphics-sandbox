using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RTS
{
    class SelectionCircle
    {
        public Model3D model;

        public SelectionCircle()
        {
            model = new Model3D(Util.ModelPath + @"\selectionCircle.obj");
            model.Meshes[0].texture = Util.TextureFromBitmap(Util.TexturePath + @"\green_texture.png");
        }

        public void Draw(Vector3 position, Vector3 scale)
        {
            model.Transformation.Translation = position;
            model.Transformation.Scale = scale;
            model.Draw();
        }
    }
}
