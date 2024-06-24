using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SocialsScrapeUploader.helpers;
using System;
using System.IO;

namespace SocialsScrapeUploader.drivers
{
	public class YoutubeDriver : SocialMediaDriver
	{
		string WebsiteUrl { get; set; }
		public YoutubeDriver(string websiteUrl, IWebDriver driver, WebDriverWait wait) : base(driver, wait)
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
                Messages.GeneralMessage("No link provided for social Youtube page.");
                return;
            }

            NavigateToWebsite(WebsiteUrl);

            string[] files = Directory.GetFiles(videosDirectoryPath, "*.mp4");
            SeleniumHelpers seleniumHelpers = new SeleniumHelpers(Wait);

            foreach (string filePath in files)
            {
                try
                {
                    seleniumHelpers.ClickElement(By.Id("create-icon"));
                    seleniumHelpers.ClickElement(By.Id("text-item-0"));
                    seleniumHelpers.SendKeys(By.Name("Filedata"), filePath);

                    System.Threading.Thread.Sleep(5000);

                    seleniumHelpers.SendKeys(By.Id("description-textarea"), string.Format("{0}\n\n{1}", Path.GetFileNameWithoutExtension(filePath), description));


                    // PLAYLIST ADDING OR EDITING [NEEDS FIXING]

                    //seleniumHelpers.ClickElement(By.Id("right-icon"));
                    //string playlistXpath = "//span[contains(@class, 'checkbox-label') and .//span[contains(text(), 'AskReddit')]]";
                    //if (seleniumHelpers.CheckIfExists(Driver, By.XPath(playlistXpath)))
                    //{
                    //    seleniumHelpers.ClickElement(By.XPath(playlistXpath));
                    //}
                    //else
                    //{
                    //    seleniumHelpers.ClickElement(By.XPath("//ytcp-button[contains(@class, 'new-playlist-button')]"));
                    //    seleniumHelpers.ClickElement(By.XPath("//tp-yt-paper-item[@test-id='new_playlist']"));
                    //
                    //}
                    //
                    //seleniumHelpers.ClickElement(By.XPath("//span[contains(@class, 'checkbox-label') and .//span[contains(text(), 'AskReddit')]]"));
                    //seleniumHelpers.ClickElement(By.XPath("//ytcp-button[contains(@class, 'done-button') and @label='Done']"));

                    seleniumHelpers.ClickElement(By.Id("toggle-button"));
                    seleniumHelpers.SendKeys(By.Id("text-input"), description.Replace(' ', ','));
                    seleniumHelpers.ClickElement(By.Id("step-badge-3"));
                    seleniumHelpers.ClickElement(By.XPath("//tp-yt-paper-radio-button[contains(@class, 'style-scope ytcp-video-visibility-select') and @name='PUBLIC']"));
                    seleniumHelpers.ClickElement(By.Id("done-button"));

                    string closeButXPath = "//ytcp-button[@id='close-button']";
                    if (seleniumHelpers.CheckIfExists(Driver, By.XPath(closeButXPath)))
                    {
                        seleniumHelpers.ClickElement(By.XPath(closeButXPath));
                    }
                    else
                    {
                        seleniumHelpers.ClickElement(By.XPath("//ytcp-button[contains(@class, 'style-scope ytcp-uploads-still-processing-dialog')]/div[contains(@class, 'label style-scope ytcp-button') and contains(text(), 'Close')]"));
                    }

                    System.Threading.Thread.Sleep(3000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while uploading a video: " + ex.Message);
                }
            }

            System.Threading.Thread.Sleep(10000);
        }
	}
}
