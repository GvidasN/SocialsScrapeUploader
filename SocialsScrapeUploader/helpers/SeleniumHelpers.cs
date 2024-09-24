using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace SocialsScrapeUploader.helpers
{
    public class SeleniumHelpers
    {
        private WebDriverWait Wait { get; set; }

        public SeleniumHelpers(WebDriverWait wait)
        {
            Wait = wait;
        }

        public void ClickElement(By by)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(by)).Click();
            System.Threading.Thread.Sleep(3000);
        }

        public void SendKeys(By by, string text)
        {
            Wait.Until(ExpectedConditions.ElementExists(by)).SendKeys(text);
            System.Threading.Thread.Sleep(3000);
        }
        public void ClickWhenExists(By by)
        {
            Wait.Until(ExpectedConditions.ElementExists(by)).Click();
            System.Threading.Thread.Sleep(3000);
        }

        public void ClickWhenEnabled(IWebElement element)
        {
            Wait.Until(driver =>
            {
                string ariaDisabled = element.GetAttribute("aria-disabled");
                return string.IsNullOrEmpty(ariaDisabled) || ariaDisabled.ToLower() == "false";
            });

            element.Click();
        }

        public void WaitUntilNotVisible(By by)
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
            System.Threading.Thread.Sleep(3000);
        }

        public bool CheckIfExists(IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by) != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void SwitchToIframe(IWebDriver driver, By by)
        {
            var iframeElement = Wait.Until(ExpectedConditions.ElementExists(by));
            driver.SwitchTo().Frame(iframeElement);
        }
    }
}
