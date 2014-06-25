using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.com.lk.Constant
{
    class Constant
    {
        public const byte NetworkNum = 0;
        public const ushort DeviceNum = 77;//77
        public const bool PairEnable = false;
        public const byte Devicetype = 127;
        public const byte Translatetype = 3;
        public const ushort Period = 8192;
        public const byte Freq = 77;
        public static string Connectstr = "provider=Microsoft.Jet.OleDb.4.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "DataBase\\" + "Lk.mdb;";
        public const ushort DeviceNum_share = 50;//50
        public const bool PairEnable_share = false;
        public const byte Devicetype_share = 126;
        public const byte Translatetype_share = 3;
        public const ushort Period_share = 512;//512
        public const byte Freq_share = 75;
        public static bool isCover = false;
        public const string mdbName = "Lk.mdb";
        public const string table_Lk = "LkTable";
        public const string table_Project = "LkProject";
        public const string table_user = "LkUser";
        public const string studentNum = "学号";
        public const string name = "姓名";
        public const string sex = "性别";
        public const string height = "身高";
        public const string height_score = "身高得分";
        public const string weight = "体重";
        public const string weight_score = "体重得分";
        public const string jump = "立定跳远";
        public const string jump_score = "立定跳远得分";
        public const string vc = "肺活量";
        public const string vc_score = "肺活量得分";
        public const string sitUp = "仰卧起坐";
        public const string sitUp_score = "仰卧起坐得分";
        public const string tiqianqu = "坐位体前屈";
        public const string tiqianqu_score = "坐位体前屈得分";
        public const string pushUp = "俯卧撑";
        public const string pushUp_score = "俯卧撑得分";
        public const string pullUp = "引体向上";
        public const string pullUp_score = "引体向上得分";
        public const string Shuttlerun = "折返跑";
        public const string Shuttlerun_score = "折返跑得分";
        public const string Run_fifty = "五十米跑";
        public const string clname = "班级";
        public const string grade = "年级";
        public const string cardNum = "卡号";
        public const string temp = "临时";
        public static string address;
        public const string username = "lingkang2014";
        public const string password = "2014";
        public static int select = 1; //用于区别是左侧是选择了全部还是临时（全部1，临时0）
      
    }
}
