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

        SoundEffectInstance creepySoundLow, creepySoundHigh;

        private double stepTimer;
        private double imbakuTimer, imbakuTimerReset;

        private bool playFootstep1, playFootstep2;
        private bool playLampSwitchOn, playLampSwitchOff;
        public bool playCreepySoundHigh;

        public SFX()
        {
            currentFootstepPlayState = FootstepPlayState.Footstep1;

            creepySoundLow = SoundManager.CreepySoundLow.CreateInstance();
            creepySoundHigh = SoundManager.CreepySoundHigh.CreateInstance();

            stepTimer = 700;
            imbakuTimer = 45;
            imbakuTimerReset = 45;

            playFootstep1 = true;
            playFootstep2 = false;
            playLampSwitchOn = true;
            playLampSwitchOff = false;
            playCreepySoundHigh = true;
        }

        public void Footsteps(GameTime gameTime) //Prova med att göra SoundEffectInstance istället, så som BGM för saferoomBGM och ambientnoise
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

        public void GlitchEncounter()
        {
            creepySoundLow.Play();
        }

        public void GlitchEncounterOff()
        {
            if (creepySoundLow.State == SoundState.Playing)
            {
                creepySoundLow.Stop();
            }
        }

        public void ImbakuEncounter()
        {
            if (playCreepySoundHigh)
            {
                SoundManager.CreepySoundHigh.Play();
                playCreepySoundHigh = false;
            }
        }

        public void ImbakuEncounterTimer(GameTime gameTime)
        {
            if (!playCreepySoundHigh)
            {
                imbakuTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (imbakuTimer <= 0)
                {
                    playCreepySoundHigh = true;
                    imbakuTimer = imbakuTimerReset;
                }
            }
        }
    }
}
