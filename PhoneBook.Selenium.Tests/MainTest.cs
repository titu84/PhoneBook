using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Selenium.Tests
{
    [TestFixture]
    public abstract class MainTest
    {
        public IWebDriver driver { private set; get; }
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(@"C:\drivers");
        }
        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
