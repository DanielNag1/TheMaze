using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileTesting
{
    public class Fish
    {
        Vector2 position;

        public Fish(Vector2 position)
        {
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.FishTex, new Rectangle((int)position.X, (int)position.Y, ConstantValues.TILE_WIDTH, ConstantValues.TILE_HEIGHT), Color.White);
        }
    }
}
