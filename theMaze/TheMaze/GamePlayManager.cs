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
        public bool killed = false;
        enum LevelState {Live,Death,CollectibleMenu}
        LevelState currentState=LevelState.Live;
        LevelManager levelManager,deathManager;
        Player player;
        public Imbaku imbaku;
        
        Saferoom saferoom;

        Lights lights;
        ParticleEngine particleEngine;
        List<ParticleEngine> particleEngines;

        public GamePlayManager ()
        {
            levelManager = new LevelManager();
            levelManager.ReadLiveMap();
            deathManager = new LevelManager();
            deathManager.ReadDeathMap();
            
            player = new Player(TextureManager.PlayerTex, levelManager.StartPositionPlayer);
            imbaku = new Imbaku(TextureManager.ImbakuTex, levelManager.ImbakuStartPosition, levelManager);
            saferoom = new Saferoom(levelManager);
            lights = new Lights(levelManager,saferoom);

            particleEngine = new ParticleEngine(TextureManager.hitParticles, imbaku.Position);
            particleEngines = new List<ParticleEngine>();

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

            if (X.IsKeyPressed(Keys.C))
            {
                if (currentState == LevelState.Live)
                {
                    currentState = LevelState.CollectibleMenu;
                }
                if(currentState == LevelState.CollectibleMenu)
                {
                    currentState = LevelState.Live;
                }
            }

            if (X.IsKeyPressed(Keys.Enter))
            {
                if (currentState == LevelState.Live) 
                {
                    currentState = LevelState.Death;
                    player.SetPosition(deathManager.StartPositionPlayer); 
                }
                else
                {
                    currentState = LevelState.Live;
                    player.SetPosition(levelManager.StartPositionPlayer);
                }
            }

            player.Update(gameTime);
            ImbakuCollision(gameTime);
            saferoom.Update(gameTime);
            lights.Update(gameTime);
            particleEngine.Update();

            switch (currentState)
            {
                case LevelState.Live:
                    Game1.penumbra.Transform = X.camera.Transform;
                    
                    foreach (WallMonster wM in levelManager.wallMonsters)
                    {
                        wM.Update(gameTime);
                        WallMonsterCollision(wM);
                    }
                    imbaku.Update(gameTime, player);
                    TakeItem();
                    player.Collision(levelManager);
                    
                    break;

                case LevelState.Death:
                    deathManager = new LevelManager();
                    deathManager.ReadDeathMap();

                    RemoveMarkers();

                    foreach (WallMonster wM in levelManager.wallMonsters)
                    {
                        wM.active = false;
                        wM.coolDownTimer.Reset();
                    }

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
                    foreach (WallMonster wM in levelManager.wallMonsters)
                    {
                        wM.Draw(spriteBatch);
                    }
                    foreach (Collectible c in levelManager.collectibles)
                    {
                        c.Draw(spriteBatch);
                    }
                    imbaku.Draw(spriteBatch);
                    Desk(spriteBatch);
                    player.Draw(spriteBatch);                    
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);
                    Game1.penumbra.Draw(gameTime);
                    particleEngine.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                    
                case LevelState.Death:
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
        

        public void ImbakuCollision(GameTime gameTime)
        {
            if (player.middleHitbox.Intersects(imbaku.imbakuRectangleHitbox) && player.currentWeapon.enabled==true && imbaku.isAlive)
            {
                //killed = true;
            }

            if(player.weaponHitbox.Intersects(imbaku.imbakuCircleHitbox) && imbaku.isAlive)
            {
                if (player.currentWeapon.color==Color.Red)
                {
                    MonsterTakeDamage(imbaku, gameTime);
                }
                
                else if (player.currentWeapon.color==Color.Goldenrod)
                {
                    imbaku.speed = 25;
                }
                else if (player.currentWeapon.color == Color.MediumBlue)
                {
                    imbaku.speed = 0;
                }
                
            }

            else
                {
                    imbaku.speed = 50;
                    foreach (ParticleEngine p in particleEngines)
                    {
                        if (particleEngines.Count > 0)
                        {
                            p.isHit = false;
                            particleEngines.Remove(p);
                            break;
                        }
                    }
                    
                }
            
        }

        public void Resurrect()
        {
            if (X.IsKeyPressed(Keys.Space) && killed)
            {
                killed = false;
                currentState = LevelState.Live;
                player.SetPosition(levelManager.StartPositionPlayer);
                imbaku.SetPosition(levelManager.ImbakuStartPosition);
            }
        }

        public void RemoveMarkers()
        {
            player.markerList.Clear();

            foreach (Light l in Game1.penumbra.Lights)
            {
                if (l.Rotation == 1f)
                {
                    Game1.penumbra.Lights.Remove(l);
                    break;
                }
            }
        }

        public void TakeItem()
        {
            foreach (Collectible c in levelManager.collectibles)
            {
                if(player.FootHitbox.Intersects(c.hitbox) && X.IsKeyPressed(Keys.F))
                {
                    levelManager.collectibles.Remove(c);
                    player.collectibles.Add(c);
                    break;
                }
            }
        }

        public void MonsterTakeDamage(Imbaku imbaku,GameTime gameTime)
        {
            particleEngines.Add(particleEngine);
            particleEngine.EmitterLocation = imbaku.Position;
            particleEngine.isHit = true;
            imbaku.health -= gameTime.ElapsedGameTime.TotalMilliseconds / 2;
        }

        public void Desk(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.RedTexture, saferoom.deskHitbox, Color.White);
        }

    }
}
