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
            textTimer = 3000f;
        }

        public void CheckProgression(GameTime gameTime)
        {

            switch (currentStage)
            {
                case Textstage.Q:
                    text = "....'SPACE'...Press...'SPACE'...";
                    if (X.IsKeyPressed(Keys.Space))
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
                    text = "I help you if you help me. Deal? Press 'SPACE' !!!";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.U;
                    break;
                case Textstage.U:
                    text = "Let's do this...Click on the desk in the saferoom.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.I;
                    break;
                case Textstage.I:
                    text = "Hm...I remember her. We have to get more pieces to get out of here.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.O;
                    break;
                case Textstage.O:
                    text = "I know we have this screen between us but...";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.P;
                    break;
                case Textstage.P:
                    text = "We are in this together.";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.Å;
                    break;
                case Textstage.Å:

                    textTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (textTimer >= 0)
                    {
                        text = "You have a bigger part than you think...";
                    }
                    else
                    {
                        text = " ";
                    }
                    if (X.player.collectibles.Count == 3)
                        currentStage = Textstage.A;
                    break;
                case Textstage.A:
                    text = "Another piece! We should go back to the saferoom and read it!";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.S;
                    break;
                case Textstage.S:
                    if (X.player.collectibles.Count == 4)
                    {
                        text = "This is going well, don't you think?";
                        if (X.IsKeyPressed(Keys.Space))
                            currentStage = Textstage.D;
                    }
                    else
                    {
                        text = " ";
                    }
                    break;
                case Textstage.D:
                    text = "We'll be out of here in no time!";

                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.F;
                    break;
                case Textstage.F:
                    text = " ";

                    if (X.player.collectibles.Count == 5)
                    {
                        currentStage = Textstage.G;
                    }

                    break;
                case Textstage.G:
                    text = "Hurry before the monsters come! Press 'ENTER' !!!";
                    if (X.IsKeyPressed(Keys.Space) || X.IsKeyPressed(Keys.Enter))
                        text = " ";
                    break;
                case Textstage.H:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.J;
                    break;
                case Textstage.J:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.K;
                    break;
                case Textstage.K:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.L;
                    break;
                case Textstage.L:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.Ö;
                    break;
                case Textstage.Ö:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.Ä;
                    break;
                case Textstage.Ä:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.Z;
                    break;
                case Textstage.Z:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.X;
                    break;
                case Textstage.X:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.C;
                    break;
                case Textstage.C:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.V;
                    break;
                case Textstage.V:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.B;
                    break;
                case Textstage.B:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.N;
                    break;
                case Textstage.N:
                    text = "FOUR";
                    if (X.IsKeyPressed(Keys.Space))
                        currentStage = Textstage.M;
                    break;
                case Textstage.M:
                    text = "FOUR";
                    break;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.TutorialFont, text, new Vector2(X.player.Position.X, X.player.Position.Y - 25), Color.White);

        }
        public static void DrawMovingTutorial(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.TutorialFont, "Move with the 'W,A,S,D' buttons.", new Vector2(X.player.Position.X, X.player.Position.Y - 25), Color.White);
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
