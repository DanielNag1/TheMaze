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
    public class WallMonster : GameObject
    {
        public Rectangle hitBoxRect;
        public Vector2 hitboxPos;
        public bool active, coolDown;
        public Stopwatch coolDownTimer,attackTimer;
        public Color color;
        public Circle hitbox;
        public Rectangle currentSourceRect;
        public int frameSize;

        private SFX sfx;

        public WallMonster(Texture2D texture, Vector2 position) : base(texture, position)
        {
            hitBoxRect = new Rectangle((int)position.X + ConstantValues.tileWidth / 2, (int)position.Y + ConstantValues.tileHeight * 2, ConstantValues.tileWidth / 8, ConstantValues.tileHeight);
            attackTimer = new Stopwatch();
            active = false;
            coolDownTimer = new Stopwatch();
            color = Color.White;
            frameSize = 128;

            currentSourceRect = new Rectangle(0, 0, frameSize, frameSize);

            sfx = new SFX();
        }

        public void Update(GameTime gameTime)
        {
            States();

            hitboxPos = position + new Vector2(65, 55);
            hitbox = new Circle(hitboxPos, 50f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y,
                    ConstantValues.tileWidth, ConstantValues.tileHeight), currentSourceRect, color);

            //spriteBatch.Draw(TextureManager.hitboxPosTex, hitBoxRect , Color.Red);

        }

        public void States()
        {
            if (active)
            {
                color = Color.Red;
                sfx.WallMonsterEncounterOn();
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
                sfx.WallMonsterEncounterOff();
            }

        }


    }
}