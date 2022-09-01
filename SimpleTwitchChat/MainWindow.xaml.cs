using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwitchLib;
using TwitchLib.Client;

namespace SimpleTwitchChat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TwitchClient client;
        public string TwitchChannel;
        public List<string> Users = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            Users.Add("l_Black_Bird_l");
            UserControl1 user = new UserControl1();
            user.main = this;
            Panels.Children.Add(user);
            user.InputDate.Focus();
        }



        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                client.SendMessage(TwitchChannel, Message.Text);

                output output = new output();

                output.Texts.Text = "> l_Black_Bird_l : " + Message.Text;

                Panels.Children.Add(output);
                Scroller.ScrollToEnd();

                Message.Text = "";
             
            }
           
               
        }

        private void Message_TextChanged(object sender, RoutedEventArgs e)
        {
            Message.ItemsSource = null;
            string[] strings = Users.ToArray();
            //MessageBox.Show(strings[0]);
            Message.ItemsSource = strings;
        }


        private void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

 
    }
}
