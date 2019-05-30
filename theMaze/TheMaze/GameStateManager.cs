﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TheMaze
{
    class GameStateManager
    {
        public enum GameState { MainMenu, Play, Pause, Killed, CollectibleMenu, Cutscene }
        public static GameState currentGameState;
        GamePlayManager gamePlayManager;
        MainMenu mainMenu;
        PauseMenu pauseMenu;
        LevelManager levelManager;
        CollectibleViewer collectibleviewer;
        BGM bgm;

        Video video;
        VideoPlayer videoplayer;
        Texture2D videoTexture;
        float videotimer;

        public GameStateManager()
        {
            gamePlayManager = new GamePlayManager();
            mainMenu = new MainMenu();
            pauseMenu = new PauseMenu();
            levelManager = new LevelManager();
            collectibleviewer = new CollectibleViewer();
            bgm = new BGM();

            videoplayer = new VideoPlayer();
            video = TextureManager.Cutscene;

            videoplayer.Play(video);
            videoTexture = videoplayer.GetTexture();
            videoplayer.Stop();
            currentGameState = GameState.MainMenu;
        }

        public void Update(GameTime gameTime)
        {
            Utility.Update(gameTime);
            PauseGame(gameTime);

            switch (currentGameState)
            {
                case GameState.MainMenu:
                    mainMenu.Update();
                    Utility.IsMouseVisible = true;
                    break;
                case GameState.Play:
                    gamePlayManager.Update(gameTime);

                    break;
                case GameState.Pause:
                    pauseMenu.Update();
                    Utility.IsMouseVisible = true;
                    break;

                case GameState.Killed:
                    break;
                case GameState.CollectibleMenu:
                    break;
                case GameState.Cutscene:
                    if (videoplayer.State == MediaState.Stopped)
                    {
                        videoplayer.Play(video);
                    }
                    if (videoplayer.State == MediaState.Playing)
                    {
                        videoTexture = videoplayer.GetTexture();
                    }
                    break;
            }

            if (gamePlayManager.killed == true)
            {
                //currentGameState = GameState.Killed; Console.WriteLine(currentGameState);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    mainMenu.Draw(spriteBatch);
                    break;
                case GameState.Play:
                    gamePlayManager.Draw(spriteBatch, gameTime);
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
                case GameState.Cutscene:
                    DrawCutscene(spriteBatch);
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
                    if (Utility.IsKeyPressed(Keys.P))
                    {
                        //currentGameState = GameState.Pause; Console.WriteLine(currentGameState);
                        pauseMenu.drawControlsMenu = false;
                    }
                    if (Utility.player.viewCollectible)
                    {
                        collectibleviewer.CreateButtons();
                        currentGameState = GameState.CollectibleMenu;
                    }
                    break;
                case GameState.Pause:
                    if (Utility.IsKeyPressed(Keys.P))
                    {
                        //currentGameState = GameState.Play; Console.WriteLine(currentGameState);
                    }
                    break;
                case GameState.Killed:
                    if (Utility.IsKeyPressed(Keys.Space) && gamePlayManager.killed)
                    {
                        gamePlayManager.Resurrect();
                        //currentGameState = GameState.Play; Console.WriteLine(currentGameState);
                    }
                    break;
                case GameState.CollectibleMenu:
                    collectibleviewer.Update();
                    if (Utility.IsKeyPressed(Keys.Space) && collectibleviewer.inMenu)
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

        public void DrawCutscene(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(videoTexture, new Rectangle(0, 0, 1920, 1080), Color.White);
            spriteBatch.End();
        }
    }
}
