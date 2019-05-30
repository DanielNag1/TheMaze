using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TheMaze
{
    public class StoryButton
    {
        Texture2D texture;
        Vector2 position,numberposition;
        Rectangle rectangle;
        private SpriteFont spriteFont;
        public int number;
        public StoryButton(Texture2D texture,Vector2 position,int number)
        {
            this.texture = texture;
            this.position = position;
            this.number = number;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            numberposition = new Vector2(position.X + texture.Width /2-10, position.Y + texture.Height / 2);
            spriteFont = TextureManager.TimesNewRomanFont;
        }

        public bool IsClicked()
        {
            bool isClicked = false;

            if (rectangle.Intersects(Utility.menumouseRect))
            {
                if (Utility.mouseState.LeftButton == ButtonState.Pressed && Utility.oldmouseState.LeftButton == ButtonState.Released)
                {
                    isClicked = true;
                }
            }

            return isClicked;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,rectangle,Color.White);
            spriteBatch.DrawString(spriteFont, number.ToString(), numberposition, Color.Black);
        }
    }
}
