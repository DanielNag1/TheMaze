using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public static class SoundManager
    {
        public static Song SafeRoomBGM { get; private set; }
        public static SoundEffect step1 { get; private set; }
        public static SoundEffect step2 { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            step1 = content.Load<SoundEffect>("Audio/SFX/step1");
            step2 = content.Load<SoundEffect>("Audio/SFX/step2");

        }

    }
}
