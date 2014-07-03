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
        public BoundingBox BoundingBox;


        public Model3D(String fileName) : base()
        {
            Meshes = ModelLoader.LoadModel(fileName);

            foreach (var mesh in Meshes)
                mesh.GenerateVerticesAndIndices();

            CreateBoundingBox();
        }

        public void Draw()
        {
            foreach (var mesh in Meshes)
            {
                mesh.Transformation = Transformation;
                mesh.Draw();
            }
        }

        public void CreateBoundingBox()
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            BoundingBox = new BoundingBox(min, max);

            foreach (var mesh in Meshes)
                BoundingBox = BoundingBox.CreateMerged(BoundingBox, mesh.BoundingBox);
        }


    }
}
