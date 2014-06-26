using Main.com.lk.DbHelper;
using Main.com.lk.Entity;
/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2014-5-9
 * Time: 15:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
namespace Main.com.lk.util
{
    public class MyPort
    {
        private StringBuilder builder = new StringBuilder();
        private SerialPort sp;
        public const String COM1 = "com1";
        public const String COM2 = "com2";
        public const String COM3 = "com3";
        public const String COM4 = "com4";
        public const String COM5 = "com5";
        public const String COM6 = "com6";
        public const String COM7 = "com7";
        public const String COM8 = "com8";
        private MainWindow window;
        private bool is_has = false;
        public List<string> list_address = new List<string>();
        private Dao dao;
        public Dictionary<string, Device> dic_device = new Dictionary<string, Device>();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private int i = 0;
        private static MyPort myPort;
        public MyPort(MainWindow window)
        {
            this.window = window;
            dao = Dao.getInstance(window);
            sp = new SerialPort();

        }
        public static MyPort getInstance(MainWindow window)
        {
            if (myPort == null)
            {
                myPort = new MyPort(window);
            }
            return myPort;
        }
        public const int Rate_300 = 300;
        public const int Rate_600 = 600;
        public const int Rate_1200 = 1200;
        public const int Rate_2400 = 2400;
        public const int Rate_4800 = 4800;
        public const int Rate_9600 = 9600;
        public const int Rate_19200 = 19200;
        public const int Rate_38400 = 38400;
        public const int Rate_43000 = 43000;
        public const int Rate_56000 = 56000;
        public const int Rate_57600 = 57600;
        public const int Rate_115200 = 115200;


        private bool isUpdate_runway1 = true;
        private bool isUpdate_runway2 = true;
        private bool isUpdate_runway3 = true;
        private bool isUpdate_runway4 = true;
        private bool isUpdate_runway5 = true;
        private bool isUpdate_runway6 = true;
        private bool isUpdate_runway7 = true;
        private bool isUpdate_runway8 = true;
        public bool IsUpdate_runway1
        {
            set { isUpdate_runway1 = value; }
            get { return isUpdate_runway1; }
        }
        public bool IsUpdate_runway2
        {
            set { isUpdate_runway2 = value; }
            get { return isUpdate_runway2; }
        }
        public bool IsUpdate_runway3
        {
            set { isUpdate_runway3 = value; }
            get { return isUpdate_runway3; }
        }
        public bool IsUpdate_runway4
        {
            set { isUpdate_runway4 = value; }
            get { return isUpdate_runway4; }
        }
        public bool IsUpdate_runway5
        {
            set { isUpdate_runway5 = value; }
            get { return isUpdate_runway5; }
        }
        public bool IsUpdate_runway6
        {
            set { isUpdate_runway6 = value; }
            get { return isUpdate_runway6; }
        }
        public bool IsUpdate_runway7
        {
            set { isUpdate_runway7 = value; }
            get { return isUpdate_runway7; }
        }
        public bool IsUpdate_runway8
        {
            set { isUpdate_runway8 = value; }
            get { return isUpdate_runway8; }
        }
        /// <summary>
        /// 显示所有的com端口
        /// </summary>
        public void showPorts()
        {
            String[] ports = SerialPort.GetPortNames();
            int len = ports.Length;
            for (int i = 0; i < len; i++)
            {
                Console.Out.WriteLine(ports[i]);
            }
        }
        /// <summary>
        /// 设置端口
        /// </summary>
        public void setPortName(String portName)
        {
            sp.PortName = portName;
        }
        /// <summary>
        /// 设置波特率
        /// </summary>
        public void setBaudRate(int baudRate)
        {
            sp.BaudRate = baudRate;
        }
        /// <summary>
        /// 设置数据位
        /// </summary>
        public void setDataBits(int dataBits)
        {
            sp.DataBits = dataBits;
        }
        /// <summary>
        /// 打开端口（无参数）
        /// </summary>
        public void openPort()
        {
            sp.Open();
            recive();
        }
        /// <summary>
        /// 打开端口（端口号，波特率，数据位）
        /// </summary>
        public void openPort(String portName, int baudRate, int dataBits)
        {

            sp.PortName = portName;
            sp.BaudRate = baudRate;
            sp.DataBits = dataBits;
            sp.StopBits = StopBits.One;
            sp.Parity = Parity.None;
            openPort();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //if (Constant.Constant.is_remove)
            //{
            //    Constant.Constant.is_remove = false;
            //    list_address.Remove(Constant.Constant.address);
            //    dic_device.Remove(Constant.Constant.address);
            //    Constant.Constant.address = "";
            //}
            int count = list_address.Count;
            if (count > 0)
            {
                window.Dispatcher.Invoke(new Action(delegate
                {
                    window.animation.Visibility = Visibility.Visible;
                    window.Lable_hint.Content = "已连接" + count + "台主机";
                    window.Lable_hint.Visibility = Visibility.Visible;
                }));
                if (i >= list_address.Count)
                {
                    i--;
                }
                string[] address = list_address.ElementAt(i).Split(',');
                Device device;
                dic_device.TryGetValue(list_address.ElementAt(i), out device);
                device.sendData(address);
                i++;
                if (i >= count)
                {
                    i = 0;
                }
            }
            else
            {
                window.Dispatcher.Invoke(new Action(delegate
                {
                    window.animation.Visibility = Visibility.Collapsed;
                    window.Lable_hint.Visibility = Visibility.Collapsed;
                }));
                //channel_share.sendAcknowledgedData(new byte[] { 50, 50, (byte)0xB0, 0, 0, 0, 0, 0 });
                sendMessage(1, new byte[] { 50, 50, (byte)0xB0, 0, 0, 0, 0, 0 });
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        //public void sendData(Byte[] data)
        //{
        //    sp.Write(data, 0, data.Length);
        //}

        public void recive()
        {
            sp.DataReceived += com_DataReceived;
        }

        /// <summary>
        /// 处理接收数据
        /// </summary>
        void com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            bool checkSum_result;
            Thread.Sleep(20);//需要休眠让缓冲区填满想要的数据，休眠时间要大于发送端的频率
            int n = sp.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致  
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据  
            sp.Read(buf, 0, n);//读取缓冲数据  
            foreach (var item in buf)
            {
                builder.Append(item.ToString("X2"));
            }
            Console.Out.WriteLine(builder.ToString());
            builder.Clear();//清除字符串构造器的内容  
            if (buf.Length == 0)
            {
                return;
            }
            if (buf[0].ToString("X2").Equals("BC") && buf[1].Equals(2))//配置通道
            {
                checkSum_result = ((byte)(buf[0] + buf[1] + buf[2] + buf[3] + buf[4] +
               buf[5] + buf[6] + buf[7] + buf[8] + buf[9])).Equals(buf[10]);
                if (checkSum_result)
                {
                    is_has = false;
                    string share_address = buf[5].ToString("X2") + "," + buf[6].ToString("X2");
                    foreach (var item in list_address)
                    {
                        if (share_address.Equals(item))
                        {
                            is_has = true;
                        }
                    }
                    if ("B2".Equals(buf[4].ToString("X2")))
                    {
                        send_cancel(buf[5], buf[6]);
                        if (!is_has)
                        {
                            Device device = new Device(this, dao, window);
                            dic_device.Add(share_address, device);
                            list_address.Add(share_address);
                        }
                        else
                        {
                            is_has = false;
                        }
                    }
                }
            }
            if (buf[0].ToString("X2").Equals("BC") && (buf[1].Equals(1) || buf[1].Equals(0)))//共享通道
            {


                string address1 = buf[2].ToString("X2") + "," + buf[3].ToString("X2");
                Device device1;
                dic_device.TryGetValue(address1, out device1);
                if (device1 != null)
                {
                    device1.doData(buf, address1);
                }
            }
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void close()
        {
            sp.Close();
        }

        /// <summary>
        /// 分配并开启通道
        /// 30 共享 10 独立
        /// </summary>
        public void openChannel(int channelNum, int deviceId, int deviceType, int period, int frequency, byte channelType, byte channelType2)
        {
            //0xAA | 通道号 | 通道类型 | 设备号(2 byte)    | 设备类型 | 通道周期(2 byte)| 通道频率 | 0x00 | 校验和
            int check = Convert.ToInt32("AA", 16)
                + channelNum
                + Convert.ToInt32(channelType.ToString(), 16)
                + Convert.ToInt16((char)deviceId)
                + Convert.ToInt16((char)(deviceId >> 8))
                + deviceType
                + Convert.ToInt32((char)period)
                + Convert.ToInt32((char)(period >> 8))
                + frequency;
            byte[] res = new byte[11];
            res[0] = 0xAA;
            res[1] = (byte)channelNum;
            res[2] = channelType2;
            res[3] = (byte)deviceId;
            res[4] = (byte)(deviceId >> 8);
            res[5] = (byte)deviceType;
            res[6] = (byte)(period & 0xff);
            res[7] = (byte)(period >> 8);
            res[8] = (byte)frequency;
            res[9] = 0x00;
            res[10] = (byte)(check & 0xff);
            sp.Write(res, 0, 11);
            if (channelNum == 1)
            {
                timer.Enabled = true;
                timer.Interval = 16;
                timer.Tick += timer_Tick;
                timer.Start();
            }
        }

        /// <summary>
        /// 关闭并注销通道
        /// </summary>
        public void closeChannel(int channelNum)
        {
            timer.Enabled = false;
            timer.Stop();
            int check = Convert.ToInt32("AB", 16)
                + channelNum;
            byte[] res = new byte[11];
            res[0] = 0xAB;
            res[1] = (byte)channelNum;
            for (int i = 2; i < 10; i++)
            {
                res[i] = 0x00;
            }
            res[10] = (byte)(check & 0xff);
            sp.Write(res, 0, 11);
        }

        /// <summary>
        /// 发送应答数据
        /// </summary>
        public void sendMessage(int channelNum, byte[] data)
        {
            int check = 172  //Convert.ToInt32("AC", 16)
                + channelNum;
            for (int j = 0; j < data.Length; j++)
            {
                check += data[j];
            }
            byte[] res = new byte[11];
            res[0] = 0xAC;
            res[1] = (byte)channelNum;
            for (int i = 2; i < data.Length + 2; i++) //10
            {
                res[i] = data[i - 2];
            }
            res[10] = (byte)(check & 0xff);
            sp.Write(res, 0, 11);
        }

        /// <summary>
        /// 复位信息
        /// </summary>
        public void reset(int channelNum)
        {
            byte[] res = new byte[11];
            res[0] = 0xBF;
            res[1] = (byte)channelNum;
            for (int i = 1; i < 10; i++)
            {
                res[i] = 0x00;
            }
            res[10] = 0xBF;
            sp.Write(res, 0, 11);
        }

        private void send_cancel(byte address1, byte address2)
        {
            //  channel_general.sendAcknowledgedData(new byte[] { 0, 0, (byte)0xBF, address1, address2, 0, 0, 0 });
            sendMessage(2, new byte[] { 0, 0, (byte)0xBF, address1, address2, 0, 0, 0 });
        }
    }
}