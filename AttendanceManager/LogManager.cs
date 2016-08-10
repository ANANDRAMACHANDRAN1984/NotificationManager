using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManager
{
   static class LogManager
    {
        public static DateTime? logOnTime = null;
        public static string filePath = System.Configuration.ConfigurationManager.AppSettings["LogFilePath"];
        internal static void CreateLogFile()
        {

            Printer.PrintMsgWithAstricks("You have Unlocked the machine. Current DateTime is {0}", DateTime.Now.ToString());

            if (logOnTime == null)
            {
                //if File Exists then set file datetime as user already logged in time
                //if File doesn't Exists then write current time in file and set file datetime as user already logged in time
                var loggedTime = File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;

                if (loggedTime.Equals(string.Empty))
                {
                    logOnTime = DateTime.Now;
                    if (File.Exists(filePath) == false) { File.WriteAllText(filePath, DateTime.Now.ToString()); }
                }
                else if (Convert.ToDateTime(loggedTime).Date.CompareTo(DateTime.Today.Date) != 0)
                {
                    logOnTime = DateTime.Now;
                    File.Delete(filePath);
                    File.WriteAllText(filePath, DateTime.Now.ToString());
                }
                else
                    logOnTime = Convert.ToDateTime(loggedTime);
                Timer.TimerEvents();

                Console.WriteLine(string.Format("You first Unlocked your machine at {0}", logOnTime));
                //Console.WriteLine();
            }

        }
    }
}
