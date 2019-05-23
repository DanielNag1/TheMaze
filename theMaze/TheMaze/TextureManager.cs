using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
namespace TheMaze
{
    static class TextureManager
    {
        public static Texture2D MainMenuTex { get; private set; }
        public static Texture2D ControlsMenuTex { get; private set; }
        public static Texture2D PauseScreen { get; private set; }
        public static Texture2D KilledScreen { get; private set; }

        public static Texture2D PlayerTex { get; private set; }
        public static Texture2D MonsterTex { get; private set; }
        public static Texture2D ImbakuTex { get; private set; }
        public static Texture2D MiniMonsterTex { get; private set; }
        public static Texture2D WallMonsterTex { get; private set; }
        public static Texture2D StalkerTex { get; private set; }
        public static Texture2D GolemTex { get; private set; }
        public static Texture2D ArmMonsterTex { get; private set; }


        public static Texture2D WaterTileTex { get; private set; }

        public static Texture2D FloorTileTex { get; private set; }

        public static Texture2D Vitadelen { get; private set; }
        public static Texture2D WhiteWallTex { get; private set; }
        public static Texture2D HallWayBackground { get; private set; }
        public static Texture2D Svartadelen { get; private set; }
        public static Texture2D WhiteTopWallSheetTex { get; private set; }


        public static Texture2D WallSheetTex { get; private set; }
        public static Texture2D TopWallSheetTex { get; private set; }
        public static Texture2D CollectibleTex { get; private set; }
        public static Texture2D CollectibleTex2 { get; private set; }
        public static Texture2D CollectibleMenu { get; private set; }
        
        public static Texture2D TransparentTex { get; private set; }
        public static Texture2D RedTexture { get; private set; }

        public static Texture2D RedHPTexture { get; private set; }

        public static Texture2D DeskTexture { get; private set; }

        public static List<Texture2D> hitParticles { get; private set; }
        public static List<Texture2D> storyTextures { get; private set; }
        public static Texture2D particle1 { get; private set; }
        public static Texture2D particle2 { get; private set; }
        public static Texture2D storyTexture1 { get; private set; }
        public static Texture2D storyTexture2 { get; private set; }
        public static Texture2D storyTexture3 { get; private set; }
        public static Texture2D storyTexture4 { get; private set; }
        public static Texture2D storyTexture5 { get; private set; }
        public static Texture2D storyTexture6 { get; private set; }
        public static Texture2D storyTexture7 { get; private set; }
        public static Texture2D storyTexture8 { get; private set; }
        public static Texture2D storyTexture9 { get; private set; }
        public static Video Cutscene { get; private set; }


        public static SpriteFont TimesNewRomanFont { get; private set; }
        public static SpriteFont TutorialFont { get; private set; }

        //public static List<Texture2D> particleTextures = new List<Texture2D>();

        public static void LoadContent(ContentManager Content)
        {
            hitParticles = new List<Texture2D>();
            storyTextures = new List<Texture2D>();
            PlayerTex = Content.Load<Texture2D>("characterspritesheet1");
            MonsterTex = Content.Load<Texture2D>("evilcat"); // Placeholder evil cat texture
            ImbakuTex = Content.Load<Texture2D>("spritesheet_Imbaku");
            MiniMonsterTex = Content.Load<Texture2D>("spritesheet_mini_imbaku");
            WallMonsterTex = Content.Load<Texture2D>("wallmonster2.2");
            StalkerTex = Content.Load<Texture2D>("stalker_spritesheet");
            GolemTex = Content.Load<Texture2D>("golem");
            ArmMonsterTex = Content.Load<Texture2D>("armmonster_spritesheet2");

            FloorTileTex = Content.Load<Texture2D>("floor tile 2");
            WaterTileTex = Content.Load<Texture2D>("water tile");
            WallSheetTex = Content.Load<Texture2D>("wall tile 2");
            TopWallSheetTex = Content.Load<Texture2D>("top tile sprite 2");
            CollectibleTex = Content.Load<Texture2D>("collectible");
            CollectibleTex2 = Content.Load<Texture2D>("collectible view");
            CollectibleMenu = Content.Load<Texture2D>("collectiblemenu");

            MainMenuTex = Content.Load<Texture2D>("mainmenu");
            ControlsMenuTex = Content.Load<Texture2D>("controlsmenu");
            PauseScreen = Content.Load<Texture2D>("pauseScreen");
            RedTexture = Content.Load<Texture2D>("red");
            TransparentTex = Content.Load<Texture2D>("transparent");
            KilledScreen = Content.Load<Texture2D>("You Died");

            TimesNewRomanFont = Content.Load<SpriteFont>("timesnewroman");
            TutorialFont = Content.Load<SpriteFont>("tutorialfont");

            particle1 = Content.Load<Texture2D>("particle1");
            particle2 = Content.Load<Texture2D>("particle2");
            hitParticles.Add(particle1);
            hitParticles.Add(particle2);

            storyTexture1 = Content.Load<Texture2D>("storyTexture1");
            storyTexture2 = Content.Load<Texture2D>("storyTexture2");
            storyTexture3 = Content.Load<Texture2D>("storyTexture3");
            storyTexture4 = Content.Load<Texture2D>("storyTexture4");
            storyTexture5 = Content.Load<Texture2D>("storyTexture5");
            storyTexture6 = Content.Load<Texture2D>("storyTexture6");
            storyTexture7 = Content.Load<Texture2D>("storyTexture7");
            storyTexture8 = Content.Load<Texture2D>("storyTexture8");
            storyTexture9 = Content.Load<Texture2D>("storyTexture9");


            storyTextures.Add(storyTexture1);
            storyTextures.Add(storyTexture2);
            storyTextures.Add(storyTexture3);
            storyTextures.Add(storyTexture4);
            storyTextures.Add(storyTexture5);
            storyTextures.Add(storyTexture6);
            storyTextures.Add(storyTexture7);
            storyTextures.Add(storyTexture8);
            storyTextures.Add(storyTexture9);

            DeskTexture = Content.Load<Texture2D>("desk");


            Vitadelen = Content.Load<Texture2D>("whitehallwayforeground");
            WhiteTopWallSheetTex = Content.Load<Texture2D>("white top tile sprite 2");
            WhiteWallTex = Content.Load<Texture2D>("whitewalltile");
            WhiteWallTex = Content.Load<Texture2D>("whitewalltileflower");
            HallWayBackground = Content.Load<Texture2D>("whitehallwaybackground1");
            Svartadelen = Content.Load<Texture2D>("hallwaybackground2");

            Cutscene = Content.Load<Video>("Cutscene final wmv");

            RedHPTexture = Content.Load<Texture2D>("redhp");
        }
    }
}
