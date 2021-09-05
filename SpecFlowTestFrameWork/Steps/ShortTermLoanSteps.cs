using DemoPro.Hooks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using SpecFlowTestFrameWork.Pages;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTestFrameWork.Steps
{
    [Binding]
    public class ShortTermLoanSteps
    {

        String test_url = "https://www.auden.com/short-term-loan";
        private OpenQA.Selenium.IWebDriver driver;

        STL stl = null;
        dynamic data = null;


        [Given(@"I am on the Short Term Loan homepage")]
        public void GivenIAmOnTheShortTermLoanHomepage()
        {
            string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\netcoreapp3.1", "");
            string path = path1 + "driver";
            driver = new ChromeDriver(path);
            driver.Navigate().GoToUrl(test_url);
            stl = new STL(driver);
            driver.Manage().Window.Maximize();
            stl.cookies.Click();
            System.Threading.Thread.Sleep(2000);
        }

        [When(@"i select weekend as Repayment day")]
        public void WhenISelectWeekendAsRepaymentDay(Table table)
        {
            data = table.CreateDynamicInstance();
            stl.clickDay(data.Day.ToString());
        }

        [Then(@"Verify Valid First Repayment Day is displayed")]
        public void ThenVerifyValidFirstRepaymentDayIsDisplayed()
        {
            int dateShouldSub = 0;
            int dateToAdd = 0;

            //Assert Not Null
            Assert.That(stl.RPday.Text, Is.Not.Null);

            //Assert selected weekend day not shown
            Assert.IsFalse(stl.RPday.Text.Contains(data.Day.ToString()));

            //Validate exact date for First RP
            DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, data.Day);
            if ((int)dateValue.DayOfWeek == 6)
            {
                dateShouldSub = data.Day - 1;
                dateToAdd = -1;
            }
            else if ((int)dateValue.DayOfWeek == 0)
            {
                dateShouldSub = data.Day - 2;
                dateToAdd = -2;
            }
            String dateshouldDisplay = dateValue.AddDays(dateToAdd).ToString("dddd") + " " + dateShouldSub.ToString() + " " + DateTime.Now.ToString("MMM") + " " + DateTime.Now.Year.ToString();

            Assert.That(stl.RPday.Text.Equals(dateshouldDisplay));

        }

        [When(@"I slide to set Loan Amount")]
        public void WhenISlideToSetLoanAmount(Table table)
        {
            data = table.CreateDynamicInstance();
            stl.setAmount(data.Amount.ToString());
        }

        [Then(@"Verify the amount displayed")]
        public void ThenVerifyTheAmountDisplayed()
        {
            Assert.That(stl.Amount.Text.Contains(data.Amount.ToString()));
            Thread.Sleep(2000);
            Assert.That(stl.LoanSummaryAmount.Text.Contains(data.Amount.ToString()));
        }

        [Then(@"i Close Browser")]
        public void ThenICloseBrowser()
        {
            driver.Quit();
        }

    }
}
