using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;

namespace TheMaze
{
    public class Saferoom
    {
        public Light saferoomLight1, saferoomLight2, saferoomLight3, saferoomLight4, saferoomLight5;
        public Light attackLight1, attackLight2, attackLight3;
        public List<Light> saferoomLights, attackLights;
        public List<Collectible> collected;
        private List<ParticleEngine> particleEngineList;
        ParticleEngine particleEngineRed,particleEngineYellow,particleEngineGreen;
        public Rectangle rectangle,attackLight1rectangle,attackLight2rectangle,attackLight3rectangle;
        public Rectangle desk;
        public bool visible,inDesk;
        public Color weapon1,weapon2,weapon3,room;
        int r, g, b,x,y;
        public int numberOfCollectibles;
        Collectible collectible;
        CollectibleViewer collectibleViewer= new CollectibleViewer(GamePlayManager.overlay);
        public enum InterActionState {Room,Desk}
        public InterActionState currentInteraction;

        public Saferoom()
        {
            particleEngineList = new List<ParticleEngine>();
            collected = new List<Collectible>();
            SafeRoomLights();
            SafeRoomParticles();
            rectangle = new Rectangle(1906, 2266, 640, 956);
            desk = new Rectangle(1926, 2655, 120, 170);
            x = 1940;
            y = 2655;
        }

        public void Update(GameTime gameTime)
        {
            VisibleCheck();
            room = new Color(r, g, b);
            
            foreach(Light l in saferoomLights)
            {
                l.Color = room;
            }

            if (visible)
            {
                LightsOn();
                foreach (SaferoomParticleEngine p in particleEngineList)
                {
                    p.Update(gameTime);
                }

                SafeRoomParticlesUpdate();
                SafeRoomInterAction();
            }
            else
            {
                LightsOff();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (visible)
            {

                spriteBatch.Draw(TextureManager.CollectibleTex, desk, Color.White);

                foreach (SaferoomParticleEngine p in particleEngineList)
                {
                    p.Draw(spriteBatch);
                }
                switch (currentInteraction)
                {
                    case InterActionState.Room:
                        {
                            break;
                        }
                    case InterActionState.Desk:
                        {
                            break;
                        }
                }
                
            }
        }

        public void SafeRoomLights()
        {
            saferoomLight1 = new PointLight();
            saferoomLight2 = new PointLight();
            saferoomLight3 = new PointLight();
            saferoomLight4 = new PointLight();
            saferoomLight5 = new PointLight();
            saferoomLight1.Position = new Vector2(2006, 2496);
            saferoomLight2.Position = new Vector2(2446, 2496);
            saferoomLight3.Position = new Vector2(2006, 3022);
            saferoomLight4.Position = new Vector2(2446, 3022);
            saferoomLight5.Position = new Vector2(2236, 2774);
            saferoomLights = new List<Light>();
            saferoomLights.Add(saferoomLight1);
            saferoomLights.Add(saferoomLight2);
            saferoomLights.Add(saferoomLight3);
            saferoomLights.Add(saferoomLight4);
            saferoomLights.Add(saferoomLight5);
            
            foreach (Light l in saferoomLights)
            {
                l.Scale = new Vector2(700, 700);
                l.Intensity = .5f;
                Game1.penumbra.Lights.Add(l);
            }

            attackLight1 = new PointLight();
            attackLight2 = new PointLight();
            attackLight3 = new PointLight();

            attackLight1.Color = Color.Red;
            attackLight2.Color = Color.Yellow;
            attackLight3.Color = Color.Green;

            attackLight1.Position = new Vector2(2006, 2266);
            attackLight2.Position = new Vector2(2236, 2266);
            attackLight3.Position = new Vector2(2446, 2266);

            attackLight1rectangle = new Rectangle((int)attackLight1.Position.X-50, (int)attackLight1.Position.Y-50, 100, 100);
            attackLight2rectangle = new Rectangle((int)attackLight2.Position.X-50, (int)attackLight2.Position.Y-50, 100, 100);
            attackLight3rectangle = new Rectangle((int)attackLight3.Position.X-50, (int)attackLight3.Position.Y-50, 100, 100);


            attackLights = new List<Light>();
            attackLights.Add(attackLight1);
            attackLights.Add(attackLight2);
            attackLights.Add(attackLight3);

            foreach (Light l in attackLights)
            {
                l.Scale = new Vector2(150, 150);
                l.Intensity = .9f;
                Game1.penumbra.Lights.Add(l);
            }

        }

        public void SafeRoomParticles()
        {
            particleEngineRed = new SaferoomParticleEngine(TextureManager.particleTextures, attackLight1.Position);
            particleEngineYellow = new SaferoomParticleEngine(TextureManager.particleTextures, attackLight2.Position);
            particleEngineGreen = new SaferoomParticleEngine(TextureManager.particleTextures, attackLight3.Position);
            particleEngineRed.color = new Color(255,100, 100);
            particleEngineGreen.color = new Color(100, 255, 100);
            particleEngineList.Add(particleEngineRed);
            particleEngineList.Add(particleEngineYellow);
            particleEngineList.Add(particleEngineGreen);
        }

        public void SafeRoomParticlesUpdate()
        {
            particleEngineRed.EmitterLocation = attackLight1.Position;
            particleEngineYellow.EmitterLocation = attackLight2.Position;
            particleEngineGreen.EmitterLocation = attackLight3.Position;
        }

        public void VisibleCheck()
        {
            
            foreach (Light l in attackLights)
            {
                if (visible)
                {
                    l.Enabled = true;
                }
                else
                {
                    l.Enabled = false;
                }
            }
        }
        
        public void LightsOn()
        {
            if (r <= 255)
            {
                r+=2;
            }
            if (b <= 255)
            {
                b+=2;
            }
            if (g <= 255)
            {
                g+=2;
            }
        }

        public void LightsOff()
        {
            if (r >= 0)
            {
                r-=2;
            }
            if (b >= 0)
            {
                b-=2;
            }
            if (g >= 0)
            {
                g-=2;
            }
        }

        public void DeskInteraction()
        {
            currentInteraction = InterActionState.Desk;

            for (int i = 0; i < numberOfCollectibles; i++)
                {
                    x += 120;
                    collectible = new Collectible(TextureManager.CollectibleTex, new Vector2(x, y));
                    collected.Add(collectible);
                }
        }

        public void RoomInterAction()
        {
            currentInteraction = InterActionState.Room;
        }

        public void DeskDraw(SpriteBatch spriteBatch)
        {
            collectibleViewer.DrawMenu(spriteBatch);
        }

        public void SafeRoomInterAction()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                currentInteraction = InterActionState.Room;
            }
        }
        

    }
}
