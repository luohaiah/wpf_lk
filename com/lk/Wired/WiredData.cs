using System;
using System.Text;
using System.IO.Ports;
using System.Timers;
using Main.com.lk.DbHelper;
using System.Threading;
using System.Windows;
using Main.com.lk.ui;
namespace Main.com.lk.Wired
{
    public class WiredData
    {
        private System.IO.Ports.SerialPort com;
        private System.Timers.Timer MyTimer;
        private System.Timers.Timer MyTimer2;
        private static WiredData wd;
        private Dao dao;
        private DataCollection window;
        private StringBuilder builder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。  
        private byte[] f1 = new byte[] { 0xF1, 00, 00, 00, 00, 00, 00, 00, 0xF1 };
        private byte[] f2 = new byte[] { 0xF2, 00, 00, 00, 00, 00, 00, 00, 0xF2 };
        private byte[] f3 = new byte[] { 0xF3, 00, 00, 00, 00, 00, 00, 00, 0xF3 };
        private byte[] f4 = new byte[] { 0xF4, 00, 00, 00, 00, 00, 00, 00, 0xF4 };
        private int time_E1 = DateTime.Now.Second;
        private bool is_start_E1 = false;
        private WiredData(Window window)
        {
            dao = Dao.getInstance(window);
        }
        public static WiredData get_Instance(Window window)
        {
            if (wd == null)
            {
                wd = new WiredData(window);
            }
            return wd;
        }

        public void init_SerialPort(DataCollection window, string com_name)
        {
            this.window = window;
            com = new SerialPort();
            com.PortName = com_name;
            com.BaudRate = 115200;
            com.DataBits = 8;
            com.Parity = Parity.None;
            com.StopBits = StopBits.One;
            com.ReceivedBytesThreshold = 1;
            com.Open();
            com.DataReceived += com_DataReceived;
            MyTimer = new System.Timers.Timer();
            MyTimer.Elapsed += MyTimer_Elapsed;
            MyTimer.Interval = 100;
            MyTimer.AutoReset = true;
            MyTimer.Enabled = true;
            MyTimer2 = new System.Timers.Timer();
            MyTimer2.Elapsed += MyTimer2_Elapsed;
            MyTimer2.Interval = 500;
            MyTimer2.AutoReset = true;
            MyTimer2.Enabled = true;
        }

        void MyTimer2_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (is_start_E1)
            {
                if (DateTime.Now.Second - time_E1 > 3)
                {
                    sendF4();
                }
            }

        }
        void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            com.Write(f2, 0, f2.Length);//初始状态发送F2命令
        }

        void com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string studentNum = null;
            bool checkSum_result;
            string temp;
            Thread.Sleep(10);//需要休眠让缓冲区填满想要的数据，休眠时间要大于发送端的频率
            if (!com.IsOpen)
            {
                return;
            }
            int n = com.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致  
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据  
            com.Read(buf, 0, n);//读取缓冲数据  
            builder.Clear();//清除字符串构造器的内容  
            if (buf.Length == 0)
            {
                return;
            }
            if (buf[0].ToString("X2").Equals("D1"))//收到D1命令回复F1命令
            {
                checkSum_result = ((byte)(buf[0] + buf[1] + buf[2] + buf[3] + buf[4] +
                buf[5] + buf[6] + buf[7])).Equals(buf[8]);
                if (checkSum_result)
                {
                    f2 = f1;
                }
            }
            else if (buf[0].ToString("X2").Equals("D2"))//收到D2命令回复F2命令
            {
                MyTimer2.Enabled = false;
                checkSum_result = ((byte)(buf[0] + buf[1] + buf[2] + buf[3] + buf[4] +
                buf[5] + buf[6] + buf[7])).Equals(buf[8]);
                if (checkSum_result)
                {
                    MyTimer.Enabled = true;
                    f2 = new byte[] { 0xF2, 00, 00, 00, 00, 00, 00, 00, 0xF2 };
                }
                window.window.Dispatcher.Invoke(new Action(delegate
              {
                  window.window.dataGrid.ItemsSource = dao.get_table_all().DefaultView;
              }));
            }
            else if (buf[0].ToString("X2").Equals("E1"))//收到E1命令
            {
                is_start_E1 = true;
                time_E1 = DateTime.Now.Second;
                MyTimer.Enabled = false;
                checkSum_result = ((byte)(buf[0] + buf[1] + buf[2] + buf[3] + buf[4] +
               buf[5] + buf[6] + buf[7] + buf[8] + buf[9] + buf[10] + buf[11] + buf[12] + buf[13] + buf[14])).Equals(buf[15]);
                if (checkSum_result)//校验和正确回复F3,数据格式：【E1,类型,学号长度,学号,成绩,校验和】
                {
                    byte type = buf[1];
                    byte studentNum_length = buf[2];
                    for (int i = 10; i >= 3; i--)
                    {
                        if (buf[i].ToString("X2").Length == 1)
                        {
                            builder.Append("0" + buf[i].ToString("X2"));
                        }
                        else
                        {
                            builder.Append(buf[i].ToString("X2"));
                        }
                    }
                    temp = Convert.ToInt64(builder.ToString(), 16).ToString();
                    if (studentNum_length > temp.Length)
                    {
                        int value = studentNum_length - temp.Length;
                        for (int i = 0; i < value; i++)
                        {
                            studentNum = "0" + temp;

                        }

                    }
                    else
                    {
                        studentNum = temp;//学号
                    }
                    Console.Write(studentNum);
                    builder.Clear();
                    if (type != 12)//不是身高体重
                    {
                        for (int i = 14; i >= 11; i--)
                        {
                            builder.Append(buf[i].ToString("X2"));

                        }
                        string result = Convert.ToInt32(builder.ToString(), 16).ToString();//成绩
                        switch (type)
                        {
                            case 17://立定跳远
                                dao.update_jump(studentNum, Convert.ToInt32(result));
                                break;
                            case 11://肺活量 
                                dao.update_vc(studentNum, Convert.ToInt32(result));
                                break;
                            case 19://折返跑
                                dao.update_shuttleRun(studentNum, Convert.ToInt32(result));
                                break;
                            case 20://仰卧起坐
                                dao.update_sitUp(studentNum, Convert.ToInt32(result));
                                break;
                            case 26://俯卧撑
                                dao.update_pushUp(studentNum, Convert.ToInt32(result));
                                break;
                            case 15://坐位体前屈
                                dao.update_proneness(studentNum, Convert.ToInt32(result) / 10.0);
                                break;
                            case 25://引体向上
                                dao.update_pullUp(studentNum, Convert.ToInt32(result));
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        for (int i = 14; i >= 13; i--)
                        {
                            builder.Append(buf[i].ToString("X2"));

                        }
                        double weight = (Convert.ToInt32(builder.ToString(), 16) / 10.0);//体重
                        builder.Clear();
                        for (int i = 12; i >= 11; i--)
                        {
                            builder.Append(buf[i].ToString("X2"));

                        }
                        double height = (Convert.ToInt32(builder.ToString(), 16) / 10.0);//身高
                        dao.update_height_weight(studentNum, height, weight);
                    }

                    sendF3();
                }
                else//校验和错误回复F4
                {
                    sendF4();
                }
            }
        }
        private void sendF3()
        {
            if (!com.IsOpen)
            {
                return;
            }
            com.Write(f3, 0, f3.Length);
        }
        private void sendF4()
        {
            if (!com.IsOpen)
            {
                return;
            }
            com.Write(f4, 0, f4.Length);
        }
        private void sendF2()
        {
            if (!com.IsOpen)
            {
                return;
            }
            com.Write(f2, 0, f2.Length);
        }
        public void close_port()
        {
            MyTimer.Enabled = false;
            MyTimer2.Enabled = false;
            com.Close();
        }
    }
}
