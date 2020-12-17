<%@ Application Language="C#" %>
<%@ Import Namespace="System.Threading " %>
<%@ Import Namespace="System.Globalization " %>

<script runat="server">

    
    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码T
        SetDB();        
    }
  
    /// <summary>
    /// 创建数据库连接
    /// </summary>
    private bool SetDB()
    {
        ////DbAccessFactory.useOleDB = true;
        //if (CoreConfig.ConnectionInfoGroup.ContainsKey(""))
        //{
        //    return true;
        //}

        //ConnectionReader connReader = new ConnectionReader();
        //IConnectionInfo conInfo = connReader.Read();

        //try
        //{
        //    IDbAccess dbAccess = conInfo.GetDBAccess();
        //    dbAccess.Open();
        //    dbAccess.Close();
        //}
        //catch (Exception E)
        //{

        //    //this.Alert("连接数据库失败!\r\n" + E.Message);
        //    return false;
        //}
        //CoreConfig.ConnectionInfoGroup.Add("", conInfo);

        return true;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码  
        try
        {
            //Exception E = Server.GetLastError();
            Server.Transfer("~/Layout/Baselayout/FrmError.aspx");
        }
        catch(Exception ee)
        {
            
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
    void Application_BeginRequest(Object sender, EventArgs e)
    {   
        try
        {
            if (Request.Cookies["language"] != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Request.Cookies["language"].Value.ToString());
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Request.Cookies["language"].Value.ToString());
            }
        }
        catch (Exception)
        { }
    }  
       
</script>
