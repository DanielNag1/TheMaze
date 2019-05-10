﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TheMaze
{
    static class TextureManager
    {
        public static Texture2D MainMenuTex { get; private set; }
        public static Texture2D PauseScreen { get; private set; }
        public static Texture2D KilledScreen { get; private set; }

        public static Texture2D PlayerTex { get; private set; }
        public static Texture2D MonsterTex { get; private set; }
        public static Texture2D ImbakuTex { get; private set; }
        //public static Texture2D FlareTex { get; private set; }
        //public static Texture2D RangeTex { get; private set; }
        public static Texture2D FloorTileTex { get; private set; }
        public static Texture2D WallSheetTex { get; private set; }
        public static Texture2D TopWallSheetTex { get; private set; }
        public static Texture2D CollectibleTex { get; private set; }
        public static Texture2D CollectibleTex2 { get; private set; }
        public static Texture2D CollectibleMenu { get; private set; }
        //public static Texture2D VignetteOverlay { get; private set; }
        //public static Texture2D LetterboxOverlay { get; private set; }
        //public static Texture2D DarkOverlay { get; private set; }
        //public static Texture2D CollectibleMenu { get; private set; }
        //public static Texture2D StoryPanelTex { get; private set; }

        public static Texture2D TransparentTex { get; private set; }
        public static Texture2D RedTexture { get; private set; }

        public static List<Texture2D> hitParticles { get; private set; }
        public static Texture2D particle1 { get; private set; }
        public static Texture2D particle2 { get; private set; }


        public static SpriteFont TimesNewRomanFont { get; private set; }

        //public static List<Texture2D> particleTextures = new List<Texture2D>();

        public static void LoadContent(ContentManager Content)
        {
            hitParticles = new List<Texture2D>();

            PlayerTex = Content.Load<Texture2D>("characterspritesheet1");
            MonsterTex = Content.Load<Texture2D>("evilcat"); // Placeholder evil cat texture
            ImbakuTex = Content.Load<Texture2D>("spritesheet1.4");
            //FlareTex = Content.Load<Texture2D>("Flare5_00000");
            //RangeTex = Content.Load<Texture2D>("rangecircle");
            FloorTileTex = Content.Load<Texture2D>("floor tile 2");
            WallSheetTex = Content.Load<Texture2D>("wall tile 2");
            TopWallSheetTex = Content.Load<Texture2D>("top tile sprite 2");
            CollectibleTex = Content.Load<Texture2D>("collectible");
            CollectibleTex2 = Content.Load<Texture2D>("collectible text");
            CollectibleMenu = Content.Load<Texture2D>("collectiblemenu");
            //particleTextures.Add(Content.Load<Texture2D>("particle2"));
            //VignetteOverlay = Content.Load<Texture2D>("vignette");
            //LetterboxOverlay = Content.Load<Texture2D>("letterbox");
            //DarkOverlay = Content.Load<Texture2D>("darkOverlay");
            //CollectibleMenu = Content.Load<Texture2D>("collectiblemenu2");
            //StoryPanelTex = Content.Load<Texture2D>("storypanel");
            MainMenuTex = Content.Load<Texture2D>("mainmenu");
            PauseScreen = Content.Load<Texture2D>("pauseScreen");
            RedTexture = Content.Load<Texture2D>("red");
            TransparentTex = Content.Load<Texture2D>("transparent");
            KilledScreen = Content.Load<Texture2D>("You Died");
            TimesNewRomanFont = Content.Load<SpriteFont>("timesnewroman");

            particle1 = Content.Load<Texture2D>("particle1");
            particle2 = Content.Load<Texture2D>("particle2");
            hitParticles.Add(particle1);
            hitParticles.Add(particle2);

        }
    }
}
