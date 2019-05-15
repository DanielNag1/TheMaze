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
        protected Vector2 direction;

        public Circle hitbox;

        protected Rectangle currentSourceRect, nextSourceRect;
        public int frameSize;
        protected int frame, nrFrames;
        protected double timer, timeIntervall;

        public float speed;
        protected Color color;

        

        public Monster(Texture2D texture, Vector2 position) : base(texture, position)
        {
            nextSourceRect = currentSourceRect;
            timer = 100;
            timeIntervall = 160;
            currentSourceRect = new Rectangle(0, 0, frameSize, frameSize);
        }

        public virtual void Update(GameTime gameTime)
        {
            Animation(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y,
                ConstantValues.tileWidth, ConstantValues.tileHeight), currentSourceRect, color);

        }


        public void Animation(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer <= 0)
            {
                timer = timeIntervall;
                frame++;
                if (frame >= nrFrames)
                {
                    frame = 0;
                }
                //currentSourceRect.X = frame * frameSize;
            }
            //currentSourceRect.Y = nextSourceRect.Y;

        }







    }
}