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
        private Stopwatch musicTimer = new Stopwatch();

        private Random random;
        private float armSpeed = 25f, maxSpeed = 250f;
        private int spawning;
        public bool coolDown, activated, isActive, slowedDown,pathfindingActivated;

        SFX sfx;

        public ArmMonster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            frameSize = 0;
            color = Color.White;
            currentSourceRect = new Rectangle(frame, frameSize, ConstantValues.tileWidth, ConstantValues.tileHeight);

            sfx = new SFX();

            armMonsterRectangleHitbox = new Rectangle((int)position.X + currentSourceRect.Width/4, (int)position.Y+currentSourceRect.Height/4, currentSourceRect.Width/2, currentSourceRect.Height/2);
            armMonsterCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            armMonsterCircleHitbox = new Circle(armMonsterCircleHitboxPos, 90f);

            random = new Random();
            activated = false;
            coolDown = false;
            isActive = false;
            slowedDown = false;
            pathfindingActivated = false;

        }

        public new void Update(GameTime gameTime, Player player)
        {
            chaseTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            currentSourceRect.X = frame * ConstantValues.tileWidth;
            currentSourceRect.Y = frameSize * ConstantValues.tileHeight;

            armMonsterRectangleHitbox.X = (int)position.X + currentSourceRect.Width / 4;
            armMonsterRectangleHitbox.Y = (int)position.Y + currentSourceRect.Height / 4;

            if (chaseTimer < 0)
            {
                path = Pathfind.CreatePath(Position, player.playerHitbox.Center.ToVector2());
                chaseTimer = resetTimer;
            }

            ActivatingArmMonster(gameTime, player);
            UpdateSourceRectangle();
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
            activated = false;
            coolDown = false;
            moving = false;
            musicTimer.Reset();
            activationTimer.Reset();
            cooldownTimer.Reset();

            armSpeed = 25f;
            SetPosition(levelManager.ArmMonsterStartPosition);


            armMonsterRectangleHitbox.X = (int)position.X + currentSourceRect.Width / 4;
            armMonsterRectangleHitbox.Y = (int)position.Y + currentSourceRect.Height / 4;

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
                    pathfindingActivated = true;
                    activationTimer.Start();
                    musicTimer.Start();

                    if(musicTimer.ElapsedMilliseconds >= 900)
                    {
                        sfx.ArmMonsterEncounter(gameTime);
                        musicTimer.Reset();
                    }

                    if (activationTimer.ElapsedMilliseconds >= 2500)
                    {
                        if(pathfindingActivated)
                        {
                            ArmMonsterPathfinding(gameTime, player);
                            pathfindingActivated = false;
                        }

                        isActive = true;
                        
                        Acceleration(gameTime);
                        CollisionWithWall(levelManager);

                        armMonsterRectangleHitbox.X = (int)position.X + currentSourceRect.Width / 4;
                        armMonsterRectangleHitbox.Y = (int)position.Y + currentSourceRect.Height / 4;

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
                    moving = false;
                }
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(texture, position, currentSourceRect, color);
            }
        }
    }
}