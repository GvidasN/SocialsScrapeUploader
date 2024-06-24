using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SocialsScrapeUploader.helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;


namespace SocialsScrapeUploader.drivers
{
	public class TiktokDriver : SocialMediaDriver, ISocialMediaDriver
    {
		string WebsiteUrl { get; set; }
		public TiktokDriver (string websiteUrl, IWebDriver driver, WebDriverWait wait) : base(driver, wait)
		{
			WebsiteUrl = websiteUrl;
		}

		public override void RunWebScrapingForVideosUpload(string videosDirectoryPath, string description)
		{
            if (!Directory.Exists(videosDirectoryPath))
            {
                Messages.GeneralMessage("Provided directory does not exist.");
                return;
            }

            if (string.IsNullOrEmpty(WebsiteUrl))
            {
                Messages.GeneralMessage("No link provided for social TikTok page.");
                return;
            }

            NavigateToWebsite(WebsiteUrl);

            string[] files = Directory.GetFiles(videosDirectoryPath, "*.mp4");
            SeleniumHelpers seleniumHelpers = new SeleniumHelpers(Wait);            

            //Initial upload window. Waiting for it to load
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("jsx-3309220042")));

            foreach (string filePath in files)
            {
                try
                {
                    seleniumHelpers.SendKeys(By.CssSelector("input[type='file'][accept='video/*']"), filePath);
                    seleniumHelpers.SendKeys(By.XPath("//span[@data-text='true']"), $"\n\n{description}");

                    // TEMPORARY SOLUTION TO MAKE THE TAGS ACTIVE

                    //if (description.Contains('#'))
                    //{
                    //    List<string> tags = description.Split(' ').ToList();
                    //    foreach (var tag in tags)
                    //    {
                    //        seleniumHelpers.SendKeys(By.XPath("//span[@data-text='true']"), tag);
                    //        Thread.Sleep(1500);
                    //        SendKeys.SendWait("{Enter}");
                    //    }
                    //}
                    //else
                    //{
                    //    seleniumHelpers.SendKeys(By.XPath("//span[@data-text='true']"), description);
                    //}

                    seleniumHelpers.ClickElement(By.ClassName("TUXButton--primary"));
                    Thread.Sleep(1500);
                    seleniumHelpers.ClickElement(By.XPath("//button[contains(@class, 'TUXButton--medium TUXButton--primary')]"));                   
                }
                catch (Exception ex)
                {
                    Messages.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
            }

            Messages.Success("Video uploads to TikTok has finished");
        }
	}
}