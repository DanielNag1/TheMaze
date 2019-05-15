﻿using Microsoft.Xna.Framework;
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
        enum LevelState {Live,Death}
        LevelState currentState=LevelState.Live;
        LevelManager levelManager,deathManager;
        Player player;
        Imbaku imbaku;
        GlitchMonster glitchMonster;
        ArmMonster armMonster;
        
        Saferoom saferoom;
        Lights lights;

        public GamePlayManager ()
        {
            levelManager = new LevelManager();
            levelManager.ReadLiveMap();
            deathManager = new LevelManager(); //detta var del av problemet - deathManager inte skapad
            deathManager.ReadDeathMap();
            
            player = new Player(TextureManager.PlayerTex, levelManager.StartPositionPlayer);
            imbaku = new Imbaku(TextureManager.ImbakuTex, levelManager.ImbakuStartPosition, levelManager);
            glitchMonster = new GlitchMonster(TextureManager.MonsterTex, levelManager.GlitchMonsterStartPosition, levelManager);
            armMonster = new ArmMonster(TextureManager.MonsterTex, levelManager.ArmMonsterStartPosition, levelManager);
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
                Console.WriteLine(imbaku.active);
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
            ImbakuCollision(gameTime);
            GlitchMonsterCollision(gameTime);
            ArmMonsterCollision(gameTime);
            saferoom.Update(gameTime);
            lights.Update(gameTime);
            
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
                    ImbakuChasePlayer();
                    glitchMonster.Update(gameTime, player);
                    armMonster.Update(gameTime, player);
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
                    foreach (Collectible c in levelManager.collectibles)
                    {
                        c.Draw(spriteBatch);
                    }
                    imbaku.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    glitchMonster.Draw(spriteBatch);
                    armMonster.Draw(spriteBatch);

                    spriteBatch.End();



                    Game1.penumbra.Draw(gameTime);
                    spriteBatch.Begin();
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

        public void ImbakuChasePlayer()
        {
            if (Vector2.Distance(player.Position, imbaku.Position) < 1000)
            {
                imbaku.active = true;
            }

            else
            {
                imbaku.active = false;
            }

        }

        public void GlitchMonsterCollision(GameTime gameTime)
        {
            if (player.middleHitbox.Intersects(glitchMonster.glitchMonsterRectangleHitbox))
            {
                player.isInverse = true;
            }

            if (player.isInverse)
            {
                glitchMonster.glitchMonsterTimer.Start();

                if (player.currentWeapon.enabled == false)
                {
                    player.isInverse = false;
                    glitchMonster.glitchMonsterTimer.Reset();
                }
                else if (glitchMonster.glitchMonsterTimer.ElapsedMilliseconds >= 4000)
                {
                    player.isInverse = false;
                    glitchMonster.glitchMonsterTimer.Reset();
                    killed = true;
                }
            }
        }

        public void ImbakuCollision(GameTime gameTime)
        {
            if (player.middleHitbox.Intersects(imbaku.imbakuRectangleHitbox) && player.currentWeapon.enabled==true && imbaku.isAlive)
            {
                killed = true;
            }

            if(player.weaponHitbox.Intersects(imbaku.imbakuCircleHitbox))
            {
                if (player.currentWeapon.color==Color.Red)
                {
                    imbaku.health -= gameTime.ElapsedGameTime.TotalMilliseconds/2;
                }
                if(player.currentWeapon.color==Color.Goldenrod)
                {
                    imbaku.speed = 50;
                }
                else if (player.currentWeapon.color == Color.MediumBlue)
                {
                    imbaku.speed = 0;
                }
                else
                {
                    imbaku.speed = 100;
                }
            }
        }

        public void ArmMonsterCollision(GameTime gameTime)
        {
            if (player.middleHitbox.Intersects(armMonster.armMonsterRectangleHitbox))
            {
                if (armMonster.activated)
                {
                    killed = true;
                }
                else
                {
                    armMonster.Activating(gameTime);
                }
            }

            if (player.weaponHitbox.Intersects(armMonster.armMonsterCircleHitbox) && armMonster.activated && player.currentWeapon.color == Color.Goldenrod)
            {
                armMonster.slowedDown = true;
            }
            else
            {
                armMonster.slowedDown = false;
            }
        }

        public void Resurrect()
        {
            if (X.IsKeyPressed(Keys.Space) && killed)
            {
                killed = false;
                currentState = LevelState.Live;
                imbaku.SetPosition(levelManager.ImbakuStartPosition);
                glitchMonster.SetPosition(levelManager.GlitchMonsterStartPosition);
                //armMonster.SetPosition(levelManager.ArmMonsterStartPosition);
                armMonster.ResetArmMonster();
                player.SetPosition(levelManager.StartPositionPlayer);
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
                    break;
                }
            }
        }

    }
}
