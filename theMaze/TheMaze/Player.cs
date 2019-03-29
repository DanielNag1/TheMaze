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
    class Player : GameObject
    {
        public Light spotlight,playerLight;
        public MouseState mouse;
        public Vector2 mousePos, lightDirection;

        public Vector2 Direction { get; private set; }
        private Vector2 oldPosition;

        private Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
        }
        private int hitboxOffsetX, hitboxOffsetY;

        private Rectangle currentSourceRect, nextSourceRect;
        public readonly int frameSize = 64;

        private int frame = 0, nrFrames = 4;
        private double timer = 100, timeIntervall = 100;

        private float speed = 2f;
        private bool moving = false;

        public static Vector2 ReturnPosition(Vector2 position)
        {
            return position;
        }
        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            spotlight = new Spotlight();
            spotlight.Scale = new Vector2(300, 300);
            spotlight.Color = Color.White;
            spotlight.Intensity = .75f;
            spotlight.Enabled = false;
            playerLight = new PointLight();
            playerLight.Scale = new Vector2(100, 100);
            playerLight.Color = Color.White;
            playerLight.Intensity = .85f;
            playerLight.Enabled = false;

            currentSourceRect = new Rectangle(0, 0, frameSize, frameSize);
            nextSourceRect = currentSourceRect;

            hitboxOffsetX = frameSize / 8;
            hitboxOffsetY = frameSize / 4 * 3;
            hitbox = new Rectangle((int)position.X + hitboxOffsetX, (int)position.Y + hitboxOffsetY, frameSize - frameSize / 4, frameSize / 5);
            oldPosition = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, frameSize, frameSize),
                currentSourceRect, Color.White);
        }
        
        public void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            mousePos = new Vector2((float)mouse.X, (float)mouse.Y);

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                spotlight.Enabled = true;
                playerLight.Enabled = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                spotlight.Enabled = false;
                playerLight.Enabled = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                spotlight.Color = Color.White;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                spotlight.Color = Color.Blue;
                spotlight.Scale = new Vector2(400, 400);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                spotlight.Color = Color.Red;
                spotlight.Scale = new Vector2(450, 450);
            }

            if (moving)
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
                    currentSourceRect.X = frame * frameSize;
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

                currentSourceRect.X = frame * frameSize;
            }

            spotlight.Position = new Vector2(position.X + 42, position.Y + 50);
            playerLight.Position = new Vector2(position.X + 30, position.Y + 40);

            lightDirection = mousePos - spotlight.Position;
            lightDirection.Normalize();

            spotlight.Rotation = (Convert.ToSingle(Math.Atan2(lightDirection.X, -lightDirection.Y))) - MathHelper.ToRadians(90f);

        }

        private void PlayerInput()
        {
            if (KeyPressed(Keys.Up) || KeyPressed(Keys.W))
            {
                Direction = new Vector2(0, -1);

                nextSourceRect.Y = 2 * frameSize;

                moving = true;
            }
            else if (KeyPressed(Keys.Down) || KeyPressed(Keys.S))
            {
                Direction = new Vector2(0, 1);

                nextSourceRect.Y = 0 * frameSize;

                moving = true;
            }
            else if (KeyPressed(Keys.Left) || KeyPressed(Keys.A))
            {
                Direction = new Vector2(-1, 0);

                nextSourceRect.Y = 3 * frameSize;

                moving = true;
            }
            else if (KeyPressed(Keys.Right) || KeyPressed(Keys.D))
            {
                Direction = new Vector2(1, 0);

                nextSourceRect.Y = 1 * frameSize;

                moving = true;
            }
            else
            {
                moving = false;
            }
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
