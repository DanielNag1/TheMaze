using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheMaze
{
    public class Tile : GameObject
    {
        protected Rectangle sourceRect;
        public Rectangle Hitbox { get; private set; }

        protected bool isWall;
        public bool IsWall
        {
            get { return isWall; }
        }

        public Tile(Texture2D texture, Vector2 position, Rectangle sourceRect, bool isWall) : base(texture, position)
        {
            this.isWall = isWall;
            this.sourceRect = sourceRect;
            Hitbox = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, sourceRect, Color.White);
        }
    }
}
