using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SocialsScrapeUploader.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SocialsScrapeUploader.drivers;

namespace SocialsScrapeUploader.helpers
{
    public static class SocialPlatformsHelpers
    {
        public static ISocialMediaDriver CreateSocialMediaDriver(SocialPlatform socialPlatform, IWebDriver driver, WebDriverWait wait)
        {
            switch (socialPlatform.Name)
            {
                case "Facebook":
                    return new FacebookDriver(socialPlatform.LinkToSite, driver, wait);
                case "Instagram":
                    return new InstagramDriver(socialPlatform.LinkToSite, driver, wait);
                case "Tiktok":
                    return new TiktokDriver(socialPlatform.LinkToSite, driver, wait);
                case "Youtube":
                    return new YoutubeDriver(socialPlatform.LinkToSite, driver, wait);
                case "Meta":
                    return new MetaBusinessSuiteDriver(socialPlatform.LinkToSite, driver, wait);
                default:
                    throw new NotSupportedException($"Social platform {socialPlatform.Name} is not supported.");
            }
        }

        public static void RunSocialMediaVideosUpload(string videosDir, string chromeProfileDir, List<SocialPlatform> socialPlatforms, string videoDescription)
        {
            IWebDriver driver = ChromeDriverHelpers.InitiateDrive(chromeProfileDir);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(10));

            foreach (SocialPlatform platform in socialPlatforms)
            {
                try
                {
                    ISocialMediaDriver socialMediaDriver = CreateSocialMediaDriver(platform, driver, wait);
                    socialMediaDriver.RunWebScrapingForVideosUpload(videosDir, videoDescription);
                }
                catch (Exception ex)
                {
                    Messages.Error(ex, MethodBase.GetCurrentMethod().Name);
                }
            }

            driver.Quit();
        }
    }
}
