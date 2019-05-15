using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class Stalker : ChasingMonster
    {
        public Rectangle stalkerMonsterRectangleHitbox;
        public Circle stalkerMonsterCircleHitbox;
        public Vector2 stalkerMonsterCircleHitboxPos;

        public Stalker(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            frame = 0;
            frameSize = 0;
            currentSourceRect = new Rectangle(frame, frameSize, 125, 210);

            nrFrames = 4;
            timeIntervall = 100;

            stalkerMonsterRectangleHitbox = new Rectangle((int)position.X, (int)position.Y, ConstantValues.tileWidth, ConstantValues.tileHeight);

            stalkerMonsterCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            stalkerMonsterCircleHitbox = new Circle(stalkerMonsterCircleHitboxPos, 90f);

            path = new List<Vector2>();

            speed = 50f;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            stalkerMonsterRectangleHitbox.X = (int)position.X;
            stalkerMonsterRectangleHitbox.Y = (int)position.Y;

            stalkerMonsterCircleHitboxPos = new Vector2(position.X + ConstantValues.tileWidth / 2, position.Y);
            stalkerMonsterCircleHitbox = new Circle(stalkerMonsterCircleHitboxPos, 90f);
        }
    }
}
