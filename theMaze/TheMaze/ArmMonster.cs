using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class ArmMonster : GameObject
    {
        TileManager tileManager;

        private Random random;
        public Circle hitboxSpawn, hitboxGrab;
        public bool activated = true;

        public ArmMonster(Texture2D texture, Vector2 position, TileManager tileManager) : base(texture, position)
        {
            this.tileManager = tileManager;

            random = new Random();
        }

        public void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (activated)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y,
                    ConstantValues.tileWidth, ConstantValues.tileHeight), Color.White);
            }
        }
    }
}
