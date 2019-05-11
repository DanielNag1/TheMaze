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
        Song ambientNoise;

        bool once = true;

        public BGM()
        {
            ambientNoise = SoundManager.AmbientNoise;

            MediaPlayer.IsRepeating = true;
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
                        if (once)
                        {
                            MediaPlayer.Play(ambientNoise);
                            once = false;
                        }

                        break;
                    }
            }
        }

    }
}
