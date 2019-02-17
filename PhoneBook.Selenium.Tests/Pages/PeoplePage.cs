using OpenQA.Selenium;
using PhoneBook.Selenium.Tests.GlobalValues;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Selenium.Tests.Pages
{
    class PeoplePage
    {
        private IWebDriver driver;
        public readonly string Url = $"{Azure.Root}/People/Index";
        public PeoplePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Url = Url;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Id, Using = "create")]
        public IWebElement CreateNew { get; set; }

        [FindsBy(How = How.TagName, Using = "h2")]
        public IWebElement H2 { get; set; }

        [FindsBy(How = How.TagName, Using = "h3")]
        public IWebElement H3 { get; set; }

        [FindsBy(How = How.Id, Using = "Edit_1")]
        public IWebElement EditFirst { get; set; }

        [FindsBy(How = How.Id, Using = "Delete_2")]
        public IWebElement DeleteSecond { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text()='People']")]
        public IWebElement People { get; set; }
        public void GoToPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
