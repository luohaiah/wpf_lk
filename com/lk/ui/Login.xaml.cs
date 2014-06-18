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
using Main.com.lk.DbHelper;
namespace Main.com.lk.ui
{
    /// <summary>
    /// Loginxaml.xaml 的交互逻辑
    /// </summary>
    public partial class Loginxaml : Window
    {
        private Dao dao;
        public Loginxaml()
        {
            InitializeComponent();
            Db_base.getInstance_Db();
            dao = Dao.getInstance(this);
            dao.openDb();
        }

        private void Window_close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_login(object sender, RoutedEventArgs e)
        {
            login();
        }

        private void window_min(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            login();
        }
        private void login()
        {
            if ("".Equals(Tb_user.Text))
            {
                MessageBox.Show(this.FindResource("inputUser") as string);
            }
            else if ("".Equals(Tb_pw.Password))
            {
                MessageBox.Show(this.FindResource("inputPw") as string);
            }
            else
            {
                int result = dao.login(Tb_user.Text, Tb_pw.Password);
                switch (result)
                {
                    case 0:
                        MessageBox.Show(this.FindResource("nouser") as string);
                        break;
                    case 1:
                        MessageBox.Show(this.FindResource("pw_error") as string);
                        break;
                    case 2:
                        new MainWindow(Tb_user.Text).Show();
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
