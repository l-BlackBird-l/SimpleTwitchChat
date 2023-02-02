using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.PubSub;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SimpleTwitchChat
{
    public class TwitchAPI
    {
        TwitchClient client;

        string TwitchChannel;
        MainWindow main;

        public TwitchAPI(string Channel, MainWindow main)
        {
            TwitchChannel = Channel;
            this.main = main;
          
            Start();
        }
        public void Start()
        {
            ConnectionCredentials credentials = new ConnectionCredentials("l_Black_Bird_l", "oauth:yl08t2ft83tky4hdk7i48hlcjppucu");
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials, TwitchChannel);

            client.OnMessageReceived += Client_OnMessageReceived;



            client.Connect();
            main.client = client;
            main.TwitchChannel = TwitchChannel;
            //   var test = client.;

        }



        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e) /// Output message
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
            output output = new output();
                if (e.ChatMessage.DisplayName != "halle35")
                {
                    output.Texts.Text = "> " + e.ChatMessage.DisplayName + " : " + e.ChatMessage.Message;
                    main.Panels.Children.Add(output);
                    main.Scroller.ScrollToEnd();

                    foreach (var item in main.Users)
                    {
                        if (item != e.ChatMessage.DisplayName)
                        {

                            main.Users.Add("@" + e.ChatMessage.DisplayName);
                            break;
                        }
                    }
                }
            });
        
        }

    }
}
