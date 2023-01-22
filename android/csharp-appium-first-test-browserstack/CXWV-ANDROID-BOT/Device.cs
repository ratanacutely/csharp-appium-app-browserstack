using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Newtonsoft.Json;

namespace CXWV_ANDROID_BOT
{
    public class Device
    {
        private string path = "./data/devices.txt";
        private string pathPhone = "./data/clean_devices.txt";
        private List<Phone> phones = new List<Phone>();
        private Random random = new Random();

        public void Start()
        {
            StreamReader reader = new StreamReader(path);
            while(true)
            {
                string model = reader.ReadLine();
                string version = reader.ReadLine();
                if (version == null)
                    break;
                Phone phone = new Phone(model, version);
                phones.Add(phone);
            }

            reader.Close();

            string json = JsonConvert.SerializeObject(phones);
            StreamWriter writer = new StreamWriter(pathPhone);
            writer.Write(json);
            writer.Close();
        }

        public Device()
        {
            string json = File.ReadAllText(pathPhone);
            phones = JsonConvert.DeserializeObject<List<Phone>>(json);
        }

        public Phone GetPhone()
        {
            return phones[random.Next(0, phones.Count)];
        }
    }
}
