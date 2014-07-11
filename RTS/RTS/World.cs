using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RTS
{
    public class World : DrawableGameComponent
    {
        public Terrain Terrain;
        public Map2D Map;
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
            Map = new Map2D(Util.TextureFromFile(Util.TexturePath + @"\maze.png"));
            Army = new SmartArmy();
            //Army = new PhysicsArmy();
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
            Map.Draw();
            //Terrain.Draw();
            base.Draw(gameTime);
        }

        public static World Instance
        {
            get;
            private set;
        }
    }
}
