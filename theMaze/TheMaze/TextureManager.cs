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
        public static Texture2D FlareTex { get; private set; }
        public static Texture2D FloorTileTex { get; private set; }
        public static Texture2D WallSheetTex { get; private set; }

        public static void LoadContent(ContentManager Content)
        {
            CatTex = Content.Load<Texture2D>("character1trans");
            FlareTex = Content.Load<Texture2D>("Flare5_00000");
            FloorTileTex = Content.Load<Texture2D>("floor tile4");
            WallSheetTex = Content.Load<Texture2D>("wall tileset big");
        }
    }
}
