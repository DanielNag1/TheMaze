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
        private enum PlayState { InSafeRoom, OutsideSafeRoom}
        private PlayState currentPlayState;

        private SoundEffectInstance ambientNoise, safeRoomBGM;

        private bool playAmbientNoise, playSafeRoomBGM;

        public BGM()
        {
            ambientNoise = SoundManager.AmbientNoise.CreateInstance();
            safeRoomBGM = SoundManager.DarkSoulsTrack31.CreateInstance();
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
                            SafeRoomBGMFadeIn();
                        }
                        else
                        {
                            SafeRoomBGMFadeOut();
                            AmbientNoiseFadeIn();
                        }

                        break;
                    }
            }
        }

        private void SafeRoomBGMFadeIn()
        {
            if (safeRoomBGM.State != SoundState.Playing)
            {
                safeRoomBGM.Volume = 0.1f;
                safeRoomBGM.Play();
            }
            if (safeRoomBGM.Volume < 0.1f)
            {
                safeRoomBGM.Volume += 0.01f;
            }
        }

        private void SafeRoomBGMFadeOut()
        {
            if (safeRoomBGM.Volume > 0.001f)
            {
                safeRoomBGM.Volume -= 0.001f;
            }
            else if (safeRoomBGM.Volume <= 0.001f)
            {
                safeRoomBGM.Stop();
            }
        }

        private void AmbientNoiseFadeIn()
        {
            if (ambientNoise.State != SoundState.Playing)
            {
                ambientNoise.Volume = 0f;
                ambientNoise.Play();
            }
            if (ambientNoise.Volume < 0.99f)
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
