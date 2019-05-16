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
        public enum GameState { MainMenu, Play, Pause,Killed,CollectibleMenu}
        public static GameState currentGameState = GameState.Play;
        GamePlayManager gamePlayManager;
        MainMenu mainMenu;
        PauseMenu pauseMenu;
        LevelManager levelManager;
        CollectibleViewer collectibleviewer;
        BGM bgm;

        public GameStateManager()
        {
            gamePlayManager = new GamePlayManager();
            mainMenu = new MainMenu();
            pauseMenu = new PauseMenu();
            levelManager = new LevelManager();
            collectibleviewer = new CollectibleViewer();
            bgm = new BGM();
        }

        public void Update(GameTime gameTime)
        {
            X.Update(gameTime);
            PauseGame(gameTime);
            bgm.PlayBGM();
            
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
                case GameState.CollectibleMenu:
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
                case GameState.CollectibleMenu:
                    collectibleviewer.Draw(spriteBatch);
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
                        pauseMenu.drawControlsMenu = false;
                    }
                    if (X.player.viewCollectible)
                    {
                        collectibleviewer.CreateButtons();
                        currentGameState = GameState.CollectibleMenu;
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
                        
                    }
                    break;
                case GameState.CollectibleMenu:
                    collectibleviewer.Update();
                    if (X.IsKeyPressed(Keys.Space) && collectibleviewer.inMenu)
                    {
                        currentGameState = GameState.Play;
                        //X.player.viewCollectible = false;
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
