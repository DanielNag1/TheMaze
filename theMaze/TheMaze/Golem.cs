using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class Golem:ChasingMonster
    {
        public Golem(Texture2D texture, Vector2 position, LevelManager levelManager) : base(texture, position, levelManager)
        {
            
        }

        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime, player);
            Pathfinding(gameTime, player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
