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
    public class PhoneBookPeopleTests : MainTest
    {
        
        [SetUp]
        public void LoginFirst()
        {
            var loginPage = new LoginPage(driver);
            loginPage.UserName.SendKeys(Azure.Login);
            loginPage.Password.SendKeys(Azure.Password);
            loginPage.Submit.Submit();           
        }
        [Test]
        public void CoToPeople_WithoutLogin()
        {
            var loginPage = new LoginPage(driver);
            loginPage.Logoff.Click();
            var peoplePage = new PeoplePage(driver);
            peoplePage.GoToPage();
            StringAssert.StartsWith($"{Azure.Root}/Account/Login", driver.Url);
        }
        [Test]
        public void CreateNew_GoesToViewByButtonClick()
        {            
            var peoplePage = new PeoplePage(driver);
            peoplePage.CreateNew.Click();            
            Assert.AreEqual("Create new", peoplePage.H2.Text);
        }
        [Test]
        public void Edit_GoesToViewToEditFirst()
        {
            var peoplePage = new PeoplePage(driver);
            peoplePage.EditFirst.Click();
            Assert.AreEqual("Edit", peoplePage.H2.Text);
        }
        [Test]
        public void Delete_GoesToViewToDeleteSecond()
        {
            var peoplePage = new PeoplePage(driver);
            peoplePage.DeleteSecond.Click();
            Assert.AreEqual("Are you sure you want to delete this?", peoplePage.H3.Text);
        }
    }
}
