using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class SkyDome
    {
        Texture2D cloudMap;
        Model skyDome;
        GraphicsDevice device;

        public SkyDome()
        {
            device = Game1.Instance.GraphicsDevice;
            skyDome = Game1.Instance.Content.Load<Model>("dome");
            Effect effect = Game1.Instance.Content.Load<Effect> ("Colored");
            skyDome.Meshes[0].MeshParts[0].Effect = effect;
            cloudMap = Game1.Instance.Content.Load<Texture2D>("cloudMap");
        }

        public void Draw()
        {
            var oldState = device.DepthStencilState;
            var newState = new DepthStencilState();
            //newState.DepthBufferWriteEnable = false;
            device.DepthStencilState = newState;
            
            Matrix[] modelTransforms = new Matrix[skyDome.Bones.Count];
            skyDome.CopyAbsoluteBoneTransformsTo(modelTransforms);

            Matrix wMatrix = Matrix.CreateTranslation(0, -0.3f, 0) * Matrix.CreateScale(500) /** Matrix.CreateTranslation(Game1.Instance.Camera.Eye)*/;
            foreach (ModelMesh mesh in skyDome.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    Matrix worldMatrix = modelTransforms[mesh.ParentBone.Index] * wMatrix;
                    currentEffect.CurrentTechnique = currentEffect.Techniques["SkyDome"];
                    currentEffect.Parameters["xWorld"].SetValue(worldMatrix);
                    currentEffect.Parameters["xView"].SetValue(Game1.Instance.userInterface.Camera.ViewMatrix);
                    currentEffect.Parameters["xProjection"].SetValue(Game1.Instance.userInterface.Camera.ProjectionMatrix);
                    currentEffect.Parameters["xTexture"].SetValue(cloudMap);
                    currentEffect.Parameters["xEnableLighting"].SetValue(false);
                }
                mesh.Draw();
            }
            device.DepthStencilState = oldState;
        }
    }
}
