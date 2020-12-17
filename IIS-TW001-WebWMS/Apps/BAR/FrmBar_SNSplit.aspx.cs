using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business.SP.ProcedureModel;

public partial class Apps_BAR_FrmBar_SNSplit :WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        showsn.itype = "2";
        showsn.SetSN = txtSnCode.ClientID;        
        showsn.SetQty = txtOldQty.ClientID;
        showsn.SetCinvCode = txtCinvCode.ClientID;
        showsn.SetPosition = txtCption.ClientID;      
        showsn.SNType = "1";
        showsn.addid = btnOK.ClientID;
       
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() == SYSOperation.New)
            { 
                grdSNDetial.DataSource = null;
                grdSNDetial.DataBind();
                this.txtSnCode.Focus();

            }
            else
            {
                ShowData();
            }
        }
        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight(); 
       this.btnCf.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCf) + ";this.disabled=true;";
       
    }

    //界面初始化
    public void InitPage()
    {
        //this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('SnCodeSplit');return false;";
        //txtSnCode.Attributes["onclick"] = "Show('" + showsn.GetDivName + "')";
       
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        //设置按钮可见及可修改性
        txtSnCode.Enabled = false;
        txtOldQty.Enabled = false;
        txtCinvCode.Enabled = false;       
        btnNew.Enabled = false;
        btnDeleteSn.Enabled = false;
        btnCf.Enabled = false;        
        //控件复制
        var id = Request.QueryString["ID"];
        txtID.Text = id;
        //获取信息
        IGenericRepository<V_BAR_SN_SPLIT_D_Query> con = new GenericRepository<V_BAR_SN_SPLIT_D_Query>(context);
        var caseList = from p in con.Get()
                       where p.ID == id orderby p.createtime descending
                       select p;
        if (caseList != null && caseList.Count() > 0)
        {
            V_BAR_SN_SPLIT_D_Query entity = caseList.ToList().FirstOrDefault<V_BAR_SN_SPLIT_D_Query>();
            if (entity != null && !string.IsNullOrEmpty(entity.ID))
            {
                txtSnCode.Text = entity.SN;
                txtOldQty.Text = entity.SN_QTY.ToString();
                if (!string.IsNullOrEmpty(entity.cpositioncode))
                {
                    txtCption.Text = entity.cpositioncode;
                }
                if (!string.IsNullOrEmpty(entity.cinvcode))
                {
                    txtCinvCode.Text = entity.cinvcode;
                }

            }
        }
      
        
        //var dtSource = BARRule.GetNewSn(unionid);

        //txtSnCode.Text = dtSource.Rows[0]["SPLIT_SN"].ToString();
        //txtOldQty.Text = dtSource.Rows[0]["SPLIT_SN_QTY"].ToString();
        //if (dtSource.Rows[0]["CPOSITIONCODE"] != null)
        //{
        //    txtCption.Text = dtSource.Rows[0]["CPOSITIONCODE"].ToString();
        //}
        //if (dtSource.Rows[0]["PO_NUMBER"] != null)
        //{
        //    txtPo.Text = dtSource.Rows[0]["PO_NUMBER"].ToString();
        //}
        //var dt = BARRule.GetSnInbillCode(txtSnCode.Text.Trim(), unionid);
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    txtCinvCode.Text = dt.Rows[0][0].ToString();
        //    txtInbillCode.Text = dt.Rows[0][1].ToString();
        //}
        
        //绑定网格
        grdSNDetial.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdSNDetial.DataBind();
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (txtSnCode.Text.Trim().Length == 0)
        {
            Alert("条码原始编码不能为空，请选择条码！");
            txtSnCode.Focus();
            return false;
        }
        if (txtOldQty.Text.Trim().Length == 0)
        {
            Alert("条码原始数量不能为空！");
            txtOldQty.Focus();
            return false;
        }
        //检查数量是否正确
        string errmsg = string.Empty;
        if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(txtOldQty.Text.Trim(), 0, 0, 1, out errmsg)))
        {
            this.Alert(errmsg);
            this.SetFocus(txtOldQty);
            return false;
        }
        //获取SN前几位
        var sncode = txtSnCode.Text.Trim();
        var snBefore = "";  
        if (sncode.Length == 75)
        {
            snBefore = sncode.Substring(0, 71);
            //type = "1";
            //mat = "D3";
        }

        decimal snQty = 0;
       
        //获取原数量
        var Qty = decimal.Parse(txtOldQty.Text.Trim());
        
        #region 明细校验
        //校验
        for (int i = 0; i < grdSNDetial.Rows.Count; i++)
        {
            var SN = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtSN")).Text;
            var qty = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtQty")).Text;            
            if (SN.IsNullOrEmpty())
            {
                Alert("第[" + (i + 1) + "]行,SN为空");
                return false;
            }
            if (qty.IsNullOrEmpty())
            {
                Alert("第[" + (i + 1) + "]行,数量为空");
                return false;
            }

            #region 长度格式检查

            //长度大于或等于前n位
            if (SN.Length >= snBefore.Length)
            {
                //SN前几位必须与设定一致
                var snnewBefore = "";
                if (SN.Length == 75)
                {
                    snnewBefore = SN.Substring(0, 71);
                } 
                if (snBefore != snnewBefore)
                {
                    Alert("第[" + (i + 1) + "]行,[" + SN + "]前几位必须为[" + snBefore + "]");
                    return false;
                }               
            }
            else
            {
                Alert("第[" + (i + 1) + "]行,[" + SN + "]前几位必须为[" + snBefore + "]");
                return false;
            }
            #endregion

            //判断长度
            if (SN.Length != 75)
            {
                Alert("第[" + (i + 1) + "]行,[" + SN + "]长度不为75位");
                return false;
            }
            //拆解长度必须与原长度一致
            if (SN.Length != sncode.Length)
            {
                Alert("第[" + (i + 1) + "]行,[" + SN + "]长度必须与原SN长度一致");
                return false;
            }
            //判断最后几位是否为流水码
            try
            {
                int.Parse(SN.Substring(snBefore.Length));
            }
            catch (Exception)
            {
                Alert("第[" + (i + 1) + "]行,[" + SN + "] 最后几位必须为流水码");
                return false;
            }

            //判断是否存在重复的SN
            if (!SN.Equals(sncode))//与原SN一致则不校验是否重复
            {
                var msg = BarQuery.IsExistsSN(SN);
                if (msg == "1")
                {
                    Alert("第[" + (i + 1) + "]行,[" + SN + "] 已经存在！");
                    return false;
                }
            }

            //判断数字
            decimal num;
            try
            {
                num = decimal.Parse(qty);
            }
            catch (Exception ex)
            {
                Alert("第[" + (i + 1) + "]行,[" + qty + "] 不是有效数字");
                return false;
            }
            //if (num <= 0)
            //{
            //    Alert("第[" + (i + 1) + "]行,[" + qty + "] 必须大于等于0");
            //    return false;
            //}
            //检查数量是否正确
            string errmsg1 = string.Empty;
            if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(qty, 0, 0, 1, out errmsg1)))
            {
                this.Alert("第[" + (i + 1) + "]行,[" + qty + "]" + errmsg1);              
                return false;
            }
            //if ((num - Math.Truncate(num)) > 0)
            //{
            //    Alert("第[" + (i + 1) + "]行,[" + qty + "] 必须为正整数");
            //    return false;
            //}
            //如果SN与原SN一致，则不允许数量一致
            if (sncode == SN && num == Qty)
            {
                Alert("第[" + (i + 1) + "]行,[" + SN + "] 与原SN一致时，数量不允许一致");
                return false;
            }
            ////可以允许重置
            //if (sncode != SN && num == 0)
            //{
            //    Alert("第[" + (i + 1) + "]行,[" + SN + "]与原SN不一致，数量不允许为0");
            //    return false;
            //}

            snQty += num;
        } 
        #endregion

        //判断数量合计
        if (snQty > Qty)
        {
            Alert("SN数量总和[" + snQty + "]与初始数量[" + Qty + "]不一致");
            return false;
        }

        #region 检查重复
        //检查是否存在和SN相同的项
        //如果有相同的SN，则报错
        var dt = GetGridViewData();
        var listq =
            from t in dt.AsEnumerable()
            group t by new { t1 = t.Field<string>("NEW_SN") }
                into m
                select new
                           {
                               rowcount = m.Count()
                           };
        var count = listq.ToList().Exists(q => q.rowcount > 1);
        if (count)
        {
            Alert("SN拆分列表存在SN一致的数据");
            return false;
        } 
        #endregion

        //拆解条数不能仅仅一条
        int vcount = grdSNDetial.Rows.Count;
        if (vcount == 0)
        {
            Alert("SN没有拆解明细，不能拆解！");
            return false;
        }
        
        if (vcount == 1)
        {
            var snqty = ((TextBox)this.grdSNDetial.Rows[0].FindControl("txtQty")).Text;
            if (decimal.Parse(snqty) == Qty)
            {
                Alert("SN拆分不能只拆解成一条数据，且数量和原数量相等");
                return false;
            }
            var SN = ((TextBox)this.grdSNDetial.Rows[0].FindControl("txtSN")).Text;
            var sntype = this.grdSNDetial.DataKeys[0].Values[1].ToString();
            //如果原SN存在且是手动新增的，则不能拆解
            if (SN.Trim() == txtSnCode.Text.Trim()&&sntype.Trim()=="0")
            {
                Alert("SN拆分不能拆解成原SN，且只有一条数据");
                return false;
            }
        }
        //数量和比原数量小
        if (snQty < Qty)
        {
            //判断SN是否存在拆解SN，并获取其数量
            int pbz = 0;
            decimal snnum = 0;
            string snid = string.Empty;
            string sntype = "0";
            for (int i = 0; i < grdSNDetial.Rows.Count; i++)
            {
                var SN = ((TextBox) this.grdSNDetial.Rows[i].FindControl("txtSN")).Text;
                snnum = decimal.Parse(((TextBox)this.grdSNDetial.Rows[i].FindControl("txtQty")).Text);
                sntype = this.grdSNDetial.DataKeys[i].Values[1].ToString(); ;//获取SN的标识
                snid = this.grdSNDetial.DataKeys[i].Values[0].ToString();
                if (SN.Trim() == txtSnCode.Text.Trim())
                {
                    if (sntype == "0")
                    {
                        pbz = 1; //手动拆解的SN
                        break;
                    }
                    else
                    {
                        pbz = 2;//已出库的SN
                        break;
                    }
                   
                }
                else
                {
                    snnum = 0;
                }

            }
            decimal newnum = 0;
            if (pbz == 1)
            {
                newnum = Qty - snQty + snnum;
            }
            else
            {
                newnum = Qty - snQty;
            }
           
            //执行删除新增操作
            InsertDeleteRow(txtSnCode.Text.Trim(), newnum, snid, pbz);

        }
        

        return true;
    }

    /// <summary>
    /// 新增删除数据
    /// </summary>
    /// <param name="sncode">SN</param>
    /// <param name="qty">数量</param>
    /// <param name="id">删除SN的ID</param>
    /// <param name="pbz">标识0不删除 1 删除</param>
    public void InsertDeleteRow(string sncode, decimal qty, string id, int pbz)
    {
        DataTable table = GetGridViewData();
       
        if (pbz == 1)
        {
            foreach (DataRow dtRow in table.Rows)
            {
                if (dtRow["ID"].ToString() == id)
                {
                    table.Rows.Remove(dtRow);
                    break;
                }
            }
            //table.Rows.Remove(table.Select("ID="+id)[0]);
        }
        DataRow newRow = table.NewRow();
        newRow["ID"] = Guid.NewGuid().ToString();
        newRow["NEW_SN"] = sncode;
        newRow["NEW_SN_QTY"] = qty;
        newRow["TYPE"] = "0";
        table.Rows.Add(newRow);
        grdSNDetial.DataSource = table;
        grdSNDetial.DataBind();
    }



    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BAR_SN_SPLIT> conn=new GenericRepository<BAR_SN_SPLIT>(db);
       // IGenericRepository<BAR_SN_SPLIT_D> connd = new GenericRepository<BAR_SN_SPLIT_D>(db);
       //IGenericRepository<STOCK_CURRENT_SN> conns = new GenericRepository<STOCK_CURRENT_SN>(db);
        if (this.CheckData())
        {
            decimal qty = 0;
            BAR_SN_SPLIT entity = new BAR_SN_SPLIT();
            if (!string.IsNullOrEmpty(txtID.Text.Trim()))
            {
                entity.id = txtID.Text;
            }
            else  entity.id = Guid.NewGuid().ToString();
            entity.split_sn = txtSnCode.Text.Trim();
            if (!string.IsNullOrEmpty(txtOldQty.Text.Trim()))
            {
                decimal.TryParse(txtOldQty.Text.Trim(), out qty);
            }
            entity.split_sn_qty = qty;
            entity.cpositioncode = txtCption.Text.Trim();
            entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.createtime = DateTime.Now;
            entity.cinvcode = txtCinvCode.Text.Trim();
            if (this.Operation() == SYSOperation.New)
            {
                conn.Insert(entity);
                conn.Save();
            }   
            try
            {
                string sn = string.Empty;
                decimal dqty = 0m;
                string ID = string.Empty;
                string createor = string.Empty;
                DateTime createtime;
                string returnvalue=string.Empty;
                string errormsg=string.Empty;
                var count = 0;
                //插入数据
                for (int i = 0; i < grdSNDetial.Rows.Count; i++)
                {      
                    if (errormsg.Length>0) break;
                    ID= entity.id;
                    sn = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtSN")).Text.ToUpper();
                    string strQty=((TextBox)this.grdSNDetial.Rows[i].FindControl("txtQty")).Text.Trim();
                    if (!string.IsNullOrEmpty(strQty))
                    {
                        Decimal.TryParse(strQty, out dqty);
                    }                    
                    createor = WmsWebUserInfo.GetCurrentUser().UserNo;
                    createtime = DateTime.Now;
                    //调用存储过程处理数据
                    Proc_ProcessSNSplit proc = new Proc_ProcessSNSplit();
                    proc.P_ID = ID;
                    proc.P_SN = sn;
                    proc.P_Qty = Convert.ToDecimal(dqty.ToString());
                    proc.P_Createor = createor;
                    proc.P_Createtime = createtime.ToString();
                    proc.Execute();
                    returnvalue = proc.P_Return_Value;
                    errormsg = proc.P_Error_Value;
                    if (returnvalue == "1")
                    {
                        Alert("拆分失败，原因是： " + errormsg + "");
                        break;
                    }
                    else
                    {
                        count++;
                    }
                }
                if (count == grdSNDetial.Rows.Count)
                {
                    this.AlertAndBack("FrmBar_SNSplit.aspx?" + BuildQueryString(SYSOperation.Modify, entity.id), "拆分成功");
                }  
            }
            catch (Exception ex)
            {               
                Alert("拆分失败![" + ex.Message + "]");
            }
        }
    }

    //添加一行
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (txtSnCode.Text.Trim().Length == 0)
        {
            Alert("条码原始编码不能为空，请选择条码！");
            txtSnCode.Focus();
            return;
        }
        if (txtOldQty.Text.Trim().Length == 0)
        {
            Alert("条码原始数量不能为空！");
            txtOldQty.Focus();
            return;
        }
        //检查数量是否正确
        string errmsg = string.Empty;
        if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(txtOldQty.Text.Trim(), 0, 0, 1, out errmsg)))
        {
            this.Alert(errmsg);
            this.SetFocus(txtOldQty);
            return;
        }
        DataTable table = GetGridViewData();
        DataRow newRow = table.NewRow();
        newRow["ID"] = Guid.NewGuid().ToString();
        newRow["TYPE"] = "0";
        //产生新的流水号
        var grdMaxSn =
                GetGridViewData().Rows.Cast<DataRow>().AsEnumerable().Select(dr => dr.Field<string>("NEW_SN")).
                    Max();         
        newRow["NEW_SN"] = GetNewSN(grdMaxSn);     
        if (string.IsNullOrEmpty(newRow["NEW_SN"].ToString()))
        {
            Alert("SN长度不是75位，不可拆分！");
            return;          
        }
        table.Rows.Add(newRow);
        grdSNDetial.DataSource = table;
        grdSNDetial.DataBind();
    }
    private string GetNewSN(string grdMaxSn)
    {
        string sncode = string.Empty;         
         sncode = GetSerialNum(grdMaxSn);      
        //检查在数据库中存不存在
        string isExists=BarQuery.IsExistsSN(sncode);
        while (isExists == "1")
        {
            sncode = GetSerialNum(sncode);
            isExists = BarQuery.IsExistsSN(sncode);
        }       
       return sncode;            
    }
    //根据SNcode获取新的sn号
    public string GetSerialNum(string sncode)
    {        
        var retVal = string.Empty;
        var serialNum = 1;
        if (sncode.Length == 75)
        {
            retVal = sncode.Substring(71, 4);
            if (string.IsNullOrEmpty(retVal))
            {
                retVal = string.Format("{0:D4}", serialNum);
            }
            else
            {
                if (!string.IsNullOrEmpty(retVal))
                {
                    try
                    {
                        serialNum = int.Parse(retVal) + 1;
                    }
                    catch
                    {
                        serialNum = 1;
                    }
                }
                else
                {
                    serialNum = 1;
                }
                retVal = string.Format("{0:D4}", serialNum);
            }
            retVal = sncode.Substring(0, 71) + retVal;
        }
        else
        {
            Alert("SN长度不是75位，不可拆分！");            
        }
        return retVal;
    }

    //删除选中的行
    protected void btnDeleteSn_Click(object sender, EventArgs e)
    {       
        DataTable table = GetGridViewData();
        string msg = string.Empty;

        for (int i = 0; i < grdSNDetial.Rows.Count; i++)
        {
            if (this.grdSNDetial.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
            {
                CheckBox chkSelect = (CheckBox)this.grdSNDetial.Rows[i].Cells[0].FindControl("chkSelect"); //20130614093155
                if (chkSelect.Checked)
                {
                    foreach (DataRow dtRow in table.Rows)
                    {
                        if (dtRow["ID"].ToString() == this.grdSNDetial.DataKeys[i].Values[0].ToString())
                        {
                            if (dtRow["TYPE"].ToString().Trim() == "0")
                            {
                                table.Rows.Remove(dtRow);
                                break;
                            }
                            else if (dtRow["TYPE"].ToString().Trim() == "1")
                            {
                                msg = "原始SN不可以删除！";
                                break;
                            }
                        }
                       
                    }
                }               
            }
        }

        grdSNDetial.DataSource = table;
        grdSNDetial.DataBind();
        if (msg.Trim().Length > 0)
        {
            this.Alert(msg);
            return;
        }
       
    }

    //获取网格数据
    private DataTable GetGridViewData()
    {
        var dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID"));
        dt.Columns.Add(new DataColumn("NEW_SN"));
        dt.Columns.Add(new DataColumn("NEW_SN_QTY"));
        dt.Columns.Add(new DataColumn("TYPE"));

        for (int i = 0; i < grdSNDetial.Rows.Count; i++)
        {
            DataRow sourseRow = dt.NewRow();
            sourseRow["ID"] = this.grdSNDetial.DataKeys[i].Values[0].ToString();
            sourseRow["NEW_SN"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtSN")).Text;
            sourseRow["NEW_SN_QTY"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtQty")).Text;
            sourseRow["TYPE"] = this.grdSNDetial.DataKeys[i].Values[1].ToString();
            dt.Rows.Add(sourseRow);
        }
        return dt;
    }

    //
    protected void grdSNDetial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtSN_NEW = (TextBox)e.Row.FindControl("txtSN");
            TextBox txtQty = (TextBox)e.Row.FindControl("txtQty");
            if (this.Operation() == SYSOperation.New)
            {
                txtSN_NEW.Enabled = true;
                txtQty.Enabled = true;
                //var sncode = txtSnCode.Text.Trim();
                //if (!sncode.IsNullOrEmpty() && txtSN_NEW.Text.IsNullOrEmpty())
                //{
                //    if (sncode.Length == 19)
                //    {
                //        txtSN_NEW.Text = sncode.Substring(0, 15) + "X";
                //    }
                //    else
                //    {
                //        txtSN_NEW.Text = sncode.Substring(0, 11) + "X";
                //    }
                //}
                //if (this.grdSNDetial.Rows.Count > 0)
                //{
                //    CheckBox chkSelect = (CheckBox)this.grdSNDetial.Rows[0].Cells[0].FindControl("chkSelect"); //20130614093155
                //    if (chkSelect != null) chkSelect.Enabled = false;
                //}
               
            }
            else
            {
                txtSN_NEW.Enabled = false;
                txtQty.Enabled = false;
             }
            txtQty.Attributes["onblur"] = "SetQtyValue('" + txtQty.ClientID + "','" + txtOldQty.ClientID + "');";
            
            //if (this.grdSNDetial.DataKeys[e.Row.RowIndex].Values[1].ToString().Equals("1"))
            //{
            //    txtSN_NEW.Enabled = false;
            //    txtQty.Enabled = false;
            //}

        }
    }

    //打印
    protected void btnprint_Click(object sender, EventArgs e)
    {
        //var unionid = Request.QueryString["ID"];
        //txtCption.Text = txtCption.Text.Trim().ToUpper();
        ////更新储位和PO
        //BARRule.UpdateSnSplit(unionid, txtCption.Text.Trim(), txtPo.Text.Trim());
        ////打印
        //DataTable dt = BARRule.GetSplitSn("'" + unionid + "'");

        //Session["SNLength"] = txtSnCode.Text.Trim().Length.ToString();
        //Session["CodeType"] = "128";
        //Session["DT"] = dt;
        //this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("/Apps/BAR/Form_SN70X25_Print.aspx", SysOperation.New, "") + "','打印SN','BAR_SN',800,600);");
    }
    
    /// SN的回车事件
    /// <summary>
    /// SN的回车事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        { 
            var dtNew = new DataTable();
            dtNew.Columns.Add(new DataColumn("ID"));
            dtNew.Columns.Add(new DataColumn("NEW_SN"));
            dtNew.Columns.Add(new DataColumn("NEW_SN_QTY"));
            dtNew.Columns.Add(new DataColumn("TYPE"));

            DataRow sourseRow = dtNew.NewRow();
            sourseRow["ID"] = Guid.NewGuid().ToString();
            sourseRow["NEW_SN"] = txtSnCode.Text.Trim();
            sourseRow["NEW_SN_QTY"] = "";
            sourseRow["TYPE"] = "1";
            dtNew.Rows.Add(sourseRow);
            //新增一行
            DataRow newRow = dtNew.NewRow();
            newRow["ID"] = Guid.NewGuid().ToString();
            newRow["TYPE"] = "0";
            ////产生新的流水号
            if (!string.IsNullOrEmpty(txtSnCode.Text.Trim()))
            {
                newRow["NEW_SN"] = GetNewSN(txtSnCode.Text.Trim());               
                if (string.IsNullOrEmpty(newRow["NEW_SN"].ToString()))
                {
                    txtCinvCode.Text = "";
                    txtCption.Text = "";
                    txtSnCode.Text = "";
                    txtOldQty.Text = "";
                    return;
                }
            }
            else
            {
                newRow["NEW_SN"] = "";
            }
            dtNew.Rows.Add(newRow);
            grdSNDetial.DataSource = dtNew;
            grdSNDetial.DataBind();
                
            

        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

    //选择打印
    //protected void btnSplitprint_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string strid = "";
    //        int vcount = 0;
    //        for (int i = 0; i < grdSNDetial.Rows.Count; i++)
    //        {
    //            if (this.grdSNDetial.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
    //            {
    //                CheckBox chkSelect = (CheckBox)this.grdSNDetial.Rows[i].Cells[0].FindControl("chkSelect"); //20130614093155
    //                if (chkSelect.Checked)
    //                {
    //                    string vsn= ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtSN")).Text;
    //                    string vQty= ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtQty")).Text;
    //                    string vcposition =txtCption.Text.Trim().ToUpper();
    //                    if (vcposition == "")
    //                    {
    //                        vcposition = " ";
    //                    }
    //                    string vpo = txtPo.Text.Trim();
    //                    if (vpo == "")
    //                    {
    //                        vpo = " ";
    //                    }
    //                    strid += txtCinvCode.Text.Trim() + ";" + vsn + ";" + vQty + ";" + vcposition + ";" + vpo + ";" + "1;" + "\r\n";
    //                    vcount++;
    //                }
    //            }
    //        }
    //        if (vcount == 0)
    //        {
    //            Alert("请选择需要打印的项！");
    //        }
    //        else
    //        {
    //            Alert(strid);
    //            Hid_Split.Value = strid;
    //            //Page.RegisterStartupScript("cccc", "<script>WriteToTxtFile();</script>");
    //            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript77788899", "<script>WriteToTxtFile();</script>");
    //        }

    //    }
    //    catch (Exception err)
    //    {
    //        Alert(err.Message);
    //    }
    //}
}