using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class ArmMonster : Monster
    {
        private Random random;
        private bool toActivate = false;
        public bool activated = false;
        public bool slowedDown = false;
        public Rectangle armMonsterRectangleHitbox;
        public Circle armMonsterCircleHitbox;
        private bool cooldown = false;
        private Stopwatch cooldownTimer = new Stopwatch();
        private Stopwatch activationTimer = new Stopwatch();
        private Stopwatch accelerationTimer = new Stopwatch();
        private List<Vector2> path;
        private Vector2 newDirection, armMonsterCircleHitboxPos;
        private float chaseTimer = 0f;
        private float resetTimer = 300f;
        private float armSpeed = 25f, maxSpeed = 250f;

        public ArmMonster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            random = new Random();

            armMonsterRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, ConstantValues.tileWidth, ConstantValues.tileHeight);

            armMonsterCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            armMonsterCircleHitbox = new Circle(armMonsterCircleHitboxPos, 90f);
        }

        public override void Update(GameTime gameTime, Player player)
        {
            chaseTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (chaseTimer < 0)
            {
                path = Pathfind.CreatePath(Position, player.playerHitbox.Center.ToVector2());
                chaseTimer = resetTimer;
            }

            if (toActivate)
            {
                activationTimer.Start();
                if (activationTimer.ElapsedMilliseconds >= 2500)
                {
                    activated = true;

                    armMonsterRectangleHitbox.X = (int)position.X;
                    armMonsterRectangleHitbox.Y = (int)position.Y;

                    armMonsterCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
                    armMonsterCircleHitbox = new Circle(armMonsterCircleHitboxPos, 90f);

                    Pathfinding(gameTime);
                    Acceleration(gameTime);
                    Collision(levelManager);
                } 
            }
        }

        public void Activating(GameTime gameTime)
        {
            if (cooldown == false)
            {
                cooldown = true;
                Cooldown(gameTime);
                int ifActivating = random.Next(0, 0);

                if (ifActivating == 0)
                {
                    toActivate = true; 
                }
            }
        }

        private void Cooldown(GameTime gameTime)
        {
            cooldownTimer.Start();
            if (cooldownTimer.ElapsedMilliseconds >= 2000)
            {
                cooldown = false;
                cooldownTimer.Reset();
            }

        }

        private void Acceleration(GameTime gameTime)
        {
            accelerationTimer.Start();
            if (accelerationTimer.ElapsedMilliseconds >= 250)
            {
                if ((armSpeed < maxSpeed) || !slowedDown)
                {
                    armSpeed = armSpeed + 25f;
                }
                else if (slowedDown)
                {
                    armSpeed = armSpeed - 50f;
                }
                accelerationTimer.Reset();
            }
        }

        public void ResetArmMonster()
        {
            toActivate = false;
            activated = false;
            activationTimer.Reset();
            cooldownTimer.Reset();
            armSpeed = 25f;
            SetPosition(levelManager.ArmMonsterStartPosition);
        }

        /*private void ChaseDirection(GameTime gameTime)
        {

            //koll så att listan med "the path" inte är tom för att motverka att programmet kraschar
            if (path.Count != 0)
            {
                //newDirection kallar på en metod i Pathfind som ger en vector där x och y antingen är 1 eller 0
                newDirection = Pathfind.SetDirectionFromNextPosition(Position, path.First());
                //använder metoden som random movement också använder
                ChangeDirection(newDirection);

            }

            position += direction * armSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

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
        }*/

        public void Collision(LevelManager levelManager)
        {
            for (int i = 0; i < levelManager.Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < levelManager.Tiles.GetLength(1); j++)
                {
                    if (levelManager.Tiles[i, j].IsWall)
                    {
                        if (armMonsterRectangleHitbox.Intersects(levelManager.Tiles[i, j].Hitbox))
                        {
                            ResetArmMonster();
                        }
                    }
                }
            }
        }

        private void Pathfinding(GameTime gameTime)
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
                position += direction * armSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (activated)
            {
                spriteBatch.Draw(texture, armMonsterRectangleHitbox, currentSourceRect, Color.Red);
            }
        }
    }
}
