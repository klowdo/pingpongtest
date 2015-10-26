using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Browser
{
    public partial class Form1 : Form
    {
        public string ApiKey { get; set; }
        public string Host { get; set; }
        public Form1()
        {
            InitializeComponent();
           
            webBrowser1.Navigate("https://pingpong.hb.se/login/processlogin?targeturl=https%3A%2F%2Fpingpong.hb.se%2FapiGetKey.do%3FapplicationId%3Dm.pingpong.net");
            //webBrowser1.Navigate("https://m.pingpong.net");

        }


        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Debug.WriteLine(e.Url.ToString());
            if (e.Url.ToString().Contains("callback"))
            {
                Debug.WriteLine("CallBack");
                var apiKey = HttpUtility.ParseQueryString(new Uri(e.Url.ToString()).Query).Get("callback");
            }
            
            
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Host = new Uri(e.Url.ToString()).Host;

            GetCookieContainer(Host);

        }
        public CookieContainer GetCookieContainer(string dom)
        {
            CookieContainer container = new CookieContainer();
            try
            {
                foreach (string cookie in webBrowser1.Document.Cookie.Split(';'))
                {
                    string name = cookie.Split('=')[0];
                    string value = cookie.Substring(name.Length + 1);
                    string path = "/";
                    string domain = dom; //change to your domain name
                    container.Add(new Cookie(name.Trim(), value.Trim(), path, domain));
                    
                    //Console.Out.WriteLine("{0}=>{1}", name.Trim(),value.Trim());
                    if (name.Trim() == "apiKey")
                    {
                        ApiKey = value.Trim();
                        Close();
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }


            return container;
        }
        public  string ExtractDomainNameFromURL(string Url)
        {
            if (!Url.Contains("://"))
                Url = "http://" + Url;

            return new Uri(Url).Host;
        }
    }
}
