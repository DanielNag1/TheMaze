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
    public class Stalker : ChasingMonster
    {
        public Rectangle stalkerRectangleHitbox;
        public Circle stalkerCircleHitbox;
        public Vector2 stalkerCircleHitboxPos;

        public Stopwatch stalkerFlashTimer = new Stopwatch();
        public Stopwatch stalkerStunnedTimer = new Stopwatch();
        public Random stalkerRandom = new Random();

        public bool stalkerStunned = false;

        public int monsterDamage = 1;

        public Stalker(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            frameSize = 0;
            //currentSourceRect = new Rectangle(0, 0, frameSize, frameSize);
            stalkerRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, currentSourceRect.Width, currentSourceRect.Height);
            nrFrames = 4;

            stalkerCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            stalkerCircleHitbox = new Circle(stalkerCircleHitboxPos, 90f);

            speed = 50f;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            stalkerRectangleHitbox.X = (int)position.X;
            stalkerRectangleHitbox.Y = (int)position.Y;

            stalkerCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            stalkerCircleHitbox = new Circle(stalkerCircleHitboxPos, 90f);
            
            if (!stalkerStunned)
            {
                Pathfinding(gameTime, player);
            }   
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (stalkerStunned)
            {
                spriteBatch.Draw(texture, stalkerRectangleHitbox, currentSourceRect, Color.Goldenrod);
            }
            else
            {
                spriteBatch.Draw(texture, stalkerRectangleHitbox, currentSourceRect, Color.DarkGray);
            }  
        }
    }
}
