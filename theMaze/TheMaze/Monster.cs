using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class Monster : GameObject
    {
        // TILL KODGRANSKAREN!!!
        // Detta är en placeholder klass med väldigt scripted movement som startas igång av en trigger.
        // Denna klassen existerar endast för att demonstrera player interaction med enemy och kommer alltså inte 
        // att existera i framtida versioner av spelet.
        //
        // TLDR du kan skita i att granska denna klassen :)

        protected LevelManager levelManager;

        //Konstanter för rörelse. Praktiskt för återanvänding och läsbarhet.
        private readonly Vector2 Up = new Vector2(0, -1);
        private readonly Vector2 Down = new Vector2(0, 1);
        private readonly Vector2 Left = new Vector2(-1, 0);
        private readonly Vector2 Right = new Vector2(1, 0);

        public Vector2 Direction { get; private set; }
        public Vector2 destination, hitboxPos;
        protected Vector2 direction;

        private Random random;

        public Circle hitbox;

        protected Rectangle currentSourceRect, nextSourceRect;
        public int frameSize;

        protected int frame = 0, nrFrames = 4;
        protected double timer = 100, timeIntervall = 100;

        public float speed = 100f;

        protected bool moving = false;
        public bool isAlive = true;

        public Color color = Color.White;
        private Color colorFade;

        public double health = 5;

        public Monster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position)
        {
            this.levelManager = levelManager;
            frameSize = 128;
            random = new Random();

            currentSourceRect = new Rectangle(0, 0, frameSize, frameSize);
            nextSourceRect = currentSourceRect;
            hitboxPos = position + new Vector2(65, 55);
            hitbox = new Circle(hitboxPos, 50f);

            colorFade = new Color(100, 100, 100, 100);
            Vector2[] directions = new[] { Up, Down, Left, Right };
        }

        public virtual void Update(GameTime gameTime, Player player)
        {
            if (moving)
            {
                timer -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer <= 0)
                {
                    timer = timeIntervall;
                    frame++;
                    if (frame >= nrFrames)
                    {
                        frame = 0;
                    }
                    currentSourceRect.X = frame * frameSize;
                }
                currentSourceRect.Y = nextSourceRect.Y;
            }
            else
            {
                frame = 0;
                currentSourceRect.X = frame * frameSize;
            }

            hitboxPos = position + new Vector2(65, 55);
            hitbox = new Circle(hitboxPos, 50f);

            //UpdateSourceRectangle();
            //Moving(gameTime);
        }

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //hitbox.Draw(spriteBatch);
            
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y,
                    ConstantValues.tileWidth, ConstantValues.tileHeight), currentSourceRect, color);
            
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

            direction = newDirection;
            Vector2 newDestination = Position + direction * ConstantValues.tileWidth;

            Tile tile = levelManager.GetTileAtPosition(direction);
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
                    //Om det inte är en vägg, lägger till den i listan "possibleDirections"
                    possibleDirections.Add(direction);
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

        //protected void UpdateSourceRectangle()
        //{
        //    if (Direction == Up)
        //    {
        //        nextSourceRect.Y = 0 * frameSize;
        //    }
        //    if (Direction == Down)
        //    {
        //        nextSourceRect.Y = 2 * frameSize;
        //    }
        //    if (Direction == Right)
        //    {
        //        nextSourceRect.Y = 1 * frameSize;
        //    }
        //    if (Direction == Left)
        //    {
        //        nextSourceRect.Y = 0 * frameSize;
        //    }
        //}

        //protected void UpdateSourceRectangle()
        //{
        //    if(Pathfind.SetDirectionFromNextPosition())
        //    if ( == Right)
        //    {
        //        frameSize = 1;
        //    }
        //    if (Direction == Left)
        //    {
        //        frameSize = 0;
        //    }

        //    if (Direction == Down)
        //    {
        //        frameSize = 2;
        //    }
        //}


    }
}