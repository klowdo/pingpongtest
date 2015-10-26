using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Browser;
using TestWindowService;
using Timer = System.Timers.Timer;

namespace PingPongService
{
    public partial class Service: ServiceBase
    {
        private Timer timer1 = null;
        static  Form1 form = new Form1();


        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer();
            this.timer1.Interval = 30000; //every 30 secs
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            timer1.Enabled = true;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            string username = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];
            

        }

        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            //try
            //{
            //    var apiKeyPath = @"c:\\temp\apiKey.txt";
            //    if (!File.Exists(apiKeyPath))
            //    {
            //        //ConfigurationSettings.AppSettings.("apiKey");
            //        //form.Closed += FormClosed;
            //        form.ShowDialog();
            //        File.WriteAllText(apiKeyPath, form.ApiKey);
            //        Library.WriteErrorLog(form.ApiKey);
            //    }
            //}
            //catch (Exception ex)
            //{
            //Library.WriteErrorLog(ex.ToString());

            //}
            
            //Write code here to do some job depends on your requirement
        }

        protected override void OnStop()
        {
        }
    }
}
