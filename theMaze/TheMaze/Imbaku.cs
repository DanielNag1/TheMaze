using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    //DANIELS FASZ-KOD

    public class Imbaku : Monster
    {
        public Rectangle imbakuRectangleHitbox;
        //En lista som håller "the path"
        public List<Vector2> path;
        public bool active;
        Vector2 newDirection,imbakuCircleHitboxPos;
        private float chaseTimer = 0f, resetTimer = 300f;
        public Circle imbakuCircleHitbox;
        
        public Imbaku(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            frameSize = 0;
            currentSourceRect = new Rectangle(frame, frameSize, 304, 462);

            nrFrames = 5;
            timeIntervall = 120;

            imbakuRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, 304 - ConstantValues.tileWidth, 462 - ConstantValues.tileHeight-50);

            imbakuCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            imbakuCircleHitbox = new Circle(imbakuCircleHitboxPos, 90f);

            path = new List<Vector2>();
            isAlive = true;
            health = 3000;
            speed = 50f;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if(health<=0)
            {
                isAlive = false;
            }

            if (isAlive)
            {
                currentSourceRect.X = frame * 290;
                currentSourceRect.Y = frameSize * 462;

                hitboxPos = new Vector2(imbakuRectangleHitbox.X, imbakuRectangleHitbox.Y);

                imbakuCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
                imbakuCircleHitbox = new Circle(imbakuCircleHitboxPos, 90f);

                imbakuRectangleHitbox.X = (int)position.X - currentSourceRect.Width / 4 + 50;
                imbakuRectangleHitbox.Y = (int)position.Y - currentSourceRect.Height / 4 - 54;


                timer -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer <= 0)
                {
                    timer = timeIntervall;
                    frame++;
                    if (frame >= nrFrames)
                    {
                        frame = 0;
                    }

                }

                //tid som räknar ner för att inte köra patfindingen för ofta
                chaseTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (chaseTimer < 0)
                {
                    path = Pathfind.CreatePath(Position, player.playerHitbox.Center.ToVector2());
                    chaseTimer = resetTimer;
                }

                Pathfinding(gameTime);

                UpdateSourceRectangle();
            }
        }

        private void Pathfinding(GameTime gameTime)
        {
            if (active)
            {
                if (!moving)
                {
                    //koll så att listan med "the path" inte är tom för att motverka att programmet kraschar
                    if (path.Count != 0)
                    {
                        //newDirection kallar på en metod i Pathfind som ger en vector där x och y antingen är 1 eller 0
                        newDirection = Pathfind.SetDirectionFromNextPosition(Position, path.First());
                        //använder metoden som random movement också använder
                        ChangeDirection(newDirection);

                    }
                }

                else
                {
                    position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (Vector2.Distance(Position, destination) < 1)
                    {
                        position = destination;
                        moving = false;
                        //kollar igen så att listan med "the path" inte är tom för att motverka krasch
                        //tar sen bort första elementet i listan så att vi kan kalla på path.first() med nästa mål
                        if (path.Count != 0)
                        {
                            path.RemoveAt(0);
                        }
                    }
                }
            }
            else
            {
                Moving(gameTime);
            }

        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                //imbakuCircleHitbox.Draw(spriteBatch);
                //spriteBatch.Draw(TextureManager.RedTexture, imbakuRectangleHitbox, Color.White);
                spriteBatch.Draw(texture, imbakuRectangleHitbox, currentSourceRect, Color.White);
            }
        }

        protected void UpdateSourceRectangle()
        {
            if (newDirection == new Vector2(1, 0))
            {
                frameSize = 1;
            }
            if (newDirection == new Vector2(-1, 0))
            {
                frameSize = 0;
            }

            if (newDirection == new Vector2(0, 1))
            {
                frameSize = 2;
            }
        }
    }
}