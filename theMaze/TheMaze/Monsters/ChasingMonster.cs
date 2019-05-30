using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class ChasingMonster : WanderingMonster
    {
        //A list that holds the path that will be used
        public List<Vector2> path;
        protected Vector2 newDirection;

        protected float chaseTimer = 0f, resetTimer = 300f;

        public ChasingMonster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            path = new List<Vector2>();
        }

        public override void Update(GameTime gameTime, Player player)
        {
            Animation(gameTime);
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected void Pathfinding(GameTime gameTime, Player player)
        {
            if (!Utility.player.insaferoom)
            {
                chaseTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (chaseTimer < 0)
                {
                    path = Pathfind.CreatePath(Position, player.FootHitbox.Center.ToVector2());
                    chaseTimer = resetTimer;
                }
                
                if (!moving)
                {
                    //Makes sure that the list "path" is not empty
                    if (path.Count != 0)
                    {
                        //"newDirection" calls for a method in "Pathfind" that returns a vector where x and y is either 1 or 0
                        newDirection = Pathfind.SetDirectionFromNextPosition(Position, path.First());
                        //Uses the same method that is used for random movement
                        ChangeDirection(newDirection);
                    }
                }

                else
                {
                    position += Direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (Vector2.Distance(Position, destination) < 1)
                    {
                        position = destination;
                        moving = false;
                        //Checks again if the list "path" is not empty and the removes the first element of the list
                        //By doing so the method "path.First()" can be called again with the next value of the list
                        if (path.Count != 0)
                        {
                            path.RemoveAt(0);
                        }
                    }
                }
            }
        }
    }
}
