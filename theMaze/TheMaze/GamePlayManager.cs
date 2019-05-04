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
    class GamePlayManager
    {
        TileManager tileManager;
        Player player;
        Saferoom saferoom;
        Monster monster, glitchMonster;
        Lights lights;
        Camera camera;
        Circle attackhitbox;
        ParticleEngine particleEngine,particleEngine2;
        Random random;

        Stopwatch timer;
        Imbaku imbaku;
        WallMonster wallMonster;
        public static MouseState mouse;
        public  Vector2 mousePos;
        public Rectangle mouseRect;
        Color selectedColor;
        public bool isMouseVisible;

        public GamePlayManager(GraphicsDevice graphicsDevice)
        {
            
            tileManager = new TileManager();
            player = new Player(TextureManager.CatTex, tileManager.StartPositionPlayer);
            monster = new Monster(TextureManager.MonsterTex, tileManager.StartPositionMonster, tileManager);
            imbaku = new Imbaku(TextureManager.Monster2Tex, tileManager.StartPositionMonster, tileManager);
            glitchMonster = new GlitchMonster(TextureManager.MonsterTex, tileManager.StartPositionMonster, tileManager);
            camera = new Camera(Game1.graphics.GraphicsDevice.Viewport);
            lights = new Lights(player, camera);
            saferoom = new Saferoom();
            timer = new Stopwatch();
            

            Game1.penumbra.Lights.Add(lights.spotLight);
            Game1.penumbra.Lights.Add(lights.playerLight);
            Game1.penumbra.Initialize();

            lights.spotLight.ShadowType = Penumbra.ShadowType.Occluded;
            //particleEngine = new ParticleEngine(TextureManager.particleTextures, monster.hitboxPos);
            particleEngine2 = new ParticleEngine(TextureManager.particleTextures, Vector2.Zero);
            random = new Random();

            Game1.penumbra.Transform = camera.Transform;
            attackhitbox = new Circle(lights.worldMouse, 40f);
            
            foreach (Tile t in tileManager.Tiles)
            {
                if (t.IsHull == true)
                {
                    Game1.penumbra.Hulls.Add(Tile.HullFromRectangle(t.HullHitbox, 1));
                }
            }
            
        }

        public void Update(GameTime gameTime)
        {
            player.Collision(tileManager);
            player.Update(gameTime);
            saferoom.Update(gameTime);
            imbaku.Update(gameTime);
            glitchMonster.Update(gameTime);
            SafeRoomInteraction();

            GlitchMonsterCollision();
            ImbakuCollision();
            monster.Update(gameTime);
            MonsterLightCollision(gameTime);
            MonsterFacePlayer();

            //particleEngine.Update(gameTime);
            
            camera.SetPosition(player.Position);
            Game1.penumbra.Transform = camera.Transform;
            lights.Update(gameTime);
            

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Game1.penumbra.BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            tileManager.Draw(spriteBatch);
            //monster.Draw(spriteBatch);
            imbaku.Draw(spriteBatch);
            glitchMonster.Draw(spriteBatch);
            player.Draw(spriteBatch);
            saferoom.Draw(spriteBatch);
            spriteBatch.End();

            Game1.penumbra.Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);
            //particleEngine.Draw(spriteBatch);

            if (player.Direction == new Vector2(0, 1) && lights.spotLight.Enabled == true)
            { spriteBatch.Draw(TextureManager.FlareTex, lights.lampPos, Color.White); }

            lights.DrawHitBox(spriteBatch);
            spriteBatch.Draw(TextureManager.FlareTex,mouseRect,Color.White);
            spriteBatch.End();
        }

        public void MonsterLightCollision(GameTime gameTime)
        {
            if (lights.CollisionWithLight(monster.hitbox))
            {
                int x = random.Next(0, 3);
                if(x==1 && monster.isAlive)
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
                monster.speed = 100f;
                if (monster.health >= 2.5f)
                {
                    monster.color = Color.White;
                }
                else if (monster.health <= 2.5f)
                {
                    monster.color = new Color(100, 100, 100, 100);
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

                lights.weapon1Power = .9f;
                lights.weapon2Power = .9f;
                lights.weapon3Power = .9f;
                lights.weapon4Power = .9f;
            }
            else
            {
                isMouseVisible = false;
                saferoom.visible = false;
                lights.canChangeWeapon = true;
                lights.playerLight.Color = Color.White;
            }

        }

        public void ChooseWeapon()
        {
            if (lights.mouseRect.Intersects(saferoom.attackLight1rectangle))
            {
                selectedColor = Color.Red;

                {
                    if(Keyboard.GetState().IsKeyDown(Keys.D2))
                    {
                        lights.weapon2.Color = selectedColor;
                        lights.playerLight.Color = selectedColor;
                        lights.playerLight.Enabled = true;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D3))
                    {
                        lights.weapon3.Color = selectedColor;
                        lights.playerLight.Color = selectedColor;
                        lights.playerLight.Enabled = true;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D4))
                    {
                        lights.weapon4.Color = selectedColor;
                        lights.playerLight.Color = selectedColor;
                        lights.playerLight.Enabled = true;
                    }
                }
                

            }
            if (lights.mouseRect.Intersects(saferoom.attackLight2rectangle))
            {
                selectedColor = Color.Yellow;

                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    lights.weapon2.Color = selectedColor;
                    lights.playerLight.Color = selectedColor;
                    lights.playerLight.Enabled = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D3))
                {
                    lights.weapon3.Color = selectedColor;
                    lights.playerLight.Color = selectedColor;
                    lights.playerLight.Enabled = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D4))
                {
                    lights.weapon4.Color = selectedColor;
                    lights.playerLight.Color = selectedColor;
                    lights.playerLight.Enabled = true;
                }
            }
            if (lights.mouseRect.Intersects(saferoom.attackLight3rectangle))
            {
                selectedColor = Color.Green;
                
                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    lights.weapon2.Color = selectedColor;
                    lights.playerLight.Color = selectedColor;
                    lights.playerLight.Enabled = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D3))
                {
                    lights.weapon3.Color = selectedColor;
                    lights.playerLight.Color = selectedColor;
                    lights.playerLight.Enabled = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D4))
                {
                    lights.weapon4.Color = selectedColor;
                    lights.playerLight.Color = selectedColor;
                    lights.playerLight.Enabled = true;
                }
                
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

            //Console.WriteLine(Vector2.Distance(player.Position, glitchMonster.hitboxPos));
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

        public void MonsterFacePlayer()
        {
            if (player.hitBoxPos.X > imbaku.hitboxPos.X)
            {
                imbaku.frameSize = 1;
            }

            else if (player.hitBoxPos.X < imbaku.hitboxPos.X)
            {
                imbaku.frameSize = 0;
            }
        }


    }
}
