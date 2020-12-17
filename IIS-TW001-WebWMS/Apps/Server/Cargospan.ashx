<%@ WebHandler Language="C#" Class="Cargospan" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using System.IO;

public class Cargospan : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string PositionCode = context.Request.QueryString["PositionCode"];
        string CinvCode = context.Request.QueryString["CinvCode"];
        string Type = context.Request.QueryString["Type"];//In or Out
        string Sum = context.Request.QueryString["Sum"];//2015-11-30 添加数量参数Sum
        string ASRSFig = context.Request.QueryString["ASRSFig"];//2015-11-30 添加是否是ASRS参数ASRSFig,1为是  [ASRS专用]
        DataTable pdat = null;
        StringBuilder sb = new StringBuilder();
        DreamTek.ASRS.Business.Base.BASE_FrmBASE_CARGOSPACEListQuery listQuery = new DreamTek.ASRS.Business.Base.BASE_FrmBASE_CARGOSPACEListQuery();
        if (Type.Equals("In"))
        {
            //2015-11-30[ASRS专用]
            if (ASRSFig == "1")
            {
                //2015-11-30 添加数量参数Sum
                pdat = listQuery.GetCargoSpaceListByBaseData(CinvCode, PositionCode, true, 15, Sum);
            }
            else
            {
                pdat = listQuery.GetCargoSpaceListByBaseData(CinvCode, PositionCode, true, 15);
            }
        }
        else if (Type.Equals("SPECIAL"))//Roger 20130618 线边维护问题
        {
            pdat = listQuery.GetCargo(PositionCode, false, 15);
        }
        else if (Type.Equals("ALLO"))//CQ 201306081910 特殊调拨单
        {
            pdat = listQuery.GetAlloCargo(PositionCode, CinvCode, false, 15);
        } 
        else
        {
             //2015-12-02[ASRS专用]
            if (ASRSFig == "1")
            {
                //2015-12-02 添加数量参数Sum
                pdat = listQuery.GetCargoSpaceListByStock(CinvCode, PositionCode, true, 15, Sum);
            }
            else
            {
                pdat = listQuery.GetCargoSpaceListByStock(CinvCode, PositionCode, true, 15);
            }
        }
        
        pdat.TableName = "reuslt";
        StringWriter sw = new StringWriter(sb);
        pdat.WriteXml(sw, System.Data.XmlWriteMode.IgnoreSchema);

        context.Response.ContentType = "text/xml";
        context.Response.Write(sb.ToString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}