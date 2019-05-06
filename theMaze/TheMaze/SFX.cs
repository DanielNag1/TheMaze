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
        public SoundEffectInstance step1, step2;

        public SFX()
        {
            step1 = SoundManager.step1.CreateInstance();
            step2 = SoundManager.step1.CreateInstance();
        }

        public void Walking()
        {
            if (step1.State == SoundState.Stopped)
            {
                step1.Play();
            }
        }
    }
}
