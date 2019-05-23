using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public abstract class GameObject
    {
        public Texture2D texture { get; protected set; }
        protected Vector2 position;

        protected Rectangle currentSourceRect, nextSourceRect;
        public int frameSize;
        protected int frame, nrFrames;
        protected double timer, timeIntervall;

        protected Vector2 direction;

        public Vector2 Position
        {
            get { return position; }
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            currentSourceRect = new Rectangle(frame, frameSize, ConstantValues.tileWidth, ConstantValues.tileHeight);
        }

        public abstract void Draw(SpriteBatch spriteBatch);


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

            }

        }

        public void UpdateSourceRectangle()
        {
            if (direction == new Vector2(1, 0))
            {
                frameSize = 1;
            }
            if (direction == new Vector2(-1, 0))
            {
                frameSize = 0;
            }

            if (direction == new Vector2(0, 1))
            {
                frameSize = 2;
            }

            if (direction == new Vector2(0, -1))
            {
                frameSize = 3;
            }
        }
    }
}