using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialsScrapeUploader.drivers
{
	public interface ISocialMediaDriver
	{
		void RunWebScrapingForVideosUpload(string videosDirectoryPath, string description);
	}
}
