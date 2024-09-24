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
            string filesDir = args[0];
            VideoType videoType = (VideoType)Enum.Parse(typeof(VideoType), args[1]);           
            
            Messages.GeneralMessage("-------------Starting video upload to social medias------------");
            
            if(videoType == VideoType.Reddit)
            {
               List<SocialPlatform> platforms = new List<SocialPlatform>
               {
                   new SocialPlatform(ResourcesSocialPlatformsNames.Instagram, ConfigurationManager.AppSettings[$"English_InstagramUrl"]),
                   new SocialPlatform(ResourcesSocialPlatformsNames.Youtube, ConfigurationManager.AppSettings[$"English_YoutubeUrl"]),
                   new SocialPlatform(ResourcesSocialPlatformsNames.Facebook, ConfigurationManager.AppSettings[$"English_FacebookUrl"]),
                   new SocialPlatform(ResourcesSocialPlatformsNames.Tiktok, ConfigurationManager.AppSettings[$"English_TiktokUrl"])
               };
            
                SocialPlatformsHelpers.RunSocialMediaVideosUpload(filesDir, ConfigurationManager.AppSettings["English_ChromeProfileDir"], platforms, Resources.AskRedditTags_English);
            }
            else if (videoType == VideoType.History)
            {
                Language language = (Language)Enum.Parse(typeof(Language), args[2]);
            
                List<SocialPlatform> platforms = new List<SocialPlatform>() {
                    new SocialPlatform(ResourcesSocialPlatformsNames.Facebook, ConfigurationManager.AppSettings[string.Concat(language, "HistorAI_FacebookUrl")]),
                    new SocialPlatform(ResourcesSocialPlatformsNames.Youtube, ConfigurationManager.AppSettings[string.Concat(language, "HistorAI_YoutubeUrl")])
                };
                
                SocialPlatformsHelpers.RunSocialMediaVideosUpload(filesDir, ConfigurationManager.AppSettings[string.Concat(language, "HistorAI_ChromeProfileDir")], platforms, Resources.HistorAITags);
            }

            // FOR DEBUGING
            //Language language = Language.English;
            //string filesDir = "C:\\Users\\lenovo\\Desktop\\HistoryVideos\\English";
            //
            //List<SocialPlatform> platforms = new List<SocialPlatform>() {
            //        new SocialPlatform(ResourcesSocialPlatformsNames.Facebook, ConfigurationManager.AppSettings[string.Concat(language, "HistorAI_FacebookUrl")]),
            //        new SocialPlatform(ResourcesSocialPlatformsNames.Youtube, ConfigurationManager.AppSettings[string.Concat(language, "HistorAI_YoutubeUrl")])
            //    };
            //
            //SocialPlatformsHelpers.RunSocialMediaVideosUpload(filesDir, ConfigurationManager.AppSettings[string.Concat(language, "HistorAI_ChromeProfileDir")], platforms, Resources.HistorAITags);

            //Delete video locally
            FileHelpers.DeleteFilesFromDir(filesDir);

            Messages.GeneralMessage("-------------Video uploading completed------------");
        }
    }
}
