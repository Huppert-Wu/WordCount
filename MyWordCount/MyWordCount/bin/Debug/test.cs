using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerDAL
{
    class Handle
    {
        SqlConnection MyConn = new SqlConnection(Connect.connstring);//链接数据库
        SqlDataAdapter SelectAdapter = new SqlDataAdapter();//定义一个数据适配器

        /// <summary>
        /// 数据添加
        /// </summary>
        /// <param name="id">添加的id</param>
        /// <param name="name">添加的用户名</param>
        /// <param name="passssword">添加的密码</param>
        public Handle(int id, string name, string passssword)
        {

            string MyInsert = "insert into ManagerTable(id, name,password)values(id,name,password)";
            SqlCommand MyCommand = new SqlCommand(MyInsert, MyConn);

            try//异常处理
            {
                MyConn.Open();
                MyCommand.ExecuteNonQuery();
                MyConn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }

        /// <summary>
        /// 按id查找信息
        /// </summary>
        /// <param name="id"></param>
        public Handle(int id)//查找
        {
            string MySelect = "SELECT id, name, password FROM ManagerTable";
            SqlCommand MyCommand = new SqlCommand(MySelect, MyConn);

            SelectAdapter.SelectCommand = MyCommand;//定义数据适配器的操作指令
            DataSet MyDataSet = new DataSet();//定义一个数据集

            try
            {
                MyConn.Open();//打开数据库连接
                SelectAdapter.SelectCommand.ExecuteNonQuery();//执行数据库查询指令
                MyConn.Close();//关闭数据库

                //SelectAdapter.Fill(MyDataSet);//填充数据集
                //DataGrid1.DataSource = MyDataSet;
                //DataGrid1.DataBind();//将数据表格用数据集中的数据填充


            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }



        ///// <summary>
        ///// 登录
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="password"></param>
        //public Handle(int id, string password)
        //{
        //    string MySelect = "SELECT id,password as keyword FROM ManagerTable where id =" + id;
        //    SqlCommand MyCommand = new SqlCommand(MySelect, MyConn);
        //    SqlDataReader DR = MyCommand.ExecuteReader();

        //    if (DR.Read())
        //    {
        //        string passtring = DR["keyword"].ToString ();
                
        //    }

        //}
    }
}
