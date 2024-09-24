using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SocialsScrapeUploader.helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialsScrapeUploader.drivers
{
	public class FacebookDriver : SocialMediaDriver, ISocialMediaDriver
	{
		string WebsiteUrl { get; set; }
		public FacebookDriver(string uploadWebUrl, IWebDriver driver, WebDriverWait wait) : base(driver, wait)
		{
			WebsiteUrl = uploadWebUrl;
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
                Messages.GeneralMessage("No link provided for social Facebook page.");
                return;
            }

            NavigateToWebsite(WebsiteUrl);

			string[] files = Directory.GetFiles(videosDirectoryPath, "*.mp4");
			SeleniumHelpers seleniumHelpers = new SeleniumHelpers(Wait);

			foreach (string filePath in files)
			{
				try
				{
					if (FileHelpers.GetVideoDuration(filePath) >= TimeSpan.FromSeconds(90))
					{
                        seleniumHelpers.ClickElement(By.XPath("//div[@role='button' and .//span[contains(text(), 'Photo/video')]]"));
                        seleniumHelpers.SendKeys(By.XPath("//div[contains(@class,'x9f619 x5yr21d x1n2onr6 xh8yej3')]//input[@type='file' and @class='x1s85apg']"), filePath);
                        seleniumHelpers.SendKeys(By.XPath("//div[@aria-label=\"What's on your mind?\"]//p[contains(@class, 'xdj266r')]"), string.Format("{0}\n\n{1}", Path.GetFileNameWithoutExtension(filePath), description));
                        seleniumHelpers.ClickElement(By.XPath("//div[@aria-label='Post'][@role='button']"));
                        System.Threading.Thread.Sleep(5000);
                        //seleniumHelpers.WaitUntilNotVisible(By.XPath("//span[contains(text(), 'Posting')]"));
                        //seleniumHelpers.ClickElement(By.XPath("//div[@role='button' and contains(@class, 'x1ypdohk') and @aria-hidden='false']"));
                    }
					else
					{
                        seleniumHelpers.ClickElement(By.XPath("//div[@role='button' and .//span[contains(text(), 'Reel')]]"));
                        seleniumHelpers.SendKeys(By.XPath("//div[contains(@class,'x9f619 x5yr21d x1n2onr6 xh8yej3')]//input[@type='file' and @class='x1s85apg']"), filePath);
                        seleniumHelpers.ClickElement(By.XPath("//div[@aria-label='Next' and @role='button']"));
                        seleniumHelpers.ClickElement(By.XPath("//div[@aria-label='Next' and @role='button' and @tabindex='0']"));
                        seleniumHelpers.SendKeys(By.XPath("//div[@aria-label=\"Describe your reel...\"]//p[contains(@class, 'xdj266r')]"), string.Format("{0}\n\n{1}", Path.GetFileNameWithoutExtension(filePath), description));
                        // seleniumHelpers.ClickWhenEnabled(Driver.FindElement(By.XPath("//div[@aria-label=\"Publish\" and @tabindex=0]")));
                        seleniumHelpers.ClickWhenExists(By.XPath("//div[@aria-label=\"Publish\" and @tabindex=0]"));
                        seleniumHelpers.ClickElement(By.XPath("//div[@role='button'][.//div[@data-video-id]]"));
                        seleniumHelpers.ClickElement(By.XPath("//div[@role='button' and contains(@class, 'x1ypdohk') and @aria-hidden='false']"));
                    }
                }
                catch (Exception ex)
				{
					Messages.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
				}
			}
		}
	}
}