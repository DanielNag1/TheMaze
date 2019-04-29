using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileTesting
{
    public class Player
    {
        public Vector2 Position { get; private set; }
        private Texture2D texture;

        public Vector2 Direction { get; private set; }
        public Vector2 oldPosition;

        public Rectangle hitbox;
        private Rectangle Hitbox
        {
            get { return hitbox; }
        }
        private int hitboxOffsetX, hitboxOffsetY;

        private Rectangle currentSourceRect, nextSourceRect;
        public readonly int frameSize = 128;

        private int frame = 0, nrFrames = 4;
        private double timer = 100, timeIntervall = 100;

        private float speed = 2f;
        private bool moving = false;

        public Player(Vector2 position)
        {
            this.Position = position;
            texture = TextureManager.CatSheetTex;
        
            currentSourceRect = new Rectangle(0, 0, frameSize, frameSize);
            nextSourceRect = currentSourceRect;

            hitboxOffsetX = ConstantValues.TILE_WIDTH / 8;
            hitboxOffsetY = ConstantValues.TILE_HEIGHT / 4 * 3;
            hitbox = new Rectangle((int)position.X + hitboxOffsetX, (int)position.Y + hitboxOffsetY, ConstantValues.TILE_WIDTH - ConstantValues.TILE_WIDTH / 4, ConstantValues.TILE_HEIGHT / 5);
            oldPosition = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, ConstantValues.TILE_WIDTH, ConstantValues.TILE_HEIGHT), currentSourceRect, Color.White);
        }

        public void Update(GameTime gameTime)
        {
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

                oldPosition = Position;
                Position += speed * Direction;

                UpdateHitboxPosition();
            }
            else
            {
                PlayerInput();
                frame = 0;

                currentSourceRect.X = frame * frameSize;
            }


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

        public void Collision(LevelManager levelManager)
        {
            for (int i = 0; i < levelManager.Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < levelManager.Tiles.GetLength(1); j++)
                {
                    if (levelManager.Tiles[i, j].IsWall)
                    {
                        if (hitbox.Intersects(levelManager.Tiles[i, j].Hitbox))
                        {
                            Position = oldPosition;
                            UpdateHitboxPosition();
                        }
                    }
                }
            }
        }

        private void UpdateHitboxPosition()
        {
            hitbox.X = (int)Position.X + hitboxOffsetX;
            hitbox.Y = (int)Position.Y + hitboxOffsetY;
        }

        private bool KeyPressed(Keys k)
        {
            return Keyboard.GetState().IsKeyDown(k);
        }
    }
}
