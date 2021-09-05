using SpecFlow;
using System;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;


namespace DemoPro.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        public static string ReportPath;
        public ExtentTest test { get; set; }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
           // DriverContext.Initialize();
            //Page.Initialize();
            string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\netcoreapp3.1", "");
            string path = path1 + "Report\\index.html";
            var htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReporter.Config.ReportName = "Automation Test Report";
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }


        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Create dynamic feature name

            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);

        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "When")
                                scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "Then")
                                scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "And")
                                scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if(ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);

                }
                else if(stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);

                }
                else if(stepType == "Then") {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);

                }
                else if(stepType == "And")
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);

                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine("AfterScenario");
            //implement logic that has to run after executing each scenario
        }



        [AfterTestRun]
        public static void AfterTestRun()
        {
           // DriverContext.Driver.Close();
           // DriverContext.Driver.Dispose();
            //kill the browser
            //Flush report once test completes
            extent.Flush();
            //kill the browser
        }


    }
}