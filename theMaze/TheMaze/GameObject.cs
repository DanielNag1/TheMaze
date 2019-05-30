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
        public Texture2D Texture { get; protected set; }
        protected Vector2 position;

        protected Rectangle currentSourceRect, nextSourceRect;
        public int frameSize;
        protected int frame, nrFrames;
        protected double timer, timeInterval;

        public Vector2 Direction { get; protected set; }

        public Vector2 Position
        {
            get { return position; }
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.position = position;
            currentSourceRect = new Rectangle(frame, frameSize, ConstantValues.tileWidth, ConstantValues.tileHeight);
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        
        public void Animation(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer <= 0)
            {
                timer = timeInterval;
                frame++;
                if (frame >= nrFrames)
                {
                    frame = 0;
                }
            }
        }

        public void UpdateSourceRectangle()
        {
            if (Direction == new Vector2(1, 0))
            {
                frameSize = 1;
            }
            if (Direction == new Vector2(-1, 0))
            {
                frameSize = 0;
            }

            if (Direction == new Vector2(0, 1))
            {
                frameSize = 2;
            }

            if (Direction == new Vector2(0, -1))
            {
                frameSize = 3;
            }
        }
    }
}