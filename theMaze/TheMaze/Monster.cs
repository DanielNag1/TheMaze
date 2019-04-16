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

        List<Point> pathPointsList;

        public Vector2 Direction { get; private set; }

        private Rectangle currentSourceRect, nextSourceRect;
        public readonly int frameSize = 64;

        private int frame = 0, nrFrames = 4;
        private double timer = 100, timeIntervall = 100;

        private float speed = 2f;  // Som visuell feedback kan monstrets speed minskas när man riktar ljuset mot det.

        private bool moving = false;

        public bool startMoving; // Tänkte att någonstans i spelets kod ska det finnas något som säger if player hamnar
                                 // på en specifik tile, spawna monster och starta movement och då ska den ta en väg mot 
                                 // spelaren eller något liknande.

        // Hitbox saknas!!

        public Monster(Texture2D texture, Vector2 position, TileManager tileManager) : base (texture, position)
        {
            this.tileManager = tileManager;

            this.position = tileManager.Tiles[1, 3].Hitbox.Center.ToVector2(); // Startposition på samma tile som player.

            pathPointsList = new List<Point>();
            // Under lägger du till koordinater/noder i ordningen du vill att monstret ska följa.
            pathPointsList.Add(tileManager.Tiles[7, 3].Hitbox.Center); // Börjar med att gå mot höger.
            pathPointsList.Add(tileManager.Tiles[7, 7].Hitbox.Center); // Sedan går den neråt.


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

                Movement();

                currentSourceRect.Y = nextSourceRect.Y;

                position += speed * Direction;
            }
            else
            {
                Movement();
                frame = 0;

                currentSourceRect.X = frame * frameSize;
            }
        }

        private void Movement()
        {
            // Koden här tar koordinaterna som finns i pathPointsList en i taget och flyttar monstret till alla koordinater i ordning.

            int x = 0;

            if (position == pathPointsList[x].ToVector2())
            {
                if (x < pathPointsList.Count() - 1) // Skrev -1 annars kraschade programmet.
                {
                    x += 1; // När monstret har nått en koordinat, ta fram nästa koordinat i listan.
                }
            }

            if (position.X > pathPointsList[x].X || position.X < pathPointsList[x].X
                || position.Y > pathPointsList[x].Y || position.Y < pathPointsList[x].Y)
            {
                moving = true;
                if (position.X > pathPointsList[x].X)
                {
                    Direction = new Vector2(-1, 0);
                    nextSourceRect.Y = 3 * frameSize;
                }
                if (position.X < pathPointsList[x].X)
                {
                    Direction = new Vector2(1, 0);
                    nextSourceRect.Y = 1 * frameSize;
                }
                if (position.Y > pathPointsList[x].Y)
                {
                    Direction = new Vector2(0, -1);
                    nextSourceRect.Y = 2 * frameSize;
                }
                if (position.Y < pathPointsList[x].Y)
                {
                    Direction = new Vector2(0, 1);
                    nextSourceRect.Y = 0 * frameSize;
                }
            }
            else
            {
                moving = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, frameSize, frameSize),
                currentSourceRect, Color.White, 0f, new Vector2(32, 50), SpriteEffects.None, 1);
        }
    }
}
