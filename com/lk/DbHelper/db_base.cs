using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using ADOX;
using System.IO;
using Main.com.lk.Constant;
namespace Main.com.lk.DbHelper
{
    class Db_base
    {
        private static Db_base db;
        private OleDbCommand ole_command = null;
        private OleDbConnection con;
        private ADOX.ColumnClass col;
        public static Db_base getInstance_Db()
        {
            if (db == null)
            {
                db = new Db_base();
            }
            return db;
        }
        Db_base()
        {
            InitDB();
        }
        private void InitDB()
        {
            create_mdb_table();

        }


        public void create_mdb_table()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "DataBase\\" + Constant.Constant.mdbName))
            {
                con = new OleDbConnection(Constant.Constant.Connectstr);
                ole_command = new OleDbCommand();
                ole_command.Connection = con;

                return;
            }
            else
            {

                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "DataBase\\"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "DataBase\\");
                }
                ADOX.CatalogClass cat = new CatalogClass();
                cat.Create(Constant.Constant.Connectstr);
                ADODB.Connection cn = new ADODB.Connection();
                cn.Open(Constant.Constant.Connectstr, null, null, -1);
                cat.ActiveConnection = cn;
                ADOX.TableClass table = new ADOX.TableClass();
                table.Name = Constant.Constant.table_Lk;
                ADOX.TableClass table2 = new ADOX.TableClass();
                table2.Name = Constant.Constant.table_Project;
                ADOX.TableClass table3 = new ADOX.TableClass();
                table3.Name = Constant.Constant.table_user;
                setPrimaryKey(cat, table);
                for (int i = 0; i < 25; i++)
                {
                    //增加一个文本字段   
                    col = new ADOX.ColumnClass();
                    col.ParentCatalog = cat;
                    switch (i)
                    {
                        case 0:
                            col.Name = Constant.Constant.studentNum;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            //  col.Type = DataTypeEnum.adInteger;//整数型这样写
                            //  table.Columns.Append(col);
                            table.Columns.Append(col, DataTypeEnum.adVarChar, 500); //字符型这样写
                            break;
                        case 1:
                            col.Name = Constant.Constant.name;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            table.Columns.Append(col, DataTypeEnum.adVarChar, 500); //字符型这样写
                            break;
                        case 2:
                            col.Name = Constant.Constant.sex;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            table.Columns.Append(col, DataTypeEnum.adVarChar, 500);
                            break;
                        case 3:
                            col.Name = Constant.Constant.height;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adDouble;
                            table.Columns.Append(col);
                            break;
                        case 4:
                            col.Name = Constant.Constant.height_score;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 5:
                            col.Name = Constant.Constant.weight;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adDouble;
                            table.Columns.Append(col);
                            break;
                        case 6:
                            col.Name = Constant.Constant.weight_score;//列的名称
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 7:
                            col.Name = Constant.Constant.jump;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 8:
                            col.Name = Constant.Constant.jump_score;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 9:
                            col.Name = Constant.Constant.vc;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 10:
                            col.Name = Constant.Constant.vc_score;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 11:
                            col.Name = Constant.Constant.tiqianqu;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adDouble;
                            table.Columns.Append(col);
                            break;
                        case 12:
                            col.Name = Constant.Constant.tiqianqu_score;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 13:
                            col.Name = Constant.Constant.sitUp;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 14:
                            col.Name = Constant.Constant.sitUp_score;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 15:
                            col.Name = Constant.Constant.pushUp;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 16:
                            col.Name = Constant.Constant.pushUp_score;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 17:
                            col.Name = Constant.Constant.pullUp;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 18:
                            col.Name = Constant.Constant.pullUp_score;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 19:
                            col.Name = Constant.Constant.Shuttlerun;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 20:
                            col.Name = Constant.Constant.Shuttlerun_score;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            table.Columns.Append(col);
                            break;
                        case 21:
                            col.Name = Constant.Constant.clname;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            table.Columns.Append(col, DataTypeEnum.adVarChar, 500);
                            break;
                        case 22:
                            col.Name = Constant.Constant.grade;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            table.Columns.Append(col, DataTypeEnum.adVarChar, 500);
                            break;
                        case 23:
                            col.Name = Constant.Constant.cardNum;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            table.Columns.Append(col, DataTypeEnum.adVarChar, 5);
                            break;
                        case 24:
                            col.Name = Constant.Constant.temp;//列的名称 
                            col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                            col.Type = DataTypeEnum.adInteger;
                            col.Properties["Default"].Value = 0;//设置字段默认值
                            table.Columns.Append(col);

                            break;
                        default:
                            break;
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    ADOX.ColumnClass col2 = new ADOX.ColumnClass();
                    col2.ParentCatalog = cat;
                    switch (i)
                    {
                        case 0:
                            col2.Name = "项目类型";//列的名称 
                            col2.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                            table2.Columns.Append(col2, DataTypeEnum.adVarChar, 500);
                            break;
                        case 1:
                            col2.Name = "状态";//列的名称 
                            col2.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                            table2.Columns.Append(col2, DataTypeEnum.adVarChar, 500);
                            break;
                        default:
                            break;
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    ADOX.ColumnClass col3 = new ADOX.ColumnClass();
                    col3.ParentCatalog = cat;
                    switch (i)
                    {
                        case 0:
                            col3.Name = "用户名";//列的名称 
                            col3.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                            table3.Columns.Append(col3, DataTypeEnum.adVarChar, 500);
                            break;
                        case 1:
                            col3.Name = "密码";//列的名称 
                            col3.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                            table3.Columns.Append(col3, DataTypeEnum.adVarChar, 500);
                            break;
                        default:
                            break;
                    }
                }
                cat.Tables.Append(table);
                cat.Tables.Append(table2);
                cat.Tables.Append(table3);
                table = null;
                table2 = null;
                table3 = null;
                cat = null;
                cn.Close();
                con = new OleDbConnection(Constant.Constant.Connectstr);
                ole_command = new OleDbCommand();
                ole_command.Connection = con;
                init_pjStatus();
                init_user();
                create_index();
            }
        }
        /// <summary>
        /// 创建索引
        /// </summary>
        private void create_index()
        {
            openDb();
            string sql = "create Index myIndex on LkTable(RecordId) WITH PRIMARY";
            SQLExecute(sql);
            closeDb();
        }


        /// <summary>
        /// 数据操作通用类
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool SQLExecute(string sql)
        {
            try
            {
                ole_command.CommandText = sql;
                ole_command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public void closeDb()
        {
            ole_command.Connection.Close();
            con.Close();
        }
        public void openDb()
        {
            con.Open();
        }

        public DataSet select(string sql)
        {
            DataSet dsMsg = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con);
            da.Fill(dsMsg);
            return dsMsg;
        }
        public DataTable select2(string sql)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }
        private void init_pjStatus()
        {
            string sql;
            openDb();
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.studentNum + "','1')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.name + "','1')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.sex + "','1')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.grade + "','1')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.clname + "','1')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.height + "','0')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.weight + "','0')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.jump + "','0')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.vc + "','0')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.tiqianqu + "','0')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.sitUp + "','0')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.pushUp + "','0')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.pullUp + "','0')";
            SQLExecute(sql);
            sql = "insert into LkProject(项目类型,状态)values('" + Constant.Constant.Shuttlerun + "','0')";
            SQLExecute(sql);
            closeDb();
        }
        private void init_user()
        {
            string sql;
            openDb();
            sql = "insert into LkUser(用户名,密码)values('" + Constant.Constant.username + "','" + Constant.Constant.password + "')";
            SQLExecute(sql);
            closeDb();
        }
        /// <summary>
        /// 设置表的主键
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="table"></param>
        private void setPrimaryKey(ADOX.CatalogClass cat, ADOX.TableClass table)
        {
            col = new ADOX.ColumnClass();
            col.ParentCatalog = cat;
            col.Name = "RecordId";
            col.Type = DataTypeEnum.adInteger;
            col.DefinedSize = 9;
            col.Properties["AutoIncrement"].Value = true;
            table.Columns.Append(col);
            table.Keys.Append("PrimaryKey", KeyTypeEnum.adKeyPrimary, col, null, null);
        }
    }
}
