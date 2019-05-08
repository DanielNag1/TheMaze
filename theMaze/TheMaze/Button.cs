using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class Button
    {
        public Color color, fontColor;

        private SpriteFont spriteFont;
        private Texture2D tex;
        private Vector2 pos;
        private Rectangle rect;

        private string text;

        public Button(Texture2D tex, Vector2 pos, SpriteFont spriteFont, string text, Color fontColor)
        {
            this.tex = tex;
            this.pos = pos;
            this.spriteFont = spriteFont;
            this.text = text;
            this.rect = new Rectangle((int)pos.X, (int)pos.Y, (int)spriteFont.MeasureString(text).Length(),
                spriteFont.LineSpacing);

            this.fontColor = fontColor;
            color = Color.Red;
        }

        public bool IsMouseHoveringOverButton()
        {
            bool isMouseHovering = false;

            if (rect.Contains(X.mousePos))
            {
                isMouseHovering = true;
            }

            return isMouseHovering;
        }

        public bool IsClicked()
        {
            bool isClicked = false;

            if (rect.Contains(X.mousePos))
            {
                if (X.mouseState.LeftButton == ButtonState.Pressed && X.oldmouseState.LeftButton == ButtonState.Released)
                {
                    isClicked = true;
                }
            }

            return isClicked;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, rect, color);
            spriteBatch.DrawString(spriteFont, text, pos, fontColor);
        }
    }
}
