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
    public class Lights
    {
        public Light spotLight,weapon1,weapon2,weapon3,weapon4, playerLight;
        public List<Light> weaponList;
        public Vector2 lightDirection, lampPos, mousePos, worldMouse,hitboxPos;
        public Player player;
        Camera camera;
        Circle attackhitbox;
        public Color weaponColor;
        public int r, g, b;
        float scaleX;
        public MouseState mouse;
        public Rectangle mouseRect;
        public bool canChangeWeapon;
        public float weapon1Power, weapon2Power, weapon3Power, weapon4Power;

        enum ColorState {White,Red,Yellow,Green}
        ColorState currentColor = ColorState.White;
        ColorState previousColor;


        enum Weapons {Weapon1,Weapon2,Weapon3,Weapon4}
        Weapons currentWeapon=Weapons.Weapon1;

        public Lights(Player player,Camera camera)
        {
            this.camera = camera;
            this.player = player;


            weapon1Power = .9f;
            weapon2Power = .9f;
            weapon3Power = .9f;
            weapon4Power = .9f;

            weapon1 = new Spotlight();
            weapon2 = new Spotlight();
            weapon3 = new Spotlight();
            weapon4 = new Spotlight();

            weaponList = new List<Light>();

            weapon1.Intensity = weapon1Power;
            weapon1.Enabled = false;

            weapon2.Intensity = weapon2Power;
            weapon2.Enabled = false;

            weapon3.Intensity = weapon3Power;
            weapon3.Enabled = false;

            weapon4.Intensity = weapon4Power;
            weapon4.Enabled = false;

            weaponList.Add(weapon1);
            weaponList.Add(weapon2);
            weaponList.Add(weapon3);
            weaponList.Add(weapon4);


            spotLight = new Spotlight();
            spotLight.Intensity = .9f;
            spotLight.Enabled = false;

            playerLight = new PointLight();
            playerLight.Scale = new Vector2(700, 700);
            playerLight.Color = Color.White;
            playerLight.Intensity = .85f;
            playerLight.Enabled = false;


            r = 0;
            b = 0;
            g = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (player.Direction == new Vector2(0, 1))
            {
                playerLight.Scale = new Vector2(500, 500);
            }
            if (player.Direction != new Vector2(0, 1))
            {
                playerLight.Scale = new Vector2(400,400);
            }

            Console.WriteLine(spotLight.Intensity);

            if (canChangeWeapon)
            {
                
                PowerDrain(gameTime);
                ChangeWeapon();
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    spotLight.Enabled = true;
                    playerLight.Enabled = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    spotLight.Enabled = false;
                    playerLight.Enabled = false;
                }
            }

            LightPositions();
        }



        public void ChangeWeapon()
        {
            weaponColor = new Color(r, g, b);
            
            switch(currentWeapon)
            {
                case Weapons.Weapon1:
                    spotLight.Color = weapon1.Color;
                    break;
                case Weapons.Weapon2:

                    if (weapon2.Color != Color.Green)
                    {
                        spotLight.Color = weapon2.Color;
                    }
                    if (weapon2.Color == Color.Green)
                    {
                        playerLight.Color = Color.Green;
                        
                    }
                    break;
                case Weapons.Weapon3:

                    if (weapon3.Color != Color.Green)
                    {
                        spotLight.Color = weapon3.Color;
                    }
                    if (weapon3.Color == Color.Green)
                    {
                        playerLight.Color = Color.Green;
                        
                    }
                    break;
                case Weapons.Weapon4:
                    if (weapon4.Color != Color.Green)
                    {
                        spotLight.Color = weapon4.Color;
                    }
                    if (weapon4.Color == Color.Green)
                    {
                        playerLight.Color = Color.Green;
                        
                    }
                    break;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                currentWeapon = Weapons.Weapon1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                if (weapon2.Color != Color.White)
                {
                    currentWeapon = Weapons.Weapon2;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                if (weapon3.Color != Color.White)
                {
                    currentWeapon = Weapons.Weapon3;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                if (weapon4.Color != Color.White)
                {
                    currentWeapon = Weapons.Weapon4;
                }
            }
        }

        //public void ColorChange()
        //{
        //    spotLight.Color = new Color(r, g, b);

        //    if (Keyboard.GetState().IsKeyDown(Keys.D1))
        //    {
        //        currentColor = ColorState.White;
        //        spotLight.Scale = new Vector2(820, 820);
             
        //    }
        //    if (Keyboard.GetState().IsKeyDown(Keys.D2))
        //    {
        //        currentColor = ColorState.Blue;
        //    }
        //    if (Keyboard.GetState().IsKeyDown(Keys.D3))
        //    {
        //        currentColor = ColorState.Red;
                
        //    }

        //    switch (currentColor)
        //    { 
        //        case ColorState.White:
        //            r = 255;
        //            g = 255;
        //            b = 255;
        //            previousColor = currentColor;
        //            break;
        //        case ColorState.Red:
        //            if (previousColor == ColorState.White)
        //            {
        //                if (b >= -1)
        //                {
        //                    b -= 2;
        //                }
                        
        //                if (g >= -1)
        //                {
        //                    g -= 2;
        //                }

        //                if (b <=0 && g <= 0)
        //                {
        //                    spotLight.Scale = new Vector2(920, 90);
        //                    previousColor = currentColor;
        //                }
        //            }
        //            if (previousColor == ColorState.Blue)
        //            {
        //                if(b>=-1)
        //                {
        //                    b -= 2;
        //                }
        //                if(r<=256)
        //                {
        //                    r += 2;
        //                }
        //                g = 0;

        //                if (r == 255 && b < 0 && g <= 0)
        //                {
        //                    spotLight.Scale = new Vector2(920, 920);
        //                    previousColor = currentColor;
        //                }
        //            }
                    
        //            break;
        //        case ColorState.Blue:
        //            if (previousColor == ColorState.Red)
        //            {
        //                if (r >= -1)
        //                {
        //                    r -= 2;
        //                }
        //                g = 0;

        //                if (b <= 256)
        //                {
        //                    b += 2;
        //                }

        //            }
        //            if (previousColor == ColorState.White)
        //            {
        //                if(g>=-1)
        //                {
        //                    g -= 2;
        //                }
        //                if(r>=-1)
        //                {
        //                    r -= 2;
        //                }
        //            }
        //            if(b==255 && r<0 && g<=0)
        //            {
        //                spotLight.Scale = new Vector2(820, 820);
        //                previousColor = currentColor;
        //            }
                    
        //            break;

        //    }
            
        //}

        public void LightPositions()
        {

            mouse = Mouse.GetState();
            mousePos = new Vector2((float)mouse.X, (float)mouse.Y);
            
            worldMouse = Vector2.Transform(mousePos, Matrix.Invert(camera.Transform));
            mouseRect = new Rectangle((int)worldMouse.X-30, (int)worldMouse.Y-30, 60, 60);

            spotLight.Position = new Vector2(player.Position.X + 23, player.Position.Y + 122);
            lampPos = new Vector2(spotLight.Position.X-TextureManager.FlareTex.Width/2, spotLight.Position.Y - TextureManager.FlareTex.Height / 2);
            playerLight.Position = new Vector2(player.Position.X + 70, player.Position.Y + 120);

            lightDirection = worldMouse - spotLight.Position;
            lightDirection.Normalize();

            spotLight.Rotation = (Convert.ToSingle(Math.Atan2(lightDirection.X, -lightDirection.Y))) - MathHelper.ToRadians(90f);

            scaleX = (Vector2.Distance(spotLight.Position, worldMouse))+250;
            
            spotLight.Scale = new Vector2(scaleX, scaleX);
            hitboxPos = new Vector2(worldMouse.X, worldMouse.Y);
            attackhitbox = new Circle(hitboxPos, 150f);
        }

        public bool CollisionWithLight(Circle other) //Anropas i objektets update-klass.
        {
            if (attackhitbox.Intersects(other)) return true;

            return false;
        }

        public void DrawHitBox(SpriteBatch spriteBatch)
        {
            attackhitbox.Draw(spriteBatch);
        }


        public void PowerDrain(GameTime gameTime)
        {
            weapon1.Intensity = weapon1Power;
            weapon2.Intensity = weapon2Power;
            weapon3.Intensity = weapon3Power;
            weapon4.Intensity = weapon4Power;

            if (spotLight.Enabled == true)
            {
                switch (currentWeapon)
                {
                    case Weapons.Weapon1:
                        spotLight.Intensity = weapon1Power;
                        weapon1Power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 500000;
                        break;
                    case Weapons.Weapon2:
                        spotLight.Intensity = weapon2Power;
                        
                            weapon2Power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 500000;
                        
                        break;
                    case Weapons.Weapon3:
                        spotLight.Intensity = weapon3Power;
                        
                            weapon3Power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 500000;
                        
                        break;
                    case Weapons.Weapon4:
                        spotLight.Intensity = weapon4Power;
                        
                            weapon4Power -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 500000;
                        
                        break;
                }
            }
        }
       
    }
}
