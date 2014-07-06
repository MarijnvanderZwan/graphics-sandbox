using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RTS
{
    public class SelectionRectangle : GameObject2D
    {
        public SelectionRectangle() 
        {
            Vertices = new Vertex[4];
            Vertices[0] = new Vertex(new Vector3(0, 0, 0), Color.White);
            Vertices[1] = new Vertex(new Vector3(1, 0, 0), Color.White);
            Vertices[2] = new Vertex(new Vector3(1, 1, 0), Color.White);
            Vertices[3] = new Vertex(new Vector3(0, 1, 0), Color.White);
            Indices = new int[5]{ 0, 1, 2, 3, 0 };
            CopyToBuffer();
        }
    }
}
