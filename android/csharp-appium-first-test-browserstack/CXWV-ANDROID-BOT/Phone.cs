using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXWV_ANDROID_BOT
{
    public class Phone
    {
        public string Model { get; set; }
        public string Version { get; set; }

        public Phone() { }
        public Phone(string model, string version)
        {
            Model = model;
            Version = version;
        }
    }
}
