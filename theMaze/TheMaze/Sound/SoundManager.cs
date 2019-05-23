﻿using Microsoft.Xna.Framework.Audio;
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
        

        public static SoundEffect GetHit { get; private set; }
        public static SoundEffect Suicide { get; private set; }
        public static SoundEffect Sprint { get; private set; }
        public static SoundEffect LowHP { get; private set; }

        public static SoundEffect GolemScream { get; private set; }
        public static SoundEffect GolemSong { get; private set; }

        public static SoundEffect StalkerGrowlFar { get; private set; }
        public static SoundEffect StalkerGrowlNear { get; private set; }

        public static SoundEffect ArmMonsterCrackle { get; private set; }
        public static SoundEffect GlitchMonsterSound { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            // BGM
            AmbientNoise = content.Load<SoundEffect>("Audio/BGM/ambientsound");
            DarkSoulsTrack31 = content.Load<SoundEffect>("Audio/BGM/darksoulsamomentspeace");
            WhiteAmbient = content.Load<SoundEffect>("Audio/BGM/whiteambient");

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

            GetHit = content.Load<SoundEffect>("Audio/SFX/gettingHit");
            Suicide = content.Load<SoundEffect>("Audio/SFX/suicide");
            Sprint = content.Load<SoundEffect>("Audio/SFX/run");
            LowHP = content.Load<SoundEffect>("Audio/SFX/lowhp2");

            GolemScream = content.Load<SoundEffect>("Audio/SFX/psychoscream");
            GolemSong = content.Load<SoundEffect>("Audio/SFX/golemsong");

            ArmMonsterCrackle = content.Load<SoundEffect>("Audio/SFX/metalclang");
            GlitchMonsterSound = content.Load<SoundEffect>("Audio/SFX/glitch");

            //StalkerGrowlFar = content.Load<SoundEffect>("Audio/SFX/whispering");
            StalkerGrowlNear = content.Load<SoundEffect>("Audio/SFX/breathingghost");

        }

    }
}

