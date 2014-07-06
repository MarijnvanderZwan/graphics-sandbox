
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
namespace RTS
{
    public class Mesh : GameObjectTextured
    {
        public List<Face> Faces = new List<Face>();
        public List<VertexPositionNormalTexture> vertices = new List<VertexPositionNormalTexture>();
        public List<int> indices = new List<int>();
        public BoundingBox BoundingBox;

        public Mesh(String fileName)
        {
            Texture = Util.TextureFromFile(fileName);
        }

        public void GenerateVerticesAndIndices()
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            foreach (Face face in Faces)
            {
                foreach (var vertex in face.Vertices)
                {
                    if (!vertices.Contains(vertex))
                    {
                        Vector3 pos = vertex.Position;
                        if (pos.X < min.X) min.X = pos.X;
                        if (pos.X > max.X) max.X = pos.X;
                        if (pos.Y < min.Y) min.Y = pos.Y;
                        if (pos.Y > max.Y) max.Y = pos.Y;
                        if (pos.Z < min.Z) min.Z = pos.Z;
                        if (pos.Z > max.Z) max.Z = pos.Z;

                        vertices.Add(vertex);
                    }
                    indices.Add(vertices.IndexOf(vertex));
                }
            }
            BoundingBox = new BoundingBox(min, max);

            Vertices = vertices.ToArray();
            Indices = indices.ToArray();
            CopyToBuffer();
        }
    }
}
