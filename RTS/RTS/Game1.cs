using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RTS
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public UserInterface userInterface;

        SpriteBatch spriteBatch;
        Indicator indicator;
        public Army army;
        
        Terrain terrain;
        RenderTarget2D gameRender;
        RenderTarget2D uiRender;
        //public Camera Camera;
        List<GameObject> gameObjects = new List<GameObject>();

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
            userInterface = new UserInterface(this);
            //Camera = userInterface.Camera;
            Components.Add(userInterface);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Grid grid = new Grid(26, 26, 1);
            terrain = new TestTerrain();   
            indicator = new Indicator();
            army = new TestArmy();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            gameRender = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, true, GraphicsDevice.DisplayMode.Format, 
                                            DepthFormat.Depth24, 0, pp.RenderTargetUsage);
            uiRender   = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, true, GraphicsDevice.DisplayMode.Format,
                                            DepthFormat.Depth24, 0, pp.RenderTargetUsage);

        }

        protected override void Update(GameTime gameTime)
        {
            InputState.Update();

            if (InputState.IsKeyPressed(Keys.Tab))
                terrain.Enabled = !terrain.Enabled;

            army.Update();
          
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RasterizerState = new RasterizerState { /*FillMode = FillMode.WireFrame, */CullMode = CullMode.None};
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;


            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            GraphicsDevice.SetRenderTarget(gameRender);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            foreach (var gameObj in gameObjects)
                gameObj.Draw();
            army.Draw();
            //testModel.Draw();
            terrain.Draw();
            GraphicsDevice.SetRenderTarget(uiRender);
            GraphicsDevice.Clear(Color.Transparent);

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin();
            spriteBatch.Draw(gameRender, GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.Draw(uiRender, GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static Game1 Instance
        {
            get;
            private set;
        }
    }
}
