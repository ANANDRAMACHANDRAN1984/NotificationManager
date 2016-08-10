using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManager
{
    static class Printer
    {

        internal static void PrintMsgWithAstricks(string msg, string formatString = "")
        {
            string unlockedMsg = string.Format(msg, formatString);
            PrintAstricks(unlockedMsg);




            Console.WriteLine(unlockedMsg);
            PrintAstricks(unlockedMsg);
        }

        internal static void PrintAstricks(string unlockedMsg)
        {
            StringBuilder astrikMessage = new StringBuilder();

            if (string.IsNullOrWhiteSpace(unlockedMsg) == false)
            {
                var lengthOfString = (unlockedMsg.Length / 2);

                for (int i = 0; i < (30 < lengthOfString ? 30 : lengthOfString); i++)
                {
                    astrikMessage.Append("*");
                }
            }
            Console.WriteLine(astrikMessage);

            Console.WriteLine();
        }
       
    }
}
