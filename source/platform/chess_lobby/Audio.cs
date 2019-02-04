using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace platform.chess_lobby
{
    class Audio
    {
        public static void play(String name)
        {
            Stream stream = Properties.Resources.ResourceManager.GetStream(name);
            SoundPlayer player = new SoundPlayer(stream);
            player.Play();
        }
    }
}
