using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class GameObjectTextured : GameObject
    {
        public Texture2D Texture;
        public new VertexPositionNormalTexture[] Vertices;
        public Transformation Transformation;

        public GameObjectTextured() : base()
        {
            Transformation = new Transformation();
        }

        public new void Draw()
        {
            if (!Enabled) return;

            var device = Game.GraphicsDevice;
            Effect.CurrentTechnique = Effect.Techniques["Textured"];
            Effect.Parameters["xTexture"].SetValue(Texture);

            Effect.Parameters["xView"].SetValue(Camera.ViewMatrix);
            Effect.Parameters["xProjection"].SetValue(Camera.ProjectionMatrix);
            Effect.Parameters["xWorld"].SetValue(Transformation.TransformationMatrix);
            Vector3 lightDirection = new Vector3(1.0f, -1.0f, -1.0f);
            lightDirection.Normalize();
            Effect.Parameters["xLightDirection"].SetValue(lightDirection);
            Effect.Parameters["xAmbient"].SetValue(0.1f);
            Effect.Parameters["xEnableLighting"].SetValue(true);

            foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
                pass.Apply();

            device.Indices = myIndexBuffer;
            device.SetVertexBuffer(myVertexBuffer);

            device.DrawIndexedPrimitives(primitiveType, 0, 0, Vertices.Length, 0, IndexCount);  
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
