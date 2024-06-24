using SocialsScrapeUploader.helpers;
using SocialsScrapeUploader.models;
using SocialsScrapeUploader.resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SocialsScrapeUploader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Messages.GeneralMessage("-------------Starting video upload to social medias------------");

            if(args[1] == "Reddit")
            {
                List<SocialPlatform> platforms = new List<SocialPlatform>
                {
                    new SocialPlatform(ResourcesSocialPlatformsNames.Instagram, ConfigurationManager.AppSettings[$"English_InstagramUrl"]),
                    new SocialPlatform(ResourcesSocialPlatformsNames.Youtube, ConfigurationManager.AppSettings[$"English_YoutubeUrl"]),
                    new SocialPlatform(ResourcesSocialPlatformsNames.Facebook, ConfigurationManager.AppSettings[$"English_FacebookUrl"]),
                    new SocialPlatform(ResourcesSocialPlatformsNames.Tiktok, ConfigurationManager.AppSettings[$"English_TiktokUrl"])
                };
            
                SocialPlatformsHelpers.RunSocialMediaVideosUpload(args[0], ConfigurationManager.AppSettings["English_ChromeProfileDir"], platforms, Resources.AskRedditTags_English);
            }
            else if (args[1] == "History")
            {
                List<SocialPlatform> platforms = new List<SocialPlatform>
                {
                    new SocialPlatform(ResourcesSocialPlatformsNames.Facebook, ConfigurationManager.AppSettings[$"HistorAI_FacebookUrl"]),
                    new SocialPlatform(ResourcesSocialPlatformsNames.Youtube, ConfigurationManager.AppSettings["HistorAI_YoutubeUrl"])
                };
               
                SocialPlatformsHelpers.RunSocialMediaVideosUpload(args[0], ConfigurationManager.AppSettings["HistorAI_ChromeProfileDir"], platforms, Resources.HistorAITags);
            }

            //Delete video locally
            FileHelpers.DeleteFilesFromDir(args[0]);

            Messages.GeneralMessage("-------------Video uploading completed------------");

        }
    }
}
