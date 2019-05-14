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
        enum LevelState { Live, Death, CollectibleMenu }
        LevelState currentState = LevelState.Live;
        enum Level { Level1, Level2, Level3 }
        Level currentLevel = Level.Level2;
        LevelManager levelManager, deathManager;
        Player player;
        public Imbaku imbaku;
        Golem golem;
      
        SFX sfx;
        private bool level1loaded, level2loaded;

        Saferoom saferoom;

        Lights lights;
        ParticleEngine particleEngine;
        List<ParticleEngine> particleEngines;

        public GamePlayManager()
        {
            levelManager = new LevelManager();
            //levelManager.LoadLevel1();
            deathManager = new LevelManager();
            deathManager.ReadDeathMap();
            LoadLevel2(levelManager);
            
            

            particleEngines = new List<ParticleEngine>();

            X.player = player;
            //X.imbaku = imbaku;
            X.LoadCamera();
            //            Game1.penumbra.Initialize();
            Game1.penumbra.Transform = X.camera.Transform;

        }




        public void LoadLevel1(LevelManager levelManager)
        {
            if (!level1loaded)
            {
                levelManager.ReadLevel1();

                player = new Player(TextureManager.PlayerTex, levelManager.StartPositionPlayer);
                saferoom = new Saferoom(levelManager);
                lights = new Lights(levelManager, saferoom);
                sfx = new SFX();


                foreach (Tile t in levelManager.Tiles)
                {
                    if (t.IsHull == true)
                    {
                        Game1.penumbra.Hulls.Add(Tile.HullFromRectangle(t.HullHitbox, 1));
                    }
                }
                level1loaded = true;
            }
        }

        public void LoadLevel2(LevelManager levelManager)
        {
            if (!level2loaded)
            {
                levelManager.ReadLevel2();
                if (player != null)
                {
                    player.SetPosition(levelManager.StartPositionPlayer);
                }
                else
                {
                    player = new Player(TextureManager.PlayerTex, levelManager.StartPositionPlayer);
                }
                saferoom = new Saferoom(levelManager);
                lights = new Lights(levelManager, saferoom);
                sfx = new SFX();

                imbaku = new Imbaku(TextureManager.ImbakuTex, levelManager.ImbakuStartPosition, levelManager);
                golem = new Golem(TextureManager.MonsterTex, levelManager.GolemStartPosition, levelManager);
                particleEngine = new ParticleEngine(TextureManager.hitParticles, imbaku.Position);

                foreach (Tile t in levelManager.Tiles)
                {
                    if (t.IsHull == true)
                    {
                        Game1.penumbra.Hulls.Add(Tile.HullFromRectangle(t.HullHitbox, 1));
                    }
                }

                level2loaded = true;
            }
        }
        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            saferoom.Update(gameTime);
            lights.Update(gameTime);

            switch (currentLevel)
            {
                case Level.Level1:
                    Level1TutorialUpdate();
                    break;
                case Level.Level2:
                    Level2Update(gameTime);
                    break;
                case Level.Level3:
                    break;
            }
            //Självmord
            switch (currentState)
            {
                case LevelState.Live:
                    Game1.penumbra.Transform = X.camera.Transform;

                    TakeItem();
                    player.Collision(levelManager);

                    if (X.IsKeyPressed(Keys.Enter))
                    {
                        currentState = LevelState.Death;
                        player.SetPosition(deathManager.StartPositionPlayer);
                        
                    }

                    break;

                case LevelState.Death:
                    RemoveMarkers();

                    foreach (WallMonster wM in levelManager.wallMonsters)
                    {
                        wM.active = false;
                        wM.coolDownTimer.Reset();
                    }

                    player.Collision(deathManager);

                    if (X.IsKeyPressed(Keys.Enter))
                    {
                        ChangeLevel();
                        currentState = LevelState.Live;
                        player.SetPosition(levelManager.StartPositionPlayer);
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (currentLevel)
            {
                case Level.Level1:
                    Level1Draw(spriteBatch, gameTime);
                    break;
                case Level.Level2:
                    Level2Draw(spriteBatch, gameTime);
                    break;
                case Level.Level3:
                    break;
            }

        }

        public void WallMonsterCollision(WallMonster wallMonster)
        {
            if (player.middleHitbox.Intersects(wallMonster.hitBoxRect) && !wallMonster.coolDown)
            {
                wallMonster.active = true;
                sfx.GlitchEncounter();
            }

            if (wallMonster.active)
            {
                player.moving = false;

            }

            if (player.weaponHitbox.Intersects(wallMonster.hitbox) && wallMonster.active && player.currentWeapon.color == Color.MediumBlue)
            {

                wallMonster.attackTimer.Start();

                if (wallMonster.attackTimer.ElapsedMilliseconds >= 3000)
                {
                    wallMonster.coolDown = true;
                    wallMonster.attackTimer.Reset();

                    sfx.GlitchEncounterOff();
                }

            }

        }

        public void Level1TutorialUpdate()
        {
            TutorialManager.buttonPressCheck();
        }

        public void Level1TutorialDraw(SpriteBatch spriteBatch)
        {
            if (!TutorialManager.tutorialMovingDone)
            {
                IngameTextmanager.DrawMovingTutorial(spriteBatch);
            }
            if (TutorialManager.onItem)
            {
                IngameTextmanager.DrawPickUpTutorial(spriteBatch);
            }
            if (!X.player.insaferoom && !TutorialManager.q && !TutorialManager.tutorialLampDone)
            {
                IngameTextmanager.DrawLampOn(spriteBatch);
            }
            if (!X.player.insaferoom && !TutorialManager.e && !TutorialManager.tutorialLampDone)
            {
                IngameTextmanager.DrawLampOff(spriteBatch);
            }
            if (X.player.collectibles.Count >= 3 && !X.player.insaferoom)
            {
                IngameTextmanager.DrawProgress(spriteBatch);
            }
        }
        public void ImbakuCollision(GameTime gameTime)
        {
            if (player.middleHitbox.Intersects(imbaku.imbakuRectangleHitbox) && player.currentWeapon.enabled == true)
            {
                //killed = true;
            }

            if (Vector2.Distance(player.middleHitbox.Center.ToVector2(), imbaku.imbakuCircleHitbox.Center) < ConstantValues.tileHeight * 2.5 &&
                player.currentWeapon.enabled && !X.player.insaferoom)
            {
                sfx.ImbakuEncounter();
            }
            if (player.weaponHitbox.Intersects(imbaku.imbakuCircleHitbox) && Vector2.Distance(player.middleHitbox.Center.ToVector2(), imbaku.imbakuCircleHitbox.Center) >
                ConstantValues.tileHeight * 2.5 && !X.player.insaferoom)
            {
                sfx.ImbakuEncounter();
            }


            if (Vector2.Distance(player.Position, imbaku.Position) < 400)
            {
                imbaku.isActive = true;
            }
            else
            {
                imbaku.isActive = false;
            }

            if (Vector2.Distance(player.Position,imbaku.Position)>2500 || Vector2.Distance(player.Position, imbaku.Position) < 600)
            {
                imbaku.isChasing = true;
            }

            if (player.weaponHitbox.Intersects(imbaku.imbakuCircleHitbox) && player.currentWeapon.color == Color.Red)
            {
                MonsterTakeDamage(imbaku, gameTime);
            }

            else
            {
                //imbaku.speed = 50;
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

        public void MiniCollision()
        {
            foreach (MiniMonster mini in imbaku.miniMonsterList)
            {
                if (player.weaponHitbox.Intersects(imbaku.miniMonster.miniCircleHitbox) && player.currentWeapon.color==Color.Red)
                {
                    imbaku.miniMonster.health--;
                    if (imbaku.miniMonster.health <= 0)
                    {
                        imbaku.miniMonsterList.Remove(mini);
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
                if (player.FootHitbox.Intersects(c.hitbox))
                {
                    TutorialManager.onItem = true;
                    break;
                }
            }
            foreach (Collectible c in levelManager.collectibles)
            {
                if (player.FootHitbox.Intersects(c.hitbox) && X.IsKeyPressed(Keys.F))
                {
                    TutorialManager.onItem = false;
                    levelManager.collectibles.Remove(c);
                    player.collectibles.Add(c);
                    break;
                }
            }
        }

        public void MonsterTakeDamage(Imbaku imbaku, GameTime gameTime)
        {
            particleEngines.Add(particleEngine);
            particleEngine.EmitterLocation = new Vector2(imbaku.imbakuRectangleHitbox.Center.X,imbaku.imbakuRectangleHitbox.Center.Y);
            particleEngine.isHit = true;
            
        }

        public void Desk(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.DeskTexture, saferoom.deskHitbox, Color.White);
        }

        public void Level1Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (currentState)
            {
                case LevelState.Live:
                    Game1.penumbra.BeginDraw();
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);

                    levelManager.Draw(spriteBatch);

                    foreach (Collectible c in levelManager.collectibles)
                    {
                        c.Draw(spriteBatch);
                    }
                    Desk(spriteBatch);
                    player.Draw(spriteBatch);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);
                    Game1.penumbra.Draw(gameTime);
                    Level1TutorialDraw(spriteBatch);
                    spriteBatch.End();
                    break;

                case LevelState.Death:
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);
                    deathManager.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    IngameTextmanager.DrawReturn(spriteBatch);
                    spriteBatch.End();
                    break;

            }
        }
        public void Level2Draw(SpriteBatch spriteBatch, GameTime gameTime)
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
                    golem.Draw(spriteBatch);
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

        public void Level2Update(GameTime gameTime)
        {
            particleEngine.Update();
            imbaku.Update(gameTime, player);
            golem.Update(gameTime,player);
            ImbakuCollision(gameTime);
            MiniCollision();
            foreach (WallMonster wM in levelManager.wallMonsters)
            {
                wM.Update(gameTime);
                WallMonsterCollision(wM);
            }

        }

        public void ChangeLevel()
        {

            if(player.collectibles.Count==3)
            {
                currentLevel = Level.Level2;
            }
            else
            {
                currentLevel = Level.Level1;
            }
            
            switch(currentLevel)
            {
                case Level.Level1:
                    LoadLevel1(levelManager);
                    break;
                case Level.Level2:
                    LoadLevel2(levelManager);
                    break;

            }

        }


    }
}
