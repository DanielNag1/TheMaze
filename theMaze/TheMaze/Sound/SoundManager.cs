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
        // BGM 
        public static SoundEffect AmbientNoise { get; private set; }
        public static SoundEffect DarkSoulsTrack31 { get; private set; }

        // SFX
        public static SoundEffect Footstep1 { get; private set; }
        public static SoundEffect Footstep2 { get; private set; }

        public static SoundEffect Footstep3 { get; private set; }
        public static SoundEffect Footstep4 { get; private set; }

        public static SoundEffect LampSwitchOn { get; private set; }
        public static SoundEffect LampSwitchOff { get; private set; }
        public static SoundEffect CreepySoundHigh { get; private set; }
        public static SoundEffect CreepySoundLow { get; private set; }

        public static SoundEffect TakeCollectible { get; private set; }
        public static SoundEffect PickWeapon { get; private set; }

        public static SoundEffect WhiteAmbient { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            // BGM
            AmbientNoise = content.Load<SoundEffect>("Audio/BGM/ambientnoise");
            DarkSoulsTrack31 = content.Load<SoundEffect>("Audio/BGM/darksoulsamomentspeace");

            // SFX
            Footstep1 = content.Load<SoundEffect>("Audio/SFX/step1");
            Footstep2 = content.Load<SoundEffect>("Audio/SFX/step2");
            Footstep3 = content.Load<SoundEffect>("Audio/SFX/step3");
            Footstep4 = content.Load<SoundEffect>("Audio/SFX/step4");

            LampSwitchOn = content.Load<SoundEffect>("Audio/SFX/lampswitchon");
            LampSwitchOff = content.Load<SoundEffect>("Audio/SFX/lampswitchoff");
            CreepySoundHigh = content.Load<SoundEffect>("Audio/SFX/creepysoundhigh");
            CreepySoundLow = content.Load<SoundEffect>("Audio/SFX/creepysoundlow");

            TakeCollectible = content.Load<SoundEffect>("Audio/SFX/takeCollectible");
            PickWeapon = content.Load<SoundEffect>("Audio/SFX/weaponpick");

            WhiteAmbient = content.Load<SoundEffect>("Audio/BGM/whiteambient");
        }

    }
}

