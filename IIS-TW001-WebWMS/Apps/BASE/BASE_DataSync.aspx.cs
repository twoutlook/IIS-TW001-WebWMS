using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Business.SP.ProcedureModel;
using System.Configuration;

public partial class Apps_BASE_BASE_DataSync : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region 事件
    /// <summary>
    /// sql 同步oralce 权限同步事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAccess_Click(object sender, EventArgs e)
    {
        string return_value = string.Empty;
        Proc_SyncAccessFmOracle proc = new Proc_SyncAccessFmOracle();
        proc.Execute();
        return_value = proc.P_Return_Value;
        if (return_value == "0")
        {
            this.Alert(Resources.Lang.BASE_DataSync_Msg01); //权限同步成功！
        }
        else
        {
            this.Alert(Resources.Lang.BASE_DataSync_Msg02); //权限同步失败！
        }
    }
    /// <summary>
    /// 部门同步事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDepartment_Click(object sender, EventArgs e)
    {
        string return_value = string.Empty;
        Proc_SyncDepartmentFmOracle proc = new Proc_SyncDepartmentFmOracle();
        proc.Execute();
        return_value = proc.P_Return_Value;
        if (return_value == "0")
        {
            this.Alert(Resources.Lang.BASE_DataSync_Msg03);//部门同步成功！
        }
        else
        {
            this.Alert(Resources.Lang.BASE_DataSync_Msg04); //部门同步失败！
        }
    }
    /// <summary>
    /// 操作人员同步事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOperator_Click(object sender, EventArgs e)
    {
        string return_value = string.Empty;
        Proc_SyncOperatorFmOracle proc = new Proc_SyncOperatorFmOracle();
        proc.Execute();
        return_value = proc.P_Return_Value;
        if (return_value == "0")
        {
            this.Alert(Resources.Lang.BASE_DataSync_Msg05);//操作人员同步成功！
        }
        else
        {
            this.Alert(Resources.Lang.BASE_DataSync_Msg06);//操作人员同步失败！

        }
    }
    #endregion

    protected void btnRight_Click(object sender, EventArgs e)
    {
        //同步SQL版权限平台的【业务类型】的权限
        string ProjectNO = ConfigurationManager.AppSettings["ProjectNO"];
        string return_value = string.Empty;
        Proc_SyncAccessRight proc = new Proc_SyncAccessRight();
        proc.P_ProjectCode = ProjectNO;
        proc.Execute();
        return_value = proc.P_Return_Value;
        if (return_value == "0")
        {
            this.Alert(Resources.Lang.BASE_DataSync_Msg01); //权限同步成功！
        }
        else
        {
            this.Alert(Resources.Lang.BASE_DataSync_Msg02); //权限同步失败！
        }
    }
}