using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace CXWV_ANDROID_BOT
{
    public class AppiumDevice
    {
        private int rewardedCount = 0;
        public int rewardedMax = 0;

        private string[] paths = { "/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.view.ViewGroup/android.widget.FrameLayout[2]/android.widget.RelativeLayout/android.widget.Button[3]",
                                    "/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.RelativeLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View/android.view.View[1]/android.widget.Button",
                                    "/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.RelativeLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View/android.view.View/android.view.View[2]/android.widget.TextView[1]"};
        private AndroidDriver<AndroidElement> driver = null;
        private AppiumOptions caps = new AppiumOptions();
        private Phone phone;
        private Random random = new Random();

        public AppiumDevice(Phone phone)
        {
            this.phone = phone;
            this.rewardedMax = 1;//random.Next(5, 11);
        }

        public void Start()
        {
            // Set your BrowserStack access credentials
            caps.AddAdditionalCapability("browserstack.user", "ratanaly_86jVWt");
            caps.AddAdditionalCapability("browserstack.key", "37Lg5xqfDAfQ6BTrF8aU");

            // Set URL of the application under test
            caps.AddAdditionalCapability("app", "bs://79230c428c3c63a34bf434afa36c565c8f51782d");

            // Specify device and os_version
            caps.AddAdditionalCapability("device", phone.Model);
            caps.AddAdditionalCapability("os_version", phone.Version);

            // Specify the platform name
            caps.PlatformName = "Android";

            // Set other BrowserStack capabilities
            caps.AddAdditionalCapability("project", "BOT AUTOPLAY");
            caps.AddAdditionalCapability("build", "CXWV SERVICE");
            caps.AddAdditionalCapability("name", "BOT APP");


            caps.AddAdditionalCapability("browserstack.networkLogs", "true");

            try
            {
                driver = new AndroidDriver<AndroidElement>(new Uri("http://hub-cloud.browserstack.com/wd/hub"), caps);
            }
            catch(Exception ex)
            {
                isComplete = true;
                return;
            }
            
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
                    if (element != null && element.Enabled && element.Displayed)
                    {
                        Console.WriteLine("===> click : " + element.Text);
                        

                        if(element.Text == "Close")
                        {
                            rewardedCount += 1;
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@ close " + rewardedCount);
                        }

                        if (rewardedCount >= rewardedMax)
                        {
                            isComplete = true;
                            break;
                        }

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
        }

        public void Stop()
        {
            if(driver != null)
                driver.Quit();
        }

        private bool isComplete = false;
        public bool IsComplete()
        {
            return isComplete;
        }

        public int GetRewardedCount()
        {
            return rewardedCount;
        }
    }
}
