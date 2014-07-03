using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    public class Indicator : GameObject
    {
        public Indicator() : base()
        {
            SetVertices();
        }

        public void SetVertices()
        {
            List<Vertex> vertices = new List<Vertex>();
            vertices.Add(new Vertex(new Vector3(-0.5f, 0, -0.5f), Color.Red));
            vertices.Add(new Vertex(new Vector3(-0.5f, 0, 0.5f), Color.Red));
            vertices.Add(new Vertex(new Vector3(0.5f, 0, -0.5f), Color.Red));
            vertices.Add(new Vertex(new Vector3(0.5f, 0, 0.5f), Color.Red));
            vertices.Add(new Vertex(new Vector3(0, 0.5f, 0), Color.Red));
            vertices.Add(new Vertex(new Vector3(0, -0.5f, 0), Color.Red));

            List<int> indices = new List<int>();
            indices.Add(1);
            indices.Add(3);
            indices.Add(5);

            indices.Add(3);
            indices.Add(4);
            indices.Add(5);

            indices.Add(4);
            indices.Add(2);
            indices.Add(5);

            indices.Add(2);
            indices.Add(1);
            indices.Add(5);

            indices.Add(1);
            indices.Add(3);
            indices.Add(6);

            indices.Add(3);
            indices.Add(4);
            indices.Add(6);

            indices.Add(4);
            indices.Add(2);
            indices.Add(6);

            indices.Add(2);
            indices.Add(1);
            indices.Add(6);
            Indices = indices.ToArray();
            Vertices = vertices.ToArray();
            CopyToBuffer();
        }
    }
}
