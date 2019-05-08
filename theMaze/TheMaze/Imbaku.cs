using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class Imbaku : Monster
    {

        Rectangle fixedPos;
        public Vector2 rotationDir;

        public Imbaku(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
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
            currentSourceRect.X = frame * 290;
            currentSourceRect.Y = frameSize * 462;
            //base.Update(gameTime);

            hitboxPos = new Vector2(fixedPos.X, fixedPos.Y);

            fixedPos.X = (int)position.X - currentSourceRect.Width / 4 + 50;
            fixedPos.Y = (int)position.Y - currentSourceRect.Height / 4 - 54;


            UpdateSourceRectangle();

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

            Moving(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, fixedPos, currentSourceRect, Color.White);

            //spriteBatch.Draw(TextureManager.hitboxPosTex, hitboxPos, Color.Red);


        }

    }
}