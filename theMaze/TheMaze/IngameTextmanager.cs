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
    public static class IngameTextmanager
    {
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
            spriteBatch.DrawString(TextureManager.TutorialFont, "Kill yourself with 'ENTER'.", new Vector2(X.player.Position.X, X.player.Position.Y - 25), Color.White);
        }
        public static void DrawReturn(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.TutorialFont, "Respawn with 'ENTER'.", new Vector2(X.player.Position.X, X.player.Position.Y - 25), Color.White);
        }
    }
}
