using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class MiniMonster : Imbaku
    {
        public Circle miniCircleHitbox;
        public Rectangle miniRectangleHitbox;
        public Vector2 miniCircleHitboxPos;

        public int health;
        private int miniMonsterPosition;
        private int chasePattern;
        protected Random random = new Random();

        public MiniMonster(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            miniMonsterPosition = random.Next(0, 50);
            currentSourceRect = new Rectangle(frame, frameSize, 41, 81);

            miniRectangleHitbox = new Rectangle((int)position.X + miniMonsterPosition, 
                (int)position.Y + miniMonsterPosition, currentSourceRect.Width, currentSourceRect.Height);
            miniCircleHitboxPos = new Vector2(miniRectangleHitbox.X, miniRectangleHitbox.Y);
            miniCircleHitbox = new Circle(miniCircleHitboxPos, 40f);

            hitbox = new Circle(position, 100);
            health = 400;

            chasePattern = random.Next(1, 3);
            speed = random.Next(50, 225);
        }

        public override void Update(GameTime gameTime, Player player)
        {
            currentSourceRect.X = frame * 41;
            currentSourceRect.Y = frameSize * 81;

            miniRectangleHitbox.X = (int)position.X + miniMonsterPosition - currentSourceRect.Width / 6 + 10;
            miniRectangleHitbox.Y = (int)position.Y + miniMonsterPosition - currentSourceRect.Height / 6 + 10;

            miniCircleHitboxPos = new Vector2(miniRectangleHitbox.X + 20, miniRectangleHitbox.Y + 40);
            miniCircleHitbox = new Circle(miniCircleHitboxPos, 30f);
            
            if (chasePattern == 3)
            {
                Pathfinding(gameTime, player);
            }
            else
            {
                Moving(gameTime);
            }          
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(position.X + miniMonsterPosition + 15, position.Y + miniMonsterPosition), 
                currentSourceRect, Color.Crimson);
        }
    }
}