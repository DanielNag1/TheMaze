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
        public Vector2 saferoomPos,saferoomLight1Pos,saferoomLight2Pos,saferoomLight3Pos,saferoomLight4Pos,saferoomLight5Pos;
        public Vector2 saferoomLightScale;
        public float saferoomLightIntensity;
        public Rectangle saferoomHitBox;
        public List<Vector2> saferoomLightPositions;
        public bool visible;
        public int r, g, b;
        public Color saferoomLightColor;
        
        public Saferoom(LevelManager levelManager)
        {
            //saferoomLightColor = Color.White;
            //saferoomLightColor = new Color(r, g, b);
            saferoomLightPositions = new List<Vector2>();
            saferoomPos = new Vector2(levelManager.StartPositionSafeRoom.X, levelManager.StartPositionSafeRoom.Y - ConstantValues.tileHeight);
            saferoomHitBox = new Rectangle((int)saferoomPos.X,(int)saferoomPos.Y, 640, 956);
            saferoomLight1Pos = new Vector2(saferoomPos.X, saferoomPos.Y + 230);
            saferoomLight2Pos = new Vector2(saferoomPos.X + 630, saferoomPos.Y + 230);
            saferoomLight3Pos = new Vector2(saferoomPos.X, saferoomPos.Y + 660);
            saferoomLight4Pos = new Vector2(saferoomPos.X + 630, saferoomPos.Y + 660);
            saferoomLight5Pos = new Vector2(saferoomPos.X + 315, saferoomPos.Y + 425);
            saferoomLightPositions.Add(saferoomLight1Pos);
            saferoomLightPositions.Add(saferoomLight2Pos);
            saferoomLightPositions.Add(saferoomLight3Pos);
            saferoomLightPositions.Add(saferoomLight4Pos);
            saferoomLightPositions.Add(saferoomLight5Pos);
            saferoomLightIntensity = .5f;
            saferoomLightScale = new Vector2(700, 700);
        }

        public void Update(GameTime gameTime)
        {
            saferoomLightColor = new Color(r, g, b);

            if (X.player.FootHitbox.Intersects(saferoomHitBox))
            {
                visible = true;
            }
            else
            {
                visible = false;
            }

            
            if(visible)
            {
                LightsOn();
                
                foreach (Weapon w in X.player.weapons)
                {
                    w.enabled = false;
                    w.power = .9f;
                }
                foreach (Light l in X.player.playerLights)
                {
                    l.Enabled = false;
                }

                X.player.canChangeWeapon = false;

            }
            else
            {
                LightsOff();
                X.player.canChangeWeapon = true;
            }
        }

        public void LightsOn()
        {
            if (r <= 255)
            {
                r += 2;
            }
            if (b <= 255)
            {
                b += 2;
            }
            if (g <= 255)
            {
                g += 2;
            }
        }

        public void LightsOff()
        {
            if (r >= 0)
            {
                r -= 2;
            }
            if (b >= 0)
            {
                b -= 2;
            }
            if (g >= 0)
            {
                g -= 2;
            }
        }


    }
}
