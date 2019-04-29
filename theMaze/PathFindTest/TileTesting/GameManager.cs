using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileTesting
{
    public class GameManager
    {
        LevelManager levelManager;
        Player player;
        Camera camera;

        PathFindTileMob mob;

        public GameManager(Viewport view)
        {
            levelManager = new LevelManager();
            player = new Player(levelManager.PlayerStartPosition);
            //camera = new Camera(view);

            mob = new PathFindTileMob(levelManager, levelManager.MobStartPosition, player.hitbox.Center.ToVector2() /*levelManager.PlayerStartPosition*/);
        }
        
        public void Update(GameTime gameTime)
        {
            player.Collision(levelManager);
            player.Update(gameTime);
            //camera.SetPosition(player.Position);

            mob.Update(gameTime, player);
            //camera.SetPosition(mob.Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);
            spriteBatch.Begin();
            levelManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            mob.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
