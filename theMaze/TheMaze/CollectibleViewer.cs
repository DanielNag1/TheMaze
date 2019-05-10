using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheMaze
{
    class CollectibleViewer
    {
        private Button collectibleButton;
        public List<Button> collectibleButtons;

        private Vector2 startPos;

        public CollectibleViewer()
        {
            collectibleButtons = new List<Button>();
            CreateButtons();
        }

        public void Update()
        {
            Console.WriteLine("Antal buttons: " + collectibleButtons.Count);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.CollectibleMenu, Vector2.Zero, Color.White);

            foreach (Button b in collectibleButtons)
            {
                b.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        public void CreateButtons()
        {
            collectibleButtons.Clear();
            startPos = new Vector2(150, 150);

            foreach (Collectible c in X.player.collectibles)
            {
                startPos.X += 250;
                collectibleButton = new Button(TextureManager.CollectibleTex2, startPos, TextureManager.TimesNewRomanFont, " ", Color.White);
                collectibleButtons.Add(collectibleButton);
            }

            }
        }

    }

