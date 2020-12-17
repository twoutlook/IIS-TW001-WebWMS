using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;


/// <summary>
/// DBHelp 的摘要说明
/// </summary>
public class DBHelp
{
    //private static DBContext context = new DBContext();
    private static string connStr = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
    //private static SqlConnection conn = null;
	public DBHelp()
	{
        //conn = new SqlConnection(connStr);
	}
    public string GetConn()
    {
        try
        {
            using (SqlConnection Conn = new SqlConnection(connStr))
            {
                Conn.Open();
            }
        }
        catch (System.Exception ex)
        {
            return ex.ToString();
        }
        return "ok";
    }


    // NOTE by Mark, 2020-12-17, 這裡有個一直都有的困擾
    // Exec dbo.Proc_TVShow_WareHouse ''
    // Msg 201, Level 16, State 4, Procedure dbo.Proc_TVShow_WareHouse, Line 2
   //  Procedure or function 'Proc_TVShow_WareHouse' expects parameter '@P_Line', which was not supplied.
    public static DataTable ExecuteToDataTable(string StrSql)
    {
        // QUICK FIX by Mark, 2020-12-17
        StrSql = " Exec dbo.Proc_TVShow_WareHouse '','1'";

        DataTable dt = new DataTable();
        using (SqlConnection Conn = new SqlConnection(connStr))
        {
            Conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(StrSql, Conn);
            sda.Fill(dt);
            dt.Dispose();
        }
        return dt;
    }

    public static DataTable ExecuteToDataTable1(string StrSql)
    {
        DataTable dt = new DataTable();
        using (SqlConnection Conn = new SqlConnection(connStr))
        {
            Conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(StrSql, Conn);
            sda.Fill(dt);
            dt.Dispose();
        }
        return dt;
    }

    //public static DataTable ExecuteToDataTable(string StrSql)
    //{
    //    SqlCommand SQL
    //    //SqlDataAdapter sda = new SqlDataAdapter(StrSql, Conn);
    //    DataTable dt = new DataTable();
    //    sda.Fill(dt);
    //    dt.Dispose();
    //    return dt;
    //}

    public static void ExecuteNonQuery(string StrSql)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            SqlCommand com = new SqlCommand(StrSql, conn);
            com.CommandTimeout = 300;
            com.ExecuteNonQuery();
        }
    }
    public static int ExecuteToInt(string StrSql)
    {
        int i;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            SqlCommand com = new SqlCommand(StrSql, conn);
            i = com.ExecuteNonQuery();
        }
        return i;
    }
    public static string ExecuteFunction(string FunName)
    {
        string result = string.Empty;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string StrSql = string.Format("select [dbo].[{0}]", FunName); ;
            SqlCommand com = new SqlCommand(StrSql, conn);
            result = com.ExecuteScalar().ToString();
        }
        return result;
    }
    public static string ExecuteScalar(string StrSql)
    {
        try
        {
            string result = string.Empty;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand com = new SqlCommand(StrSql, conn);
                result = com.ExecuteScalar().ToString();
            }
            return result;
        }
        catch (System.Exception e)
        {
            return null;
        
        }
    }
    public static string[] ExecuteProc(string Proc, List<string> SqlParameterList)
    {
       DataTable dt = new DataTable();
       SqlParameter[] sqlParms = new SqlParameter[SqlParameterList.Count];
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Proc, con);
                cmd.CommandTimeout = 300;
                cmd.CommandType = CommandType.StoredProcedure;            
                for (int i = 0; i < SqlParameterList.Count; i++)
                {
                    string[] SqlParameter = SqlParameterList[i].ToString().Split(':');
                    sqlParms[i] = new SqlParameter(SqlParameter[0], SqlParameter[1]);
                    if (i == SqlParameterList.Count - 1)//errormsg
                    {
                        sqlParms[i].SqlDbType = SqlDbType.VarChar;
                        sqlParms[i].Size = 5000;
                        sqlParms[i].Direction = ParameterDirection.Output;
                    }
                    if (i == SqlParameterList.Count - 2)//returncode
                    {
                        sqlParms[i].Direction = ParameterDirection.Output;
                    }
                    cmd.Parameters.Add(sqlParms[i]);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }         
        }
        catch (Exception e)
        {
          return new string[]{"1",e.ToString()};
        }
        return new string[] { sqlParms[SqlParameterList.Count - 2].Value.ToString(), sqlParms[SqlParameterList.Count - 1].Value.ToString() };
    }

    public static string ExecuteProcReturnValue(string Proc, List<string> SqlParameterList, string P_ReturnValue)
    {
        DataTable dt = new DataTable();
        SqlParameter[] sqlParms = new SqlParameter[SqlParameterList.Count];
        SqlParameter Output = new SqlParameter(P_ReturnValue,"");　　//定义输出参数  
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Proc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < SqlParameterList.Count; i++)
                {
                    string[] SqlParameter = SqlParameterList[i].ToString().Split(':');
                    sqlParms[i] = new SqlParameter(SqlParameter[0], SqlParameter[1]);
                    cmd.Parameters.Add(sqlParms[i]);
                }
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception e)
        {
            return e.ToString();
        }
        return Output.Value.ToString();
    }

    public static string ExecuteProcReturnMsg(string Proc, List<string> SqlParameterList, string P_ReturnValue)
    {
        DataTable dt = new DataTable();
        SqlParameter[] sqlParms = new SqlParameter[SqlParameterList.Count];
        SqlParameter Output = new SqlParameter(P_ReturnValue, "");　　//定义输出参数  
        Output.SqlDbType = SqlDbType.NVarChar;
        Output.Size = 1500;

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Proc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < SqlParameterList.Count; i++)
                {
                    string[] SqlParameter = SqlParameterList[i].ToString().Split(':');
                    sqlParms[i] = new SqlParameter(SqlParameter[0], SqlParameter[1]);
                    cmd.Parameters.Add(sqlParms[i]);
                }
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception e)
        {
            return e.ToString();
        }
        return Output.Value.ToString();
    }
    public static DataSet ExecuteToDataSet(string StrSql)
    {
        DataSet ds = new DataSet();
        using (SqlConnection Conn = new SqlConnection(connStr))
        {
            Conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(StrSql, Conn);
            sda.Fill(ds);
            ds.Dispose();
        }
        return ds;
    }



    #region 执行查询返回第一行，第一列---------------------------------




    public static object ExcuteScalarSQL(string strSQL)
    {
        return ExcuteScalarSQL(strSQL, null);
    }

    public static object ExcuteScalarSQL(string strSQL, SqlParameter[] paras)
    {
        return ExcuteScalarSQL(strSQL, paras, CommandType.Text);
    }
    public static object ExcuteScalarProc(string strSQL, SqlParameter[] paras)
    {
        return ExcuteScalarSQL(strSQL, paras, CommandType.StoredProcedure);
    }
    /// <summary>
    /// 执行SQL语句，返回第一行，第一列
    /// </summary>
    /// <param name="strSQL">要执行的SQL语句</param>
    /// <param name="paras">参数列表，没有参数填入null</param>
    /// <returns>返回影响行数</returns>
    public static object ExcuteScalarSQL(string strSQL, SqlParameter[] paras, CommandType cmdType)
    {
        object i;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.CommandType = cmdType;
            if (paras != null)
            {
                cmd.Parameters.AddRange(paras);
            }
            conn.Open();
            i = cmd.ExecuteScalar();
            conn.Close();
        }
        return i;

    }


    #endregion
}