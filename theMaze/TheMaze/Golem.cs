using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class Golem : ChasingMonster
    {
        public Rectangle golemRectangleHitbox;
        public Circle golemCircleHitbox;
        public Vector2 golemCircleHitboxPos;

        public bool isActive, isSleeping;

        public Golem(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            frameSize = 0;
            golemRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, currentSourceRect.Width, currentSourceRect.Height);

            golemCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            golemCircleHitbox = new Circle(golemCircleHitboxPos, 90f);

            color = Color.Brown;
            speed = 50f;
            monsterDamage = 1;

            isActive = false;
            isSleeping = false;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            //base.Update(gameTime, player);
            golemRectangleHitbox.X = (int)position.X;
            golemRectangleHitbox.Y = (int)position.Y;

            golemCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y + ConstantValues.tileWidth / 2);
            golemCircleHitbox = new Circle(golemCircleHitboxPos, 90f);

            GolemStates();
            Pathfinding(gameTime, player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //golemCircleHitbox.Draw(spriteBatch);
            //spriteBatch.Draw(TextureManager.RedTexture, golemRectangleHitbox, Color.Red);
            spriteBatch.Draw(texture, golemRectangleHitbox, currentSourceRect, color);
        }

        public void GolemStates()
        {
            if (isSleeping)
            {
                color = Color.Gray;
                speed = 0;
            }

            if (isActive)
            {
                color = Color.Red;
                speed = 100;
            }



        }

    }
}