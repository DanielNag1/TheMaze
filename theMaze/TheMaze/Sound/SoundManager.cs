using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public static class SoundManager
    {
        public static Song AmbientNoise { get; private set; }
        public static SoundEffect Footstep1 { get; private set; }
        public static SoundEffect Footstep2 { get; private set; }
        public static SoundEffect LampSwitchOn { get; private set; }
        public static SoundEffect LampSwitchOff { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            Footstep1 = content.Load<SoundEffect>("Audio/SFX/step1");
            Footstep2 = content.Load<SoundEffect>("Audio/SFX/step2");
            LampSwitchOn = content.Load<SoundEffect>("Audio/SFX/lampswitchon");
            LampSwitchOff = content.Load<SoundEffect>("Audio/SFX/lampswitchoff");
            AmbientNoise = content.Load<Song>("Audio/BGM/ambientnoise");
        }

    }
}
