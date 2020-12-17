using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_Base_TVShow_Warehouse : System.Web.UI.Page
{
    public static string str_divshow;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblRefreshtime.Text = "5000";//DreamTek.ASRS.Business.Base.CommFunction.GetConFig("100000"); //
        

            //Refresh();
        
    }
   
    //private void Refresh()
    //{
    //    System.Timers.Timer timer = new System.Timers.Timer();
    //        timer.Enabled = true;
    //        timer.Interval = 60000;//4秒执行间隔时间,单位为毫秒   
    //        timer.Start();
    //        timer.Elapsed += new System.Timers.ElapsedEventHandler(BindData);
    //}
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }
    private void BindData()
    {
        string strSQL = string.Format("Exec dbo.Proc_TVShow_WareHouse '{0}'", "1");
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        StringBuilder str_header = new StringBuilder();
        if (dt.Rows.Count > 0)
        {
            //lblXianZhi.Text = dt.Rows[0]["XZCount"].ToString();
            //lblShiYong.Text = dt.Rows[0]["SYZCount"].ToString();
            //lblBaoFei.Text = dt.Rows[0]["BFCount"].ToString();
            //lblDongJie.Text = dt.Rows[0]["DJCount"].ToString();
            str_header.Append(dt.Rows[0]["XZCount"].ToString());
            str_header.Append("||");
            str_header.Append(dt.Rows[0]["SYZCount"].ToString());
            str_header.Append("||");
            str_header.Append(dt.Rows[0]["BFCount"].ToString());
            str_header.Append("||");
            str_header.Append(dt.Rows[0]["DJCount"].ToString());
            str_header.Append("||");
            str_header.Append(dt.Rows[0]["TotalspaceRate"].ToString());
            str_header.Append("||");
            str_header.Append(dt.Rows[0]["TotalPosUseRate"].ToString());
            str_header.Append("||");
            lbl_str_divshow.Text = str_header.ToString();
            Div1.InnerHtml = aaaa(dt);
        }
        else
        {
            lblXianZhi.Text = "";
            lblShiYong.Text = "";
            lblBaoFei.Text = "";
            lblDongJie.Text = "";
        }
    }
    public string aaaa(DataTable dt)
    {
        StringBuilder str_ret1 = new StringBuilder();
        StringBuilder str_ret = new StringBuilder();
        //string strSQL = string.Format("Exec dbo.Proc_TVShow_WareHouse '{0}'", "");
        //DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            string str_cx = "", str_cz = "";
            string str_lineID = "";
            string str_cpositioncode = "";
            DataRow[] drcy;
            DataRow[] drcinv;

            var list_line = dt.AsEnumerable().Select(x => x.Field<string>("LineID")).Distinct().ToList();

            //DataRow dr = dt.Select("1=1").FirstOrDefault();
            //str_ret1.Append(dr["XZCount"].ToString());
            //str_ret1.Append("||");
            //str_ret1.Append(dr["SYZCount"].ToString());
            //str_ret1.Append("||");
            //str_ret1.Append(dr["BFCount"].ToString());
            //str_ret1.Append("||");
            //str_ret1.Append(dr["DJCount"].ToString());
            //str_ret1.Append("||");
            //str_ret1.Append(dr["TotalspaceRate"].ToString());
            //str_ret1.Append("||");
            //str_ret1.Append(dr["TotalPosUseRate"].ToString());
            //str_ret1.Append("||");

            for (int i = 0; i < list_line.Count(); i++)
            {
                if (str_lineID != list_line[i].ToString())
                {
                    str_lineID = list_line[i].ToString();
                    var list_cx = dt.Select("LineID = '" + str_lineID + "'", "order1 asc").Select(x => x.Field<string>("cx")).Distinct().ToList();
                    for (int ii = 0; ii < list_cx.Count; ii++)
                    {
                        if (str_cx != list_cx[ii].ToString())
                        {
                            str_cx = list_cx[ii].ToString();
                            var countdz = dt.Select("cx = '" + str_cx + "' and LineID = '" + str_lineID + "'").Select(x => x.Field<string>("cz")).Distinct().Count();
                            str_ret.Append(" </br><table border='1' style='width:95%'>");
                            str_ret.Append("<tr><td style='border:0;' rowspan=" + countdz + ">" + str_lineID + "_ " + str_cx + "</td>");
                            var list_dz = dt.Select("cx = '" + str_cx + "' and LineID = '" + str_lineID + "'", "cy ASC,cz DESC").Select(x => x.Field<string>("cz")).Distinct().ToList();

                            for (int h = 0; h < list_dz.Count; h++)
                            {
                                if (str_cz != list_dz[h].ToString())
                                {
                                    str_cz = list_dz[h].ToString();
                                    str_ret.Append("<td style='border:0;'>" + str_cz + "</td>");
                                    drcy = dt.Select("cx = '" + str_cx + "' and cz= '" + str_cz + "' and LineID = '" + str_lineID + "'");
                                    //首先获取cz=04的所有储位
                                    for (int k = 0; k < drcy.Count(); k++)
                                    {
                                        var cinvcode = drcy[k]["Cinvcode"].ToString();
                                        var cstatus = drcy[k]["cstatus"].ToString();
                                        StringBuilder tooltip = new StringBuilder();
                                        if (str_cpositioncode != drcy[k]["CpositionCode"].ToString())
                                        {
                                            str_cpositioncode = drcy[k]["CpositionCode"].ToString();
                                            tooltip.Append("储位编码：" + str_cpositioncode + "&#10;");
                                            if (!string.IsNullOrEmpty(cinvcode))
                                            {
                                                tooltip.Append("总库存：" + drcy[k]["PosTotalStock"].ToString() + "&#10;" + "空间使用率：" + drcy[k]["PosspaceRate"].ToString() + "%&#10;");
                                                drcinv = dt.Select("CpositionCode = '" + str_cpositioncode + "'");
                                                if (drcinv != null)
                                                {
                                                    foreach (var item in drcinv)
                                                    {
                                                        tooltip.Append("料号：" + item["Cinvcode"].ToString() + "&nbsp;数量：" + item["cinvqty"].ToString() + "&#10;");
                                                    }
                                                }
                                            }
                                        }
                                        if (string.IsNullOrEmpty(cinvcode) && cstatus == "0") //储位没有料号，但是状态是可用的
                                        {
                                            str_ret.Append("<td style='background:#717975;'><span title=" + tooltip + ">&nbsp</span></td>");
                                        }
                                        else if (!string.IsNullOrEmpty(cinvcode) && cstatus == "0")
                                        {
                                            str_ret.Append("<td style='background:#DE8028;'><span title=" + tooltip + ">&nbsp</span></td>");
                                        }
                                        else if (cstatus == "4") //不可用
                                        {
                                            str_ret.Append("<td style='background:#D42B40;'><span title=" + tooltip + ">&nbsp</span></td>");
                                        }
                                        else if (cstatus == "1")//冻结
                                        {
                                            str_ret.Append("<td style='background:#EBF5F4;'><span title=" + tooltip + ">&nbsp</span></td>");
                                        }
                                    }

                                    str_ret.Append("</tr>");
                                }
                            }

                        }

                    }
                }
            }
            str_ret.Append("</tr></br></table>");
            str_divshow = str_ret1.ToString();
        
           // str_ret1.Append(str_ret.ToString());
            //Div1.InnerHtml = str_ret.ToString(); 
        }
        return str_ret.ToString();    
    }
   // [WebMethod]
    //public static string GetData(string strCRANE)
    //{
    //    StringBuilder str_ret1 = new StringBuilder();
    //    StringBuilder str_ret = new StringBuilder();
    //    string strSQL = string.Format("Exec dbo.Proc_TVShow_WareHouse '{0}'", "");
    //    DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
    //    if (dt.Rows.Count > 0)
    //    {
    //        string str_cx = "", str_cz = "";
    //        string str_lineID = "";
    //        string str_cpositioncode = "";
    //        DataRow[] drcy;
    //        DataRow[] drcinv;

    //        var list_line = dt.AsEnumerable().Select(x => x.Field<string>("LineID")).Distinct().ToList();

    //        DataRow dr = dt.Select("1=1").FirstOrDefault();
    //        str_ret.Append(dr["XZCount"].ToString());
    //        str_ret.Append("||");
    //        str_ret.Append(dr["SYZCount"].ToString());
    //        str_ret.Append("||");
    //        str_ret.Append(dr["BFCount"].ToString());
    //        str_ret.Append("||");
    //        str_ret.Append(dr["DJCount"].ToString());
    //        str_ret.Append("||");
    //        str_ret.Append(dr["TotalspaceRate"].ToString());
    //        str_ret.Append("||");
    //        str_ret.Append(dr["TotalPosUseRate"].ToString());
    //        str_ret.Append("||");

    //        for (int i = 0; i < list_line.Count(); i++)
    //        {
    //            if (str_lineID != list_line[i].ToString())
    //            {
    //                str_lineID = list_line[i].ToString();
    //                var list_cx = dt.Select("LineID = '" + str_lineID + "'", "order1 asc").Select(x => x.Field<string>("cx")).Distinct().ToList();
    //                for (int ii = 0; ii < list_cx.Count; ii++)
    //                {
    //                    if (str_cx != list_cx[ii].ToString())
    //                    {
    //                        str_cx = list_cx[ii].ToString();
    //                        var countdz = dt.Select("cx = '" + str_cx + "' and LineID = '" + str_lineID + "'").Select(x => x.Field<string>("cz")).Distinct().Count();
    //                        str_ret.Append(" </br><table border='1'>");
    //                        str_ret.Append("<tr><td style='border:0;' rowspan=" + countdz + ">" + str_lineID + "_ " + str_cx + "</td>");
    //                        var list_dz = dt.Select("cx = '" + str_cx + "' and LineID = '" + str_lineID + "'", "cy ASC,cz DESC").Select(x => x.Field<string>("cz")).Distinct().ToList();

    //                        for (int h = 0; h < list_dz.Count; h++)
    //                        {
    //                            if (str_cz != list_dz[h].ToString())
    //                            {
    //                                str_cz = list_dz[h].ToString();
    //                                str_ret.Append("<td style='border:0;'>" + str_cz + "</td>");
    //                                drcy = dt.Select("cx = '" + str_cx + "' and cz= '" + str_cz + "' and LineID = '" + str_lineID + "'");
    //                                //首先获取cz=04的所有储位
    //                                for (int k = 0; k < drcy.Count(); k++)
    //                                {
    //                                    var cinvcode = drcy[k]["Cinvcode"].ToString();
    //                                    var cstatus = drcy[k]["cstatus"].ToString();
    //                                    StringBuilder tooltip = new StringBuilder();
    //                                    if (str_cpositioncode != drcy[k]["CpositionCode"].ToString())
    //                                    {
    //                                        str_cpositioncode = drcy[k]["CpositionCode"].ToString();
    //                                        tooltip.Append("储位编码：" + str_cpositioncode + "&#10;");
    //                                        if (!string.IsNullOrEmpty(cinvcode))
    //                                        {
    //                                            tooltip.Append("总库存：" + drcy[k]["PosTotalStock"].ToString() + "&#10;" + "空间使用率：" + drcy[k]["PosspaceRate"].ToString() + "&#10;");
    //                                            drcinv = dt.Select("CpositionCode = '" + str_cpositioncode + "'");
    //                                            if (drcinv != null)
    //                                            {
    //                                                foreach (var item in drcinv)
    //                                                {
    //                                                    tooltip.Append("料号：" + item["Cinvcode"].ToString() + "&nbsp;数量：" + item["cinvqty"].ToString() + "&#10;");
    //                                                }
    //                                            }
    //                                        }
    //                                    }
    //                                    if (string.IsNullOrEmpty(cinvcode) && cstatus == "0") //储位没有料号，但是状态是可用的
    //                                    {
    //                                        str_ret.Append("<td style='background:#717975;'><span title=" + tooltip + ">&nbsp</span></td>");
    //                                    }
    //                                    else if (!string.IsNullOrEmpty(cinvcode) && cstatus == "0")
    //                                    {
    //                                        str_ret.Append("<td style='background:#DE8028;'><span title=" + tooltip + ">&nbsp</span></td>");
    //                                    }
    //                                    else if (cstatus == "4") //不可用
    //                                    {
    //                                        str_ret.Append("<td style='background:#D42B40;'><span title=" + tooltip + ">&nbsp</span></td>");
    //                                    }
    //                                    else if (cstatus == "1")//冻结
    //                                    {
    //                                        str_ret.Append("<td style='background:#EBF5F4;'><span title=" + tooltip + ">&nbsp</span></td>");
    //                                    }
    //                                }

    //                                str_ret.Append("</tr>");
    //                            }
    //                        }

    //                    }

    //                }
    //            }
    //        }
    //        str_ret.Append("</tr></br></table>");
    //        str_divshow = str_ret.ToString();
    //       // str_ret1.Append(str_ret.ToString());
    //        //Div1.InnerHtml = str_ret.ToString(); 
    //    }
    //    return str_ret.ToString();
    //}
}