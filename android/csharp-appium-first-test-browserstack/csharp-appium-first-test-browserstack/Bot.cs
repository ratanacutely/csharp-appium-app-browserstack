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
        private string[] paths = { "/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.view.ViewGroup/android.widget.FrameLayout[2]/android.widget.RelativeLayout/android.widget.Button[3]",
                                    "/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.RelativeLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View/android.view.View[1]/android.widget.Button",
                                    "/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.RelativeLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View/android.view.View/android.view.View[2]/android.widget.TextView[1]"};
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
            caps.AddAdditionalCapability("device", "Samsung Galaxy S9");
            caps.AddAdditionalCapability("os_version", "8.0");

            // Specify the platform name
            caps.PlatformName = "Android";

            // Set other BrowserStack capabilities
            caps.AddAdditionalCapability("project", "BOT AUTOPLAY");
            caps.AddAdditionalCapability("build", "CXWV SERVICE");
            caps.AddAdditionalCapability("name", "BOT APP");
            driver = new AndroidDriver<AndroidElement>(new Uri("http://hub-cloud.browserstack.com/wd/hub"), caps);
            bool isFirstTime = true;
            int index = 0;
            while (true)
            {
                int wait = isFirstTime ? 5000 : 2000;
                System.Threading.Thread.Sleep(wait);
                isFirstTime = false;

                try
                {
                    AndroidElement element = driver.FindElementByXPath(paths[index]);
                    if (element != null)
                    {
                        Console.WriteLine("===> click : "+ element.Text);
                        element.Click();

                    }
                    index++;
                    if (index == paths.Length)
                        index = 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error : " + index + " | " + ex.Message);
                    index++;
                    if (index == paths.Length)
                        index = 0;
                }

            }

            driver.Quit();
        }

        public void SearchShow(AndroidDriver<AndroidElement> driver)
        {
            try
            {
                AndroidElement searchElement = driver.FindElementById("com.cxwv.unblockme:id/rewarded");
                //AndroidElement searchElement = (AndroidElement)new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                //                  .Until
                //                  (SeleniumExtras.WaitHelpers.ExpectedConditions
                //                  .ElementToBeClickable(MobileBy.Id("com.cxwv.unblockme:id/rewarded")));
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
                AndroidElement buttonClose = driver.FindElementByXPath("/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.RelativeLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View/android.view.View[1]/android.widget.Button");
                //AndroidElement close = (AndroidElement)new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                //         .Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                //         .ElementToBeClickable(MobileBy.XPath("	/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.RelativeLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View/android.view.View[1]/android.widget.Button")));

                if (buttonClose != null && buttonClose.Text == "Close")
                {
                    Console.WriteLine("back : <=");
                    buttonClose.Click();

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
