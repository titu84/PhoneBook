using OpenQA.Selenium;
using PhoneBook.Selenium.Tests.GlobalValues;
using SeleniumExtras.PageObjects;

namespace PhoneBook.Selenium.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Url = $"{Azure.Root}/";
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Id, Using = "log")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "pwd")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "login")] 
        public IWebElement Submit { get; set; }

        [FindsBy(How = How.Id, Using = "peopleLink")] 
        public IWebElement People { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text()='Log off']")]
        public IWebElement Logoff { get; set; }
        
        [FindsBy(How = How.Id, Using = "registerLink")]
        public IWebElement Register { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[text()='Invalid login attempt.']")]
        public IWebElement MessageInvalidLogin { get; set; }
    }
}
