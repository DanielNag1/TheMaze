using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System;

namespace TheMaze
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
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
            //IsMouseVisible = true;
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadContent(Content);
            
            gameManager = new GamePlayManager();
            penumbra.AmbientColor = Color.Black;
            graphics.PreferredBackBufferWidth = gameManager.PrefWindowSize().X;
            graphics.PreferredBackBufferHeight = gameManager.PrefWindowSize().Y;
            graphics.ApplyChanges();
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameManager.Update(gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            gameManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
