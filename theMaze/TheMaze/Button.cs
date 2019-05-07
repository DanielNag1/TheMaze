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
        MouseState mouseState, prevMouseState;

        Texture2D tex;
        Rectangle rect;
        Vector2 pos;

        public Color color;

        public Button(Texture2D tex, Vector2 pos)
        {
            this.tex = tex;
            this.rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            this.pos = pos;

            color = Color.White;
        }

        public bool IsClicked()
        {
            mouseState = Mouse.GetState();
            bool isClicked = false;

            if (rect.Contains(mouseState.Position))
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    isClicked = true;
                }
            }

            prevMouseState = mouseState;
            return isClicked;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, color);
        }
    }
}
