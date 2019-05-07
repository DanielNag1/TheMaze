using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System.Diagnostics;

namespace TheMaze
{
    public class GamePlayManager
    {
        enum LevelState {Live,Death}
        LevelState currentState=LevelState.Live;
        LevelManager levelManager,deathManager;
        Player player;
        Saferoom saferoom;
        Monster glitchMonster;
        Stopwatch timer;
        Imbaku imbaku;
        WallMonster wallMonster;


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
            saferoom = new Saferoom();

            imbaku = new Imbaku(TextureManager.Monster2Tex, levelManager.StartPositionMonster, levelManager);
            glitchMonster = new GlitchMonster(TextureManager.MonsterTex, levelManager.StartPositionMonster, levelManager);
            timer = new Stopwatch();

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
            saferoom.Update(gameTime);
            imbaku.Update(gameTime);
            glitchMonster.Update(gameTime);

                        foreach (WallMonster wM in tileManager.wallMonsters)
                        {
                            wM.Update(gameTime);
                            WallMonsterCollision(wM);
                        }

                        SafeRoomInteraction();

                        GlitchMonsterCollision();
                        ImbakuCollision();
                        MonsterLightCollision(gameTime);
                        ImbakuFacePlayer();

            if (X.IsKeyPressed(Keys.Enter))
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Game1.penumbra.BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            tileManager.Draw(spriteBatch);
            //monster.Draw(spriteBatch);
            imbaku.Draw(spriteBatch);
            glitchMonster.Draw(spriteBatch);

            foreach (WallMonster wM in tileManager.wallMonsters)
            {
                wM.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);
            saferoom.Draw(spriteBatch);
            spriteBatch.End();

            Game1.penumbra.Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);
            //particleEngine.Draw(spriteBatch);

            if (player.Direction == new Vector2(0, 1) && lights.spotLight.Enabled == true)
            { spriteBatch.Draw(TextureManager.FlareTex, lights.lampPos, Color.White); }

            lights.DrawHitBox(spriteBatch);
            spriteBatch.Draw(TextureManager.FlareTex, mouseRect, Color.White);
            spriteBatch.End();
        }

        public void MonsterLightCollision(GameTime gameTime)
        {
            if (lights.CollisionWithLight(monster.hitbox))
            {
                int x = random.Next(0, 3);
                if (x == 1 && monster.isAlive)
                {
                    //particleEngine.EmitterLocation = monster.hitboxPos;
                }
                monster.health -= gameTime.ElapsedGameTime.TotalSeconds;

                if (monster.health <= 0)
                {
                    monster.isAlive = false;
                }

                monster.speed = 50f;

                monster.color = new Color(Color.Red, 0);
            }
            else
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
        }

        public void SafeRoomInteraction()
        {

            if (player.Hitbox.Intersects(saferoom.rectangle))
            {
                isMouseVisible = true;
                saferoom.visible = true;
                lights.playerLight.Enabled = false;
                lights.spotLight.Enabled = false;
                lights.canChangeWeapon = false;
                ChooseWeapon();

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

        public void GlitchMonsterCollision()
        {
            if (Vector2.Distance(player.hitBoxPos, glitchMonster.hitboxPos) <= 200)
            {
                glitchMonster.color = Color.Blue;
                player.isInverse = true;
                lights.isInverse = true;
            }

            if (player.isInverse && lights.isInverse)
            {
                timer.Start();

                if (lights.spotLight.Enabled == false)
                {
                    glitchMonster.color = Color.White;
                    player.isInverse = false;
                    lights.isInverse = false;
                    timer.Reset();
                }
                else if (timer.ElapsedMilliseconds >= 4000)
                {

                }
            }


        }

        public void ImbakuCollision()
        {
            if (Vector2.Distance(player.hitBoxPos, imbaku.hitboxPos) <= 800)
            {
                imbaku.speed = 0;
            }

            else
            {
                imbaku.speed = 100;
            }

        }

        public void ImbakuFacePlayer()
        {
            if (player.hitBoxPos.X > imbaku.hitboxPos.X)
            {
                imbaku.frameSize = 1;
            }

            else if (player.hitBoxPos.X < imbaku.hitboxPos.X)
            {
                imbaku.frameSize = 0;
            }

            else if (player.hitBoxPos.Y > imbaku.hitboxPos.Y)
            {
                imbaku.frameSize = 2;
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

            if (lights.attackhitbox.Intersects(wallMonster.hitbox) && wallMonster.active)
            {

                timer.Start();

                if (timer.ElapsedMilliseconds >= 3000)
                {
                    wallMonster.coolDown = true;
                    timer.Reset();
                }}}
    }
}
