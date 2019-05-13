using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class MiniMonster:Imbaku
    {
        protected Random random = new Random();


        public MiniMonster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            position = new Vector2(imbakuRectangleHitbox.Width/2, imbakuRectangleHitbox.Height/2);
            currentSourceRect = new Rectangle(frame, frameSize, 41, 81);

            hitbox = new Circle(position, 20);

            
        }

        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime, player);
            currentSourceRect.X = frame * 41;
            currentSourceRect.Y = frameSize * 81;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            hitbox.Draw(spriteBatch);
            spriteBatch.Draw(texture, position, currentSourceRect, Color.White);
            
        }
    }
}
