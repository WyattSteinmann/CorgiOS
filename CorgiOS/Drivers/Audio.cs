using Cosmos.HAL.Drivers.Audio;
using Cosmos.System.Audio.IO;
using Cosmos.System.Audio;
using Sys = Cosmos.System;
using Cosmos.HAL.Audio;

namespace CorgiOS.Drivers
{
    public static class Audio
    {
        public static void PlayBeep(uint freq, uint duration)
        {
            Sys.PCSpeaker.Beep(freq, duration);
        }

        public static bool PlayNote(Sys.Notes note, Sys.Durations duration)
        {
            if (Sys.VMTools.IsVMWare)
                return false; // Return False If VMWare Because VMWare Sucks Well At Least With Audio In Cosmos I Actually Like VMWare More Then QEMU Or Virtual Box Any Ways No Sound For VMWare Users ONLY ): BECAUSE IT CAUSES A CRASH
            Sys.PCSpeaker.Beep(note, duration);
            return true;
        }

        public static bool playSound(byte[] sound)
        {
            if (Sys.VMTools.IsVMWare)
                return false; // Return False If VMWare Because VMWare Sucks Well At Least With Audio In Cosmos I Actually Like VMWare More Then QEMU Or Virtual Box Any Ways No Sound For VMWare Users ONLY ): BECAUSE IT CAUSES A CRASH
            var mixer = new AudioMixer();
            // var audioStream = MemoryAudioStream.FromWave(sound);
            var audioStream = new MemoryAudioStream(new SampleFormat(AudioBitDepth.Bits16, 2, true), 48000, sound);
            var driver = AC97.Initialize(bufferSize: 4096);
            mixer.Streams.Add(audioStream);

            var audioManager = new AudioManager()
            {
                Stream = mixer,
                Output = driver
            };
            audioManager.Enable();
            return true;
        }
    }
}
