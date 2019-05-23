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

            Vector2[] directions = new[] { Up, Down, Left, Right };

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
            //Array av de fyra olika riktingarna som heter "directions"
            Vector2[] directions = new[] { Up, Down, Left, Right };

            //En ny lista av Vector2 med namn "possibleDirections", som är tom
            List<Vector2> possibleDirections = new List<Vector2>();

            //foreach loop av arrayen "directions"
            foreach (Vector2 direction in directions)
            {
                //Tittar åt alla riktingar vad det är för sorts Tile
                Tile tile = levelManager.GetTileAtPosition(position +
                    new Vector2(direction.X * ConstantValues.tileWidth, direction.Y * ConstantValues.tileHeight));

                //Kollar om det inte är en vägg
                if (!tile.IsWall)
                {
                    if (!tile.IsEntrance)
                    {
                        possibleDirections.Add(direction);
                    }
                    //Om det inte är en vägg, lägger till den i listan "possibleDirections"

                }
            }

            //Om det finns mer än två möjliga vägar, tar bort den förra samt inverterar den så spöket inte går bakåt.
            if (possibleDirections.Count > 1)
            {
                possibleDirections.Remove(-Direction);
            }

            //Väljer en riktning slumpässigt ut av de som existerar
            Direction = possibleDirections[random.Next(0, possibleDirections.Count)];
        }

        //public void UpdateSourceRectangle()
        //{
        //    if (Direction == new Vector2(1, 0))
        //    {
        //        frameSize = 1;
        //    }
        //    if (Direction == new Vector2(-1, 0))
        //    {
        //        frameSize = 0;
        //    }

        //    if (Direction == new Vector2(0, 1))
        //    {
        //        frameSize = 2;
        //    }

        //    if (Direction == new Vector2(0, -1))
        //    {
        //        frameSize = 3;
        //    }
        //}
    }
}
