using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace csharp_appium_first_test_browserstack
{
    public class Bot
    {
        private int count = 1;
        public void Start()
        {
            AndroidDriver<AndroidElement> driver = null;
            AppiumOptions caps = new AppiumOptions();

            // Set your BrowserStack access credentials
            caps.AddAdditionalCapability("browserstack.user", "ratanaly_86jVWt");
            caps.AddAdditionalCapability("browserstack.key", "37Lg5xqfDAfQ6BTrF8aU");

            // Set URL of the application under test
            caps.AddAdditionalCapability("app", "bs://79230c428c3c63a34bf434afa36c565c8f51782d");

            // Specify device and os_version
            caps.AddAdditionalCapability("device", "Xiaomi Redmi Note 9");
            caps.AddAdditionalCapability("os_version", "10.0");

            // Specify the platform name
            caps.PlatformName = "Android";

            // Set other BrowserStack capabilities
            caps.AddAdditionalCapability("project", "BOT AUTOPLAY");
            caps.AddAdditionalCapability("build", "CXWV SERVICE");
            caps.AddAdditionalCapability("name", "BOT APP");
            driver = new AndroidDriver<AndroidElement>(new Uri("http://hub-cloud.browserstack.com/wd/hub"), caps);

            while (true)
            {
                System.Threading.Thread.Sleep(2000);

                SearchShow(driver);
                SearchClose(driver);
              
            }

            driver.Quit();
        }

        public void SearchShow(AndroidDriver<AndroidElement> driver)
        {
            try
            {

                AndroidElement searchElement = (AndroidElement)new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                                  .Until
                                  (SeleniumExtras.WaitHelpers.ExpectedConditions
                                  .ElementToBeClickable(MobileBy.Id("com.cxwv.unblockme:id/rewarded")));
                if (searchElement != null && searchElement.Text == "READY SHOW REWARDED VIDEO")
                {
                    Console.WriteLine("name : " + searchElement.Text);
                    searchElement.Click();
                    System.Threading.Thread.Sleep(31000);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
                switch (count)
                {
                    case 1: SearchShow(driver); break;
                    case 2: SearchClose(driver); break;
                }
                count++;
                if (count >= 3)
                    count = 1;
            }

        }

        public void SearchClose(AndroidDriver<AndroidElement> driver)
        {

            try
            {

                AndroidElement close = (AndroidElement)new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                         .Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                         .ElementToBeClickable(MobileBy.XPath("	/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.RelativeLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View/android.view.View[1]/android.widget.Button")));

                if (close != null && close.Text == "Close")
                {
                    Console.WriteLine("back : <=");
                    close.Click();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
                switch (count)
                {
                    case 1: SearchShow(driver); break;
                    case 2: SearchClose(driver); break;
                }
                count++;
                if (count >= 3)
                    count = 1;
            }
        }
    }
}
