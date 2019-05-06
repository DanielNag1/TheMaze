using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class WallMonster:Monster
    {
        public Rectangle hitBoxRect;
        public Vector2 offset;
        public bool active;

        public WallMonster(Texture2D texture, Vector2 position, TileManager tileManager):base(texture,position,tileManager)
        {
            hitBoxRect = new Rectangle((int)position.X+ConstantValues.tileWidth/2, (int)position.Y+ConstantValues.tileHeight*2, ConstantValues.tileWidth/8, ConstantValues.tileHeight);
            active = false;
            
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(TextureManager.hitboxPosTex, hitBoxRect , Color.Red);
            //hitbox.Draw(spriteBatch);
        }


    }
}
