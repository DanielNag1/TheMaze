using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public static class SoundManager
    {
        public static SoundEffect StepsSFX { get; private set; }
        public static SoundEffect SafeRoomBGM { get; private set; }


        public static void LoadContent(ContentManager content)
        {
            StepsSFX = content.Load<SoundEffect>("walking");


        }

    }
}
