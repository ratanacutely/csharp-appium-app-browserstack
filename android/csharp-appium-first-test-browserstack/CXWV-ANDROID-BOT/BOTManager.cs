using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXWV_ANDROID_BOT
{
    public class BOTManager
    {
        private static BOTManager instance = null;

        public Device device = null;

        public static BOTManager GetInstance()
        {
            if(instance == null)
            {
                instance = new BOTManager();
                instance.Init();
            }
            return instance;
        }

        private void Init()
        {
            device = new Device();
        }
    }
}
