using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheMaze
{
    public static class X
    {
        public static KeyboardState keyboardState, oldkeyboardState;
        public static MouseState mouseState, oldmouseState;
        public static Rectangle mouseRect,menumouseRect;
        public static Vector2 mousePos, worldMouse,mousePlayerDirection;
        public static Camera camera;
        public static Player player;
        public static float mouseLampDistance;
        public static bool Exit;

        public static void LoadCamera()
        {
            camera = new Camera(Game1.graphics.GraphicsDevice.Viewport);
            camera.SetPosition(player.Position);
        }

        public static void Update(GameTime gameTime)
        {
            oldmouseState = mouseState;
            mouseState = Mouse.GetState();
            oldkeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            camera.SetPosition(player.Position);
            mousePos = new Vector2((float)mouseState.X, (float)mouseState.Y);
            worldMouse = Vector2.Transform(mousePos, Matrix.Invert(camera.Transform));
            mouseLampDistance = (Vector2.Distance(player.lampPosition, worldMouse)) + 250;

            mousePlayerDirection = worldMouse - player.lampPosition;
            mousePlayerDirection.Normalize();

            mouseRect = new Rectangle((int)worldMouse.X - 10, (int)worldMouse.Y - 10, 20, 20);
            menumouseRect = new Rectangle((int)mousePos.X - 10, (int)mousePos.Y - 10, 20, 20);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.RedTexture, mouseRect, Color.White);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return

                keyboardState.IsKeyDown(key) && oldkeyboardState.IsKeyUp(key);
                
                    
                
            }
        }
    }

