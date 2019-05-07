using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class BGM
    {
        enum PlayState { mainMenu, playing, saferoom}
        PlayState playState;



        public BGM()
        {
            MediaPlayer.IsRepeating = true;
        }

    }
}
