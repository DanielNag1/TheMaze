using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Penumbra;

namespace TheMaze
{
    public class GamePlayManager
    {
        enum LevelState {Live,Death}
        LevelState currentState=LevelState.Live;
        LevelManager levelManager;
        Player player;
        Lights lights;

        public GamePlayManager ()
        {
            levelManager = new LevelManager();
            player = new Player(TextureManager.PlayerTex, levelManager.StartPositionPlayer);
            lights = new Lights();

            X.player = player;
            X.LoadCamera();
            Game1.penumbra.Initialize();
            Game1.penumbra.Transform = X.camera.Transform;

            foreach (Tile t in levelManager.Tiles)
            {
                if (t.IsHull == true)
                {
                    Game1.penumbra.Hulls.Add(Tile.HullFromRectangle(t.HullHitbox, 1));
                }
            }
        }
        
        
        public void Update(GameTime gameTime)
        {
            player.Collision(levelManager);
            player.Update(gameTime);
            Game1.penumbra.Transform = X.camera.Transform;

            switch (currentState)
            {
                case LevelState.Live:
                    lights.Update(gameTime);
                    break;

                case LevelState.Death:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            Game1.penumbra.BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);
            levelManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
            Game1.penumbra.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.End();

            switch (currentState)
            {
                case LevelState.Live:
                    break;
                    
                case LevelState.Death:
                    break;
            }
        }
    }
}
