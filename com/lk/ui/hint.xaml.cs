using Main.com.lk.Constant;
using System;
using System.Collections.Generic;
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
namespace Main
{
    /// <summary>
    /// hint.xaml 的交互逻辑
    /// </summary>
    public partial class hint : Window
    {
        public hint()
        {
            InitializeComponent();
        }
        //覆盖成绩
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Constant.isCover = true;
            Close();
        }
        //不覆盖成绩
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Constant.isCover = false;
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }
        
    }
}
