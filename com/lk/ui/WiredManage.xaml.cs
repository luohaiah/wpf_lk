using System;
using System.Windows;
using System.IO.Ports;
using System.Data;
using Main.com.lk.Wired;
using Main.com.lk.DbHelper;
using Main.com.lk.Constant;
namespace Main
{
    /// <summary>
    /// WiredManage.xaml 的交互逻辑
    /// </summary>
    public partial class WiredManage : Window
    {
        private string[] ports;
        private WiredData wd;
        private MainWindow window;
        private Dao dao;
        public WiredManage(MainWindow window)
        {
            InitializeComponent();
            ports = SerialPort.GetPortNames();
            Cbox_port.ItemsSource = ports;
            wd = WiredData.get_Instance(window);
            this.window = window;
            dao = Dao.getInstance(window);
        }

        /// <summary>
        /// 打开串口设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Cbox_port.SelectedValue != null)
            {

                if ((this.FindResource("open_port") as string).Equals(Btn_open.Content))
                {
                   // wd.init_SerialPort(this, Cbox_port.SelectedValue.ToString());
                    Btn_open.Content = this.FindResource("close_port") as string;
                }
                else
                {

                    wd.close_port();
                    Btn_open.Content = this.FindResource("open_port") as string;
                }
            }
            else
            {
                if (Cbox_port.SelectedValue == null)
                {
                    MessageBox.Show(this.FindResource("choose_port") as string);

                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

            if ((this.FindResource("close_port") as string).Equals(Btn_open.Content))
            {
                wd.close_port();
            }
        }

    }
}
