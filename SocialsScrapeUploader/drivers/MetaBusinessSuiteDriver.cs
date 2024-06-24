using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SocialsScrapeUploader.helpers;
using System;
using System.IO;
using System.Windows.Forms;

namespace SocialsScrapeUploader.drivers
{
	public class MetaBusinessSuiteDriver : SocialMediaDriver, ISocialMediaDriver
    {
        string WebsiteUrl { get; set; }
        public MetaBusinessSuiteDriver(string uploadWebUrl, IWebDriver driver, WebDriverWait wait) : base(driver, wait)
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

            foreach (string filePath in files)
            {
                try
                {
                    seleniumHelpers.ClickElement(By.XPath("//div[contains(@class, 'x1t137rt')]//div[contains(text(), 'Create post')]"));

                }
                catch (Exception ex)
                {
                    Messages.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
            }

        }
        public void RunWebScrapingForVideosUploadForReelsOnly(string videosDirectoryPath, string description)
        {
            try
            {
                NavigateToWebsite(WebsiteUrl);

                if (Directory.Exists(videosDirectoryPath))
                {
                    string[] files = Directory.GetFiles(videosDirectoryPath);

                    foreach (string filePath in files)
                    {
                        Driver.FindElement(By.XPath(
                            @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                            div/div/div/div/div/div/div/div/div[1]/div[1]/div[2]/div/div/div/div[2]/div/div/span/div/div/div[2]"))
                            .Click();

                        System.Threading.Thread.Sleep(3000);                       

                        // TEMP SOLUTION TO UPLOAD VIDEOS. THESE COMMANDS SEND ACTUAL LIVE INPUT - USER SHOULD NOT INTERFERE WHILE THE SOLUTION ARE RUNNING
                        Driver.FindElement(By.XPath(
                           @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                           div/div/div/div/div/div/div/div/div/div[2]/div/div[1]/div/div/div[1]/div[1]/div/div[2]/
                           div/div[2]/div[1]/div[2]/div/div/div/div/span/div/div/div[2]"))
                           .Click();

                        System.Threading.Thread.Sleep(2000);

                        SendKeys.SendWait(filePath);
                        System.Threading.Thread.Sleep(2000);
                        SendKeys.SendWait("{Enter}");

                        Driver.FindElement(By.XPath(
                            @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                            div/div/div/div/div/div/div/div/div/div[2]/div/div[1]/div/div/div[1]/div[1]/div/div[3]/
                            div/div[2]/div[1]/div[2]/div/div[1]/div[2]/div[1]/div/div/div[2]/div"))
                            .SendKeys(description);

                        System.Threading.Thread.Sleep(60000);

                        IWebElement element = Driver.FindElement(By.XPath(
                            @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                            div/div/div/div/div/div/div/div/div/div[1]/div/div/div[1]/div/div[2]/div[3]/div[2]"));

                        ClickWhenEnabled(element);                      

                        System.Threading.Thread.Sleep(2000);

                        Driver.FindElement(By.XPath(
                            @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                            div/div/div/div/div/div/div/div/div/div[2]/div/div[1]/div/div/div[2]/div/div/div/div[2]/div/div[2]/div"))
                            .Click();

                        System.Threading.Thread.Sleep(5000);
                    }

                    Messages.Success("Video uploads to Facebook and Instagram has finished");
                }
                else
                {
                    Console.WriteLine("Provided directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                Messages.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        //public void RunWebscrapingForPostsUpload(string videosDirectoryPath, string description)
        public void RunWebScrapingForVideosUploadOld(string videosDirectoryPath, string description)
        {
            try
            {
                NavigateToWebsite(WebsiteUrl);

                if (Directory.Exists(videosDirectoryPath))
                {
                    string[] files = Directory.GetFiles(videosDirectoryPath);
                    bool isReel = false;

                    foreach (string filePath in files)
                    {                       
                        Driver.FindElement(By.XPath(
                            @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                            div/div/div/div/div/div/div/div/div[1]/div[1]/div[2]/div/div/div/div[3]/div[1]/span/div/div/div[2]"))
                            .Click();

                        System.Threading.Thread.Sleep(3000);

                        Driver.FindElement(By.XPath(
                           @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                            div/div/div[1]/div/div/div/div/div/div[2]/div/div/div[1]/div[1]/div[2]/div/div[2]/div[1]/div[2]/div/div[2]/div"))
                           .Click();

                        Driver.FindElement(By.XPath(
                            @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[2]/div/
                            div/div[1]/div[1]/div/div/div[1]/div[2]/div/div[1]/div/div"))
                            .Click();

                        System.Threading.Thread.Sleep(2000);

                        SendKeys.SendWait(filePath);
                        System.Threading.Thread.Sleep(2000);
                        SendKeys.SendWait("{Enter}");

                        System.Threading.Thread.Sleep(3000);                       

                        try
						{
                            // VIDEO TITLE
                            /*Driver.FindElement(By.XPath(
                                @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                                div/div/div[1]/div/div/div/div/div/div[2]/div/div/div[1]/div[1]/div[3]/div/div[2]/div[1]/
                                div[2]/div[1]/div[2]/div/div[2]/div[2]/div/div/div/div[1]/div[2]/div/div/input"))
                                .SendKeys(Path.GetFileNameWithoutExtension(filePath));*/

                            // SELECTING INSTAGRAM
                            Driver.FindElement(By.XPath(
                            @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                            div/div/div[1]/div/div/div/div/div/div[2]/div/div/div[1]/div[1]/div[1]/div/div[2]/div[1]/
                            div[2]/div/div[2]/div/div/div/div"))
                            .Click();

                            System.Threading.Thread.Sleep(1000);

                            Driver.FindElement(By.XPath(
                                @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[2]/div/
                            div[2]/div[1]/div[1]/div/div/div[1]/div[2]/div/div[3]"))
                                .Click();

                        }
                        catch
						{
                            isReel = true;
                        }

                        if (!isReel)
						{
                            Driver.FindElement(By.XPath(
                              @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/div/
                              div/div[1]/div/div/div/div/div/div[2]/div/div/div[1]/div[1]/div[4]/div/div[2]/div[1]/div[2]/
                              div[4]/div[1]/div[2]/div/div/div[1]/div/div/div[2]/div/div/div/div"))
                             .SendKeys(string.Format("{0}\n\n{1}", Path.GetFileNameWithoutExtension(filePath), description));

                            Driver.FindElement(By.XPath(
                                @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                            div/div/div[1]/div/div/div/div/div/div[2]/div/div/div[1]/div[2]/div/div/div/div[1]/div[2]/
                            div/div[1]/div/div/div[1]/div[3]/div[2]"))
                                .Click();

                            System.Threading.Thread.Sleep(2000);

                            Driver.FindElement(By.XPath(
                                @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                            div/div/div[1]/div/div/div/div/div/div[2]/div/div/div[1]/div[2]/div/div/div/div[1]/div[2]/
                            div/div[2]/div/div"))
                                .Click();

                            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(
                                    "/html/body/div[6]/div[1]/div[1]/div/div/div/div/div[3]/div/div/div"))).Click();

                            System.Threading.Thread.Sleep(2000);

                            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(
                                    "/html/body/div[6]/div[1]/div[1]/div/div/div/div/div/div[1]/div[2]/div[2]/div/div/div[2]/div/div[2]/div"))).Click();

                        }
  
                        SubmitReelSteps(string.Format("{0}\n\n{1}", Path.GetFileNameWithoutExtension(filePath), description));

                        isReel = false;
                    }

                    Messages.Success("Video uploads to Facebook and Instagram has finished");
                }
                else
                {
                    Console.WriteLine("Provided directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                Messages.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        void SubmitReelSteps(string description)
		{
            try
			{
                Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(
                    @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                    div/div/div/div/div/div/div/div/div/div[2]/div/div[1]/div/div/div[1]/div[1]/div/div[4]/
                    div/div[2]/div[1]/div[2]/div/div[1]/div[2]/div[1]/div/div/div[2]/div")))
                    .SendKeys(description);

                IWebElement element = Driver.FindElement(By.XPath(
                    @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                div/div/div/div/div/div/div/div/div/div[1]/div/div/div[1]/div/div[2]/div[3]/div[2]"));

                ClickWhenEnabled(element);

                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.XPath(
                    @"/html/body/div[1]/div/div[1]/div/div[2]/div/div/div[1]/div[1]/div/div[1]/div[1]/div/
                    div/div/div/div/div/div/div/div/div/div[2]/div/div[1]/div/div/div[2]/div/div/div/div[2]/
                    div/div[2]/div"))
                    .Click();

                System.Threading.Thread.Sleep(5000);
            }
            catch(Exception ex)
			{
                Messages.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
			}            
        }

        void ClickWhenEnabled (IWebElement element)
		{
            Wait.Until(driver =>
            {               
                string ariaDisabled = element.GetAttribute("aria-disabled");
                return string.IsNullOrEmpty(ariaDisabled) || ariaDisabled.ToLower() == "false";
            });

            element.Click();
        }
	}
}
