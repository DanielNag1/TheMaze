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

        public GamePlayManager()
        {
            tileManager = new TileManager();
            player = new Player(TextureManager.CatTex, new Vector2(64, 128));
            Game1.penumbra.Lights.Add(player.spotlight);
            Game1.penumbra.Lights.Add(player.playerLight);
            Game1.penumbra.Initialize();
        }
        
        public Point PrefWindowSize()
        {
            return new Point(tileManager.Tiles.GetLength(0) * tileManager.tileSize, tileManager.Tiles.GetLength(1) * tileManager.tileSize);
        }

        public void Update(GameTime gameTime)
        {
            player.Collision(tileManager);
            player.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Game1.penumbra.BeginDraw();

            spriteBatch.Begin();
            tileManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
