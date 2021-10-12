using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace VkGroupImageParser.Utility
{
    public class BrowserHelper
    {

        public IWebDriver Browser { get; set; }

        public BrowserHelper(IWebDriver browser)
        {
            Browser = browser;
        }
        public void ScrollDown()
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    Browser.ExecuteJavaScript("scrollBy(0,1100)");
                    Thread.Sleep(200);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddScrolls(int count, BrowserHelper bh)
        {
            int iterations = count / 5;

            for (int i = 0; i < iterations; i++)
            {
                bh.ScrollDown();
                Thread.Sleep(100);
            }
        }
    }
}
