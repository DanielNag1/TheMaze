﻿using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class SFX
    {
        enum PlayState { playStep1, playStep2}
        PlayState playState;

        private SoundEffectInstance step1;
        private SoundEffectInstance step2;

        public SFX()
        {
            playState = PlayState.playStep1;

            step1 = SoundManager.step1.CreateInstance();
            step2 = SoundManager.step2.CreateInstance();
        }

        public void Walking()
        {
            switch (playState)
            {
                case PlayState.playStep1:
                    {
                        if (step1.State == SoundState.Stopped && step2.State == SoundState.Stopped)
                        {
                            step1.Play();
                            playState = PlayState.playStep2;
                        }

                        break;
                    }
                case PlayState.playStep2:
                    {
                        if (step1.State == SoundState.Stopped && step2.State == SoundState.Stopped)
                        {
                            step2.Play();
                            playState = PlayState.playStep1;
                        }

                        break;
                    }
            }
        }
    }
}
