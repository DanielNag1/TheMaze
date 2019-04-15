using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class InvisibleMonster : GameObject
    {
        //Konstanter för rörelse. Praktiskt för återanvänding och läsbarhet.
        private static Vector2 Up = new Vector2(0, -1);
        private static Vector2 Down = new Vector2(0, 1);
        private static Vector2 Left = new Vector2(-1, 0);
        private static Vector2 Right = new Vector2(1, 0);

        public Rectangle hitbox;

        Random rand;
        int frame;
        SpriteEffects ghostFx = SpriteEffects.None;
        TimeSpan previousTime = new TimeSpan();


        private TileManager tileManager;

        private int ticks = 0;

        private Vector2 direction;

        public InvisibleMonster(Texture2D texture, Vector2 position, TileManager tileManager) : base(texture, position)
        {
            this.Texture = texture;

            this.tileManager = tileManager;
            
            this.rand = new Random(GetHashCode());

            NewDirection();
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
                Vector2 centerPosition = 
                //Tittar åt alla riktingar vad det är för sorts Tile
                Tile tile = tileManager.GetTileAtPosition(position + new Vector2(direction.X * ConstantValues.tileWidth, direction.Y * ConstantValues.tileHeight));

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
                possibleDirections.Remove(-direction);
            }

            //Väljer en riktning slumpässigt ut av de som existerar
            direction = possibleDirections[rand.Next(0, possibleDirections.Count)];
        }

        public void Update(GameTime gameTime)
        {
            //Konstant uppdaterar spökenas rörelse
            ticks++;

            //Var 32:e uppdate har de rört sig en till en ny tile, anroppar då NewDirection.
            //Varje tile är 32 pixlar, därför kollar vi var 32:e uppdate.
            int current = ticks % 128;

            if (current == 0)
            {
                NewDirection();
            }

            //Uppdaterar spökets position genom att flytta det ett steg mot direction.
            position += direction;

            //Spökets hitbox
            hitbox = new Rectangle((int)position.X, (int)position.Y, 128, 128);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, Color.White);
            //spriteBatch.Draw(
            //    Texture,
            //    position,
            //    null,
            //    Color.White,
            //    0,
            //    new Vector2(64, 64),
            //    1,
            //    ghostFx,
            //    1
            //    );
        }
    }
}
