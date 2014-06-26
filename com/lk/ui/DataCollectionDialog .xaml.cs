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
using Main.com.lk.Ant;

namespace Main.com.lk.ui
{
    /// <summary>
    /// DataCollection.xaml 的交互逻辑
    /// </summary>
    public partial class DataCollection : Window
    {
        private string[] ports;
        private WiredData wd;
        public MainWindow window;
        private Dao dao;
        private AntManage2 antmanage;
        public DataCollection(MainWindow window)
        {
            InitializeComponent();
            init(window);
        }

        private void label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            setLable_bg(label, "pack://application:,,,/resource/image/bg_name_coll.png");
        }

        private void xButton_Click(object sender, RoutedEventArgs e)
        {
            label.Background = null;
            if ((this.FindResource("open_port") as string).Equals(xButton.Content))
            {
                if (xComboBox.SelectedValue != null)
                {
                    wd.init_SerialPort(this, xComboBox.SelectedValue.ToString());
                    xButton.Content = this.FindResource("close_port") as string;
                    Utils.EditConfig("youxian", xButton.Content.ToString());

                }
                else
                {
                    if (xComboBox.SelectedValue == null)
                    {
                        MessageBox.Show(this.FindResource("choose_port") as string);

                    }
                }
            }
            else
            {
                wd.close_port();
                xButton.Content = this.FindResource("open_port") as string;
                Utils.EditConfig("youxian", xButton.Content.ToString());
            }
        }


        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void com_youxian_xiala(object sender, EventArgs e)
        {
            ports = SerialPort.GetPortNames();
            xComboBox.ItemsSource = ports;
        }
        private void setLable_bg(Label label_part, string image_url)
        {
            ImageBrush Ib = new ImageBrush();
            Ib.ImageSource = new BitmapImage(new Uri(image_url));
            Ib.Stretch = Stretch.Fill;
            label_part.Background = Ib;
        }

        private void xButton_wuxian_Click(object sender, RoutedEventArgs e)
        {
            label_wuxian.Background = null;
            if ((this.FindResource("open_port") as string).Equals(xButton.Content))
            {
                if (xComboBox_wuxian.SelectedValue != null)
                {
                    antmanage.initAnt(xComboBox_wuxian.SelectedValue.ToString());
                    xButton_wuxian.Content = this.FindResource("close_port") as string;
                    Utils.EditConfig("wuxian", xButton.Content.ToString());

                }
                else
                {
                    if (xComboBox_wuxian.SelectedValue == null)
                    {
                        MessageBox.Show(this.FindResource("choose_port") as string);

                    }
                }
            }
            else
            {
                antmanage.closeChannel();
                xButton.Content = this.FindResource("open_port") as string;
                Utils.EditConfig("wuxian", xButton.Content.ToString());
            }
        }

        private void label_wuxian_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            setLable_bg(label_wuxian, "pack://application:,,,/resource/image/bg_name_coll.png");
        }

        private void com_wuxian_xiala(object sender, EventArgs e)
        {
            ports = SerialPort.GetPortNames();
            xComboBox_wuxian.ItemsSource = ports;
        }
        private void init(MainWindow window)
        {
            string youxian_status = Utils.getConfig("youxian");
            string wuxian_status = Utils.getConfig("wuxian");
            if (!youxian_status.Equals(""))
            {
                xButton.Content = youxian_status;
            }
            if (!wuxian_status.Equals(""))
            {
                xButton_wuxian.Content = wuxian_status;
            }
            wd = WiredData.get_Instance(window);
            this.window = window;
            dao = Dao.getInstance(window);
            antmanage = new AntManage2(window);
        }
    }
}
