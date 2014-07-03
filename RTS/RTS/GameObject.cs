using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class GameObject
    {
        public Vertex[] Vertices;
        public Effect Effect;
        public Vector3 Position = new Vector3();
        public Game1 Game;
        public PrimitiveType primitiveType = PrimitiveType.TriangleList;

        public int[] Indices;
        public VertexBuffer myVertexBuffer;
        public IndexBuffer myIndexBuffer;

        public bool Enabled = true;

        public GameObject()
        {
            Game = Game1.Instance;
            Effect = Game.Content.Load<Effect>("Colored");
        }

        public void Draw()
        {
            if (!Enabled) return;

            Effect.CurrentTechnique = Effect.Techniques["Colored"];
            Effect.Parameters["xView"].SetValue(Game.userInterface.Camera.ViewMatrix);
            Effect.Parameters["xProjection"].SetValue(Game.userInterface.Camera.ProjectionMatrix);
            Effect.Parameters["xWorld"].SetValue(Matrix.CreateTranslation(Position));
            Vector3 lightDirection = new Vector3(1.0f, -1.0f, -1.0f);
            lightDirection.Normalize();
            Effect.Parameters["xLightDirection"].SetValue(lightDirection);
            Effect.Parameters["xAmbient"].SetValue(0.1f);
            Effect.Parameters["xEnableLighting"].SetValue(true);

            foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
                pass.Apply();

            Game.GraphicsDevice.Indices = myIndexBuffer;
            Game.GraphicsDevice.SetVertexBuffer(myVertexBuffer);

            Game.GraphicsDevice.DrawIndexedPrimitives(primitiveType, 0, 0, Vertices.Length, 0, IndexCount);  
        }

        public void CopyToBuffer()
        {
            myVertexBuffer = new VertexBuffer(Game.GraphicsDevice, Vertex.VertexDeclaration, Vertices.Length, BufferUsage.WriteOnly);
            myVertexBuffer.SetData(Vertices);

            myIndexBuffer = new IndexBuffer(Game.GraphicsDevice, typeof(int), Indices.Length, BufferUsage.WriteOnly);
            myIndexBuffer.SetData(Indices);
        }

        public int IndexCount
        {
            get
            {
                if (primitiveType == PrimitiveType.TriangleList) return Indices.Length / 3;
                if (primitiveType == PrimitiveType.LineList) return Indices.Length / 2;
                if (primitiveType == PrimitiveType.LineStrip) return Indices.Length - 1;
                else return Indices.Length / 3;
            }
        }

    }
}
