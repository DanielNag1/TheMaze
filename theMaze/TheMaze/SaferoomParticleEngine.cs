using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheMaze
{

    public class SaferoomParticleEngine:ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation;
        private List<Particle> particles;
        private List<Texture2D> textures;
        private float particletimer;
        public Rectangle SaferoommRectangle;

        public SaferoomParticleEngine(List<Texture2D> textures, Vector2 location): base(textures,location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
            size = .125f;
            offsetX = 5;
            offsetY = 5;
            particletimer = 60f;
        }
        public override void Update(GameTime gameTime)
        {
            particletimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            ttl = 10 + random.Next(0, 40);
            velocity = new Vector2(0, random.Next(1, 4));
            if(particletimer>=60 && particletimer<=80)
            {
                velocity.X = -0.35f;
            }
            if (particletimer<=0)
            {
                velocity.X = 0.35f;
                particletimer = 80f;
            }

            base.Update(gameTime);
        }




    }
}
