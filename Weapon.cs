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
    public class Weapon
    {
        Color color;
        float Power;
        int r, g, b;

        public Weapon()
        {
            color = new Color(r, g, b);
        }
    }
}
