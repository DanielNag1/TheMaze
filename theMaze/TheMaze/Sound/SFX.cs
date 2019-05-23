using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class SFX
    {
        private enum FootstepPlayState { Footstep1, Footstep2 }
        private FootstepPlayState currentFootstepPlayState;

        SoundEffectInstance creepySoundLow, creepySoundHigh;
        SoundEffectInstance takeCollectible, pickWeapon;
        SoundEffectInstance suicide, gettingHit, sprint, lowhp;

        SoundEffectInstance golemScream, golemSong;
        SoundEffectInstance stalkerGrowlNear,stalkerGrowlFar;
        SoundEffectInstance armMonsterCrackle;
        SoundEffectInstance glitchMonsterSound;
        SoundEffectInstance lightAttack;

        private double stepTimer;
        private double imbakuTimer, imbakuTimerReset;
        private double golemTimer, golemTimerReset, golemSongTimer, golemSongTimerReset;
        private double armMonsterTimer, armMonsterTimerReset;
        private int stalkerTimer, stalkerTimerReset;

        private bool playFootstep1, playFootstep2;
        private bool playLampSwitchOn, playLampSwitchOff;
        public bool playCreepySoundHigh, playCreepySoundLow;

        public bool playGolemScream, playArmMonsterCrackle;

        public SFX()
        {
            currentFootstepPlayState = FootstepPlayState.Footstep1;

            suicide = SoundManager.Suicide.CreateInstance();
            gettingHit = SoundManager.GetHit.CreateInstance();
            sprint = SoundManager.Sprint.CreateInstance();
            lowhp = SoundManager.LowHP.CreateInstance();
            lightAttack = SoundManager.LightAttack.CreateInstance();

            suicide.Volume = 0.2f;
            creepySoundLow = SoundManager.CreepySoundLow.CreateInstance();
            creepySoundHigh = SoundManager.CreepySoundHigh.CreateInstance();

            takeCollectible = SoundManager.TakeCollectible.CreateInstance();
            takeCollectible.Volume = 0.3f;
            pickWeapon = SoundManager.PickWeapon.CreateInstance();
            pickWeapon.Volume = 0.2f;
            stepTimer = 700;
            imbakuTimer = 120;
            imbakuTimerReset = 120;

            golemScream = SoundManager.GolemScream.CreateInstance();
            golemSong = SoundManager.GolemSong.CreateInstance();
            golemSong.Volume = 0.02f;

            stalkerGrowlFar = SoundManager.StalkerGrowlFar.CreateInstance();
            stalkerGrowlNear = SoundManager.StalkerGrowlNear.CreateInstance();

            armMonsterCrackle = SoundManager.ArmMonsterCrackle.CreateInstance();

            glitchMonsterSound = SoundManager.GlitchMonsterSound.CreateInstance();

            imbakuTimer = 120;
            imbakuTimerReset = 120;

            golemTimer = 0;
            golemTimerReset = 2000;
            golemSongTimer = 40;
            golemSongTimerReset = 70;
            armMonsterTimer = 0;
            armMonsterTimerReset = 5000;

            stalkerTimer = 12;
            stalkerTimerReset = 60;

            playFootstep1 = true;
            playFootstep2 = false;
            playLampSwitchOn = true;
            playLampSwitchOff = false;
            playCreepySoundHigh = true;
            playCreepySoundLow = true;
            playGolemScream = false;
        }

        public void Footsteps(GameTime gameTime) //Prova med att göra SoundEffectInstance istället, så som BGM för saferoomBGM och ambientnoise - kommentar från zirko till framtida zirko
        {
            switch (currentFootstepPlayState)
            {
                case FootstepPlayState.Footstep1:
                    {
                        if (playFootstep1)
                        {
                            if (sprint.State == SoundState.Playing)
                            {
                                stepTimer = 150;
                            }
                            else
                            {
                                stepTimer = 700;
                            }
                            if (GamePlayManager.currentState == GamePlayManager.LevelState.Live)
                            {
                                SoundManager.Footstep1.Play();
                            }
                            else
                            {
                                SoundManager.Footstep3.Play();
                            }
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
                            if (sprint.State == SoundState.Playing)
                            {
                                stepTimer = 150;
                            }
                            else
                            {
                                stepTimer = 700;
                            }
                            if (GamePlayManager.currentState == GamePlayManager.LevelState.Live)
                            {
                                SoundManager.Footstep2.Play();
                            }
                            else
                            {
                                SoundManager.Footstep4.Play();
                            }
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

        public void WallMonsterEncounterOn()
        {
            if (playCreepySoundLow)
            {
                creepySoundLow.Play();
                playCreepySoundLow = false;
            }
        }

        public void WallMonsterEncounterOff()
        {
            playCreepySoundLow = true;
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

        public void TakeItem()
        {
            takeCollectible.Play();
        }

        public void PickWeapon()
        {
            pickWeapon.Play();
        }

        public void GetHit()
        {
            gettingHit.Play();
        }

        public void Suicide()
        {
            suicide.Play();
        }

        public void Sprint()
        {
            sprint.Play();
        }

        public void StopSprint()
        {
            sprint.Stop();
        }

        public void LowHP()
        {
            lowhp.Play();
        }

        public void LightMonsterCollision()
        {
            lightAttack.Play();
        }

        public void GolemEncounter(GameTime gameTime)
        {

            if (playGolemScream)
            {
                SoundManager.GolemScream.Play();
                playGolemScream = false;
            }

            if (!playGolemScream)
            {
                golemTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (golemTimer <= 0)
                {
                    playGolemScream = true;
                    golemTimer = golemTimerReset;
                }
            }

        }

        public void GolemSong(GameTime gameTime)
        {
            golemSongTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (golemSongTimer <= 0)
            {
                SoundManager.GolemSong.Play();
                golemSongTimer = golemSongTimerReset;
            }
        }

        public void StalkerEncounter()
        {
            stalkerGrowlNear.Play();
        }

        public void StalkerWhispers(GameTime gameTime)
        {
            stalkerTimer -= (int)gameTime.ElapsedGameTime.TotalSeconds;

            if (stalkerTimer <= 0)
            {
                SoundManager.StalkerGrowlFar.Play();
                stalkerTimer = stalkerTimerReset;
            }
        }

        public void ArmMonsterEncounter(GameTime gameTime)
        {

            if (playArmMonsterCrackle)
            {
                SoundManager.ArmMonsterCrackle.Play();
                playArmMonsterCrackle = false;
            }

            if (!playArmMonsterCrackle)
            {
                armMonsterTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (armMonsterTimer <= 0)
                {
                    playArmMonsterCrackle = true;
                    armMonsterTimer = armMonsterTimerReset;
                }
            }


        }

        public void GlitchMonsterEncounter(GameTime gameTime)
        {
            glitchMonsterSound.Play();

        }
        public void GlitchMonsterStop()
        {
            glitchMonsterSound.Pause();
        }

    }
}
