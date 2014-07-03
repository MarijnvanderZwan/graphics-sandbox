using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    public class CubeModel
    {
        public List<Vertex> Vertices = new List<Vertex>();
        public List<int> Indices = new List<int>();

        public CubeModel(float size, Color color)
        {
            float diameter = size / 2;
            Vertices.Add(new Vertex(new Vector3(-diameter, 0, -diameter), color));
            Vertices.Add(new Vertex(new Vector3(-diameter, 0, diameter), color));
            Vertices.Add(new Vertex(new Vector3(diameter, 0, -diameter), color));
            Vertices.Add(new Vertex(new Vector3(diameter, 0, diameter), color));

            Vertices.Add(new Vertex(new Vector3(-diameter, size, -diameter), color));
            Vertices.Add(new Vertex(new Vector3(-diameter, size, diameter), color));
            Vertices.Add(new Vertex(new Vector3(diameter, size, -diameter), color));
            Vertices.Add(new Vertex(new Vector3(diameter, size, diameter), color));
            
            // Bottom
            Indices.Add(0);
            Indices.Add(2);
            Indices.Add(1);

            Indices.Add(1);
            Indices.Add(2);
            Indices.Add(3);
            
            // Top
            Indices.Add(4);
            Indices.Add(6);
            Indices.Add(5);

            Indices.Add(5);
            Indices.Add(6);
            Indices.Add(7);
            
            // Left side
            Indices.Add(5);
            Indices.Add(1);
            Indices.Add(0);

            Indices.Add(0);
            Indices.Add(4);
            Indices.Add(5);
            
            // Front side
            Indices.Add(4);
            Indices.Add(0);
            Indices.Add(2);

            Indices.Add(2);
            Indices.Add(6);
            Indices.Add(4);
           
            // Right side
            Indices.Add(6);
            Indices.Add(2);
            Indices.Add(3);

            Indices.Add(3);
            Indices.Add(7);
            Indices.Add(6);
            
            // Back side
            Indices.Add(7);
            Indices.Add(3);
            Indices.Add(1);
            
            Indices.Add(1);
            Indices.Add(5);
            Indices.Add(7);
        }
    }
}
