using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using DreamTek.ASRS.Business.Base;
using DreamTek.ASRS.Business.RD;
using DreamTek.ASRS.Business;
using DreamTek.ASRS.Business.Allocate;
using DreamTek.ASRS.Business.SP.ProcedureModel;
using DreamTek.ASRS.Business.Others;

/*
Roger
2013/6/17 11:28:10
20130617112810
普通出库单不允许占用线边储位出库
*/
/*
Roger
2013/6/24 15:24:04
20130624152404
wip return 数量增加卡控
*/
/*
Roger
2013/7/31 13:46:49
20130731134649
增加校验是否为正在修改中的料号
*
/*
Roger
2013/10/28 9:17:27
20131028091727
增加变更通知单处理
*/
/*
Roger
2014/1/9 13:26:34
20140109132634
特殊wip return不允许修改数量
*/
public partial class Apps_BASE_SubmitDate : System.Web.UI.Page
{
    public DBContext db = new DBContext();
    public BaseCommQuery qry = new BaseCommQuery();
    public INQuery inQry = new INQuery();
    public OutQuery outQry = new OutQuery();

    protected void Page_Load(object sender, EventArgs e)
    {
        //提交数量
        if (!IsPostBack)
        {
            //类型 ： In入，Out出
            string Type = Request.QueryString["Type"].Trim();
            string DataType = Request.QueryString["DataType"].Trim();
            string Ids = Request.QueryString["Ids"].Trim();
            string Qty = Request.QueryString["Qty"].Trim();
            string Line_Qty = Request.QueryString["Line_Qty"].Trim();
            string PositionCode = Request.QueryString["PositionCode"].Trim();
            string msg = Resources.Lang.SubmitDate_Msg01; //提交失败！

            if (Type.Equals("In"))
            {
                #region 入库检查
                //20140109132634
                //INBILL_DEntity entity = new INBILL_DEntity();
                //entity.IDS = Ids;
                //entity.SelectByPKeys();

                IGenericRepository<INBILL_D> conn = new GenericRepository<INBILL_D>(db);
                INBILL_D entity = (from p in conn.Get()
                                   where p.ids == Ids
                                   select p).FirstOrDefault();

                //var inbillEntity = new INBILLEntity().GetINBILLEntityById(entity.ID);

                IGenericRepository<INBILL> connM = new GenericRepository<INBILL>(db);
                INBILL inbillEntity = (from p in connM.Get()
                                       where p.id == entity.id
                                       select p).FirstOrDefault();

                //特殊wip return
                IGenericRepository<INASN> connAsn = new GenericRepository<INASN>(db);
                INASN asnEntity = (from p in connAsn.Get()
                                       where p.id == inbillEntity.casnid && p.specil_return==1
                                       select p).FirstOrDefault();

                //var isSpecialReturn = INASNRule.isSpecialWipReturn(inbillEntity.CASNID);
                bool isSpecialReturn = (asnEntity != null && !asnEntity.id.IsNullOrEmpty());
                if (isSpecialReturn)
                {
                    Qty = entity.iquantity.ToString();
                }
                if (isSpecialReturn && DataType.Equals("Qty"))
                {
                    msg = Resources.Lang.SubmitDate_Msg02; //特殊Wip Return不允许修改数量
                }
                else
                {
                    //20130731134649 校验是否存在修改中的料
                    //var result = CommonFunction.CanModDebit("", "", "6", Ids, "", "");
                    var result = qry.CanModDebit("", "", "6", Ids, "", "");

                    if (!result.Equals("1"))
                    {
                        msg = result;
                    }
                    else
                    {
                        #region 入库单检查

                        try
                        {
                            //20130624152404
                            //var WipReturnFlag = INBILL_DRule.IsWipReturn(Ids);
                            var WipReturnFlag = qry.IsWipReturn(Ids);
                            if (WipReturnFlag) //WipReturn单独校验
                            {
                                string checkMsg1 = "";
                                string checkMsg2 = "";

                                if (DataType == "Qty")
                                {
                                    checkMsg1 = CheckInData("Qty", Ids, Qty, PositionCode); //判断数量
                                    if (PositionCode != "")
                                    {
                                        checkMsg2 = CheckInData("PositionCode", Ids, Qty, PositionCode); //判断储位
                                    }
                                }
                                else
                                {
                                    checkMsg2 = CheckInData("PositionCode", Ids, Qty, PositionCode); //判断储位
                                    if (Qty != "")
                                    {
                                        checkMsg1 = CheckInData("PositionCodeQty", Ids, Qty, PositionCode); //判断数量
                                    }
                                }

                                if (checkMsg1.Length > 0) //数量存在问题
                                {
                                    msg = checkMsg1;
                                }
                                else
                                {
                                    if (checkMsg2.Length > 0) //储位存在问题
                                    {
                                        msg = checkMsg2;
                                    }
                                    else
                                    {
                                        var Info = string.Empty;
                                        switch (DataType) //确认提示信息
                                        {
                                            case "Qty": //数量
                                                Info = "[" + Qty + "]" + Resources.Lang.Common_IQUANTITY; //数量
                                                break;
                                            case "PositionCode": //儲位編碼
                                                Info = "[" + PositionCode + "]" + Resources.Lang.FrmBASE_OPERATOR_AREAEdit_lblCCARGOID; //储位编码
                                                break;
                                        }
                                        //if (INBILL_DRule.UpdateInBill_DByIds(Ids, Convert.ToDecimal(Qty), PositionCode))
                                        if (outQry.UpdateInBill_DByIds(Ids, Convert.ToDecimal(Qty), PositionCode))
                                        {
                                            msg = Info + Resources.Lang.SubmitDate_Msg03;//提交成功！
                                        }
                                        else
                                        {
                                            msg = Info + Resources.Lang.SubmitDate_Msg01;//提交失败！
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string checkMsg = CheckInData(DataType, Ids, Qty, PositionCode);
                                if (checkMsg.Length > 0)
                                {
                                    msg = checkMsg;
                                }
                                else
                                {
                                    switch (DataType)
                                    {
                                        case "Qty": //数量
                                            //if (INBILL_DRule.UpdateInBill_DByIds(Ids, Convert.ToDecimal(Qty), null))
                                            if (inQry.UpdateInBill_DByIds(Ids, Convert.ToDecimal(Qty), null))
                                            {
                                                msg = "[" + Qty + "]" + Resources.Lang.Common_IQUANTITY + Resources.Lang.SubmitDate_Msg03;//数量 提交成功！
                                            }
                                            else
                                            {
                                                msg = "[" + Qty + "]" + Resources.Lang.Common_IQUANTITY + Resources.Lang.SubmitDate_Msg03;//数量 提交成功！
                                            }
                                            break;
                                        case "PositionCode": //儲位編碼
                                            if (inQry.UpdateInBill_DByIds(Ids, 0, PositionCode))
                                            {
                                                msg = "[" + PositionCode + "]"+ Resources.Lang.FrmBASE_OPERATOR_AREAEdit_lblCCARGOID +  Resources.Lang.SubmitDate_Msg03;//储位编码 提交成功！
                                            }
                                            else
                                            {
                                                msg = "[" + PositionCode + "]"+ Resources.Lang.FrmBASE_OPERATOR_AREAEdit_lblCCARGOID + Resources.Lang.SubmitDate_Msg01;//储位编码 提交失败！
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            msg = Resources.Lang.SubmitDate_Msg04;//提交异常！
                        }

                        #endregion
                    }
                } 
                #endregion
            }
            else if (Type.Equals("ALLO"))//CQ 20130618 特殊调拨单
            {
                #region 特殊调拨单检查
                try
                {
                    AllocateQuery allQry = new AllocateQuery();
                    string checkMsg = CheckAlloData(DataType, Ids, Qty, PositionCode);
                    if (checkMsg.Length > 0)
                    {
                        msg = checkMsg;
                    }
                    else
                    {
                        string errmsg = string.Empty;
                        switch (DataType)
                        {
                            case "Qty"://数量
                                #region MyRegion
                                //if (ALLOCATE_DRule.UpdateAllocate_DByIds(Ids, Convert.ToDecimal(Qty), null))
                                //{
                                //    msg = "[" + Qty + "]数量 提交成功！";
                                //}
                                //else
                                //{
                                //    msg = "[" + Qty + "]数量 提交失败！";
                                //} 
                                #endregion
                                
                                if (allQry.Run_Proc_SpecialAllo_Update(Ids, Convert.ToDecimal(Qty), "", WmsWebUserInfo.GetCurrentUser().UserNo, ref errmsg))
                                {
                                    msg = "[" + Qty + "]" + Resources.Lang.Common_IQUANTITY + Resources.Lang.SubmitDate_Msg03; //数量 提交成功！
                                }
                                else
                                {
                                    msg = errmsg;
                                }
                                break;
                            case "PositionCode"://儲位編碼

                                #region MyRegion
                                //if (ALLOCATE_DRule.UpdateAllocate_DByIds(Ids, -1, PositionCode))
                                //{
                                //    msg = "[" + PositionCode + "]儲位編碼 提交成功！";
                                //}
                                //else
                                //{
                                //    msg = "[" + PositionCode + "]儲位編碼 提交失败！";
                                //} 
                                #endregion

                                if (allQry.Run_Proc_SpecialAllo_Update(Ids, -1, PositionCode, WmsWebUserInfo.GetCurrentUser().UserNo, ref errmsg))
                                {
                                    msg = "[" + PositionCode + "]"+ Resources.Lang.FrmBASE_OPERATOR_AREAEdit_lblCCARGOID + Resources.Lang.SubmitDate_Msg03; //储位编码 提交成功！
                                }
                                else
                                {
                                    msg = errmsg;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception)
                {
                    msg = Resources.Lang.SubmitDate_Msg04;//提交异常！
                }
                #endregion
            }
            else if (Type.Equals("OutChange") || Type.Equals("InChange"))//Roger 20131028091727 通知单变更
            {
                #region 通知单变更
                try
                {
                    switch (DataType)
                    {
                        case "Qty":
                            //提交数量

                            //var Result = OUT_FrmOutChangeCtl.UpdateAsnChange_DByIds(Ids, Convert.ToDecimal(Qty), WmsWebUserInfo.GetCurrentUser().UserNo, Type);
                            //if (Result.Equals("OK"))
                            //{
                            //    msg = "[" + Qty + "]数量 提交成功！";
                            //}
                            //else
                            //{
                            //    msg = "[" + Qty + "]数量 提交失败:" + Result + "！";
                            //}

                            var Result = outQry.UpdateAsnChange_DByIds(Ids, Convert.ToDecimal(Qty), WmsWebUserInfo.GetCurrentUser().UserNo, Type);
                            if (Result.Equals("OK"))
                            {
                                msg = "[" + Qty + "]" + Resources.Lang.Common_IQUANTITY + Resources.Lang.SubmitDate_Msg03; //数量 提交成功！
                            }
                            else
                            {
                                msg = "[" + Qty + "]" + Resources.Lang.Common_IQUANTITY + Resources.Lang.SubmitDate_Msg01 + ":" + Result + "！"; //数量 提交失败
                            }

                            break;
                        default:
                            msg = Resources.Lang.SubmitDate_Msg08 +"[" + DataType + "]";//传入DateType有误
                            break;
                    }
                }
                catch (Exception ex)
                {
                    msg = Resources.Lang.SubmitDate_Msg04 + "(" + ex.Message + ")！";//提交异常
                } 
                #endregion
            }
            else
            {
                #region 出库单检查
                //20130731134649 校验是否存在修改中的料
                var result = qry.CanModDebit("", "", "5", Ids, "", "");
                if (!result.Equals("1"))
                {
                    msg = result;
                }
                else
                {
                    #region 出库单检查

                    try
                    {
                        string checkMsg = CheckOutData(Ids, DataType, Qty, Line_Qty, PositionCode);
                        if (checkMsg.Length > 0)
                        {
                            msg = checkMsg;
                        }
                        else
                        {
                            switch (DataType)
                            {
                                case "Qty": //数量  20130617112810 临时注销 Roger
                                    //DBUtil.BeginTrans();
                                    //var procPHL = new proc_check_outbill_dqty
                                    //{
                                    //    pID = Ids,
                                    //    pUserNo = WebUserInfo.GetCurrentUser().UserNo,
                                    //    pQty = Qty
                                    //};
                                    //procPHL.Execute();

                                    var procPHL = new Proc_Check_OutBill_DQty
                                    {
                                        pID = Ids,
                                        pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo,
                                        pQty = Qty,
                                        pReserveField2 = string.Empty,
                                        pRetCode = string.Empty,
                                        pRetMsg = string.Empty
                                    };
                                    procPHL.Execute();

                                    if (procPHL.ReturnValue ==1) //非1-校验失败
                                    {
                                        //DBUtil.Rollback();
                                        msg = "[" + Qty + "]" + Resources.Lang.Common_IQUANTITY + Resources.Lang.SubmitDate_Msg01 + "[" + procPHL.ErrorMessage + "]"; //数量 提交失败！
                                    }
                                    else
                                    {
                                        //DBUtil.Commit();
                                        msg = "[" + Qty + "]" + Resources.Lang.Common_IQUANTITY + Resources.Lang.SubmitDate_Msg03;//数量 提交成功！
                                    }
                                    //if (OUTBILL_DRule.UpdateOutBill_DByIds(Ids, Convert.ToDecimal(Qty), "", null))
                                    //{
                                    //    msg = "[" + Qty + "]数量 提交成功！";
                                    //}
                                    //else
                                    //{
                                    //    msg = "[" + Qty + "]数量 提交失败！";
                                    //}
                                    break;
                                case "Line_Qty": //超發數
                                    if (outQry.UpdateOutBill_DByIds(Ids, 0, Line_Qty, PositionCode))
                                    {
                                        if (Line_Qty != "")
                                        {
                                            msg = "[" + Line_Qty + "]" + Resources.Lang.SubmitDate_Msg05+ Resources.Lang.SubmitDate_Msg03;//超發數 提交成功！
                                        }
                                        else
                                        {
                                            msg = Resources.Lang.SubmitDate_Msg05 + Resources.Lang.SubmitDate_Msg07;//超發數 清除成功！
                                        }
                                    }
                                    else
                                    {
                                        if (Line_Qty != "")
                                        {
                                            msg = "[" + Line_Qty + "]" + Resources.Lang.SubmitDate_Msg05 + Resources.Lang.SubmitDate_Msg01;//超發數 提交失败！
                                        }
                                        else
                                        {
                                            msg =  Resources.Lang.SubmitDate_Msg05 + Resources.Lang.SubmitDate_Msg06;//超發數 清除失败！
                                        }
                                    }
                                    break;
                                case "PositionCode": //儲位編碼
                                    if (outQry.UpdateOutBill_DByIds(Ids, 0, "", PositionCode))
                                    {
                                        msg = "[" + PositionCode + "]"+ Resources.Lang.FrmBASE_OPERATOR_AREAEdit_lblCCARGOID + Resources.Lang.SubmitDate_Msg03;//儲位編碼 提交成功！
                                    }
                                    else
                                    {
                                        msg = "[" + PositionCode + "]" + Resources.Lang.FrmBASE_OPERATOR_AREAEdit_lblCCARGOID + Resources.Lang.SubmitDate_Msg01;//儲位編碼 提交失败！
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        msg = Resources.Lang.SubmitDate_Msg04;//提交异常！
                    }

                    #endregion
                } 
                #endregion
            }
            Response.Write(msg);
            Response.End();
        }
    }

    /// 入库验证
    /// <summary>
    /// 入库验证
    /// </summary>
    /// <param name="DataType"></param>
    /// <param name="ids"></param>
    /// <param name="Qty"></param>
    /// <param name="PositionCode"></param>
    /// <returns></returns>
    public string  CheckInData(string DataType, string ids, string Qty, string PositionCode)
    {
        string msg = string.Empty;
        ///检查状态是否是未处理状态
        //if (!(INBILL_DRule.CheckStatus("IN", ids)))
        if (!(inQry.CheckStatus("IN", ids)))
        {
            return Resources.Lang.SubmitDate_Msg09;//不是未处理状态不能修改！
        }
        switch (DataType)
        {
            case "PositionCodeQty":
            case "Qty"://数量
                if (Qty == "")
                {
                    return Resources.Lang.SubmitDate_Msg10;//数量不能空 ！
                }

                //检查数量，不允许小数，负数，0 CQ 2014-2-13 13:39:48
                string errmsg = string.Empty;
                //if (!(Comm_Function.Fun_IsDecimal(Qty, 0, 0, 0, out errmsg)))
                if (!(qry.Fun_IsDecimal(Qty, 0, 0, 0, out errmsg)))
                {
                    return errmsg;
                }

                #region 注销
                //try
                //{
                //    decimal strMessageIQUANTITY = Convert.ToDecimal(Qty);
                //    if (strMessageIQUANTITY <= 0)
                //    {
                //        return "数量不能小于等于 0 ！";
                //    }
                //}
                //catch (Exception)
                //{
                //    return "数量不是有效的十进制数字！[" + Qty + "]";
                //} 
                #endregion

                //出库数量不能大于出库通知单的数量
                msg = inQry.ValidateInQty_Ajax(ids, Qty);
                //if (!msg.Equals("OK"))
                if (!msg.Equals("0"))//Roger 2013-5-3 13:34:33
                {
                    if (msg.Equals("1"))
                    {
                        return "[" + Qty + "]" + Resources.Lang.SubmitDate_Msg16;//大于通知单明细数量
                    }
                    else
                    {
                        return msg;
                    }
                }

                if (DataType != "PositionCodeQty")
                {
                    //Roger 2013/11/28 14:02:35 SN整合 如果存在SN，则必须先删除SN才可以修改数量
                    var boolmsg = inQry.ExistSN(ids);
                    if (boolmsg)
                    {
                        return "[" + ids + "]" + Resources.Lang.SubmitDate_Msg17;//已经存在对应SN信息，请先删除！
                    }
                }

                break;
            case "PositionCode"://儲位編碼
                if (PositionCode == "")
                {
                    return Resources.Lang.SubmitDate_Msg11;//儲位編碼不能空 ！
                }
                //验证储位是否存在
                BASE_CARGOSPACE_Query bcQry = new BASE_CARGOSPACE_Query();
                BASE_CARGOSPACE bcEntity = bcQry.GetBaseCargoByCode(PositionCode);

                //if (BASE_CARGOSPACERule.GetCpositionByCpositionCode(PositionCode).Trim().Length == 0)
                if(bcEntity != null && !bcEntity.id.IsNullOrEmpty())
                {
                    return "[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg12;//儲位編碼不存在 ！
                }
                //判断储位是否是冻结
                //BASE_CARGOSPACERule da = new BASE_CARGOSPACERule();
                //if (da.CheckCpositionStatus(PositionCode.Trim().ToUpper(), "1"))
                if (bcEntity != null && bcEntity.cstatus.Equals("1"))
                {
                    return "[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg13;//儲位編碼状态为[冻结],不能提交！
                }

                //20140109132634 

                IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(db);
                INBILL_D entity = (from p in con.Get()
                                   where p.ids == ids
                                  select p).SingleOrDefault();

                //INBILL_DEntity entity = new INBILL_DEntity();
                //entity.IDS = ids;
                //entity.SelectByPKeys();

                //var inbillEntity = new INBILLEntity().GetINBILLEntityById(entity.ID);
                IGenericRepository<INBILL> connM = new GenericRepository<INBILL>(db);
                INBILL inbillEntity = (from p in connM.Get()
                                       where p.id == entity.id
                                       select p).FirstOrDefault();

                IGenericRepository<INASN> connAsn = new GenericRepository<INASN>(db);
                INASN asnEntity = (from p in connAsn.Get()
                                       where p.id == inbillEntity.casnid && p.specil_return==1
                                       select p).FirstOrDefault();

                
                bool isSpecialReturn = (asnEntity != null && !asnEntity.id.IsNullOrEmpty());

                //特殊wip return
                //var isSpecialReturn = INASNRule.isSpecialWipReturn(inbillEntity.CASNID);
                if (isSpecialReturn)//判断储位是否输入理想储位类型
                {
                    if (!inQry.IsSimilarPositionCode(PositionCode, entity.cmemo))
                    {
                        return Resources.Lang.SubmitDate_Msg18 + entity.cmemo + Resources.Lang.FrmBASE_CARGOSPACEList_Mag05 + "，[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg19;//此明细需求" + entity.cmemo + "储位，[" + PositionCode + "]不为此类型储位,不能提交！
                    }
                }
                //检查料号储位 CQ 2014-2-20 17:26:11
                string StrCinv = entity.cinvcode;
                string vResult = string.Empty;
                if (!(qry.Fun_IsControl_Area(StrCinv, PositionCode, WmsWebUserInfo.GetCurrentUser().UserNo, 0, "", "", out vResult)))
                {
                    return vResult;
                }
                //检查是否是RMA整新单据入库 CQ 2014-8-22 17:37:28
                string inbill_ID = entity.id;
                string vReturnValue = string.Empty;
                if (!(Comm_Function.Fun_RMA_To_WareHouse(inbill_ID, PositionCode, WmsWebUserInfo.GetCurrentUser().UserNo, "","", out vReturnValue)))
                {
                    return vReturnValue;
                }


                //判退单据的储位选择验证
                decimal InQty = 0;
                if (Qty.Length > 0)
                {
                    InQty = Convert.ToDecimal(Qty);
                }
                //杂入、验证入库选择的储位是否是可用(保税，非保税) PO Receipt=18 
                //WIP Return=43 
                msg = inQry.CheckSelectCARGOSPACE(ids, PositionCode, InQty).Trim();
                if (!msg.Equals("0"))
                {
                    return "[" + PositionCode + "]" + msg;
                }
                break;
            default:
                return Resources.Lang.SubmitDate_Msg20;//数据类型异常 ！
        }

        return "";
    }

    ///特殊调拨单修改
    /// <summary>
    /// 特殊调拨单修改
    /// </summary>
    /// <param name="DataType"></param>
    /// <param name="ids"></param>
    /// <param name="Qty"></param>
    /// <param name="PositionCode"></param>
    /// <returns></returns>
    public string CheckAlloData(string DataType, string ids, string Qty, string PositionCode)
    {

        string msg = string.Empty;

        ///检查状态是否是未处理状态
        //if (!(ALLOCATERule.CheckAlloStatus(ids, "0")))
        //{
        //    return "不是未处理状态不能修改！";
        //}

        //CQ 2013-6-28 13:35:07
        string status = "";

        IGenericRepository<ALLOCATE> connM = new GenericRepository<ALLOCATE>(db);
        IGenericRepository<ALLOCATE_D> connD = new GenericRepository<ALLOCATE_D>(db);
        status = (from p in connM.Get()
                      join d in connD.Get() on p.id equals d.id
                      where d.ids == ids
                      select p.cstatus
                          ).SingleOrDefault();

        //status = ALLOCATERule.GetAlloStatus(ids);
        if (status == null)
        {
            return Resources.Lang.SubmitDate_Msg21; //ids不正确
        }
        if (!(status.Equals("0")))
        {
            if (status.Equals("1"))
            {
                
            }
            else
            {
                return Resources.Lang.SubmitDate_Msg09;//不是未处理状态不能修改！
            }
        }

        switch (DataType)
        {
            case "Qty"://数量
                if (Qty == "")
                {
                    return Resources.Lang.SubmitDate_Msg10;//数量不能空 ！
                }
                //检查数量，不允许小数，负数 CQ 2014-2-13 13:39:48
                string errmsg = string.Empty;
                if (!(qry.Fun_IsDecimal(Qty, 0, 1, 0, out errmsg)))
                {
                    return errmsg;
                }

                #region 注销
                //try
                //{
                //    decimal strMessageIQUANTITY = Convert.ToDecimal(Qty);
                //    if (strMessageIQUANTITY < 0)
                //    {
                //        return "数量不能小于 0 ！";
                //    }
                //}
                //catch (Exception)
                //{
                //    return "数量不是有效的十进制数字！[" + Qty + "]";
                //} 
                #endregion

                //调拨数量不能大于可调拨数量
                msg = qry.CheckAllocateNum(ids, Qty, 0);
                if (!msg.Equals("0"))
                {
                    return msg;
                }
                break;
            case "PositionCode"://儲位編碼
                if (PositionCode == "")
                {
                    return Resources.Lang.SubmitDate_Msg11;//儲位編碼不能空 ！
                }

                BASE_CARGOSPACE_Query bcQry = new BASE_CARGOSPACE_Query();
                BASE_CARGOSPACE bcEntity = bcQry.GetBaseCargoByCode(PositionCode);

                //验证储位是否存在
                //if (BASE_CARGOSPACERule.GetCpositionByCpositionCode(PositionCode).Trim().Length == 0)
                if(bcEntity!=null && !bcEntity.id.IsNullOrEmpty())
                {
                    return "[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg12;//儲位編碼不存在 ！
                }
                //判断储位是否是冻结
                //BASE_CARGOSPACERule da = new BASE_CARGOSPACERule();
                //if (da.CheckCpositionStatus(PositionCode.Trim().ToUpper(), "1"))
                if(bcEntity != null && bcEntity.cstatus.Equals("1"))
                {
                    return "[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg13;//儲位編碼状态为[冻结],不能提交！
                }
                //检查目的储位是否允许调拨
                //ALLOCATE_FrmALLOCATE_DListQuery dad = new ALLOCATE_FrmALLOCATE_DListQuery();
                //if (dad.CheckCposition(PositionCode.Trim()))
                if(bcEntity != null && bcEntity.is_allo.HasValue && bcEntity.is_allo == 1)
                {
                    return "[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg23;//儲位不允许调拨,不能提交！
                }
                //判断储位是否是线边仓储位

                IGenericRepository<BASE_LINE_INFO> con = new GenericRepository<BASE_LINE_INFO>(db);
                BASE_LINE_INFO bliBO = (from p in con.Get()
                                       where p.cpositioncode == PositionCode.Trim().ToUpper() && p.vendor_code == "0"
                                       select p).SingleOrDefault();

                //if (BASE_CARGOSPACERule.CheckLine_info(PositionCode.Trim().ToUpper()))
                if (bliBO != null && !bliBO.id.IsNullOrEmpty())
                {
                    return "[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg24;//儲位是线边仓储位,不能提交！
                }
                //检查目的储位的剩余空间
                decimal syspace = 0;
                //syspace = dad.GetSurplusQtyByPartCodeAndCPositionCode(PositionCode.Trim());
                BasePartListQuery bpQry = new BasePartListQuery();
                syspace = bpQry.GetSurplusQtyByPartCodeAndCPositionCode(PositionCode.Trim());

                IGenericRepository<ALLOCATE_D> connAll = new GenericRepository<ALLOCATE_D>(db);
                ALLOCATE_D aldBO = (from p in connAll.Get()
                                     where p.ids == ids
                                     select p).SingleOrDefault();
                if (aldBO != null && !aldBO.ids.IsNullOrEmpty() && aldBO.iquantity != null)
                {
                    //if (syspace < ALLOCATE_DRule.GetAllo_D_Qty(ids))
                    if (Convert.ToDecimal(syspace) < aldBO.iquantity)
                    {
                        return "[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg25 + "[" + syspace + "]," + Resources.Lang.SubmitDate_Msg26;//儲位可用空間不足,剩餘空間為[" + syspace + "],或未設置最大量,不能提交！
                    }
                }
                //检查目的储位保税非保税
                string ychuw = "";
                string ycw = "";
                string mdchuw = "";
                ycw = qry.GetAlloCpositinBond(ids);
                if (ycw == "0")
                {
                    ychuw = "Y";
                }
                else if (ycw == "1")
                {
                    ychuw = "N";
                }
                mdchuw = qry.GetCpositionBond(PositionCode);
                if (ychuw != mdchuw)
                {
                    return "[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg27;//储位税别和原储位税别不一致,不能提交！
                }

                break;
            default:
                return Resources.Lang.SubmitDate_Msg20;//数据类型异常 ！
        }

        return "";
    }

    /// <summary>
    /// 出库验证
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="DataType"></param>
    /// <param name="Qty"></param>
    /// <param name="Line_Qty"></param>
    /// <param name="PositionCode"></param>
    /// <returns></returns>
    public string CheckOutData(string ids, string DataType, string Qty, string Line_Qty, string PositionCode)
    {
        decimal strMessageIQUANTITY;
        //Roger 2013-5-2 16:51:19 增加判断是否已经扣帐
        if (!(inQry.CheckStatus("OUT", ids)))
        {
            return Resources.Lang.SubmitDate_Msg09;//不是未处理状态不能修改！
        }
        switch (DataType)
        {
            case "Qty"://数量
                if (Qty == "")
                {
                    return Resources.Lang.SubmitDate_Msg10;//数量不能空 ！
                }

                //检查数量，不允许小数，负数，0 CQ 2014-2-13 13:39:48
                string errmsg = string.Empty;
                if (!(qry.Fun_IsDecimal(Qty, 0, 0, 0, out errmsg)))
                {
                    return errmsg;
                }

                #region 注销
                //try
                //{
                //    strMessageIQUANTITY = Convert.ToDecimal(Qty);
                //    if (strMessageIQUANTITY <= 0)
                //    {
                //        return "数量不能小于等于 0 ！";
                //    }
                //}
                //catch (Exception)
                //{
                //    return "数量不是有效的十进制数字！[" + Qty + "]";
                //} 
                #endregion

                //出库数量不能大于出库通知单的数量
                //if (!OUTBILL_DRule.CheckOutBillQty_Ajax(ids, Qty))
                if (!outQry.CheckOutBillQty_Ajax(ids, Qty))
                {
                    return "[" + Qty + "]" + Resources.Lang.SubmitDate_Msg28;//出库数量不能大于出库通知单的数量!
                }

                //Roger 2013/11/28 14:02:35 SN整合 如果存在SN，则必须先删除SN才可以修改数量
                //var boolmsg = OUTBILL_DRule.ExistSN(ids);
                var boolmsg = outQry.ExistSN(ids);
                if (boolmsg)
                {
                    return "[" + ids + "]" + Resources.Lang.SubmitDate_Msg17;//已经存在对应SN信息，请先删除！
                }

                break;
            case "Line_Qty"://超發數
                if (Line_Qty != "")
                {
                    //检查数量，不允许小数，负数，0 CQ 2014-2-13 13:39:48
                    string errmsg2 = string.Empty;
                    if (!(qry.Fun_IsDecimal(Line_Qty, 0, 0, 0, out errmsg2)))
                    {
                        return errmsg2;
                    }

                    //超领出库通知单卡控检查 CQ 2013-9-17 15:40:04
                    //string msg = OUTASNRule.CheckOverCollar(ids, "", Convert.ToDecimal(Line_Qty), WebUserInfo.GetCurrentUser().UserNo, "3");
                    string msg = outQry.CheckOverCollar(ids, "", Convert.ToDecimal(Line_Qty), WmsWebUserInfo.GetCurrentUser().UserNo, "3");
                    if (!msg.Equals("0"))
                    {
                        return msg;
                    }

                    //Roger 2013/11/28 14:02:35 SN整合 如果存在SN，则必须先删除SN才可以修改数量
                    var boolmsgnew = outQry.ExistSN(ids);
                    if (boolmsgnew)
                    {
                        return "[" + ids + "]" + Resources.Lang.SubmitDate_Msg17;//已经存在对应SN信息，请先删除！
                    }

                }
                string selectPositionCode = outQry.GetPositioncodeByOutBillIds(ids);
                if (selectPositionCode.Length == 0)
                {
                    return Resources.Lang.SubmitDate_Msg30;//请先选择储位!
                }
                if (Line_Qty != "")
                {
                    //if (!(Comm_Function.Fun_CheckCinvCode_ChaoFa(ids, "", WebUserInfo.GetCurrentUser().UserNo, "0", Line_Qty, "")))
                    if (!(qry.Fun_CheckCinvCode_ChaoFa(ids, "", WmsWebUserInfo.GetCurrentUser().UserNo, "0", Line_Qty, "")))
                    {
                        return Resources.Lang.SubmitDate_Msg31;//当前料号不允许超发
                    }
                    ////判断是否可以超发
                    //string ConFigvalue = Comm_Function.GetConFig("000002");
                    //if (ConFigvalue == "1")
                    //{
                    //    #region 批号管理检查
                    //    //批号管理检查 
                    //    if (!(Comm_Function.Fun_CheckCinvCode_ChaoFa(ids, "", WebUserInfo.GetCurrentUser().UserNo, "0", Line_Qty, "")))
                    //    {
                    //        return "当前料号不允许超发";
                    //    }
                    //    #endregion
                        
                    //}
                    //else
                    //{
                    //    #region 条码管理
                    //    //条码管理

                    //    //是否要验证储位是否可以超发
                    //    bool IsValidateCargSpace = true;
                    //    //特殊料号可以超发
                    //    if (new BASE_FrmBASE_PARTListQuery().CheckPartNumberIsFlag(ids))
                    //    {
                    //        IsValidateCargSpace = false;
                    //    }
                    //    if (IsValidateCargSpace && !BASE_CARGOSPACERule.ValidateIsMonetaryhighArea(selectPositionCode.Trim()))
                    //    {
                    //        return "[" + selectPositionCode + "]当前储位所在区域不允许超发！";
                    //    } 
                    //    #endregion
                    //}
                   
                }

                break;
            case "PositionCode"://儲位編碼
                if (PositionCode == "")
                {
                    return Resources.Lang.SubmitDate_Msg11;//儲位編碼不能空 ！
                }
                BASE_CARGOSPACE_Query bcQry = new BASE_CARGOSPACE_Query();
                BASE_CARGOSPACE bcEntity = bcQry.GetBaseCargoByCode(PositionCode);

                if(bcEntity != null && !bcEntity.id.IsNullOrEmpty())
                //验证储位是否存在
                //if (BASE_CARGOSPACERule.GetCpositionByCpositionCode(PositionCode).Trim().Length == 0)
                {
                    return "[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg12;//儲位編碼不存在 ！
                }
                //判断储位是否是冻结
                //BASE_CARGOSPACERule da = new BASE_CARGOSPACERule();
                //if (da.CheckCpositionStatus(PositionCode.Trim().ToUpper(), "1"))
                if (bcEntity != null && bcEntity.cstatus.Equals("1"))
                {
                    return "[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg13;//儲位編碼状态为[冻结],不能提交！
                }
                //20130617112810 普通出库不允许从线边储位出库
                if (Request.QueryString["Special"].Trim().Equals("0"))
                {
                    IGenericRepository<BASE_LINE_INFO> con = new GenericRepository<BASE_LINE_INFO>(db);
                    BASE_LINE_INFO bliBO = (from p in con.Get()
                                            where p.cpositioncode == PositionCode.Trim().ToUpper() && p.vendor_code == "0"
                                            select p).SingleOrDefault();
                    if (bliBO != null && !bliBO.id.IsNullOrEmpty())
                    //if (!BASE_CARGOSPACERule.CheckLineCpositioncode(PositionCode))
                    {
                        return "[" + PositionCode + "]" + Resources.Lang.SubmitDate_Msg14;//为线边储位,不能提交！
                    }
                }

                //CQ 2014-12-4 10:19:07

                IGenericRepository<OUTBILL_D> connd = new GenericRepository<OUTBILL_D>(db);
                OUTBILL_D obdBO = (from p in connd.Get()
                                   where p.ids == ids
                                 select p).SingleOrDefault();
						   

                //OUTBILL_DEntity entity = new OUTBILL_DEntity();
                //entity.IDS = ids;
                //entity.SelectByPKeys();

                string msg1 = string.Empty;
                //if (!(Comm_Function.Fun_CheckTax_SameBond(entity.CINVCODE.Trim().ToUpper(), out msg1)))
                if (!(qry.Fun_CheckTax_SameBond(obdBO.cinvcode.Trim().ToUpper(), out msg1)))
                {
                    return "[" + PositionCode + "]" + msg1;
                }

                 var boolmsgcp = outQry.ExistSN(ids);
                 if (boolmsgcp)
                    {
                        return "[" + ids + "]" + Resources.Lang.SubmitDate_Msg15;//已经存在对应SN信息，请先删除后修改储位！
                    }
                break;
            default:
                return Resources.Lang.SubmitDate_Msg20;//数据类型异常 ！
        }

        return "";
    }
}