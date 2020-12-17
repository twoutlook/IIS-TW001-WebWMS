using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using DreamTek.ASRS.Business.Base;
using DreamTek.ASRS.Business.SP.ProcedureModel;

/// <summary>
/// WmsDBCommon_ASRS 的摘要说明
/// </summary>
public class WmsDBCommon_ASRS
{
    private static DBContext context = new DBContext();
    private static SqlConnection DBConn = new SqlConnection();
    private static InBill inBill = new InBill();

	public WmsDBCommon_ASRS()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
       
	}

    /// 获取配置表中的值
    /// <summary>
    /// 获取配置表中的值
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string GetConFig(string code)
    {
        string val = string.Empty;
        try
        {
            string Sql = string.Format(@"select fi.config_value from sys_config fi where fi.code='{0}' ", code);
            val = DBHelp.ExecuteScalar(Sql).ToString();
        }
        catch { }
        return val;
    }

    #region 入库
    //入库
    public static bool ASRS_InChangeStatus(string ids, string str_Space, out string errmsg, string LineId, string filepath = "")
    {
        try
        {
            errmsg = "";
            bool dbflag = false;
            string inbillstatus = string.Empty;
            int WmstskId = 0;
            int intNum = 0;

            if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
            {
                dbflag = true;
            }
            else
            {
                return false;
            }

            //pan gao 20160530
            //如果没有先做出库动作，直接做入库操作，则提示不可以入库，要先做出库动作
            //if (GetCargospacePALLET_CODE(ids))
            //{
            //    errmsg = "该储位内侧位置有物，请先出库！";
            //    DBConnClose();
            //    return false;
            //}

            if (!HasEmptyCargo())
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg1 + "!"; //"必须有2个空储位且没有栈板！";
                return false;
            }
            // WL 20160516
            if (GetInbillPallet(ids) == "0")
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg2 + "!";//"站点选择错位，请重新选择";
                DBConnClose();
                return false;
            }
            else
            {
                str_Space = GetInbillPallet(ids);
            }

            //测试连接成功
            if (dbflag)
            {
                 string IsOnlyRun = GetConFig("300002");
                 if (IsOnlyRun == "1")
                 {
                if (CheckStatusCmd_Mst())
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg3 + "!";//"有正在执行的AS/RS命令，请稍后再试！";
                    DBConnClose();
                    return false;
                }
                 }
                 bool ingCmd = CheckPositionUsed(ids, "IN");
                 if (ingCmd)
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg4 + "!";////"当前储位或相邻储位有正在执行未完成的AS/RS命令!";
                     return false;
                 }
                 string figvalue = GetConFig("800001");//0:默认物料仓  1:模具仓
                 string cmdno = Getcmdno();
                 if (!InQuantityCheck(ids) && figvalue == "0")
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg5 + "!";//"入库单中的数量与明细中的数量不一致!";
                    DBConnClose();
                    return false;
                }

                //判断入库单状态，如果是0，则插入数据至中间表
                inbillstatus = WmsDBCommon_ASRS.GetInbillStatus(ids);
                if (inbillstatus.Equals("0"))
                {
                    #region 未处理
                    WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                    IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.ids == ids
                                   select p;
                    INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                    if (string.IsNullOrEmpty(entity.cposition) || string.IsNullOrEmpty(entity.iquantity.ToString()))
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg6 + "!"; // //"储位或数量为空，不能入库";
                        DBConnClose();
                        return false;
                    }
                    //if (figvalue == "1" && (string.IsNullOrEmpty(entity.LINE) || string.IsNullOrEmpty(entity.pallet_code.ToString())))
                    //{
                    //    errmsg = "线别或站点为空，不能入库";
                    //    return false;
                    //}
                    string Loc = "";

                    Loc = GetLocXYZ(entity.cpositioncode.Trim());
                    if (Loc == "" || Loc.Length < 7)
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg7 + "!";//"未配置储位的X或Y或Z";
                        DBConnClose();
                        return false;
                    }
                    //种类
                    string LocSize = "";
                    LocSize = GetLocSize(entity.cpositioncode.Trim()).Trim();
                    if (string.IsNullOrEmpty(LocSize))
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg8 + "!"; //"未配置储位的种类";
                        DBConnClose();
                        return false;
                    }
                    string CmdSno = GetCmdSno();
                  
                    // 判断相邻储位是否有阻挡[判断Y01,Y02上是否有阻挡]
                    string strCposition = new InBill().GetStopCurrent_RD(ids);
                    if (strCposition != "")
                    {
                        // 获取阻碍储位的Y的值
                        string strCy = new InBill().strGetCY(strCposition);

                        string strTempCpositon = ""; // 临时储位TEMP000
                        string strTempLoc = ""; //临时储位LOC
                        string strStopLoc = ""; //对面阻止的储位

                        strStopLoc = GetLocXYZ(strCposition);
                        string CZ = strStopLoc.Substring(5, 2);
                        // 判断是否会被阻挡
                        // WL 20160524
                        //if (GetCmd_Loc(strStopLoc, out errmsg))
                        //{
                        //    DBConnClose();
                        //    return false;
                        //}

                        // 判断相邻储位是否可以移动
                        DataTable dt = inBill.GetAllCurrentByNull(strCy);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int min = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                int width = Convert.ToInt32(strStopLoc.Substring(2, 3)) - Convert.ToInt32(dt.Rows[i]["CY"].ToString());
                                int height = Convert.ToInt32(strStopLoc.Substring(strStopLoc.Length - 2)) - Convert.ToInt32(dt.Rows[i]["CZ"].ToString());
                                int result = (width * width) + (height * height);
                                result = Convert.ToInt32(Math.Sqrt(result));
                                if (min == 0)
                                {
                                    min = result;
                                    strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                                }
                                if (result < min)
                                {
                                    min = result;
                                    strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(strTempCpositon))
                        {
                            errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg9 + "!"; //"没有多余的移动储位,阻挡储位无法移动！";

                            return false;
                        }
                        strTempLoc = GetLocXYZ(strTempCpositon);
                        
                        //strTempLoc = "0200203";

                        // 移动
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strStopLoc, strTempLoc, LocSize, 5, str_Space,LineId,cmdno, out errmsg))
                        {
                            //DBConnClose();
                            UpdateCmd_Pattle_I(strTempLoc);
                            UpdateCmd_Pattle_O(strStopLoc);
                            intNum++;
                        }

                        if (new InBill().CheckBASE_CARGOSPACE_OK(ids))//有栈板先出库
                        {
                            // 出库
                            CmdSno = GetCmdSno();
                            WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                            if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 2, str_Space, LineId, cmdno, out errmsg))
                            {
                                intNum++;
                                UpdateCmd_Pattle_O(Loc);
                            }
                        }

                        // 入库
                        CmdSno = GetCmdSno();
                        WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space, LineId, cmdno, out errmsg))
                        {
                            //DBConnClose();
                            UpdateCmd_Pattle_I(Loc);
                            intNum++;
                        }
                        //pan gao
                        //判断储位上是否有物品，如果有，再把移动过去的棧板再移动回来。
                        if (!new InBill().CheckStockByStopLoc(strStopLoc))
                        {
                            string strStopPostion = GetPositionByXYZ(strStopLoc);
                            string strTempPostion = GetPositionByXYZ(strTempLoc);
                            new InBill().UpdatePostionForStop("IN", ids, strStopPostion, strTempPostion);
                            // 返回
                            //CmdSno = GetCmdSno();
                            //WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                            //if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strTempLoc, strStopLoc, LocSize, 5, str_Space, out errmsg))
                            //{
                            //    UpdateCmd_Pattle_I(strStopLoc);
                            //    UpdateCmd_Pattle_O(strTempLoc);
                            //}
                            //else
                            //{
                            //    DBConnClose();
                            //    return false;
                            //}

                            PROC_AUTO_ALLOCATE prc = new PROC_AUTO_ALLOCATE();
                            prc.P_PositonFrom = strCposition;
                            prc.P_PositonTo = strTempCpositon;
                            prc.P_UserNo = "WMS";
                            prc.Execute();
                            }
                        entity.asrs_status = 1;
                        entity.wmstskid = WmstskId;
                        entity.asrs_num = 3;
                        con.Update(entity);
                        con.Save();
                    }
                    else
                    {
                        if (!new InBill().CheckBASE_CARGOSPACE_OK(ids))//有栈板先出库
                        {
                            // 出库
                            CmdSno = GetCmdSno();
                            WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                            if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 2, str_Space, LineId, cmdno, out errmsg))
                            {
                                intNum++;
                                UpdateCmd_Pattle_O(Loc);
                            }
                        }
                        //入库
                        CmdSno = GetCmdSno();
                        WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space, LineId, cmdno, out errmsg))
                        {
                            entity.asrs_status = 1;
                            entity.wmstskid = WmstskId;
                            entity.asrs_num = 0;
                            con.Update(entity);
                            con.Save();
                            UpdateCmd_Pattle_I(Loc);
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                    }
                    DBConnClose();
                    #endregion
                }
                else if (inbillstatus.Equals("1"))
                {
                    #region 运作中
                    IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.ids == ids
                                   select p;
                    INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                    WmstskId = Convert.ToInt32(entity.wmstskid);
                    string mststatus = string.Empty;

                    if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                    {
                        mststatus = errmsg;
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }

                    if (mststatus.Equals("0"))
                    {
                        //entity.ASRS_NUM = 0;
                        // 回滚处理
                        if (entity.asrs_num > 0)
                        {
                            // 第二次判断
                            string mststatus2 = string.Empty;
                            if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - 2, out errmsg))
                            {
                                mststatus2 = errmsg;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                            if (!mststatus2.Equals("0"))
                            {

                                //更新inbill_d状态
                                //entity.ASRS_STATUS = Convert.ToInt32(mststatus2);
                                con.Update(entity);
                                con.Save();
                                DBConnClose();
                                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!"; //"该明细状态是[执行中]，不能取消";
                                return false;
                            }

                            for (int i = 1; i < entity.asrs_num; i++)
                            {
                                if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId - i, out errmsg))
                                {
                                    //  
                                }
                            }
                            if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                            {
                                entity.wmstskid = null;
                                entity.asrs_status = 0;
                                entity.asrs_num = 0;
                                con.Update(entity);
                                con.Save();
                                DBConnClose();
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }

                        }
                        else
                        {
                            //删除原中间表状态，更新inbill表状态
                            if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                            {
                                //WL 20160516
                                UpdateCmd_Pattle_O(entity.cpositioncode);
                                //entity.PALLET_CODE = "0";
                                // --END -- 

                                entity.wmstskid = null;
                                entity.asrs_status = 0;
                                entity.asrs_num = 0;
                                con.Update(entity);
                                con.Save();
                                // 栈板取消
                                DBConnClose();
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //更新inbill_d状态
                        entity.asrs_status = Convert.ToInt32(mststatus);
                        con.Update(entity);
                        con.Save();
                        DBConnClose();
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!"; //"该明细状态是[执行中]，不能取消";
                        return false;
                    }

                    #endregion
                }
                else if (inbillstatus.Equals("8"))
                {
                    #region 处理异常
                    IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.ids == ids
                                   select p;
                    INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                    //处理异常
                    WmstskId = Convert.ToInt32(entity.wmstskid);
                    //string mststatus = DBCommon_ASRS.GetCmd_MstStatus(WmstskId);
                    string mststatus = string.Empty;

                    if (entity.asrs_num == 0)
                    {
                        if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                        {
                            mststatus = errmsg;
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }

                        if (mststatus.Equals("8"))
                        {
                            if (WmsDBCommon_ASRS.UpdateCmd_mst(WmstskId, out errmsg))
                            {
                                entity.asrs_status = 1;
                                con.Update(entity);
                                con.Save();
                                DBConnClose();
                                return true;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                    }

                    else
                    {
                        for (int i = 0; i < entity.asrs_num; i++)
                        {
                            if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - i, out errmsg))
                            {
                                mststatus = errmsg;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }

                            if (mststatus.Equals("8"))
                            {
                                if (WmsDBCommon_ASRS.UpdateCmd_mst(WmstskId - i, out errmsg))
                                {
                                    entity.asrs_status = 1;
                                    con.Update(entity);
                                    con.Save();
                                    DBConnClose();
                                    return true;
                                }
                                else
                                {
                                    DBConnClose();
                                    return false;
                                }
                            }
                        }
                    }
                    DBConnClose();
                    return true;
                    #endregion
                }
                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception err)
        {
            errmsg = err.Message;
            DBConnClose();
            return false;
        }
    }

    /// <summary>
    /// 判断当前使用的储位及阻挡储位，是否有未完成的命令
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="InOrOut">IN,OUT</param>
    /// <returns></returns>
    public static bool CheckPositionUsed(string ids, string InOrOut)
    {
        bool bl = false;
        string currentLoc = string.Empty;//当前操作储位的xyz
        string stopLoc = string.Empty;//阻挡的xyz
        string CX = string.Empty;
        string inSql = string.Empty;
        string sql = @"
                        select count(1) from cmd_mst t
                        where t.cmdsts in ('0','1') and
                        (
                        t.loc in ({0})
                        or 
                        t.newloc in ({1})
                        )
                        ";

        if (InOrOut.ToUpper().Equals("IN"))
        {
            //入库
            //获取入库储位的xyz
            string clSql = string.Format(@"
                                            select bc.cx+bc.cy+bc.cz as loc from base_cargospace bc 
                                            where bc.cpositioncode 
                                            in
                                            (
                                            select top 1 d.cpositioncode from inbill_d d where d.ids='{0}'
                                            )
                                           ", ids);
            object loc = DBHelp.ExecuteScalar(clSql);
            if (loc != null) currentLoc = loc.ToString();
        }
        else if (InOrOut.ToUpper().Equals("OUT"))
        {
            //出库
            //获取出库储位的xyz
            string clSql = string.Format(@"
                                            select bc.cx+bc.cy+bc.cz as loc from base_cargospace bc 
                                            where bc.cpositioncode 
                                            in
                                            (
                                            select top 1 d.cpositioncode from outbill_d d where d.ids='{0}'
                                            )
                                           ", ids);
            currentLoc = DBHelp.ExecuteScalar(clSql).ToString();
        }

        if (!string.IsNullOrEmpty(currentLoc))
        {
            CX = currentLoc.Substring(0, 2);
            //X坐标是03,04时才会被阻挡
            if (CX.Equals("03"))
            {
                stopLoc = "01" + currentLoc.Substring(2);
            }
            else if (CX.Equals("04"))
            {
                stopLoc = "02" + currentLoc.Substring(2);
            }
        }

        if (!string.IsNullOrEmpty(currentLoc) && !string.IsNullOrEmpty(stopLoc))
        {
            //有阻挡
            inSql = string.Format("'{0}','{1}'", currentLoc, stopLoc);
        }
        else
        {
            //没有阻挡
            inSql = string.Format("'{0}'", currentLoc);
        }

        string countSql = string.Format(sql, inSql, inSql);
        int i = int.Parse(DBHelp.ExecuteScalar(countSql).ToString());
        if (i > 0)
        {
            bl = true;
        }
        return bl;
    }

    //入库刷新单项
    public static bool ASRS_InRefresh(string ids, out string errmsg)
    {
        try
        {
            errmsg = "";

            // NOTE by Mark, 09/17 16:12 小仲 and Mark
            //bool dbflag = false;
            bool dbflag = true;


            string inbillstatus = string.Empty;
            int WmstskId = 0;
            IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
            //var caseList = from p in con.Get()
            //               where p.ids == ids
            //               select p;
            //INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
            var entity = (from p in con.Get()
                          where p.ids == ids
                          select p).ToList().FirstOrDefault<INBILL_D>();
            if (string.IsNullOrEmpty(entity.wmstskid.ToString()))
            {
                return true;
            }

            // NOTE by Mark, 09/17 16:12 小仲 and Mark
            //if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
            //{
            //    dbflag = true;
            //}
            //else
            //{
            //    return false;
            //}
            if (dbflag)
            {
                //INBILL_DEntity entity = new INBILL_DEntity();
                //entity.IDS = ids;
                //entity.SelectByPKeys();
                WmstskId = Convert.ToInt32(entity.wmstskid);
                //string mststatus = DBCommon_ASRS.GetCmd_MstStatus(WmstskId);
                string mststatus = string.Empty;
                if (entity.asrs_num==null || entity.asrs_num == 0)
                {
                    if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                    {
                        mststatus = errmsg;
                        if (mststatus != "0")
                        {
                            entity.asrs_status = Convert.ToInt32(mststatus);
                            con.Update(entity);
                            con.Save();
                        }

                        // NOTE by Mark, 09/17 16:12 小仲 and Mark
                        //DBConnClose();
                        return true;
                    }
                    else
                    {
                        // NOTE by Mark, 09/17 16:12 小仲 and Mark
                        //DBConnClose();
                        return false;
                    }
                }
                else
                {

                    for (int i = 0; i < entity.asrs_num; i++)
                    {
                        if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - i, out errmsg))
                        {
                            mststatus = errmsg;
                            if (mststatus != "0")
                            {
                                if (mststatus == "7")
                                {
                                    if (i == 0)
                                    {
                                        entity.asrs_status = Convert.ToInt32(mststatus);
                                        con.Update(entity);
                                        con.Save();

                                        // NOTE by Mark, 09/17 16:12 小仲 and Mark
                                        //DBConnClose();
                                        return true;
                                    }
                                    else
                                    {
                                        entity.asrs_status = Convert.ToInt32(1);
                                        con.Update(entity);
                                        con.Save();

                                        // NOTE by Mark, 09/17 16:12 小仲 and Mark
                                        //DBConnClose();
                                        return true;
                                    }
                                }
                                else
                                {
                                    entity.asrs_status = Convert.ToInt32(mststatus);
                                    con.Update(entity);
                                    con.Save();

                                    // NOTE by Mark, 09/17 16:12 小仲 and Mark
                                    //DBConnClose();
                                    return true;
                                }
                            }
                        }

                    }
                }

                // NOTE by Mark, 09/17 16:12 小仲 and Mark
                //DBConnClose();
                return true;

            }
            else
            {
                return false;
            }
        }
        catch (Exception err)
        {
            errmsg = err.Message;
            return false;
        }
    }
    public static bool ASRS_AlloCancel(string ids, out string errmsg)
    {
        try
        {
            errmsg = "";
            bool dbflag = true;
            string allostatus = string.Empty;
            int WmstskId = 0;
            //判断调拨单状态，如果是0，则插入数据至中间表
            allostatus = WmsDBCommon_ASRS.GetAlloStatus(ids);
            if (allostatus.Equals("1"))
            {
                #region 运作中

                IGenericRepository<ALLOCATE_D> conn = new GenericRepository<ALLOCATE_D>(context);
                var entity = ( from p in conn.Get()
                               where p.ids == ids
                               select p ).FirstOrDefault();


                //ALLOCATE_DEntity entity = new ALLOCATE_DEntity();
                //entity.IDS = ids;
                //entity.SelectByPKeys();
                WmstskId = Convert.ToInt32(entity.wmstskid);
                string mststatus = string.Empty;
                if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                {
                    mststatus = errmsg;
                }
                else
                {
                    // 
                    return false;
                }

                if (mststatus.Equals("0"))
                {
                    //entity.ASRS_NUM = 0;
                    // 回滚处理
                    if (entity.asrs_num > 0)
                    {
                        // 第二次判断
                        string mststatus2 = string.Empty;
                        if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - (Convert.ToInt32(entity.asrs_num) - 1), out errmsg))
                        {
                            mststatus2 = errmsg;
                        }
                        else
                        {

                            return false;
                        }
                        if (!mststatus2.Equals("0"))
                        {

                            //更新inbill_d状态
                            //entity.ASRS_STATUS = Convert.ToInt32(mststatus2);
                            //ALLOCATE_DRule.Update(entity);
                            conn.Update(entity);
                            conn.Save();
                            errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!";//"该明细状态是[执行中]，不能取消";
                            return false;
                        }

                        for (int i = 1; i < entity.asrs_num; i++)
                        {
                            if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId - i, out errmsg))
                            {
                                //  
                            }
                        }
                        if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                        {
                            //entity.wmstskid = "0";
                            entity.asrs_status = 0;
                            entity.asrs_num = 0;
                            //ALLOCATE_DRule.Update(entity);
                            conn.Update(entity);
                            conn.Save();

                        }
                        else
                        {

                            return false;
                        }

                    }
                    else
                    {
                        //删除原中间表状态，更新inbill表状态
                        if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                        {
                            //entity.wmstskid = 0;
                            entity.asrs_status = 0;
                            entity.asrs_num = 0;
                            //ALLOCATE_DRule.Update(entity);
                            conn.Update(entity);
                            conn.Save();

                        }
                        else
                        {

                            return false;
                        }
                    }
                    /*
                    //删除原中间表状态，更新ALLOCATE_D表状态
                    if (OracleDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                    {
                        entity.WMSTSKID = "";
                        entity.ASRS_STATUS = 0;
                        ALLOCATE_DRule.Update(entity);
                        // 
                    }
                    else
                    {
                        // 
                        return false;
                    }*/
                }
                else
                {
                    //更新ALLOCATE_D状态
                    entity.asrs_status = Convert.ToInt32(mststatus);
                    //ALLOCATE_DRule.Update(entity);
                    // 
                    conn.Update(entity);
                    conn.Save();
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!";//"该明细状态是[执行中]，不能取消";
                    return false;
                }
                #endregion
            }

        }
        catch (Exception err)
        {
            errmsg = err.Message;
            //
            return false;
        }

        return true;
    }

    //调拨刷新单项
    public static bool ASRS_AlloRefresh(string ids, out string errmsg)
    {
        try
        {
            errmsg = "";
            bool dbflag = true;
            string inbillstatus = string.Empty;
            int WmstskId = 0;

            IGenericRepository<ALLOCATE_D> conn = new GenericRepository<ALLOCATE_D>(context);
            ALLOCATE_D entity = (from p in conn.Get()
                           where p.ids == ids
                           select p).ToList().FirstOrDefault();


            //ALLOCATE_DEntity entity = new ALLOCATE_DEntity();
            //entity.IDS = ids;
            //entity.SelectByPKeys();
            if (!entity.wmstskid.HasValue || string.IsNullOrEmpty(entity.wmstskid.Value.ToString()))
            {
                return true;
            }

            if (dbflag)
            {
                /*
                WmstskId = Convert.ToInt32(entity.WMSTSKID);
                string mststatus = string.Empty;
                if (OracleDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                {
                    mststatus = errmsg;
                    if (mststatus != "0")
                    {
                        entity.ASRS_STATUS = Convert.ToInt32(mststatus);
                        ALLOCATE_DRule.Update(entity);
                    }

                        
                    return true;
                }
                else
                {
                        
                    return false;
                }*/
                WmstskId = Convert.ToInt32(entity.wmstskid.Value);
                string mststatus = string.Empty;
                //entity.ASRS_NUM = 1;
                for (int i = 0; i < entity.asrs_num; i++)
                {
                    if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - i, out errmsg))
                    {
                        mststatus = errmsg;
                        if (mststatus != "0")
                        {
                            if (mststatus == "7")
                            {
                                if (i == 0)
                                {
                                    entity.asrs_status = Convert.ToInt32(mststatus);

                                    //ALLOCATE_DRule.Update(entity);
                                    conn.Update(entity);
                                    conn.Save();

                                    //IGenericRepository<ALLOCATE> connM = new GenericRepository<ALLOCATE>(context);
                                    //var main = (from p in connM.Get()
                                    //              where p.id == entity.id
                                    //              select p).FirstOrDefault();

                                    ////ALLOCATEEntity main = new ALLOCATEEntity();
                                    //main.cstatus = "0";
                                    //connM.Update(main);
                                    //connM.Save();

                                    return true;
                                }
                                else
                                {
                                    entity.asrs_status = Convert.ToInt32(1);
                                    //ALLOCATE_DRule.Update(entity);
                                    conn.Update(entity);
                                    conn.Save();
                                    return true;
                                }
                            }
                            else
                            {
                                entity.asrs_status = Convert.ToInt32(mststatus);
                                //ALLOCATE_DRule.Update(entity);
                                conn.Update(entity);
                                conn.Save();
                                return true;
                            }
                        }
                    }

                }

                return true;

            }
            else
            {
                return false;
            }
        }
        catch (Exception err)
        {
            errmsg = err.Message;
            return false;
        }
    }


    //获取入库单明细asrs状态
    public static string GetInbillStatus(string ids)
    {
        IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
        var caseList = from p in con.Get()
                       where p.ids == ids
                       select p;
        return  caseList.ToList().FirstOrDefault<INBILL_D>().asrs_status.ToString();
    }

    //获取入库单明细选择的站点
    public static string GetInbillPallet(string ids)
    {
        IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
        var caseList = from p in con.Get()
                       where p.ids == ids && p.pallet_code!=null
                       select p;
        if (caseList.Count() > 0)
            return caseList.ToList().FirstOrDefault<INBILL_D>().pallet_code.ToString();
        else
            return "";
    }

    //如果没有先做出库动作，直接做入库操作，则提示不可以入库，要先做出库动作
    //判断目标储位是否有栈板
    public static bool GetCargospacePALLET_CODE(string ids)
    {
        bool bl = false;
        string Sql = string.Format(@" select isnull(bc.pallet_code,0) from inbill_d bd 
                                             left join base_cargospace bc on bd.cpositioncode = bc.cpositioncode
                                             where bd.ids='{0}' ", ids);
        string code = DBHelp.ExecuteScalar(Sql).ToString();
        if (string.IsNullOrEmpty(code) || code == "0")
            return false;
        else return true;
    }

    //入库刷新全部
    public static bool ASRS_InRefresh_All(string Id, out string errmsg)
    {
        try
        {
            errmsg = "";
            bool dbflag = false;
            //打开SQL连接
            if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
            {
                dbflag = true;
            }
            else
            {
                return false;
            }
            //INBILL_DEntity entity = new INBILL_DEntity();
            //entity.IDS = Id;
            //entity.SelectByPKeys();
            //if (string.IsNullOrEmpty(entity.WMSTSKID))
            //{
            //    return true;
            //}
            //if (DBCommon_ASRS.DataBaseConn(out errmsg))
            //{
            //    dbflag = true;
            //}
            //else
            //{
            //    return false;
            //}
            if (dbflag)
            {

                DataTable tb = GetInBill_D(Id);
                if (tb != null && tb.Rows.Count > 0)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        int asrs_num = Convert.ToInt32(tb.Rows[i]["asrs_num"].ToString());
                        if (asrs_num < 2)
                        {
                            string tskid = tb.Rows[i]["WMSTSKID"].ToString();
                            if (string.IsNullOrEmpty(tskid) == false)
                            {
                                string msg = string.Empty;
                                //tskid 不为空
                                if (WmsDBCommon_ASRS.GetCmd_MstStatus(Convert.ToInt32(tskid), out msg))
                                {
                                    if (msg != tb.Rows[i]["ASRS_STATUS"].ToString() && msg != "0")
                                    {
                                        IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                                        var caseList = from p in con.Get()
                                                       where p.ids == tb.Rows[i]["IDS"].ToString()
                                                       select p;
                                        INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();                                
                                        entity.asrs_status = Convert.ToInt32(msg);
                                        con.Update(entity);
                                        con.Save();
                                        //关闭
                                        DBConnClose();
                                        return true;
                                    }
                                }

                            }
                        }
                        else
                        {
                            string tskid = tb.Rows[i]["WMSTSKID"].ToString();
                            if (string.IsNullOrEmpty(tskid) == false)
                            {
                                string msg = string.Empty;

                                for (int j = 0; j < asrs_num; j++)
                                {
                                    //tskid 不为空
                                    if (WmsDBCommon_ASRS.GetCmd_MstStatus(Convert.ToInt32(tskid) - j, out msg))
                                    {
                                        if (msg != tb.Rows[i]["ASRS_STATUS"].ToString() && msg != "0")
                                        {
                                            if (msg == "7")
                                            {
                                                if (j == 0)
                                                {
                                                    IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                                                    var caseList = from p in con.Get()
                                                                   where p.ids == tb.Rows[i]["IDS"].ToString()
                                                                   select p;
                                                    INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                                                    entity.asrs_status = Convert.ToInt32(msg);
                                                    con.Update(entity);
                                                    con.Save();
                                                    //关闭
                                                    DBConnClose();
                                                    return true;
                                                }
                                                else
                                                {
                                                    IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                                                    var caseList = from p in con.Get()
                                                                   where p.ids == tb.Rows[i]["IDS"].ToString()
                                                                   select p;
                                                    INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                                                    entity.asrs_status = Convert.ToInt32(1);
                                                    con.Update(entity);
                                                    con.Save();
                                                    //关闭
                                                    DBConnClose();
                                                    return true;
                                                }
                                            }
                                            else
                                            {
                                                IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                                                var caseList = from p in con.Get()
                                                               where p.ids == tb.Rows[i]["IDS"].ToString()
                                                               select p;
                                                INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                                                entity.asrs_status = Convert.ToInt32(msg);
                                                con.Update(entity);
                                                con.Save();
                                                //关闭
                                                DBConnClose();
                                                return true;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                //关闭
                DBConnClose();
                return true;
                /*
                int WmstskId = 0;
                WmstskId = Convert.ToInt32(entity.WMSTSKID);
                   
                string mststatus = string.Empty;
                for (int i = 0; i < entity.ASRS_NUM; i++)
                {
                    if (DBCommon_ASRS.GetCmd_MstStatus(WmstskId - i, out errmsg))
                    {
                        mststatus = errmsg;
                        if (mststatus != "0")
                        {
                            if (mststatus == "7")
                            {
                                if (i == 0)
                                {
                                    entity.ASRS_STATUS = Convert.ToInt32(mststatus);
                                    INBILL_DRule.Update(entity);
                                    DBConnClose();
                                    return true;
                                }
                                else
                                {
                                    entity.ASRS_STATUS = Convert.ToInt32(1);
                                    INBILL_DRule.Update(entity);
                                    DBConnClose();
                                    return true;
                                }
                            }
                            else
                            {
                                entity.ASRS_STATUS = Convert.ToInt32(mststatus);
                                INBILL_DRule.Update(entity);
                                DBConnClose();
                                return true;
                            }
                        }
                    }

                }
                DBConnClose();
                return true;*/

            }
            else
            {
                return false;
            }
        }
        catch (Exception err)
        {
            errmsg = err.Message;
            return false;
        }
    }

    //入库按钮选择
    public static bool ASRS_InBill_All(string ids, string str_Space, string LineId, out string errmsg)
    {

        try
        {
            errmsg = "";
            bool dbflag = true;
            string inbillstatus = string.Empty;
            int WmstskId = 0;
            //判断入库单状态，如果是0，则插入数据至中间表
            inbillstatus = WmsDBCommon_ASRS.GetInbillStatus(ids);
            if (inbillstatus.Equals("0"))
            {
                string cmdno = Getcmdno();
                #region 未处理
                WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                var caseList = from p in con.Get()
                               where p.ids == ids
                               select p;
                INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                if (string.IsNullOrEmpty(entity.cpositioncode) || string.IsNullOrEmpty(entity.iquantity.ToString()))
                {
                    errmsg = "[" + entity.cinvcode + "]" + Resources.Lang.WmsDBCommon_ASRS_Msg6 + "!"; 
                    //"]储位或数量为空，不能入库";
                    return false;
                }
                string Loc = "";

                Loc = GetLocXYZ(entity.cpositioncode.Trim());
                if (Loc == "" || Loc.Length < 6)
                {
                    errmsg = "[" + entity.cinvcode + "]" + Resources.Lang.WmsDBCommon_ASRS_Msg7 + "!"; 
                    //"]未配置储位的X或Y或Z";
                    return false;
                }
                //种类
                string LocSize = "";
                LocSize = GetLocSize(entity.cpositioncode.Trim()).Trim();
                if (string.IsNullOrEmpty(LocSize))
                {
                    errmsg = "[" + entity.cinvcode + "]" + Resources.Lang.WmsDBCommon_ASRS_Msg8 + "!"; 
                    //"]未配置储位的种类";
                    return false;
                }
                string CmdSno = GetCmdSno();

                // 判断相邻储位是否有阻挡[判断Y01,Y02上是否有阻挡]
                string strCposition = new InBill().GetStopCurrent_RD(ids);
                if (strCposition != "")
                {
                    // 获取阻碍储位的Y的值
                    string strCy = new InBill().strGetCY(strCposition);

                    string strTempCpositon = ""; // 临时储位TEMP000
                    string strTempLoc = ""; //临时储位LOC
                    string strStopLoc = ""; //对面阻止的储位

                    strStopLoc = GetLocXYZ(strCposition);
                    string CZ = strStopLoc.Substring(5, 2);
                    // 判断是否会被阻挡
                    if (GetCmd_Loc(strStopLoc, out errmsg))
                    {
                        DBConnClose();
                        return false;
                    }

                    // 判断相邻储位是否可以移动
                    DataTable dt = inBill.GetAllCurrentByNull(strCy);
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int min = 0;
                            int width = Convert.ToInt32(strStopLoc.Substring(0, 2)) - Convert.ToInt32(dt.Rows[0]["CX"].ToString());
                            int height = Convert.ToInt32(strStopLoc.Substring(strStopLoc.Length - 2)) - Convert.ToInt32(dt.Rows[0]["CZ"].ToString());
                            int result = (width * width) + (height * height);
                            result = Convert.ToInt32(Math.Sqrt(result));
                            if (result > min)
                            {
                                min = result;
                                strTempCpositon = dt.Rows[0]["CPOSITIONCODE"].ToString();
                            }
                        }
                    }
                    //if (!string.IsNullOrEmpty(strTempCpositon))
                    //{
                    //    errmsg = "[" + entity.cinvcode + "]未配置储位的种类";
                    //    return false;
                    //}
                    if (string.IsNullOrEmpty(strTempCpositon))
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg9 + "!"; //
                        //"没有多余的移动储位,阻挡储位无法移动！";
                        return false;
                    }
                    strTempLoc = GetLocXYZ(strTempCpositon);

                    // 移动
                    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strStopLoc, strTempLoc, LocSize, 1, str_Space, LineId, cmdno, out errmsg))
                    {
                        //DBConnClose();
                    }
                    // 入库
                    WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space,LineId, cmdno, out errmsg))
                    {
                        //DBConnClose();
                    }
                    // 返回
                    WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strTempLoc, strStopLoc, LocSize, 1, str_Space,LineId, cmdno, out errmsg))
                    {
                        entity.asrs_status = 1;
                        entity.wmstskid = WmstskId;
                        entity.asrs_num = 3;
                        con.Update(entity);
                        con.Save();
                        DBConnClose();
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }
                }
                else
                {
                    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space,LineId, cmdno, out errmsg))
                    {
                        entity.asrs_status = 1;
                        entity.wmstskid = WmstskId;
                        entity.asrs_num = 0;
                        con.Update(entity);
                        con.Save();
                        DBConnClose();
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }
                }
                #endregion
            }
            else
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg25 + "!";// "入库单明细ASRS状态不是未处理，不能入库";
                return false;
            }

            return true;


        }
        catch (Exception err)
        {
            errmsg = err.Message;
            return false;
        }
    }

    //取消按钮选择
    public static bool ASRS_InCancel_All(string ids, out string errmsg)
    {

        try
        {
            errmsg = "";
            bool dbflag = false;
            string inbillstatus = string.Empty;
            int WmstskId = 0;
            //判断入库单状态，如果是0，则插入数据至中间表
            inbillstatus = WmsDBCommon_ASRS.GetInbillStatus(ids);
            if (inbillstatus.Equals("1"))
            {
                #region 运作中
                IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                var caseList = from p in con.Get()
                               where p.ids == ids
                               select p;
                INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                WmstskId = Convert.ToInt32(entity.wmstskid);
                string mststatus = string.Empty;
                if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                {
                    mststatus = errmsg;
                }
                else
                {
                    return false;
                }

                if (mststatus.Equals("0"))
                {
                    //entity.ASRS_NUM = 0;
                    // 回滚处理
                    if (entity.asrs_num > 0)
                    {
                        // 第二次判断
                        string mststatus2 = string.Empty;
                        if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - 2, out errmsg))
                        {
                            mststatus2 = errmsg;
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                        if (!mststatus2.Equals("0"))
                        {

                            //更新inbill_d状态
                            //entity.ASRS_STATUS = Convert.ToInt32(mststatus2);
                            con.Update(entity);
                            con.Save();
                            DBConnClose();
                            errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!";//"该明细状态是[执行中]，不能取消";
                            return false;
                        }

                        for (int i = 1; i < entity.asrs_num; i++)
                        {
                            if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId - i, out errmsg))
                            {
                                //  
                            }
                        }
                        if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                        {
                            entity.wmstskid = null;
                            entity.asrs_status = 0;
                            entity.asrs_num = 0;
                            con.Update(entity);
                            con.Save();
                            DBConnClose();
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }

                    }
                    else
                    {
                        //删除原中间表状态，更新inbill表状态
                        if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                        {
                            entity.wmstskid = null;
                            entity.asrs_status = 0;
                            entity.asrs_num = 0;
                            con.Update(entity);
                            con.Save();
                            DBConnClose();
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                    }
                    //pan gao 20160601
                    //CancelMVS_CMD(WmstskId,out errmsg);
                }
                else
                {
                    //更新inbill_d状态
                    entity.asrs_status = Convert.ToInt32(mststatus);
                    con.Update(entity);
                    con.Save();
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!";//"该明细状态是[执行中]，不能取消";
                    return false;
                }
                #endregion
            }

            return true;

        }
        catch (Exception err)
        {
            errmsg = err.Message;
            return false;
        }
    }


    //获取入库单信息
    public static DataTable GetInBill_D(string id)
    {
        string Sql = string.Format(@"select bid.ids,bid.wmstskid,bid.asrs_status, bid.asrs_num from inbill_d bid where bid.id='{0}' and bid.wmstskid is not null ", id);
        return DBHelp.ExecuteToDataTable(Sql);
    }

    // WL 空栈板入库
    public static bool ASRS_InChangeStatus_S_I(string ids, string str_Pallet,string LineId, out string errmsg)
    {
        try
        {
            errmsg = "";
            bool dbflag = false;
            string inbillstatus = string.Empty;
            int WmstskId = 0;
            int intNum = 0;
            string str_Space = "0";

            if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
            {
                dbflag = true;
            }
            else
            {
                return false;
            }

            // WL 20160516
            if (GetInbillPallet(ids) == "0")
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg2 + "!";// "站点选择错位，请重新选择!";
                DBConnClose();
                return false;
            }
            else
            {
                str_Space = GetInbillPallet(ids);
            }

            //测试连接成功
            if (dbflag)
            {
                // WL 201605116 选择标签
                //判断入库单状态，如果是0，则插入数据至中间表
                inbillstatus = WmsDBCommon_ASRS.GetInbillStatus(ids);
                string cmdno = Getcmdno();
                if (inbillstatus.Equals("0"))
                {
                    #region 未处理
                    WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                    IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.ids == ids
                                   select p;
                    INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                    if (string.IsNullOrEmpty(entity.cpositioncode) || string.IsNullOrEmpty(entity.iquantity.ToString()))
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg6 + "!"; // "储位或数量为空，不能入库";
                        DBConnClose();
                        return false;
                    }
                    string Loc = "";

                    Loc = GetLocXYZ(entity.cpositioncode.Trim());
                    if (Loc == "" || Loc.Length < 7)
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg7 + "!";// "未配置储位的X或Y或Z";
                        DBConnClose();
                        return false;
                    }
                    //种类
                    string LocSize = "";
                    LocSize = GetLocSize(entity.cpositioncode.Trim()).Trim();
                    if (string.IsNullOrEmpty(LocSize))
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg8+ "!"; //"未配置储位的种类";
                        DBConnClose();
                        return false;
                    }
                    string CmdSno = GetCmdSno();
                    //string CmdSno = str_Space;

                    // -- 201501216 ADD -- 
                    // 判断是否入储位是否为空
                    //bool blcurrent = listQuery.CheckStockByCurrent(ids);
                    //if (blcurrent)
                    //{
                    //}
                    //else
                    //{
                    //    errmsg = "储位以满,请调整储位!";
                    //    DBConnClose();
                    //    return false;
                    //}

                    // 判断相邻储位是否有阻挡[判断Y01,Y02上是否有阻挡]
                    string strCposition =new InBill().GetStopCurrent_RD(ids);
                    if (strCposition != "")
                    {
                        // 获取阻碍储位的Y的值
                        string strCy = new InBill().strGetCY(strCposition);

                        string strTempCpositon = ""; // 临时储位TEMP000
                        string strTempLoc = ""; //临时储位LOC
                        string strStopLoc = ""; //对面阻止的储位

                        strStopLoc = GetLocXYZ(strCposition);
                        string CZ = strStopLoc.Substring(5, 2);
                        // 判断是否会被阻挡
                        if (GetCmd_Loc(strStopLoc, out errmsg))
                        {
                            DBConnClose();
                            return false;
                        }

                        // 判断相邻储位是否可以移动
                        // 判断相邻储位是否可以移动
                        DataTable dt = inBill.GetAllCurrentByNull(strCy);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int min = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                int width = Convert.ToInt32(strStopLoc.Substring(2, 3)) - Convert.ToInt32(dt.Rows[i]["CY"].ToString());
                                int height = Convert.ToInt32(strStopLoc.Substring(strStopLoc.Length - 2)) - Convert.ToInt32(dt.Rows[i]["CZ"].ToString());
                                int result = (width * width) + (height * height);
                                result = Convert.ToInt32(Math.Sqrt(result));
                                if (min == 0)
                                {
                                    min = result;
                                    strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                                }
                                if (result < min)
                                {
                                    min = result;
                                    strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(strTempCpositon))
                        {
                            errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg9 + "!"; // //"没有多余的移动储位,阻挡储位无法移动！";
                            return false;
                        }
                        strTempLoc = GetLocXYZ(strTempCpositon);

                        // 移动
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strStopLoc, strTempLoc, LocSize, 5, str_Space,LineId, cmdno, out errmsg))
                        {
                            //DBConnClose();
                            intNum++;
                        }
                        // 入库
                        CmdSno = GetCmdSno();
                        WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space,LineId, cmdno, out errmsg)) ;
                        {
                            //DBConnClose();
                            intNum++;
                        }
                        // 返回
                        CmdSno = GetCmdSno();
                        WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strTempLoc, strStopLoc, LocSize, 5, str_Space,LineId, cmdno, out errmsg))
                        {
                            //entity.ASRS_STATUS = 1;
                            //entity.WMSTSKID = WmstskId.ToString();
                            //entity.ASRS_NUM = 3;
                            //INBILL_DRule.Update(entity);
                            if (str_Pallet == "0")
                            {
                                UpdateCmd_Pattle_I(Loc);
                                DBConnClose();
                            }
                            else
                            {
                                UpdateCmd_Pattle_O(Loc);
                                DBConnClose();
                            }
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                    }
                    else
                    {
                        if (str_Pallet == "0")
                        {
                            if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space,LineId, cmdno, out errmsg))
                            {

                                UpdateCmd_Pattle_I(Loc);
                                DBConnClose();
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                        else
                        {
                            if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 2, str_Space,LineId, cmdno, out errmsg))
                            {
                                UpdateCmd_Pattle_O(Loc);
                                DBConnClose();
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                    }
                    #endregion
                }
                else if (inbillstatus.Equals("1"))
                {
                    #region 运作中
                    IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.ids == ids
                                   select p;
                    INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                    WmstskId = Convert.ToInt32(entity.wmstskid);
                    string mststatus = string.Empty;

                    if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                    {
                        mststatus = errmsg;
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }

                    if (mststatus.Equals("0"))
                    {
                        //entity.ASRS_NUM = 0;
                        // 回滚处理
                        if (entity.asrs_num > 0)
                        {
                            // 第二次判断
                            string mststatus2 = string.Empty;
                            if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - 2, out errmsg))
                            {
                                mststatus2 = errmsg;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                            if (!mststatus2.Equals("0"))
                            {

                                //更新inbill_d状态
                                //entity.ASRS_STATUS = Convert.ToInt32(mststatus2);
                                con.Update(entity);
                                con.Save();
                                DBConnClose();
                                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!"; //"该明细状态是[执行中]，不能取消";
                                return false;
                            }

                            for (int i = 1; i < entity.asrs_num; i++)
                            {
                                if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId - i, out errmsg))
                                {
                                    //  
                                }
                            }
                            if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                            {
                                entity.wmstskid = null;
                                entity.asrs_status = 0;
                                entity.asrs_num = 0;
                                con.Update(entity);
                                con.Save();
                                DBConnClose();
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }

                        }
                        else
                        {
                            //删除原中间表状态，更新inbill表状态
                            if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                            {
                                entity.wmstskid = null;
                                entity.asrs_status = 0;
                                entity.asrs_num = 0;
                                con.Update(entity);
                                con.Save();
                                DBConnClose();
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //更新inbill_d状态
                        entity.asrs_status = Convert.ToInt32(mststatus);
                        con.Update(entity);
                        con.Save();
                        DBConnClose();
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!"; //"该明细状态是[执行中]，不能取消";
                        return false;
                    }

                    #endregion
                }
                else if (inbillstatus.Equals("8"))
                {
                    #region 处理异常
                    //处理异常
                    IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.ids == ids
                                   select p;
                    INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                    WmstskId = Convert.ToInt32(entity.wmstskid);
                    //string mststatus = WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId);
                    string mststatus = string.Empty;

                    if (entity.asrs_num == 0)
                    {
                        if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                        {
                            mststatus = errmsg;
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }

                        if (mststatus.Equals("8"))
                        {
                            if (WmsDBCommon_ASRS.UpdateCmd_mst(WmstskId, out errmsg))
                            {
                                entity.asrs_status = 1;
                                con.Update(entity);
                                con.Save();
                                DBConnClose();
                                return true;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                    }

                    else
                    {
                        for (int i = 0; i < entity.asrs_num; i++)
                        {
                            if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - i, out errmsg))
                            {
                                mststatus = errmsg;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }

                            if (mststatus.Equals("8"))
                            {
                                if (WmsDBCommon_ASRS.UpdateCmd_mst(WmstskId - i, out errmsg))
                                {
                                    entity.asrs_status = 1;
                                    con.Update(entity);
                                    con.Save();
                                    DBConnClose();
                                    return true;
                                }
                                else
                                {
                                    DBConnClose();
                                    return false;
                                }
                            }
                        }
                    }
                    DBConnClose();
                    return true;

                    /*
                    if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                    {
                        mststatus = errmsg;
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }
                        
                    if (mststatus.Equals("8"))
                    {
                        if (WmsDBCommon_ASRS.UpdateCmd_mst(WmstskId, out errmsg))
                        {
                            entity.ASRS_STATUS = 1;
                            INBILL_DRule.Update(entity);
                            DBConnClose();
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                    }
                    */

                    #endregion
                }
                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception err)
        {
            errmsg = err.Message;
            DBConnClose();
            return false;
        }
    }
    // WL 空栈板出库
    public static bool ASRS_InChangeStatus_S_O(string ids, string str_Pallet,string LineId, out string errmsg)
    {
        try
        {
            errmsg = "";
            bool dbflag = false;
            string inbillstatus = string.Empty;
            int WmstskId = 0;
            int intNum = 0;
            string str_Space = "0";

            if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
            {
                dbflag = true;
            }
            else
            {
                return false;
            }

            // WL 20160516
            str_Space = GetInbillPallet(ids);
            if (str_Space == "0" || str_Space == "")
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg2 + "!"; //"站点选择错位，请重新选择!";
                DBConnClose();
                return false;
            }

            //测试连接成功
            if (dbflag)
            {
                //判断入库单状态，如果是0，则插入数据至中间表
                inbillstatus = WmsDBCommon_ASRS.GetInbillStatus(ids);
                string cmdno = Getcmdno();
                if (inbillstatus.Equals("0"))
                {
                    #region 未处理
                    WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                    IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.ids == ids
                                   select p;
                    INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                    if (string.IsNullOrEmpty(entity.cpositioncode) || string.IsNullOrEmpty(entity.iquantity.ToString()))
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg6 + "!";  //"储位或数量为空，不能入库";
                        DBConnClose();
                        return false;
                    }
                    string Loc = "";

                    Loc = GetLocXYZ(entity.cpositioncode.Trim());
                    if (Loc == "" || Loc.Length < 7)
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg7 + "!"; //"未配置储位的X或Y或Z";
                        DBConnClose();
                        return false;
                    }
                    //种类
                    string LocSize = "";
                    LocSize = GetLocSize(entity.cpositioncode.Trim()).Trim();
                    if (string.IsNullOrEmpty(LocSize))
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg8 + "!"; //"未配置储位的种类";
                        DBConnClose();
                        return false;
                    }
                    string CmdSno = GetCmdSno();
                    //string CmdSno = str_Space;

                    // -- 201501216 ADD -- 
                    // 判断是否入储位是否为空
                    //bool blcurrent = listQuery.CheckStockByCurrent(ids);
                    //if (blcurrent)
                    //{
                    //}
                    //else
                    //{
                    //    errmsg = "储位以满,请调整储位!";
                    //    DBConnClose();
                    //    return false;
                    //}

                    // 判断相邻储位是否有阻挡[判断Y01,Y02上是否有阻挡]
                    string strCposition = new InBill().GetStopCurrent_RD(ids);
                    if (strCposition != "")
                    {
                        // 获取阻碍储位的Y的值
                        string strCy = new InBill().strGetCY(strCposition);

                        string strTempCpositon = ""; // 临时储位TEMP000
                        string strTempLoc = ""; //临时储位LOC
                        string strStopLoc = ""; //对面阻止的储位

                        strStopLoc = GetLocXYZ(strCposition);
                        string CZ = strStopLoc.Substring(5, 2);
                        // 判断是否会被阻挡
                        if (GetCmd_Loc(strStopLoc, out errmsg))
                        {
                            DBConnClose();
                            return false;
                        }

                        // 判断相邻储位是否可以移动
                        // 判断相邻储位是否可以移动
                        DataTable dt = inBill.GetAllCurrentByNull(strCy);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int min = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                int width = Convert.ToInt32(strStopLoc.Substring(2, 3)) - Convert.ToInt32(dt.Rows[i]["CY"].ToString());
                                int height = Convert.ToInt32(strStopLoc.Substring(strStopLoc.Length - 2)) - Convert.ToInt32(dt.Rows[i]["CZ"].ToString());
                                int result = (width * width) + (height * height);
                                result = Convert.ToInt32(Math.Sqrt(result));
                                if (min == 0)
                                {
                                    min = result;
                                    strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                                }
                                if (result < min)
                                {
                                    min = result;
                                    strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(strTempCpositon))
                        {
                            errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg9 + "!"; //"没有多余的移动储位,阻挡储位无法移动！";
                            return false;
                        }
                        strTempLoc = GetLocXYZ(strTempCpositon);

                        //strTempLoc = "0200203";
                        // 移动出
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strStopLoc, strTempLoc, LocSize, 5, str_Space,LineId, cmdno, out errmsg))
                        {
                            //DBConnClose();
                            intNum++;
                            UpdateCmd_Pattle_O(strStopLoc);
                            UpdateCmd_Pattle_I(strTempLoc);
                        }
                        // 出库
                        CmdSno = GetCmdSno();
                        WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 2, str_Space,LineId, cmdno, out errmsg))
                        {
                            //DBConnClose();
                            intNum++;
                            UpdateCmd_Pattle_O(Loc);

                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }

                        //pan gao
                        //判断储位上是否有物品，如果有，再把移动过去的棧板再移动回来。
                        if (!new InBill().CheckStockByStopLoc(strStopLoc))
                        {
                            CmdSno = GetCmdSno();
                            WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                            if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strTempLoc, strStopLoc, LocSize, 5, str_Space,LineId, cmdno, out errmsg))
                            {
                                UpdateCmd_Pattle_O(strTempLoc);
                                UpdateCmd_Pattle_I(strStopLoc);
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }

                        //DBConnClose();//pan gao LED update 20160608
                        // 返回
                        /* PG 20160525
                        CmdSno = GetCmdSno();
                        WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strTempLoc, strStopLoc, LocSize, 5, str_Space, out errmsg))
                        {
                            //entity.ASRS_STATUS = 1;
                            //entity.WMSTSKID = WmstskId.ToString();
                            //entity.ASRS_NUM = 3;
                            //INBILL_DRule.Update(entity);
                            if (str_Pallet == "0")
                            {
                                UpdateCmd_Pattle_I(Loc);
                                DBConnClose();
                            }
                            else
                            {
                                UpdateCmd_Pattle_O(Loc);
                                DBConnClose();
                            }
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }*/
                    }
                    else
                    {
                        if (str_Pallet == "0")
                        {
                            if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space,LineId, cmdno, out errmsg))
                            {

                                UpdateCmd_Pattle_I(Loc);
                                //DBConnClose();//pan gao LED update 20160608
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                        else
                        {
                            if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 2, str_Space,LineId, cmdno, out errmsg))
                            {
                                UpdateCmd_Pattle_O(Loc);
                                //DBConnClose();//pan gao LED update 20160608
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                    }
                    #endregion


                    DBConnClose();//pan gao LED update 20160608
                }
                else if (inbillstatus.Equals("1"))
                {
                    #region 运作中
                    IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.ids == ids
                                   select p;
                    INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                    WmstskId = Convert.ToInt32(entity.wmstskid);
                    string mststatus = string.Empty;

                    if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                    {
                        mststatus = errmsg;
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }

                    if (mststatus.Equals("0"))
                    {
                        //entity.ASRS_NUM = 0;
                        // 回滚处理
                        if (entity.asrs_num > 0)
                        {
                            // 第二次判断
                            string mststatus2 = string.Empty;
                            if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - 2, out errmsg))
                            {
                                mststatus2 = errmsg;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                            if (!mststatus2.Equals("0"))
                            {

                                //更新inbill_d状态
                                //entity.ASRS_STATUS = Convert.ToInt32(mststatus2);
                                con.Update(entity); con.Save();
                                DBConnClose();
                                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!"; //"该明细状态是[执行中]，不能取消";
                                return false;
                            }

                            for (int i = 1; i < entity.asrs_num; i++)
                            {
                                if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId - i, out errmsg))
                                {
                                    //  
                                }
                            }
                            if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                            {
                                entity.wmstskid = null;
                                entity.asrs_status = 0;
                                entity.asrs_num = 0;
                                con.Update(entity);
                                con.Save();
                                DBConnClose();
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }

                        }
                        else
                        {
                            //删除原中间表状态，更新inbill表状态
                            if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                            {
                                entity.wmstskid = null;
                                entity.asrs_status = 0;
                                entity.asrs_num = 0;
                                con.Update(entity); con.Save();
                                DBConnClose();
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //更新inbill_d状态
                        entity.asrs_status = Convert.ToInt32(mststatus);
                        con.Update(entity); con.Save();
                        DBConnClose();
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!"; //"该明细状态是[执行中]，不能取消";
                        return false;
                    }

                    #endregion
                }
                else if (inbillstatus.Equals("8"))
                {
                    #region 处理异常
                    //处理异常
                    IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.ids == ids
                                   select p;
                    INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                    WmstskId = Convert.ToInt32(entity.wmstskid);
                    //string mststatus = WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId);
                    string mststatus = string.Empty;

                    if (entity.asrs_num == 0)
                    {
                        if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                        {
                            mststatus = errmsg;
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }

                        if (mststatus.Equals("8"))
                        {
                            if (WmsDBCommon_ASRS.UpdateCmd_mst(WmstskId, out errmsg))
                            {
                                entity.asrs_status = 1;
                                con.Update(entity); con.Save();
                                DBConnClose();
                                return true;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                    }

                    else
                    {
                        for (int i = 0; i < entity.asrs_num; i++)
                        {
                            if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - i, out errmsg))
                            {
                                mststatus = errmsg;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }

                            if (mststatus.Equals("8"))
                            {
                                if (WmsDBCommon_ASRS.UpdateCmd_mst(WmstskId - i, out errmsg))
                                {
                                    entity.asrs_status = 1;
                                    con.Update(entity); con.Save();
                                    DBConnClose();
                                    return true;
                                }
                                else
                                {
                                    DBConnClose();
                                    return false;
                                }
                            }
                        }
                    }
                    DBConnClose();
                    return true;

                    /*
                    if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                    {
                        mststatus = errmsg;
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }
                        
                    if (mststatus.Equals("8"))
                    {
                        if (WmsDBCommon_ASRS.UpdateCmd_mst(WmstskId, out errmsg))
                        {
                            entity.ASRS_STATUS = 1;
                            INBILL_DRule.Update(entity);
                            DBConnClose();
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                    }
                    */

                    #endregion
                }



                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception err)
        {
            errmsg = err.Message;
            DBConnClose();
            return false;
        }
    }
    #endregion
    #region 出库
    //pan gao 出库（出库+返库合并在一起）
    //出库
    /// <summary>
    /// 出库动作（出库+返库合并在一起）
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="str_Space"></param>
    /// <param name="errmsg"></param>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static bool ASRS_OutChangeStatus(string ids, string str_Space, out string errmsg,string LineId, string filePath = "")
    {

        try
        {
            errmsg = "";
            bool dbflag = false;
            string outbillstatus = string.Empty;
            int WmstskId = 0;

            if (!HasEmptyCargo())
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg1 + "!"; //"必须有2个空储位且没有栈板！";
                return false;
            }

            if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
            {
                dbflag = true;
            }
            else
            {
                return false;
            }

            // WL 20160516
            str_Space = GetInbillPallet_Out(ids);
            if (str_Space == "0" || str_Space == "")
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg2 + "!"; //"站点选择错位，请重新选择!";
                DBConnClose();
                return false;
            }

            //测试连接成功
            if (dbflag)
            {
                string IsOnlyRun = GetConFig("300002");
                    if (IsOnlyRun == "1")
                    {
                if (CheckStatusCmd_Mst())
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg3 + "!";//"有正在执行的AS/RS命令，请稍后再试！";
                    DBConnClose();
                    return false;
                }
                    }

                bool ingCmd = CheckPositionUsed(ids, "OUT");
                    if (ingCmd)
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg4 + "!";//"当前储位或相邻储位有正在执行未完成的AS/RS命令!";
                    return false;
                }

                //if (!OutQuantityCheck(ids))
                //{
                //    errmsg = "出库单中的数量与明细中的数量不一致!";
                //    DBConnClose();
                //    return false;
                //}

                string cmdno = Getcmdno();
                //判断入库单状态，如果是0，则插入数据至中间表
                outbillstatus =WmsDBCommon_ASRS.GetOutbillStatus(ids);
                if (outbillstatus.Equals("0"))
                {

                    #region 未处理
                    WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                    IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.ids == ids.Trim()
                                   select p;
                    OUTBILL_D entityOutBill_d = caseList.ToList().FirstOrDefault();
                    if (string.IsNullOrEmpty(entityOutBill_d.cposition) || string.IsNullOrEmpty(entityOutBill_d.iquantity.ToString()))
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg27 + "!"; //"储位或数量为空，不能出库";
                        DBConnClose();
                        return false;
                    }
                    string Loc = "";
                    Loc = GetLocXYZ(entityOutBill_d.cpositioncode.Trim());
                    if (Loc == "" || Loc.Length < 6)
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg7 + "!"; //"未配置储位的X或Y或Z";
                        DBConnClose();
                        return false;
                    }
                    //种类
                    string LocSize = "";
                    LocSize = GetLocSize(entityOutBill_d.cpositioncode.Trim()).Trim();
                    if (string.IsNullOrEmpty(LocSize))
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg8 + "!"; //"未配置储位的种类";
                        DBConnClose();
                        return false;
                    }
                    string CmdSno = GetCmdSno();


                    // 判断相邻储位是否有阻挡[判断Y01,Y02上是否有阻挡]
                    string strCposition = new OutBillQuery().GetStopCurrent_Out(ids);
                    if (strCposition != "")
                    {
                        // 获取阻碍储位的Y的值
                        string strCy = new OutBillQuery().strGetCY(strCposition);

                        string strTempCpositon = ""; // 临时储位TEMP000
                        string strTempLoc = ""; //临时储位LOC
                        string strStopLoc = ""; //对面阻止的储位

                        strStopLoc = GetLocXYZ(strCposition);
                        string CZ = strStopLoc.Substring(5, 2);
                        // 判断是否会被阻挡
                        if (GetCmd_Loc(strStopLoc, out errmsg))
                        {
                            DBConnClose();
                            return false;
                        }

                        // 判断相邻储位是否可以移动
                        //DataTable dt = listQuery.GetAllCurrentByNull(strCy);
                        //if (dt != null && dt.Rows.Count > 0)
                        //{

                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        int min = 0;
                        //        int width = Convert.ToInt32(strStopLoc.Substring(0, 2)) - Convert.ToInt32(dt.Rows[0]["CX"].ToString());
                        //        int height = Convert.ToInt32(strStopLoc.Substring(strStopLoc.Length - 2)) - Convert.ToInt32(dt.Rows[0]["CZ"].ToString());
                        //        int result = (width * width) + (height * height);
                        //        result = Convert.ToInt32(Math.Sqrt(result));
                        //        if (result > min)
                        //        {
                        //            min = result;
                        //            strTempCpositon = dt.Rows[0]["CPOSITIONCODE"].ToString();
                        //        }
                        //    }
                        //}
                        //strTempLoc = GetLocXYZ(strTempCpositon);


                        // 判断相邻储位是否可以移动

                        DataTable dt = inBill.GetAllCurrentByNull(strCy,CZ);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int min = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                int width = Convert.ToInt32(strStopLoc.Substring(2, 3)) - Convert.ToInt32(dt.Rows[i]["CY"].ToString());
                                int height = Convert.ToInt32(strStopLoc.Substring(strStopLoc.Length - 2)) - Convert.ToInt32(dt.Rows[i]["CZ"].ToString());
                                int result = (width * width) + (height * height);
                                result = Convert.ToInt32(Math.Sqrt(result));
                                if (min == 0)
                                {
                                    min = result;
                                    strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                                }
                                if (result < min)
                                {
                                    min = result;
                                    strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(strTempCpositon))
                        {
                            errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg9 + "!"; //"没有多余的移动储位,阻挡储位无法移动！";
                            return false;
                        }
                        strTempLoc = GetLocXYZ(strTempCpositon);
                        //strTempLoc = "0200203";

                        // 移动
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strStopLoc, strTempLoc, LocSize, 5, str_Space,LineId, cmdno, out errmsg))
                        {
                            UpdateCmd_Pattle_O(strStopLoc);
                            UpdateCmd_Pattle_I(strTempLoc);

                        }
                        // 出库
                        WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                        CmdSno = GetCmdSno();
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 2, str_Space,LineId, cmdno, out errmsg))
                        {
                            //UpdateCmd_Pattle_O(Loc);
                        }

                        //返库
                        CmdSno = GetCmdSno();
                        WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space,LineId, cmdno, out errmsg))
                        {
                            //UpdateCmd_Pattle_I(Loc);
                        }
                        //返回阻碍储位
                        //判断储位上是否有物品，如果有，再把移动过去的棧板再移动回来。
                        //if (!new OutBillQuery().CheckStockByStopLoc(strStopLoc))
                        //{
                        //    WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                        //    CmdSno = GetCmdSno();
                        //    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strTempLoc, strStopLoc, LocSize, 5, str_Space,LineId, cmdno, out errmsg))
                        //    {
                        //        UpdateCmd_Pattle_O(strTempLoc);
                        //        UpdateCmd_Pattle_I(strStopLoc);
                        //    }
                        //    else
                        //    {
                        //        DBConnClose();
                        //        return false;
                        //    }
                        //}
                        if (!new OutBillQuery().CheckStockByStopLoc(strStopLoc))
                        {
                                string strStopPostion = GetPositionByXYZ(strStopLoc);
                                string strTempPostion = GetPositionByXYZ(strTempLoc);
                                //阻挡有货物不返回，产生调拨单
                                PROC_AUTO_ALLOCATE prc = new PROC_AUTO_ALLOCATE();
                                prc.P_PositonFrom = strStopPostion;
                                prc.P_PositonTo = strTempPostion;
                                prc.P_UserNo = "WMS";
                                prc.Execute();
                        }
                        entityOutBill_d.asrs_num = 3;
                        entityOutBill_d.asrs_status = 1;
                        entityOutBill_d.wmstskid = WmstskId;
                        con.Update(entityOutBill_d);
                        con.Save();

                    }
                    else
                    {
                        //*****************************************************************
                        //出库
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 2, str_Space,LineId, cmdno, out errmsg))
                        {
                            //返库
                            CmdSno = GetCmdSno();
                            WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                            if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space,LineId, cmdno, out errmsg))
                            {
                                //UpdateCmd_Pattle_I(Loc);
                            }
                            entityOutBill_d.asrs_status = 1;
                            entityOutBill_d.wmstskid = WmstskId;
                            con.Update(entityOutBill_d);
                            con.Save();
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                    }
                    #endregion
                    //pan gao 20160601 inert to LED
                    //LED_OUT(ids, CmdSno, out errmsg, filePath);
                    DBConnClose();//pan gao LED update 20160608
                }
                else if (outbillstatus.Equals("1"))
                {
                    #region 运作中
                    IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.id == ids
                                   select p;
                    OUTBILL_D entity = caseList.ToList().FirstOrDefault();
                    WmstskId = Convert.ToInt32(entity.wmstskid);
                    string mststatus = string.Empty;
                    if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                    {
                        mststatus = errmsg;
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }

                    if (mststatus.Equals("0"))
                    {
                        // 回滚处理
                        if (entity.asrs_num > 0)
                        {
                            // 第二次判断
                            string mststatus2 = string.Empty;
                            if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - 2, out errmsg))
                            {
                                mststatus2 = errmsg;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                            if (!mststatus2.Equals("0"))
                            {

                                //更新inbill_d状态
                                //entity.ASRS_STATUS = Convert.ToInt32(mststatus2);
                                con.Update(entity);
                                con.Save();
                                DBConnClose();
                                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!"; //"该明细状态是[执行中]，不能取消";
                                return false;
                            }

                            for (int i = 1; i < entity.asrs_num; i++)
                            {
                                if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId - i, out errmsg))
                                {
                                    //  
                                }
                            }
                            if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                            {
                                entity.wmstskid = null;
                                entity.asrs_status = 0;
                                entity.asrs_num = 0;
                                con.Update(entity);
                                con.Save();
                                UpdateCmd_Pattle_I(entity.cpositioncode);
                                DBConnClose();
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }

                        }
                        else
                        {
                            //删除原中间表状态，更新inbill表状态
                            if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                            {
                                entity.wmstskid = null;
                                entity.asrs_status = 0;
                                entity.asrs_num = 0;
                                con.Update(entity);
                                con.Save();
                                UpdateCmd_Pattle_I(entity.cpositioncode);
                                DBConnClose();
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //更新inbill_d状态
                        entity.asrs_status = Convert.ToInt32(mststatus);
                        con.Update(entity);
                        con.Save();
                        DBConnClose();
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!"; //"该明细状态是[执行中]，不能取消";
                        return false;
                    }
                    #endregion
                }
                else if (outbillstatus.Equals("8"))
                {
                    #region 处理异常
                    //处理异常
                    IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
                    var caseList = from p in con.Get()
                                   where p.ids == ids
                                   select p;
                    OUTBILL_D entity = caseList.ToList().FirstOrDefault();
                    WmstskId = Convert.ToInt32(entity.wmstskid);
                    //string mststatus = DBCommon_ASRS.GetCmd_MstStatus(WmstskId);
                    string mststatus = string.Empty;
                    if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                    {
                        mststatus = errmsg;
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }
                    for (int i = 0; i < entity.asrs_num; i++)
                    {
                        if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - i, out errmsg))
                        {
                            mststatus = errmsg;
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }

                        if (mststatus.Equals("8"))
                        {
                            if (WmsDBCommon_ASRS.UpdateCmd_mst(WmstskId - i, out errmsg))
                            {
                                entity.asrs_status = 1;
                                con.Update(entity);
                                con.Save();
                                DBConnClose();
                                return true;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                    }
                    #endregion
                }
                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception err)
        {
            errmsg = err.Message;
            DBConnClose();
            return false;
        }
    }

    //批量出库专用
    public static bool ASRS_OutStatus(string ids, string str_Space,string LineId, out string errmsg)
    {

        try
        {
            errmsg = "";
            // bool dbflag = false;
            string outbillstatus = string.Empty;
            int WmstskId = 0;

            //判断入库单状态，如果是0，则插入数据至中间表
            outbillstatus = WmsDBCommon_ASRS.GetOutbillStatus(ids);
            if (outbillstatus.Equals("0"))
            {
                string cmdno = Getcmdno();
                #region 未处理
                WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
                var caseList = from p in con.Get()
                               where p.ids == ids
                               select p;
                OUTBILL_D entity = caseList.ToList().FirstOrDefault();
                if (string.IsNullOrEmpty(entity.cposition) || string.IsNullOrEmpty(entity.iquantity.ToString()))
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg27 + "!"; //"储位或数量为空，不能出库";
                    //DBConnClose();
                    return false;
                }
                string Loc = "";
                Loc = GetLocXYZ(entity.cpositioncode.Trim());
                if (Loc == "" || Loc.Length < 6)
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg7 + "!"; //"未配置储位的X或Y或Z";
                    // DBConnClose();
                    return false;
                }
                //种类
                string LocSize = "";
                LocSize = GetLocSize(entity.cpositioncode.Trim()).Trim();
                if (string.IsNullOrEmpty(LocSize))
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg8 + "!"; //"未配置储位的种类";
                    // DBConnClose();
                    return false;
                }
                string CmdSno = GetCmdSno();

                //*****************************************************************
                // 判断相邻储位是否有阻挡[判断Y01,Y02上是否有阻挡]
                InBill listQuery = new InBill();
                string strCposition = new OutBillQuery().GetStopCurrent_Out(ids);
                if (strCposition != "")
                {
                    // 获取阻碍储位的Y的值
                    string strCy = listQuery.strGetCY(strCposition);

                    string strTempCpositon = ""; // 临时储位TEMP000
                    string strTempLoc = ""; //临时储位LOC
                    string strStopLoc = ""; //对面阻止的储位

                    strStopLoc = GetLocXYZ(strCposition);
                    string CZ = strStopLoc.Substring(5, 2);
                    // 判断是否会被阻挡
                    if (GetCmd_Loc(strStopLoc, out errmsg))
                    {
                        DBConnClose();
                        return false;
                    }

                    // 判断相邻储位是否可以移动
                    DataTable dt = inBill.GetAllCurrentByNull(strCy);
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int min = 0;
                            int width = Convert.ToInt32(strStopLoc.Substring(0, 2)) - Convert.ToInt32(dt.Rows[0]["CX"].ToString());
                            int height = Convert.ToInt32(strStopLoc.Substring(strStopLoc.Length - 2)) - Convert.ToInt32(dt.Rows[0]["CZ"].ToString());
                            int result = (width * width) + (height * height);
                            result = Convert.ToInt32(Math.Sqrt(result));
                            if (result > min)
                            {
                                min = result;
                                strTempCpositon = dt.Rows[0]["CPOSITIONCODE"].ToString();
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(strTempCpositon))
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg9 + "!"; //"没有多余的移动储位,阻挡储位无法移动！";
                        return false;
                    }
                    strTempLoc = GetLocXYZ(strTempCpositon);

                    // 移动
                    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strStopLoc, strTempLoc, LocSize, 2, str_Space,LineId, cmdno, out errmsg))
                    {
                        //DBConnClose();

                    }
                    // 入库
                    WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 2, str_Space,LineId, cmdno, out errmsg)) ;
                    {
                        //DBConnClose();
                    }
                    // 返回
                    WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strTempLoc, strStopLoc, LocSize, 2, str_Space,LineId, cmdno, out errmsg))
                    {
                        entity.asrs_num = 3;
                        entity.asrs_status = 1;
                        entity.wmstskid = WmstskId;
                        con.Update(entity);
                        con.Save();
                        DBConnClose();
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }
                }
                else
                {
                    //*****************************************************************
                    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 2, str_Space,LineId, cmdno, out errmsg))
                    {
                        entity.asrs_status = 1;
                        entity.wmstskid = WmstskId;
                        con.Update(entity);
                        con.Save();
                        DBConnClose();
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }
                }
                #endregion
            }
            else
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg28 + "!"; //"出库单明细ASRS状态不是未处理，不能出库";
                return false;

            }
            //else if (outbillstatus.Equals("1"))
            //{
            //    #region 运作中
            //    OUTBILL_DEntity entity = new OUTBILL_DEntity();
            //    entity.IDS = ids;
            //    entity.SelectByPKeys();
            //    WmstskId = Convert.ToInt32(entity.WMSTSKID);
            //    string mststatus = string.Empty;
            //    if (DBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
            //    {
            //        mststatus = errmsg;
            //    }
            //    else
            //    {
            //        // DBConnClose();
            //        return false;
            //    }

            //    if (mststatus.Equals("0"))
            //    {
            //        //删除原中间表状态，更新inbill表状态
            //        if (DBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
            //        {
            //            entity.WMSTSKID = "";
            //            entity.ASRS_STATUS = 0;
            //            OUTBILL_DRule.Update(entity);
            //            // DBConnClose();
            //        }
            //        else
            //        {
            //            //DBConnClose();
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        //更新inbill_d状态
            //        entity.ASRS_STATUS = Convert.ToInt32(mststatus);
            //        OUTBILL_DRule.Update(entity);
            //        //  DBConnClose();
            //        errmsg = "该明细状态是[执行中]，不能取消";
            //        return false;
            //    }
            //    #endregion
            //}
            //else if (outbillstatus.Equals("8"))
            //{
            //    #region 处理异常
            //    //处理异常
            //    OUTBILL_DEntity entity = new OUTBILL_DEntity();
            //    entity.IDS = ids;
            //    entity.SelectByPKeys();
            //    WmstskId = Convert.ToInt32(entity.WMSTSKID);
            //    //string mststatus = DBCommon_ASRS.GetCmd_MstStatus(WmstskId);
            //    string mststatus = string.Empty;
            //    if (DBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
            //    {
            //        mststatus = errmsg;
            //    }
            //    else
            //    {
            //        DBConnClose();
            //        return false;
            //    }

            //    if (mststatus.Equals("8"))
            //    {
            //        if (DBCommon_ASRS.UpdateCmd_mst(WmstskId, out errmsg))
            //        {
            //            entity.ASRS_STATUS = 1;
            //            OUTBILL_DRule.Update(entity);
            //            // DBConnClose();
            //        }
            //        else
            //        {
            //            //  DBConnClose();

            //        }
            //        //}
            //    #endregion
            //    }
            //    return true;
            //}

            //else
            //{
            //    return false;
            //}


        }
        catch (Exception err)
        {
            errmsg = err.Message;
            //DBConnClose();
            return false;
        }
        return true;
    }

    //批量出库专用
    public static bool ASRS_OutCancel(string ids, out string errmsg)
    {

        try
        {
            errmsg = "";
            // bool dbflag = false;
            string outbillstatus = string.Empty;
            int WmstskId = 0;

            //判断入库单状态，如果是0，则插入数据至中间表
            outbillstatus = WmsDBCommon_ASRS.GetOutbillStatus(ids);
            //if (outbillstatus.Equals("0"))
            //{

            //    #region 未处理
            //    WmstskId = DBCommon_ASRS.GetWmsTskID();
            //    OUTBILL_DEntity entity = new OUTBILL_DEntity();
            //    entity.IDS = ids;
            //    entity.SelectByPKeys();
            //    if (string.IsNullOrEmpty(entity.CPOSITIONCODE) || string.IsNullOrEmpty(entity.IQUANTITY.ToString()))
            //    {
            //        errmsg = "储位或数量为空，不能出库";
            //        //DBConnClose();
            //        return false;
            //    }
            //    string Loc = "";
            //    Loc = GetLocXYZ(entity.CPOSITIONCODE.Trim());
            //    if (Loc == "" || Loc.Length < 7)
            //    {
            //        errmsg = "未配置储位的X或Y或Z";
            //        // DBConnClose();
            //        return false;
            //    }
            //    //种类
            //    string LocSize = "";
            //    LocSize = GetLocSize(entity.CPOSITIONCODE.Trim()).Trim();
            //    if (string.IsNullOrEmpty(LocSize))
            //    {
            //        errmsg = "未配置储位的种类";
            //        // DBConnClose();
            //        return false;
            //    }
            //    string CmdSno = GetCmdSno();

            //    //*****************************************************************
            //    if (DBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 2, out errmsg))
            //    {
            //        entity.ASRS_STATUS = 1;
            //        entity.WMSTSKID = WmstskId.ToString();
            //        OUTBILL_DRule.Update(entity);
            //        //DBConnClose();
            //    }
            //    else
            //    {
            //        // DBConnClose();
            //        return false;
            //    }
            //    #endregion
            //}

            //else 
            if (outbillstatus.Equals("1"))
            {
                #region 运作中
                IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
                var caseList = from p in con.Get()
                               where p.ids == ids
                               select p;
                OUTBILL_D entity = caseList.ToList().FirstOrDefault();
                WmstskId = Convert.ToInt32(entity.wmstskid);
                string mststatus = string.Empty;
                if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                {
                    mststatus = errmsg;
                }
                else
                {
                    // DBConnClose();
                    return false;
                }

                if (mststatus.Equals("0"))
                {
                    // 回滚处理
                    if (entity.asrs_num > 0)
                    {
                        // 第二次判断
                        string mststatus2 = string.Empty;
                        if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId - 2, out errmsg))
                        {
                            mststatus2 = errmsg;
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                        if (!mststatus2.Equals("0"))
                        {

                            //更新inbill_d状态
                            //entity.ASRS_STATUS = Convert.ToInt32(mststatus2);
                            con.Update(entity);
                            con.Save();
                            DBConnClose();
                            errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!"; //"该明细状态是[执行中]，不能取消";
                            return false;
                        }

                        for (int i = 1; i < entity.asrs_num; i++)
                        {
                            if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId - i, out errmsg))
                            {
                                //  
                            }
                        }
                        if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                        {
                            entity.wmstskid = null;
                            entity.asrs_status = 0;
                            entity.asrs_num = 0;
                            con.Update(entity);
                            con.Save();
                            DBConnClose();
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }

                    }
                    else
                    {
                        //删除原中间表状态，更新inbill表状态
                        if (WmsDBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                        {
                            entity.wmstskid = null;
                            entity.asrs_status = 0;
                            entity.asrs_num = 0;
                            con.Update(entity);
                            con.Save();
                            DBConnClose();
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                    }
                }
                else
                {
                    //更新inbill_d状态
                    entity.asrs_status = Convert.ToInt32(mststatus);
                    con.Update(entity);
                    con.Save();
                    //  DBConnClose();
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg10 + "!"; //"该明细状态是[执行中]，不能取消";
                    return false;
                }
                #endregion
            }
            //else if (outbillstatus.Equals("8"))
            //{
            //    #region 处理异常
            //    //处理异常
            //    OUTBILL_DEntity entity = new OUTBILL_DEntity();
            //    entity.IDS = ids;
            //    entity.SelectByPKeys();
            //    WmstskId = Convert.ToInt32(entity.WMSTSKID);
            //    //string mststatus = DBCommon_ASRS.GetCmd_MstStatus(WmstskId);
            //    string mststatus = string.Empty;
            //    if (DBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
            //    {
            //        mststatus = errmsg;
            //    }
            //    else
            //    {
            //        DBConnClose();
            //        return false;
            //    }

            //    if (mststatus.Equals("8"))
            //    {
            //        if (DBCommon_ASRS.UpdateCmd_mst(WmstskId, out errmsg))
            //        {
            //            entity.ASRS_STATUS = 1;
            //            OUTBILL_DRule.Update(entity);
            //            // DBConnClose();
            //        }
            //        else
            //        {
            //            //  DBConnClose();

            //        }
            //        //}
            //    #endregion
            //    }
            //    return true;
            //}

            //else
            //{
            //    return false;
            //}


        }
        catch (Exception err)
        {
            errmsg = err.Message;
            //DBConnClose();
            return false;
        }
        return true;
    }

    //出库刷新单项
    public static bool ASRS_OutRefresh(string ids, out string errmsg)
    {
        try
        {
            errmsg = "";
            bool dbflag = false;
            int WmstskId = 0;
            IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
            var caseList = from p in con.Get()
                           where p.ids == ids
                           select p;
            OUTBILL_D entity = caseList.ToList().FirstOrDefault();
            if (string.IsNullOrEmpty(entity.wmstskid.ToString()))
            {
                return true;
            }
            if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
            {
                dbflag = true;
            }
            else
            {
                return false;
            }
            if (dbflag)
            {

                WmstskId = Convert.ToInt32(entity.wmstskid);
                string mststatus = string.Empty;
                if (WmsDBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                {
                    mststatus = errmsg;
                    if (mststatus != "0")
                    {
                        entity.asrs_status = Convert.ToInt32(mststatus);
                        con.Update(entity);
                        con.Save();
                    }

                    DBConnClose();
                    return true;
                }
                else
                {
                    DBConnClose();
                    return false;
                }

                /*
                WmstskId = Convert.ToInt32(entity.WMSTSKID);
                //string mststatus = DBCommon_ASRS.GetCmd_MstStatus(WmstskId);
                string mststatus = string.Empty;
                for (int i = 0; i < entity.ASRS_NUM; i++)
                {
                    if (DBCommon_ASRS.GetCmd_MstStatus(WmstskId - i, out errmsg))
                    {
                        mststatus = errmsg;
                        if (mststatus != "0")
                        {
                            if (mststatus == "7")
                            {
                                if (i == 0)
                                {
                                    entity.ASRS_STATUS = Convert.ToInt32(mststatus);
                                    OUTBILL_DRule.Update(entity);
                                    DBConnClose();
                                    return true;
                                }
                                else
                                {
                                    entity.ASRS_STATUS = Convert.ToInt32(1);
                                    OUTBILL_DRule.Update(entity);
                                    DBConnClose();
                                    return true;
                                }
                            }
                            else
                            {
                                entity.ASRS_STATUS = Convert.ToInt32(mststatus);
                                OUTBILL_DRule.Update(entity);
                                DBConnClose();
                                return true;
                            }
                        }
                    }

                }
                DBConnClose();
                return true;
                */
            }
            else
            {
                return false;
            }
        }
        catch (Exception err)
        {
            errmsg = err.Message;
            return false;
        }
    }

    //出库刷新全部
    public static bool ASRS_OutRefresh_All(string Id, out string errmsg)
    {
        try
        {
            errmsg = "";
            string inbillstatus = string.Empty;
            DataTable tb = GetOutBill_D(Id);
            if (tb != null && tb.Rows.Count > 0)
            {
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    /*
                    string tskid = tb.Rows[i]["WMSTSKID"].ToString().Trim();
                    if (string.IsNullOrEmpty(tskid) == false)
                    {
                        string msg = string.Empty;
                        //tskid 不为空
                        if (DBCommon_ASRS.GetCmd_MstStatus(Convert.ToInt32(tskid), out msg))
                        {
                            if (msg != tb.Rows[i]["ASRS_STATUS"].ToString() && msg != "0")
                            {
                                OUTBILL_DEntity entity = new OUTBILL_DEntity();
                                entity.IDS = tb.Rows[i]["IDS"].ToString();
                                entity.SelectByPKeys();
                                entity.ASRS_STATUS = Convert.ToInt32(msg);
                                OUTBILL_DRule.Update(entity);
                            }
                        }

                    }*/
                    int asrs_num = 0;
                    int.TryParse(tb.Rows[i]["asrs_num"].ToString(), out asrs_num);
                    if (asrs_num < 2)
                    {
                        string tskid = tb.Rows[i]["WMSTSKID"].ToString();
                        if (string.IsNullOrEmpty(tskid) == false)
                        {
                            string msg = string.Empty;
                            //tskid 不为空
                            if (WmsDBCommon_ASRS.GetCmd_MstStatus(Convert.ToInt32(tskid), out msg))
                            {
                                if (msg != tb.Rows[i]["ASRS_STATUS"].ToString() && msg != "0")
                                {
                                    IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
                                    string ids = tb.Rows[i]["IDS"].ToString();
                                    var caseList = from p in con.Get()
                                                   where p.ids == ids
                                                   select p;
                                    OUTBILL_D entity = caseList.ToList().FirstOrDefault();
                                    entity.asrs_status = Convert.ToInt32(msg);
                                    con.Update(entity);
                                    con.Save();
                                }
                            }

                        }
                    }
                    else
                    {
                        string tskid = tb.Rows[i]["WMSTSKID"].ToString();
                        if (string.IsNullOrEmpty(tskid) == false)
                        {
                            string msg = string.Empty;

                            for (int j = 0; j < asrs_num; j++)
                            {
                                //tskid 不为空
                                if (WmsDBCommon_ASRS.GetCmd_MstStatus(Convert.ToInt32(tskid) - j, out msg))
                                {
                                    if (msg != tb.Rows[i]["ASRS_STATUS"].ToString() && msg != "0")
                                    {
                                        if (msg == "7")
                                        {
                                            if (j == 0)
                                            {
                                                IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
                                                string ids = tb.Rows[i]["IDS"].ToString();
                                                var caseList = from p in con.Get()
                                                               where p.ids == ids
                                                               select p;
                                                OUTBILL_D entity = caseList.ToList().FirstOrDefault();
                                               
                                                entity.asrs_status = Convert.ToInt32(msg);
                                                con.Update(entity);
                                                con.Save();
                                            }
                                            else
                                            {
                                                IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
                                                string ids = tb.Rows[i]["IDS"].ToString();
                                                var caseList = from p in con.Get()
                                                               where p.ids == ids
                                                               select p;
                                                OUTBILL_D entity = caseList.ToList().FirstOrDefault();

                                                entity.asrs_status = Convert.ToInt32(1);
                                                con.Update(entity);
                                                con.Save();
                                            }
                                        }
                                        else
                                        {
                                            IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
                                            string ids = tb.Rows[i]["IDS"].ToString();
                                            var caseList = from p in con.Get()
                                                           where p.ids == ids
                                                           select p;
                                            OUTBILL_D entity = caseList.ToList().FirstOrDefault();

                                            entity.asrs_status = Convert.ToInt32(msg);
                                            con.Update(entity);
                                            con.Save();
                                         
                                        }
                                    }
                                }
                            }

                        }
                    }
                }

            }



            return true;
        }
        catch (Exception err)
        {
            errmsg = err.Message;
            return false;
        }

        // return false;
    }

    public static string GetOutbillStatus(string ids)
    {
        string Sql = string.Format(@"select bid.asrs_status from outbill_d bid where bid.ids='{0}'", ids);
        object obj=DBHelp.ExecuteScalar(Sql);
        if(obj!=null)
        return DBHelp.ExecuteScalar(Sql).ToString();
        else
            return "";
    }

    /// <summary>
    /// 出库之后的返库动作
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="errmsg"></param>
    /// <returns></returns>
    public static bool ASRS_InChangeStatus_Out_I(string ids,string LineId, out string errmsg)
    {
        try
        {
            errmsg = "";
            bool dbflag = false;
            string inbillstatus = string.Empty;
            int WmstskId = 0;
            int intNum = 0;
            string str_Space = "0";

            if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
            {
                dbflag = true;
            }
            else
            {
                return false;
            }

            // WL 20160516
            str_Space = GetInbillPallet_Out(ids);
            if (str_Space == "0" || str_Space == "")
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg2 + "!"; //"站点选择错位，请重新选择!";
                DBConnClose();
                return false;
            }

            //测试连接成功
            if (dbflag)
            {
                // WL 201605116 选择标签
                //判断入库单状态，如果是0，则插入数据至中间表
                #region 未处理
                WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
                var caseList = from p in con.Get()
                               where p.ids == ids&&p.isgoback==1
                               select p;
                OUTBILL_D entity = caseList.ToList().FirstOrDefault();
                string cmdno = Getcmdno();
                if (string.IsNullOrEmpty(entity.cposition) || string.IsNullOrEmpty(entity.iquantity.ToString()))
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg6 + "!"; //"储位或数量为空，不能入库";
                    DBConnClose();
                    return false;
                }
                string Loc = "";

                Loc = GetLocXYZ(entity.cpositioncode.Trim());
                if (Loc == "" || Loc.Length < 7)
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg7 + "!"; //"未配置储位的X或Y或Z";
                    DBConnClose();
                    return false;
                }
                //种类
                string LocSize = "";
                LocSize = GetLocSize(entity.cpositioncode.Trim()).Trim();
                if (string.IsNullOrEmpty(LocSize))
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg8 + "!"; //"未配置储位的种类";
                    DBConnClose();
                    return false;
                }
                string CmdSno = GetCmdSno();
                // 判断相邻储位是否有阻挡[判断Y01,Y02上是否有阻挡]
                string strCposition = new OutBillQuery().GetStopCurrent_Out(ids);
                if (strCposition != "")
                {
                    // 获取阻碍储位的Y的值
                    string strCy = new OutBillQuery().strGetCY(strCposition);

                    string strTempCpositon = ""; // 临时储位TEMP000
                    string strTempLoc = ""; //临时储位LOC
                    string strStopLoc = ""; //对面阻止的储位

                    strStopLoc = GetLocXYZ(strCposition);
                    string CZ = strStopLoc.Substring(5, 2);
                    // 判断是否会被阻挡
                    if (GetCmd_Loc(strStopLoc, out errmsg))
                    {
                        DBConnClose();
                        return false;
                    }

                    // 判断相邻储位是否可以移动
                    // 判断相邻储位是否可以移动
                    DataTable dt = inBill.GetAllCurrentByNull(strCy);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int min = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int width = Convert.ToInt32(strStopLoc.Substring(2, 3)) - Convert.ToInt32(dt.Rows[i]["CY"].ToString());
                            int height = Convert.ToInt32(strStopLoc.Substring(strStopLoc.Length - 2)) - Convert.ToInt32(dt.Rows[i]["CZ"].ToString());
                            int result = (width * width) + (height * height);
                            result = Convert.ToInt32(Math.Sqrt(result));
                            if (min == 0)
                            {
                                min = result;
                                strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                            }
                            if (result < min)
                            {
                                min = result;
                                strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(strTempCpositon))
                    {
                        errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg9 + "!"; //"没有多余的移动储位,阻挡储位无法移动！";
                        return false;
                    }
                    strTempLoc = GetLocXYZ(strTempCpositon);

                    // 移动
                    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strStopLoc, strTempLoc, LocSize, 5, str_Space,LineId, cmdno, out errmsg))
                    {
                        UpdateCmd_Pattle_O(strStopLoc);
                        UpdateCmd_Pattle_I(strTempLoc);
                        //DBConnClose();
                        intNum++;
                    }
                    // 入库
                    CmdSno = GetCmdSno();
                    WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space,LineId, cmdno, out errmsg))
                    {
                        UpdateCmd_Pattle_I(Loc);
                        intNum++;
                    }

                    //判断储位上是否有物品，如果有，再把移动过去的棧板再移动回来。
                    if (!new OutBillQuery().CheckStockByStopLoc(strStopLoc))
                    {
                        // 返回
                        CmdSno = GetCmdSno();
                        WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                        if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strTempLoc, strStopLoc, LocSize, 5, str_Space,LineId, cmdno, out errmsg))
                        {
                            //entity.ASRS_STATUS = 1;
                            //entity.WMSTSKID = WmstskId.ToString();
                            //entity.ASRS_NUM = 3;
                            //INBILL_DRule.Update(entity);
                            //if (str_Pallet == "0")
                            //{
                            //    UpdateCmd_Pattle_I(Loc);
                            //    DBConnClose();
                            //}
                            //else
                            //{
                            //    UpdateCmd_Pattle_I(Loc);
                            //    DBConnClose();
                            //}
                            UpdateCmd_Pattle_O(strTempLoc);
                            UpdateCmd_Pattle_I(strStopLoc);
                            //UpdateCmd_Pattle_I(Loc);
                            DBConnClose();
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                    }

                }
                else
                {
                    // 
                    if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space,LineId, cmdno, out errmsg))
                    {
                        UpdateCmd_Pattle_I(Loc);
                        DBConnClose();
                    }
                    else
                    {
                        DBConnClose();
                        return false;
                    }
                    /*
                    if (str_Pallet == "0")
                    {
                        if (DBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space, out errmsg))
                        {

                            UpdateCmd_Pattle_I(Loc);
                            DBConnClose();
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                    }
                    else
                    {
                        if (DBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space, out errmsg))
                        {
                            UpdateCmd_Pattle_I(Loc);
                            DBConnClose();
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }
                    }*/
                }
                #endregion

                #region 注释
                /* 20160517
                    inbillstatus = DBCommon_ASRS.GetOutbillStatus(ids); 
                    if (inbillstatus.Equals("0"))
                    {
                        #region 未处理
                        WmstskId = DBCommon_ASRS.GetWmsTskID();
                        INBILL_DEntity entity = new INBILL_DEntity();

                        BASE_CARGOSPACEEntity entity_Car = new BASE_CARGOSPACEEntity();

                        entity.IDS = ids;
                        entity.SelectByPKeys();
                        if (string.IsNullOrEmpty(entity.CPOSITIONCODE) || string.IsNullOrEmpty(entity.IQUANTITY.ToString()))
                        {
                            errmsg = "储位或数量为空，不能入库";
                            DBConnClose();
                            return false;
                        }
                        string Loc = "";

                        Loc = GetLocXYZ(entity.CPOSITIONCODE.Trim());
                        if (Loc == "" || Loc.Length < 7)
                        {
                            errmsg = "未配置储位的X或Y或Z";
                            DBConnClose();
                            return false;
                        }
                        //种类
                        string LocSize = "";
                        LocSize = GetLocSize(entity.CPOSITIONCODE.Trim()).Trim();
                        if (string.IsNullOrEmpty(LocSize))
                        {
                            errmsg = "未配置储位的种类";
                            DBConnClose();
                            return false;
                        }
                        string CmdSno = GetCmdSno();
                        //string CmdSno = str_Space;

                        // -- 201501216 ADD -- 
                        RD_FrmINBILL_DListQuery listQuery = new RD_FrmINBILL_DListQuery();
                        // 判断是否入储位是否为空
                        //bool blcurrent = listQuery.CheckStockByCurrent(ids);
                        //if (blcurrent)
                        //{
                        //}
                        //else
                        //{
                        //    errmsg = "储位以满,请调整储位!";
                        //    DBConnClose();
                        //    return false;
                        //}

                        // 判断相邻储位是否有阻挡[判断Y01,Y02上是否有阻挡]
                        string strCposition = listQuery.GetStopCurrent_RD(ids);
                        if (strCposition != "")
                        {
                            // 获取阻碍储位的Y的值
                            string strCy = listQuery.strGetCY(strCposition);

                            string strTempCpositon = ""; // 临时储位TEMP000
                            string strTempLoc = ""; //临时储位LOC
                            string strStopLoc = ""; //对面阻止的储位

                            strStopLoc = GetLocXYZ(strCposition);

                            // 判断是否会被阻挡
                            if (GetCmd_Loc(strStopLoc, out errmsg))
                            {
                                DBConnClose();
                                return false;
                            }

                            // 判断相邻储位是否可以移动
                            // 判断相邻储位是否可以移动
                            DataTable dt = listQuery.GetAllCurrentByNull(strCy);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                int min = 0;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    int width = Convert.ToInt32(strStopLoc.Substring(2, 3)) - Convert.ToInt32(dt.Rows[i]["CY"].ToString());
                                    int height = Convert.ToInt32(strStopLoc.Substring(strStopLoc.Length - 2)) - Convert.ToInt32(dt.Rows[i]["CZ"].ToString());
                                    int result = (width * width) + (height * height);
                                    result = Convert.ToInt32(Math.Sqrt(result));
                                    if (min == 0)
                                    {
                                        min = result;
                                        strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                                    }
                                    if (result < min)
                                    {
                                        min = result;
                                        strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                                    }
                                }
                            }
                            strTempLoc = GetLocXYZ(strTempCpositon);

                            // 移动
                            if (DBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strStopLoc, strTempLoc, LocSize, 5, str_Space, out errmsg))
                            {
                                //DBConnClose();
                                intNum++;
                            }
                            // 入库
                            CmdSno = GetCmdSno();
                            WmstskId = DBCommon_ASRS.GetWmsTskID();
                            if (DBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space, out errmsg)) ;
                            {
                                //DBConnClose();
                                intNum++;
                            }
                            // 返回
                            CmdSno = GetCmdSno();
                            WmstskId = DBCommon_ASRS.GetWmsTskID();
                            if (DBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strTempLoc, strStopLoc, LocSize, 5, str_Space, out errmsg))
                            {
                                //entity.ASRS_STATUS = 1;
                                //entity.WMSTSKID = WmstskId.ToString();
                                //entity.ASRS_NUM = 3;
                                //INBILL_DRule.Update(entity);
                                if (str_Pallet == "0")
                                {
                                    UpdateCmd_Pattle_I(Loc);
                                    DBConnClose();
                                }
                                else
                                {
                                    UpdateCmd_Pattle_I(Loc);
                                    DBConnClose();
                                }
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }
                        }
                        else
                        {
                            if (str_Pallet == "0")
                            {
                                if (DBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 1, str_Space, out errmsg))
                                {

                                    UpdateCmd_Pattle_I(Loc);
                                    DBConnClose();
                                }
                                else
                                {
                                    DBConnClose();
                                    return false;
                                }
                            }
                            else
                            {
                                if (DBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", LocSize, 2, str_Space, out errmsg))
                                {
                                    UpdateCmd_Pattle_I(Loc);
                                    DBConnClose();
                                }
                                else
                                {
                                    DBConnClose();
                                    return false;
                                }
                            }
                        }
                        #endregion
                    }
                    else if (inbillstatus.Equals("1"))
                    {
                        #region 运作中
                        INBILL_DEntity entity = new INBILL_DEntity();
                        entity.IDS = ids;
                        entity.SelectByPKeys();
                        WmstskId = Convert.ToInt32(entity.WMSTSKID);
                        string mststatus = string.Empty;

                        if (DBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                        {
                            mststatus = errmsg;
                        }
                        else
                        {
                            DBConnClose();
                            return false;
                        }

                        if (mststatus.Equals("0"))
                        {
                            //entity.ASRS_NUM = 0;
                            // 回滚处理
                            if (entity.ASRS_NUM > 0)
                            {
                                // 第二次判断
                                string mststatus2 = string.Empty;
                                if (DBCommon_ASRS.GetCmd_MstStatus(WmstskId - 2, out errmsg))
                                {
                                    mststatus2 = errmsg;
                                }
                                else
                                {
                                    DBConnClose();
                                    return false;
                                }
                                if (!mststatus2.Equals("0"))
                                {

                                    //更新inbill_d状态
                                    //entity.ASRS_STATUS = Convert.ToInt32(mststatus2);
                                    INBILL_DRule.Update(entity);
                                    DBConnClose();
                                    errmsg = "该明细状态是[执行中]，不能取消";
                                    return false;
                                }

                                for (int i = 1; i < entity.ASRS_NUM; i++)
                                {
                                    if (DBCommon_ASRS.DeleteCmd_Mst(WmstskId - i, out errmsg))
                                    {
                                        //  
                                    }
                                }
                                if (DBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                                {
                                    entity.WMSTSKID = "";
                                    entity.ASRS_STATUS = 0;
                                    entity.ASRS_NUM = 0;
                                    INBILL_DRule.Update(entity);
                                    DBConnClose();
                                }
                                else
                                {
                                    DBConnClose();
                                    return false;
                                }

                            }
                            else
                            {
                                //删除原中间表状态，更新inbill表状态
                                if (DBCommon_ASRS.DeleteCmd_Mst(WmstskId, out errmsg))
                                {
                                    entity.WMSTSKID = "";
                                    entity.ASRS_STATUS = 0;
                                    entity.ASRS_NUM = 0;
                                    INBILL_DRule.Update(entity);
                                    DBConnClose();
                                }
                                else
                                {
                                    DBConnClose();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            //更新inbill_d状态
                            entity.ASRS_STATUS = Convert.ToInt32(mststatus);
                            INBILL_DRule.Update(entity);
                            DBConnClose();
                            errmsg = "该明细状态是[执行中]，不能取消";
                            return false;
                        }

                        #endregion
                    }
                    else if (inbillstatus.Equals("8"))
                    {
                        #region 处理异常
                        //处理异常
                        INBILL_DEntity entity = new INBILL_DEntity();
                        entity.IDS = ids;
                        entity.SelectByPKeys();
                        WmstskId = Convert.ToInt32(entity.WMSTSKID);
                        //string mststatus = DBCommon_ASRS.GetCmd_MstStatus(WmstskId);
                        string mststatus = string.Empty;

                        if (entity.ASRS_NUM == 0)
                        {
                            if (DBCommon_ASRS.GetCmd_MstStatus(WmstskId, out errmsg))
                            {
                                mststatus = errmsg;
                            }
                            else
                            {
                                DBConnClose();
                                return false;
                            }

                            if (mststatus.Equals("8"))
                            {
                                if (DBCommon_ASRS.UpdateCmd_mst(WmstskId, out errmsg))
                                {
                                    entity.ASRS_STATUS = 1;
                                    INBILL_DRule.Update(entity);
                                    DBConnClose();
                                    return true;
                                }
                                else
                                {
                                    DBConnClose();
                                    return false;
                                }
                            }
                        }

                        else
                        {
                            for (int i = 0; i < entity.ASRS_NUM; i++)
                            {
                                if (DBCommon_ASRS.GetCmd_MstStatus(WmstskId - i, out errmsg))
                                {
                                    mststatus = errmsg;
                                }
                                else
                                {
                                    DBConnClose();
                                    return false;
                                }

                                if (mststatus.Equals("8"))
                                {
                                    if (DBCommon_ASRS.UpdateCmd_mst(WmstskId - i, out errmsg))
                                    {
                                        entity.ASRS_STATUS = 1;
                                        INBILL_DRule.Update(entity);
                                        DBConnClose();
                                        return true;
                                    }
                                    else
                                    {
                                        DBConnClose();
                                        return false;
                                    }
                                }
                            }
                        }
                        DBConnClose();
                        return true;
                       
                        #endregion
                    }*/
                #endregion
                DBConnClose();
                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception err)
        {
            errmsg = err.Message;
            DBConnClose();
            return false;
        }
    }

   

   

    //获取出库库单明细选择的站点
    public static string GetInbillPallet_Out(string ids)
    {
        string Sql = string.Format(@"select bid.pallet_code  from outbill_d bid where bid.ids='{0}'", ids);
        object obj=DBHelp.ExecuteScalar(Sql);
        if(obj!=null)
            return DBHelp.ExecuteScalar(Sql).ToString();
        else
            return "";
    }
    #endregion
    #region 公<%=Resources.Lang.Common_TotalPage %>方法


    /// <summary>
    /// 中英文字符串截取，中文占2个字节
    /// </summary>
    /// <param name="stringToSub"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string CutString(string stringToSub, int length)
    {
        char[] stringChar = stringToSub.ToCharArray();
        StringBuilder sb = new StringBuilder();
        int nLength = 0;
        for (int i = 0; i < stringChar.Length; i++)
        {
            if ((int)stringChar[i] > 255 || (int)stringChar[i] < 0)
            {
                nLength += 2;
            }
            else
            {
                nLength = nLength + 1;
            }

            if (nLength <= length)
            {
                sb.Append(stringChar[i]);
            }
            else
            {
                break;
            }
        }
        if (sb.ToString() != stringToSub)
        {
            sb.Append("...");
        }
        return sb.ToString();
    }


    //获取出库单信息
    public static DataTable GetOutBill_D(string id)
    {
        string Sql = string.Format(@"select bid.ids,bid.wmstskid,bid.asrs_status, bid.asrs_num  from outbill_d bid where bid.id='{0}' and bid.wmstskid is not null ", id);
        return DBHelp.ExecuteToDataTable(Sql);
    }


    /// <summary>
    /// 插入中间表数据
    /// </summary>
    /// <param name="wmstskid">WmsTskID</param>
    /// <param name="Loc">出入库使用储位，调拨原储位</param>
    /// <param name="NewLoc">调拨目的储位</param>
    /// <param name="cmdmode">类型1入库2出库3盘点5调拨</param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static bool InsertCmd_Mst(int wmstskid, string CmdSno, string Loc, string NewLoc, string LocSize, int cmdmode, string StnNO,string LineId,string cmdno, out string msg)
    {
        try
        {
            msg = "";
            string sql = string.Format(@"insert into CMD_MST(WmsTskId,CmdSno,CmdSts,PRT,StnNo,CmdMode,Loc,NewLoc,LocSize,RodID,Trace,TrnDate,LineId,cmdno) 
                                              values({0},'{1}',0,'5','{7}',{2},'{3}','{4}','{5}',null,'0','{6}','{8}','{9}')", wmstskid, CmdSno, cmdmode, Loc, NewLoc, LocSize, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), StnNO,LineId,cmdno);
            try
            {
                DBHelp.ExecuteNonQuery(sql);
                return true;
            }
           catch (System.Exception e)
            {
                msg = Resources.Lang.WmsDBCommon_ASRS_Msg11 + "!"; //"插入中间表CMD_MST数据失败";
                return false;
            }
        }
        catch (Exception err)
        {
            msg = err.Message;
            return false;
            }

    }

    //pan gao 20160531
    /// <summary>
    /// 入库时，LED电子屏幕显示相关信息
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="str_Space"></param>
    /// <param name="errmsg"></param>
    /// <returns></returns>
    public static void LED_IN(string ids, string cmdSN, out string errmsg, string filepath = "")
    {
        errmsg = "";
        DataTable dt = new InBill().GetListByIDS(ids);
        if (dt != null && dt.Rows.Count > 0)
        {
            string PALLET_CODE = dt.Rows[0]["PALLET_CODE"].ToString();
            string cinvcode = dt.Rows[0]["cinvcode"].ToString();
            //string cinvname = dt.Rows[0]["cinvname"].ToString();
            string cpositioncode = dt.Rows[0]["cpositioncode"].ToString();
            string iquantity = dt.Rows[0]["iquantity"].ToString();
            //string wmstskid = dt.Rows[0]["wmstskid"].ToString();
            string cticketcode = dt.Rows[0]["cticketcode"].ToString();//入库单号
            int iqt = 0;
            int.TryParse(iquantity, out iqt);

            string info = Resources.Lang.WmsDBCommon_ASRS_Msg12 + "...\r\n" 
                + Resources.Lang.WmsDBCommon_ASRS_Msg15 + ":" + cticketcode 
                + "\r\n"+Resources.Lang.WmsDBCommon_ASRS_Msg18 + cinvcode + "\r\n"
                +Resources.Lang.WmsDBCommon_ASRS_Msg24 + iqt.ToString() + "\r\n"
                + Resources.Lang.WmsDBCommon_ASRS_Msg26 + cpositioncode;
            //"入库中...\r\n入库单号:" + cticketcode + "\r\n物料号:" + cinvcode + "\r\n数量:" + iqt.ToString() + "\r\n储位:" + cpositioncode;

            //pan gao test led 
            //1
            //                string sql = string.Format(@" insert into dbo.MVS_CMD(ap_id,mvs_id,stn_no,cmd_sno,dsp_inf,show_sts,CreateTime) 
            //                                                values('{0}','{1}','{2}','{3}','{4}','{5}','{6}') ",
            //                                                PALLET_CODE, "显示幕ID", PALLET_CODE, cmdSN, info, "0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //                DBCmd = new SqlCommand(sql, DBConn);
            //                int vCount = DBCmd.ExecuteNonQuery();
            //                if (vCount > 0)
            //                {
            //                    errmsg = "OK";
            //                }
            //                else
            //                {
            //                    errmsg = "插入中间表MVS_CMD数据失败";
            //                }

            //2
            string ledWEB = ConfigurationManager.AppSettings["LEDWEB"];
            if (ledWEB == "1")
            {
                WmsLEDSendText ledSend = new WmsLEDSendText(filepath);
                string msg = string.Empty;
                int siteID = 2;
                if (!int.TryParse(PALLET_CODE, out siteID))
                {
                    siteID = 2;
                }

                if (siteID == 2)//站点2是右边的屏幕，硬件设置的ID是1
                {
                    siteID = 1;
                }
                else
                {
                    siteID = 2;
                }

                ledSend.SendTextToLED(siteID, info, out msg);
            }
        }
    }


    /// <summary>
    /// 取消入库
    /// </summary>
    public static void CancelMVS_CMD(int WmstskId, out string errmsg)
    {
        errmsg = "";
        string sql = string.Format(@" delete from dbo.MVS_CMD where cmd_sno in ( select CmdSno from dbo.CMD_MST where wmsTskId = '{0}' ) ", WmstskId.ToString());
        try
        {
            DBHelp.ExecuteNonQuery(sql);
        }
       catch(System.Exception e)
        {
            errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg29 + "!"; //"删除中间表MVS_CMD数据失败";
        }
    }
    // 20151218中间表记录迁移次数
    public static bool InsertCmd_MstNum(int wmstskid, int cmdNum, out string msg)
    {
        try
        {
            msg = "";
            string sql = string.Format(@"insert into CMD_MST_NUM(WmsTskId,cmdNum,TrnDate) 
                                              values({0},'{1}','{2}')", wmstskid, cmdNum, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            try
            {
                DBHelp.ExecuteNonQuery(sql);
                return true;
            }
            catch(System.Exception e)
            {
                msg = Resources.Lang.WmsDBCommon_ASRS_Msg30 + "!"; //"插入中间表CMD_MST_NUM数据失败";
                return false;
            }
        }
        catch (Exception err)
        {
            msg = err.Message;
            return false;
        }

    }

    // 根据WmsTskID，获得[CMD_MST_NUM]
    public static int GetCmdMstNum(string WmsTskID)
    {
        string Sql = string.Format(@" select CmdNum from [dbo].[CMD_MST_NUM] where WmsTskId = '{0}'", WmsTskID);
        DataTable dt = DBHelp.ExecuteToDataTable(Sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }

    // 查询未处理的记录
    public static bool CheckStatusCmd_Mst()
    {
        try
        {
            string Sql = @" select count(1) from [dbo].[CMD_MST] where CmdSts in ('0','1')  ";
            DataTable dt = DBHelp.ExecuteToDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0
                && int.Parse(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception err)
        {
            return false;
        }
    }

    // 查询未处理的记录
    public static bool GetStatusCmd_Mst(string strLoc)
    {
        try
        {
            string Sql = string.Format(@" select * from [dbo].[CMD_MST] where CmdSts in ('0','1') and  loc ='{0}' ", strLoc);
            DataTable dt = DBHelp.ExecuteToDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception err)
        {
            return false;
        }
    }

    //修改状态为未处理
    public static bool UpdateCmd_mst(int wmstskid, out string msg)
    {
        try
        {
            msg = "";
            string sql = @"Update CMD_MST set CmdSts=0  where WmsTskId=" + wmstskid + " ";
            try
            {
                DBHelp.ExecuteNonQuery(sql);
                return true;
            }
            catch (System.Exception e)
            {
                msg = Resources.Lang.WmsDBCommon_ASRS_Msg31 + "!";// "修改中间表CMD_MST数据失败";
                return false;
            }
        }
        catch (Exception err)
        {
            msg = err.Message;
            return false;
        }

    }

    //删除操作
    public static bool DeleteCmd_Mst(int wmstskid, out string msg)
    {
        try
        {
            msg = "";
            string sql = @"Delete from CMD_MST where WmsTskId=" + wmstskid + " ";
            try
            {
                DBHelp.ExecuteNonQuery(sql);
                return true;
            }
           catch(System.Exception e)
            {
                msg = Resources.Lang.WmsDBCommon_ASRS_Msg32 + "!";//"删除中间表CMD_MST数据失败";
                return false;
            }
        }
        catch (Exception err)
        {
            msg = err.Message;
            return false;
        }

    }

    /// <summary>
    /// 获取储位NewLoc
    /// </summary>
    /// <param name="cpositioncode"></param>
    /// <returns></returns>
    public static string GetLocXYZ(string cpositioncode)
    {
        try
        {
            string Sql = string.Format(@"select (bc.cx+bc.cy+bc.cz) GetLocXYZ from base_cargospace bc where bc.cpositioncode='{0}'",
               cpositioncode);
            DataTable dt = DBHelp.ExecuteToDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        catch (Exception)
        {

            return "";
        }

    }

    //获取储位种类
    public static string GetLocSize(string cpositioncode)
    {
        try
        {
            string Sql = string.Format(@"select bc.ctype from base_cargospace bc where bc.cpositioncode='{0}'",
               cpositioncode);
            DataTable dt = DBHelp.ExecuteToDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        catch (Exception)
        {

            return "";
        }
    }

    //查询状态
    public static bool GetCmd_MstStatus(int wmstskid, out string cmdsts)
    {
        try
        {
            cmdsts = "";
            string sql = "SELECT  CmdSts  FROM  CMD_MST  where WmsTskId=" + wmstskid + "";
            DataTable dt = DBHelp.ExecuteToDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                cmdsts = dt.Rows[0][0].ToString();
                return true;
            }
            else
            {
                cmdsts = Resources.Lang.WmsDBCommon_ASRS_Msg33 + "!";//"表中没有数据！";
                return false;
            }
        }
        catch (Exception err)
        {

            cmdsts = err.Message;
            return false;
        }

    }

    //获取WmsTskID
    public static int GetWmsTskID()
    {
        string Sql = "SELECT NEXT VALUE FOR ASRS_SEQ";
        return Convert.ToInt32(DBHelp.ExecuteScalar(Sql));
    }

    //获取CmdSno
    public static string GetCmdSno()
    {
        string Sql = "DECLARE @Temp VARCHAR(4) SET @Temp=NEXT VALUE FOR ASRS_SNO_SEQ SELECT REPLICATE('0',5-len(@Temp))+@Temp CmdSno";
        return DBHelp.ExecuteScalar(Sql).ToString();
    }

    //获取cmdno
    /// <summary>
    /// 一组命令cmdno是相同的
    /// </summary>
    /// <returns></returns>
    public static string Getcmdno()
    {
        string Sql = "select NEXT VALUE FOR dbo.ASRS_cmdno_SEQ";
        return DBHelp.ExecuteScalar(Sql).ToString();
    }

    //测试连接是否正确
    public static bool DataBaseConn(out string msg)
    {
        bool boolTF = true;
        msg = "";
        try
        {
            if (DBConn.State == ConnectionState.Open)
            {
                boolTF = true;
            }
            else
            {
                string ConnStr = WmsDBCommon_ASRS.GetDBConnStr();
                DBConn.ConnectionString = ConnStr;
                DBConn.Open();
            }
        }
        catch (Exception err)
        {
            msg = Resources.Lang.WmsDBCommon_ASRS_Msg34 + "!";// "打开连接失败！";
            msg = err.Message;
            boolTF = false;
        }
        return boolTF;
    }

    //获取查询语句
    public static string GetDBConnStr()
    {
        string dbid = Base_Asrs_db.GetDBID();
        if (string.IsNullOrEmpty(dbid) == false)
        {
            IGenericRepository<BASE_ASRS_DB> con = new GenericRepository<BASE_ASRS_DB>(context);
            var caseList = from p in con.Get()
                           where p.id == dbid
                           select p;
            BASE_ASRS_DB entity = caseList.ToList().FirstOrDefault<BASE_ASRS_DB>();

           string ConnStr = string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3};", entity.db_ip,
                                    entity.db_datebase, entity.account, entity.password);
            return ConnStr;
        }
        return "";
    }

    //关闭数据库
    public static void DBConnClose()
    {
        if (DBConn != null)
        {
            DBConn.Close();
            DBConn.Dispose();
        }
    }

    //查询是否存在需要执行的数据
    public static bool GetCmd_Loc(string strLoc, out string msg)
    {
        try
        {
            msg = "";
            string Sql = string.Format(@" SELECT WmsTskId FROM CMD_MST WHERE CmdSts IN ('0','1') AND Loc ='{0}'
                                                     UNION 
                                                    SELECT WmsTskId FROM CMD_MST WHERE CmdSts IN ('0','1') AND NewLoc ='{0}' ", strLoc);

            DataTable dt = DBHelp.ExecuteToDataTable(Sql);
            if (dt.Rows.Count > 0)
            {
                msg = Resources.Lang.WmsDBCommon_ASRS_Msg35 + "!"; //"暂时无法处理，相关储位被阻挡！";
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception err)
        {
            msg = err.Message;
            return false;
        }

    }

    // 更新储位中占位信息
    public static void UpdateCmd_Pattle_I(string str_Loc)
    {
        try
        {
            var UpdSql = @" update BASE_CARGOSPACE  set pallet_code ='1'  where (cx+cy+cz)  ='" + str_Loc + "'";
            DBHelp.ExecuteNonQuery(UpdSql);
        }
        catch (Exception err)
        {

        }
    }

    public static void UpdateCmd_Pattle_O(string str_Loc)
    {
        try
        {
            var UpdSql = @" update BASE_CARGOSPACE  set pallet_code ='0'  where (cx+cy+cz)  ='" + str_Loc + "'";
            DBHelp.ExecuteNonQuery(UpdSql);
        }
        catch (Exception err)
        {

        }
    }

    //判断目的储位是否存在数据
    public static bool CheckNewLoc(string strNewLoc)
    {
        bool pda = false;
        try
        {
            string Sql = string.Format(@"select a.pallet_code from BASE_CARGOSPACE a where (a.cx+a.cy+a.cz)  = '{0}' ", strNewLoc);
            DataTable dt = DBHelp.ExecuteToDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0][0].ToString()) == "1")
                {
                    pda = true;
                }

            }
        }
        catch (Exception)
        {
            pda = false;
        }
        return pda;
    }

    //pan gao
    #region 空栈板出/入库

    /// <summary>
    /// 空栈板出/入库
    /// cmdMode  1：入库  2：出库
    /// </summary>
    /// <param name="positionCode">储位编码</param>
    /// <param name="errmsg"></param>
    /// <param name="cmdMode">1：入库  2：出库</param>
    /// <returns></returns>
    public static bool EmptyCargospaceInOrOut(string positionCode, out string errmsg, int cmdMode,string LineId,string StnNo)
    {
        bool bl = false;
        errmsg = string.Empty;
        bool dbflag = false;
        int WmstskId = 0;

        //储位上有没有货的判断
        //储位上有没有栈板判断

        if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
        {
            dbflag = true;
        }
        else
        {
            return false;
        }
        //测试连接成功
        if (dbflag)
        {
            //判断是否只有一个ASRS命令在跑
            string IsOnlyRun = GetConFig("300002");
            if (IsOnlyRun == "1")
            {
            if (CheckStatusCmd_Mst())
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg3 + "!";//"有正在执行的AS/RS命令，请稍后再试！";
                DBConnClose();
                return false;
            }
            }

            //检查储位是否存在
            if (!new InBill().CheckBASEExistingCARGOSPACE(positionCode))
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg36 + "!"; //"请输入正确的储位！";
                DBConnClose();
                return false;
            }

            //出库时，出库储位上不能有货物
            if (cmdMode == 2 && new Stock_CurrentQuery().CheckStockIncludedInfo(positionCode))
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg37 + "!"; //"出库储位上不能有货物！";
                DBConnClose();
                return false;
            }
            //出库时，出库储位上不能没有栈板
            if (cmdMode == 2 && new InBill().CheckBASE_CARGOSPACE(positionCode))
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg38 + "!"; //"出库储位上没有栈板！";
                DBConnClose();
                return false;
            }

            //入库时，储位上不能有栈板
            if (cmdMode == 1 && !new InBill().CheckBASE_CARGOSPACE(positionCode))
            {
                errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg39 + "!"; // "入库储位上有栈板！";
                DBConnClose();
                return false;
            }



            WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
            string CmdSno = GetCmdSno();
            string Loc = GetLocXYZ(positionCode);
            string cmdno = Getcmdno();
            //1.移走阻碍的储位--判断相邻储位是否有阻挡
            string strStopLoc = GetStopLocByLoc(Loc);
            string strTempCpositon = string.Empty;
            string strTempLoc = string.Empty;
            if (!string.IsNullOrEmpty(strStopLoc))
            {
                // 判断相邻储位是否可以移动
                DataTable dt = inBill.GetAllCurrentByNull("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    int min = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (strStopLoc != dt.Rows[i]["CPOSITIONCODE"].ToString())
                        {
                        int width = Convert.ToInt32(strStopLoc.Substring(2, 3)) - Convert.ToInt32(dt.Rows[i]["CY"].ToString());
                        int height = Convert.ToInt32(strStopLoc.Substring(strStopLoc.Length - 2)) - Convert.ToInt32(dt.Rows[i]["CZ"].ToString());
                        int result = (width * width) + (height * height);
                        result = Convert.ToInt32(Math.Sqrt(result));
                        if (min == 0)
                        {
                            min = result;
                            strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                        }
                        if (result < min)
                        {
                            min = result;
                            strTempCpositon = dt.Rows[i]["CPOSITIONCODE"].ToString();
                        }
                    }
                }
                }
                if (string.IsNullOrEmpty(strTempCpositon))
                {
                    errmsg = Resources.Lang.WmsDBCommon_ASRS_Msg9 + "!"; //"没有多余的移动储位,阻挡储位无法移动！";
                    return false;
                }
                strTempLoc = GetLocXYZ(strTempCpositon);
                // 移动阻碍的储位
                if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strStopLoc, strTempLoc, "", 5, StnNo,LineId,cmdno, out errmsg))
                {
                    UpdateCmd_Pattle_I(strTempLoc);
                    UpdateCmd_Pattle_O(strStopLoc);
                }
            }

            //2.入库/出库
            WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
            CmdSno = GetCmdSno();
            if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, Loc, "", "", cmdMode, StnNo,LineId,cmdno, out errmsg))
            {
                if (cmdMode == 1)
                {
                    UpdateCmd_Pattle_I(Loc);
                }
                else
                {
                    UpdateCmd_Pattle_O(Loc);
                }

            }

            //3.返库，如果不是空栈板
            //pan gao
            //判断储位上是否有物品，如果有，再把移动过去的棧板再移动回来。
            if (strTempLoc != string.Empty && !string.IsNullOrEmpty(strStopLoc)
                && !new InBill().CheckStockByStopLoc(strStopLoc))
            {
                //CmdSno = GetCmdSno();
                //WmstskId = WmsDBCommon_ASRS.GetWmsTskID();
                //if (WmsDBCommon_ASRS.InsertCmd_Mst(WmstskId, CmdSno, strTempLoc, strStopLoc, "", 5, "1", out errmsg))
                //{
                //    UpdateCmd_Pattle_I(strStopLoc);
                //    UpdateCmd_Pattle_O(strTempLoc);
                //}
                //永佳改为调拨
                string StopLocPosition = GetPositionByXYZ(strStopLoc);
                string TempLocPosition = GetPositionByXYZ(strTempLoc);
                PROC_AUTO_ALLOCATE prc = new PROC_AUTO_ALLOCATE();
                prc.P_PositonFrom = StopLocPosition;
                prc.P_PositonTo = TempLocPosition;
                prc.P_UserNo = "WMS";
                prc.Execute();
            }

            DBConnClose();
            return true;

        }

        return bl;
    }

    public static string GetPositionByXYZ(string xyz)
    {
        try
        {
            string Sql = string.Format(@"select cpositioncode  GetLocXYZ from base_cargospace  where (cx+cy+cz) ='{0}'", xyz);
            DataTable tb = new DataTable();
            object posit = DBHelp.ExecuteScalar(Sql);

            if (posit != null && posit.ToString().Length > 0)
            {
                return posit.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        catch (Exception)
        {

            return "";
        }

    }

    // 活动所有空的储位 
    public static bool IsEmptyLoc(string strStopLoc)
    {
        string Sql = string.Format(@" select CPOSITIONCODE from BASE_CARGOSPACE  
                                          where   PALLET_CODE = '0' and (cx+cy+cz)='{0}'", strStopLoc);
        DataTable dtstrStopLoc = DBHelp.ExecuteToDataTable(Sql);
        if (dtstrStopLoc.Rows.Count > 0)
            return false;
        else
            return true;
    }

    /// <summary>
    /// 根据储位获取其阻碍的储位
    /// </summary>
    /// <returns></returns>
    public static string GetStopLocByLoc(string Loc)
    {
        string stopLoc = string.Empty;
        try
        {
            //只有3列和4列才有阻碍，对应的阻碍时01和02
            if (Loc.Length == 7 && Loc.Substring(0, 2) == "03")
            {
                stopLoc = "01" + Loc.Substring(2, 5);
            }
            else if (Loc.Length == 7 && Loc.Substring(0, 2) == "04")
            {
                stopLoc = "02" + Loc.Substring(2, 5);
            }
        }
        catch { }
        return stopLoc;
    }
    //判断出库单数量与明细中的数量是否相等
    //pan gao 20160614
    public static bool InQuantityCheck(string ids)
    {
        bool bl = true;
        string Sql= string.Format(@"SELECT ibd.id FROM inbill_d ibd inner JOIN 
(SELECT isnull(SUM(QUANTITY),0) AS IQUANTITY,inbill_d_ids FROM inbill_d_sn GROUP BY inbill_d_ids) 
ibdsn ON ibd.ids=ibdsn.inbill_d_ids AND ISNULL(ibd.IQUANTITY,0)=ISNULL(ibdsn.IQUANTITY,0)
AND ibd.ids='{0}'", ids);
        DataTable dt = DBHelp.ExecuteToDataTable(Sql);
        if (dt.Rows.Count==0)
        {
            bl = false;
        }
        return bl;
    }
      //判断出库单数量与明细中的数量是否相等
        //pan gao 20160614
        public static bool OutQuantityCheck(string ids)
        { 
        bool bl = true;
        string Sql= string.Format(@"SELECT obd.id FROM outbill_d obd inner JOIN 
(SELECT isnull(SUM(QUANTITY),0) AS IQUANTITY,outbill_d_ids FROM outbill_d_sn GROUP BY outbill_d_ids) 
obdsn ON obd.ids=obdsn.outbill_d_ids AND ISNULL(obd.IQUANTITY,0)=ISNULL(obdsn.IQUANTITY,0)
AND obd.ids='{0}'", ids);
        DataTable dt = DBHelp.ExecuteToDataTable(Sql);
        if (dt.Rows.Count==0)
        {
            bl = false;
        }
        return bl;
        }
    #endregion      

        /// <summary>
        /// 必须有2个空储位，且没有栈板
        /// true:有
        /// false:没有
        /// </summary>
        /// <returns></returns>
        public static bool HasEmptyCargo()
        {
            bool bl = true;
            string sql = @" select count(1) from base_cargospace bc where bc.cpositioncode not in (
                                select sc.cpositioncode from stock_current sc
                                ) and bc.pallet_code='0' ";
            var count = int.Parse(DBHelp.ExecuteScalar(sql).ToString());
            if (count < 2)
            {
                bl = false;
            }
            return bl;
        }

        //获取调拨单明细asrs状态
        public static string GetAlloStatus(string ids)
        {
            string Sql = string.Format(@"select bid.asrs_status from allocate_d bid where bid.ids='{0}'", ids);
            return DBHelp.ExecuteScalar(Sql).ToString();
        }

        //获取调拨单asrs状态
        public static string GetAlloMainStatus(string ids)
        {
            string Sql = string.Format(@"select CSTATUS from ALLOCATE  WHERE ID IN ( SELECT ID FROM ALLOCATE_D WHERE IDS='{0}' )", ids);
            return DBHelp.ExecuteScalar(Sql).ToString();
        }

        // 根据储位判断是不有正在执行的asrs指令
        public static bool CheckStatusCmd_MstByCwCode(string CwCode)
        {
            try
            {
                string Sql = string.Format(@" select count(1) from CMD_MST where CmdSts in ('0','1') and (LOC='{0}'  or NEWLOC='{1}')  ", CwCode, CwCode);

                DataTable dt = new DataTable();
                dt = DBHelp.ExecuteToDataTable(Sql);
                if (dt != null && dt.Rows.Count > 0
                    && int.Parse(dt.Rows[0][0].ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                return false;
            }
        }


        

    #endregion
}