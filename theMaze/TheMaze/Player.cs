using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        
        private Vector2 oldPosition;

        private Rectangle footHitbox;
        public Rectangle FootHitbox
        {
            get { return footHitbox; }
        }
        private int hitboxOffsetX, hitboxOffsetY;

        //private Rectangle currentSourceRect, nextSourceRect;
        public Rectangle middleHitbox, playerHitbox;
        public readonly int frameSizeX = 125;
        public readonly int frameSizeY = 250;

        //private int frame = 0, nrFrames = 4;
        //private double timer = 100, timeIntervall = 100;

        private float speed = 3f;

        public bool moving = false;
        public bool isInverse;

        public bool canChangeWeapon, insaferoom, viewCollectible;

        public static Vector2 playerLightPosition, playerSpotLightPosition;
        public Vector2 lampPosition, markerPos;

        public float weaponHitboxRadius;

        public Light playerPointLight, playerSpotLight;
        public List<Light> playerLights;
        public List<Light> markerList;
        public Weapon currentWeapon, weaponSlot1, weaponSlot2, weaponSlot3, weaponSlot4;
        public List<Weapon> weapons;
        public static Color selectedColor;

        public Circle weaponHitbox;
        public static int markers;
        public List<Collectible> collectibles;

        public float stamina;
        public int playerHealth;
        Stopwatch playerImmunityTimer = new Stopwatch();
        public bool playerImmunity = false;


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
            currentSourceRect = new Rectangle(frame, frameSize, frameSizeX, frameSizeY);
            nextSourceRect = currentSourceRect;

            nrFrames = 3;
            timer = 100;
            timeIntervall = 100;

            hitboxOffsetX = frameSizeX / 8;
            hitboxOffsetY = frameSizeY / 4 * 3;
            footHitbox = new Rectangle((int)position.X + hitboxOffsetX, (int)position.Y + hitboxOffsetY, frameSizeX - frameSizeX / 4, frameSizeY / 5);
            middleHitbox = new Rectangle((int)position.X, (int)position.Y, currentSourceRect.Width / 8, currentSourceRect.Height - 10);
            playerHitbox = new Rectangle((int)position.X, (int)position.Y, currentSourceRect.Width, currentSourceRect.Height);

            oldPosition = position;

            CreatePlayerLights();
            CreateWeapons();

            markerList = new List<Light>();
            collectibles = new List<Collectible>();

            isInverse = false;

            stamina = 5000f;
            playerHealth = 3;
            insaferoom = true;

        }

        public void Update(GameTime gameTime)
        {
            

            if (playerHealth == 1)
            {
                sfx.LowHP();
            }

            if (moving)
            {
                PlayerInput(gameTime);
                currentSourceRect.X = frame * frameSizeX;
                currentSourceRect.Y = frameSize * frameSizeY;
                UpdateSourceRectangle();

                sfx.Footsteps(gameTime);

                currentSourceRect = nextSourceRect;
                oldPosition = position;
                position += speed * Direction;


            }
            else
            {
                PlayerInput(gameTime);
                currentSourceRect.X = frame * frameSizeX;
            }

            UpdateHitboxPosition();
            UpdateLights();

            if (playerImmunity)
            {
                PlayerImmunity(gameTime);
            }

            if (canChangeWeapon)
            {
                PowerDrain(gameTime);
                WeaponInput();
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, currentSourceRect, Color.White);

        }


        private void ChangeWeapon()
        {
            playerSpotLight.Color = currentWeapon.color;
            playerSpotLight.Intensity = currentWeapon.power;
        }

        private void PowerDrain(GameTime gameTime)
        {
            if (playerSpotLight.Enabled == true)
            {
                if (currentWeapon == weaponSlot1)
                {
                    weaponSlot1.power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 250000;
                }
                if (currentWeapon == weaponSlot2)
                {
                    weaponSlot2.power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 250000;
                }
                if (currentWeapon == weaponSlot3)
                {
                    weaponSlot3.power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 250000;
                }
                if (currentWeapon == weaponSlot4)
                {
                    weaponSlot4.power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 250000;
                }
            }
        }

        public void ApplyWeapon()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                if (selectedColor != new Color(0, 0, 0, 0))
                {
                    sfx.PickWeapon();
                }
                weaponSlot2.color = selectedColor;
                playerPointLight.Color = selectedColor;
                playerPointLight.Enabled = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                if (selectedColor != new Color(0, 0, 0, 0))
                {
                    sfx.PickWeapon();
                }
                weaponSlot3.color = selectedColor;
                playerPointLight.Color = selectedColor;
                playerPointLight.Enabled = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                if (selectedColor != new Color(0, 0, 0, 0))
                {
                    sfx.PickWeapon();
                }
                weaponSlot4.color = selectedColor;
                playerPointLight.Color = selectedColor;
                playerPointLight.Enabled = true;
            }
        }

        private void PlayerInput(GameTime gameTime)
        {
            if (X.IsKeyPressed(Keys.G) || X.IsKeyPressed(Keys.G))
            {
                Console.WriteLine(markers);
                Console.WriteLine(markerList.Count);

                if (markerList.Count < 15)
                {
                    markers--;
                    Light markerLight = new PointLight();
                    markerLight.Position = new Vector2(position.X + ConstantValues.playerWidth, position.Y + ConstantValues.playerHeight);
                    markerLight.Intensity = .4f;
                    markerLight.Rotation = 1f;
                    markerLight.Scale = new Vector2(100, 100);
                    markerList.Add(markerLight);
                    Game1.penumbra.Lights.Add(markerLight);
                }
            }

            if (isInverse)
            {
                InversePlayerInput();
            }

            else
            {
                if (KeyPressed(Keys.Up) || KeyPressed(Keys.W))
                {
                    Direction = new Vector2(0, -1);

                    nextSourceRect.Y = 3 * frameSizeY;

                    moving = true;
                }
                else if (KeyPressed(Keys.Down) || KeyPressed(Keys.S))
                {
                    Direction = new Vector2(0, 1);
                    if (playerHealth == 3)
                    {
                        nextSourceRect.Y = 2 * frameSizeY;
                    }
                    else if (playerHealth == 2)
                    {
                        nextSourceRect.Y = 4 * frameSizeY;
                    }

                    else if (playerHealth == 1)
                    {
                        nextSourceRect.Y = 5 * frameSizeY;
                    }
                    

                    moving = true;
                }
                else if (KeyPressed(Keys.Left) || KeyPressed(Keys.A))
                {
                    Direction = new Vector2(-1, 0);
                    nextSourceRect.Y = 0 * frameSizeY;

                    moving = true;
                }
                else if (KeyPressed(Keys.Right) || KeyPressed(Keys.D))
                {
                    Direction = new Vector2(1, 0);

                    nextSourceRect.Y = 1 * frameSizeY;
                    moving = true;
                }
                
                else
                {
                    moving = false;
                }
            }

            if (KeyPressed(Keys.LeftShift))
            {
                if (stamina >= 0)
                {
                    sfx.Sprint();
                    speed = 3.72f;
                    stamina -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    speed = 3f;
                }
            }

            else
            {
                speed = 3f;
                sfx.StopSprint();
                if (stamina <= 5000)
                    stamina += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }


        }

        private void WeaponInput()
        {
            ChangeWeapon();

            if (KeyPressed(Keys.Q))
            {
                sfx.LampSwitchOn();

                currentWeapon.enabled = true;
            }
            else if (KeyPressed(Keys.E))
            {
                sfx.LampSwitchOff();

                currentWeapon.enabled = false;
            }

            if (KeyPressed(Keys.D1))
            {
                currentWeapon = weaponSlot1;
                weaponSlot1.enabled = true;
            }
            else if (KeyPressed(Keys.D2))
            {
                if (weaponSlot2.color != new Color(0, 0, 0, 255))
                {
                    currentWeapon = weaponSlot2;
                    weaponSlot2.enabled = true;
                }
            }
            else if (KeyPressed(Keys.D3))
            {
                if (weaponSlot3.color != new Color(0, 0, 0, 255))
                {
                    currentWeapon = weaponSlot3;
                    weaponSlot3.enabled = true;
                }
            }
            else if (KeyPressed(Keys.D4))
            {
                if (weaponSlot4.color != new Color(0, 0, 0, 255))
                {
                    currentWeapon = weaponSlot4;
                    weaponSlot4.enabled = true;
                }
            }

        }

        private void CreateWeapons()
        {
            currentWeapon = new Weapon();
            weaponSlot1 = new Weapon();
            weaponSlot2 = new Weapon();
            weaponSlot3 = new Weapon();
            weaponSlot4 = new Weapon();
            weapons.Add(currentWeapon);
            weapons.Add(weaponSlot1);
            weapons.Add(weaponSlot2);
            weapons.Add(weaponSlot3);
            weapons.Add(weaponSlot4);
            weaponSlot1.color = Color.AntiqueWhite;

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
                        if (footHitbox.Intersects(levelManager.Tiles[i, j].Hitbox))
                        {
                            position = oldPosition;
                            UpdateHitboxPosition();
                        }
                    }
                }
            }
        }

        public void CreatePlayerLights()
        {
            playerPointLight = new PointLight();
            playerPointLight.Scale = new Vector2(700, 700);
            playerPointLight.Color = Color.White;
            playerPointLight.Intensity = .85f;
            playerPointLight.Position = playerLightPosition;

            playerSpotLight = new Spotlight();
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
            footHitbox.X = (int)position.X + hitboxOffsetX;
            footHitbox.Y = (int)position.Y + hitboxOffsetY;

            middleHitbox.X = (int)position.X + footHitbox.Width / 2;
            middleHitbox.Y = (int)position.Y;

            playerHitbox.X = (int)position.X;
            playerHitbox.Y = (int)position.Y;

            weaponHitbox = new Circle(X.worldMouse, weaponHitboxRadius);
            weaponHitboxRadius = Vector2.Distance(X.worldMouse, lampPosition) / 2;

        }

        private void UpdateLights()
        {
            foreach (Light l in playerLights)
            {
                if (currentWeapon.enabled)
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

            if (Direction == new Vector2(1, 0))
            {
                lampPosition = new Vector2(Position.X + 110, Position.Y + 140);
            }
            if (Direction == new Vector2(0, 1))
            {
                lampPosition = new Vector2(Position.X + 16, Position.Y + 135);
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

        public void PlayerImmunity(GameTime gameTime)
        {
            playerImmunityTimer.Start();

            if (playerImmunityTimer.ElapsedMilliseconds > 2000)
            {
                playerImmunity = false;
                playerImmunityTimer.Reset();
            }
        }

        private void InversePlayerInput()
        {
            if (KeyPressed(Keys.Up) || KeyPressed(Keys.W))
            {
                Direction = new Vector2(0, 1);

                nextSourceRect.Y = 0 * frameSizeY;

                moving = true;
            }
            else if (KeyPressed(Keys.Down) || KeyPressed(Keys.S))
            {
                Direction = new Vector2(0, -1);

                nextSourceRect.Y = 1 * frameSizeY;

                moving = true;
            }
            else if (KeyPressed(Keys.Left) || KeyPressed(Keys.A))
            {
                Direction = new Vector2(1, 0);
                nextSourceRect.Y = 3 * frameSizeY;

                moving = true;
            }
            else if (KeyPressed(Keys.Right) || KeyPressed(Keys.D))
            {
                Direction = new Vector2(-1, 0);

                nextSourceRect.Y = 2 * frameSizeY;
                moving = true;
            }
            else
            {
                moving = false;
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
