using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADOX;
using System.Data.OleDb;
using System.Data;
using Main.com.lk.Constant;
using Main;
using System.Data.SqlClient;
using Main.com.lk.ui;
using System.Windows;

namespace Main.com.lk.DbHelper
{
    public class Dao
    {
        private static Dao dao;
        private Db_base db;
        private Window window;
        public static Dao getInstance(Window login1)
        {
            if (dao == null)
            {
                dao = new Dao(login1);
            }
            return dao;

        }
        public Dao(Window login1)
        {
            this.window = login1;
            db = Db_base.getInstance_Db();
        }

        public bool importExcel(String path)
        {
            try
            {
                string sql;
                OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                connectStringBuilder.DataSource = path;
                connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                connectStringBuilder.Add("Extended Properties", "Excel 8.0");
                using (OleDbConnection con = new OleDbConnection(connectStringBuilder.ConnectionString))
                {
                    DataSet ds = new DataSet();
                    sql = "Select * from [Sheet1$]";
                    OleDbCommand cmdLiming = new OleDbCommand(sql, con);
                    con.Open();
                    using (OleDbDataReader drLiming = cmdLiming.ExecuteReader())
                    {
                        ds.Load(drLiming, LoadOption.OverwriteChanges, new string[] { "Sheet1" });
                        DataTable dt = ds.Tables["Sheet1"];
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                //写入数据库数据
                                sql = "select " + Constant.Constant.studentNum + " from LkTable where " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                DataSet dsMsg = db.select(sql);
                                if (dsMsg.Tables[0].Rows.Count == 0)
                                {
                                    sql = "insert into LkTable(" + Constant.Constant.name + "," + Constant.Constant.studentNum + "," + Constant.Constant.sex + "," + Constant.Constant.clname + "," + Constant.Constant.grade + "," + Constant.Constant.temp + ")values('" + dt.Rows[i][Constant.Constant.name].ToString() + "','" + dt.Rows[i][Constant.Constant.studentNum] + "','" + dt.Rows[i][Constant.Constant.sex].ToString() + "','" + dt.Rows[i][Constant.Constant.clname].ToString() + "','" + dt.Rows[i][Constant.Constant.grade].ToString() + "',1)";
                                    db.SQLExecute(sql);
                                }
                                else
                                {
                                    sql = "UPDATE LkTable SET " + Constant.Constant.name + " = '" + dt.Rows[i][Constant.Constant.name].ToString() + "'," + Constant.Constant.sex + " = '" + dt.Rows[i][Constant.Constant.sex].ToString() + "'," + Constant.Constant.clname + " = '" + dt.Rows[i][Constant.Constant.clname].ToString() + "'," + Constant.Constant.grade + " = '" + dt.Rows[i][Constant.Constant.grade].ToString() + "'," + Constant.Constant.temp + " = 1 WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                    db.SQLExecute(sql);
                                }
                                //if (Constant.Constant.isCover)
                                //{
                                //    for (int y = 0; y < dt.Columns.Count; y++)
                                //    {

                                //        if (Constant.Constant.height.Equals(dt.Columns[y].ToString()))
                                //        {

                                //            sql = "UPDATE LkTable SET " + Constant.Constant.height + " = " + dt.Rows[i][y] + " WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);

                                //        }
                                //        else if (Constant.Constant.height_score.Equals(dt.Columns[y].ToString()))
                                //        {
                                //            sql = "UPDATE LkTable SET " + Constant.Constant.height_score + " = '" + dt.Rows[i][y] + "' WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);
                                //        }
                                //        else if (Constant.Constant.weight.Equals(dt.Columns[y].ToString()))
                                //        {

                                //            sql = "UPDATE LkTable SET " + Constant.Constant.weight + " = " + dt.Rows[i][y] + " WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);
                                //        }
                                //        else if (Constant.Constant.weight_score.Equals(dt.Columns[y].ToString()))
                                //        {
                                //            sql = "UPDATE LkTable SET " + Constant.Constant.weight_score + " = '" + dt.Rows[i][y] + "' WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);
                                //        }
                                //        else if (Constant.Constant.jump.Equals(dt.Columns[y].ToString()))
                                //        {

                                //            sql = "UPDATE LkTable SET " + Constant.Constant.jump + " = " + dt.Rows[i][y] + " WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);
                                //        }
                                //        else if (Constant.Constant.jump_score.Equals(dt.Columns[y].ToString()))
                                //        {
                                //            sql = "UPDATE LkTable SET " + Constant.Constant.jump_score + " = '" + dt.Rows[i][y] + "' WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);
                                //        }
                                //        else if (Constant.Constant.vc.Equals(dt.Columns[y].ToString()))
                                //        {

                                //            sql = "UPDATE LkTable SET " + Constant.Constant.vc + " = " + dt.Rows[i][y] + " WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);

                                //        }
                                //        else if (Constant.Constant.vc_score.Equals(dt.Columns[y].ToString()))
                                //        {
                                //            sql = "UPDATE LkTable SET " + Constant.Constant.vc_score + " = '" + dt.Rows[i][y] + "' WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);
                                //        }
                                //        else if (Constant.Constant.tiqianqu.Equals(dt.Columns[y].ToString()))
                                //        {


                                //            sql = "UPDATE LkTable SET " + Constant.Constant.tiqianqu + " = " + dt.Rows[i][y] + " WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);

                                //        }
                                //        else if (Constant.Constant.tiqianqu_score.Equals(dt.Columns[y].ToString()))
                                //        {
                                //            sql = "UPDATE LkTable SET " + Constant.Constant.tiqianqu_score + " = '" + dt.Rows[i][y] + "' WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);
                                //        }
                                //        else if (Constant.Constant.sitUp.Equals(dt.Columns[y].ToString()))
                                //        {


                                //            sql = "UPDATE LkTable SET " + Constant.Constant.sitUp + " = " + dt.Rows[i][y] + " WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);

                                //        }
                                //        else if (Constant.Constant.sitUp_score.Equals(dt.Columns[y].ToString()))
                                //        {
                                //            sql = "UPDATE LkTable SET " + Constant.Constant.sitUp_score + " = '" + dt.Rows[i][y] + "' WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);
                                //        }
                                //        else if (Constant.Constant.pushUp.Equals(dt.Columns[y].ToString()))
                                //        {


                                //            sql = "UPDATE LkTable SET " + Constant.Constant.pushUp + " = " + dt.Rows[i][y] + " WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);

                                //        }
                                //        else if (Constant.Constant.pushUp_score.Equals(dt.Columns[y].ToString()))
                                //        {
                                //            sql = "UPDATE LkTable SET " + Constant.Constant.pushUp_score + " = '" + dt.Rows[i][y] + "' WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);
                                //        }
                                //        else if (Constant.Constant.pullUp.Equals(dt.Columns[y].ToString()))
                                //        {


                                //            sql = "UPDATE LkTable SET " + Constant.Constant.pullUp + " = " + dt.Rows[i][y] + " WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);

                                //        }
                                //        else if (Constant.Constant.pullUp_score.Equals(dt.Columns[y].ToString()))
                                //        {
                                //            sql = "UPDATE LkTable SET " + Constant.Constant.pullUp_score + " = '" + dt.Rows[i][y] + "' WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);
                                //        }
                                //        else if (Constant.Constant.Shuttlerun.Equals(dt.Columns[y].ToString()))
                                //        {


                                //            sql = "UPDATE LkTable SET " + Constant.Constant.Shuttlerun + " = " + dt.Rows[i][y] + " WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);

                                //        }
                                //        else if (Constant.Constant.Shuttlerun_score.Equals(dt.Columns[y].ToString()))
                                //        {
                                //            sql = "UPDATE LkTable SET " + Constant.Constant.Shuttlerun_score + " = '" + dt.Rows[i][y] + "' WHERE " + Constant.Constant.studentNum + " = '" + dt.Rows[i][Constant.Constant.studentNum] + "'";
                                //            db.SQLExecute(sql);
                                //        }
                                //    }
                                //}
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataTable get_table(int index)
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            //string sql = "select top  60 * from LkTable where RecordId not in(select top " + 60 * (index - 1) + " RecordId from LkTable );"; 不要使用not in语句 效率很低
            //select top 每页数量 * from 表 where id >(select top 1 max(id) from (select top （页数-1）*每页数量 from 表 order by id,name)) 
            string sql = "select top 60 * from LkTable where RecordId > (select top 1 max(RecordId) from (select top " + 60 * (index - 1) + " RecordId from LkTable order by RecordId)) order by RecordId;";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
        public DataTable get_table_all()
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select * from LkTable;";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 获取数据总条数
        /// </summary>
        /// <returns></returns>
        public int get_totalCount()
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select * from LkTable";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt.Rows.Count;
        }

        public int get_totalCount_juti()
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select * from LkTable where " + Constant.Constant.temp + " = 1";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt.Rows.Count;
        }
        /// <summary>
        /// 获取年级数据总条数
        /// </summary>
        /// <returns></returns>
        public int get_totalCount_grade(string grade)
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select * from LkTable where " + Constant.Constant.grade + "='" + grade + "'";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt.Rows.Count;
        }
        /// <summary>
        /// 获取班级数据总条数
        /// </summary>
        /// <returns></returns>
        public int get_totalCount_class(string grade, string classname)
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select * from LkTable where " + Constant.Constant.grade + "='" + grade + "' and " + Constant.Constant.clname + "='" + classname + "'";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt.Rows.Count;
        }

        public int get_totalCount_sex(string grade, string classname, string sex)
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select * from LkTable where " + Constant.Constant.grade + "='" + grade + "' and " + Constant.Constant.clname + "='" + classname + "' and " + Constant.Constant.sex + "='" + sex + "'";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt.Rows.Count;
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public DataTable get_table_first()
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select top 60 * from LkTable";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        public DataTable get_table_grade(int index, string grade)
        {
            int i = DateTime.Now.Second;
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            //  string sql = "select top  60 * from LkTable where " + Constant.Constant.grade + "='" + grade + "' and RecordId not in(select top " + (60 * (index - 1)) + " RecordId from LkTable  where " + Constant.Constant.grade + "='" + grade + "');";
            string sql = "select top 60 * from LkTable where " + Constant.Constant.grade + "='" + grade + "' and RecordId >(select top 1 max(RecordId) from (select top " + 60 * (index - 1) + " RecordId from LkTable where " + Constant.Constant.grade + "='" + grade + "' order by RecordId)) order by RecordId;";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            Console.Out.WriteLine(DateTime.Now.Second - i);
            return dt;
        }
        public DataTable get_table_class(int index, string grade, string classname)
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select top  60 * from LkTable where " + Constant.Constant.grade + "='" + grade + "' and " + Constant.Constant.clname + "='" + classname + "' and RecordId >(select top 1 max(RecordId) from (select top " + 60 * (index - 1) + " RecordId from LkTable where " + Constant.Constant.grade + "='" + grade + "' and " + Constant.Constant.clname + "='" + classname + "' order by RecordId)) order by RecordId;";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        public DataTable get_table_grade_first(string grade)
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select top 60 * from LkTable where " + Constant.Constant.grade + "='" + grade + "'";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        public DataTable get_table_class_first(string grade, string classname)
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select top 60 * from LkTable where " + Constant.Constant.grade + "='" + grade + "' and " + Constant.Constant.clname + "='" + classname + "'";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
        public DataTable get_table_sex_first(string grade, string classname, string sex)
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select top 60 * from LkTable where " + Constant.Constant.grade + "='" + grade + "' and " + Constant.Constant.clname + "='" + classname + "' and " + Constant.Constant.sex + "='" + sex + "'";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
        public DataTable get_table_sex(int index, string grade, string classname, string sex)
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select top  60 * from LkTable where " + Constant.Constant.grade + "='" + grade + "' and " + Constant.Constant.clname + "='" + classname + "' and " + Constant.Constant.sex + "='" + sex + "' and RecordId >(select top 1 max(RecordId) from (select top " + 60 * (index - 1) + " RecordId from LkTable  where " + Constant.Constant.grade + "='" + grade + "' and " + Constant.Constant.clname + "='" + classname + "' and " + Constant.Constant.sex + "='" + sex + "' order by RecordId)) order by RecordId;";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 临时数据首页
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="classname"></param>
        /// <param name="sex"></param>
        /// <returns></returns>
        public DataTable get_table_temp_first()
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select * from LkTable where " + Constant.Constant.temp + " = 0";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        public DataTable get_table_juti_first()
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            string sql = "select top 60 * from LkTable where " + Constant.Constant.temp + " = 1";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
        public DataTable get_table_juti(int index)
        {
            OleDbConnection con = new OleDbConnection(Constant.Constant.Connectstr);
            //string sql = "select top  60 * from LkTable where RecordId not in(select top " + 60 * (index - 1) + " RecordId from LkTable );"; 不要使用not in语句 效率很低
            //select top 每页数量 * from 表 where id >(select top 1 max(id) from (select top （页数-1）*每页数量 from 表 order by id,name)) 
            string sql = "select top 60 * from LkTable where " + Constant.Constant.temp + " = 1 and RecordId > (select top 1 max(RecordId) from (select top " + 60 * (index - 1) + " RecordId from LkTable order by RecordId)) order by RecordId;";
            OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
        public bool export_Excel(string filename, int type, NodeNew node)
        {
            string sql;
            Db_base db = null;
            string pjNames = "";
            try
            {
                db = Db_base.getInstance_Db();
                int index = filename.LastIndexOf("//");//获取最后一个/的索引  
                filename = filename.Substring(index + 1);//获取excel名称(新建表的路径相对于SaveFileDialog的路径)  
                sql = "select 项目类型 from LkProject  where 状态 = '1'";
                DataSet ds = db.select(sql);
                for (int i = 5; i < ds.Tables[0].Rows.Count; i++)
                {
                    pjNames = pjNames + "," + ds.Tables[0].Rows[i][0] as string + "," + ds.Tables[0].Rows[i][0] as string + window.FindResource("score") as string;
                }
                switch (type)
                {
                    case 0:
                        sql = "select " + Constant.Constant.name + "," + Constant.Constant.studentNum + "," + Constant.Constant.cardNum + "," + Constant.Constant.sex + "," + Constant.Constant.grade + "," + Constant.Constant.clname + " " + pjNames + " into   [Excel 8.0;database=" + filename + "].[Sheet1] from LkTable";
                        break;
                    case 1://年级
                        sql = "select " + Constant.Constant.name + "," + Constant.Constant.studentNum + "," + Constant.Constant.cardNum + "," + Constant.Constant.sex + "," + Constant.Constant.grade + "," + Constant.Constant.clname + " " + pjNames + " into   [Excel 8.0;database=" + filename + "].[Sheet1] from LkTable where " + Constant.Constant.grade + " = '" + node.Name + "'";
                        break;
                    case 2://班级
                        sql = "select " + Constant.Constant.name + "," + Constant.Constant.studentNum + "," + Constant.Constant.cardNum + "," + Constant.Constant.sex + "," + Constant.Constant.grade + "," + Constant.Constant.clname + " " + pjNames + " into   [Excel 8.0;database=" + filename + "].[Sheet1] from LkTable where " + Constant.Constant.grade + "='" + node.grade + "' and " + Constant.Constant.clname + " ='" + node.Name + "'";
                        break;
                    case 3://性别
                        sql = "select " + Constant.Constant.name + "," + Constant.Constant.studentNum + "," + Constant.Constant.cardNum + "," + Constant.Constant.sex + "," + Constant.Constant.grade + "," + Constant.Constant.clname + " " + pjNames + " into   [Excel 8.0;database=" + filename + "].[Sheet1] from LkTable where " + Constant.Constant.grade + "='" + node.grade + "' and " + Constant.Constant.clname + " ='" + node.classname + "' and " + Constant.Constant.sex + " ='" + node.Name + "'";
                        break;
                    case 4://临时
                        sql = "select " + Constant.Constant.studentNum + "," + pjNames + " into   [Excel 8.0;database=" + filename + "].[Sheet1] from LkTable where " + Constant.Constant.temp + " = 0";
                        break;
                    case 5://具体
                        sql = "select " + Constant.Constant.name + "," + Constant.Constant.studentNum + "," + Constant.Constant.cardNum + "," + Constant.Constant.sex + "," + Constant.Constant.grade + "," + Constant.Constant.clname + " " + pjNames + " into   [Excel 8.0;database=" + filename + "].[Sheet1] from LkTable where " + Constant.Constant.temp + " = 1";
                        break;
                    default:
                        break;
                }
                db.SQLExecute(sql);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //更新项目选中状态
        public void update_pjStatus(string status, string pjType)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select 项目类型 from LkProject  where 项目类型 = '" + pjType + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkProject(项目类型,状态)values('" + pjType + "','" + status + "')";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkProject SET 状态 = '" + status + "' WHERE 项目类型 = '" + pjType + "'";
                db.SQLExecute(sql);
            }
        }
        /// <summary>
        /// 未选中的项目
        /// </summary>
        /// <returns></returns>
        public DataSet select_noChoosePj()
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select 项目类型 from LkProject  where 状态 = '0'";
            DataSet dsMsg = db.select(sql);
            return dsMsg;
        }
        public DataTable select_Class(string grade)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select DISTINCT " + Constant.Constant.clname + " from LkTable where " + Constant.Constant.grade + "='" + grade + "'";
            DataTable dt = db.select2(sql);
            return dt;
        }
        public DataTable select_Grade()
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select DISTINCT " + Constant.Constant.grade + " from LkTable ";
            DataTable dt = db.select2(sql);
            return dt;
        }
        public DataTable select_Sex(string grade, string classname)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select DISTINCT " + Constant.Constant.sex + " from LkTable where " + Constant.Constant.grade + "='" + grade + "' and  " + Constant.Constant.clname + "='" + classname + "'";
            DataTable dt = db.select2(sql);
            return dt;
        }

        /// <summary>
        /// 选中的项目
        /// </summary>
        /// <returns></returns>
        public DataSet select_yesChoosePj()
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select 项目类型 from LkProject  where 状态 = '1'";
            DataSet dsMsg = db.select(sql);
            return dsMsg;
        }
        //立定跳远
        public void update_jump(string studentNum, Int32 result)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select " + Constant.Constant.studentNum + " from LkTable  where " + Constant.Constant.studentNum + " = '" + studentNum + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkTable(" + Constant.Constant.studentNum + "," + Constant.Constant.jump + ")values('" + studentNum + "'," + result + ")";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkTable SET " + Constant.Constant.jump + " = " + result + " WHERE " + Constant.Constant.studentNum + " = '" + studentNum + "'";
                db.SQLExecute(sql);
            }
        }
        //肺活量
        public void update_vc(string studentNum, Int32 result)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select " + Constant.Constant.studentNum + " from LkTable  where " + Constant.Constant.studentNum + " = '" + studentNum + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkTable(" + Constant.Constant.studentNum + "," + Constant.Constant.vc + ")values('" + studentNum + "'," + result + ")";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkTable SET " + Constant.Constant.vc + " = " + result + " WHERE " + Constant.Constant.studentNum + " = '" + studentNum + "'";
                db.SQLExecute(sql);
            }
        }
        //坐位体前屈
        public void update_proneness(string studentNum, double result)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select " + Constant.Constant.studentNum + " from LkTable  where " + Constant.Constant.studentNum + " = '" + studentNum + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkTable(" + Constant.Constant.studentNum + "," + Constant.Constant.tiqianqu + ")values('" + studentNum + "'," + result + ")";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkTable SET " + Constant.Constant.tiqianqu + " = " + result + " WHERE " + Constant.Constant.studentNum + " = '" + studentNum + "'";
                db.SQLExecute(sql);
            }
        }
        //俯卧撑
        public void update_pushUp(string studentNum, Int32 result)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select " + Constant.Constant.studentNum + " from LkTable  where " + Constant.Constant.studentNum + " = '" + studentNum + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkTable(" + Constant.Constant.studentNum + "," + Constant.Constant.pushUp + ")values('" + studentNum + "'," + result + ")";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkTable SET " + Constant.Constant.pushUp + " = " + result + " WHERE " + Constant.Constant.studentNum + " = '" + studentNum + "'";
                db.SQLExecute(sql);
            }
        }
        //仰卧起坐
        public void update_sitUp(string studentNum, Int32 result)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select " + Constant.Constant.studentNum + " from LkTable  where " + Constant.Constant.studentNum + " = '" + studentNum + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkTable(" + Constant.Constant.studentNum + "," + Constant.Constant.sitUp + ")values('" + studentNum + "'," + result + ")";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkTable SET " + Constant.Constant.sitUp + " = " + result + " WHERE " + Constant.Constant.studentNum + " = '" + studentNum + "'";
                db.SQLExecute(sql);
            }
        }
        //引体向上
        public void update_pullUp(string studentNum, Int32 result)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select " + Constant.Constant.studentNum + " from LkTable  where " + Constant.Constant.studentNum + " = '" + studentNum + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkTable(" + Constant.Constant.studentNum + "," + Constant.Constant.pullUp + ")values('" + studentNum + "'," + result + ")";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkTable SET " + Constant.Constant.pullUp + " = " + result + " WHERE " + Constant.Constant.studentNum + " = '" + studentNum + "'";
                db.SQLExecute(sql);
            }
        }
        //折返跑
        public void update_shuttleRun(string studentNum, Int32 result)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select " + Constant.Constant.studentNum + " from LkTable  where " + Constant.Constant.studentNum + " = '" + studentNum + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkTable(" + Constant.Constant.studentNum + "," + Constant.Constant.Shuttlerun + ")values('" + studentNum + "'," + result + ")";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkTable SET " + Constant.Constant.Shuttlerun + " = " + result + " WHERE " + Constant.Constant.studentNum + " = '" + studentNum + "'";
                db.SQLExecute(sql);
            }
        }
        //身高
        public void update_height(string studentNum, Int32 result)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select " + Constant.Constant.studentNum + " from LkTable  where " + Constant.Constant.studentNum + " = '" + studentNum + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkTable(" + Constant.Constant.studentNum + "," + Constant.Constant.height + ")values('" + studentNum + "'," + result + ")";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkTable SET " + Constant.Constant.height + " = " + result + " WHERE " + Constant.Constant.studentNum + " = '" + studentNum + "'";
                db.SQLExecute(sql);
            }
        }
        //体重
        public void update_weight(string studentNum, Int32 result)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select " + Constant.Constant.studentNum + " from LkTable  where " + Constant.Constant.studentNum + " = '" + studentNum + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkTable(" + Constant.Constant.studentNum + "," + Constant.Constant.weight + ")values('" + studentNum + "'," + result + ")";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkTable SET " + Constant.Constant.weight + " = " + result + " WHERE " + Constant.Constant.studentNum + " = '" + studentNum + "'";
                db.SQLExecute(sql);
            }
        }
        //身高体重
        public void update_height_weight(string studentNum, double height, double weight)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select " + Constant.Constant.studentNum + " from LkTable  where " + Constant.Constant.studentNum + " = '" + studentNum + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkTable(" + Constant.Constant.studentNum + "," + Constant.Constant.height + "," + Constant.Constant.weight + ")values('" + studentNum + "'," + height + "," + weight + ")";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkTable SET " + Constant.Constant.height + " = " + height + "," + Constant.Constant.weight + " = " + weight + "  WHERE " + Constant.Constant.studentNum + " = '" + studentNum + "'";
                db.SQLExecute(sql);
            }
        }
        //50米跑
        public void update_run_fifity(string studentNum, double result)
        {
            string sql;
            Db_base db = Db_base.getInstance_Db();
            sql = "select " + Constant.Constant.studentNum + " from LkTable  where " + Constant.Constant.studentNum + " = '" + studentNum + "'";
            DataSet dsMsg = db.select(sql);
            if (dsMsg.Tables[0].Rows.Count == 0)
            {
                sql = "insert into LkTable(" + Constant.Constant.studentNum + "," + Constant.Constant.Run_fifty + ")values('" + studentNum + "'," + result + ")";
                db.SQLExecute(sql);
            }
            else
            {
                sql = "UPDATE LkTable SET " + Constant.Constant.Run_fifty + " = " + result + " WHERE " + Constant.Constant.studentNum + " = '" + studentNum + "'";
                db.SQLExecute(sql);
            }
        }
        /// <summary>
        /// 删除表数据
        /// </summary>
        public void delete_data(int type, NodeNew node)
        {
            string sql = null;
            Db_base db = null;
            db = Db_base.getInstance_Db();
            switch (type)
            {
                case 0:
                    sql = "DELETE * FROM LkTable";
                    break;
                case 1://年级
                    sql = "DELETE * FROM LkTable where " + Constant.Constant.grade + " = '" + node.Name + "'";
                    break;
                case 2://班级
                    sql = "DELETE * FROM LkTable where " + Constant.Constant.grade + "='" + node.grade + "' and " + Constant.Constant.clname + " ='" + node.Name + "'";
                    break;
                case 3://性别
                    sql = "DELETE * FROM LkTable where " + Constant.Constant.grade + "='" + node.grade + "' and " + Constant.Constant.clname + " ='" + node.classname + "' and " + Constant.Constant.sex + " ='" + node.Name + "'";
                    break;
                case 4:
                    sql = "DELETE * FROM LkTable where " + Constant.Constant.clname + " =''";
                    break;
                case 5:
                    sql = "DELETE * FROM LkTable where " + Constant.Constant.clname + " <>''";
                    break;
                default:
                    break;
            }
            db.SQLExecute(sql);
        }
        /// <summary>
        /// 返回值:0代表没有此用户,1代表密码错误，2代表用户名密码正确
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int login(string username, string password)
        {
            string sql = "select 密码 from LkUser where 用户名='" + username + "'";
            DataTable dt = db.select2(sql);
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else if (!dt.Rows[0]["密码"].Equals(password))
            {
                return 1;
            }
            else
            {
                return 2;
            }

        }
        public void openDb()
        {
            db.openDb();
        }
        public void closeDb()
        {
            db.closeDb();
        }
    }
}
