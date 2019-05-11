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
        private enum PlayState { Step1, Step2 }
        private PlayState currentPlayState;

        private double stepTimer;
        private bool playStep1, playStep2;

        public SFX()
        {
            currentPlayState = PlayState.Step1;

            stepTimer = 700;

            playStep1 = true;
            playStep2 = false;
        }

        public void Footsteps(GameTime gameTime)
        {
            switch (currentPlayState)                 //efter andra steget så tar det tre sekunder att spela upp första steget igen
            {
                case PlayState.Step1:
                    {
                        if (playStep1)
                        {
                            stepTimer = 700;

                            SoundManager.step1.Play();

                            playStep1 = false;   
                        }
                        else
                        {
                            stepTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (stepTimer <= 0)
                            {
                                playStep2 = true;
                                currentPlayState = PlayState.Step2;
                            }
                        }

                        break;
                    }
                case PlayState.Step2:
                    {
                        if (playStep2)
                        {
                            stepTimer = 700;

                            SoundManager.step2.Play();

                            playStep2 = false;
                        }
                        else
                        {
                            stepTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (stepTimer <= 0)
                            {
                                playStep1 = true;
                                currentPlayState = PlayState.Step1;
                            }
                        }

                        break;
                    }
            }
        }
    }
}
