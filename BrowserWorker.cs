using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using VkGroupImageParser.Models;
using VkGroupImageParser.Utility;

namespace VkGroupImageParser;

public class VkBrowserWorker
{
    private IWebDriver? _browser;
    private Account Account { get; }
    private bool BackgroundMode { get; set; }

    public VkBrowserWorker(Account account, bool backgroundMode)
    {
        Account = account;
        BackgroundMode = backgroundMode;
        CreateBrowser();
    }
    private void CreateBrowser()
    {
        var driverService = ChromeDriverService.CreateDefaultService();
        //driverService.HideCommandPromptWindow = true; Убирает консоль от драйвера
        var options = new ChromeOptions();

        if (BackgroundMode)
            options.AddArguments("--headless");
        
        _browser = new ChromeDriver(driverService, options);
        _browser.Manage().Window.Maximize();
    } 
    public void Auth()
    {
        if (_browser != null)
        {
            _browser?.Navigate().GoToUrl("https://vk.com/id1");

            IWebElement emailElement = _browser.FindElement(By.Id("quick_email"), 15);
            emailElement.SendKeys(Account.Username);
            IWebElement passwordEl = _browser.FindElement(By.Id("quick_pass"), 15);
            passwordEl.SendKeys(Account.Password);
            IWebElement loginBtnEl = _browser.FindElement(By.ClassName("quick_login_button"), 15);
            loginBtnEl.SendKeys(Keys.Enter);
            Thread.Sleep(5000);
        }
    }
    public void DownloadImages(string folderPath, string groupId, int count)
    {
        if (_browser != null)
        {
            int limit = 1500;
            WebDownloader downloader = new WebDownloader();
            BrowserHelper bh = new BrowserHelper(_browser);


            _browser.Navigate().GoToUrl(groupId);

            if (count > limit)
                count = limit;

            bh.AddScrolls(count, bh);

            var elements = _browser.FindElements(By.XPath("//div[@class='wall_text']//div[contains(@class, 'page_post_sized_thumbs  clear_fix')]//a"));

            List<string> imageLinks = new List<string>();
            
            foreach (var webElement in elements)
            {
                string aria = webElement.GetAttribute("aria-label");
                string onclickValue = webElement.GetAttribute("onclick");

                if (aria != null)
                {
                    if (aria.Contains("фото"))
                    {
                        string link = onclickValue.Split(',')[3].Replace("\"", "").Replace(@"\", "").Replace(@"y\", "").Replace("y:", "").Replace("x_:", "").Replace("[", "");
                        imageLinks.Add(link);
                    }
                }
            }

            imageLinks = imageLinks.Take(count).ToList();

            int fails = 0;

            Task[] tasks = new Task[10];
            int valueForRange = imageLinks.Count / tasks.Length;
            int rangeValue = 0;
            for (int i = 0; i < tasks.Length; i++)
            {
                var value = rangeValue;
                tasks[i] = new Task((() => downloader.DownloadImages(imageLinks.GetRange(value, valueForRange), folderPath)));
                rangeValue += valueForRange;
            }
            foreach (Task task in tasks)
                task.Start();

            Task.WaitAll(tasks);
        }
    }
    public void Close()
    {
        _browser?.Close();
        _browser?.Quit();
    }
}

