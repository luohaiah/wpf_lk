
using Main.com.lk.Ant;
using Main.com.lk.DbHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using Main.com.lk.Constant;
using Main.com.lk.ui;
using System.Threading;
using System.Windows.Media.Animation;

namespace Main
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private AntManage antmanage;
        private Dao dao;
        private int index = 1;//当前页数
        private int totalpage;//总页数
        private NodeNew node;
        private DataView dv;
        private BackgroundWorker work;
        private bool is_nodeAll = true;
        public MainWindow(string username)
        {
            InitializeComponent();
            image_user.Content = username;
            antmanage = new AntManage(this);
            dao = Dao.getInstance(this);
            init_GridView(true);
            Lb_current.Content = index;
            transform(dao.select_Grade());
        }


        private void getTotalPage(int type, string grade, string classname, string sex)
        {
            int temp = 0;
            switch (type)
            {
                case 0:
                    temp = dao.get_totalCount();
                    break;
                case 1:
                    temp = dao.get_totalCount_grade(grade);
                    break;
                case 2:
                    temp = dao.get_totalCount_class(grade, classname);
                    break;
                case 3:
                    temp = dao.get_totalCount_sex(grade, classname, sex);
                    break;
                default:
                    break;
            }

            if (temp % 60 == 0)
            {
                totalpage = temp / 60;
            }
            else
            {
                totalpage = temp / 60 + 1;
            }
        }

        private void Data_export_Click(object sender, RoutedEventArgs e)
        {
            export_data();
        }

        private void Data_import_Click(object sender, RoutedEventArgs e)
        {
            import_data();
        }

        private void init_GridView(bool is_init)
        {
            dataGrid.Columns.Clear();
            DataSet ds = dao.select_yesChoosePj();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (Constant.studentNum.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.studentNum), Width = 140 });
                }
                else if (Constant.name.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.name), Width = 70 });
                }
                else if (Constant.sex.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.sex), Width = 50 });
                }
                else if (Constant.grade.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.grade), Width = 60 });
                }
                else if (Constant.clname.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.clname), Width = 60 });
                }
                else if (Constant.height.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.height), Width = 60 });
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = this.FindResource("score") as string, Binding = new System.Windows.Data.Binding(Constant.height_score), Width = 50 });
                }
                else if (Constant.weight.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.weight), Width = 60 });
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = this.FindResource("score") as string, Binding = new System.Windows.Data.Binding(Constant.weight_score), Width = 50 });
                }
                else if (Constant.jump.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.jump), Width = 60 });
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = this.FindResource("score") as string, Binding = new System.Windows.Data.Binding(Constant.jump_score), Width = 50 });
                }
                else if (Constant.vc.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.vc), Width = 60 });
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = this.FindResource("score") as string, Binding = new System.Windows.Data.Binding(Constant.vc_score), Width = 50 });
                }
                else if (Constant.tiqianqu.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.tiqianqu), Width = 70 });
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = this.FindResource("score") as string, Binding = new System.Windows.Data.Binding(Constant.tiqianqu_score), Width = 50 });
                }
                else if (Constant.sitUp.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.sitUp), Width = 60 });
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = this.FindResource("score") as string, Binding = new System.Windows.Data.Binding(Constant.sitUp_score), Width = 50 });
                }
                else if (Constant.pushUp.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.pushUp), Width = 60 });
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = this.FindResource("score") as string, Binding = new System.Windows.Data.Binding(Constant.pushUp_score), Width = 50 });
                }
                else if (Constant.pullUp.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.pullUp), Width = 60 });
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = this.FindResource("score") as string, Binding = new System.Windows.Data.Binding(Constant.pullUp_score), Width = 50 });
                }
                else if (Constant.Shuttlerun.Equals(ds.Tables[0].Rows[i][0]))
                {
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = ds.Tables[0].Rows[i][0], Binding = new System.Windows.Data.Binding(Constant.Shuttlerun), Width = 60 });
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = this.FindResource("score") as string, Binding = new System.Windows.Data.Binding(Constant.Shuttlerun_score), Width = 50 });
                }
            }
            if (is_init)
            {
                dataGrid.ItemsSource = dao.get_table_first().DefaultView;
            }
        }
        private void import_data()
        {
            hint hint = new Main.hint();
            hint.ShowDialog();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel文件(*.xls)|*.xls";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = dlg.FileName;
                if (dao.importExcel(filePath))
                {
                    dataGrid.ItemsSource = dao.get_table_first().DefaultView;
                    transform(dao.select_Grade());
                    System.Windows.MessageBox.Show(this.FindResource("import_success") as string);
                }
                else
                {
                    System.Windows.MessageBox.Show(this.FindResource("import_fail") as string);
                }
            }
        }
        private void export_data()
        {
            int choose = 0;
            NodeNew node;
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = ("Excel 文件(*.xls)|*.xls");//指定文件后缀名为Excel 文件。  
            choose = getChoose();
            node = TreeView_data.SelectedItem as NodeNew;
            switch (choose)
            {
                case 0:
                    saveFile.FileName = this.FindResource("testData") as string;
                    break;
                case 1:
                    saveFile.FileName = this.FindResource("testData") as string + "(" + node.Name + ")";
                    break;
                case 2:
                    saveFile.FileName = this.FindResource("testData") as string + "(" + node.grade + "," + node.Name + ")";
                    break;
                case 3:
                    saveFile.FileName = this.FindResource("testData") as string + "(" + node.grade + "," + node.classname + "," + node.Name + ")";
                    break;
                case 4:
                    saveFile.FileName = this.FindResource("tempData") as string;
                    break;
                case 5:
                    saveFile.FileName = this.FindResource("testData") as string + "(" + this.FindResource("specific") + ")";
                    break;
                default:
                    break;
            }
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = saveFile.FileName;

                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);//如果文件存在删除文件。  
                }
                if (dao.export_Excel(filename, choose, node))
                {
                    System.Windows.MessageBox.Show(this.FindResource("export_success") as string);
                }
                else
                {
                    System.Windows.MessageBox.Show(this.FindResource("export_fail") as string);
                }
            }
        }

        //有线传输（串口通信）
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            new WiredManage(this).ShowDialog();
        }

        private void Btn_prev_Click(object sender, RoutedEventArgs e)
        {
            if (TreeView_data.SelectedItem != null)
            {
                node = (TreeView_data.SelectedItem as NodeNew);
                is_nodeAll = TreeView_data.SelectedItem.ToString().Contains(toast("all"));
                if (!is_nodeAll && node == null)
                {
                    return;
                }
            }
            if (index > 2)
            {
                index--;
                Lb_current.Content = index;
                start_Thread();
            }
            else
            {
                if (index == 2)
                {
                    index--;
                }
                Lb_current.Content = index;
                start_Thread();
            }
        }

        private void Btn_next_Click(object sender, RoutedEventArgs e)
        {

            if (TreeView_data.SelectedItem != null)
            {
                node = (TreeView_data.SelectedItem as NodeNew);
                if (!TreeView_data.SelectedItem.ToString().Contains(toast("all")) && node == null)
                {
                    return;
                }
            }
            if (index < totalpage)
            {
                index++;
                Lb_current.Content = index;
                start_Thread();
            }

        }

        void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGrid.ItemsSource = dv;
        }

        void work_DoWork(object sender, DoWorkEventArgs e)
        {
            work.ReportProgress(1);
            adapterData(index, node);
        }

        private void Btn_go_Click(object sender, RoutedEventArgs e)
        {
            if (TreeView_data.SelectedItem != null)
            {
                node = (TreeView_data.SelectedItem as NodeNew);
                if (!TreeView_data.SelectedItem.ToString().Contains(toast("all")) && node == null)
                {
                    return;
                }
            }
            index = Convert.ToInt32(Tb_go.Text);
            Lb_current.Content = index;
            start_Thread();

        }
        private void adapterData(int index, NodeNew node)
        {
            int choose = 0;
            if (node == null)
            {
                if (index < 2)
                {
                    dv = dao.get_table_first().DefaultView;
                }
                else
                {
                    dv = dao.get_table(index).DefaultView;
                }
            }
            else
            {
                choose = node.floor;
                switch (choose)
                {
                    case 1:
                        if (index < 2)
                        {
                            dv = dao.get_table_grade_first(node.Name).DefaultView;
                        }
                        else
                        {
                            dv = dao.get_table_grade(index, node.Name).DefaultView;
                        }
                        break;
                    case 2:
                        if (index < 2)
                        {
                            dv = dao.get_table_class_first(node.grade, node.Name).DefaultView;
                        }
                        else
                        {
                            dv = dao.get_table_class(index, node.grade, node.Name).DefaultView;
                        }
                        break;
                    case 3:
                        if (index < 2)
                        {
                            dv = dao.get_table_sex_first(node.grade, node.classname, node.Name).DefaultView;
                        }
                        else
                        {
                            dv = dao.get_table_sex(index, node.grade, node.classname, node.Name).DefaultView;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 0代表全部 1代表年级 2代表班级 3代表性别
        /// </summary>
        /// <param name="type"></param>
        private void initPage(int type, string grade, string classname, string sex)
        {
            index = 1;
            Lb_current.Content = index;
            getTotalPage(type, grade, classname, sex);
            Lb_total.Content = totalpage;
        }


        private void Window_Activated(object sender, EventArgs e)
        {
            getTotalPage(0, null, null, null);
            Lb_total.Content = totalpage;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// 赋值给Node集合
        /// </summary>
        /// <param name="dt"></param>
        public void transform(DataTable dt)
        {
            List<NodeNew> listNode = new List<NodeNew>();
            foreach (DataRow dr in dt.Rows)
            {
                NodeNew temp = ConvertToTableModel(dr, Constant.grade);
                temp.floor = 1;
                //赋值
                listNode.Add(temp);
            }

            List<NodeNew> outputList = Bind(listNode);
            Tv_Item.ItemsSource = outputList;
        }

        /// <summary>
        /// 赋值给Node
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static NodeNew ConvertToTableModel(DataRow dr, string name)
        {
            NodeNew result = new NodeNew();
            if (false == DBNull.Value.Equals(dr[name])) result.Name = dr[name].ToString();
            return result;
        }

        /// <summary>
        /// 绑定树
        /// </summary>
        List<NodeNew> Bind(List<NodeNew> nodes)
        {
            //定义一个返回值变量
            List<NodeNew> outputList = new List<NodeNew>();

            //循环遍历控件
            for (int i = 0; i < nodes.Count; i++)
            {
                outputList.Add(nodes[i]);

                FindDownward(nodes[i], null);
            }
            return outputList;
        }

        /// <summary>
        /// 向下查找
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        void FindDownward(NodeNew node, string grade)
        {
            DataTable dt = null;
            NodeNew temp = null;
            if (node == null) return;
            //寻找子节点
            if (grade == null)
            {

                dt = dao.select_Class(node.Name);
            }
            else
            {
                dt = dao.select_Sex(grade, node.Name);
            }
            foreach (DataRow dr in dt.Rows)
            {
                if (grade == null)
                {
                    temp = ConvertToTableModel(dr, Constant.clname);
                    temp.floor = 2;
                    temp.grade = node.Name;
                    node.Nodes1.Add(temp);
                    FindDownward(temp, node.Name);
                }
                else
                {
                    temp = ConvertToTableModel(dr, Constant.sex);
                    temp.floor = 3;
                    temp.grade = grade;
                    temp.classname = node.Name;
                    node.Nodes1.Add(temp);
                }
            }
        }

        private void TreeView_data_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string name = null;
            int floor = 0;
            string grade, classname;
            if ((e.NewValue as NodeNew) != null)
            {
                floor = (e.NewValue as NodeNew).floor;
                name = (e.NewValue as NodeNew).Name;

                switch (floor)
                {
                    case 1://年级
                        initPage(1, name, null, null);
                        dataGrid.ItemsSource = dao.get_table_grade_first(name).DefaultView;
                        break;
                    case 2://班级
                        grade = (e.NewValue as NodeNew).grade;
                        initPage(2, grade, name, null);
                        dataGrid.ItemsSource = dao.get_table_class_first(grade, name).DefaultView;
                        break;
                    case 3://性别
                        grade = (e.NewValue as NodeNew).grade;
                        classname = (e.NewValue as NodeNew).classname;
                        initPage(3, grade, classname, name);
                        dataGrid.ItemsSource = dao.get_table_sex_first(grade, classname, name).DefaultView;
                        break;
                    default:
                        break;
                }
            }
        }
        private int getChoose()
        {
            int choose = 0;
            if (TreeView_data.SelectedItem == null || TreeView_data.SelectedItem.ToString().Contains(toast("all")))
            {
                choose = 0;

            }
            else if (TreeView_data.SelectedItem.ToString().Contains(toast("temp")))
            {
                choose = 4;
            }
            else if (TreeView_data.SelectedItem.ToString().Contains(toast("specific")))
            {
                choose = 5;
            }
            else
            {
                if ((TreeView_data.SelectedItem as NodeNew).floor == 1)
                {
                    choose = 1;
                }
                if ((TreeView_data.SelectedItem as NodeNew).floor == 2)
                {
                    choose = 2;
                }
                if ((TreeView_data.SelectedItem as NodeNew).floor == 3)
                {
                    choose = 3;
                }
            }
            return choose;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void start_Thread()
        {

            work = new BackgroundWorker();
            work.WorkerReportsProgress = true;
            work.WorkerSupportsCancellation = true;
            work.DoWork += work_DoWork;
            work.RunWorkerCompleted += work_RunWorkerCompleted;
            work.ProgressChanged += work_ProgressChanged;
            work.RunWorkerAsync();
        }

        void work_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            BackgroundWorker work1 = new BackgroundWorker();
            work1.DoWork += work1_DoWork;
            work1.RunWorkerAsync();
        }

        void work1_DoWork(object sender, DoWorkEventArgs e)
        {
            Constant.select = 1;
            showHideBaseInfo("1");
            this.Dispatcher.Invoke(new Action(delegate
            {
                init_GridView(false);
                initPage(0, null, null, null);
                dataGrid.ItemsSource = dao.get_table_first().DefaultView;
            }));
        }
        //删除数据
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            dao.delete_data(getChoose(), TreeView_data.SelectedItem as NodeNew);
            dataGrid.ItemsSource = null;
            transform(dao.select_Grade());
        }
        //临时数据
        private void TreeViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            BackgroundWorker work3 = new BackgroundWorker();
            work3.DoWork += work3_DoWork;
            work3.RunWorkerAsync();
        }

        void work3_DoWork(object sender, DoWorkEventArgs e)
        {
            Constant.select = 0;
            this.Dispatcher.Invoke(new Action(delegate
            {
                showHideBaseInfo("0");
                init_GridView(false);
                dataGrid.ItemsSource = dao.get_table_temp_first().DefaultView;
                index = 1;
                totalpage = 1;
                Lb_current.Content = index;
                Lb_total.Content = totalpage;
            }));
        }

        private string toast(string key)
        {
            return this.FindResource(key) as string;
        }


        private void showAndHideCol(object sender, RoutedEventArgs e)
        {
            Show showdialog = new Show(this);
            showdialog.ShowDialog();
        }

        private void window_min(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1500);
            Dispatcher.Invoke(new Action(delegate
            {
                this.Close();
            }));
        }


        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 获取鼠标相对标题栏位置  
            Point position = e.GetPosition(TitleBar);

            // 如果鼠标位置在标题栏内，允许拖动  
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < TitleBar.ActualWidth && position.Y >= 0 && position.Y < TitleBar.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        private void test(object sender, RoutedEventArgs e)
        {
            if (!antmanage.initAnt())
            {
                antmanage.shutdown();
            }
        }

        private void Tbox_search_click(object sender, MouseButtonEventArgs e)
        {
            if (Tbox_search.Text.Equals(toast("search")))
            {
                Tbox_search.Clear();
            }
        }

        private void Tbox_search_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (Tbox_search.Text.Length == 0)
            {
                Tbox_search.Text = toast("search");
            }
        }

        private void Image_search_click(object sender, MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show("123");
        }

        private void Tbox_search_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.Controls.TextBox txt = sender as System.Windows.Controls.TextBox;

            //屏蔽非法按键
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal)
            {
                if (e.Key == Key.Decimal)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.OemPeriod) && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
            {
                if (e.Key == Key.OemPeriod)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// status 0=隐藏 1=显示
        /// </summary>
        /// <param name="status"></param>
        private void showHideBaseInfo(string status)
        {
            dao.update_pjStatus(status, "姓名");
            dao.update_pjStatus(status, "性别");
            dao.update_pjStatus(status, "年级");
            dao.update_pjStatus(status, "班级");
        }

        private void Tv_Item_Selected(object sender, RoutedEventArgs e)
        {
            BackgroundWorker work2 = new BackgroundWorker();
            work2.DoWork += work2_DoWork;
            work2.RunWorkerAsync();
        }

        void work2_DoWork(object sender, DoWorkEventArgs e)
        {
            Constant.select = 1;
            showHideBaseInfo("1");
            this.Dispatcher.Invoke(new Action(delegate
            {
                init_GridView(false);
            }));
        }

        private void test2(object sender, RoutedEventArgs e)
        {
        }

        private void close(object sender, RoutedEventArgs e)
        {
            antmanage.shutdown();
            dao.closeDb();
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();

        }
    }


    /// <summary>
    /// 节点类
    /// </summary>
    public class NodeNew
    {
        public NodeNew()
        {
            this.Nodes1 = new List<NodeNew>();
        }
        public string Name { get; set; }
        public List<NodeNew> Nodes1 { get; set; }
        public int floor;//用于标识在第几层
        public string grade;
        public string classname;
    }

}
