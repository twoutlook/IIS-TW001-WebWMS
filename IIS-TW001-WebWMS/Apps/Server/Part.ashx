<%@ WebHandler Language="C#" Class="Part" %>

using System;
using System.Web;
/* Roger
 * 2013/5/16 17:42:04
 * 20130516174204
 * 完工分组料只能以6、7开头的有效料号
 */
public class Part : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string partNumber = context.Request.QueryString["partNumber"];
            string intype = context.Request.QueryString["intype"];
            string erpcode = context.Request.QueryString["erpcode"];
            string Asn_id = context.Request.QueryString["Asn_id"];
            string Asn_type = context.Request.QueryString["Asn_type"];
            //20130516174204
            string groupFlag = context.Request.QueryString["flag1"];

            //DreamTek.WebWMS.Business.BASE.BASE_FrmBASE_PARTListQuery listQuery = new DreamTek.WebWMS.Business.BASE.BASE_FrmBASE_PARTListQuery();
            if (!string.IsNullOrEmpty(partNumber))
            {
                partNumber = partNumber.ToUpper();
            }
            System.Data.DataTable pdat = null;

            //if (!string.IsNullOrEmpty(erpcode))
            //{
            //    if (intype.Equals("38"))//Wip Negative Issue : 38  入库
            //    {
            //        string IsSpecialWIP_N_I = context.Request.QueryString["IsAll"];
            //        pdat = listQuery.GetWipNegativeIssuePartList(partNumber, "", erpcode, IsSpecialWIP_N_I, true, false, 0, 12);
            //    }
            //    else if (intype.Equals("103"))//WipReturn : 43-103 入库
            //    {
            //        string IsSpecialWIP_Return = context.Request.QueryString["IsAll"];

            //        pdat = listQuery.GetWipReturnPartList(partNumber, "", erpcode, IsSpecialWIP_Return, true, false, 0, 12);
            //    }
            //    else if (intype.Equals("102"))//RMA Receipt 销货退回 : 15-102 入库
            //    {

            //        pdat = listQuery.GetRMAReceiptPartList(partNumber, "", erpcode, true, false, 0, 12);
                    
            //    }           //出      Wip Issue 35-203 工单发料 ；GNT維修工單領料 : 460 。
            //    else if (intype.Equals("203") || intype.Equals("460"))//Wip Issue : 35
            //    {
            //        //特殊发料 WIP Issue 只能发工单以外的料
            //        string IsSpecialWIP_Issue = "0";
            //        IsSpecialWIP_Issue = context.Request.QueryString["IsSpecialWIP_Issue"];

            //        pdat = listQuery.GetWipIssuePartList(partNumber, "", erpcode, IsSpecialWIP_Issue, true, false, 0, 12);
            //    }//Return to Vendor : 36 . 只能退相同ERPcode下的料和数量
            //    else if (intype.Equals("201"))//ReturnToVendor : 36-201
            //    {
            //        pdat = listQuery.GetReturnToVendorPartList(partNumber, "", erpcode, true, false, 0, 12);
            //    }//WIP Negative Return 48 工单负退料； 
            //    else if (intype.Equals("48"))
            //    {
            //        //特殊发料 WIP Issue 只能发工单以外的料
            //        string IsSpecialWIP_Issue = "0";
            //        IsSpecialWIP_Issue = context.Request.QueryString["IsSpecialWIP_Issue"];

            //        pdat = listQuery.GetWipNegativeReturnPartList(partNumber, "", erpcode, IsSpecialWIP_Issue, true, false, 0, 12);
            //    }//  工单超领退只能退工单超领相同工单号下的料
            //    else if (new DreamTek.WebWMS.Business.RD.InTypeQuery().ValidateInTypeIsWipIssue(intype.Trim()))
            //    {
            //        //工单超领退只能退工单超领相同工单号下的料
            //        pdat = listQuery.GetWipIssueInTypePartList(partNumber.Trim(), "", erpcode.Trim(), true, false, 0, 12);
            //    }//    工单超领只能领wip issue 相同工单下的料
            //    else if (new DreamTek.WebWMS.Business.OUT.OUT_FrmOUTTYPEListQuery().ValidateOutTypeIsWipIssue(intype.Trim()))
            //    {
            //        //工单超领只能领wip issue 相同工单下的料
            //        pdat = listQuery.GetWipIssueOutTypePartList(partNumber.Trim(), "", erpcode.Trim(), true, false, 0, 12);
            //    }
            //}

            //if (!string.IsNullOrEmpty(Asn_id))
            //{
            //    pdat = listQuery.GetListShowName(partNumber, "", Asn_id, Asn_type, true, false, 0, 12);
            //}

            //20130516174204
            //listQuery.flag = "0";
            //if (groupFlag.Trim() == "1")
            //{
            //    listQuery.flag = "1";
            //    pdat = listQuery.GetList("", "", partNumber, "", "", "", "", "", "0", "", "", "", "", "", "", false, 0, 12);
            //}

            //if (pdat == null)
            //    pdat = listQuery.GetList("", "", partNumber, "", "", "", "", "", "", "", "", "", "", "", "", false, 0, 12);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //foreach (System.Data.DataRowView drv in pdat.DefaultView)
            //{
            //    sb.AppendFormat("{0}|{1}\n", drv["PN"].ToString(), drv["P_NAME"].ToString());
            //}
            pdat.TableName = "reuslt";
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            pdat.WriteXml(sw, System.Data.XmlWriteMode.IgnoreSchema);

            context.Response.ContentType = "text/xml";
            context.Response.Write(sb.ToString());
        }
        catch (Exception)
        {
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}