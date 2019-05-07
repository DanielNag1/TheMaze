using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class SFX
    {
        enum PlayState { Step1, Step2 }
        PlayState playState;

        private SoundEffectInstance step1;
        private SoundEffectInstance step2;

        double stepTimer;
        bool startStepTimer, playStep1, playStep2;

        public SFX()
        {
            playState = PlayState.Step1;

            step1 = SoundManager.step1.CreateInstance();
            step2 = SoundManager.step2.CreateInstance();

            stepTimer = 1000;

            startStepTimer = false;
            playStep1 = true;
            playStep2 = false;
        }

        public void Footsteps(GameTime gameTime)
        {
            switch (playState)                 //efter andra steget så tar det tre sekunder att spela upp första steget igen
            {
                case PlayState.Step1:
                    {
                        if (playStep1)
                        {
                            step1.Play();

                            playStep1 = false;
                            startStepTimer = true;
                        }

                        if (startStepTimer)
                        {
                            stepTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (stepTimer <= 0)
                            {
                                startStepTimer = false;
                                stepTimer = 1000;
                                playStep2 = true;
                                playState = PlayState.Step2;
                            }
                        }

                        break;
                    }
                case PlayState.Step2:
                    {
                        if (playStep2)
                        {
                            step2.Play();

                            playStep2 = false;
                            startStepTimer = true;
                        }

                        if (startStepTimer)
                        {
                            stepTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (stepTimer <= 0)
                            {
                                startStepTimer = false;
                                stepTimer = 1000;
                                playStep1 = true;
                                playState = PlayState.Step1;
                            }
                        }

                        break;
                    }
            }
        }
    }
}
