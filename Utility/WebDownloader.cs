using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VkGroupImageParser.Utility
{
    public class WebDownloader
    {
        public void DownloadImages(List<string> links, string folderPath)
        {
            foreach (var link in links)
            {
                try
                {
                    using (WebClient client = new())
                    {
                        client.DownloadFile(new Uri(link), $"{folderPath}/{StringGenerator.RandomString(14)}.png");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }

        }
    }
}
