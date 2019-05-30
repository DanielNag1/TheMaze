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
    public class Button
    {
        public Color color, fontColor;

        private SpriteFont spriteFont;
        private Texture2D tex;
        public Vector2 pos;
        public Rectangle rect;

        public string text;

        public Button(Texture2D tex, Vector2 pos, SpriteFont spriteFont, string text, Color fontColor)
        {
            this.tex = tex;
            this.pos = pos;
            this.spriteFont = spriteFont;
            this.text = text;
            this.rect = new Rectangle((int)pos.X, (int)pos.Y, (int)spriteFont.MeasureString(text).Length(), spriteFont.LineSpacing);

            this.fontColor = fontColor;
            color = Color.White;
        }

        public bool IsMouseHoveringOverButton()
        {
            bool isMouseHovering = false;

            if (rect.Contains(Utility.mousePos))
            {
                isMouseHovering = true;
            }

            return isMouseHovering;
        }

        public bool IsClicked()
        {
            bool isClicked = false;

            if (rect.Contains(Utility.mousePos))
            {
                if (Utility.mouseState.LeftButton == ButtonState.Pressed && Utility.oldmouseState.LeftButton == ButtonState.Released)
                {
                    isClicked = true;
                }
            }

            return isClicked;
        }

        public void HighlightButtonText()
        {
            if (IsMouseHoveringOverButton())
            {
                fontColor = Color.White;
            }
            else
            {
                fontColor = Color.LightGray;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, rect, color);
            spriteBatch.DrawString(spriteFont, text, pos, fontColor);
        }
    }
}
