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
        InvisibleMonster invMon;
        Player player;
        Lights lights;
        Camera camera;

        public GamePlayManager(GraphicsDevice graphicsDevice)
        {
            tileManager = new TileManager();

            invMon = new InvisibleMonster(TextureManager.invMonTex, new Vector2(128, 384), tileManager);

            player = new Player(TextureManager.CatTex, new Vector2(128, 445));
            camera = new Camera(Game1.graphics.GraphicsDevice.Viewport);
            lights = new Lights(player,camera);
            Game1.penumbra.Lights.Add(lights.spotlight);
            Game1.penumbra.Lights.Add(lights.playerLight);
            Game1.penumbra.Initialize();

            Game1.penumbra.Transform = camera.Transform;
        }
        
        public void Update(GameTime gameTime)
        {
            player.Collision(tileManager);
            player.Update(gameTime);
            camera.SetPosition(player.Position);
            Game1.penumbra.Transform = camera.Transform;
            lights.Update();
            invMon.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            Game1.penumbra.BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);
            tileManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            invMon.Draw(spriteBatch);
            spriteBatch.End();

            Game1.penumbra.Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            if (player.Direction == new Vector2(0, 1) && lights.spotlight.Enabled==true)
            { spriteBatch.Draw(TextureManager.FlareTex, lights.lampPos, Color.White); }
            
            spriteBatch.End();
        }
    }
}
