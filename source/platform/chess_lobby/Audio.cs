using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NAudio.Wave;

namespace platform.chess_lobby
{
    public class Audio
    {
        public static void play(String name)
        {
            WaveOutEvent output_device = new WaveOutEvent();
            Stream stream = Properties.Resources.ResourceManager.GetStream(name);
            WaveFileReader reader = new WaveFileReader(stream);
            output_device.Init(reader);
            output_device.Play();
        }

        public static void play(IEnumerable<String> names)
        {
            try
            {
                play(names.Single());
            }
            catch(InvalidOperationException)
            {
                new AudioPlayList().play(names);
            }
        }
    }

    class AudioPlayList
    {
        #region ' Constructors '

        public AudioPlayList()
        {; }

        #endregion

        #region ' Properties '

        private WaveOutEvent output_device { get; set; }
        private IEnumerator<WaveFileReader> enumerator { get; set; }

        #endregion

        #region ' Methods '

        public void play(IEnumerable<String> names)
        {
            output_device = new WaveOutEvent();
            output_device.PlaybackStopped += on_playback_stopped;
            IEnumerable<WaveFileReader> readers = new List<WaveFileReader>();
            foreach(String name in names)
            {
                Stream stream = Properties.Resources.ResourceManager.
                    GetStream(name);
                WaveFileReader reader = new WaveFileReader(stream);
                readers = readers.Append(reader);
            }
            enumerator = readers.GetEnumerator();
            on_playback_stopped(new Object(), new EventArgs());
        }

        private void on_playback_stopped(Object sender, EventArgs e)
        {
            if (!enumerator.MoveNext())
                return;
            output_device.Init(enumerator.Current);
            output_device.Play();
        }

        #endregion
    }
}
