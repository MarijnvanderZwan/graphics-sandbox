using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class GameObject2D : GameObject
    {
        public Vector3 Scale = new Vector3(1, 1, 1);
       
        public GameObject2D()
        {
            Game = Game1.Instance;
            Effect = Game.Content.Load<Effect>("ScreenSpaceEffect");
            Camera = Camera.Instance;
            primitiveType = PrimitiveType.LineStrip;
        }

        public new void Draw()
        {
            var device = Game.GraphicsDevice;
            Effect.Parameters["ScreenWidth"].SetValue(1024f);
            Effect.Parameters["ScreenHeight"].SetValue(768f);
            Effect.Parameters["World"].SetValue(Matrix.Identity * Matrix.CreateScale(Scale) * Matrix.CreateTranslation(Position));
            Effect.CurrentTechnique.Passes[0].Apply();
            device.DrawUserIndexedPrimitives(PrimitiveType.LineStrip, Vertices, 0, Vertices.Length, Indices, 0, IndexCount);
      
        }
    }
}
