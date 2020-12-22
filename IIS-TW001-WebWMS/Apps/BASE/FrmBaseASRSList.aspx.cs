using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using DreamTek.ASRS.Business.SP.ProcedureModel;

public partial class Apps_BASE_FrmBaseASRSList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["code"] != null && Request.QueryString["caseType"] != null && Request.QueryString["positonCode"] != null)
            {
                string code = Request.QueryString["code"].ToString();
                string positonCode = Request.QueryString["positonCode"].ToString();
                //string caseType = Request.QueryString["caseType"].ToString();
                //BaseASRSQuery ba = new BaseASRSQuery();
                //string code = ba.GetCaseCode(ids, caseType);
                txtCTICKETCODE.Text = code;
                txtPositionCode.Text = positonCode;
                btnSearch_Click(null, null);
                InitPage();
            }
        }
    }

    public void InitPage()
    {
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('ASRS_LIST');return false;";
    }

    public bool CheckData()
    {
        return true;
    }


    protected DataTable grdNavigatorINBILL_D_GetExportToExcelSource()
    {
        //BaseASRSQuery listQuery = new BaseASRSQuery();
        //DataTable dtSource = listQuery.GetList_V(txtCTICKETCODE.Text, ddlStn.SelectedValue, ddlCaseType.SelectedValue, txtPositionCode.Text, false, -1, -1);
        //return dtSource;
        return null;
    }

    protected void grdINBILL_D_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
    }

    protected void grdINBILL_D_PageIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void grdINBILL_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        Bind("");
    }

    //Note by Qamar 2020-12-10
    protected string PostUrl(string url, string taskNo)
    {
        try
        {
            //string data = "taskNo=1&palletNo=" + palletNo + "&state=13";
            string data = "taskNo=" + taskNo;

            byte[] buffer = Encoding.ASCII.GetBytes(data);
            System.Net.HttpWebRequest WebReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

            WebReq.Timeout = 1000; //請求超過時間
            WebReq.Method = "POST";
            WebReq.ContentType = "application/x-www-form-urlencoded";
            WebReq.ContentLength = buffer.Length;

            using (System.IO.Stream PostData = WebReq.GetRequestStream())
            {
                PostData.Write(buffer, 0, buffer.Length);

                System.Net.HttpWebResponse WebResp = (System.Net.HttpWebResponse)WebReq.GetResponse();
                using (System.IO.Stream stream = WebResp.GetResponseStream())
                {
                    using (System.IO.StreamReader strReader = new System.IO.StreamReader(stream))
                    {
                        return strReader.ReadToEnd();
                    }
                }
                WebResp.Close();
            }
        }
        catch { }
        return String.Empty;
    }

    protected void LinkASRS_STATUS_Click(object sender, EventArgs e)
    {
        /* 強制完成 */

        string wmstskid = (sender as Button).CommandArgument;
        Proc_ASRS_Operate proc = new Proc_ASRS_Operate();
        proc.P_wmstskid = !string.IsNullOrEmpty(wmstskid)? Convert.ToInt32(wmstskid):0;
        proc.P_Opera = "7";
        proc.Execute();
        if (proc.ReturnValue == 0)
        {
            this.Alert(Resources.Lang.FrmBaseASRSList_Msg01); //强制完成成功!
        }
        else
        {
            this.Alert(Resources.Lang.FrmBaseASRSList_Msg02 + "：" + proc.ErrorMessage); //强制完成失败
        }
        btnSearch_Click(null, null);

        /*
        //Note by Qamar 2020-12-10
        //測試post到webapi
        string palletNo = "";
        IGenericRepository<CMD_MST> conn = new GenericRepository<CMD_MST>(db);
        var caseList = from p in conn.Get()
                       where p.WmsTskId == int_wmstskid
                       select p;
        Alert(PostUrl("http://localhost:2418/WMSWCS/TaskStatesUpdate", caseList.FirstOrDefault().PACKAGENO));
        */

        //Note by Qamar 2020-12-11
        //清除WCS命令
        PostUrl("http://192.168.1.200/WMSWCS/api/WMSWCS/MoveTaskForceClear", wmstskid.PadLeft(5, '0'));
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        /* 取消命令 */

        string wmstskid = (sender as Button).CommandArgument;
        Proc_ASRS_Operate proc = new Proc_ASRS_Operate();
        proc.P_wmstskid = !string.IsNullOrEmpty(wmstskid) ? Convert.ToInt32(wmstskid) : 0; ;
        proc.P_Opera = "0";
        proc.Execute();
        if (proc.ReturnValue == 0)
        {
            this.Alert(Resources.Lang.FrmBaseASRSList_Msg03); //取消命令成功!
        }
        else
        {
            this.Alert(Resources.Lang.FrmBaseASRSList_Msg04 + "：" + proc.ErrorMessage); //取消命令失败
        }
        btnSearch_Click(null, null);

        //Note by Qamar 2020-12-11
        //清除WCS命令
        PostUrl("http://192.168.1.200/WMSWCS/api/WMSWCS/MoveTaskForceClear", wmstskid.PadLeft(5, '0'));
    }

    public IQueryable<V_AsrsList> GetQueryList()
    {
        IGenericRepository<V_AsrsList> conn = new GenericRepository<V_AsrsList>(db);
        var caseList = from p in conn.Get()
                       orderby p.wmstskid ascending
                       where 1==1
                       select p;
        
        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCTICKETCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CTICKETCODE) && x.CTICKETCODE.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (ddlStn.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.stnno) && x.stnno.Contains(ddlStn.SelectedValue));
            }
            if (ddlCaseType.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.caseType) && x.caseType.Contains(ddlCaseType.SelectedValue));
            }
            if (txtPositionCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtPositionCode.Text.Trim()));
            }
        }
        return caseList;
    }

    public void Bind(string sortStr)
    {
        var caseList = GetQueryList();

        if (caseList != null && caseList.Count() > 0)
        {
            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        grdINBILL_D.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdINBILL_D.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }




   


}