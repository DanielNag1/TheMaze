using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Penumbra;

namespace TheMaze
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        GameStateManager gameStateManager;
        public static PenumbraComponent penumbra;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            penumbra = new PenumbraComponent(this);
            Components.Add(penumbra);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = ConstantValues.screenWidth;
            graphics.PreferredBackBufferHeight = ConstantValues.screenHeight;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SoundManager.LoadContent(Content);
            TextureManager.LoadContent(Content);
            gameStateManager = new GameStateManager();
            penumbra.AmbientColor = Color.Black;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            ExitGame();
            MouseCheck();
            gameStateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            gameStateManager.Draw(spriteBatch,gameTime);
            
        }

        public void ExitGame()
        {
            if (X.Exit || (X.keyboardState.IsKeyDown(Keys.Escape) && X.oldkeyboardState.IsKeyUp(Keys.Escape)))
            {
                Exit();
            }
            
        }

        public void MouseCheck()
        {
            if(X.IsMouseVisible==true)
            {
                IsMouseVisible = true;
            }
            else
            {
                IsMouseVisible = false;
            }
        }
    }
}
