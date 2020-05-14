using System;
using System.IO;
using System.Reflection;

namespace Anunturi.Mobile.Infrastructure.Helpers
{
    public static class AudioHelper
    {

        public static void PlayNotification()
        {
            try
            {
                Stream stream = GetStreamFromFile("notif.mp3");
                var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                audio.Load(stream);
                audio.Play();
            }
            catch(Exception e)
            {

            }
        }

        static Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;

            var stream = assembly.GetManifestResourceStream("Anunturi.Mobile." + filename);

            return stream;
        }
    }
}
