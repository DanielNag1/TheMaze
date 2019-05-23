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
        public static bool gameCompleted;
        public enum LevelState { Live, Death }
        public static LevelState currentState = LevelState.Live;
        public enum Level { Level1, Level2, Level3, Level4 }
        public static Level currentLevel = Level.Level1;
        LevelManager levelManager, deathManager;

        Player player;

        SFX sfx;
        BGM bgm;

        private bool level1loaded, level2loaded, level3loaded, level4loaded;
        public static bool black;
        Saferoom saferoom;

        Lights lights;
        //ParticleEngine particleEngine;
        //List<ParticleEngine> particleEngines;
        Rectangle toRespawnRectangle;
        IngameTextmanager ingameTextmanager;

        public GamePlayManager()
        {
            levelManager = new LevelManager();
            deathManager = new LevelManager();
            deathManager.ReadDeathMap();
            ingameTextmanager = new IngameTextmanager();

            LoadLevel1(levelManager);
            //LoadLevel2(levelManager);
            //LoadLevel3(levelManager);
            toRespawnRectangle = new Rectangle((int)deathManager.SuicideHallwayStopPosition.X, (int)deathManager.SuicideHallwayStopPosition.Y, ConstantValues.tileWidth, ConstantValues.tileHeight);

            X.player = player;
            X.LoadCamera();
            //Game1.penumbra.Initialize();
            Game1.penumbra.Transform = X.camera.Transform;

            bgm = new BGM();
            sfx = new SFX();

            gameCompleted = false;
        }




        public void LoadLevel1(LevelManager levelManager)
        {
            if (!level1loaded)
            {
                levelManager.ReadLevel1();

                player = new Player(TextureManager.PlayerTex, levelManager.StartPositionPlayer);
                saferoom = new Saferoom(levelManager);
                lights = new Lights(levelManager, saferoom);

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
                Game1.penumbra.Lights.Clear();
                Game1.penumbra.Hulls.Clear();
                RemovePreviousCollectibleLights();

                ingameTextmanager.currentStage = IngameTextmanager.Textstage.Q;
                levelManager.ReadLevel2();

                if (player != null)
                {
                    player.CreatePlayerLights();
                    player.SetPosition(levelManager.StartPositionPlayer);
                }
                else
                {
                    player = new Player(TextureManager.PlayerTex, levelManager.StartPositionPlayer);
                }

                saferoom = new Saferoom(levelManager);
                lights = new Lights(levelManager, saferoom);

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
        public void LoadLevel3(LevelManager levelmanager)
        {
            if (!level3loaded)
            {
                Game1.penumbra.Lights.Clear();
                Game1.penumbra.Hulls.Clear();
                RemovePreviousCollectibleLights();

                levelManager.ReadLevel3();

                if (player != null)
                {
                    player.CreatePlayerLights();
                    player.SetPosition(levelManager.StartPositionPlayer);
                }
                else
                {
                    player = new Player(TextureManager.PlayerTex, levelManager.StartPositionPlayer);
                }

                saferoom = new Saferoom(levelManager);
                lights = new Lights(levelManager, saferoom);

                foreach (Tile t in levelManager.Tiles)
                {
                    if (t.IsHull == true)
                    {
                        Game1.penumbra.Hulls.Add(Tile.HullFromRectangle(t.HullHitbox, 1));
                    }
                }

                level3loaded = true;
            }
        }
        public void LoadLevel4(LevelManager levelmanager)
        {
            if (!level4loaded)
            {
                Game1.penumbra.Lights.Clear();
                Game1.penumbra.Hulls.Clear();
                RemovePreviousCollectibleLights();

                levelManager.ReadLevel4();

                if (player != null)
                {
                    player.CreatePlayerLights();
                    player.SetPosition(levelManager.StartPositionPlayer);
                }
                else
                {
                    player = new Player(TextureManager.PlayerTex, levelManager.StartPositionPlayer);
                }

                saferoom = new Saferoom(levelManager);
                lights = new Lights(levelManager, saferoom);

                foreach (Tile t in levelManager.Tiles)
                {
                    if (t.IsHull == true)
                    {
                        Game1.penumbra.Hulls.Add(Tile.HullFromRectangle(t.HullHitbox, 1));
                    }
                }

                level4loaded = true;
            }
        }

        public void Update(GameTime gameTime)
        {

            Console.WriteLine(ingameTextmanager.currentStage);
            Console.WriteLine(player.collectibles.Count);
            player.Update(gameTime);

            saferoom.Update(gameTime);
            lights.Update(gameTime);
            IngameTextShowing(gameTime);



            if (black)
            {
                switch (currentLevel)
                {
                    case Level.Level1:
                        Level1TutorialUpdate();
                        Level1Update(gameTime);
                        break;
                    case Level.Level2:
                        Level2Update(gameTime);
                        break;
                    case Level.Level3:
                        Level3Update(gameTime);
                        break;
                    case Level.Level4:
                        Level4Update(gameTime);
                        break;
                }

            }


            //Självmord
            switch (currentState)
            {
                case LevelState.Live:
                    bgm.PlayBGM();
                    black = true;
                    Game1.penumbra.Transform = X.camera.Transform;

                    TakeItem();
                    player.Collision(levelManager);

                    if (currentLevel != Level.Level4)
                    {
                        if (X.IsKeyPressed(Keys.Enter))
                        {
                            sfx.Suicide();
                            currentState = LevelState.Death;
                            player.SetPosition(deathManager.StartPositionPlayer);

                        }
                    }
                    if (currentLevel == Level.Level4 && player.insaferoom)
                    {
                        if (X.IsKeyPressed(Keys.Enter))
                        {
                            currentState = LevelState.Death;
                            player.SetPosition(deathManager.StartPositionPlayer);

                        }
                    }

                    break;

                case LevelState.Death:

                    black = false;
                    RemoveMarkers();
                    player.playerImmunity = true;
                    player.insaferoom = false;

                    foreach (WallMonster wallMonster in levelManager.wallMonsterList)
                    {
                        wallMonster.active = false;
                        wallMonster.coolDownTimer.Reset();
                    }

                    player.Collision(deathManager);

                    if (player.middleHitbox.Intersects(toRespawnRectangle))
                    {
                        ChangeLevel();
                        currentState = LevelState.Live;
                        player.SetPosition(levelManager.StartPositionPlayer);
                    }

                    break;
            }

            bgm.PlayWhiteBGM();
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
                    Level3Draw(spriteBatch, gameTime);
                    break;
                case Level.Level4:
                    Level4Draw(spriteBatch, gameTime);
                    break;
            }

        }


        public void Level1TutorialUpdate()
        {
            TutorialManager.buttonPressCheck();
        }

        public void Level1Update(GameTime gameTime)
        {
            foreach (WallMonster wallMonster in levelManager.wallMonsterList)
            {
                wallMonster.Update(gameTime, player);
                WallMonsterCollision(wallMonster);
            }

        }

        public void Level1TutorialDraw(SpriteBatch spriteBatch)
        {
            if (!TutorialManager.tutorialMovingDone)
            {
                IngameTextmanager.DrawMovingTutorial(spriteBatch);
            }
            if (TutorialManager.onItem)
            {
                TutorialManager.tutorialMovingDone = true;
                TutorialManager.tutorialLampDone = true;
                IngameTextmanager.DrawPickUpTutorial(spriteBatch);
            }
            if (!X.player.insaferoom && !TutorialManager.q && !TutorialManager.tutorialLampDone)
            {
                TutorialManager.tutorialMovingDone = true;
                IngameTextmanager.DrawLampOn(spriteBatch);
            }
            if (!X.player.insaferoom && !TutorialManager.e && !TutorialManager.tutorialLampDone)
            {
                IngameTextmanager.DrawLampOff(spriteBatch);
            }
            if (X.player.collectibles.Count == 1 && !X.player.insaferoom)
            {
                IngameTextmanager.DrawProgress(spriteBatch);
            }
        }


        public void Resurrect()
        {
            if (X.IsKeyPressed(Keys.Space) && killed)
            {
                killed = false;
                currentState = LevelState.Live;
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

        public void RemovePreviousCollectibleLights()
        {
            levelManager.collectiblePositions.Clear();
            foreach (Light l in Game1.penumbra.Lights)
            {
                if (l.Rotation == 2f)
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
                    sfx.TakeItem();
                    TutorialManager.onItem = false;
                    levelManager.collectibles.Remove(c);
                    player.collectibles.Add(c);
                    break;
                }
            }
        }

        public void MonsterTakeDamage(Imbaku imbaku, GameTime gameTime)
        {

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

                    foreach (WallMonster wallMonster in levelManager.wallMonsterList)
                    {
                        wallMonster.Draw(spriteBatch);
                    }
                    Desk(spriteBatch);
                    player.Draw(spriteBatch);

                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);
                    Game1.penumbra.Draw(gameTime);
                    Level1TutorialDraw(spriteBatch);

                    DrawHPWarning(spriteBatch);

                    spriteBatch.End();
                    break;

                case LevelState.Death:
                    DeathDraw(spriteBatch);
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


                    foreach (Collectible c in levelManager.collectibles)
                    {
                        c.Draw(spriteBatch);
                    }

                    foreach (ArmMonster armMonster in levelManager.armMonsterList)
                    {
                        armMonster.Draw(spriteBatch);
                    }
                    foreach (Stalker stalker in levelManager.stalkerList)
                    {
                        stalker.Draw(spriteBatch);
                    }

                    Desk(spriteBatch);
                    player.Draw(spriteBatch);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);
                    Game1.penumbra.Draw(gameTime);
                    ingameTextmanager.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case LevelState.Death:
                    DeathDraw(spriteBatch);
                    break;

            }
        }

        public void Level3Draw(SpriteBatch spriteBatch, GameTime gameTime)
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

                    foreach (ArmMonster armMonster in levelManager.armMonsterList)
                    {
                        armMonster.Draw(spriteBatch);
                    }

                    foreach (Imbaku imbaku in levelManager.imbakuList)
                    {
                        imbaku.Draw(spriteBatch);

                        foreach (MiniMonster miniMonster in imbaku.miniMonsterList)
                        {
                            miniMonster.Draw(spriteBatch);

                        }

                    }

                    foreach (Golem golem in levelManager.golemList)
                    {
                        golem.Draw(spriteBatch);
                    }

                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);
                    Game1.penumbra.Draw(gameTime);
                    ingameTextmanager.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case LevelState.Death:
                    DeathDraw(spriteBatch);
                    break;

            }
        }

        public void Level4Draw(SpriteBatch spriteBatch, GameTime gameTime)
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

                    foreach (Golem golem in levelManager.golemList)
                    {
                        golem.Draw(spriteBatch);
                    }

                    foreach (WallMonster wallMonster in levelManager.wallMonsterList)
                    {
                        wallMonster.Draw(spriteBatch);
                    }

                    foreach (Stalker stalker in levelManager.stalkerList)
                    {
                        stalker.Draw(spriteBatch);
                    }

                    Desk(spriteBatch);
                    player.Draw(spriteBatch);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.Transform);
                    Game1.penumbra.Draw(gameTime);
                    ingameTextmanager.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case LevelState.Death:
                    DeathDraw(spriteBatch);
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.TransformHallway);
                    ingameTextmanager.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

            }
        }

        public void DeathDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, X.camera.TransformHallway);
            spriteBatch.Draw(TextureManager.HallWayBackground, new Vector2(toRespawnRectangle.Center.X - (TextureManager.Vitadelen.Width + 1000), toRespawnRectangle.Center.Y - 760), Color.White);
            deathManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.Draw(TextureManager.Vitadelen, new Vector2(toRespawnRectangle.Center.X - TextureManager.Vitadelen.Width / 2 - 500, toRespawnRectangle.Center.Y - 768), Color.White);
            spriteBatch.Draw(TextureManager.Svartadelen, new Vector2(toRespawnRectangle.Center.X - TextureManager.Svartadelen.Width - 100, toRespawnRectangle.Center.Y - 760), Color.White);

            if (currentLevel != Level.Level4)
            {
                IngameTextmanager.DrawReturn(spriteBatch);
            }

            spriteBatch.End();
        }

        public void Level2Update(GameTime gameTime)
        {
            foreach (ArmMonster armMonster in levelManager.armMonsterList)
            {
                armMonster.Update(gameTime, player);
                ArmMonsterCollision(gameTime, armMonster);
            }

            foreach (GlitchMonster glitchMonster in levelManager.glitchMonsterList)
            {
                glitchMonster.Update(gameTime, player);
                GlitchMonsterCollision(gameTime, glitchMonster);
            }
            foreach (Stalker stalker in levelManager.stalkerList)
            {
                stalker.Update(gameTime, player);
                StalkerCollision(gameTime, stalker);
            }


        }

        public void Level3Update(GameTime gameTime)
        {
            foreach (ArmMonster armMonster in levelManager.armMonsterList)
            {
                armMonster.Update(gameTime, player);
                ArmMonsterCollision(gameTime, armMonster);
            }
            foreach (Imbaku imbaku in levelManager.imbakuList)
            {
                imbaku.Update(gameTime, player);
                ImbakuCollision(gameTime, imbaku);

                foreach (MiniMonster miniMonster in imbaku.miniMonsterList)
                {
                    miniMonster.Update(gameTime, player);
                }

                MiniCollision(levelManager, imbaku);
            }
            foreach (Golem golem in levelManager.golemList)
            {
                golem.Update(gameTime, player);
                GolemCollision(gameTime, golem);
            }

            sfx.GolemSong(gameTime);
        }

        public void Level4Update(GameTime gameTime)
        {
            foreach (Golem golem in levelManager.golemList)
            {
                golem.Update(gameTime, player);
                GolemCollision(gameTime, golem);

            }
            foreach (WallMonster wallMonster in levelManager.wallMonsterList)
            {
                wallMonster.Update(gameTime, player);
                WallMonsterCollision(wallMonster);
            }
            foreach (GlitchMonster glitchMonster in levelManager.glitchMonsterList)
            {
                glitchMonster.Update(gameTime, player);
                GlitchMonsterCollision(gameTime, glitchMonster);
            }

            foreach (Stalker stalker in levelManager.stalkerList)
            {
                stalker.Update(gameTime, player);
                StalkerCollision(gameTime, stalker);
            }

            sfx.GolemSong(gameTime);
        }

        public void ChangeLevel()
        {

            if (player.collectibles.Count == 1)
            {
                currentLevel = Level.Level2;
            }
            else if (player.collectibles.Count == 3)
            {
                currentLevel = Level.Level3;
            }
            else if (player.collectibles.Count == 6)
            {
                currentLevel = Level.Level4;
            }
            else if (player.collectibles.Count == 9)
            {
                GameStateManager.currentGameState = GameStateManager.GameState.Cutscene;

            }


            switch (currentLevel)
            {
                case Level.Level1:
                    LoadLevel1(levelManager);
                    break;
                case Level.Level2:
                    LoadLevel2(levelManager);
                    break;
                case Level.Level3:
                    LoadLevel3(levelManager);
                    break;
                case Level.Level4:
                    LoadLevel4(levelManager);
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

            if (player.weaponHitbox.Intersects(wallMonster.hitbox) && wallMonster.active && player.currentWeapon.color == Color.Red)
            {
                sfx.LightMonsterCollision();
                wallMonster.attackTimer.Start();

                if (wallMonster.attackTimer.ElapsedMilliseconds >= 3000)
                {
                    wallMonster.coolDown = true;
                    wallMonster.attackTimer.Reset();


                }

            }

        }

        public void StalkerCollision(GameTime gameTime, Stalker stalker)
        {
            if (player.middleHitbox.Intersects(stalker.stalkerRectangleHitbox) && !stalker.stalkerStunned && !player.playerImmunity)
            {
                PlayerDamage(stalker.monsterDamage);
            }

            if (Vector2.Distance(player.Position, stalker.Position) < 1500)
            {
                int stalkerPlayerDistance = (int)Math.Truncate(Vector2.Distance(player.Position, stalker.Position));

                int lightFail = stalker.stalkerRandom.Next(0, stalkerPlayerDistance / 5);

                if (lightFail == 0 && player.currentWeapon.enabled == true && !stalker.stalkerStunned)
                {
                    stalker.stalkerFlashTimer.Start();
                    player.currentWeapon.enabled = false;

                }

                if (stalker.stalkerFlashTimer.ElapsedMilliseconds > 100)
                {
                    player.currentWeapon.enabled = true;
                    stalker.stalkerFlashTimer.Reset();
                }
            }
            if (Vector2.Distance(player.Position, stalker.Position) < 600 && !player.insaferoom)
            {
                sfx.StalkerEncounter();
            }

            if (player.weaponHitbox.Intersects(stalker.stalkerCircleHitbox) && player.currentWeapon.color == Color.Goldenrod)
            {
                stalker.stalkerStunned = true;
            }

            if ((!player.weaponHitbox.Intersects(stalker.stalkerCircleHitbox) || player.currentWeapon.color != Color.Goldenrod) && stalker.stalkerStunned)
            {
                stalker.stalkerStunnedTimer.Start();
            }

            if (stalker.stalkerStunnedTimer.ElapsedMilliseconds > 2000)
            {
                stalker.stalkerStunnedTimer.Reset();
                stalker.stalkerStunned = false;
            }
        }

        public void GolemCollision(GameTime gameTime, Golem golem)
        {

            if (player.weaponHitbox.Intersects(golem.golemCircleHitbox) && player.currentWeapon.enabled == true)
            {
                if (golem.isSleeping)
                {
                    golem.isActive = true;
                    sfx.GolemEncounter(gameTime);
                }
            }

            else
            {
                golem.isActive = false;
            }

            if (Vector2.Distance(player.Position, golem.Position) <= 800 && !golem.isActive)
            {
                golem.isSleeping = true;
            }
            else
            {
                golem.isSleeping = false;
            }


        }

        public void GlitchMonsterCollision(GameTime gameTime, GlitchMonster glitchMonster)
        {
            if (player.middleHitbox.Intersects(glitchMonster.glitchMonsterRectangleHitbox))
            {
                player.isInverse = true;
                sfx.GlitchMonsterEncounter(gameTime);
            }

            if (player.isInverse)
            {
                glitchMonster.glitchMonsterTimer.Start();

                if (X.IsKeyPressed(Keys.E) && player.currentWeapon.enabled == true)
                {
                    player.isInverse = false;
                    glitchMonster.glitchMonsterTimer.Reset();
                    sfx.GlitchMonsterStop();
                }
                else if (glitchMonster.glitchMonsterTimer.ElapsedMilliseconds >= 4000)
                {
                    player.isInverse = false;
                    glitchMonster.glitchMonsterTimer.Reset();
                }
            }
        }

        public void ArmMonsterCollision(GameTime gameTime, ArmMonster armMonster)
        {
            if (player.middleHitbox.Intersects(armMonster.armMonsterRectangleHitbox) && !armMonster.coolDown)
            {
                if (armMonster.isActive)
                {
                    //DAMAGE
                }
                else
                {
                    armMonster.activated = true;
                    armMonster.coolDown = true;
                }
            }

            if (!player.middleHitbox.Intersects(armMonster.armMonsterRectangleHitbox) && armMonster.coolDown)
            {
                armMonster.coolDown = false;
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

        //Monster respawn för varje bana
        public void ResetMonsterPosition()
        {

        }



        public void ImbakuCollision(GameTime gameTime, Imbaku imbaku)
        {
            if (player.middleHitbox.Intersects(imbaku.imbakuRectangleHitbox))
            {
                //DAMAGE
                PlayerDamage(imbaku.monsterDamage);
            }

            if (Vector2.Distance(player.middleHitbox.Center.ToVector2(), imbaku.imbakuCircleHitbox.Center) < ConstantValues.tileHeight * 2.5
                && player.currentWeapon.enabled && !X.player.insaferoom)
            {
                sfx.ImbakuEncounter();
            }
            //INTE LÄNGRE KONSTIGT
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

            if (Vector2.Distance(player.Position, imbaku.Position) > 2500 || Vector2.Distance(player.Position, imbaku.Position) < 800)
            {
                imbaku.isChasing = true;
            }

        }

        public void MiniCollision(LevelManager levelManager, Imbaku imbaku)
        {
            List<MiniMonster> miniMonsterToRemove = new List<MiniMonster>();

            foreach (MiniMonster mini in imbaku.miniMonsterList)
            {
                if (player.middleHitbox.Intersects(mini.miniRectangleHitbox) || Vector2.Distance(player.Position, mini.Position) > 650)
                {
                    miniMonsterToRemove.Add(mini);
                }

                for (int i = 0; i < levelManager.Tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < levelManager.Tiles.GetLength(1); j++)
                    {
                        if (levelManager.Tiles[i, j].IsWall)
                        {
                            if (mini.miniRectangleHitbox.Intersects(levelManager.Tiles[i, j].Hitbox))
                            {
                                miniMonsterToRemove.Add(mini);
                            }
                        }
                    }
                }

                if (player.weaponHitbox.Intersects(mini.miniCircleHitbox) && player.currentWeapon.color == Color.Red)
                {
                    mini.health--;
                    if (mini.health <= 0)
                    {
                        miniMonsterToRemove.Add(mini);
                        break;
                    }
                }
            }

            foreach (MiniMonster miniRemove in miniMonsterToRemove)
            {
                imbaku.miniMonsterList.Remove(miniRemove);

            }
        }
        //SPELAREN SKADAS
        public void PlayerDamage(int monsterDamage)
        {
            sfx.GetHit();
            player.playerHealth = player.playerHealth - monsterDamage;
            player.playerImmunity = true;

            if (player.playerHealth <= 0)
            {
                killed = true;
            }
        }

        public void IngameTextShowing(GameTime gameTime)
        {
            if (player.viewCollectible == false)
            {
                ingameTextmanager.CheckProgression(gameTime);
            }
        }

        public void DrawHPWarning(SpriteBatch spriteBatch)
        {
            if (player.playerHealth == 1)
            {
                spriteBatch.Draw(TextureManager.RedHPTexture, new Vector2(player.Position.X - 960, player.Position.Y - 540), Color.White);
            }
        }

    }
}
