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
    public class Lights
    {
        Saferoom saferoom;
        public Light saferoomLight,saferoomweaponLight,collectibleLight;
        public List<Light> saferoomLightList;
        public List<Light> saferoomWeaponList;
        
        public Lights(LevelManager levelManager,Saferoom saferoom)
        {
            this.saferoom = saferoom;
            saferoomLightList = new List<Light>();
            saferoomWeaponList = new List<Light>();

            foreach (Vector2 position in saferoom.saferoomLightPositions)
            {
                saferoomLight = new PointLight();
                saferoomLight.Color = saferoom.saferoomLightColor;
                saferoomLight.Position = position;
                saferoomLight.Intensity = saferoom.saferoomLightIntensity;
                saferoomLight.Scale = saferoom.saferoomLightScale;
                saferoomLightList.Add(saferoomLight);
                Game1.penumbra.Lights.Add(saferoomLight);
            }

            foreach (Vector2 position in levelManager.collectiblePositions)
            {
                collectibleLight = new PointLight();
                collectibleLight.Color = Color.BlueViolet;
                collectibleLight.Rotation = 2f;
                collectibleLight.Position = position;
                collectibleLight.Intensity = .5f;
                collectibleLight.Scale = new Vector2(200, 200);
                Game1.penumbra.Lights.Add(collectibleLight);
            }

            switch (GamePlayManager.currentLevel)
            {
                case GamePlayManager.Level.Level1:
                    break;
                case GamePlayManager.Level.Level2:
                    foreach (Vector2 weaponposition in saferoom.saferoomWeaponLightPositions)
                    {
                        saferoomLight = new PointLight();
                        saferoomLight.Position = weaponposition;
                        saferoomLight.Intensity = saferoom.saferoomWeaponLightIntensity;
                        saferoomLight.Scale = saferoom.saferoomWeaponLightScale;
                        saferoomWeaponList.Add(saferoomLight);
                        Game1.penumbra.Lights.Add(saferoomLight);
                    }

                    foreach (Light l in saferoomWeaponList)
                    {
                        saferoomWeaponList[0].Color = saferoom.weaponLight1Color;
                        
                    }

                    break;
                case GamePlayManager.Level.Level3:
                    foreach (Vector2 weaponposition in saferoom.saferoomWeaponLightPositions)
                    {
                        saferoomLight = new PointLight();
                        saferoomLight.Position = weaponposition;
                        saferoomLight.Intensity = saferoom.saferoomWeaponLightIntensity;
                        saferoomLight.Scale = saferoom.saferoomWeaponLightScale;
                        saferoomWeaponList.Add(saferoomLight);
                        Game1.penumbra.Lights.Add(saferoomLight);
                    }

                    foreach (Light l in saferoomWeaponList)
                    {
                        saferoomWeaponList[0].Color = saferoom.weaponLight1Color;
                        saferoomWeaponList[1].Color = saferoom.weaponLight2Color;
                    }
                    break;
                case GamePlayManager.Level.Level4:
                    foreach (Vector2 weaponposition in saferoom.saferoomWeaponLightPositions)
                    {
                        saferoomLight = new PointLight();
                        saferoomLight.Position = weaponposition;
                        saferoomLight.Intensity = saferoom.saferoomWeaponLightIntensity;
                        saferoomLight.Scale = saferoom.saferoomWeaponLightScale;
                        saferoomWeaponList.Add(saferoomLight);
                        Game1.penumbra.Lights.Add(saferoomLight);
                    }

                    foreach (Light l in saferoomWeaponList)
                    {
                        saferoomWeaponList[0].Color = saferoom.weaponLight1Color;
                        saferoomWeaponList[1].Color = saferoom.weaponLight2Color;
                    }
                    break;
            }
            

        }

        public void Update(GameTime gameTime)
        {
            foreach (Light l in saferoomLightList)
            {
                l.Color = saferoom.saferoomLightColor;
            }


            foreach(Light l in saferoomWeaponList)
            {
                l.Intensity = saferoom.saferoomWeaponLightIntensity;
            }

            

        }


    }
}
