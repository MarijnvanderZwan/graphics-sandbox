using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    public class Model3D
    {
        public List<Mesh> Meshes = new List<Mesh>();
        public Transformation Transformation = new Transformation();

        public Model3D(String fileName) : base()
        {
            Meshes = ModelLoader.LoadModel(fileName);
            
            foreach (var mesh in Meshes)
                mesh.GenerateVerticesAndIndices();

        }

        public void Draw()
        {
            foreach (var mesh in Meshes)
            {
                mesh.Transformation = Transformation;
                mesh.Draw();
            }
        }


    }
}
