using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class WallMonster : Monster
    {
        public Rectangle hitBoxRect;
        public bool active, coolDown;
        public Stopwatch coolDownTimer,attackTimer;
        

        public WallMonster(Texture2D texture, Vector2 position) : base(texture, position)
        {
            hitBoxRect = new Rectangle((int)position.X + ConstantValues.tileWidth / 2, (int)position.Y + ConstantValues.tileHeight * 2, ConstantValues.tileWidth / 8, ConstantValues.tileHeight);
            attackTimer = new Stopwatch();
            active = false;
            coolDownTimer = new Stopwatch();
            color = Color.White;
            frameSize = 128;

            currentSourceRect = new Rectangle(0, 0, frameSize, frameSize);

        }

        public override void Update(GameTime gameTime)
        {
            States();

            hitboxPos = position + new Vector2(65, 55);
            hitbox = new Circle(hitboxPos, 50f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y,
                    ConstantValues.tileWidth, ConstantValues.tileHeight), currentSourceRect, color);

            

        }

        public void States()
        {
            if (active)
            {
                color = Color.Red;
            }

            if (coolDown)
            {
                color = Color.Blue;
                active = false;
                coolDownTimer.Start();
            }

            if (coolDownTimer.ElapsedMilliseconds >= 5000)
            {
                coolDown = false;
                color = Color.White;
                coolDownTimer.Reset();

            }

        }


    }
}