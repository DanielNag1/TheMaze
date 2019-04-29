using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class Imbaku:Monster
    {

        

        public Imbaku(Texture2D texture, Vector2 position, TileManager tileManager):base(texture,position,tileManager)
        {

            frameSize = 200;
            currentSourceRect = new Rectangle(frame, frameSize, 300, 400);
            
            nrFrames = 1;
            timeIntervall = 800;
            
        }

        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
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


            Moving(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //spriteBatch.Draw(Texture, position, currentSourceRect, Color.White, 0f, new Vector2(),
            //    1f, SpriteEffects.None, 0f);

            //spriteBatch.Draw(TextureManager.rangeTex, currentSourceRect, Color.Red);


        }


    }
}
