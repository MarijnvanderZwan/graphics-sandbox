using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    public class SelectionRectangle
    {
        Vertex[] vertices = new Vertex[4];
        int[] indices = { 0, 1, 2, 3, 0 };
        Effect effect;
        public Vector3 Position = new Vector3(0, 0, 0);
        public Vector3 Scale = new Vector3(1, 1, 1);
        GraphicsDevice device;

        public SelectionRectangle()
        {
            device = Game1.Instance.GraphicsDevice;
            vertices[0] = new Vertex(new Vector3(0, 0, 0), Color.White);
            vertices[1] = new Vertex(new Vector3(1, 0, 0), Color.White);
            vertices[2] = new Vertex(new Vector3(1, 1, 0), Color.White);
            vertices[3] = new Vertex(new Vector3(0, 1, 0), Color.White);

            effect = Game1.Instance.Content.Load<Effect>("ScreenSpaceEffect");
        }

        public void Draw()
        {
            effect.Parameters["ScreenWidth"].SetValue(1024f);
            effect.Parameters["ScreenHeight"].SetValue(768f);
            effect.Parameters["World"].SetValue(Matrix.Identity * Matrix.CreateScale(Scale) * Matrix.CreateTranslation(Position));
            effect.CurrentTechnique.Passes[0].Apply();
            device.DrawUserIndexedPrimitives(PrimitiveType.LineStrip, vertices, 0, vertices.Length, indices, 0, indices.Length - 1);
        }
    }
}
