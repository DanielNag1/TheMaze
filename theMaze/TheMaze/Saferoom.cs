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
        public Vector2 saferoomPos,saferoomLight1Pos,saferoomLight2Pos,saferoomLight3Pos,saferoomLight4Pos,saferoomLight5Pos,saferoomLight6Pos;
        public Vector2 weaponLight1Pos, weaponLight2Pos, healingPos,deskPos;
        public Rectangle weaponLight1Rectangle, weaponLight2Rectangle;
        public Vector2 saferoomLightScale,saferoomWeaponLightScale;
        public float saferoomLightIntensity,saferoomWeaponLightIntensity;
        public Rectangle saferoomHitBox,healingHitbox,deskHitbox;
        public List<Vector2> saferoomLightPositions,saferoomWeaponLightPositions;
        public bool visible;
        public int r, g, b;
        public Color saferoomLightColor, weaponLight1Color, weaponLight2Color;
        SFX sfx = new SFX();
        
        public Saferoom(LevelManager levelManager)
        {
            saferoomLightPositions = new List<Vector2>();
            saferoomWeaponLightPositions = new List<Vector2>();

            saferoomPos = new Vector2(levelManager.StartPositionSafeRoom.X, levelManager.StartPositionSafeRoom.Y - ConstantValues.tileHeight);
            saferoomHitBox = new Rectangle((int)saferoomPos.X, (int)saferoomPos.Y, 640, 956);

            deskPos = new Vector2(saferoomPos.X + 15, saferoomPos.Y + 350);
            deskHitbox = new Rectangle((int)deskPos.X, (int)deskPos.Y, 100, 200);
            
            saferoomLight1Pos = new Vector2(saferoomPos.X, saferoomPos.Y + 230);
            saferoomLight2Pos = new Vector2(saferoomPos.X + 630, saferoomPos.Y + 230);
            saferoomLight3Pos = new Vector2(saferoomPos.X, saferoomPos.Y + 660);
            saferoomLight4Pos = new Vector2(saferoomPos.X + 630, saferoomPos.Y + 660);
            saferoomLight5Pos = new Vector2(saferoomPos.X + 315, saferoomPos.Y + 335);
            saferoomLight6Pos = new Vector2(saferoomPos.X + 315, saferoomPos.Y + 635);


            saferoomLightPositions.Add(saferoomLight1Pos);
            saferoomLightPositions.Add(saferoomLight2Pos);
            saferoomLightPositions.Add(saferoomLight3Pos);
            saferoomLightPositions.Add(saferoomLight4Pos);
            saferoomLightPositions.Add(saferoomLight5Pos);
            saferoomLightPositions.Add(saferoomLight6Pos);

            saferoomLightIntensity = .5f;
            saferoomLightScale = new Vector2(700, 700);

            switch(GamePlayManager.currentLevel)
            {
                case GamePlayManager.Level.Level1:
                    break;
                case GamePlayManager.Level.Level2:
                    weaponLight1Pos = new Vector2(saferoomPos.X + 225, saferoomPos.Y);
                    weaponLight1Rectangle = new Rectangle((int)weaponLight1Pos.X - 50, (int)weaponLight1Pos.Y - 50, 100, 100);
                    saferoomWeaponLightPositions.Add(weaponLight1Pos);

                    break;
                case GamePlayManager.Level.Level3:
                    weaponLight1Pos = new Vector2(saferoomPos.X + 225, saferoomPos.Y);
                    weaponLight1Rectangle = new Rectangle((int)weaponLight1Pos.X - 50, (int)weaponLight1Pos.Y - 50, 100, 100);
                    saferoomWeaponLightPositions.Add(weaponLight1Pos);

                    weaponLight2Pos = new Vector2(saferoomPos.X + 425, saferoomPos.Y);
                    weaponLight2Rectangle = new Rectangle((int)weaponLight2Pos.X - 50, (int)weaponLight2Pos.Y - 50, 100, 100);
                    saferoomWeaponLightPositions.Add(weaponLight2Pos);

                    break;
                case GamePlayManager.Level.Level4:
                    weaponLight1Pos = new Vector2(saferoomPos.X + 225, saferoomPos.Y);
                    weaponLight1Rectangle = new Rectangle((int)weaponLight1Pos.X - 50, (int)weaponLight1Pos.Y - 50, 100, 100);
                    saferoomWeaponLightPositions.Add(weaponLight1Pos);

                    weaponLight2Pos = new Vector2(saferoomPos.X + 425, saferoomPos.Y);
                    weaponLight2Rectangle = new Rectangle((int)weaponLight2Pos.X - 50, (int)weaponLight2Pos.Y - 50, 100, 100);
                    saferoomWeaponLightPositions.Add(weaponLight2Pos);

                    break;
            }
            
            weaponLight1Color = Color.Goldenrod;
            weaponLight2Color = Color.Red;
            
            saferoomWeaponLightIntensity = .9f;
            saferoomWeaponLightScale = new Vector2(150, 150);

            sfx = new SFX();
        }

        public void SelectWeapon()
        {
            if(Utility.mouseRect.Intersects(weaponLight1Rectangle) && Utility.mouseState.LeftButton==ButtonState.Pressed && Utility.oldmouseState.LeftButton==ButtonState.Released)
            {
                Player.selectedColor = weaponLight1Color;
                Utility.player.playerPointLight.Color = weaponLight1Color;
            }

            if (Utility.mouseRect.Intersects(weaponLight2Rectangle) && Utility.mouseState.LeftButton == ButtonState.Pressed && Utility.oldmouseState.LeftButton == ButtonState.Released)
            {
                Player.selectedColor = weaponLight2Color;
                Utility.player.playerPointLight.Color = weaponLight2Color;
            }
            
            Utility.player.ApplyWeapon();
        }

        public void Update(GameTime gameTime)
        {
            saferoomLightColor = new Color(r, g, b);

            if (Utility.player.FootHitbox.Intersects(saferoomHitBox))
            {
                Utility.player.insaferoom = true;
                Utility.player.playerHealth = 3;
                Player.markers = 15;
                visible = true;

                if (Utility.mouseRect.Intersects(deskHitbox) && Utility.mouseState.LeftButton == ButtonState.Pressed && Utility.oldmouseState.LeftButton==ButtonState.Released)
                {
                    Utility.player.viewCollectible = true;
                }
                else
                {
                    Utility.player.viewCollectible = false;
                }
            }
            else
            {
                Utility.player.viewCollectible = false;
                Utility.player.insaferoom = false;
                visible = false;
                Utility.player.playerPointLight.Color = Color.White;
            }
            
            if(visible)
            {
                Utility.IsMouseVisible = true;
                LightsOn();

                saferoomWeaponLightIntensity = .9f;

                SelectWeapon();

                foreach (Weapon w in Utility.player.weapons)
                {
                    w.power = .9f;
                    w.enabled = false;
                }
                
                Utility.player.canChangeWeapon = false;
            }
            else
            {
                Utility.IsMouseVisible = false;
                LightsOff();
                Utility.player.canChangeWeapon = true;
                saferoomWeaponLightIntensity = 0f;
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
