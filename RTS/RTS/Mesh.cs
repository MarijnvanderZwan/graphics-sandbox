using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace RTS
{
    public class Mesh : GameObjectTextured
    {
        public List<Face> Faces = new List<Face>();
        public List<VertexPositionNormalTexture> vertices = new List<VertexPositionNormalTexture>();
        public List<int> indices = new List<int>();

        public Mesh(String fileName)
        {
            texture = Util.TextureFromBitmap(fileName);
        }

        public void GenerateVerticesAndIndices()
        {
            foreach (Face face in Faces)
            {
                foreach (var vertex in face.Vertices)
                {
                    if (!vertices.Contains(vertex))
                        vertices.Add(vertex);
                    indices.Add(vertices.IndexOf(vertex));
                }
            }
            Vertices = vertices.ToArray();
            Indices = indices.ToArray();
            CopyToBuffer();
        }
    }
}
