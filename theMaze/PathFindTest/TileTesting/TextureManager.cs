using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileTesting
{
    static class TextureManager
    {
        public static Texture2D FloorTileTex { get; private set; }
        public static Texture2D WallSheetTex { get; private set; }
        public static Texture2D TopWallSheetTex { get; private set; }

        public static Texture2D CatSheetTex { get; private set; }

        public static Texture2D CatTex { get; private set; }
        public static Texture2D FishTex { get; private set; }

        public static void LoadContent(ContentManager Content)
        {
            FloorTileTex = Content.Load<Texture2D>("Floor Tile Big");
            WallSheetTex = Content.Load<Texture2D>("Wall Tile Sheet Big");
            TopWallSheetTex = Content.Load<Texture2D>("Top Tile Sheet Big");

            CatSheetTex = Content.Load<Texture2D>("cat sprite big2");

            CatTex = Content.Load<Texture2D>("cat");
            FishTex = Content.Load<Texture2D>("Fish Big");
        }
    }
}
