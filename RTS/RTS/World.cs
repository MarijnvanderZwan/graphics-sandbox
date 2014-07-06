using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RTS
{
    public class World : DrawableGameComponent
    {
        public Terrain Terrain;
        public Army Army;
        
        public World(Game1 game) : base(game)
        {
            Instance = this;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Terrain = new TestTerrain();

            Army = new TestArmy();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputState.IsKeyPressed(Keys.Tab))
                Terrain.Enabled = !Terrain.Enabled;

            Army.Update();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RasterizerState = new RasterizerState { /*FillMode = FillMode.WireFrame, */CullMode = CullMode.None };
            GraphicsDevice.BlendState = BlendState.Opaque;

            Army.Draw();
            Terrain.Draw();
            base.Draw(gameTime);
        }

        public static World Instance
        {
            get;
            private set;
        }
    }
}
