using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class MiniMonster:Imbaku
    {
        protected Random random = new Random();
        public Circle miniCircleHitbox;
        public Rectangle miniRectangleHitbox;
        public Vector2 miniCircleHitboxPos;
        public int health;


        public MiniMonster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            position = new Vector2(imbakuRectangleHitbox.Width/2, imbakuRectangleHitbox.Height/2);
            currentSourceRect = new Rectangle(frame, frameSize, 41, 81);

            miniRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, currentSourceRect.Width, currentSourceRect.Height);

            miniCircleHitboxPos = new Vector2(miniRectangleHitbox.X,miniRectangleHitbox.Y);
            miniCircleHitbox = new Circle(miniCircleHitboxPos, 40f);

            hitbox = new Circle(position, 100);
            health = 400;

            speed = random.Next(50, 500);
            
        }

        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime, player);

            currentSourceRect.X = frame * 41;
            currentSourceRect.Y = frameSize * 81;

            miniRectangleHitbox.X = (int)position.X - currentSourceRect.Width/6+10;
            miniRectangleHitbox.Y = (int)position.Y - currentSourceRect.Height/6+10;

            miniCircleHitboxPos = new Vector2(miniRectangleHitbox.X+20, miniRectangleHitbox.Y+40);
            miniCircleHitbox = new Circle(miniCircleHitboxPos, 30f);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //miniCircleHitbox.Draw(spriteBatch);
            //spriteBatch.Draw(TextureManager.RedTexture, miniRectangleHitbox, Color.Red);
            spriteBatch.Draw(texture, position, currentSourceRect, Color.Crimson);
            
        }
    }
}
