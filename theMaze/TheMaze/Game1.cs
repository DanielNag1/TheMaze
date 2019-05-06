using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System;

namespace TheMaze
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        GamePlayManager gameManager;
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
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadContent(Content);
            
            gameManager = new GamePlayManager(GraphicsDevice);
            penumbra.AmbientColor = Color.Black;
        }

        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(gameManager.isMouseVisible)
            {
                IsMouseVisible = true;
            }
            else
            {
                IsMouseVisible = false;
            }
            gameManager.Update(gameTime);
            
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            gameManager.Draw(spriteBatch,gameTime);

            //base.Draw(gameTime);
        }
    }
}
