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

        TileManager tileManager;

        //List<Point> pathPointsList; 

        //Konstanter för rörelse. Praktiskt för återanvänding och läsbarhet.
        private readonly Vector2 Up = new Vector2(0, -1);
        private readonly Vector2 Down = new Vector2(0, 1);
        private readonly Vector2 Left = new Vector2(-1, 0);
        private readonly Vector2 Right = new Vector2(1, 0);

        public Vector2 Direction { get; private set; }
        private Vector2 destination;

        private Random random;

        private Rectangle currentSourceRect, nextSourceRect;
        public readonly int frameSize = 128;

        private int frame = 0, nrFrames = 4;
        private double timer = 100, timeIntervall = 100;

        private float speed = 100f;  // Som visuell feedback kan monstrets speed minskas när man riktar ljuset mot det.

        private bool moving = false;

        public bool startMoving; // Tänkte att någonstans i spelets kod ska det finnas något som säger if player hamnar
                                 // på en specifik tile, spawna monster och starta movement och då ska den ta en väg mot 
                                 // spelaren eller något liknande.

        // Hitbox saknas!!

        public Monster(Texture2D texture, Vector2 position, TileManager tileManager) : base (texture, position)
        {
            this.tileManager = tileManager;
            
            random = new Random();

            currentSourceRect = new Rectangle(0, 0, frameSize, frameSize);
            nextSourceRect = currentSourceRect;

            startMoving = false;
        }

        public void Update(GameTime gameTime)
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

            UpdateSourceRectangle();
            Moving(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, 
                ConstantValues.tileWidth, ConstantValues.tileHeight), currentSourceRect, Color.White);
        }

        private void Moving(GameTime gameTime)
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

        private void ChangeDirection(Vector2 newDirection)
        {
            Direction = newDirection;
            Vector2 newDestination = Position + Direction * ConstantValues.tileWidth;

            Tile tile = tileManager.GetTileAtPosition(Direction);
            if (tile.IsWall)
            {
                destination = newDestination;
                moving = true;
            }
        }

        private void NewDirection()
        {
            //Array av de fyra olika riktingarna som heter "directions"
            Vector2[] directions = new[] { Up, Down, Left, Right };

            //En ny lista av Vector2 med namn "possibleDirections", som är tom
            List<Vector2> possibleDirections = new List<Vector2>();

            //foreach loop av arrayen "directions"
            foreach (Vector2 direction in directions)
            {
                //Tittar åt alla riktingar vad det är för sorts Tile
                Tile tile = tileManager.GetTileAtPosition(position + 
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

        private void UpdateSourceRectangle()
        {
            if (Direction == Up)
            {
                nextSourceRect.Y = 2 * frameSize;
            }
            if (Direction == Down)
            {
                nextSourceRect.Y = 0 * frameSize;
            }
            if (Direction == Right)
            {
                nextSourceRect.Y = 1 * frameSize;
            }
            if (Direction == Left)
            {
                nextSourceRect.Y = 3 * frameSize;
            }
        }
    }
}
