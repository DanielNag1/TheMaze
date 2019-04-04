using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class GamePlayManager
    {
        TileManager tileManager;
        Player player;
        Enemy enemy;
        Lights lights;
        Camera camera;

        public GamePlayManager()
        {
            tileManager = new TileManager();
            player = new Player(TextureManager.CatTex, new Vector2(64, 128));
            enemy = new Enemy(TextureManager.EvilCatTex, new Vector2(720, 728));
            lights = new Lights(player);
            Game1.penumbra.Lights.Add(lights.spotlight);
            Game1.penumbra.Lights.Add(lights.playerLight);
            Game1.penumbra.Initialize();
            camera = new Camera(Game1.graphics.GraphicsDevice.Viewport);
            Game1.penumbra.Transform = camera.Transform;
        }
        
        public Point PrefWindowSize()
        {
            return new Point(tileManager.Tiles.GetLength(0) * tileManager.tileSize, tileManager.Tiles.GetLength(1) * tileManager.tileSize);
        }

        public void Update(GameTime gameTime)
        {
            player.Collision(tileManager);
            player.Update(gameTime);
            //enemy.Collision(tileManager);
            enemy.Update(gameTime);
            camera.SetPosition(player.Position);
            Game1.penumbra.Transform = camera.Transform;
            lights.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Game1.penumbra.BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);
            tileManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
