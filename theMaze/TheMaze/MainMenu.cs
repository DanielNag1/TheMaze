using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheMaze
{
    class MainMenu
    {
        private Button startButton, controlsButton, exitButton;
        private Vector2 startPos, controlsPos, exitPos;

        private bool drawControlsMenu;

        public MainMenu()
        {
            startPos = new Vector2(10, 1000 - TextureManager.TimesNewRomanFont.LineSpacing);
            controlsPos = new Vector2(10, 1000);
            exitPos = new Vector2(10, 1000 + TextureManager.TimesNewRomanFont.LineSpacing);
            startButton = new Button(TextureManager.TransparentTex, startPos, TextureManager.TimesNewRomanFont,
                "START GAME", Color.LightGray);
            controlsButton = new Button(TextureManager.TransparentTex, controlsPos, TextureManager.TimesNewRomanFont,
                "CONTROLS", Color.LightGray);
            exitButton = new Button(TextureManager.TransparentTex, exitPos,
                TextureManager.TimesNewRomanFont, "EXIT GAME", Color.LightGray);

            drawControlsMenu = false;
        }

        public void Update()
        {
            if (!drawControlsMenu)
            {
                StartButton();
                ExitButton();
            }

            ControlsButton();
        }

        public void StartButton()
        {
            startButton.HighlightButtonText();

            if (startButton.IsClicked())
            {
                GameStateManager.currentGameState = GameStateManager.GameState.Play;
            }
        }

        public void ControlsButton()
        {
            controlsButton.HighlightButtonText();

            if (controlsButton.IsClicked())
            {
                if (!drawControlsMenu) drawControlsMenu = true;
                else drawControlsMenu = false;
            }

            if (drawControlsMenu)
            {
                controlsButton.text = "BACK";
                controlsButton.pos = Vector2.Zero;
                controlsButton.rect.X = (int)Vector2.Zero.X;
                controlsButton.rect.Y = (int)Vector2.Zero.Y;
            }
            else
            {
                controlsButton.text = "CONTROLS";
                controlsButton.pos = controlsPos;
                controlsButton.rect.X = (int)controlsPos.X;
                controlsButton.rect.Y = (int)controlsPos.Y;
            }
        }

        public void ExitButton()
        {
            exitButton.HighlightButtonText();

            if (exitButton.IsClicked())
            {
                X.Exit = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.MainMenuTex, Vector2.Zero, Color.White);
            startButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);

            if (drawControlsMenu)
            {
                spriteBatch.Draw(TextureManager.ControlsMenuTex, Vector2.Zero, Color.White);
            }

            controlsButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
