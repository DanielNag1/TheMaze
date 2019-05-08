using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheMaze
{
    class PauseMenu
    {
        private Button continueButton, exitButton;
        private Vector2 startPos, exitPos;

        public PauseMenu()
        {
            startPos = new Vector2(10, 1000);
            exitPos = new Vector2(10, 1000 + TextureManager.TimesNewRomanFont.LineSpacing);
            continueButton = new Button(TextureManager.TransparentTex, startPos, TextureManager.TimesNewRomanFont,
                "CONTINUE", Color.LightGray);
            exitButton = new Button(TextureManager.TransparentTex, exitPos,
                TextureManager.TimesNewRomanFont, "EXIT GAME", Color.LightGray);
        }

        public void Update()
        {
            ContinueButton();
            ExitButton();
            //knappmetoder kallas här
        }

        //knappmetoder skrivs här och innehåller kollision med musen
        public void ContinueButton()
        {
            if (continueButton.IsMouseHoveringOverButton())
            {
                continueButton.fontColor = Color.White;
            }
            else
            {
                continueButton.fontColor = Color.LightGray;
            }

            if (continueButton.IsClicked())
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
            spriteBatch.Draw(TextureManager.PauseScreen, Vector2.Zero, Color.White);
            continueButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
