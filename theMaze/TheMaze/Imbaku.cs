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

        Rectangle fixedPos;


        public Imbaku(Texture2D texture, Vector2 position, TileManager tileManager):base(texture,position,tileManager)
        {
            frameSize = 0;
            currentSourceRect = new Rectangle(frame, frameSize, 304, 462);
            
            nrFrames = 5;
            timeIntervall = 120;

            fixedPos = new Rectangle((int)position.X, (int)position.Y,
                        304 - ConstantValues.tileWidth, 462 - ConstantValues.tileHeight);
        }

        public override void Update(GameTime gameTime)
        {
            currentSourceRect.X = frame * 292;
            currentSourceRect.Y = frameSize * 462;

            fixedPos.X = (int)position.X - currentSourceRect.Width/4+50;
            fixedPos.Y = (int)position.Y - currentSourceRect.Height/4-57;

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
                
            }

            //fixedPos = new Rectangle((int)position.X, (int)position.Y,
            //            304 - ConstantValues.tileWidth, 462 - ConstantValues.tileHeight);
            Moving(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(Texture, fixedPos, currentSourceRect, Color.White);
            

            //spriteBatch.Draw(TextureManager.rangeTex, currentSourceRect, Color.Red);


        }
        
    }
}
