using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RTS
{
    public class Grid : GameObject
    {
        public Grid(int width, int height, float cellSize) : base()
        {
            primitiveType = PrimitiveType.LineList;

            float minX = -width * cellSize / 2;
            float maxX =  width * cellSize / 2;
            float minY = -height * cellSize / 2;
            float maxY = height * cellSize / 2;
            List<Vertex> vertices = new List<Vertex>();
            for (float x = minX; x <= maxX; x+= cellSize)
            {
                vertices.Add( new Vertex(new Vector3(x, 0, minY), Color.White));
                vertices.Add(new Vertex(new Vector3(x, 0, maxY), Color.White));
            }

            for (float y = minY; y <= maxY; y += cellSize)
            {
                vertices.Add(new Vertex(new Vector3(minX, 0, y), Color.White));
                vertices.Add(new Vertex(new Vector3(maxX, 0, y), Color.White));
            }

            Vertices = vertices.ToArray();
            Indices = new int[Vertices.Length];
            for (int i = 0; i < Vertices.Length; i++)
                Indices[i] = i;

            CopyToBuffer();
        }
    }
}
