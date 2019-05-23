using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TheMaze
{
    class CollectibleViewer
    {
        private StoryButton collectibleButton;
        public List<StoryButton> collectibleButtons;

        private Vector2 startPos;
        private int numberToWrite, numberOfAvailableButtons;
        public bool inMenu;

        enum View { menu, story }
        View currentView = View.menu;

        public CollectibleViewer()
        {
            collectibleButtons = new List<StoryButton>();
            CreateButtons();
        }

        public void Update()
        {
            switch (currentView)
            {
                case View.menu:
                    MenuUpdate();
                    ShowRightTexture();
                    break;
                case View.story:
                    StoryUpdate();
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(TextureManager.CollectibleMenu, Vector2.Zero, Color.White);
            switch (currentView)
            {
                case View.menu:

                    foreach (StoryButton b in collectibleButtons)
                    {
                        b.Draw(spriteBatch);
                    }

                    break;
                case View.story:
                    DrawStory(spriteBatch);
                    break;
            }
            spriteBatch.End();
        }

        public void CreateButtons()
        {
            collectibleButtons.Clear();
            startPos = new Vector2(0, 125);
            numberToWrite = 0;
            foreach (Collectible c in X.player.collectibles)
            {
                startPos.X += 180;
                numberToWrite++;
                collectibleButton = new StoryButton(TextureManager.CollectibleTex2, startPos, numberToWrite);
                collectibleButtons.Add(collectibleButton);
            }

        }

        public void ShowRightTexture()
        {

            if (collectibleButtons.Count > 0)
            {
                if (collectibleButtons[numberToWrite - 1].IsClicked())
                {
                    currentView = View.story;
                    numberOfAvailableButtons = numberToWrite - 1;

                }
            }
            if (collectibleButtons.Count > 1)
            {
                if (collectibleButtons[numberToWrite - 2].IsClicked())
                {
                    currentView = View.story;
                    numberOfAvailableButtons = numberToWrite - 2;

                }
            }
            if (collectibleButtons.Count > 2)
            {
                if (collectibleButtons[numberToWrite - 3].IsClicked())
                {
                    currentView = View.story;
                    numberOfAvailableButtons = numberToWrite - 3;

                }
            }
            if (collectibleButtons.Count > 3)
            {
                if (collectibleButtons[numberToWrite - 4].IsClicked())
                {
                    currentView = View.story;
                    numberOfAvailableButtons = numberToWrite - 4;

                }
            }
            if (collectibleButtons.Count > 4)
            {
                if (collectibleButtons[numberToWrite - 5].IsClicked())
                {
                    currentView = View.story;
                    numberOfAvailableButtons = numberToWrite - 5;

                }
            }
            if (collectibleButtons.Count > 5)
            {
                if (collectibleButtons[numberToWrite - 6].IsClicked())
                {
                    currentView = View.story;
                    numberOfAvailableButtons = numberToWrite - 6;

                }
            }
            if (collectibleButtons.Count > 6)
            {
                if (collectibleButtons[numberToWrite - 7].IsClicked())
                {
                    currentView = View.story;
                    numberOfAvailableButtons = numberToWrite - 7;

                }
            }
            if (collectibleButtons.Count > 7)
            {
                if (collectibleButtons[numberToWrite - 8].IsClicked())
                {
                    currentView = View.story;
                    numberOfAvailableButtons = numberToWrite - 8;

                }
            }
            if (collectibleButtons.Count > 8)
            {
                if (collectibleButtons[numberToWrite - 9].IsClicked())
                {
                    currentView = View.story;
                    numberOfAvailableButtons = numberToWrite - 9;

                }
            }
            if (collectibleButtons.Count > 9)
            {
                if (collectibleButtons[numberToWrite - 10].IsClicked())
                {
                    currentView = View.story;
                    numberOfAvailableButtons = numberToWrite - 10;

                }
            }


        }
        public void DrawStory(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.storyTextures[numberOfAvailableButtons], new Vector2(500, 0), Color.White);
        }

        public void MenuUpdate()
        {
            inMenu = true;
        }

        public void StoryUpdate()
        {
            inMenu = false;
            if (X.IsKeyPressed(Keys.Space))
            {
                currentView = View.menu;
            }
        }
    }
}






