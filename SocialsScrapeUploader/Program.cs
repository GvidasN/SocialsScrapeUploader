using SocialsScrapeUploader.helpers;
using SocialsScrapeUploader.models;
using SocialsScrapeUploader.resources;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace SocialsScrapeUploader
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<SocialPlatform> platforms = new List<SocialPlatform>
            {
                new SocialPlatform(ResourcesSocialPlatformsNames.Instagram, ConfigurationManager.AppSettings[$"InstagramUrl"]),
                new SocialPlatform(ResourcesSocialPlatformsNames.Youtube, ConfigurationManager.AppSettings[$"YoutubeUrl"]),
                new SocialPlatform(ResourcesSocialPlatformsNames.Facebook, ConfigurationManager.AppSettings[$"FacebookUrl"]),
                new SocialPlatform(ResourcesSocialPlatformsNames.Tiktok, ConfigurationManager.AppSettings[$"TiktokUrl"])
            };

            string filesDir = UserInterface.RetrievePlatformInfo(platforms);
            string videosDescription = UserInterface.RetrieveUserInput("Please Provide a description for the videos:");
            platforms.RemoveAll(platform => string.IsNullOrEmpty(platform.LinkToSite));
            
            Messages.GeneralMessage("------------- Starting videos upload ------------");

            SocialPlatformsHelpers.RunSocialMediaVideosUpload(filesDir, ConfigurationManager.AppSettings["English_ChromeProfileDir"], platforms, videosDescription);

            Messages.GeneralMessage("------------- Video uploading completed ------------");
        }
    }
}
