using Main.com.lk.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Main.com.lk.Constant;
using System.Threading;

namespace Main.com.lk.Ant
{
    class AntManage2
    {
        private MainWindow window;
        private MyPort port;
        public AntManage2(MainWindow window)
        {
            this.window = window;
            port = new MyPort(window);
        }
        public void initAnt()
        {
            port.openPort("COM4", MyPort.Rate_115200, 8);
            port.openChannel(2, Constant.Constant.DeviceNum, Constant.Constant.Devicetype, Constant.Constant.Period, Constant.Constant.Freq, 10, 0x10);//配置通道
            Thread.Sleep(5000);
            port.openChannel(1, Constant.Constant.DeviceNum_share, Constant.Constant.Devicetype_share, Constant.Constant.Period_share, Constant.Constant.Freq_share, 30, 0x30);//共享通道
        }

        public void closeChannel() {
            port.closeChannel(1);
            port.closeChannel(2);
            port.close();
        }
    }
}
