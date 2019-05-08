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
        public enum GameState { MainMenu, Play, Pause,Killed}
        public static GameState currentGameState = GameState.MainMenu;
        GamePlayManager gamePlayManager;
        MainMenu mainMenu;
        PauseMenu pauseMenu;

        public GameStateManager()
        {
            gamePlayManager = new GamePlayManager();
            mainMenu = new MainMenu();
            pauseMenu = new PauseMenu();
        }

        public void Update(GameTime gameTime)
        {
            X.Update(gameTime);
            PauseGame(gameTime);

            switch(currentGameState)
            {
                case GameState.MainMenu:
                    mainMenu.Update();
                    X.IsMouseVisible = true;
                    break;

                case GameState.Play:
                    gamePlayManager.Update(gameTime);

                    break;
                case GameState.Pause:
                    pauseMenu.Update();
                    X.IsMouseVisible = true;
                    break;

                case GameState.Killed:
                    break;
            }

            if (gamePlayManager.killed == true)
            {
                currentGameState = GameState.Killed; Console.WriteLine(currentGameState);
            }

        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    mainMenu.Draw(spriteBatch);
                    break;
                case GameState.Play:
                    gamePlayManager.Draw(spriteBatch,gameTime);
                    break;
                case GameState.Pause:
                    pauseMenu.Draw(spriteBatch);
                    break;
                case GameState.Killed:
                    DrawKilledScreen(spriteBatch);
                    break;
            }
            
        }

        public void PauseGame(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.MainMenu:

                    break;
                case GameState.Play:
                    if (X.IsKeyPressed(Keys.P))
                    {
                        currentGameState = GameState.Pause; Console.WriteLine(currentGameState);
                    }
                    break;
                case GameState.Pause:
                    if (X.IsKeyPressed(Keys.P))
                    {
                        currentGameState = GameState.Play; Console.WriteLine(currentGameState);
                    }
                    break;
                case GameState.Killed:
                    if (X.IsKeyPressed(Keys.Space) && gamePlayManager.killed)
                    {
                        gamePlayManager.Resurrect();
                        currentGameState = GameState.Play; Console.WriteLine(currentGameState);
                        gamePlayManager.killed = false;
                    }
                    break;
            }
        }
        public void DrawKilledScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(TextureManager.KilledScreen, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

    }
}
