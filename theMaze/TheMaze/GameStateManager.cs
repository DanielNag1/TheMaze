using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheMaze
{
    class GameStateManager
    {
        enum GameState { Play,Pause}
        GameState currentGameState=GameState.Pause;
        GamePlayManager gamePlayManager;

        public GameStateManager()
        {
            gamePlayManager = new GamePlayManager();

        }

        public void Update(GameTime gameTime)
        {
            X.Update(gameTime);
            PauseGame(gameTime);

            switch(currentGameState)
            {
                case GameState.Play:
                    gamePlayManager.Update(gameTime);

                    break;
                case GameState.Pause:

                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.Play:
                    gamePlayManager.Draw(spriteBatch,gameTime);
                    break;
                case GameState.Pause:
                    DrawPauseScreen(spriteBatch);
                    break;
            }
            
        }

        public void PauseGame(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.Play:
                    if (X.IsKeyPressed)
                    {
                        currentGameState = GameState.Pause; Console.WriteLine(currentGameState);
                    }
                    break;

                case GameState.Pause:
                    if (X.IsKeyPressed)
                    {
                        currentGameState = GameState.Play; Console.WriteLine(currentGameState);
                    }
                    break;
            }
        }

        public void DrawPauseScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(TextureManager.PauseScreen, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
