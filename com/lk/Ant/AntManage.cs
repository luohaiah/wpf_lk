using System;
using System.Collections.Generic;
using System.Linq;
using ANT_Managed_Library;
using Main.com.lk.DbHelper;
using Main.com.lk.Entity;
using Main.com.lk.ui;
using System.Windows;
namespace Main.com.lk.Ant
{
    class AntManage
    {
        private ANT_Device device0;
        public ANT_Channel channel_general, channel_share;
        private bool is_has = false;
        private List<string> list_address = new List<string>();
        private int i = 0;
        private Dao dao;
        private int time = DateTime.Now.Second;
        private MainWindow window;
        private Dictionary<string, Device> dic_device = new Dictionary<string, Device>();
        public AntManage(MainWindow window)
        {
            this.window = window;
        }
        public bool initAnt()
        {
            try
            {
                dao = Dao.getInstance(window);
                device0 = new ANT_Device();
                device0.deviceResponse += new ANT_Device.dDeviceResponseHandler(device0_deviceResponse);
                device0.getChannel(2).channelResponse += new dChannelResponseHandler(d0channel_channelResponse);
                device0.getChannel(1).channelResponse += new dChannelResponseHandler(d0channel_channelResponse);
                setupAndOpen(device0, ANT_ReferenceLibrary.ChannelType.BASE_Master_Transmit_0x10, ANT_ReferenceLibrary.ChannelType.ADV_Shared_0x30);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        private void setupAndOpen(ANT_Device deviceToSetup, ANT_ReferenceLibrary.ChannelType channelType, ANT_ReferenceLibrary.ChannelType channelType2)
        {
            try
            {
                channel_general = deviceToSetup.getChannel(2);
                channel_general.assignChannel(channelType, 0, 500);
                channel_general.setChannelID(Constant.Constant.DeviceNum
        , false, Constant.Constant.Devicetype, Constant.Constant.Translatetype, 500);
                channel_general.setChannelPeriod(Constant.Constant.Period, 500);
                channel_general.setChannelFreq(Constant.Constant.Freq, 500);
                channel_general.openChannel(500);
                channel_share = deviceToSetup.getChannel(1);
                channel_share.assignChannel(channelType2, 0, 500);
                channel_share.setChannelID(Constant.Constant.DeviceNum_share
        , false, Constant.Constant.Devicetype_share, Constant.Constant.Translatetype_share, 500);
                channel_share.setChannelPeriod(Constant.Constant.Period_share, 500);
                channel_share.setChannelFreq(Constant.Constant.Freq_share, 500);
                channel_share.openChannel(500);
            }
            catch (Exception ex)
            {
                throw new Exception("Setup and Open Failed. " + ex.Message + Environment.NewLine);
            }

        }
        public void shutdown()
        {
            ANT_Device.shutdownDeviceInstance(ref device0);
        }
        private void decodeChannelFeedback_channel(ANT_Response response)
        {
            if (response.antChannel.Equals(channel_general.getChannelNum()))//配置通道
            {
                if (response.responseID == (byte)ANT_ReferenceLibrary.ANTMessageID.RESPONSE_EVENT_0x40)
                {

                }
                else //This is a receive event
                {
                    is_has = false;
                    string content = BitConverter.ToString(response.messageContents);
                    string[] data = content.Split('-');
                    string share_address = data[4] + "," + data[5];
                    foreach (var item in list_address)
                    {
                        if (share_address.Equals(item))
                        {
                            is_has = true;
                        }
                    }
                    if ("B2".Equals(data[3]))
                    {
                        send_cancel(response.messageContents[4], response.messageContents[5]);
                        if (!is_has)
                        {
                         //   Device device = new Device(channel_share, dao, window);
                         //  dic_device.Add(share_address, device);
                            list_address.Add(share_address);
                        }
                        else
                        {
                            is_has = false;
                        }
                    }
                }
            }
            if (response.antChannel.Equals(channel_share.getChannelNum()))//共享数据采集通道
            {

                switch ((ANT_ReferenceLibrary.ANTMessageID)response.responseID)
                {
                    case ANT_ReferenceLibrary.ANTMessageID.RESPONSE_EVENT_0x40:
                        {
                            switch (response.getChannelEventCode())
                            {
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_TX_0x03:
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
                                        channel_share.sendAcknowledgedData(new byte[] { 50, 50, (byte)0xB0, 0, 0, 0, 0, 0 });
                                    }
                                    break;
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_TX_COMPLETED_0x05:
                                    {
                                        //  Console.WriteLine("Transfer Completed");
                                        break;
                                    }
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_TX_FAILED_0x06:
                                    {
                                        // Console.WriteLine("Transfer Failed");
                                        break;
                                    }
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_CHANNEL_COLLISION_0x09:
                                    {
                                        // Console.WriteLine("Channel Collision");
                                        break;
                                    }

                            }
                            break;
                        }
                    case ANT_ReferenceLibrary.ANTMessageID.ACKNOWLEDGED_DATA_0x4F:
                        {
                            string content = BitConverter.ToString(response.messageContents);
                            string[] data = content.Split('-');
                            Console.WriteLine(content);
                            string address1 = data[1] + "," + data[2];
                            Device device1;
                            dic_device.TryGetValue(address1, out device1);
                            if (device1 != null)
                            {
                              //  device1.doData(data, address1, response);
                            }
                            break;
                        }
                    case ANT_ReferenceLibrary.ANTMessageID.BURST_DATA_0x50:
                        break;
                }
            }
        }

        private void d0channel_channelResponse(ANT_Response response)
        {
            decodeChannelFeedback_channel(response);
        }

        private String decodeDeviceFeedback(ANT_Response response)
        {
            string toDisplay = "Device: ";
            if (response.responseID == (byte)ANT_ReferenceLibrary.ANTMessageID.RESPONSE_EVENT_0x40)
            {
                //We cast the byte to its messageID string and add the channel number byte associated with the message
                toDisplay += (ANT_ReferenceLibrary.ANTMessageID)response.messageContents[1] + ", Ch:" + response.messageContents[0];
                //Check if the eventID shows an error, if it does, show the error message
                if ((ANT_ReferenceLibrary.ANTEventID)response.messageContents[2] != ANT_ReferenceLibrary.ANTEventID.RESPONSE_NO_ERROR_0x00)
                    toDisplay += Environment.NewLine + ((ANT_ReferenceLibrary.ANTEventID)response.messageContents[2]).ToString();
            }
            else   //If the message is not an event, we just show the messageID
                toDisplay += ((ANT_ReferenceLibrary.ANTMessageID)response.responseID).ToString();

            //Finally we display the raw byte contents of the response, converting it to hex
            toDisplay += Environment.NewLine + "::" + Convert.ToString(response.responseID, 16) + ", " + BitConverter.ToString(response.messageContents) + Environment.NewLine;
            return toDisplay;
        }
        private void device0_deviceResponse(ANT_Response response)
        {
            decodeDeviceFeedback(response);
        }
        private void send_cancel(byte address1, byte address2)
        {
            channel_general.sendAcknowledgedData(new byte[] { 0, 0, (byte)0xBF, address1, address2, 0, 0, 0 });
        }
    }
}
