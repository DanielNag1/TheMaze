using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class GlitchMonster : WanderingMonster
    {
        public Rectangle glitchMonsterRectangleHitbox;

        public Stopwatch glitchMonsterTimer = new Stopwatch();

        public GlitchMonster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            frameSize = 128;
            currentSourceRect = new Rectangle(0, 0, frameSize, frameSize);
            glitchMonsterRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, currentSourceRect.Width, currentSourceRect.Height);
            nrFrames = 4;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            glitchMonsterRectangleHitbox.X = (int)position.X;
            glitchMonsterRectangleHitbox.Y = (int)position.Y;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.RedTexture, glitchMonsterRectangleHitbox, Color.Red);
            spriteBatch.Draw(texture, glitchMonsterRectangleHitbox, currentSourceRect, Color.White);

        }
    }
}
