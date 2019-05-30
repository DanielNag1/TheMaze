using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class Golem : ChasingMonster
    {
        public Rectangle golemRectangleHitbox;
        public Circle golemCircleHitbox;
        public Vector2 golemCircleHitboxPos;
        public Vector2 drawingOffset;

        public SFX sfx;

        public bool isActive, isSleeping;

        public Golem(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            frameSize = 0;
            currentSourceRect = new Rectangle(frame, frameSize, 117, 200);
            golemRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, ConstantValues.tileWidth, ConstantValues.tileHeight);

            golemCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            golemCircleHitbox = new Circle(golemCircleHitboxPos, 90f);

            drawingOffset = new Vector2((int)position.X, (int)position.Y - 75);

            sfx = new SFX();

            color = Color.White;

            speed = 50f;
            monsterDamage = 1;

            isActive = false;
            isSleeping = false;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            currentSourceRect.X = frame * 117;
            currentSourceRect.Y = frameSize * 200;

            golemRectangleHitbox.X = (int)position.X;
            golemRectangleHitbox.Y = (int)position.Y;

            golemCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y + ConstantValues.tileWidth / 2);
            golemCircleHitbox = new Circle(golemCircleHitboxPos, 90f);

            drawingOffset.X = (int)position.X;
            drawingOffset.Y = (int)position.Y - 75;

            GolemStates(gameTime, player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, drawingOffset, currentSourceRect, color);
        }

        public void GolemStates(GameTime gameTime, Player player)
        {
            if (isSleeping)
            {
                speed = 0;
            }
            else
            {
                Pathfinding(gameTime, player);
                speed = 50;
            }

            if (isActive)
            {
                speed = 120;
            }
        }
    }
}