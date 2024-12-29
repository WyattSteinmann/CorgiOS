using Sys = Cosmos.System;
using System;

namespace CorgiOS.Core
{
    public static class ACPIManager
    {
        public static string Shutdown()
        {
            try
            {
                Sys.Power.Shutdown();
                return "Its Now Safe To Turn Off Your PC.";
            }
            catch (Exception ex)
            {
                return "Failed To Shutdown Due To Error: " + ex.Message;
            }
        }

        public static string Reboot()
        {
            try
            {
                Sys.Power.Reboot();
                return "Its Now Safe To Turn Off Your PC.";
            }
            catch (Exception ex)
            {
                return "Failed To Reboot Due To Error: " + ex.Message;
            }
        }
    }
}
