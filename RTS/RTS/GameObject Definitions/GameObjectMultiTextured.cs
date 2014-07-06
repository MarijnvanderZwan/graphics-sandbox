using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class GameObjectMultiTextured : GameObject
    {
        public Texture2D texture;
        public new VertexPositionNormalTexture[] Vertices;


        public GameObjectMultiTextured() : base()
        {
        }

        public new void Draw()
        {
            var device = Game.GraphicsDevice;
            if (!Enabled) return;

            Effect.CurrentTechnique = Effect.Techniques["Textured"];
            Effect.Parameters["xTexture"].SetValue(texture);

            Effect.Parameters["xView"].SetValue(Camera.ViewMatrix);
            Effect.Parameters["xProjection"].SetValue(Camera.ProjectionMatrix);
            Effect.Parameters["xWorld"].SetValue(Matrix.CreateTranslation(Position));
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
