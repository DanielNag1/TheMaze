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

        public static bool lightsOn = false;
        public static Vector2 playerLightPosition,playerSpotLightPosition;
        public Vector2 lampPosition;

        private Light playerPointLight, playerSpotLight;

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Direction = new Vector2(0, 1);
            currentSourceRect = new Rectangle(0, 0, frameSizeX, frameSizeY);
            nextSourceRect = currentSourceRect;

            hitboxOffsetX = frameSizeX / 8;
            hitboxOffsetY = frameSizeY / 4 * 3;
            hitbox = new Rectangle((int)position.X + hitboxOffsetX, (int)position.Y + hitboxOffsetY, frameSizeX - frameSizeX / 4, frameSizeY / 5);
            oldPosition = position;

            CreatePlayerLights();
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

            UpdateLights();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, frameSizeX, frameSizeY), currentSourceRect, Color.White);
        }

        private void PlayerInput()
        {
            if(KeyPressed(Keys.Q))
            {
                lightsOn = true;
            }
            else if (KeyPressed(Keys.E))
            {
                lightsOn = false;
            }

            if (KeyPressed(Keys.Up) || KeyPressed(Keys.W))
            {
                Direction = new Vector2(0, -1);

                nextSourceRect.Y = 1 * frameSizeY;

                moving = true;
            }
            else if (KeyPressed(Keys.Down) || KeyPressed(Keys.S))
            {
                Direction = new Vector2(0, 1);

                nextSourceRect.Y = 0 * frameSizeY;

                moving = true;
            }
            else if (KeyPressed(Keys.Left) || KeyPressed(Keys.A))
            {
                Direction = new Vector2(-1, 0);
                nextSourceRect.Y = 2 * frameSizeY;

                moving = true;
            }
            else if (KeyPressed(Keys.Right) || KeyPressed(Keys.D))
            {
                Direction = new Vector2(1, 0);

                nextSourceRect.Y = 3 * frameSizeY;
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
                            position = oldPosition;
                            UpdateHitboxPosition();
                        }
                    }
                }
            }
        }

        private void CreatePlayerLights()
        {
            playerPointLight = new PointLight();
            playerPointLight.Scale = new Vector2(700, 700);
            playerPointLight.Color = Color.White;
            playerPointLight.Intensity = .85f;
            playerPointLight.Position = playerLightPosition;

            playerSpotLight = new Spotlight();
            playerSpotLight.Scale = new Vector2(X.mouseLampDistance, X.mouseLampDistance);
            playerSpotLight.Color = Color.White;

            playerSpotLight.Scale = new Vector2(X.mouseLampDistance, X.mouseLampDistance);
            playerSpotLight.Rotation = (Convert.ToSingle(Math.Atan2(X.mousePlayerDirection.X, -X.mousePlayerDirection.Y))) - MathHelper.ToRadians(90f);

            Game1.penumbra.Lights.Add(playerSpotLight);
            Game1.penumbra.Lights.Add(playerPointLight);
        }
        private void UpdateHitboxPosition()
        {
            hitbox.X = (int)position.X + hitboxOffsetX;
            hitbox.Y = (int)position.Y + hitboxOffsetY;
        }

        private void UpdateLights()
        {
            playerLightPosition = new Vector2(Position.X + 70, Position.Y + 120);
            playerPointLight.Position = playerLightPosition;
            
            UpdateSpotLightPosition();

            playerSpotLight.Scale = new Vector2(X.mouseLampDistance, X.mouseLampDistance);
            playerSpotLight.Rotation = (Convert.ToSingle(Math.Atan2(X.mousePlayerDirection.X, -X.mousePlayerDirection.Y))) - MathHelper.ToRadians(90f);

        }

        private void UpdateSpotLightPosition()
        {
            playerSpotLight.Position = lampPosition;

            if (Direction == new Vector2(1,0))
            {
                lampPosition = new Vector2(Position.X + 102, Position.Y + 123);
            }
            if (Direction == new Vector2(0, 1))
            {
                lampPosition = new Vector2(Position.X + 28, Position.Y + 110);
            }
            if (Direction == new Vector2(-1, 0))
            {
                lampPosition = new Vector2(Position.X + 15, Position.Y + 113);
            }
            if (Direction == new Vector2(0, -1))
            {
                lampPosition = new Vector2(Position.X + 77, Position.Y + 110);
                
            }
        }

        public static Vector2 ReturnPosition(Vector2 position)
        {
            return position;
        }

        private bool KeyPressed(Keys k)
        {
            return Keyboard.GetState().IsKeyDown(k);
        }
    }
}
