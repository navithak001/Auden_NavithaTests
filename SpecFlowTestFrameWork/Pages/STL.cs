using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SpecFlowTestFrameWork.Pages
{
    public class STL
    {

        public IWebDriver WebDriver { get; }

        public int maxLoanAmount =500;

        public int minLoanAmount = 200;

        public STL(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public IWebElement RPday => WebDriver.FindElement(By.XPath("//span[contains(@class,'first-repayment')]"));

        public IWebElement cookies => WebDriver.FindElement(By.XPath("//button[contains(text(),'Accept all')]"));
        public IWebElement AmtSlider => WebDriver.FindElement(By.Id("range"));
        public IWebElement Amount => WebDriver.FindElement(By.XPath("//p[@data-testid='loan-amount-value']"));
        public IWebElement LoanSummaryAmount => WebDriver.FindElement(By.XPath("//strong[@data-testid='loan-calculator-summary-amount']"));

        public void clickDay(String dayNum)
        {
            WebDriver.FindElement(By.XPath("//button[@value='"+dayNum+"']")).Click();
            Thread.Sleep(1000);
        }

        public void setAmount(String amt)
        {
            //Convert String to Integer and Adding 10 for normalising slider to set
          
            int amtloan = Int16.Parse(amt);
            if (amtloan<= 300)
            {
                amtloan = amtloan + 10;

            } else if (amtloan > 400)
            {
                amtloan = amtloan - 10;
            }

                Actions move = new Actions(WebDriver);
            int PixelsToMove = GetPixelsToMove(AmtSlider, amtloan, maxLoanAmount, minLoanAmount);
            move.ClickAndHold(AmtSlider).MoveByOffset((-(int)AmtSlider.Size.Width / 2), 0).MoveByOffset(PixelsToMove, 0).Release().Perform();
        }

        public static int GetPixelsToMove(IWebElement Slider, decimal Amount, decimal SliderMax, decimal SliderMin)
        {
            int pixels = 0;
            decimal tempPixels = Slider.Size.Width;
            tempPixels = tempPixels / (SliderMax - SliderMin);
            tempPixels = tempPixels * (Amount - SliderMin);
            pixels = Convert.ToInt32(tempPixels);
            return pixels;
        }
    }

            
    }


