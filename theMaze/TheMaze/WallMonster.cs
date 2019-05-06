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
    public class WallMonster:GameObject
    {
        public Rectangle hitBoxRect;
        public Vector2 offset;
        public bool active, coolDown;
        public Stopwatch coolDownTimer;
        public Color color;
        public Circle hitbox;
        

        public WallMonster(Texture2D texture, Vector2 position):base(texture,position)
        {
            hitBoxRect = new Rectangle((int)position.X+ConstantValues.tileWidth/2, (int)position.Y+ConstantValues.tileHeight*2, ConstantValues.tileWidth/8, ConstantValues.tileHeight);
            active = false;
            coolDownTimer = new Stopwatch();
            hitbox = 
            
        }

        public void Update(GameTime gameTime)
        {
            States();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            spriteBatch.Draw(TextureManager.hitboxPosTex, hitBoxRect , Color.Red);
            //hitbox.Draw(spriteBatch);
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

            if(coolDownTimer.ElapsedMilliseconds >= 5000)
            {
                coolDown = false;
                color = Color.White;
                coolDownTimer.Reset();
                
            }

        }


    }
}
