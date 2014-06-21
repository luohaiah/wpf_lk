using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANT_Managed_Library;
using Main.com.lk.DbHelper;
using System.Threading;
using Main.com.lk.Constant;
using Main.com.lk.util;

namespace Main.com.lk.Entity
{
    class Device
    {
        private byte cmd = 0xBF;
        private int studentNum_length;
        private string studentNum1, studentNum2, studentNum, result;
        private Dictionary<string, string> dic = new Dictionary<string, string>();
        private MyPort port;
        private Dao dao;
        private byte address1, address2;
        private MainWindow window;
        private Int32 result_int;

        public Device(MyPort port, Dao dao, MainWindow window)
        {
            this.port = port;
            this.dao = dao;
            this.window = window;
        }
        public void doData(byte[] data, string address)
        {
            //if ("B1".Equals(data[3]) && "B0".Equals(data[4]))//数据头
            if ("B0".Equals(data[5].ToString("X2")))//数据头
            {
                if (!dic.Keys.Contains(address))
                {
                    dic.Add(address, data[6].ToString("X2"));
                }
                studentNum_length = Convert.ToInt32(data[7].ToString(), 16);
                send_start_share();
            }
            else //数据部分
            {

                if ("01".Equals(data[5].ToString("X2")))
                {
                    studentNum1 = data[9].ToString("X2") + data[8].ToString("X2") + data[7].ToString("X2") + data[6].ToString("X2");
                    send_first_share();
                }
                else if ("02".Equals(data[5].ToString("X2")))
                {
                    studentNum2 = data[9].ToString("X2") + data[8].ToString("X2") + data[7].ToString("X2") + data[6].ToString("X2");
                    send_second_share();
                }
                else if ("03".Equals(data[5].ToString("X2")))
                {
                    studentNum = (studentNum2 + studentNum1);
                    result = (data[9].ToString("X2") + data[8].ToString("X2") + data[7].ToString("X2") + data[6].ToString("X2"));
                    studentNum = Convert.ToInt64(studentNum, 16).ToString();
                    int length = studentNum.Length;
                    if (studentNum_length > length)
                    {
                        for (int i = 0; i < studentNum_length - length; i++)
                        {
                            studentNum = "0" + studentNum;
                        }
                    }
                    result_int = Convert.ToInt32(result, 16);
                    string type;
                    dic.TryGetValue(address, out type);
                    if ("11".Equals(type))//立定跳远
                    {
                        dao.update_jump(studentNum, result_int);
                    }
                    else if ("0B".Equals(type))//肺活量
                    {
                        dao.update_vc(studentNum, result_int);
                    }
                    else if ("13".Equals(type))//折返跑
                    {
                        dao.update_shuttleRun(studentNum, result_int);
                    }
                    else if ("14".Equals(type))//仰卧起坐
                    {
                        dao.update_sitUp(studentNum, result_int);
                    }
                    else if ("1A".Equals(type))//俯卧撑
                    {
                        dao.update_pushUp(studentNum, result_int);
                    }
                    else if ("0F".Equals(type))//坐位体前屈
                    {
                        dao.update_proneness(studentNum, result_int / 10.0);
                    }
                    else if ("0C".Equals(type))//身高体重
                    {
                        double height = Convert.ToInt32(data[7].ToString("X2") + data[6].ToString("X2"), 16) / 10.0;
                        double weight = Convert.ToInt32(data[9].ToString("X2") + data[8].ToString("X2"), 16) / 10.0;
                        dao.update_height_weight(studentNum, height, weight);
                    }
                    send_Third_share();
                }
                else if ("BF".Equals(data[5].ToString("X2")))
                {
                    Constant.Constant.is_remove = true;
                    Constant.Constant.address = address;
                    dic.Remove(address);
                    Thread.Sleep(1000);
                    window.Dispatcher.Invoke(new Action(delegate
                    {
                        window.dataGrid.ItemsSource = dao.get_table_all().DefaultView;
                    }));
                }
            }
        }
        public void sendData(string[] address)
        {
            address1 = Convert.ToByte(address[0], 16);
            address2 = Convert.ToByte(address[1], 16);
            //channel.sendAcknowledgedData(new byte[] { address1, address2, cmd, 0, 0, 0, 0, 0 });//测试
            //   channel.sendBurstTransfer(new byte[] { address1, address2, cmd, 0, 0, 0, 0, 0 });//测试
            port.sendMessage(1, new byte[] { address1, address2, cmd, 0, 0, 0, 0, 0 });
            send_cancel_share();
        }

        private void send_start_share()
        {
            cmd = 0xC1;
        }
        private void send_first_share()
        {
            cmd = 0xD1;
        }
        private void send_second_share()
        {
            cmd = 0xD2;
        }
        private void send_Third_share()
        {
            cmd = 0xD3;
        }
        private void send_cancel_share()
        {
            cmd = 0xBF;
        }
    }
}
