using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXWV_ANDROID_BOT
{
    public class BOT
    {
        public Phone phone;
        public int rewardedCount = 0;
        private AppiumDevice appiumDevice;

        public BOT(Phone phone)
        {
            this.phone = phone;
            this.appiumDevice = new AppiumDevice(phone);
        }

        public void Start()
        {
            this.appiumDevice.Start();
        }

        public bool IsComplete()
        {
            return appiumDevice.IsComplete();
        }

        private bool isHasRewarded = false;
        public bool IsRewarded()
        {
            int rewarded = appiumDevice.GetRewardedCount();
            if(rewarded > rewardedCount)
            {
                rewardedCount = rewarded;
                isHasRewarded = true;
            }
            else
            {
                isHasRewarded = false;
            }
            return isHasRewarded;
        }

        public int GetRewardedMax()
        {
            return appiumDevice.rewardedMax;
        }

        public void Stop()
        {
            appiumDevice.Stop();
        }
    }
}
