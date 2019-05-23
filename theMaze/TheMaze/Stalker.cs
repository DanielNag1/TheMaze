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

        SFX sfx;

        public Stalker(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            frameSize = 0;
            nrFrames = 3;

            currentSourceRect = new Rectangle(frame, frameSize, 125, 250);

            stalkerRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, ConstantValues.tileWidth, ConstantValues.tileHeight);

            stalkerCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            stalkerCircleHitbox = new Circle(stalkerCircleHitboxPos, 90f);

            sfx = new SFX();
            timer = 200;
            timeIntervall = 200;
            speed = 40f;
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

            //sfx.StalkerWhispers(gameTime);

            if (!stalkerStunned)
            {
                Pathfinding(gameTime, player);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawingOffset, currentSourceRect, Color.White);
        }
    }
}