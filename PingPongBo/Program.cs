using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Browser;

namespace PingPongBo
{
    class Program
    {
        static private Form1 form = new Form1();

        [STAThread]
        static void Main(string[] args)
        {
            var apiKeyPath = @"c:\\temp\apiKey.txt";
            if (!File.Exists(apiKeyPath))
            {
                //ConfigurationSettings.AppSettings.("apiKey");
                form.Closed += FormClosed;
                form.ShowDialog();
                File.WriteAllText(apiKeyPath, form.ApiKey);

                Console.WriteLine(form.ApiKey);
                Thread.Sleep(10000);
            }
            var apiKey = File.ReadAllText(apiKeyPath);
            
            var api = new PingPongApi(apiKey, "https://pingpong.hb.se");
            var events = api.GetEvents(0, 50);

            foreach (var @event in events)
            {

                //var evenet = api.GetEventAndEnabledFunctions(@event.Id);
                //foreach (var enabledFunctionse in evenet.Functions)
                //{
                //    Console.Out.WriteLine(enabledFunctionse.Name);
                    
                //
                Console.Out.WriteLine(@event.Name + "  hej");
                var path = Path.Combine(@"c:\\temp\pingpong", CleanPath(@event.Name.Length> 30 ?@event.Name.Substring(0,30).Trim():@event.Name));
                var data = api.GetPongDocumentResourceOrFolder(@event.Id);
              
                FolderIteratoe(data,path);




                //Console.Out.WriteLine(@event.Name);
            }
            //Console.ReadKey();

        }

        private static void FolderIteratoe(PingPongDocumentResourceOrFolder data, string path)
        {
            if (data.GetType() == typeof(PingPongDocumentFolder))
            {
                //if (str ing.IsNullOrEmpty(data.Name)) return;
                var newPath = Path.Combine(path, CleanPath(data.Name));
                Console.Out.WriteLine(newPath);
                Directory.CreateDirectory(newPath);
                var data2 = (PingPongDocumentFolder)data;
                Console.Out.WriteLine(data2.Type);
                data2.Children.ForEach(c => FolderIteratoe(c, Path.Combine(path,data.Name)));
            }
        
            else
            {
                var data2 = (PingPongDocumentResource)data;
                CreateFile(data2,path);
            }
        }

        public static void CreateFile(PingPongDocumentResource file,string path)
        {

            var webClient = new WebClient();
            //Console.Out.WriteLine(file.Url);
            webClient.DownloadFile("https://pingpong.hb.se"+file.Url,Path.Combine(path,file.FileName));
        }

        static void FormClosed(object sender,EventArgs eventArgs)
        {
            
        }

        private static string CleanPath(string filename)
        {
            return Path.GetInvalidFileNameChars().Aggregate(filename, (current, c) => current.Replace(c, '_'));
        }
    }
}
