using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AttendanceManager
{
    static class Timer
    {
        static System.Timers.Timer aTimer;
         static readonly int firstHourNotifierTime = 0;
         static readonly int secondHourNotifierTime = 0;
         static Timer()
        {
            firstHourNotifierTime = Convert.ToInt32(ConfigurationManager.AppSettings["FirstHourNotifierTime"]);
            secondHourNotifierTime = Convert.ToInt32(ConfigurationManager.AppSettings["SecondHourNotifierTime"]);
        }
        internal static void TimerEvents()
        {
            try
            {
                aTimer = new System.Timers.Timer();
                if (ConfigurationManager.AppSettings.HasKeys() && ConfigurationManager.AppSettings["TimerInterval"] != null)
                    aTimer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["TimerInterval"]); //1 hour

                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;


                // Have the timer fire repeated events (true is the default)
                aTimer.AutoReset = true;

                // Start the timer
                aTimer.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();

            }
        }

        internal static void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            TimeSpan timeDiff = new TimeSpan();
            if ((LogManager.logOnTime != null) && (LogManager.logOnTime.Value != null))
            {
                timeDiff = e.SignalTime.Subtract(LogManager.logOnTime.Value);
                if (LogManager.logOnTime.Value.Date != DateTime.Now.Date)
                {
                    LogManager.logOnTime = null; SessionSwitchEventManager.totalBrkTimeSpan = new TimeSpan();
                    Console.Clear();
                    if (File.Exists(LogManager.filePath)) File.Delete(LogManager.filePath);

                }
            }
            var currentTime = DateTime.Now;
            string defaultMsg = "Hours Completed.You can leave for the day if you wish. Current Time is";
            string msg = string.Format("{0} {1} {2}", firstHourNotifierTime, defaultMsg, currentTime);
            var timeDiffTime = timeDiff.Hours;


#if DEBUG
            timeDiffTime = timeDiff.Minutes;
#endif
            switch (timeDiffTime)
            {


                case 7: //Should be changed to 7
                    MessageBox.Show(msg, "Notification To Leave", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
                    break;
                case 9: //Should be changed to 9
                    msg = string.Format("{0} {1} {2}", secondHourNotifierTime, defaultMsg, currentTime);
                    MessageBox.Show(msg, "Notification To Leave", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
                    //Console.WriteLine("---- ----  -----  -----");
                    Console.Clear();
                    break;

                default:
                    if (timeDiffTime < secondHourNotifierTime && timeDiffTime > firstHourNotifierTime)
                    {
                        msg = string.Format("{0} {1} {2}", timeDiffTime, defaultMsg, currentTime);
                        MessageBox.Show(msg, "Notification To Leave", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        break;
                    }
                    break;
            }



        }


    }
}
