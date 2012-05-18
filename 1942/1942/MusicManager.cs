using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace _1942
{
    public static class MusicManager
    {
        public static Song activeSong;

        public static void SetMusic(Song m_activeSong)
        {
            if (activeSong != m_activeSong)
            {
                StopMusic();
                activeSong = m_activeSong;
                PlayMusic();
            }
        }

        public static void PlayMusic()
        {
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(activeSong);
            }
        }

        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }

    }
}
