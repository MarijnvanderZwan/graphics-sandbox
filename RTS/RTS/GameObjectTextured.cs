using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class GameObjectTextured : GameObject
    {
        public Texture2D texture;
        public new VertexPositionNormalTexture[] Vertices;
        public Transformation Transformation;

        public GameObjectTextured() : base()
        {
            Transformation = new Transformation();
        }

        public new void Draw()
        {
            if (!Enabled) return;

            Effect.CurrentTechnique = Effect.Techniques["Textured"];
            Effect.Parameters["xTexture"].SetValue(texture);

            Effect.Parameters["xView"].SetValue(Game.userInterface.Camera.ViewMatrix);
            Effect.Parameters["xProjection"].SetValue(Game.userInterface.Camera.ProjectionMatrix);
            Effect.Parameters["xWorld"].SetValue(Transformation.TransformationMatrix);
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

        public new void CopyToBuffer()
        {
            myVertexBuffer = new VertexBuffer(Game.GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, Vertices.Length, BufferUsage.WriteOnly);
            myVertexBuffer.SetData(Vertices);

            myIndexBuffer = new IndexBuffer(Game.GraphicsDevice, typeof(int), Indices.Length, BufferUsage.WriteOnly);
            myIndexBuffer.SetData(Indices);
        }
    }
}
