﻿using Microsoft.Xna.Framework.Content;
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
        public static Texture2D FlareTex { get; private set; }
        public static Texture2D rangeTex { get; private set; }
        public static Texture2D FloorTileTex { get; private set; }
        public static Texture2D WallSheetTex { get; private set; }
        public static Texture2D TopWallSheetTex { get; private set; }
        public static List<Texture2D> particleTextures = new List<Texture2D>();

        public static void LoadContent(ContentManager Content)
        {
            CatTex = Content.Load<Texture2D>("character1trans");
            MonsterTex = Content.Load<Texture2D>("evilcat"); // Placeholder evil cat texture
            FlareTex = Content.Load<Texture2D>("Flare5_00000");
            rangeTex = Content.Load<Texture2D>("rangecircle");
            FloorTileTex = Content.Load<Texture2D>("Floor Tile Big");
            WallSheetTex = Content.Load<Texture2D>("Wall Tile Sheet Big - Copy");
            TopWallSheetTex = Content.Load<Texture2D>("Top Tile Sheet Big");

            //particleTextures.Add(Content.Load<Texture2D>("particle1"));
            particleTextures.Add(Content.Load<Texture2D>("particle2"));
        }
    }
}
