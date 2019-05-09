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
        public Vector2 weaponLight1Pos, weaponLight2Pos, weaponLight3Pos, healingPos;
        public Rectangle weaponLight1Rectangle, weaponLight2Rectangle, weaponLight3Rectangle;
        public Vector2 saferoomLightScale,saferoomWeaponLightScale;
        public float saferoomLightIntensity,saferoomWeaponLightIntensity;
        public Rectangle saferoomHitBox,healingHitbox;
        public List<Vector2> saferoomLightPositions,saferoomWeaponLightPositions;
        public bool visible;
        public int r, g, b;
        public Color saferoomLightColor,weaponLight1Color,weaponLight2Color,weaponLight3Color;
        
        public Saferoom(LevelManager levelManager)
        {
            saferoomLightPositions = new List<Vector2>();
            saferoomWeaponLightPositions = new List<Vector2>();

            saferoomPos = new Vector2(levelManager.StartPositionSafeRoom.X, levelManager.StartPositionSafeRoom.Y - ConstantValues.tileHeight);
            saferoomHitBox = new Rectangle((int)saferoomPos.X,(int)saferoomPos.Y, 640, 956);

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


            weaponLight1Pos = new Vector2(saferoomPos.X + 125, saferoomPos.Y);
            weaponLight2Pos = new Vector2(saferoomPos.X + 325, saferoomPos.Y);
            weaponLight3Pos = new Vector2(saferoomPos.X + 525, saferoomPos.Y);

            saferoomWeaponLightPositions.Add(weaponLight1Pos);
            saferoomWeaponLightPositions.Add(weaponLight2Pos);
            saferoomWeaponLightPositions.Add(weaponLight3Pos);

            weaponLight1Rectangle = new Rectangle((int)weaponLight1Pos.X - 50, (int)weaponLight1Pos.Y - 50,100,100);
            weaponLight2Rectangle = new Rectangle((int)weaponLight2Pos.X - 50, (int)weaponLight2Pos.Y - 50, 100, 100);
            weaponLight3Rectangle = new Rectangle((int)weaponLight3Pos.X - 50, (int)weaponLight3Pos.Y - 50, 100, 100);

            weaponLight1Color = Color.Red;
            weaponLight2Color = Color.Goldenrod;
            weaponLight3Color = Color.MediumBlue;

            saferoomWeaponLightIntensity = .9f;
            saferoomWeaponLightScale = new Vector2(150, 150);
            
        }

        public void SelectWeapon()
        {
            if(X.mouseRect.Intersects(weaponLight1Rectangle) && X.mouseState.LeftButton==ButtonState.Pressed && X.oldmouseState.LeftButton==ButtonState.Released)
            {
                Player.selectedColor = weaponLight1Color;
                X.player.playerPointLight.Color = weaponLight1Color;
                
            }
            if (X.mouseRect.Intersects(weaponLight2Rectangle) && X.mouseState.LeftButton == ButtonState.Pressed && X.oldmouseState.LeftButton == ButtonState.Released)
            {
                Player.selectedColor = weaponLight2Color;
                X.player.playerPointLight.Color = weaponLight2Color;
                
            }
            if (X.mouseRect.Intersects(weaponLight3Rectangle) && X.mouseState.LeftButton == ButtonState.Pressed && X.oldmouseState.LeftButton == ButtonState.Released)
            {
                Player.selectedColor = weaponLight3Color;
                X.player.playerPointLight.Color = weaponLight3Color;
                
            }

            X.player.ApplyWeapon();

        }

        public void Update(GameTime gameTime)
        {
            saferoomLightColor = new Color(r, g, b);

            if (X.player.FootHitbox.Intersects(saferoomHitBox))
            {
                Player.markers = 15;
                visible = true;
            }
            else
            {
                visible = false;
                X.player.playerPointLight.Color = Color.White;
            }

            
            if(visible)
            {
                X.IsMouseVisible = true;
                LightsOn();

                saferoomWeaponLightIntensity = .9f;

                SelectWeapon();

                foreach (Weapon w in X.player.weapons)
                {
                    w.power = .9f;
                    w.enabled = false;
                }
                

                X.player.canChangeWeapon = false;

            }
            else
            {
                X.IsMouseVisible = false;
                LightsOff();
                X.player.canChangeWeapon = true;
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
