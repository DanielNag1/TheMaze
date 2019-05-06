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
        public static Texture2D rangeTex { get; private set; }
        public static Texture2D FloorTileTex { get; private set; }
        public static Texture2D WallSheetTex { get; private set; }
        public static Texture2D TopWallSheetTex { get; private set; }
        public static Texture2D hitboxPosTex { get; private set; }
        public static List<Texture2D> particleTextures = new List<Texture2D>();

        public static void LoadContent(ContentManager Content)
        {
            CatTex = Content.Load<Texture2D>("characterspritesheet1");
            MonsterTex = Content.Load<Texture2D>("evilcat"); // Placeholder evil cat texture
            Monster2Tex = Content.Load<Texture2D>("spritesheet1.4");
            FlareTex = Content.Load<Texture2D>("Flare5_00000");
            rangeTex = Content.Load<Texture2D>("rangecircle");
            FloorTileTex = Content.Load<Texture2D>("floor tile 2");
            WallSheetTex = Content.Load<Texture2D>("wall tile 2");
            TopWallSheetTex = Content.Load<Texture2D>("top tile sprite 2");
            hitboxPosTex = Content.Load<Texture2D>("hitboxpos");
            
            particleTextures.Add(Content.Load<Texture2D>("particle2"));
        }
    }
}
