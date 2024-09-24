using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialsScrapeUploader.models
{
    public class SocialPlatform
    {
        public string Name { get; }
        public string LinkToSite { get; set; }

        public SocialPlatform(string name, string linkToSite)
        {
            Name = name;
            LinkToSite = linkToSite;
        }
    }
}
