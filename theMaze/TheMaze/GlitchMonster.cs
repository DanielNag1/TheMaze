using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class GlitchMonster : Monster
    {
        public Rectangle glitchMonsterRectangleHitbox;

        public GlitchMonster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            glitchMonsterRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, ConstantValues.tileWidth, ConstantValues.tileHeight);
        }

        public override void Update(GameTime gameTime, Player player)
        {
            glitchMonsterRectangleHitbox.X = (int)position.X;
            glitchMonsterRectangleHitbox.Y = (int)position.Y;

            Moving(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            {
                spriteBatch.Draw(texture, glitchMonsterRectangleHitbox, currentSourceRect, Color.White);
            }
        }
    }
}
