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
    class HomePage
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//a[text()='Go to People list']")]
        public IWebElement GoToPeopleButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#peopleLink")]
        public IWebElement HeaderPeopleLink { get; set; }
        public void GoToPage()
        {
            driver.Navigate().GoToUrl($"{Azure.Root}/ Home /Index");
        }
    }
}
