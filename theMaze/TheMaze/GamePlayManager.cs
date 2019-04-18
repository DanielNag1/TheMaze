using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace TheMaze
{
    class GamePlayManager
    {
        TileManager tileManager;
        Player player;
        Monster monster;
        Lights lights;
        Camera camera;
        Circle attackhitbox;

        public GamePlayManager(GraphicsDevice graphicsDevice)
        {
            tileManager = new TileManager();
            player = new Player(TextureManager.CatTex, tileManager.StartPositionPlayer);
            monster = new Monster(TextureManager.MonsterTex, tileManager.StartPositionMonster, tileManager);
            camera = new Camera(Game1.graphics.GraphicsDevice.Viewport);
            lights = new Lights(player, camera);
            Game1.penumbra.Lights.Add(lights.spotlight);
            Game1.penumbra.Lights.Add(lights.playerLight);
            Game1.penumbra.Initialize();

            Game1.penumbra.Transform = camera.Transform;
            attackhitbox = new Circle(lights.worldMouse, 40f);
        }

        public void Update(GameTime gameTime)
        {
            player.Collision(tileManager);
            player.Update(gameTime);

            monster.Update(gameTime);
            MonsterLightCollision(gameTime);

            camera.SetPosition(player.Position);
            Game1.penumbra.Transform = camera.Transform;
            lights.Update();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Game1.penumbra.BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            tileManager.Draw(spriteBatch);

            monster.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();

            Game1.penumbra.Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            if (player.Direction == new Vector2(0, 1) && lights.spotlight.Enabled == true)
            { spriteBatch.Draw(TextureManager.FlareTex, lights.lampPos, Color.White); }

            lights.DrawHitBox(spriteBatch);
            spriteBatch.End();
        }

        public void MonsterLightCollision(GameTime gameTime)
        {
            if (lights.CollisionWithLight(monster.hitbox))
            {
                monster.health -= gameTime.ElapsedGameTime.TotalSeconds;

                if (monster.health <= 0)
                {
                    monster.isAlive = false;
                }

                monster.speed = 50f;

                monster.color = new Color(Color.Red, 0);
            }
            else
            {
                monster.speed = 100f;
                if (monster.health >= 2.5f)
                {
                    monster.color = Color.White;
                }
                else if (monster.health <= 2.5f)
                {
                    monster.color = new Color(100, 100, 100, 100);
                }
            }
        }
    }
}
