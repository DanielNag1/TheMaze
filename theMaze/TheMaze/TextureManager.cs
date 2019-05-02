using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    static class TextureManager
    {
        public static Texture2D CatTex { get; private set; }
        public static Texture2D MonsterTex { get; private set; }
        public static Texture2D Monster2Tex { get; private set; }
        public static Texture2D FlareTex { get; private set; }
        public static Texture2D RangeTex { get; private set; }
        public static Texture2D FloorTileTex { get; private set; }
        public static Texture2D WallSheetTex { get; private set; }
        public static Texture2D TopWallSheetTex { get; private set; }
        public static Texture2D CollectibleTex { get; private set; }
        public static Texture2D VignetteOverlay { get; private set; }
        public static Texture2D LetterboxOverlay { get; private set; }
        public static Texture2D DarkOverlay { get; private set; }
        public static Texture2D CollectibleMenu { get; private set; }

        public static List<Texture2D> particleTextures = new List<Texture2D>();

        public static void LoadContent(ContentManager Content)
        {
            CatTex = Content.Load<Texture2D>("character1trans");
            MonsterTex = Content.Load<Texture2D>("evilcat"); // Placeholder evil cat texture
            Monster2Tex = Content.Load<Texture2D>("spritesheet1.1");
            FlareTex = Content.Load<Texture2D>("Flare5_00000");
            RangeTex = Content.Load<Texture2D>("rangecircle");
            FloorTileTex = Content.Load<Texture2D>("Floor Tile Big");
            WallSheetTex = Content.Load<Texture2D>("Wall Tile Sheet Big - Copy");
            TopWallSheetTex = Content.Load<Texture2D>("Top Tile Sheet Big");
            CollectibleTex = Content.Load<Texture2D>("collectible");
            particleTextures.Add(Content.Load<Texture2D>("particle2"));
            VignetteOverlay = Content.Load<Texture2D>("vignette");
            LetterboxOverlay = Content.Load<Texture2D>("letterbox");
            DarkOverlay = Content.Load<Texture2D>("darkOverlay");
            CollectibleMenu = Content.Load<Texture2D>("collectiblemenu");
        }
    }
}
