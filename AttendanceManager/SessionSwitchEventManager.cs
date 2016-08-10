using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManager
{
    public static class SessionSwitchEventManager
    {
        private static DateTime? lockTime = null;
        private static DateTime? unlockTime = null;
        internal static TimeSpan totalBrkTimeSpan = new TimeSpan();
        internal static void RegisterSessionSwitchEvents()
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            Console.ReadLine();
            SystemEvents.SessionSwitch -= new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }
        static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            string unlockedMsg = string.Empty;
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                lockTime = DateTime.Now;
                unlockedMsg = string.Format("You have Locked the machine. Current DateTime is {0}", DateTime.Now);
                Printer.PrintMsgWithAstricks(unlockedMsg);
            }
            if ((e.Reason == SessionSwitchReason.SessionUnlock) || (e.Reason == SessionSwitchReason.SessionLogon))
            {
                unlockTime = DateTime.Now;


                totalBrkTimeSpan = totalBrkTimeSpan + unlockTime.Value.Subtract(lockTime.Value);


                int totalBrkHour = (int)totalBrkTimeSpan.TotalHours;
                int totalBrkMinute = (int)totalBrkTimeSpan.TotalMinutes;
                int totalBrkSeconds = (int)totalBrkTimeSpan.TotalSeconds;
                if (totalBrkHour > 0) totalBrkMinute = 0;
                if (totalBrkMinute > 0) totalBrkSeconds = 0;

                unlockedMsg = string.Format("You have taken total break time of {0} hours : {1} minutes : {2} seconds from the time you first logged in",
                   totalBrkHour,
                    totalBrkMinute,
                    totalBrkSeconds);


                Printer.PrintMsgWithAstricks(unlockedMsg);
                LogManager.CreateLogFile();

            }


        }
    }
}
