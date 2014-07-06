using Microsoft.Xna.Framework;

namespace RTS
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public UserInterface UserInterface;
        public World World;

        public Game1()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
        }
        
        protected override void Initialize()
        {
            InputState.Initialize();

            World = new World(this);
            UserInterface = new UserInterface(this);
            Components.Add(World);
            Components.Add(UserInterface);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            InputState.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        public static Game1 Instance
        {
            get;
            private set;
        }
    }
}
