using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public abstract class GameObject
    {
        public Texture2D Texture { get; protected set; }
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            this.position = position;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
