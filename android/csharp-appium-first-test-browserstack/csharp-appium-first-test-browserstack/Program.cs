using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace csharp_appium_first_test_browserstack
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot botAutoPlay = new Bot();
            botAutoPlay.Start();
        }
    }
}
