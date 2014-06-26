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

namespace Main.com.lk.ui
{
    /// <summary>
    /// ImportExcelDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ImportExcelDialog : Window
    {
        public ImportExcelDialog()
        {
            InitializeComponent();
        }
        public void setProgress(int progress) {
            Lable_progress.Content = progress + "%";
        }
    }
}
