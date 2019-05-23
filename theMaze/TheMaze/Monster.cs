using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class Monster : GameObject
    {
        public Vector2 destination, hitboxPos;
        

        public Circle hitbox;

        public float speed;
        protected Color color;

        public int monsterDamage;

        public Monster(Texture2D texture, Vector2 position) : base(texture, position)
        {
            nextSourceRect = currentSourceRect;
            timer = 100;
            timeIntervall = 160;
            currentSourceRect = new Rectangle(frame, frameSize, ConstantValues.tileWidth, ConstantValues.tileHeight);
        }

        public virtual void Update(GameTime gameTime)
        {
            Animation(gameTime);
            UpdateSourceRectangle();
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y,
                ConstantValues.tileWidth, ConstantValues.tileHeight), currentSourceRect, color);

        }

        //public void UpdateSourceRectangle()
        //{
        //    if (direction == new Vector2(1, 0))
        //    {
        //        frameSize = 1;
        //    }
        //    if (direction == new Vector2(-1, 0))
        //    {
        //        frameSize = 0;
        //    }

        //    if (direction == new Vector2(0, 1))
        //    {
        //        frameSize = 2;
        //    }

        //    if (direction == new Vector2(0, -1))
        //    {
        //        frameSize = 3;
        //    }
        //}

    }
}