using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialsScrapeUploader.helpers
{
    public static class Messages
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        public static void Success(string message)
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger.Info(message);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Error(Exception ex, string functionName)
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger.Error(string.Concat("Error while executing function ", functionName), ex);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error while executing function {0}:", functionName);
            Console.WriteLine("Error Message.: " + ex.Message);
            Console.WriteLine("Exception.:" + ex.InnerException);
            Console.ResetColor();
        }

        public static void GeneralMessage(string message)
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger.Info(message);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
