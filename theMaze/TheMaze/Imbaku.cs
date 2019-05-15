using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{


    public class Imbaku : ChasingMonster
    {
        public Rectangle imbakuRectangleHitbox;

        public bool isActive, miniIsAlive, isChasing;
        Vector2 imbakuCircleHitboxPos;
        
        private float creatingMiniTimer = 0f, resetMiniTimer = 3f;
        public Circle imbakuCircleHitbox;

        public MiniMonster miniMonster;
        public List<MiniMonster> miniMonsterList;

        public int monsterDamage = 1;

        public Imbaku(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            frameSize = 0;
            currentSourceRect = new Rectangle(frame, frameSize, 125, 245);

            nrFrames = 5;

            imbakuRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, ConstantValues.tileWidth, ConstantValues.tileHeight + 120);

            imbakuCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            imbakuCircleHitbox = new Circle(imbakuCircleHitboxPos, 90f);

            path = new List<Vector2>();
            
            isActive = false;
            speed = 50f;

            miniMonsterList = new List<MiniMonster>();
            miniIsAlive = true;
        }

        public override void Update(GameTime gameTime, Player player)
        {

            currentSourceRect.X = frame * 125;
            currentSourceRect.Y = frameSize * 250;

            hitboxPos = new Vector2(imbakuRectangleHitbox.X, imbakuRectangleHitbox.Y);

            imbakuCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            imbakuCircleHitbox = new Circle(imbakuCircleHitboxPos, 90f);

            imbakuRectangleHitbox.X = (int)position.X - currentSourceRect.Width / 4 + 30;
            imbakuRectangleHitbox.Y = (int)position.Y - currentSourceRect.Height / 4 - 35;

            UpdateImbakuSourceRectangle();
            ImbakuStates(gameTime, player);
            

            foreach (MiniMonster mini in miniMonsterList)
            {
                mini.Update(gameTime, player);
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, imbakuRectangleHitbox, currentSourceRect, Color.White);

            foreach (MiniMonster mini in miniMonsterList)
            {
                mini.Draw(spriteBatch);
            }

        }

        protected void UpdateImbakuSourceRectangle()
        {
            if (isActive)
            {
                if (direction == new Vector2(1, 0))
                {
                    frameSize = 5;
                }
                if (direction == new Vector2(-1, 0))
                {
                    frameSize = 4;
                }

                if (direction == new Vector2(0, 1))
                {
                    frameSize = 6;
                }

                if (direction == new Vector2(0, -1))
                {
                    frameSize = 7;
                }

            }

        }


        protected void ImbakuStates(GameTime gameTime, Player player)
        {
            timer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (isActive)
            {
                if (timer <= 0)
                {
                    timer = timeIntervall;
                    frame++;
                    if (frame >= nrFrames)
                    {
                        frame = 5;
                    }

                }

                speed = 0;

                if (miniMonsterList.Count <= 4)
                {
                    CreateMini(gameTime);
                }

            }

            else
            {

                Animation(gameTime);
                //if (timer <= 0)
                //{
                //    timer = timeIntervall;
                //    frame++;
                //    if (frame >= nrFrames)
                //    {
                //        frame = 0;
                //    }

                //}

                speed = 50f;

            }

            if (isChasing)
            {
                Pathfinding(gameTime, player);
            }

            else
            {
                Moving(gameTime);
            }


        }

        


        protected void CreateMini(GameTime gameTime)
        {
            for (int i = 0; i < 5; i++)
            {
                creatingMiniTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (creatingMiniTimer <= 0)
                {
                    miniMonster = new MiniMonster(TextureManager.MiniMonsterTex, position, levelManager);
                    miniMonsterList.Add(miniMonster);
                    Console.WriteLine(miniMonster.speed);
                    creatingMiniTimer = resetMiniTimer;
                }

            }

        }


    }
}