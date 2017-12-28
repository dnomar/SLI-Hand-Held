using System;

using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Media;

namespace sli1
{
    public static class UHFHelper
    {
        /// <summary>
        /// Check if English OS
        /// </summary>
         public static   bool isEnglish = true;
        public static Dictionary<byte, string> dicFreMode = new Dictionary<byte, string>();
        public static Dictionary<byte, string> dicFreBase = new Dictionary<byte, string>();
        public static Dictionary<byte, string> dicFreHop = new Dictionary<byte, string>();

        static UHFHelper()
        {
            dicFreMode.Add(0x00, "China Standard 920-925MHz");
            dicFreMode.Add(0x01, "China Standard 840-845MHz");
            dicFreMode.Add(0x02, "ETSI Standard 865-868MHz");
            dicFreMode.Add(0x03, "Fixed Mode(915MHz)");
            dicFreMode.Add(0x04, "Customized Mode");

            dicFreBase.Add(0x00, "50KHz");
            dicFreBase.Add(0x01, "125KHz");

            dicFreHop.Add(0x00, "Random Frequency hopping");
            dicFreHop.Add(0x01, "Frequency hopping from high to low");
            dicFreHop.Add(0x10, "Frequency hopping from low to high");
        }
    }
    static class Win32
    {
        private const string filePath = @"\windows\Barcodebeep.wav";
        /// <summary>
        /// Play when reading successfully
        /// </summary>
        /// <returns></returns>
        public static bool PlayScanSound()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    Sound player = new Sound(filePath);
                    player.Play();
                    //SoundPlayer player = new SoundPlayer(filePath);
                    //player.Play();
                }
                return true;

            }
            catch (System.Exception)
            {
                return false;
            }

        }
    }
}
