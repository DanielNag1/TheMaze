using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class ChasingMonster:WanderingMonster
    {
        //En lista som håller "the path"
        public List<Vector2> path;
        Vector2 newDirection;

        protected float chaseTimer = 0f, resetTimer = 300f;

        public ChasingMonster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            path = new List<Vector2>();

        }

        public virtual void Update(GameTime gameTime, Player player)
        {
            Animation(gameTime);

        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected void Pathfinding(GameTime gameTime, Player player)
        {
            chaseTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (chaseTimer < 0)
            {
                path = Pathfind.CreatePath(Position, player.playerHitbox.Center.ToVector2());
                chaseTimer = resetTimer;
            }


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

        


    }
}
