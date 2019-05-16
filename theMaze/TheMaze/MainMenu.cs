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
        private Button startButton, controlsButton, exitButton, creditsButton;
        private Vector2 startPos, controlsPos, exitPos, creditsPos, creditsRoll;

        private bool drawControlsMenu, showCredits;

        private float creditsTimer;

        public MainMenu()
        {
            startPos = new Vector2(10, 1000 - TextureManager.TimesNewRomanFont.LineSpacing * 2);
            controlsPos = new Vector2(10, 1000 - TextureManager.TimesNewRomanFont.LineSpacing);
            creditsPos = new Vector2(10, 1000);
            exitPos = new Vector2(10, 1000 + TextureManager.TimesNewRomanFont.LineSpacing);
            startButton = new Button(TextureManager.TransparentTex, startPos, TextureManager.TimesNewRomanFont,
                "START GAME", Color.LightGray);
            controlsButton = new Button(TextureManager.TransparentTex, controlsPos, TextureManager.TimesNewRomanFont,
                "CONTROLS", Color.LightGray);
            exitButton = new Button(TextureManager.TransparentTex, exitPos,
                TextureManager.TimesNewRomanFont, "EXIT GAME", Color.LightGray);
            creditsButton = new Button(TextureManager.TransparentTex, creditsPos, TextureManager.TimesNewRomanFont,
                "CREDITS", Color.LightGray);

            drawControlsMenu = false;
            showCredits = false;

            creditsRoll = new Vector2(0, 0);
            creditsTimer = 120f;
        }

        public void Update()
        {
            if (!showCredits)
            {
                if (!drawControlsMenu)
                {
                    StartButton();
                    ControlsButton();
                    CreditsButton();
                    ExitButton();
                }
                else
                {
                    ControlsButton();
                }
            }
            else
            {
                CreditsButton();
            }

            RollCredits();
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
                controlsButton.pos = exitPos;
                controlsButton.rect.X = (int)exitPos.X;
                controlsButton.rect.Y = (int)exitPos.Y;
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

        public void CreditsButton()
        {
            creditsButton.HighlightButtonText();

            if (creditsButton.IsClicked())
            {
                if (!showCredits) showCredits = true;
                else              showCredits = false;
            }

            if (showCredits)
            {
                creditsButton.text = "BACK";
                creditsButton.pos = exitPos;
                creditsButton.rect.X = (int)exitPos.X;
                creditsButton.rect.Y = (int)exitPos.Y;
            }
            else
            {
                creditsButton.text = "CREDITS";
                creditsButton.pos = creditsPos;
                creditsButton.rect.X = (int)creditsPos.X;
                creditsButton.rect.Y = (int)creditsPos.Y;
            }
        }

        public void RollCredits()
        {
            if (showCredits)
            {
                creditsTimer -= 1f;
                if (creditsTimer <= 0)
                {
                    creditsRoll.Y -= 1;
                }
            }
            else
            {
                creditsRoll = new Vector2(0, 0);
                creditsTimer = 120f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            if (!showCredits)
            {
                spriteBatch.Draw(TextureManager.MainMenuTex, Vector2.Zero, Color.White);
                startButton.Draw(spriteBatch);
                exitButton.Draw(spriteBatch);
                creditsButton.Draw(spriteBatch);

                if (drawControlsMenu)
                {
                    spriteBatch.Draw(TextureManager.ControlsMenuTex, Vector2.Zero, Color.White);
                }

                controlsButton.Draw(spriteBatch);
            }
            else
            {
                spriteBatch.Draw(TextureManager.CreditsTex, creditsRoll, Color.White);
                creditsButton.Draw(spriteBatch);
            }


            spriteBatch.End();
        }
    }
}
