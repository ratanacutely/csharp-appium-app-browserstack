using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CXWV_ANDROID_BOT
{
    public partial class cxwv_android_bot : Form
    {
        public cxwv_android_bot()
        {
            InitializeComponent();
        }

        private BOTManager botManager = null;
        private List<BOT> bots = new List<BOT>();

        private void cxwv_android_bot_Load(object sender, EventArgs e)
        {
            //Device device = new Device();
            //device.Start();

            botManager = BOTManager.GetInstance();
            this.InitBot(5);
            this.Bind();
            timer1.Start();
        }

        private void InitBot(int count)
        {
            for(int i = 1; i <= count; i++)
            {
                StartNewBot();
            }
        }

        private void StartNewBot()
        {
            Phone phone = botManager.device.GetPhone();
            BOT bot = new BOT(phone);
            new Thread(() =>
            {
                bot.Start();
            }).Start();
            bots.Add(bot);
        }

        private void Bind()
        {
            dgvDevice.Rows.Clear();
            for(int i = 0; i < bots.Count; i++)
            {
                BOT bot = bots[i];
                int no = i + 1;
                dgvDevice.Rows.Add(no, bot.phone.Model, bot.phone.Version,bot.rewardedCount,bot.GetRewardedMax(), "running");
            }
            Console.WriteLine("======================== > bind " + bots.Count);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for(int i =0; i < bots.Count;i++)
            {
                BOT bot = bots[i];
                if (bot.IsComplete())
                {
                    bot.Stop();
                    bots.Remove(bot);
                    StartNewBot();
                    Bind();
                }
                else if (bot.IsRewarded())
                    Bind();

            }
        }
    }
}
