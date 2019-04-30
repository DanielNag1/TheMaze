using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;

namespace TheMaze
{
    public class Player : GameObject
    {
        public Vector2 Direction { get; set; }
        private Vector2 oldPosition;
        public Vector2 hitBoxPos;

        private Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
        }
        private int hitboxOffsetX, hitboxOffsetY;

        private Rectangle currentSourceRect, nextSourceRect;
        public readonly int frameSizeX = 125;
        public readonly int frameSizeY = 210;

        private int frame = 0, nrFrames = 4;
        private double timer = 100, timeIntervall = 100;

        private float speed = 3f;
        private bool moving = false;
        public bool isInverse = false;

        public static Vector2 ReturnPosition(Vector2 position)
        {
            return position;
        }
        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {

            currentSourceRect = new Rectangle(0, 0, frameSizeX, frameSizeY);
            nextSourceRect = currentSourceRect;

            hitboxOffsetX = frameSizeX / 8;
            hitboxOffsetY = frameSizeY / 4 * 3;
            hitbox = new Rectangle((int)position.X + hitboxOffsetX, (int)position.Y + hitboxOffsetY, frameSizeX - frameSizeX / 4, frameSizeY / 5);
            oldPosition = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, frameSizeX, frameSizeY),
                currentSourceRect, Color.White);
        }
        
        public void Update(GameTime gameTime)
        {
            if (moving)
            {
                //timer -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer <= 0)
                {
                    timer = timeIntervall;
                    frame++;
                    if (frame >= nrFrames)
                    {
                        frame = 0;
                    }
                    currentSourceRect.X = frame * frameSizeX;
                }

                PlayerInput();

                currentSourceRect.Y = nextSourceRect.Y;

                oldPosition = position;
                position += speed * Direction;

                UpdateHitboxPosition();
            }
            else
            {
                PlayerInput();
                frame = 0;

                currentSourceRect.X = frame * frameSizeX;
            }

            hitBoxPos = position + new Vector2(frameSizeX / 2, frameSizeY / 2);
        }

        private void PlayerInput()
        {
            Vector2 newDirection;

            if (KeyPressed(Keys.Up) || KeyPressed(Keys.W))
            {
                newDirection = new Vector2(0, -1);

                nextSourceRect.Y = 1 * frameSizeY;

                moving = true;
            }
            else if (KeyPressed(Keys.Down) || KeyPressed(Keys.S))
            {
                newDirection = new Vector2(0, 1);

                nextSourceRect.Y = 0 * frameSizeY;

                moving = true;
            }
            else if (KeyPressed(Keys.Left) || KeyPressed(Keys.A))
            {
                newDirection = new Vector2(-1, 0);

                nextSourceRect.Y = 2 * frameSizeY;

                moving = true;
            }
            else if (KeyPressed(Keys.Right) || KeyPressed(Keys.D))
            {
                newDirection = new Vector2(1, 0);

                nextSourceRect.Y = 3 * frameSizeY;

                moving = true;
            }
            else
            {
                newDirection = new Vector2();

                moving = false;
            }

            if (isInverse)
            {
                newDirection.X *= -1;
                newDirection.Y *= -1;
            }

            Direction = newDirection;
        }

        public void Collision(TileManager tileManager)
        {
            for (int i = 0; i < tileManager.Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tileManager.Tiles.GetLength(1); j++)
                {
                    if (tileManager.Tiles[i, j].IsWall)
                    {
                        if (hitbox.Intersects(tileManager.Tiles[i, j].Hitbox))
                        {
                            position = oldPosition;
                            UpdateHitboxPosition();
                        }
                    }
                }
            }
        }

        private void UpdateHitboxPosition()
        {
            hitbox.X = (int)position.X + hitboxOffsetX;
            hitbox.Y = (int)position.Y + hitboxOffsetY;
        }

        private bool KeyPressed(Keys k)
        {
            return Keyboard.GetState().IsKeyDown(k);
        }
    }
}
