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
    public static class TutorialManager
    {
        public static bool tutorialMovingDone,tutorialLampDone,onItem;
        private static bool w, a, s, d;
        public static bool q;
        public static bool e = true;
        public static void buttonPressCheck()
        {
            if(Utility.IsKeyPressed(Keys.W))
            {
                w = true;
            }
            if (Utility.IsKeyPressed(Keys.A))
            {
                a = true;
            }
            if (Utility.IsKeyPressed(Keys.S))
            {
                s = true;
            }
            if (Utility.IsKeyPressed(Keys.D))
            {
                d = true;
            }
            if (Utility.IsKeyPressed(Keys.Q))
            {
                q = true;
                e = false;
            }
            if (Utility.IsKeyPressed(Keys.E))
            {
                e = true;
            }
            if (w==true && a==true && s==true && d==true)
            {
                tutorialMovingDone = true;
            }
            if (q==true && e==true)
            {
                tutorialLampDone = true;
            }
        }
    }
}
