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
        public Vector2 drawingOffset;

        public Stopwatch stalkerFlashTimer = new Stopwatch();
        public Stopwatch stalkerStunnedTimer = new Stopwatch();
        public Random stalkerRandom = new Random();

        public bool stalkerStunned = false;



        public Stalker(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            frameSize = 0;
            currentSourceRect = new Rectangle(frame, frameSize, 125, 250);
            stalkerRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, ConstantValues.tileWidth, ConstantValues.tileHeight);
            nrFrames = 3;

            drawingOffset = new Vector2((int)position.X, (int)position.Y-50);

            stalkerCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            stalkerCircleHitbox = new Circle(stalkerCircleHitboxPos, 90f);

            timer = 200;
            timeIntervall = 200;
            speed = 50f;
            monsterDamage = 1;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime);
            currentSourceRect.X = frame * 125;
            currentSourceRect.Y = frameSize * 250;

            drawingOffset.X = (int)position.X;
            drawingOffset.Y = (int)position.Y - 120;

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

            spriteBatch.Draw(TextureManager.RedTexture, stalkerRectangleHitbox, Color.Red);
            spriteBatch.Draw(texture, drawingOffset, currentSourceRect, Color.White);
            


            //if (stalkerStunned)
            //{
            //    spriteBatch.Draw(texture, stalkerRectangleHitbox, currentSourceRect, Color.Goldenrod);
            //}
            //else
            //{
            //    spriteBatch.Draw(texture, stalkerRectangleHitbox, currentSourceRect, Color.DarkGray);
            //}
        }
    }
}