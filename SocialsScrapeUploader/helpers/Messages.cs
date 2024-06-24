using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialsScrapeUploader.helpers
{
    public static class Messages
    {
        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Error(Exception ex, string functionName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error while executing function {0}:", functionName);
            Console.WriteLine("Error Message.: " + ex.Message);
            Console.WriteLine("Exception.:" + ex.InnerException);
            Console.ResetColor();
        }

        public static void GeneralMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
