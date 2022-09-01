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

namespace SimpleTwitchChat
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public MainWindow main;
        private void TextWithQuestion_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int LeftTrick = Convert.ToInt32(TextWithQuestion.ActualWidth) + 20;
            InputDate.Margin = new Thickness(LeftTrick, 0, 0, 0);

   
        }

        void SearchStream(string Streamer)
        {
            
            ChannelInfo info = new ChannelInfo(Streamer, main);
        }


        private void InputDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                SearchStream(InputDate.Text);
        }
    }
}
