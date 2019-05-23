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
    public class IngameTextmanager
    {
        public enum Textstage
        {
            Q, W, E, R, T, Y, U, I, O, P, Å, A, S, D, F, G, H, J, K, L, Ö, Ä, Z, X, C, V, B, N, M,
            QW, QE, QR, QT, QY, QU, QI, QO, QP, QÅ, QA, QS, QD, QF, QG, QH, QJ, QK, QL
        }
        public Textstage currentStage;
        private string text;
        private float textTimer;

        public IngameTextmanager()
        {
            text = " ";
            textTimer = 7000f;
        }

        public void CheckProgression(GameTime gameTime)
        {

            switch (currentStage)
            {
                case Textstage.Q:
                    text = "....'SPACE'...Press...'SPACE'...";
                    if (X.IsKeyPressed(Keys.Space) && GamePlayManager.currentLevel == GamePlayManager.Level.Level2)
                        currentStage = Textstage.W;
                    break;
                case Textstage.W:
                    text = "....You can read this right? Press 'SPACE' if you can.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.E;
                    break;
                case Textstage.E:
                    text = "Ahh, thank god. I thought I was completely alone.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.R;
                    break;
                case Textstage.R:
                    text = "This..stupid body won't move. But it seems like you have control over it.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.T;
                    break;
                case Textstage.T:
                    text = "I have my reasons to get out..and I'm sure you have too.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.Y;
                    break;
                case Textstage.Y:
                    text = "You can always commit suicide to teleport back here.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.U;
                    break;
                case Textstage.U:
                    text = "Let's do this...Click on the desk in the saferoom.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.I;
                    break;
                case Textstage.I:
                    text = "We have to get more pieces to get out of here.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.O;
                    break;
                case Textstage.O:
                    text = "You see that light on the wall? That will be our weapon.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.P;
                    break;
                case Textstage.P:
                    text = "Move the mouse over it and click to select the color.";
                    if (X.mouseState.LeftButton == ButtonState.Pressed)
                        currentStage = Textstage.Å;
                    break;
                case Textstage.Å:
                    text = "You can press a weapon slot between [2,3,4] and equip that weapon to it.";
                    if (X.player.playerPointLight.Enabled == true)
                    {
                        currentStage = Textstage.A;
                    }
                    break;
                case Textstage.A:
                    text = " I know we have this screen between us but...";

                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.S;
                    break;
                case Textstage.S:
                    if (textTimer >= 0)
                    {
                        textTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }

                    if (textTimer >= 6000)
                    {
                        text = "We are in this together.";
                    }

                    if (textTimer <= 3000)
                    {
                        text = "You have a bigger part than you think...";

                    }
                    
                    if (textTimer <= 0)
                    {
                        currentStage = Textstage.D;
                    }

                    break;
                case Textstage.D:

                    if (X.player.collectibles.Count == 2)
                    {
                        text = "Another piece! We should go back to the saferoom and read it!";
                    }
                    else
                    {
                        text = " ";
                    }

                    if (X.player.collectibles.Count == 2 && X.IsKeyPressed(Keys.Space) || X.IsKeyPressed(Keys.Enter))
                        currentStage = Textstage.F;
                    break;
                case Textstage.F:

                    if (X.player.collectibles.Count == 3)
                    {
                        text = "This is going well, don't you think?";
                    }
                    else
                    {
                        text = " ";
                    }

                    if (X.IsKeyPressed(Keys.Space) && X.player.collectibles.Count == 3)
                        currentStage = Textstage.H;
                    break;
                case Textstage.H:
                    if (X.player.insaferoom)
                    {
                        text = "Look! A new weapon! Go get it!";
                    }
                    else
                    {
                        text = " ";
                    }
                    
                    if (X.mouseState.LeftButton == ButtonState.Pressed)
                        currentStage = Textstage.J;
                    break;
                case Textstage.J:
                    if (!X.player.insaferoom)
                    {
                        text = "You are a kind person. I can feel it.";
                    }
                    else
                    {
                        text = " ";
                    }

                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.K;
                    break;
                case Textstage.K:
                    if (X.player.collectibles.Count == 5)
                    {
                        text = "Hm...this one is heavy. Go read it!";
                    }
                    else
                    {
                        text = " ";
                    }
                    if (X.IsKeyPressed(Keys.Space) && (X.player.collectibles.Count == 5))
                        currentStage = Textstage.L;
                    break;
                case Textstage.L:
                    if (X.player.collectibles.Count == 6)
                    {
                        text = "What a tragedy...";
                    }
                    else
                    {
                        text = " ";
                    }
                    if (X.IsKeyPressed(Keys.Space) && (X.player.collectibles.Count == 6))
                        currentStage = Textstage.Ö;
                    break;
                case Textstage.Ö:
                    if (X.player.collectibles.Count == 7)
                    {
                        text = "She was the only thing...";
                    }
                    else
                    {
                        text = " ";
                    }
                    if (X.IsKeyPressed(Keys.Space) && (X.player.collectibles.Count == 7))
                        currentStage = Textstage.Ä;
                    break;
                case Textstage.Ä:
                    text = "...and he lost it.";

                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.Z;
                    break;
                case Textstage.Z:
                    if (X.player.collectibles.Count == 8)
                    {
                        text = "We are so close...I am so close...";
                    }
                    else
                    {
                        text = " ";
                    }

                    if (X.IsKeyPressed(Keys.Space) && X.player.collectibles.Count == 8)
                        currentStage = Textstage.X;
                    break;
                case Textstage.X:
                    if (X.player.collectibles.Count == 9)
                    {
                        text = "This is the last one!!!";
                    }
                    else
                    {
                        text = " ";
                    }
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.C;
                    break;
                case Textstage.C:
                    text = "You should go back to the saferoom to read it.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.V;
                    break;
                case Textstage.V:
                    if (X.player.insaferoom)
                    {
                        text = "Read it and let's get out of here !!!";
                    }
                    else
                    {
                        text = " ";
                    }
                    if (X.IsKeyPressed(Keys.Enter))
                        currentStage = Textstage.B;
                    break;
                case Textstage.B:
                    textTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (textTimer >= 100 && textTimer <=3000)
                    {
                        text = "You did good! Let's go to the light!";
                    }
                    if (textTimer >= 3000 && textTimer <= 5000)
                    {
                        text = "I will tell you one last thing...";
                    }
                    if (textTimer > 5000)
                    {
                        text = "You are no hero in this story!";
                    }
                    if (textTimer > 7000)
                    {
                        currentStage = Textstage.N;
                    }
                    break;
                case Textstage.N:
                    text = " ";
                    break;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.TutorialFont, text, new Vector2(X.player.Position.X, X.player.Position.Y - 25), Color.White);

        }
        public static void DrawMovingTutorial(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.TutorialFont, "Move with the 'W,A,S,D' buttons. Sprint with 'SHIFT'.", new Vector2(X.player.Position.X, X.player.Position.Y - 25), Color.White);
        }
        public static void DrawPickUpTutorial(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.TutorialFont, "Press 'F' to pick up this item.", new Vector2(X.player.Position.X, X.player.Position.Y - 25), Color.White);
        }
        public static void DrawLampOn(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.TutorialFont, "Turn on your lantern with 'Q'.", new Vector2(X.player.Position.X, X.player.Position.Y - 25), Color.White);
        }
        public static void DrawLampOff(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.TutorialFont, "Turn off your lantern with 'E'.", new Vector2(X.player.Position.X, X.player.Position.Y - 25), Color.White);
        }
        public static void DrawProgress(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.TutorialFont, "Press 'ENTER' to commit suicide.", new Vector2(X.player.Position.X, X.player.Position.Y - 25), Color.White);
        }
        public static void DrawReturn(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.TutorialFont, "Go to the LIGHT.", new Vector2(X.player.Position.X, X.player.Position.Y - 25), Color.White);
        }
    }
}
