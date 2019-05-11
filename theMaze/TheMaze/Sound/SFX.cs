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
        private enum FootstepPlayState { Footstep1, Footstep2 }
        private FootstepPlayState currentFootstepPlayState;

        private double stepTimer;
        private bool playFootstep1, playFootstep2;
        private bool playLampSwitchOn, playLampSwitchOff;

        public SFX()
        {
            currentFootstepPlayState = FootstepPlayState.Footstep1;

            stepTimer = 700;

            playFootstep1 = true;
            playFootstep2 = false;
            playLampSwitchOn = true;
            playLampSwitchOff = false;
        }

        public void Footsteps(GameTime gameTime)
        {
            switch (currentFootstepPlayState)
            {
                case FootstepPlayState.Footstep1:
                    {
                        if (playFootstep1)
                        {
                            stepTimer = 700;

                            SoundManager.Footstep1.Play();

                            playFootstep1 = false;   
                        }
                        else
                        {
                            stepTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (stepTimer <= 0)
                            {
                                playFootstep2 = true;
                                currentFootstepPlayState = FootstepPlayState.Footstep2;
                            }
                        }

                        break;
                    }
                case FootstepPlayState.Footstep2:
                    {
                        if (playFootstep2)
                        {
                            stepTimer = 700;

                            SoundManager.Footstep2.Play();

                            playFootstep2 = false;
                        }
                        else
                        {
                            stepTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (stepTimer <= 0)
                            {
                                playFootstep1 = true;
                                currentFootstepPlayState = FootstepPlayState.Footstep1;
                            }
                        }

                        break;
                    }
            }
        }

        public void LampSwitchOn()
        {
            if (playLampSwitchOn)
            {
                SoundManager.LampSwitchOn.Play();
                playLampSwitchOn = false;
                playLampSwitchOff = true;
            }
        }

        public void LampSwitchOff()
        {
            if (playLampSwitchOff)
            {
                SoundManager.LampSwitchOff.Play();
                playLampSwitchOff = false;
                playLampSwitchOn = true;
            }
        }
    }
}
