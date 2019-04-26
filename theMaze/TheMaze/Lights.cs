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
        public Light spotlight, playerLight;
        public MouseState mouse;
        public Vector2 mousePos, lightDirection,lampPos,worldMouse,hitboxPos;
        public Player player;
        Camera camera;
        Circle attackhitbox;

        int r, g, b;
        float scaleX;
        
        enum ColorState {White,Red,Blue}
        ColorState currentColor = ColorState.White;
        ColorState previousColor;

        public Lights(Player player,Camera camera)
        {
            this.camera = camera;
            this.player = player;
            spotlight = new Spotlight();
            spotlight.Scale = new Vector2(820, 820);
            spotlight.Color = Color.White;
            spotlight.Intensity = .9f;
            spotlight.Enabled = false;
            playerLight = new PointLight();
            playerLight.Scale = new Vector2(500, 500);
            playerLight.Color = Color.White;
            playerLight.Intensity = .85f;
            playerLight.Enabled = false;
            
        }

        public void Update()
        {
            
            ColorChange();
            if (player.Direction == new Vector2(0, 1))
            {
                playerLight.Scale = new Vector2(500, 500);
            }
            if (player.Direction != new Vector2(0, 1))
            {
                playerLight.Scale = new Vector2(400,400);
            }
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

            LightPositions();
        }



        public void ColorChange()
        {
            spotlight.Color = new Color(r, g, b);

            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                currentColor = ColorState.White;
                spotlight.Scale = new Vector2(820, 820);
             
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                currentColor = ColorState.Blue;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                currentColor = ColorState.Red;
                
            }

            switch (currentColor)
            { 
                case ColorState.White:
                    r = 255;
                    g = 255;
                    b = 255;
                    previousColor = currentColor;
                    break;
                case ColorState.Red:
                    if (previousColor == ColorState.White)
                    {
                        if (b >= -1)
                        {
                            b -= 2;
                        }
                        
                        if (g >= -1)
                        {
                            g -= 2;
                        }

                        if (b <=0 && g <= 0)
                        {
                            spotlight.Scale = new Vector2(920, 90);
                            previousColor = currentColor;
                        }
                    }
                    if (previousColor == ColorState.Blue)
                    {
                        if(b>=-1)
                        {
                            b -= 2;
                        }
                        if(r<=256)
                        {
                            r += 2;
                        }
                        g = 0;

                        if (r == 255 && b < 0 && g <= 0)
                        {
                            spotlight.Scale = new Vector2(920, 920);
                            previousColor = currentColor;
                        }
                    }
                    
                    break;
                case ColorState.Blue:
                    if (previousColor == ColorState.Red)
                    {
                        if (r >= -1)
                        {
                            r -= 2;
                        }
                        g = 0;

                        if (b <= 256)
                        {
                            b += 2;
                        }

                    }
                    if (previousColor == ColorState.White)
                    {
                        if(g>=-1)
                        {
                            g -= 2;
                        }
                        if(r>=-1)
                        {
                            r -= 2;
                        }
                    }
                    if(b==255 && r<0 && g<=0)
                    {
                        spotlight.Scale = new Vector2(820, 820);
                        previousColor = currentColor;
                    }
                    
                    break;

            }
            
        }

        public void LightPositions()
        {
            mouse = Mouse.GetState();
            mousePos = new Vector2((float)mouse.X, (float)mouse.Y);

            worldMouse = Vector2.Transform(mousePos, Matrix.Invert(camera.Transform));
            spotlight.Position = new Vector2(player.Position.X + 23, player.Position.Y + 122);
            lampPos = new Vector2(spotlight.Position.X-TextureManager.FlareTex.Width/2, spotlight.Position.Y - TextureManager.FlareTex.Height / 2);
            playerLight.Position = new Vector2(player.Position.X + 70, player.Position.Y + 120);

            lightDirection = worldMouse - spotlight.Position;
            lightDirection.Normalize();

            spotlight.Rotation = (Convert.ToSingle(Math.Atan2(lightDirection.X, -lightDirection.Y))) - MathHelper.ToRadians(90f);

            scaleX = (Vector2.Distance(spotlight.Position, worldMouse))+250;
            spotlight.Scale = new Vector2(scaleX, scaleX);
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
            //attackhitbox.Draw(spriteBatch);
        }
    }
}
