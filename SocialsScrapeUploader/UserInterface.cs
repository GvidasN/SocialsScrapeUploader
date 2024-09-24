using OpenQA.Selenium;
using SocialsScrapeUploader.helpers;
using SocialsScrapeUploader.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialsScrapeUploader
{
    public static class UserInterface
    {
        public static string RetrievePlatformInfo(List<SocialPlatform> platforms)
        {
            Messages.GeneralMessage("Hello! Before the social media scraping begins, some information is required from you:\n");
           
            foreach (SocialPlatform platform in platforms)
            {
                string toggleQuestion = string.Format("\nCurrent Set link for platform {0} is set to: {1} Do you wish to update the link or Not upload to this Social Media?", platform.Name, platform.LinkToSite);
                string inputQuestion = string.Format("\nIf you wish to skip this platform, leave it empty. Otherwise provide link where your video will be uploaded in {0}", platform.Name);
                string platformLink = ToggleAndAnswer(toggleQuestion, inputQuestion);

                platform.LinkToSite = platformLink;
                Messages.Success("Information updated");
            }

            Console.WriteLine("Provide directory path, where the video(s) are stored:");
            return Console.ReadLine();
        }

        public static string RetrieveUserInput(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        static string ToggleAndAnswer(string toggleQuestion, string inputQuestion)
        {
            Console.WriteLine(string.Format("{0} Press 'y' for YES and any other letter for NO", toggleQuestion));
            char toggleChoice = Console.ReadKey().KeyChar;

            if (toggleChoice == 'y')
            {
                Console.WriteLine(inputQuestion);
                return Console.ReadLine();               
            }
            
            return string.Empty;
        }
    }
}
