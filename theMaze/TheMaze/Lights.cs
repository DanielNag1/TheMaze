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
        public Vector2 mousePos, lightDirection;
        public Player player;

        public Lights(Player player)
        {
            this.player = player;
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

        }

        public void Update()
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

            spotlight.Position = new Vector2(player.Position.X + 42, player.Position.Y + 50);
            playerLight.Position = new Vector2(player.Position.X + 30, player.Position.Y + 40);
            lightDirection = mousePos - spotlight.Position;
            lightDirection.Normalize();

            spotlight.Rotation = (Convert.ToSingle(Math.Atan2(lightDirection.X, -lightDirection.Y))) - MathHelper.ToRadians(90f);

        }
    }
}
