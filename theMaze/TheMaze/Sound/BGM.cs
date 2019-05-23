using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    class BGM
    {
        private enum PlayState { InSafeRoom, OutsideSafeRoom }
        private PlayState currentPlayState;

        private SoundEffectInstance ambientNoise, safeRoomBGM,whiteBGM;

        private bool playAmbientNoise, playSafeRoomBGM;

        public BGM()
        {
            ambientNoise = SoundManager.AmbientNoise.CreateInstance();
            ambientNoise.Volume = 0.1f;
            safeRoomBGM = SoundManager.DarkSoulsTrack31.CreateInstance();
            whiteBGM = SoundManager.WhiteAmbient.CreateInstance();
            whiteBGM.Volume = 0.2f;
            ambientNoise.IsLooped = true;
            safeRoomBGM.IsLooped = true;

            currentPlayState = PlayState.InSafeRoom;

            playAmbientNoise = false;
            playSafeRoomBGM = true;
        }

        public void PlayBGM()
        {
            switch (GameStateManager.currentGameState)
            {
                case GameStateManager.GameState.MainMenu:
                    {

                        break;
                    }
                case GameStateManager.GameState.Play:
                    {
                        if (X.player.insaferoom)
                        {
                            //ambientNoise.Stop();

                            AmbientNoiseFadeOut();
                            FadeIn(safeRoomBGM);
                        }
                        else
                        {
                            FadeOut(safeRoomBGM);
                            AmbientNoiseFadeIn();
                        }

                        break;
                    }
            }
        }

        public void PlayWhiteBGM()
        {
            if (GamePlayManager.currentState == GamePlayManager.LevelState.Death)
            {
                FadeIn(whiteBGM);
            }
            else
            {
                FadeOut(whiteBGM);
            }
        }
        private void FadeIn(SoundEffectInstance BGM)
        {
            if (BGM.State != SoundState.Playing)
            {
                BGM.Volume = 0.1f;
                BGM.Play();
            }
            if (BGM.Volume < 0.1f)
            {
                BGM.Volume += 0.01f;
            }
        }
        private void FadeOut(SoundEffectInstance BGM)
        {
            if (BGM.Volume > 0.001f)
            {
                BGM.Volume -= 0.001f;
            }
            else if (BGM.Volume <= 0.001f)
            {
                BGM.Stop();
            }
        }
        //private void SafeRoomBGMFadeIn()
        //{
        //    if (safeRoomBGM.State != SoundState.Playing)
        //    {
        //        safeRoomBGM.Volume = 0.1f;
        //        safeRoomBGM.Play();
        //    }
        //    if (safeRoomBGM.Volume < 0.1f)
        //    {
        //        safeRoomBGM.Volume += 0.01f;
        //    }
        //}

        //private void SafeRoomBGMFadeOut()
        //{
        //    if (safeRoomBGM.Volume > 0.001f)
        //    {
        //        safeRoomBGM.Volume -= 0.001f;
        //    }
        //    else if (safeRoomBGM.Volume <= 0.001f)
        //    {
        //        safeRoomBGM.Stop();
        //    }
        //}

        private void AmbientNoiseFadeIn()
        {
            if (ambientNoise.State != SoundState.Playing)
            {
                ambientNoise.Volume = 0f;
                ambientNoise.Play();
            }
            if (ambientNoise.Volume < 0.1f)
            {
                ambientNoise.Volume += 0.01f;
            }
        }

        private void AmbientNoiseFadeOut()
        {
            if (ambientNoise.Volume > 0.001f)
            {
                ambientNoise.Volume -= 0.01f;
            }
            else if (ambientNoise.Volume <= 0.001f)
            {
                ambientNoise.Stop();
            }
        }

    }
}
