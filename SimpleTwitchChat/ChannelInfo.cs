using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleTwitchChat
{
    internal class ChannelInfo
    {
        string _id = "ytqmaf001z01x5uai9bjkinpbh20d3";
        string _secret = "aswueqtjecfdo7deoz0hs53ru7nr8y";
        string streamer_name = "";
        string access = "Bearer 2c7fw8hqvgsmbigabbplolbixr8nlu";
        MainWindow main;
        PyPoints points;
        bool connect = false;
        public ChannelInfo(string Channel, MainWindow main)
        {
            streamer_name = Channel;
            this.main = main;


            Thread LT = new Thread(new ThreadStart(StreamState));
            LT.Start();



            points = new PyPoints("PointCount.py", streamer_name);

        }

        void Waiting()
        {
            string waitString = "";
            Random rnd = new Random();

            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                main.Panels.Children.Clear();
            });

            while (!connect)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    waitString = rnd.Next(9999, 99999).ToString() + " " + rnd.Next(9999, 99999).ToString() + " " + rnd.Next(9999, 99999).ToString() + " " + rnd.Next(9999, 99999).ToString() + " " + rnd.Next(9999, 99999).ToString() + " " + rnd.Next(9999, 99999).ToString() + " " + rnd.Next(9999, 99999).ToString() + " " + rnd.Next(9999, 99999).ToString() + " ";
                    output output = new output();
                    output.Texts.Text = waitString;
                    main.Panels.Children.Add(output);
                    main.Scroller.ScrollToEnd();

                    waitString = "";
                });
            }
            main.Panels.Children.Clear();
        }


        public void UpdateState()
        {
            string GenerateTitle;
            try
            {
                bool finish = true;
                string StreamName = "";
                string title = "";
                string viewers = "";
                while (finish)
                {
                    string Url = "https://api.twitch.tv/helix/streams?user_login=" + streamer_name;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                    request.Method = "Get";
                    request.Timeout = 5000;
                    request.Headers.Add("Client-ID", _id);
                    request.Headers.Add("Authorization", access);

                    using (var s = request.GetResponse().GetResponseStream())
                    {
                        using (var sr = new System.IO.StreamReader(s))
                        {
                            var jsonObject = JObject.Parse(sr.ReadToEnd());

                            var theValue = jsonObject.SelectToken("data[0].title");
                            if (theValue != null)
                                title = theValue.ToString();

                            theValue = jsonObject.SelectToken("data[0].viewer_count");
                            if (theValue != null)
                                viewers = theValue.ToString();

                            theValue = jsonObject.SelectToken("data[0].user_name");
                            if (theValue != null)
                                StreamName = theValue.ToString();


                            GenerateTitle = "Name: " + StreamName + " | Viewers: " + viewers+ " | Points: " + points.GetPoints() + " | Title: " + title;
                                Application.Current.Dispatcher.Invoke((Action)delegate
                                {
                                try
                                {
                                    main.Title = "TwitchChat || " + GenerateTitle.Replace("\r\n", " ");
                                    }
                                    catch (Exception) { }
                                });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void StreamState()
        {
            string GenerateTitle;
            try
            {

                    string StreamName = "";
                    string title = "";
                    string viewers = "";
                    string Url = "https://api.twitch.tv/helix/streams?user_login=" + streamer_name;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                    request.Method = "Get";
                    request.Timeout = 5000;
                    request.Headers.Add("Client-ID", _id);
                    request.Headers.Add("Authorization", access);

                    using (var s = request.GetResponse().GetResponseStream())
                    {
                        using (var sr = new System.IO.StreamReader(s))
                        {
                            var jsonObject = JObject.Parse(sr.ReadToEnd());

                        var theValue = jsonObject.SelectToken("data[0].title");
                            if (theValue != null)
                                title = theValue.ToString();

                            theValue = jsonObject.SelectToken("data[0].viewer_count");
                            if (theValue != null)
                                viewers = theValue.ToString();

                            bool isReal = true;
                            theValue = jsonObject.SelectToken("data[0].user_name");
                            if (theValue != null)
                                StreamName = theValue.ToString();
                            else
                            {
                                isReal = false;
                            
                            }
                        PyPoints points = new PyPoints("PointCount.py", streamer_name);
                        GenerateTitle = "Name: " + StreamName + " | Viewers: " + viewers + " | Points: " + points.GetPoints() + " | Title: " + title;

                        Application.Current.Dispatcher.Invoke((Action)delegate
                                {
                                    if (isReal)
                                    {
                                        main.Title = "TwitchChat || " + GenerateTitle.Replace("\r\n", " ");
                                        main.Panels.Children.Clear();
                                        main.Message.Visibility = Visibility.Visible;
                                        main.Panels.Margin = new Thickness(0, 0, 0, 30);
                                        TwitchAPI aPI = new TwitchAPI(StreamName, main);
                                        Thread LT = new Thread(new ThreadStart(UpdateState));
                                        LT.Start();
                                    }
                                    else
                                    {
                                        main.Panels.Children.Clear();
                                        output output = new output();
                                        output.Texts.Text = "> Error streamer Name or streamer offline";
                                        main.Panels.Children.Add(output);

                                        UserControl1 user = new UserControl1();
                                        user.main = main;
                                        main.Panels.Children.Add(user);
                                        user.InputDate.Focus();

                                    }
                                });
                            }
                        }
                    }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
