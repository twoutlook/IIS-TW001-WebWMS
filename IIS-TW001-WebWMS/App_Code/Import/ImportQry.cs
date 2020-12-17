using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// ImportQry 的摘要说明
/// </summary>
public class ImportQry
{
    public ImportQry()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public DataTable GetAllWareHouseTempList()
    {
        string strSQL = @" select
cwareid 仓库编码,
cwarename 仓库名称,
CDEFINE2 仓库类型,
leadercode 供应商编码,
leader 供应商名称,
leaderphone 电话,
cstatus 状态,
bonded 是否保税仓,
CDEFINE1 是否良品仓,
companyno 企业编号,
strongholdno 据点编号,
organizationno 应用组织,
cmemo 备注
from BASE_WAREHOUSE_TEMP t
where t.flag=1 ";
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        return dt;
    }

    public DataTable GetAllPartTempList()
    {
        string strSQL = @" select
CPARTNUMBER 料号,
CPARTNAME 品名,
CTYPE 类型,
MTYPE 类别,
CERPCODE ERP编码,
ICW 毛重,
INW 净重,
CUNITS 重量单位,
BASEUNIT 单位,
REFERUNIT 参考单位,
ILENGTH 长,
IWIDTH 宽,
IHEIGHT 高,
CVOLUME 材积,
VOLUMEUNITS 材积单位,
DEXPIREDATE 终止日期,
CSTATUS 状态,
BONDED 是否保税,
INEEDCHECK 是否免检,
INEEDWARN 是否预警 ,
CDEFAULTWARE 默认仓库,
CDEFAULTCARGO 默认储位,
CDEFAULTVENDOR 默认供应商,
CINRULE 上架规则,
COUTRULE 下架规则,
CBARRULE 条码规则,
CVERSION 版本,
COMPANYNO 企业编号,
STRONGHOLDNO 据点编号,
ORGANIZATIONNO 应用组织,
CUSETYPE 用途,
CMEMO 备注,
CALIAS 助记码,
CSPECIFICATIONS 规格
from BASE_PART_TEMP t where t.flag=1";
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        return dt;
    }

//    public DataTable GetAllAreaTempList()
//    {
//        string strSQL = @" select areaname       区域名称,
//       handover_cargo 备料储位编号,
//       flag           是否超发,
//       is_control     是否控制区域,
//       memo           备注
//  from temp_base_area where error_flag = 1";
//        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
//        return dt;
//    }
    public DataTable GetAllAreaTempList()
    {
        string strSQL = @" SELECT AREANAME AS 区域名称, BACKUPCPOSITIONCODE AS 备料储位编号, BACKUPCPOSITION AS 备料储位名称, 
                FLAG AS 是否超发 FROM dbo.TEMP_BASE_AREA WHERE issave = 1";
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        return dt;
    }
    public DataTable GetAllCarGoSpaceTempList()
    {
        string strSQL = @" select
CPOSITIONCODE 储位编码 ,
CPOSITION 储位名称 ,
IMAXCAPACITY 最大量 ,
CALIAS 助记码 ,
CERPCODE ERP编码 ,
CTYPE 种类 ,
CDEFINE1 区域 ,
WAREHOUSEID 所属仓库ID ,
IPRIORITY 优先级 ,
ILENGTH 长 ,
IWIDTH 宽 ,
IHEIGHT 高 ,
IVOLUME 体积 ,
CUSETYPE 用途 ,
IPERMITMIX 是否允许混放 ,
CX X ,
CY Y ,
CZ Z ,
WEIGHT 重量 ,
WEIGHTUNIT 重量单位 ,
VOLUME 材积 ,
VOLUMEUNIT 材积单位 ,
DEXPIREDATE 终止日期 ,
CSTATUS 状态 ,
IS_ALLO 是否允许调拨 ,
CMEMO 备注 
from BASE_CARGOSPACE_TEMP t where t.flag=1;";
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        return dt;
    }




    public DataTable GetAllStockTempList()
    {
        string strSQL = @" select  CWAREHOUSECODE '仓库编码',CPOSITIONCODE '储位编码', CINVCODE '物料编号',IQTY '数量' from TEMP_BASE_STOCK_CURRENT where error_flag=1 ";
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        return dt;
    }
    public DataTable GetVENDORempList()
    {
        string strSQL = @" select CVENDORID 供应商编码,
CVENDOR 供应商名称,
CSTATUS 状态,
CCONTACTPERSON 联系人,
CPHONE 联系电话,
CADDRESS 联系地址,
CTNPE 供应商类型,
CALIAS 助记码,
ILEVEL 级别,
CMEMO 备注,
CERPCODE ERP编码 from TEMP_BASE_VENDOR where issave=1";
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        return dt;
    }
    public DataTable GetCLIENTTempList()
    {
        string strSQL = @" select CCLIENTID 客户编码,
CCLIENTNAME 客户名称,
CSTATUS 状态,
CCONTACTPERSON 联系人,
CPHONE 联系电话,
CADDRESS 联系地址,
CTYPE 客户类型,
CALIAS 助记码,
ILEVER 级别,
CMEMO 备注,
CERPCODE ERP编码 from TEMP_BASE_CLIENT where issave=1";
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        return dt;
    }


}