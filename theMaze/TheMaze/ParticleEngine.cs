using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation,velocity;
        private List<Particle> particles;
        private List<Texture2D> textures;
        protected int total,ttl,offsetX,offsetY;
        protected float angle, angularVelocity, size;
        public Color color;
        private float particletimer;
        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
            color = Color.White;
            angle = 0;
            offsetX = -30;
            offsetY = 15;
            size = .3f;
            ttl = 10 + random.Next(40);
            total = 1;
            particletimer = 20f;
        }

        public virtual void Update(GameTime gameTime)
        {
            EmitterLocation.X += offsetX;
            EmitterLocation.Y += offsetY;
            particletimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (particletimer<=0)
            {
                for (int i = 0; i < total; i++)
                {
                    particles.Add(GenerateNewParticle());
                }

                for (int particle = 0; particle < particles.Count; particle++)
                {
                    particles[particle].Update();
                    if (particles[particle].TTL <= 0)
                    {
                        particles.RemoveAt(particle);
                        particle--;
                    }
                }
                particletimer = 20f;
            }
        }

        public virtual Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            
            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }
    }
}
