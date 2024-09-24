using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialsScrapeUploader.helpers
{
    public static class ChromeDriverHelpers
    {
        public static void CloseExistingChromeSessions()
        {
            foreach (var process in Process.GetProcessesByName("chrome"))
            {
                try
                {
                    process.Kill();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    Messages.Error(ex, MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public static ChromeOptions InitializeChromeOptions(string chromeProfile)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--no-sandbox");            
            options.AddArgument(ConfigurationManager.AppSettings["ChromeUsersDataPath"]);
            options.AddArgument(chromeProfile);
            //options.AddArgument("--headless");
            //options.AddArgument("--disable-gpu");
            return options;
        }

        public static IWebDriver InitiateDrive(string chromeProfile)
        {
            CloseExistingChromeSessions();
            ChromeOptions options = InitializeChromeOptions(chromeProfile);
            IWebDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            return driver;
        }
    }
}
