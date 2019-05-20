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
    public class ArmMonster : ChasingMonster
    {
        public Rectangle armMonsterRectangleHitbox;
        public Circle armMonsterCircleHitbox;
        private Vector2 armMonsterCircleHitboxPos;

        private Stopwatch cooldownTimer = new Stopwatch();
        private Stopwatch activationTimer = new Stopwatch();
        private Stopwatch accelerationTimer = new Stopwatch();

        private Random random;
        private float armSpeed = 25f, maxSpeed = 250f;
        private int spawning;
        public bool coolDown, activated, isActive, slowedDown;


        public ArmMonster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            frameSize = 0;

            armMonsterRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, currentSourceRect.Width, currentSourceRect.Height);
            armMonsterCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            armMonsterCircleHitbox = new Circle(armMonsterCircleHitboxPos, 90f);

            random = new Random();
            activated = false;
            coolDown = false;
            isActive = false;
            slowedDown = false;
        }

        public new void Update(GameTime gameTime, Player player)
        {
            chaseTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (chaseTimer < 0)
            {
                path = Pathfind.CreatePath(Position, player.playerHitbox.Center.ToVector2());
                chaseTimer = resetTimer;
            }

            ActivatingArmMonster(gameTime, player);

        }

        private void Acceleration(GameTime gameTime)
        {
            accelerationTimer.Start();

            if (accelerationTimer.ElapsedMilliseconds >= 250)
            {
                if ((armSpeed < maxSpeed) && !slowedDown)
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
            isActive = false;
            coolDown = false;
            activationTimer.Reset();
            cooldownTimer.Reset();
            armSpeed = 25f;
            SetPosition(levelManager.ArmMonsterStartPosition);

            armMonsterRectangleHitbox.X = (int)position.X;
            armMonsterRectangleHitbox.Y = (int)position.Y;

            armMonsterCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            armMonsterCircleHitbox = new Circle(armMonsterCircleHitboxPos, 90f);
        }

        public void CollisionWithWall(LevelManager levelManager)
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

        private void ActivatingArmMonster(GameTime gameTime, Player player)
        {
            if (activated)
            {
                spawning = random.Next(0, 0);

                if (spawning == 0)
                {
                    activationTimer.Start();
                    if (activationTimer.ElapsedMilliseconds >= 2500)
                    {
                        isActive = true;
                        ArmMonsterPathfinding(gameTime, player);
                        Acceleration(gameTime);
                        CollisionWithWall(levelManager);

                        armMonsterRectangleHitbox.X = (int)position.X;
                        armMonsterRectangleHitbox.Y = (int)position.Y;

                        armMonsterCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
                        armMonsterCircleHitbox = new Circle(armMonsterCircleHitboxPos, 90f);

                    }

                }

            }


        }

        private void ArmMonsterPathfinding(GameTime gameTime, Player player)
        {
            if (!moving)
            {
                //koll så att listan med "the path" inte är tom för att motverka att programmet kraschar
                if (path.Count > 1)
                {
                    //newDirection kallar på en metod i Pathfind som ger en vector där x och y antingen är 1 eller 0
                    newDirection = Pathfind.SetDirectionFromNextPosition(Position, path[1]);

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

                }
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(texture, armMonsterRectangleHitbox, currentSourceRect, Color.Red);
            }
        }
    }
}