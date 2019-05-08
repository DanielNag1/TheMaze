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
        Imbaku imbaku;
        
        Saferoom saferoom;
        Lights lights;

        public GamePlayManager ()
        {
            levelManager = new LevelManager();
            levelManager.ReadLiveMap();
            deathManager = new LevelManager(); //detta var del av problemet - deathManager inte skapad
            deathManager.ReadDeathMap();

            //switch (currentState)
            //{
            //    case LevelState.Live:
            //        levelManager.ReadLiveMap();
            //        break;
            //    case LevelState.Death:
            //        deathManager.ReadDeathMap();
            //        break;
            //}

            player = new Player(TextureManager.PlayerTex, levelManager.StartPositionPlayer);
            imbaku = new Imbaku(TextureManager.ImbakuTex, levelManager.ImbakuStartPosition, levelManager);
            saferoom = new Saferoom(levelManager);
            lights = new Lights(levelManager,saferoom);

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
            if(X.IsKeyPressed(Keys.Space))
            {
            }

            if (X.IsKeyPressed(Keys.Enter))
            {
                if (currentState == LevelState.Live) //Gjort så att man kan testa att gå fram och tillbaka mellan dem
                {
                    currentState = LevelState.Death;
                    player.SetPosition(deathManager.StartPositionPlayer); //detta var andra delen av problemet
                }
                else
                {
                    currentState = LevelState.Live;
                    player.SetPosition(levelManager.StartPositionPlayer);
                }
            }

            player.Update(gameTime);
            saferoom.Update(gameTime);
            lights.Update(gameTime);

            switch (currentState)
            {
                case LevelState.Live:
                    Game1.penumbra.Transform = X.camera.Transform;
                    imbaku.Update(gameTime);

                    foreach (WallMonster wM in levelManager.wallMonsters)
                    {
                        wM.Update(gameTime);
                        WallMonsterCollision(wM);
                    }

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
            //Så här bör knappar ritas ut
            //spriteBatch.Begin();
            ////testButton.Draw(spriteBatch); 
            //spriteBatch.End();

            switch (currentState)
            {
                case LevelState.Live:
                    Game1.penumbra.BeginDraw();
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);

                    levelManager.Draw(spriteBatch);
                    foreach (WallMonster wM in levelManager.wallMonsters)
                    {
                        wM.Draw(spriteBatch);
                    }
                    imbaku.Draw(spriteBatch);
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



        public void WallMonsterCollision(WallMonster wallMonster)
        {
            if (player.middleHitbox.Intersects(wallMonster.hitBoxRect) && !wallMonster.coolDown)
            {
                wallMonster.active = true;

            }

            if (wallMonster.active)
            {
                player.moving = false;

            }

            if (player.weaponHitbox.Intersects(wallMonster.hitbox) && wallMonster.active && player.currentWeapon.color==Color.MediumBlue)
            {

                wallMonster.attackTimer.Start();

                if (wallMonster.attackTimer.ElapsedMilliseconds >= 3000)
                {
                    wallMonster.coolDown = true;
                    wallMonster.attackTimer.Reset();
                }

            }






        }

    }
}
