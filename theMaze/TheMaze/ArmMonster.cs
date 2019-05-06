using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class ArmMonster : GameObject
    {
        TileManager tileManager;

        private Random random;
        public Circle hitboxSpawn, hitboxGrab;
        public bool activated = false;
        public Rectangle armPosition;
        public int frameSize;
        public Vector2 hitboxSpawnPos;
        public bool cooldown = false;
        private Stopwatch timer = new Stopwatch();

        protected Rectangle currentSourceRect, nextSourceRect;

        public ArmMonster(Texture2D texture, Vector2 position, TileManager tileManager) : base(texture, position)
        {
            this.tileManager = tileManager;
            frameSize = 128;
            random = new Random();

            currentSourceRect = new Rectangle(0, 0, frameSize, frameSize);
            nextSourceRect = currentSourceRect;

            armPosition = new Rectangle((int)position.X, (int)position.Y,
                        304 - ConstantValues.tileWidth, 462 - ConstantValues.tileHeight);
        }

        public void Update(GameTime gameTime)
        {
            armPosition.X = (int)position.X;
            armPosition.Y = (int)position.Y;

            hitboxSpawnPos = position + new Vector2(65, 55);
            hitboxSpawn = new Circle(hitboxSpawnPos, 50f);

            //UpdateSourceRectangle();
        }

        public void Activating()
        {
            if (cooldown == false)
            {
                cooldown = true;

                int toActivate = random.Next(0, 4);

                if (toActivate == 0)
                {
                    activated = true;
                }
            }
        }

        public void Cooldown(GameTime gameTime)
        {
            if (cooldown == true)
            {
                timer.Start();
                if (timer.ElapsedMilliseconds >= 2000)
                {
                    cooldown = false;
                    timer.Reset();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (activated)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y,
                    ConstantValues.tileWidth, ConstantValues.tileHeight), currentSourceRect, Color.White);
            }
        }

        /*
        protected void UpdateSourceRectangle()
        {
            if (Direction == Right)
            {
                frameSize = 1;
            }
            if (Direction == Left)
            {
                frameSize = 0;
            }
        }
        */
    }
}
