using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using OpenQA.Selenium.Support.UI;using OpenQA.Selenium.Chrome;
using PhoneBook.Selenium.Tests.GlobalValues;
using PhoneBook.Selenium.Tests.Pages;

namespace PhoneBook.Selenium.Tests
{
    [TestFixture]
    public class PhoneBookAuthTests : MainTest
    {

        [Test]
        public void Login_PasswordOk()
        {
            var loginPage = new LoginPage(driver);
            loginPage.UserName.SendKeys(Azure.Login);
            loginPage.Password.SendKeys(Azure.Password);
            loginPage.Submit.Submit();
            Assert.IsTrue(loginPage.People.Displayed);
            Assert.AreEqual("People", loginPage.People.Text);            loginPage.Logoff.Click();
            Assert.IsTrue(loginPage.Register.Displayed);
        }

        [Test]
        public void Login_PasswordNotOkAndThenOK()
        {
            var loginPage = new LoginPage(driver);
            loginPage.UserName.SendKeys(Azure.Login);
            loginPage.Password.SendKeys(Azure.Password + "xyz");
            loginPage.Submit.Submit();
            StringAssert.StartsWith("Invalid", loginPage.MessageInvalidLogin.Text);
            loginPage.Password.SendKeys(Azure.Password);
            loginPage.Submit.Submit();
            Assert.AreEqual("People", loginPage.People.Text);
        }

    }
}
