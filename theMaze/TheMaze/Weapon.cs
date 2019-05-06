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
        public Color color;
        public float power;
        public int r, g, b;
        public bool enabled;

        public Weapon()
        {
            r = 0;
            g = 0;
            b = 0;
            color = new Color(r, g, b);
            power = .9f;
            enabled = false;
        }
    }
}
