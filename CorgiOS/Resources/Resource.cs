using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;

namespace CorgiOS.Resources
{
    public static class Resource
    {
        [ManifestResourceStream(ResourceName = "CorgiOS.Resources.Wallpapers.bliss1.bmp")] public static byte[] BlissRAW;
        public static Bitmap Bliss = new Bitmap(BlissRAW);
        [ManifestResourceStream(ResourceName = "CorgiOS.Resources.Art.Logo_256PX.bmp")] public static byte[] LogoRAW;
        public static Bitmap Logo = new Bitmap(LogoRAW);
        [ManifestResourceStream(ResourceName = "CorgiOS.Resources.Sound.oxp.wav")] public static byte[] StartupAudioRAW;
        [ManifestResourceStream(ResourceName = "CorgiOS.Resources.Sound.winxpshutdown.wav")] public static byte[] ShutdownAudioRAW;
    }
}
