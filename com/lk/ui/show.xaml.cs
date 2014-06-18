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
using System.Data;
using Main.com.lk.DbHelper;
using Main.com.lk.Constant;

namespace Main
{
    /// <summary>
    /// show.xaml 的交互逻辑
    /// </summary>
    public partial class Show : Window
    {
        DataTable dt_left, dt_right;
        MainWindow window;
        Dao dao;
        public Show(MainWindow window)
        {
            InitializeComponent();
            dao = Dao.getInstance(window);
            this.window = window;
            Lv_left.ItemsSource = CreateData_left().DefaultView;
            Lv_right.ItemsSource = CreateData_right().DefaultView;
        }
        DataTable CreateData_left()
        {
            dt_left = new DataTable();
            dt_left.Columns.Add("projectName");
            DataSet ds = dao.select_noChoosePj();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt_left.Rows.Add(ds.Tables[0].Rows[i][0]);
            }
            return dt_left;
        }
        DataTable CreateData_right()
        {
            dt_right = new DataTable();
            dt_right.Columns.Add("ShowProjectName");
            DataSet ds = dao.select_yesChoosePj();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt_right.Rows.Add(ds.Tables[0].Rows[i][0]);
            }
            return dt_right;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Lv_left.SelectedIndex != -1)
            {
                DataRow dr = dt_left.Rows[Lv_left.SelectedIndex];
                dao.update_pjStatus("1", dr[0] as string);
                dt_right.Rows.Add(dr[0] as string);
                dt_left.Rows.RemoveAt(Lv_left.SelectedIndex);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Constant.select == 1)
            {
                for (int i = 5; i < window.dataGrid.Columns.Count; )
                {
                    window.dataGrid.Columns.RemoveAt(i);
                }
            }
            else if (Constant.select == 0)
            {
                for (int i = 1; i < window.dataGrid.Columns.Count; )
                {
                    window.dataGrid.Columns.RemoveAt(i);
                }
            }
            for (int i = 0; i < dt_right.Rows.Count; i++)
            {
                if ("身高".Equals(dt_right.Rows[i][0]))
                {
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = dt_right.Rows[i][0], Binding = new Binding("身高"), Width = 60 });
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = "评分", Binding = new Binding("身高得分"), Width = 50 });
                }
                if ("体重".Equals(dt_right.Rows[i][0]))
                {
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = dt_right.Rows[i][0], Binding = new Binding("体重"), Width = 60 });
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = "评分", Binding = new Binding("体重得分"), Width = 50 });
                }
                if ("立定跳远".Equals(dt_right.Rows[i][0]))
                {
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = dt_right.Rows[i][0], Binding = new Binding("立定跳远"), Width = 60 });
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = "评分", Binding = new Binding("立定跳远得分"), Width = 50 });
                }
                if ("俯卧撑".Equals(dt_right.Rows[i][0]))
                {
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = dt_right.Rows[i][0], Binding = new Binding("俯卧撑"), Width = 60 });
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = "评分", Binding = new Binding("俯卧撑得分"), Width = 50 });
                }
                if ("仰卧起坐".Equals(dt_right.Rows[i][0]))
                {
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = dt_right.Rows[i][0], Binding = new Binding("仰卧起坐"), Width = 60 });
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = "评分", Binding = new Binding("仰卧起坐得分"), Width = 50 });
                }
                if ("引体向上".Equals(dt_right.Rows[i][0]))
                {
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = dt_right.Rows[i][0], Binding = new Binding("引体向上"), Width = 60 });
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = "评分", Binding = new Binding("引体向上得分"), Width = 50 });
                }
                if ("肺活量".Equals(dt_right.Rows[i][0]))
                {
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = dt_right.Rows[i][0], Binding = new Binding("肺活量"), Width = 60 });
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = "评分", Binding = new Binding("肺活量得分"), Width = 50 });
                }
                if ("折返跑".Equals(dt_right.Rows[i][0]))
                {
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = dt_right.Rows[i][0], Binding = new Binding("折返跑"), Width = 60 });
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = "评分", Binding = new Binding("折返跑得分"), Width = 50 });
                }
                if ("坐位体前屈".Equals(dt_right.Rows[i][0]))
                {
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = dt_right.Rows[i][0], Binding = new Binding("坐位体前屈"), Width = 70 });
                    window.dataGrid.Columns.Add(new DataGridTextColumn() { Header = "评分", Binding = new Binding("坐位体前屈得分"), Width = 50 });
                }
            }
            Close();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Lv_right.SelectedIndex != -1 && Lv_right.SelectedIndex != 0)
            {
                DataRow dr = dt_right.Rows[Lv_right.SelectedIndex];
                dao.update_pjStatus("0", dr[0] as string);
                dt_left.Rows.Add(dr[0] as string);
                dt_right.Rows.RemoveAt(Lv_right.SelectedIndex);
            }
        }
    }
}
