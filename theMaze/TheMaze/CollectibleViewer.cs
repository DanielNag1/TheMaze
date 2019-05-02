using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheMaze
{
    class CollectibleViewer
    {
        Overlay overlay;
        public CollectibleViewer(Overlay overlay)
        {
            this.overlay = overlay;
        }

        public void DrawMenu(SpriteBatch spriteBatch)
        {
            overlay.DrawMenuOverlay(spriteBatch);

        }

        public void DrawCollectibles(SpriteBatch spriteBatch)
        {

        }
    }
}
