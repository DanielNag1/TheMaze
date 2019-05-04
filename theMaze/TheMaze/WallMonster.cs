using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class WallMonster
    {
        public Texture2D texture;
        public Color color;
        public Rectangle destinationRectangle;

        public WallMonster(Texture2D texture)
        {
            this.texture = texture;
            color = Color.White;
            
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,destinationRectangle, color);
        }


    }
}
