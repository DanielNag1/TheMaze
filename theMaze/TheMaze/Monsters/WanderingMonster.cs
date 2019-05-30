using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class WanderingMonster : Monster
    {
        protected LevelManager levelManager;

        private readonly Vector2 Up = new Vector2(0, -1);
        private readonly Vector2 Down = new Vector2(0, 1);
        private readonly Vector2 Left = new Vector2(-1, 0);
        private readonly Vector2 Right = new Vector2(1, 0);
        
        private Random random;

        protected bool moving;

        public WanderingMonster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position)
        {
            this.levelManager = levelManager;
            
            random = new Random();

            speed = 50f;
            moving = false;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime, player);
            Moving(gameTime);
            UpdateSourceRectangle();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        protected void Moving(GameTime gameTime)
        {
            if (!moving)
            {
                NewDirection();
                ChangeDirection(Direction);
            }

            else
            {
                position += Direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Vector2.Distance(Position, destination) < 1)
                {
                    position = destination;
                    moving = false;
                }
            }
        }

        protected void ChangeDirection(Vector2 newDirection)
        {
            Direction = newDirection;
            Vector2 newDestination = Position + Direction * ConstantValues.tileWidth;

            Tile tile = levelManager.GetTileAtPosition(Direction);
            if (tile.IsWall)
            {
                destination = newDestination;
                moving = true;
            }
        }

        protected void NewDirection()
        {
            //Saves the diffrent directions in an array
            Vector2[] directions = new[] { Up, Down, Left, Right };

            //Creates a new empty list
            List<Vector2> possibleDirections = new List<Vector2>();

            //Loops through all directions in "directions"
            foreach (Vector2 direction in directions)
            {
                //Check what kind if "Tile" it is
                Tile tile = levelManager.GetTileAtPosition(position +
                    new Vector2(direction.X * ConstantValues.tileWidth, direction.Y * ConstantValues.tileHeight));

                //Checks if the "Tile" is a wall
                if (!tile.IsWall)
                {
                    if (!tile.IsEntrance)
                    {
                        possibleDirections.Add(direction);
                    }
                    //If it is not a wall it is added to the list of possible directions
                }
            }
            
            //If there is more than two possible moves, remove the last one and invert it so that it does not go backwards
            if (possibleDirections.Count > 1)
            {
                possibleDirections.Remove(-Direction);
            }

            //Pick a random move from the existing ones
            Direction = possibleDirections[random.Next(0, possibleDirections.Count)];
        }
    }
}
