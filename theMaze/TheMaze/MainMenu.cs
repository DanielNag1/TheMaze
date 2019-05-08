using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheMaze
{
    class MainMenu
    {
        private Button startButton, exitButton;
        private Vector2 startPos, exitPos;

        public MainMenu()
        {
            startPos = new Vector2(10, 1000);
            exitPos = new Vector2(10, 1000 + TextureManager.TimesNewRomanFont.LineSpacing);
            startButton = new Button(TextureManager.TransparentTex, startPos, TextureManager.TimesNewRomanFont,
                "START GAME", Color.LightGray);
            exitButton = new Button(TextureManager.TransparentTex, exitPos,
                TextureManager.TimesNewRomanFont, "EXIT GAME", Color.LightGray);
        }

        public void Update()
        {
            StartButton();
            ExitButton();
            //knappmetoder kallas här
        }

        //knappmetoder skrivs här och innehåller kollision med musen
        public void StartButton()
        {
            if (startButton.IsMouseHoveringOverButton())
            {
                startButton.fontColor = Color.White;
            }
            else
            {
                startButton.fontColor = Color.LightGray;
            }

            if (startButton.IsClicked())
            {
                GameStateManager.currentGameState = GameStateManager.GameState.Play;
            }
        }

        public void ExitButton()
        {
            if (exitButton.IsMouseHoveringOverButton())
            {
                exitButton.fontColor = Color.White;
            }
            else
            {
                exitButton.fontColor = Color.LightGray;
            }

            if (exitButton.IsClicked())
            {
                X.Exit = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //knappar ritas ut här
            spriteBatch.Begin();
            spriteBatch.Draw(TextureManager.MainMenuTex, Vector2.Zero, Color.White);
            startButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
