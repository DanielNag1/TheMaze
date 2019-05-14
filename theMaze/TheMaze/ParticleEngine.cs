using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace TheMaze
{
    public class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        public List<Particle> particles;
        private List<Texture2D> textures;
        public bool isHit;
        private int total;

        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
            isHit = false;
        }

        public void Update()
        {
            total = 1;

            if (isHit)
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
            }
            
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    8f * (float)(random.NextDouble() * 2 - 1),
                                    8f * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 1f * (float)(random.NextDouble() *.1);
            Color color = new Color(
                        (float)random.NextDouble(),
                        (float)random.NextDouble(),
                        (float)random.NextDouble());
            float size = random.Next(2, 7)*.1f;
            int ttl = 15 + random.Next(4);


            return new Particle(texture, position, velocity, angle, angularVelocity, Color.White, size, ttl);


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isHit)
            {
                for (int index = 0; index < particles.Count; index++)
                {
                    particles[index].Draw(spriteBatch);
                }
            }
        }
        

    }
}