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
    class Overlay
    {
        public static Vector2 CentereredPlayerPosition;

        public Overlay(Vector2 PlayerPos)
        {
            CentereredPlayerPosition = new Vector2(PlayerPos.X - ConstantValues.screenWidth / 2, PlayerPos.Y - ConstantValues.screenHeight / 2);
        }
            
        public void Update(GameTime gameTime,Vector2 PlayerPos)
        {
            CentereredPlayerPosition = new Vector2(PlayerPos.X - ConstantValues.screenWidth / 2, PlayerPos.Y - ConstantValues.screenHeight / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.LetterboxOverlay, CentereredPlayerPosition, Color.White);
            spriteBatch.Draw(TextureManager.VignetteOverlay, CentereredPlayerPosition, Color.White);
        }

        public void DrawMenuOverlay(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.DarkOverlay, CentereredPlayerPosition, Color.White);
            spriteBatch.Draw(TextureManager.CollectibleMenu, CentereredPlayerPosition, Color.White);

        }
    }
}
