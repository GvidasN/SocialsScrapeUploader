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
	public class InstagramDriver : SocialMediaDriver, ISocialMediaDriver
	{
		string WebsiteUrl { get; set; }
		public InstagramDriver(string uploadWebUrl, IWebDriver driver, WebDriverWait wait) : base(driver, wait)
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
				Messages.GeneralMessage("No link provided for social Instagram page.");
				return;
			}

			NavigateToWebsite(WebsiteUrl);

			string[] files = Directory.GetFiles(videosDirectoryPath, "*.mp4");
			SeleniumHelpers seleniumHelpers = new SeleniumHelpers(Wait);

			string butNotificationsAc = "//button[contains(@class, '_a9-- _ap36 _a9_1')]";
			if (seleniumHelpers.CheckIfExists(Driver, By.XPath(butNotificationsAc)))
			{
                seleniumHelpers.ClickElement(By.XPath(butNotificationsAc));
            }

                foreach (string filePath in files)
			{
				try
				{
					seleniumHelpers.ClickElement(By.XPath("//a[contains(@class, '_a6hd') and .//span[contains(text(), 'Create')]]"));
					seleniumHelpers.ClickElement(By.XPath("//a[contains(@class, 'x1i10hfl') and .//span[contains(text(), 'Post')]]"));
					seleniumHelpers.SendKeys(By.CssSelector("input._ac69[type='file']"), filePath);
					seleniumHelpers.ClickElement(By.XPath("//div[contains(@class, 'x1i10hfl') and @role='button' and @tabindex='0' and text()='Next']"));
                    seleniumHelpers.ClickElement(By.XPath("//div[contains(@class, 'x1i10hfl') and @role='button' and @tabindex='0' and text()='Next']"));
					seleniumHelpers.SendKeys(By.CssSelector("p.xdj266r.x11i5rnm.xat24cr.x1mh8g0r"), string.Format("{0}\n\n{1}", Path.GetFileNameWithoutExtension(filePath), description));
					seleniumHelpers.ClickElement(By.XPath("//div[text()='Share']"));
					seleniumHelpers.ClickElement(By.CssSelector("div img[alt='Animated checkmark']"));
					seleniumHelpers.ClickElement(By.CssSelector("div.x160vmok.x10l6tqk.x1eu8d0j.x1vjfegm"));
				}
				catch(Exception ex)
				{
					Messages.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
				}					
			}
		}
	}
}