using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace SocialsScrapeUploader.drivers
{
	public abstract class SocialMediaDriver : ISocialMediaDriver
	{
		public IWebDriver Driver { get; set; }
		public WebDriverWait Wait { get; set; }

		public SocialMediaDriver(IWebDriver driver, WebDriverWait wait)
		{
			Driver = driver;
			Wait = wait;
		}

		public abstract void RunWebScrapingForVideosUpload(string videosDirectoryPath, string description);

		public void NavigateToWebsite (string websiteUrl)
		{
			((IJavaScriptExecutor)Driver).ExecuteScript("window.open();");
			Driver.SwitchTo().Window(Driver.WindowHandles.Last());
			Wait.Until(d => Driver.WindowHandles.Count > 1);
            Driver.Navigate().GoToUrl(websiteUrl);
			Wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
	}
}
