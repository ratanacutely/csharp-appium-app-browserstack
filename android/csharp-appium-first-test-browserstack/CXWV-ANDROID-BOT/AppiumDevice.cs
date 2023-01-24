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

        private string[] countrysName = {"CA","US","AU","ES","HK"};
        private AndroidDriver<AndroidElement> driver = null;
        private AppiumOptions caps = new AppiumOptions();
        private Phone phone;
        private Random random = new Random();

        public AppiumDevice(Phone phone)
        {
            this.phone = phone;
            this.rewardedMax = random.Next(3,10);
        }

        public void Start()
        {
            // Set your BrowserStack access credentials
            caps.AddAdditionalCapability("browserstack.user", "devgmailbl_ICm2Oa");
            caps.AddAdditionalCapability("browserstack.key", "uMJQFyruDM2iXx9c21pY");

            // Set URL of the application under test
            caps.AddAdditionalCapability("app", "bs://932bc22b59ba41eb61efccc65c7fea0ce29f18fb");

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
            //caps.AddAdditionalCapability("browserstack.geoLocation", countrysName[random.Next(0,countrysName.Length)]);

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
            int notFoundControl = 0;
            
            while (true)
            {
                int wait = isFirstTime ? 5000 : 1000;
                System.Threading.Thread.Sleep(wait);
                isFirstTime = false;

                notFoundControl++;
                if (notFoundControl >= 70)
                {
                    isComplete = true;
                    Stop();
                    Console.WriteLine("===================> force break");
                    break;
                }

                try
                {
                    AndroidElement element = driver.FindElementByXPath(paths[index]);
                    if (element != null && element.Enabled && element.Displayed)
                    {
                        Console.WriteLine("===> click : " + element.Text);

                        if(element.Text == "Close")
                        {
                            notFoundControl = 0;
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
