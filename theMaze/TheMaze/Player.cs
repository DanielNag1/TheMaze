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
        private SFX sfx;

        public Vector2 Direction { get; set; }
        private Vector2 oldPosition;
        public Vector2 hitBoxPos;
        public Rectangle middleHitbox;
            Vector2 newDirection;
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
        public bool moving = false;
        public bool isInverse = false;

        public static bool lightsOn = false;

        public static Vector2 playerLightPosition,playerSpotLightPosition;
        public Vector2 lampPosition;

        private Light playerPointLight, playerSpotLight;
        private List<Light> playerLights;

        public Weapon currentWeapon, weaponSlot1, weaponSlot2, weaponSlot3, weaponSlot4;
        public List<Weapon> weapons;
        public static Color selectedColor;

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            sfx = new SFX();
            playerLights = new List<Light>();
            weapons = new List<Weapon>();
            Direction = new Vector2(0, 1);
            currentSourceRect = new Rectangle(0, 0, frameSizeX, frameSizeY);
            nextSourceRect = currentSourceRect;



            hitboxOffsetX = frameSizeX / 8;
            hitboxOffsetY = frameSizeY / 4 * 3;
            hitbox = new Rectangle((int)position.X + hitboxOffsetX, (int)position.Y + hitboxOffsetY, frameSizeX - frameSizeX / 4, frameSizeY / 5);
            oldPosition = position;

            CreatePlayerLights();
            ChooseWeapons();
            middleHitbox = new Rectangle((int)position.X, (int)position.Y, currentSourceRect.Width/8, currentSourceRect.Height);
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

                //sfx.Footsteps(gameTime);

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
            PowerDrain(gameTime);
            hitBoxPos = position + new Vector2(frameSizeX / 2, frameSizeY / 2);
            middleHitbox.X = (int)position.X + hitbox.Width/2;
            middleHitbox.Y = (int)position.Y;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, frameSizeX, frameSizeY), currentSourceRect, Color.White);
        }


        private void ChangeWeapon()
        {
            playerSpotLight.Color = currentWeapon.color;
            playerSpotLight.Intensity = currentWeapon.power;
            playerSpotLight.Enabled = currentWeapon.enabled;

        }

        private void PowerDrain(GameTime gameTime)
        {
            if(playerSpotLight.Enabled==true)
            {
                if(currentWeapon==weaponSlot1)
                {
                    weaponSlot1.power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 500000;
                }
                if (currentWeapon == weaponSlot2)
                {
                    weaponSlot2.power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 500000;
                }
                if (currentWeapon == weaponSlot3)
                {
                    weaponSlot3.power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 500000;
                }
                if (currentWeapon == weaponSlot4)
                {
                    weaponSlot4.power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 500000;
                }
            }
        }

        private void PlayerInput()
        {
            ChangeWeapon();

            if(KeyPressed(Keys.Q))
            {
                lightsOn = true;
            }
            else if (KeyPressed(Keys.E))
            {
                lightsOn = false;
            }

            if (KeyPressed(Keys.D1))
            {
                currentWeapon = weaponSlot1;
            }
            else if (KeyPressed(Keys.D2))
            {
                currentWeapon = weaponSlot2;

            }
            else if (KeyPressed(Keys.D3))
            {
                currentWeapon = weaponSlot3;
            }
            else if (KeyPressed(Keys.D4))
            {
                currentWeapon = weaponSlot4;
            }

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
                Direction = new Vector2(-1, 0);
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

        private void ChooseWeapons()
        {
            currentWeapon = new Weapon();
            weaponSlot1 = new Weapon();
            weaponSlot2 = new Weapon();
            weaponSlot3 = new Weapon();
            weaponSlot4 = new Weapon();

            weaponSlot1.color = Color.AntiqueWhite;
            weaponSlot2.color = Color.Red;
            weaponSlot3.color = Color.Yellow;
            weaponSlot4.color = Color.CornflowerBlue;

            currentWeapon = weaponSlot1;
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

            playerLights.Add(playerSpotLight);
            playerLights.Add(playerPointLight);

            foreach (Light l in playerLights)
            {
                Game1.penumbra.Lights.Add(l);
            }
        }

        private void UpdateHitboxPosition()
        {
            hitbox.X = (int)position.X + hitboxOffsetX;
            hitbox.Y = (int)position.Y + hitboxOffsetY;
        }

        private void UpdateLights()
        {
            foreach (Light l in playerLights)
            {
                if (lightsOn)
                {
                    l.Enabled = true;
                }

                else
                {
                    l.Enabled = false;
                }
            }

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
