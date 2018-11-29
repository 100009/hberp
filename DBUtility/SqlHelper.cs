using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace LXMS.DBUtility
{
    public class SqlHelper
    {
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        #region 通用方法
        // 数据连接池
        //private SqlConnection con;
        /// <summary>
        /// 返回数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static SqlConnection getConn()
        {
            return new SqlConnection(connectionString);
        }
        #endregion
        #region 执行sql字符串
        /// <summary>
        /// 执行不带参数的SQL语句
        /// </summary>
        /// <param name="Sqlstr"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(String Sqlstr, SqlParameter[] param = null)
        {
            using (SqlConnection conn = getConn())
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
				if (param != null)
					cmd.Parameters.AddRange(param);

				cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
				conn.Close();
                return 1;
            }
        }
        /// <summary>
        /// 执行带参数的SQL语句
        /// </summary>
        /// <param name="Sqlstr">SQL语句</param>
        /// <param name="param">参数对象数组</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType cmdType, String Sqlstr, SqlParameter[] param = null)
        {
            using (SqlConnection conn = getConn())
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandType = cmdType;
				cmd.CommandText = Sqlstr;
				if (param != null)
					cmd.Parameters.AddRange(param);                
				
                cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
				conn.Close();
                return 1;
            }
        }

		/// <summary>
		/// 返回DataReader
		/// </summary>
		/// <param name="Sqlstr"></param>
		/// <returns></returns>
		public static SqlDataReader ExecuteReader(CommandType cmdType, String Sqlstr, SqlParameter[] param = null)
		{
			SqlConnection conn = getConn();
			if (conn.State != ConnectionState.Open) conn.Open();
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = conn;
			cmd.CommandType = cmdType;
			cmd.CommandText = Sqlstr;
			if (param != null)
				cmd.Parameters.AddRange(param);

			var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);//关闭关联的Connection
			cmd.Parameters.Clear();

			return rdr;
		}
		/// <summary>
		/// 返回DataReader
		/// </summary>
		/// <param name="Sqlstr"></param>
		/// <returns></returns>
		public static SqlDataReader ExecuteReader(String Sqlstr, SqlParameter[] param = null)
        {            
            SqlConnection conn = getConn();
            if (conn.State != ConnectionState.Open) conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
			cmd.CommandType = CommandType.Text;
            cmd.CommandText = Sqlstr;
			if (param != null)
				cmd.Parameters.AddRange(param);

			var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);//关闭关联的Connection
			cmd.Parameters.Clear();

			return rdr;
        }
		/// <summary>
		/// 返回DataReader
		/// </summary>
		/// <param name="Sqlstr"></param>
		/// <returns></returns>
		public static object ExecuteScalar(CommandType cmdType, String Sqlstr, SqlParameter[] param = null)
		{
			using (SqlConnection conn = getConn())
			{
				if (conn.State != ConnectionState.Open) conn.Open();
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandType = cmdType;
				cmd.CommandText = Sqlstr;
				if(param != null)
					cmd.Parameters.AddRange(param);

				object obj = cmd.ExecuteScalar();//关闭关联的Connection
				cmd.Parameters.Clear();

				return obj;
			}
		}
		/// <summary>
		/// 返回DataReader
		/// </summary>
		/// <param name="Sqlstr"></param>
		/// <returns></returns>
		public static object ExecuteScalar(String Sqlstr, SqlParameter[] param = null)
        {            
            using (SqlConnection conn = getConn())
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
				if (param != null)
					cmd.Parameters.AddRange(param);

				object obj = cmd.ExecuteScalar();//关闭关联的Connection
				cmd.Parameters.Clear();

				return obj;
            }
        }
        /// <summary>
        /// 执行SQL语句并返回数据表
        /// </summary>
        /// <param name="Sqlstr">SQL语句</param>
        /// <returns></returns>
        public static DataTable ExecuteDt(String Sqlstr)
        {
            using (SqlConnection conn = getConn())
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Sqlstr, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
								
                return dt;
            }
        }
        /// <summary>
        /// 执行SQL语句并返回DataSet
        /// </summary>
        /// <param name="Sqlstr">SQL语句</param>
        /// <returns></returns>
        public static DataSet ExecuteDs(String Sqlstr)
        {
            using (SqlConnection conn = getConn())
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Sqlstr, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="cmd">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        public static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

        #region 操作存储过程
        /// <summary>
        /// 运行存储过程(已重载)
        /// </summary>
        /// <param name="procName">存储过程的名字</param>
        /// <returns>存储过程的返回值</returns>
        public int RunProc(string procName, string outField)
        {
            using (SqlConnection conn = getConn())
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                SqlCommand cmd = CreateCommand(procName, null);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();                
                var rt = (int)cmd.Parameters[outField].Value;

				return rt;
            }
        }
        /// <summary>
        /// 运行存储过程(已重载)
        /// </summary>
        /// <param name="procName">存储过程的名字</param>
        /// <param name="prams">存储过程的输入参数列表</param>
        /// <returns>存储过程的返回值</returns>
        public int RunProc(string procName, SqlParameter[] prams, string outField)
        {
            using (SqlConnection conn = getConn())
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                SqlCommand cmd = CreateCommand(procName, prams);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
				var rt = (int)cmd.Parameters[outField].Value;
				cmd.Parameters.Clear();

				return rt;
			}
        }
        /// <summary>
        /// 运行存储过程(已重载)
        /// </summary>
        /// <param name="procName">存储过程的名字</param>
        /// <param name="dataReader">结果集</param>
        public void RunProc(string procName, out SqlDataReader dataReader)
        {
            using (SqlConnection conn = getConn())
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                SqlCommand cmd = CreateCommand(procName, null);
                cmd.Connection = conn;
                dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
        }
        /// <summary>
        /// 运行存储过程(已重载)
        /// </summary>
        /// <param name="procName">存储过程的名字</param>
        /// <param name="prams">存储过程的输入参数列表</param>
        /// <param name="dataReader">结果集</param>
        public void RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader)
        {
            using (SqlConnection conn = getConn())
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                SqlCommand cmd = CreateCommand(procName, prams);
                cmd.Connection = conn;
                dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
								
				cmd.Parameters.Clear();
			}
        }
        /// <summary>
        /// 创建Command对象用于访问存储过程
        /// </summary>
        /// <param name="procName">存储过程的名字</param>
        /// <param name="prams">存储过程的输入参数列表</param>
        /// <returns>Command对象</returns>
        private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
        {
            // 确定连接是打开的
            using (SqlConnection conn = getConn())
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                //command = new SqlCommand( sprocName, new SqlConnection( ConfigManager.DALConnectionString ) );
                SqlCommand cmd = new SqlCommand(procName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // 添加存储过程的输入参数列表
                if (prams != null)
                {
                    foreach (SqlParameter parameter in prams)
                        cmd.Parameters.Add(parameter);
                }
                // 返回Command对象
                return cmd;
            }
        }
        /// <summary>
        /// 创建输入参数
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Value">参数值</param>
        /// <returns>新参数对象</returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }
        /// <summary>
        /// 创建输出参数
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <returns>新参数对象</returns>
        public SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }
        /// <summary>
        /// 创建存储过程参数
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">参数的方向(输入/输出)</param>
        /// <param name="Value">参数值</param>
        /// <returns>新参数对象</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;
            if (Size > 0)
            {
                param = new SqlParameter(ParamName, DbType, Size);
            }
            else
            {
                param = new SqlParameter(ParamName, DbType);
            }
            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
            {
                param.Value = Value;
            }
            return param;
        }
        #endregion        
    }
}