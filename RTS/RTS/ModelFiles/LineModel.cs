using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RTS
{
    public class LineModel : GameObject
    {
        public LineModel(List<Vector3> lines, Vector3 start) : base()
        {
            primitiveType = PrimitiveType.LineStrip;
            Vertices = new Vertex[lines.Count + 1];
            Indices = new int[lines.Count + 1];
            Vertices[0] = new Vertex(start, Color.White);
            Indices[0] = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                Vertices[i + 1] = new Vertex(lines[i], Color.White);
                Indices[i + 1] = i + 1;
            }


            CopyToBuffer();
                  
        }
    }
}
