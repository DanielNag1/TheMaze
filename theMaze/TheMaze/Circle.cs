using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public struct Circle
    {
        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        Rectangle rangeRect;

        public Circle(Vector2 center, float radius)
        {
            Center = new Vector2(center.X, center.Y);
            Radius = radius;
            rangeRect = new Rectangle((int)Center.X-(int)radius, (int)Center.Y-(int)radius, (int)radius * 2, (int)radius * 2);
        }

        public bool Contains(Vector2 point)
        {
            return ((point - Center).Length() <= Radius);
        }

        public bool Intersects(Circle other)
        {
            return ((other.Center - Center).Length() < (other.Radius + Radius));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(TextureManager.rangeTex, rangeRect, Color.White);
        }
    }
}
