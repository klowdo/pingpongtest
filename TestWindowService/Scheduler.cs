using System;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Timers;

namespace TestWindowService
{
    public partial class Scheduler : ServiceBase
    {
        private Timer timer1 = null;
        //static Form1 form = new Form1();


        public Scheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer();
            this.timer1.Interval = 30000; //every 30 secs
            this.timer1.Elapsed += new ElapsedEventHandler(this.timer1_Tick);
            timer1.Enabled = true;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            string username = (string) collection.Cast<ManagementBaseObject>().First()["UserName"];
            Library.WriteErrorLog(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            Library.WriteErrorLog(Environment.MachineName);
            Library.WriteErrorLog("Test window service started");
                
        }

        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            //var apiKeyPath = @"c:\\temp\apiKey.txt";
            //if (!File.Exists(apiKeyPath))
            //{
            //    //ConfigurationSettings.AppSettings.("apiKey");
            //    //form.Closed += FormClosed;
            //    form.ShowDialog();
            //    File.WriteAllText(apiKeyPath, form.ApiKey);

            //    Console.WriteLine(form.ApiKey);
            //    //Thread.Sleep(10000);
            //}
            //Write code here to do some job depends on your requirement
            Library.WriteErrorLog("Timer ticked and some job has been done successfully");
        }

        protected override void OnStop()
        {
            timer1.Enabled = false;
            Library.WriteErrorLog("Test window service stopped");
        }

    
    }

    
}
