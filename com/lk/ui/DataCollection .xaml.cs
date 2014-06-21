using Main.com.lk.DbHelper;
using Main.com.lk.Wired;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using Main.com.lk.util;

namespace Main.com.lk.ui
{
    /// <summary>
    /// DataCollection.xaml 的交互逻辑
    /// </summary>
    public partial class DataCollection : Window
    {
        private string[] ports;
        private WiredData wd;
        private MainWindow window;
        private Dao dao;
        public DataCollection(MainWindow window)
        {
            InitializeComponent();
            string youxian_status = ConfigurationManager.AppSettings["youxian"];
            if (!youxian_status.Equals(""))
            {
                xButton.Content = youxian_status;
            }
            ports = SerialPort.GetPortNames();
            xComboBox.ItemsSource = ports;
            wd = WiredData.get_Instance(window);
            this.window = window;
            dao = Dao.getInstance(window);
        }

        private void label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImageBrush Ib = new ImageBrush();
            Ib.ImageSource = new BitmapImage(new Uri("pack://application:,,,/resource/image/bg_name_coll.png"));
            Ib.Stretch = Stretch.Fill;
            label.Background = Ib;
        }

        private void xButton_Click(object sender, RoutedEventArgs e)
        {
            label.Background = null;
            if (xComboBox.SelectedValue != null)
            {

                if ((this.FindResource("open_port") as string).Equals(xButton.Content))
                {
                    wd.init_SerialPort(this, xComboBox.SelectedValue.ToString());
                    xButton.Content = this.FindResource("close_port") as string;
                    Utils.EditConfig("youxian", xButton.Content.ToString());
                }
                else
                {

                    wd.close_port();
                    xButton.Content = this.FindResource("open_port") as string;
                    Utils.EditConfig("youxian", xButton.Content.ToString());
                }
            }
            else
            {
                if (xComboBox.SelectedValue == null)
                {
                    MessageBox.Show(this.FindResource("choose_port") as string);

                }
            }
        }


        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
