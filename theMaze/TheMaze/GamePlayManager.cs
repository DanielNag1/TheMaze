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
        LevelManager levelManager,deathManager;
        Player player;
        
        public GamePlayManager ()
        {
            levelManager = new LevelManager();
            
            switch (currentState)
            {
                case LevelState.Live:
                    levelManager.ReadLiveMap();
                    break;
                case LevelState.Death:
                    levelManager.ReadDeathMap();
                    break;
            }

            player = new Player(TextureManager.PlayerTex, levelManager.StartPositionPlayer);
            
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
                if (X.keyboardState.IsKeyDown(Keys.Enter) && X.oldkeyboardState.IsKeyUp(Keys.Enter))
                {
                    currentState = LevelState.Death;
                }
            
            
            player.Update(gameTime);

            switch (currentState)
            {
                case LevelState.Live:
                    Game1.penumbra.Transform = X.camera.Transform;
                    player.Collision(levelManager);
                    break;

                case LevelState.Death:
                    deathManager = new LevelManager();
                    deathManager.ReadDeathMap();

                    player.Collision(deathManager);
                    
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            
            switch (currentState)
            {
                case LevelState.Live:
                    Game1.penumbra.BeginDraw();
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);
                    levelManager.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    spriteBatch.End();
                    Game1.penumbra.Draw(gameTime);
                    spriteBatch.Begin();
                    spriteBatch.End();
                    break;
                    
                case LevelState.Death:
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);
                    deathManager.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
            }
        }
    }
}
