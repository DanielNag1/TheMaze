using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class PathfindMob
    {
        public Vector2 Position { get; private set; }
        private Texture2D texture;

        LevelManager levelManager;

        //En lista som håller "the path"
        private List<Vector2> path;

        private Vector2 direction, destination;

        private float speed = 100;
        private bool moving = false;

        private float timer = 0f, resetTimer = 700f;


        public PathfindMob(LevelManager levelManager, Vector2 startPosition)
        {
            Position = startPosition;
            //texture = TextureManager.CatTex;

            this.levelManager = levelManager;

            path = new List<Vector2>();
        }

        public void Update(GameTime gameTime, Player player)
        {
            //tid som räknar ner för att inte köra patfindingen för ofta
            timer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer < 0)
            {
                path = Pathfind.CreatePath(Position, player.Hitbox.Center.ToVector2());
                timer = resetTimer;
            }

            Moving(gameTime);
        }

        private void Moving(GameTime gameTime)
        {
            if (!moving)
            {
                //koll så att listan med "the path" inte är tom för att motverka att programmet kraschar
                if (path.Count != 0)
                {
                    //newDirection kallar på en metod i Pathfind som ger en vector där x och y antingen är 1 eller 0
                    Vector2 newDirection = Pathfind.SetDirectionFromNextPosition(Position, path.First());
                    //använder metoden som random movement också använder
                    ChangeDirection(newDirection);
                }
            }

            else
            {
                Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Vector2.Distance(Position, destination) < 1)
                {
                    Position = destination;
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

        private void ChangeDirection(Vector2 newDirection)
        {
            direction = newDirection;
            Vector2 newDestination = Position + direction * ConstantValues.tileWidth;

            Tile tile = levelManager.GetTileAtPosition(direction);
            if (tile.IsWall)
            {
                destination = newDestination;
                moving = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y,
                ConstantValues.tileWidth, ConstantValues.tileHeight), Color.White);
        }
    }
}
