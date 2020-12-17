using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using DreamTek.ASRS.Business;
using DreamTek.ASRS.Business.Base;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;


public partial class _Login : WMSBasePage
{
    public static string aa;
    protected void Page_Load(object sender, EventArgs e)
    {        
       //this.Response.Redirect("Layout/BaseLayout/Login.aspx");
        //TestU8();
        //Cicle();
        aa = a();

        string strHtml = aa;
    }
    private string a()
    {
        StringBuilder str_ret = new StringBuilder();
        string strSQL = string.Format("Exec dbo.Proc_TVShow_WareHouse '{0}'", "");
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            string str_cx = "", str_cz = "";
            string str_lineID = "";
            string str_cpositioncode = "";
            DataRow[] drcy;
            DataRow[] drcinv;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var countdz = dt.Select("cx = '" + dt.Rows[i]["cx"].ToString() + "' and LineID = '" + dt.Rows[i]["LineID"].ToString() + "'").Select(x => x.Field<string>("cz")).Distinct().Count();

                if (str_cx != dt.Rows[i]["cx"].ToString())
                {
                    str_cx = dt.Rows[i]["cx"].ToString();                   
                    str_ret.Append("</br></br><table border='1'>");
                    str_ret.Append("<tr><td style='border:0;' rowspan=" + countdz + ">" + dt.Rows[i]["LineID"].ToString() + "_ " + dt.Rows[i]["cx"].ToString() + "</td>");
                }
                else
                {
                    str_cx = dt.Rows[i]["cx"].ToString();
                    continue;
                }
                if (str_lineID != dt.Rows[i]["LineID"].ToString())
                {
                    str_lineID = dt.Rows[i]["LineID"].ToString();
                }
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
                                    tooltip.Append("总库存：" + drcy[k]["PosTotalStock"].ToString() + "&#10;" + "空间使用率：" + drcy[k]["PosspaceRate"].ToString() + "&#10;");
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
                                //ret += "<td style='background:#717975;'><asp:Label runat='server' ToolTip='" + tooltip + "'  Text='&nbsp'></asp:Label></td>";
                                //ret += "<td style='background:#717975;'><span title=" + tooltip + ">&nbsp</span></td>";
                                str_ret.Append("<td style='background:#717975;'><span title=" + tooltip + ">&nbsp</span></td>");
                            }
                            else if (!string.IsNullOrEmpty(cinvcode) && cstatus == "0")
                            {
                                // ret += "<td style='background:#DE8028;'><span title=" + tooltip + ">&nbsp</span></td>";
                                str_ret.Append("<td style='background:#DE8028;'><span title=" + tooltip + ">&nbsp</span></td>");
                            }
                            else if (cstatus == "4") //不可用
                            {
                                //ret += "<td style='background:#D42B40;'><span title=" + tooltip + ">&nbsp</span></td>";
                                str_ret.Append("<td style='background:#D42B40;'><span title=" + tooltip + ">&nbsp</span></td>");
                            }
                            else if (cstatus == "1")//冻结
                            {
                                // ret += "<td style='background:#EBF5F4;'><span title=" + tooltip + ">&nbsp</span></td>";
                                str_ret.Append("<td style='background:#EBF5F4;'><span title=" + tooltip + ">&nbsp</span></td>");
                            }
                        }
                        //ret += "</tr>";                   
                        str_ret.Append("</tr>");
                    }
                }
            }

            //  ret += "</tr></table>";
            str_ret.Append("</tr></br></br></table>");

        }
        return str_ret.ToString();
    }
    /// <summary>
    /// 获取多条主表的信息
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static IEnumerable<XElement> GetMainRows_U8(string xml)
    {
        IEnumerable<XElement> mainXml;
        try
        {
            XElement xmlStr = XElement.Parse(xml);
            mainXml = (from e in xmlStr.Descendants("Query")
                       select e
                               );
        }
        catch
        {
            mainXml = null;
        }
        return mainXml;
    }


    public string TestU8()
    {
        //DBContext context = new DBContext();
        //string erpcRdCode = "";
        //var templist = context.BASE_TypeMapping.Where(x => x.WMS_TypeCode == "46" && x.CStatus == "0" && x.type == "IN").ToList();
        //if (templist != null && templist.Count > 0)
        //{
        //    templist.ForEach(x => erpcRdCode += "'"+ x.ERP_TypeCode + "',");
        //    erpcRdCode = erpcRdCode.TrimEnd(',').ToString();
        //}
        //Alert(erpcRdCode);
        //return "";

        // Create the web request
        //string xml =
        //    " <?xml version=\"1.0\" encoding=\"utf-8\"?> "
        //    + "<ufinterface sender=\"006\" receiver=\"u8\" roottag=\"customer\" docid=\"\" proc=\"Query\" codeexchanged=\"N\" exportneedexch=\"N\" paginate=\"0\"  timestamp=\"\"> "
        //     + "<customer> <field display=\"\" name=\"code\" operation=\"=\" value=\"EAAA0001\" logic=\"\" /> 	</customer>"
        //    + "</ufinterface> ";


        //string xml =
        //       " <?xml version=\"1.0\" encoding=\"utf-8\"?> "
        //     + "<ufinterface sender=\"006\" receiver=\"u8\" roottag=\"inventory\" docid=\"\" proc=\"Query\" codeexchanged=\"N\" exportneedexch=\"N\" paginate=\"0\" display=\"存货档案\" family=\"基础档案\" timestamp=\"\" version=\"2.0\">"
        //     + "<inventory importfile=\"\" exportfile=\"\" code=\"006\" bincrementout=\"n\"> "
        //     + "<field display=\"启用日期\" name=\"cinvcode\" operation=\"=\" value=\"EAAA0007\" logic=\"and\" />  "
        //    + " <field display=\"启用日期\" name=\"dModifyDate\" operation=\"=\" value=\"2013-05-11\" logic=\"\" /> </inventory>"
        //      + "</ufinterface> ";


        //ModifyDate
        //<field display=\"存货编码\" name=\"cinvcode\" operation=\"=\" value=\"EAAA0007\" logic=\"\" /> 
        //<field display=\"存货编码\" name=\"ModifyDate\" operation=\"=\" value=\"2014-08-22 00:00:00\" logic=\"or\" /> 
        // string xml =
        //  " <?xml version=\"1.0\" encoding=\"utf-8\"?> "
        //+ "<ufinterface sender=\"006\" receiver=\"u8\" roottag=\"department\" docid=\"\" proc=\"Query\" codeexchanged=\"N\" exportneedexch=\"N\" paginate=\"0\" display=\"部门档案\" family=\"基础档案\" timestamp=\"\" version=\"2.0\">"
        //+ "<department importfile=\"\" exportfile=\"\" code=\"006\" bincrementout=\"n\"> <field display=\"部门编码\" name=\"cDepCode\" operation=\"=\" value=\"3\" logic=\"\" />  	</department>"
        // + "</ufinterface> ";
       
        string a = "2018-01-08T11:34:45.453 08:00";
        string strdateformat = a.Split(' ')[0].Replace("T", " ");
        DateTime c = DateTime.Parse(strdateformat);
        DateTime b = DateTime.ParseExact(a,"yyyy-MM-ddTHH:mm:ss.fffzzz",null);
       
        return "";

        //string xml =
        //       " <?xml version=\"1.0\" encoding=\"utf-8\"?> "
        //     + "<ufinterface sender=\"006\" receiver=\"u8\" roottag=\"sqlexe\" docid=\"\" proc=\"Query\" codeexchanged=\"N\" exportneedexch=\"N\" paginate=\"0\" display=\"存货档案\" family=\"基础档案\" timestamp=\"\" version=\"2.0\">"
        //     + "<sql value=\"select top 5 *  from rdrecord10 \"></sql>"
        //      + "</ufinterface> ";

        //EAAA0007
        //string xml =
        //     " <?xml version=\"1.0\" encoding=\"utf-8\"?> "
        //   + "<ufinterface sender=\"006\" receiver=\"u8\" roottag=\"sqlexe\" docid=\"\" proc=\"Query\" codeexchanged=\"N\" exportneedexch=\"N\" paginate=\"0\" display=\"\" family=\"\" timestamp=\"\" version=\"2.0\">"
        //    //+ "<sql value=\"SELECT * from (select ROW_NUMBER() OVER(order BY a.dDate) AS RowId,b.* from RdRecord10 a join  RdRecords10 b on a.ID = b.ID ) t where t.RowId between "+a+" and "+b+" \"></sql>"
        //     + "<sql value=\"select count(1) as count from inventory where cInvCCode = 'EAAA' \"></sql>"
        //    + "</ufinterface> ";

        string xml =
               " <?xml version=\"1.0\" encoding=\"utf-8\"?> "
             + "<ufinterface sender=\"106\" receiver=\"u8\" roottag=\"sqlexe\" docid=\"\" proc=\"Query\" codeexchanged=\"N\" exportneedexch=\"N\" paginate=\"0\" display=\"\" family=\"\" timestamp=\"\" version=\"2.0\">"
            //+ "<sql value=\"SELECT * from (select ROW_NUMBER() OVER(order BY a.dDate) AS RowId,b.* from RdRecord10 a join  RdRecords10 b on a.ID = b.ID ) t where t.RowId between "+a+" and "+b+" \"></sql>"
               + "<sql value=\"SELECT TOP 10 ROW_NUMBER() over (order by t.Lastupdatetime asc ) AS row,* FROM (select " +
                                   " a.dnverifytime,a.dnmodifytime,a.ID,a.cWhCode,a.cCode,a.cVouchType,a.cCusCode,b.cInvCode,b.iQuantity,b.cBVencode,b.csocode,b.irowno, " +
                                   " case when a.dnmodifytime is null then a.dnverifytime  " +
                                   "      when DATEDIFF(SECOND,a.dnverifytime,a.dnmodifytime)>=0 then a.dnmodifytime  " +
                                   "     else a.dnverifytime END AS 'Lastupdatetime'  " +
                                   " from RdRecord10 a JOIN rdRecords10 b ON a.ID = b.ID where a.cWhCode = '05' ) t where t.Lastupdatetime > = '' order by t.Lastupdatetime asc" +
                                   "\"></sql>"
              + "</ufinterface> ";


        string resultxml = GetERPXML(xml);
        string result = "";
        int[] start; int[] end;
        IEnumerable<XElement> mainXmlRows = GetMainRows_U8(resultxml);
      //  DateTime lastrowLastupdatetime = mainXmlRows.LastOrDefault().Descendants("Lastupdatetime").FirstOrDefault().Value; 

        

        return "";
        int ccount = Convert.ToInt32(GetCountInfo(resultxml));
       
        GetGroupByCount(ccount, 10, out start, out end);
        //int[] start = listarray[0] as int[];
        //int[] end = listarray[1] as int[];
        string xml1 = "";

        for (int i = 0; i < start.Length; i++)
        {
            xml1 = " <?xml version=\"1.0\" encoding=\"utf-8\"?> "
    + "<ufinterface sender=\"006\" receiver=\"u8\" roottag=\"sqlexe\" docid=\"\" proc=\"Query\" codeexchanged=\"N\" exportneedexch=\"N\" paginate=\"0\" display=\"\" family=\"\" timestamp=\"\" version=\"2.0\">"
            + "<sql value=\"SELECT cInvCode,cInvName,cInvStd,cInvCCode,fLength,fWidth,fHeight,fGrossW,iVolume,cInvMnemCode,cVUnit,cInvDefine4,cInvDefine13 from inventory where cInvCode = 'EAAA0071' \"></sql>"
           + "</ufinterface> ";
            if (i == 0) result = GetERPXML(xml1);//cInvCode,cInvName,cInvStd,cInvCCode,fLength,fWidth,fHeight,fGrossW,iVolume,cInvMnemCode,cVUnit,cInvDefine4,cInvDefine13
            else
            {
                //result = result + GetERPXML(xml1);
                result = UnionXML(result, GetERPXML(xml1));
            }
        }

        //    xml1 = " <?xml version=\"1.0\" encoding=\"utf-8\"?> "
        //+ "<ufinterface sender=\"006\" receiver=\"u8\" roottag=\"sqlexe\" docid=\"\" proc=\"Query\" codeexchanged=\"N\" exportneedexch=\"N\" paginate=\"0\" display=\"\" family=\"\" timestamp=\"\" version=\"2.0\">"
        //        + "<sql value=\"SELECT * from inventory where cInvCCode = 'EAAA' \"></sql>"
        //       + "</ufinterface> ";

        //result = GetERPXML(xml1);
        //   xml =
        //   " <?xml version=\"1.0\" encoding=\"utf-8\"?> "
        // + "<ufinterface sender=\"006\" receiver=\"u8\" roottag=\"sqlexe\" docid=\"\" proc=\"Query\" codeexchanged=\"N\" exportneedexch=\"N\" paginate=\"0\" display=\"\" family=\"\" timestamp=\"\" version=\"2.0\">"
        ////+ "<sql value=\"SELECT * from (select ROW_NUMBER() OVER(order BY a.dDate) AS RowId,b.* from RdRecord10 a join  RdRecords10 b on a.ID = b.ID ) t where t.RowId between "+a+" and "+b+" \"></sql>"
        //   //+ "<sql value=\"select count(1) as aa from CurrentStock \"></sql>"
        //  + "</ufinterface> ";


        //   string xml =
        //    " <?xml version=\"1.0\" encoding=\"utf-8\"?> "
        //  + "<ufinterface sender=\"006\" receiver=\"u8\" roottag=\"sqlexe\" docid=\"\" proc=\"Query\" codeexchanged=\"N\" exportneedexch=\"N\" paginate=\"0\" display=\"\" family=\"\" timestamp=\"\" version=\"2.0\">"
        //       //+ "<sql value=\"SELECT * from (select ROW_NUMBER() OVER(order BY a.dDate) AS RowId,b.* from RdRecord10 a join  RdRecords10 b on a.ID = b.ID ) t where t.RowId between "+a+" and "+b+" \"></sql>"
        //    //+ "<sql value=\"select count(1) as aa from CurrentStock \"></sql>"
        //   + "</ufinterface> ";

        return result;
        // Alert(result);
        result = WS_PDTINASN_U8(result);
        Alert(result);
        return result;

    }
    public string UnionXML(string xml1, string xml2)
    {
        var result =
        from e in XElement.Parse(xml1).Elements().Union(XElement.Parse(xml2).Elements())
        group e by e.Name into g
        select g;
        var merged = new XDocument(
            new XElement("root", result));

        return merged.ToString();

    }
    /// <summary>
    /// 根据查询XML获得ETP返回XML
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public string GetERPXML(string xml)
    {
        string url = "http://192.168.201.10/u8eai/import.asp";
        byte[] dataArray = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(xml); //Encoding.Default.GetBytes(xml);
        System.Net.HttpWebRequest request = System.Net.WebRequest.Create(url) as System.Net.HttpWebRequest;
        request.Method = "post";
        request.ContentType = "application/x-www-form-urlencoded";//"application/json";//
        request.ContentLength = dataArray.Length;
        System.IO.Stream dataStream = null;
        try
        {
            dataStream = request.GetRequestStream();
        }
        catch (Exception)
        {
            return null;//连接服务器失败
        }
        //发送请求
        dataStream.Write(dataArray, 0, dataArray.Length);
        dataStream.Close();
        // Get response
        using (System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8"));
            String s = reader.ReadToEnd();
            String result = HttpUtility.UrlDecode(s);
            return result;
        }
    }
    /// <summary>
    /// 获得分组拿数据的开始结束行数
    /// </summary>
    /// <param name="count"></param>
    /// <param name="rptcount"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void GetGroupByCount(int count, int rptcount, out int[] start, out int[] end)
    {
        int length = count / rptcount + 1;
        List<int> start1 = new List<int>();
        List<int> end1 = new List<int>();
        int i = 0;
        while (i < length - 1)
        {
            start1.Add(rptcount * i + 1);
            end1.Add(rptcount * (i + 1));
            i++;
        }
        start1.Add(rptcount * i + 1);
        end1.Add(count);
        start = start1.ToArray();
        end = end1.ToArray();
    }

    #region 物料
    /// <summary>
    /// 雪龙物料接口
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public string WS_MATERIAL_U8(string xml)
    {
        string result = "";
        //WCFLog.Log("新增物料接口WS_MATERIAL_U8收到请求,请求参数为:" + xml);
        using (var modContext = this.context)
        {
            using (var dbContextTransaction = modContext.Database.BeginTransaction())
            {
                //请求ID                
                int RowSeq = 1;
                try
                {
                    IEnumerable<XElement> mainXmlRows = GetMainRows(xml);
                    string errMsg = string.Empty;
                    IGenericRepository<BASE_PART> bptcon = new GenericRepository<BASE_PART>(modContext);
                    IGenericRepository<BASE_PART_CARGOSPACE> con = new GenericRepository<BASE_PART_CARGOSPACE>(modContext);
                    if (mainXmlRows != null && mainXmlRows.Any())
                    {

                        //获取基础资料api允许接收的最大数据条数的配置
                        int maxCount = 50;
                        var modConfig = modContext.SYS_CONFIG.Where(x => x.code == "010001").FirstOrDefault();
                        if (modConfig != null)
                        {
                            maxCount = Convert.ToInt32(modConfig.config_value);
                        }
                        if (mainXmlRows.Count() > maxCount)
                        {
                            errMsg += "为保证接口的性能及稳定，最多限制传输" + maxCount.ToString() + "条数据！";
                        }

                        if (string.IsNullOrEmpty(errMsg))
                        {
                            foreach (var mainRow in mainXmlRows)
                            {
                                bool isNew = true;//默认是新增操作
                                var rowcInvCode = mainRow.Descendants("cInvCode").FirstOrDefault();     //物料编码                                  
                                if (rowcInvCode != null)
                                {
                                    #region 验证是新增还是修改
                                    BASE_PART modPart = modContext.BASE_PART.Where(x => x.cpartnumber == rowcInvCode.Value.Trim()).FirstOrDefault();
                                    if (modPart == null)
                                    {
                                        modPart = new BASE_PART();
                                        modPart.id = Guid.NewGuid().ToString();
                                    }
                                    else
                                    {
                                        isNew = false;//不是新增即为修改
                                    }
                                    #endregion

                                    BASE_PART_CARGOSPACE base_part = new BASE_PART_CARGOSPACE();
                                    base_part.id = Guid.NewGuid().ToString();
                                    #region 参数验证

                                    //物料编码
                                    if (rowcInvCode != null)
                                    {
                                        if (isNew)
                                        {
                                            if (string.IsNullOrEmpty(rowcInvCode.Value.Trim()))
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "物料编码不能为空!";
                                                continue;
                                            }
                                            if (rowcInvCode.Value.Trim().Length > 50)
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "物料编码长度不能超过50!";
                                                continue;
                                            }
                                            modPart.cpartnumber = rowcInvCode.Value.Trim();
                                        }
                                    }
                                    //物料名称
                                    var rowcInvName = mainRow.Descendants("cInvName").FirstOrDefault();
                                    if (rowcInvName != null)
                                    {
                                        if (string.IsNullOrEmpty(rowcInvName.Value.Trim()))
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "物料名称不能为空!";
                                            continue;
                                        }
                                        if (rowcInvName.Value.Trim().Length > 150)
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "物料名称长度不能超过150!";
                                            continue;
                                        }
                                        modPart.cpartname = rowcInvName.Value.Trim();
                                    }
                                    //物料规格
                                    var rowcInvStd = mainRow.Descendants("cInvStd").FirstOrDefault();
                                    if (rowcInvStd != null)
                                    {
                                        if (!string.IsNullOrEmpty(rowcInvStd.Value.Trim()) && rowcInvStd.Value.Trim().Length > 300)
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "物料规格长度不能超过300!";
                                            continue;
                                        }
                                        modPart.cspecifications = rowcInvStd.Value.Trim();
                                    }

                                    //物料类型
                                    var rowcInvCCode = mainRow.Descendants("cInvCCode").FirstOrDefault();
                                    if (rowcInvCCode != null)
                                    {
                                        var modType = modContext.BASE_TypeMapping.Where(x => x.ERP_TypeCode.Equals(rowcInvCCode.Value.Trim()) && x.CStatus == "0" && x.type == "PART_TYPE").FirstOrDefault();
                                        if (modType == null)
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "物料类型参数无效!";
                                            continue;
                                        }
                                        else
                                        {
                                            modPart.ctype = modType.WMS_TypeCode;
                                        }
                                    }
                                    //物料状态

                                    modPart.cstatus = "0";



                                    //长
                                    var rowfLength = mainRow.Descendants("fLength").FirstOrDefault();
                                    if (rowfLength != null)
                                    {
                                        if (!string.IsNullOrEmpty(rowfLength.Value.Trim()))
                                        {
                                            decimal decmalVal = 0;
                                            if (Decimal.TryParse(rowfLength.Value.Trim(), out decmalVal))
                                            {
                                                modPart.ilength = decmalVal;
                                            }
                                            else
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "长不是有效的数值!";
                                                continue;
                                            }
                                        }
                                    }
                                    //宽
                                    var rowfWidth = mainRow.Descendants("fWidth").FirstOrDefault();
                                    if (rowfWidth != null)
                                    {
                                        if (!string.IsNullOrEmpty(rowfWidth.Value.Trim()))
                                        {
                                            decimal decmalVal = 0;
                                            if (Decimal.TryParse(rowfWidth.Value.Trim(), out decmalVal))
                                            {
                                                modPart.iwidth = decmalVal;
                                            }
                                            else
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "宽不是有效的数值!";
                                                continue;
                                            }
                                        }
                                    }
                                    //高
                                    var rowfHeight = mainRow.Descendants("fHeight").FirstOrDefault();
                                    if (rowfHeight != null)
                                    {
                                        if (!string.IsNullOrEmpty(rowfHeight.Value.Trim()))
                                        {
                                            decimal decmalVal = 0;
                                            if (Decimal.TryParse(rowfHeight.Value.Trim(), out decmalVal))
                                            {
                                                modPart.iheight = decmalVal;
                                            }
                                            else
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "高不是有效的数值!";
                                                continue;
                                            }
                                        }
                                    }
                                    //毛重
                                    var rowfGrossW = mainRow.Descendants("fGrossW").FirstOrDefault();
                                    if (rowfGrossW != null)
                                    {
                                        if (!string.IsNullOrEmpty(rowfGrossW.Value.Trim()))
                                        {
                                            decimal decmalVal = 0;
                                            if (Decimal.TryParse(rowfGrossW.Value.Trim(), out decmalVal))
                                            {
                                                modPart.icw = decmalVal;
                                            }
                                            else
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "毛重不是有效的数值!";
                                                continue;
                                            }
                                        }
                                    }
                                    //体积
                                    var rowiVolume = mainRow.Descendants("iVolume").FirstOrDefault();
                                    if (rowiVolume != null)
                                    {
                                        if (!string.IsNullOrEmpty(rowiVolume.Value.Trim()))
                                        {
                                            decimal decmalVal = 0;
                                            if (Decimal.TryParse(rowiVolume.Value.Trim(), out decmalVal))
                                            {
                                                modPart.cvolume = decmalVal;
                                            }
                                            else
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "体积不是有效的数值!";
                                                continue;
                                            }
                                        }
                                    }
                                    //助记码
                                    var rowcInvMnemCode = mainRow.Descendants("cInvMnemCode").FirstOrDefault();
                                    if (rowcInvMnemCode != null)
                                    {
                                        if (!string.IsNullOrEmpty(rowcInvMnemCode.Value.Trim()) && rowcInvMnemCode.Value.Trim().Length > 200)
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "助记码长度不能超过100!";
                                            continue;
                                        }
                                        modPart.calias = rowcInvMnemCode.Value.Trim();
                                    }
                                    //单位
                                    var rowcVUnit = mainRow.Descendants("rowcVUnit").FirstOrDefault();
                                    if (rowcVUnit != null)
                                    {
                                        if (!string.IsNullOrEmpty(rowcVUnit.Value.Trim()) && rowcVUnit.Value.Trim().Length > 20)
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "单位长度不能超过20!";
                                            continue;
                                        }
                                        modPart.cunits = rowcVUnit.Value.Trim();
                                    }
                                    #region 暂时没有找到对应字段的的字段
                                    ////保质期(天数)

                                    //if (item.Attribute("name") != null && item.Attribute("name").Value == "EXP_DAYS")
                                    //{
                                    //    if (!string.IsNullOrEmpty(item.Value.Trim()))
                                    //    {
                                    //        decimal decmalVal = 0;
                                    //        if (Decimal.TryParse(item.Value.Trim(), out decmalVal))
                                    //        {
                                    //            modPart.exp_days = decmalVal;
                                    //        }
                                    //        else
                                    //        {
                                    //            errMsg += "第" + RowSeq + "条数据" + "保质期(天数)不是有效的数值!";
                                    //            continue;
                                    //        }
                                    //    }
                                    //}

                                    ////物料号的ERP编码
                                    //if (item.Attribute("name") != null && item.Attribute("name").Value == "CERPCODE")
                                    //{
                                    //    if (!string.IsNullOrEmpty(item.Value.Trim()) && item.Value.Trim().Length > 50)
                                    //    {
                                    //        errMsg += "第" + RowSeq + "条数据" + "物料号的ERP编码长度不能超过50!";
                                    //        continue;
                                    //    }
                                    //    modPart.cerpcode = item.Value.Trim();
                                    //}
                                    ////默认储位
                                    //if (item.Attribute("name") != null && item.Attribute("name").Value == "CDEFAULTCARGO")
                                    //{
                                    //    if (!string.IsNullOrEmpty(item.Value.Trim()))
                                    //    {
                                    //        //查询数据库中的储位信息
                                    //        var modCargospace = modContext.BASE_CARGOSPACE.Where(x => x.cpositioncode == item.Value.Trim()).FirstOrDefault();
                                    //        if (modCargospace != null)
                                    //        {
                                    //            //TODO:旧的物料区域关系是否要删除
                                    //            modPart.cdefaultcargo = modCargospace.cpositioncode;

                                    //            base_part.ccreateownercode = "WSM";
                                    //            base_part.cpositioncode = modCargospace.cpositioncode;

                                    //            //储位对应仓库
                                    //            var modWarehouse = modContext.BASE_WAREHOUSE.Where(x => x.id == modCargospace.warehouseid).FirstOrDefault();
                                    //            //传入仓库
                                    //            var nodeWare = rowFields.Where(x => x.Attribute("name").Value == "CDEFAULTWARE").FirstOrDefault();
                                    //            if (nodeWare != null && !string.IsNullOrEmpty(nodeWare.Value.Trim()))
                                    //            {
                                    //                if (modWarehouse.cwareid == nodeWare.Value.Trim())
                                    //                {
                                    //                    base_part.cwareid = modWarehouse.id;
                                    //                    modPart.cdefaultware = modWarehouse.id;
                                    //                }
                                    //                else
                                    //                {
                                    //                    errMsg += "第" + RowSeq + "条数据" + "默认储位【" + item.Value.Trim() + "】不在默认仓库【" + nodeWare.Value.Trim() + "】";
                                    //                    continue;
                                    //                }
                                    //            }
                                    //            else
                                    //            {
                                    //                //未传入默认仓库时，直接按默认储位对应仓库保存
                                    //                modPart.cdefaultware = modWarehouse.id;
                                    //                base_part.cwareid = modWarehouse.id;
                                    //            }
                                    //        }
                                    //        else
                                    //        {
                                    //            errMsg += "第" + RowSeq + "条数据" + "默认储位【" + item.Value.Trim() + "】不存在！";
                                    //            continue;
                                    //        }
                                    //    }
                                    //}
                                    ////是否保税
                                    //if (item.Attribute("name") != null && item.Attribute("name").Value == "BONDED")
                                    //{
                                    //    if (!string.IsNullOrEmpty(item.Value.Trim()))
                                    //    {
                                    //        if (item.Value.Trim() != "0" && item.Value.Trim() != "1")
                                    //        {
                                    //            errMsg += "第" + RowSeq + "条数据" + "是否保税参数无效!";
                                    //            continue;
                                    //        }
                                    //        modPart.bonded = Convert.ToDecimal(item.Value.Trim());
                                    //    }
                                    //    else
                                    //    {
                                    //        modPart.bonded = 1;//不传默认非保税
                                    //    }
                                    //}
                                    //ABC料
                                    //if (item.Attribute("name") != null && item.Attribute("name").Value == "MTYPE")
                                    //{
                                    //    if (!string.IsNullOrEmpty(item.Value.Trim()))
                                    //    {
                                    //        if (item.Value.Trim() != "0" && item.Value.Trim() != "1" && item.Value.Trim() != "2")
                                    //        {
                                    //            errMsg += "第" + RowSeq + "条数据" + "ABC料参数无效!";
                                    //            continue;
                                    //        }
                                    //        modPart.mtype = Convert.ToDecimal(item.Value.Trim());
                                    //    }
                                    //}
                                    ////净重
                                    //if (item.Attribute("name") != null && item.Attribute("name").Value == "INW")
                                    //{
                                    //    if (!string.IsNullOrEmpty(item.Value.Trim()))
                                    //    {
                                    //        decimal decmalVal = 0;
                                    //        if (Decimal.TryParse(item.Value.Trim(), out decmalVal))
                                    //        {
                                    //            modPart.inw = decmalVal;
                                    //        }
                                    //        else
                                    //        {
                                    //            errMsg += "第" + RowSeq + "条数据" + "净重不是有效的数值!";
                                    //            continue;
                                    //        }
                                    //    }
                                    //}

                                    ////备注
                                    //if (item.Attribute("name") != null && item.Attribute("name").Value == "CMEMO")
                                    //{
                                    //    if (!string.IsNullOrEmpty(item.Value.Trim()) && item.Value.Trim().Length > 100)
                                    //    {
                                    //        errMsg += "第" + RowSeq + "条数据" + "备注长度不能超过100!";
                                    //        continue;
                                    //    }
                                    //    modPart.cmemo = item.Value.Trim();
                                    //}
                                    ////包装数量
                                    //if (item.Attribute("name") != null && item.Attribute("name").Value == "BOXNUM")
                                    //{
                                    //    if (!string.IsNullOrEmpty(item.Value.Trim()))
                                    //    {
                                    //        decimal decmalVal = 0;
                                    //        if (Decimal.TryParse(item.Value.Trim(), out decmalVal))
                                    //        {
                                    //            modPart.boxnum = decmalVal;
                                    //        }
                                    //        else
                                    //        {
                                    //            errMsg += "第" + RowSeq + "条数据" + "包装数量不是有效的数值!";
                                    //            continue;
                                    //        }
                                    //    }
                                    //}
                                    #endregion

                                    #endregion

                                }
                                else
                                {
                                    errMsg = "第" + RowSeq + "条数据" + "格式不正确！";
                                    break;
                                }
                                RowSeq++;
                            }

                            //bptcon.Save();
                            //con.Save();

                        }
                    }
                    else
                    {
                        errMsg = "请求信息格式不正确或者当前没有满足条件的数据！";
                    }
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        dbContextTransaction.Rollback();
                        result = ReturnMSG("", "1", errMsg);
                        //InsertErrorMsg(this.context, errMsg, "WS_MATERIAL", "保存物料信息失败", "", "3", "物料信息", xml);
                    }
                    else
                    {
                        result = ReturnMSG("", "0", "保存成功！");
                        //InsertErrorMsg(modContext, strSuccessValue, "WS_MATERIAL", "保存物料信息成功", "", "3", "物料信息", xml);
                        dbContextTransaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    result = ReturnMSG("", "1", "保存失败！");
                    //InsertErrorMsg(this.context, "发生异常：当前数据行" + RowSeq.ToString() + ex.ToString(), "WS_MATERIAL", "新增物料失败", "", "3", "物料信息", xml);
                }
            }
        }
        // WCFLog.Log("新增物料接口WS_MATERIAL返回结果为:" + result);
        return result;
    }
    #endregion
    //public string WS_CUSTOMER_TEST(string xml)
    //{
    //    string result = "";
    //    // WCFLog.Log("新增客户接口WS_CUSTOMER收到请求,请求参数为:" + xml);
    //    using (var modContext = this.context)
    //    {
    //        using (var dbContextTransaction = modContext.Database.BeginTransaction())
    //        {
    //            //请求ID
    //            string requestId = string.Empty;
    //            int RowSeq = 1;
    //            try
    //            {
    //                //22
    //                //IEnumerable<XElement> mainXmlRows = GetMainRows(xml);
    //                IEnumerable<XElement> mainXmlRows = GetMainRows(xml);
    //                //22
    //                // requestId = GetRequestId(xml);
    //                //requestId = GetRequestId(xml);
    //                string errMsg = string.Empty;

    //                if (mainXmlRows != null && mainXmlRows.Any())
    //                {

    //                    //获取基础资料api允许接收的最大数据条数的配置
    //                    int maxCount = 50;
    //                    var modConfig = modContext.SYS_CONFIG.Where(x => x.code == "010001").FirstOrDefault();
    //                    if (modConfig != null)
    //                    {
    //                        maxCount = Convert.ToInt32(modConfig.config_value);
    //                    }
    //                    if (mainXmlRows.Count() > maxCount)
    //                    {
    //                        errMsg += "为保证接口的性能及稳定，最多限制传输" + maxCount.ToString() + "条数据！";
    //                    }

    //                    if (string.IsNullOrEmpty(errMsg))
    //                    {
    //                        foreach (var mainRow in mainXmlRows)
    //                        {
    //                            bool isNew = true;//默认是新增操作
    //                            var rowcCusCode = mainRow.Descendants("cCusCode").FirstOrDefault();
    //                            if (rowcCusCode != null)
    //                            {
    //                                #region 验证是新增还是修改
    //                                BASE_CLIENT modClient = modContext.BASE_CLIENT.Where(x => x.cclientid == rowcCusCode.Value.Trim()).FirstOrDefault();
    //                                if (modClient == null)
    //                                {
    //                                    modClient = new BASE_CLIENT();
    //                                    modClient.id = Guid.NewGuid().ToString();
    //                                }
    //                                else
    //                                {
    //                                    isNew = false;//不是新增即为修改
    //                                }
    //                                #endregion

    //                                #region 参数验证
    //                                //客户编码
    //                                if (rowcCusCode != null)
    //                                {
    //                                    if (isNew)
    //                                    {
    //                                        if (string.IsNullOrEmpty(rowcCusCode.Value.Trim()))
    //                                        {
    //                                            errMsg += "第" + RowSeq + "条数据" + "客户编码不能为空!";
    //                                            continue;
    //                                        }
    //                                        if (rowcCusCode.Value.Trim().Length > 30)
    //                                        {
    //                                            errMsg += "第" + RowSeq + "条数据" + "客户编码长度不能超过30!";
    //                                            continue;
    //                                        }
    //                                        modClient.cclientid = rowcCusCode.Value.Trim();
    //                                    }
    //                                }
    //                                var rowcCusName = mainRow.Descendants("cCusName").FirstOrDefault();
    //                                //客户名称
    //                                if (rowcCusName != null)
    //                                {
    //                                    if (string.IsNullOrEmpty(rowcCusName.Value.Trim()))
    //                                    {
    //                                        errMsg += "第" + RowSeq + "条数据" + "客户名称不能为空!";
    //                                        continue;
    //                                    }
    //                                    if (rowcCusName.Value.Trim().Length > 240)
    //                                    {
    //                                        errMsg += "第" + RowSeq + "条数据" + "客户名称长度不能超过240!";
    //                                        continue;
    //                                    }
    //                                    modClient.cclientname = rowcCusName.Value.Trim();
    //                                }
    //                                else
    //                                {
    //                                    errMsg += "第" + RowSeq + "条数据" + "客户名称不能为空!";
    //                                    continue;
    //                                }
    //                                //助记码
    //                                var rowccalias = mainRow.Descendants("cCusMnemCode").FirstOrDefault();
    //                                if (rowccalias != null)
    //                                {
    //                                    if (rowccalias.Value.Trim().Length > 50)
    //                                    {
    //                                        errMsg += "第" + RowSeq + "条数据" + "助记码长度不能超过50!";
    //                                        continue;
    //                                    }
    //                                    modClient.calias = rowccalias.Value.Trim();
    //                                }
    //                                else
    //                                {
    //                                    modClient.calias = "";
    //                                }
    //                                ////客户状态
    //                                var rowbCusState = mainRow.Descendants("bCusState").FirstOrDefault();
    //                                if (rowbCusState != null)
    //                                {
    //                                    if (rowbCusState.Value.Trim() != "0" && rowbCusState.Value.Trim() != "true")
    //                                    {
    //                                        errMsg += "第" + RowSeq + "条数据" + "客户状态参数无效!";
    //                                        continue;
    //                                    }
    //                                    modClient.cstatus = "0";
    //                                }
    //                                else
    //                                {
    //                                    modClient.cstatus = "0";
    //                                }

    //                                #endregion

    //                                #region 数据保存
    //                                //没有错误信息
    //                                if (string.IsNullOrEmpty(errMsg))
    //                                {
    //                                    if (isNew)
    //                                    {
    //                                        modClient.createtime = DateTime.Now;
    //                                        modClient.createowner = "WMS";
    //                                        modClient.lastupdatetime = DateTime.Now;
    //                                        modClient.lastupdateowner = "WMS";
    //                                        modContext.BASE_CLIENT.Add(modClient);
    //                                        modContext.SaveChanges();
    //                                    }
    //                                    else
    //                                    {
    //                                        modClient.lastupdatetime = DateTime.Now;
    //                                        modClient.lastupdateowner = "WMS";

    //                                        modContext.BASE_CLIENT.Attach(modClient);
    //                                        modContext.Entry(modClient).State = System.Data.Entity.EntityState.Modified;
    //                                        modContext.SaveChanges();
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    break;//结束保存数据
    //                                }
    //                                #endregion

    //                            }
    //                            else
    //                            {
    //                                errMsg = "第" + RowSeq + "条数据" + "格式不正确！";
    //                                break;
    //                            }
    //                            RowSeq++;
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    errMsg = "请求信息格式不正确或者当前没有满足条件的数据！";
    //                }


    //                if (!string.IsNullOrEmpty(errMsg))
    //                {
    //                    dbContextTransaction.Rollback();
    //                    //22 
    //                    //result = ReturnMSG(requestId, "1", errMsg);
    //                    result = ReturnMSG(requestId, "1", errMsg);
    //                    //22
    //                    //InsertErrorMsg(this.context, errMsg, "WS_CUSTOMER", "保存客户信息失败", "", "1", "客户信息", xml);
    //                }
    //                else
    //                {
    //                    //22
    //                    //result = ReturnMSG(requestId, "0", "保存成功！");
    //                    result = ReturnMSG(requestId, "0", "保存成功！");
    //                    //InsertErrorMsg(modContext, strSuccessValue, "WS_CUSTOMER", "保存客户信息成功", "", "1", "客户信息", xml);
    //                    dbContextTransaction.Commit();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                dbContextTransaction.Rollback();
    //                //22
    //                //result = ReturnMSG(requestId, "1", "保存失败！");
    //                result = ReturnMSG(requestId, "1", "保存失败！");
    //                //22
    //                //InsertErrorMsg(this.context, "发生异常：当前数据行" + RowSeq.ToString() + ex.ToString(), "WS_CUSTOMER", "保存客户信息失败", "", "1", "客户信息", xml);
    //            }
    //        }
    //    }
    //    //22
    //    //WCFLog.Log("新增客户接口WS_CUSTOMER返回结果为:" + result);
    //    return result;
    //}
    #region 成品入库
    public string WS_PDTINASN_U8(string xml)
    {

        string result = "";
        //WCFLog.Log("新增成品入库接口WS_PDTINASN_U8收到请求,请求参数为:" + xml);
        using (var modContext = this.context)
        {
            using (var dbContextTransaction = modContext.Database.BeginTransaction())
            {
                //请求ID                
                int RowSeq = 1;
                try
                {
                    IEnumerable<XElement> mainXmlRows = GetMainRows(xml);
                    string errMsg = string.Empty;
                    int lineid = 1;//项次
                    if (mainXmlRows != null && mainXmlRows.Any())
                    {
                        if (string.IsNullOrEmpty(errMsg))
                        {
                            foreach (var mainRow in mainXmlRows)
                            {
                                bool isNew = true;//默认是新增操作
                                bool IsNewBody = true;
                                var rowID = mainRow.Descendants("ID").FirstOrDefault(); //ID
                                var rowcCode = mainRow.Descendants("cCode").FirstOrDefault(); //ERPCODE
                                var rowcInvCode = mainRow.Descendants("cInvCode").FirstOrDefault(); //cInvCode 物料编码
                                if (rowID != null && rowcCode != null && rowcInvCode != null)
                                {
                                    #region 验证是新增还是修改
                                    //表头
                                    INASN modInAsn = modContext.INASN.AsEnumerable().Where(x => x.cerpcode == rowcCode.Value.Trim() && x.ccreateownercode == "ERP").FirstOrDefault();
                                    if (modInAsn == null)
                                    {
                                        modInAsn = new INASN();
                                        modInAsn.id = Guid.NewGuid().ToString();
                                        modInAsn.cticketcode = new Fun_CreateNo().CreateNo("INASN");
                                    }
                                    else
                                    {
                                        isNew = false;//不是新增即为修改
                                    }
                                    //表身
                                    INASN_D modInAsn_D = modContext.INASN_D.AsEnumerable().Where(x => x.id == modInAsn.id && x.cinvcode == rowcInvCode.Value.Trim()).FirstOrDefault();
                                    if (modInAsn_D == null)
                                    {

                                        modInAsn_D = new INASN_D();
                                        modInAsn_D.id = modInAsn.id;
                                        modInAsn_D.ids = Guid.NewGuid().ToString();
                                        if (isNew)
                                        {
                                            lineid = 1;
                                        }
                                        modInAsn_D.LineID = lineid;
                                        lineid++;

                                    }
                                    else
                                    {
                                        modInAsn_D.LineID = lineid;
                                        IsNewBody = false;//不是新增即为修改
                                    }

                                    #endregion

                                    #region 参数验证
                                    //ERPCODE                                 
                                    if (rowcCode != null)
                                    {
                                        if (rowcCode.Value.Trim().Length > 30)
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "ERPCOED长度不能超过30!";
                                            continue;
                                        }
                                        modInAsn.cerpcode = rowcCode.Value.Trim();

                                    }
                                    //入库类型 cVouchType
                                    var rowcVouchType = mainRow.Descendants("cVouchType").FirstOrDefault();
                                    if (rowcVouchType != null)
                                    {
                                        if (isNew)
                                        {
                                            if (string.IsNullOrEmpty(rowcVouchType.Value.Trim()))
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "入库类型不能为空!";
                                                continue;
                                            }
                                            var modType = modContext.BASE_TypeMapping.Where(x => x.ERP_TypeCode.Equals(rowcVouchType.Value.Trim()) && x.CStatus == "0" && x.type == "IN").FirstOrDefault();
                                            if (modType != null)
                                            {
                                                modInAsn.itype = modType.WMS_TypeCode.Trim();
                                            }
                                            else
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "入库类型在WMS中没有匹配值!";
                                                continue;
                                            }
                                        }
                                    }
                                    //仓库编码
                                    var rowcWhCode = mainRow.Descendants("cWhCode").FirstOrDefault();
                                    if (rowcWhCode != null)
                                    {
                                        if (IsNewBody)
                                        {
                                            if (string.IsNullOrEmpty(rowcWhCode.Value.Trim()))
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "仓库编码不能为空!";
                                                continue;
                                            }
                                            if (rowcWhCode.Value.Trim().Length > 30)
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "仓库编码长度不能超过30!";
                                                continue;
                                            }
                                            var modType = modContext.BASE_TypeMapping.Where(x => x.ERP_TypeCode.Equals(rowcWhCode.Value.Trim()) && x.CStatus == "0" && x.type == "WareHouse_Type").FirstOrDefault();
                                            if (modType != null)
                                            {
                                                modInAsn_D.DfWHSCode = modType.WMS_TypeCode.Trim();
                                                modInAsn.WORKTYPE = CommonHelp.GetWorkTypeByWareHouse(rowcWhCode.Value.Trim());
                                            }
                                            else
                                            {
                                                errMsg += "第" + RowSeq + "条数据" + "仓库编码在WMS中没有匹配值!";
                                                continue;
                                            }
                                        }
                                    }
                                    //料号
                                    if (rowcInvCode != null)
                                    {
                                        if (string.IsNullOrEmpty(rowcInvCode.Value.Trim()))
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "物料编码不能为空!";
                                            continue;
                                        }
                                        if (rowcInvCode.Value.Trim().Length > 240)
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "物料编码长度不能超过240!";
                                            continue;
                                        }
                                        var modCinv = modContext.BASE_PART.Where(x => x.cpartnumber.Equals(rowcInvCode.Value.Trim()) && x.cstatus == "0").FirstOrDefault();
                                        if (modCinv != null)
                                        {
                                            modInAsn_D.cinvcode = rowcInvCode.Value.Trim();
                                            modInAsn_D.cinvname = modCinv.cpartname;
                                        }
                                        else
                                        {
                                            modInAsn_D.cinvcode = rowcInvCode.Value.Trim();
                                            modInAsn_D.cinvname = rowcInvCode.Value.Trim();
                                            //errMsg += "第" + RowSeq + "条数据" + "物料编码在WMS中不存在!";
                                            //continue;
                                        }
                                    }
                                    else
                                    {
                                        errMsg += "第" + RowSeq + "条数据" + "物料编码不能为空!";
                                        continue;
                                    }
                                    //数量
                                    var rowiQuantity = mainRow.Descendants("iQuantity").FirstOrDefault();
                                    if (rowiQuantity != null)
                                    {
                                        decimal dqty = 0m;
                                        bool isDecimal = decimal.TryParse(rowiQuantity.Value.Trim(), out dqty);
                                        if (!isDecimal)
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "数量格式不正确!";
                                            continue;
                                        }
                                        modInAsn_D.iquantity = dqty;
                                    }
                                    else
                                    {
                                        errMsg += "第" + RowSeq + "条数据" + "数量不能为空!";
                                        continue;
                                    }
                                    //CERPCODELINE
                                    var rowirowno = mainRow.Descendants("irowno").FirstOrDefault();
                                    if (rowirowno != null)
                                    {
                                        modInAsn_D.cerpcodeline = rowirowno.Value.Trim();
                                    }
                                    else
                                    {
                                        errMsg += "第" + RowSeq + "条数据" + "ERPCodeLine不能为空";
                                        continue;
                                    }
                                    //客户
                                    var rowcCusCode = mainRow.Descendants("cCusCode").FirstOrDefault();
                                    if (rowcCusCode != null)
                                    {
                                        if (rowcInvCode.Value.Trim().Length > 30)
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "客户编码长度不能超过30!";
                                            continue;
                                        }
                                        var modCint = modContext.BASE_CLIENT.Where(x => x.cclientid.Equals(rowcCusCode.Value.Trim()) && x.cstatus == "0").FirstOrDefault();
                                        if (modCint != null)
                                        {
                                            modInAsn.cvendercode = modCint.cclientid;
                                            modInAsn.cvender = modCint.cclientname;
                                        }
                                        else
                                        {
                                            errMsg += "第" + RowSeq + "条数据" + "客户编码在WMS中不存在!";
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        modInAsn_D.po_linenumbername = "";
                                        modInAsn_D.po_numbername = "";
                                    }

                                    #endregion

                                    #region 数据保存
                                    //没有错误信息
                                    if (string.IsNullOrEmpty(errMsg))
                                    {
                                        //表头只有新增才处理，更新是不做任何处理
                                        if (isNew)
                                        {
                                            var current = DateTime.Now;
                                            modInAsn.dcreatetime = current;
                                            modInAsn.ddefine4 = "1";//数量来源 ( 0 : WMS，1 :oracle ERP )
                                            modInAsn.ccreateownercode = "ERP";
                                            modInAsn.cstatus = "0";
                                            modInAsn.reasoncode = string.Empty;
                                            modInAsn.reasoncontent = string.Empty;
                                            modContext.INASN.Add(modInAsn);
                                        }
                                        if (IsNewBody)
                                        {
                                            modInAsn_D.cstatus = "0";
                                            modInAsn_D.manual = 0;
                                            modContext.INASN_D.Add(modInAsn_D);
                                            modContext.SaveChanges();
                                        }
                                        else
                                        {
                                            modContext.INASN_D.Attach(modInAsn_D);
                                            modContext.Entry(modInAsn_D).State = System.Data.Entity.EntityState.Modified;
                                            modContext.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        break;//结束保存数据
                                    }
                                    #endregion

                                }
                                else
                                {
                                    errMsg = "第" + RowSeq + "条数据" + "格式不正确！";
                                    break;
                                }
                                RowSeq++;
                            }
                        }
                    }
                    else
                    {
                        errMsg = "请求信息格式不正确或者当前没有满足条件的数据！";
                    }
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        dbContextTransaction.Rollback();
                        result = ReturnMSG("", "1", errMsg);
                        //InsertErrorMsg(this.context, errMsg, "WS_PDTINASN_U8", "保存成品入库信息失败", "", "1", "成品入库信息", xml);

                    }
                    else
                    {

                        result = ReturnMSG("", "0", "保存成功！");
                        //InsertErrorMsg(modContext, strSuccessValue, "WS_PDTINASN_U8", "保存成品入库信息成功", "", "1", "成品入库信息", xml);
                        dbContextTransaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    result = ReturnMSG("", "1", "保存失败！");
                    //InsertErrorMsg(this.context, "发生异常：当前数据行" + RowSeq.ToString() + ex.ToString(), "WS_PDTINASN_U8", "保存成品入库信息失败", "", "1", "成品入库信息", xml);
                }
            }
        }
        // WCFLog.Log("新增成品入库接口WS_PDTINASN_U8返回结果为:" + result);
        return result;
    }
    #endregion
    #region XMLhelper
    /// <summary>
    /// 获取XML的类型名称
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="flag"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static string GetXMLCaseType(string xml, out int flag, out string msg)
    {
        string caseType = string.Empty;
        flag = 0;
        msg = string.Empty;
        XElement xmlStr = null;
        try
        {
            IEnumerable<XElement> xmlList = GetMainInfo(xml);
            if (xmlList == null)
            {
                throw new Exception("单头数据格式不正确");
            }
            else if (xmlList != null && xmlList.Count() == 0)
            {
                throw new Exception("单头数据格式不正确");
            }


            //xmlStr = XElement.Parse(xml);
            //XElement mainXml = (from e in xmlStr.Descendants("service")
            //                    select e
            //               ).First();
            //caseType = mainXml.Attribute("name").Value.ToUpper();

            //if (!caseType.Equals("WS_CUSTOMER") && !caseType.Equals("WS_SUPPLIER") && !caseType.Equals("WS_MATERIAL") && !caseType.Equals("WS_PO_CREATE")
            //    && !caseType.Equals("WS_INASN_CREATE") && !caseType.Equals("WS_OUTASN_CREATE") && !caseType.Equals("WS_OUTORDER_CREATE") && !caseType.Equals("WS_OUTORDER_CHANGE")
            //    )
            //{
            //    flag = 1;
            //    msg = "传入的单据类型不正确！";
            //}

            //if (caseType.Equals("WS_INASN_CREATE") || caseType.Equals("WS_OUTASN_CREATE") || caseType.Equals("WS_OUTORDER_CREATE") || caseType.Equals("WS_PO_CREATE")
            //    || caseType.Equals("WS_OUTORDER_CHANGE"))
            //{
            //xmlList = GetDetailsInfo(xml);
            //if (xmlList == null)
            //{
            //    throw new Exception("单身数据格式不正确");
            //}
            //else if (xmlList != null && xmlList.Count() == 0)
            //{
            //    throw new Exception("单身数据格式不正确");
            //}
            //}
        }
        catch (Exception ex)
        {
            flag = 1;
            msg = "xml格式不正确!" + ex.Message;
        }
        return caseType;
    }

    /// <summary>
    /// 获取主表的信息
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static IEnumerable<XElement> GetMainInfo(string xml)
    {
        XElement xmlStr = XElement.Parse(xml);
        IEnumerable<XElement> mainXml = (from e in xmlStr.Descendants("ufinterface")//.Elements("NewDataSet").Elements("Query")
                                         select e
                           );
        return mainXml;
    }
    /// <summary>
    /// 通过返回的XML格式解析出当前查询的总条数
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static int GetCountInfo(string xml)
    {
        XElement xmlStr = XElement.Parse(xml);
        int count = int.Parse((from e in xmlStr.Descendants("count")//.Elements("NewDataSet").Elements("Query")
                               select e
                           ).FirstOrDefault().Value);
        return count;
    }

    /// <summary>
    /// 获取多条主表的信息
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static IEnumerable<XElement> GetMainRows(string xml)
    {
        XElement xmlStr = XElement.Parse(xml);
        IEnumerable<XElement> mainXml = (from e in xmlStr.Descendants("Query")
                                         select e
                           );
        return mainXml;
    }


    /// <summary>
    /// 获取明细表的信息
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static IEnumerable<XElement> GetDetailsInfo(string xml)
    {
        XElement xmlStr = XElement.Parse(xml);
        //解析明细
        IEnumerable<XElement> detailsXML = (from e in xmlStr.Descendants("data_request").Elements("datainfo").Elements("parameter").Elements("data").Elements("row").Elements("detail").Elements("row")
                                            select e
                      )
                     ;
        return detailsXML;
    }

    /// <summary>
    /// 获取请求id
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static string GetRequestId(string xml)
    {
        XElement xmlStr = XElement.Parse(xml);
        //判断请求的第一个节点是否是request,且包含属性
        if (xmlStr.Name == "request" && xmlStr.HasAttributes)
        {
            //返回其属性id的值
            if (xmlStr.Attribute("id") != null)
            {
                return xmlStr.Attribute("id").Value;
            }
        }
        else
        {
            //第一个节点不是request，则往下寻找request节点，并返回ID
            var requestNode = xmlStr.Descendants("request").FirstOrDefault();
            if (requestNode != null && xmlStr.HasAttributes)
            {
                if (requestNode.Attribute("id") != null)
                {
                    return requestNode.Attribute("id").Value;
                }
            }
        }
        return string.Empty;
    }

    /// <summary>
    /// 返回给ERP接口的XML信息
    /// </summary>
    /// <param name="requestId">请求id</param>
    /// <param name="status">0代表成功，1代表失败</param>
    /// <param name="msg">如果生成成功，则是对应的单号，如果失败，则是失败的具体信息</param>
    /// <returns></returns>
    public static string ReturnMSG(string requestId, string status, string msg)
    {

        //string resultXML = string.Format(@"<response><code>019</code><message>平台返回同步结果</message><reqid>{0}</reqid><srvver>2.0</srvver><srvcode>000</srvcode><payload><param key=""std_data"" type=/""xml/""><data_response><execution><status code=""0"" description="""" sql_code=""0""/></execution><datainfo><parameter key=""Status"" type=""string"">{1}</parameter><parameter key=""Message"" type=""string"">{2}</parameter></datainfo></data_response></param></payload></response>",requestId, status, msg);
        string resultXML = string.Format(@"<response><srvver>2.0</srvver><srvcode>000</srvcode><payload><param key=""std_data"" type=""xml""><data_response><execution><status code=""{0}"" sql_code=""{0}"" description=""{1}"" /></execution></data_response></param></payload></response>", status, msg);
        return resultXML;
    }
    #endregion
}
