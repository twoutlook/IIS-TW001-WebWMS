using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Drawing;
using System.Text.RegularExpressions;
using DreamTek.ASRS.Business.Others;

public partial class Apps_BAR_FrmBar_SNManagementEdit :WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    private bool IsEnabled = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        //btnSave.Attributes.Add("onclick", "this.value='正在提交中,請稍等……';this.disabled=true;" + this.GetPostBackEventReference(btnSave) + ";");
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() == SYSOperation.Modify)
            {
                ShowData();
            }
            else
            {
                this.trSumNum.Visible = true;
                txtMultinum16.Text = "1";
                txtDateCode16.Text = string.Format(DateTime.Now.ToString("yyMMdd"));
            }
            this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
        }

        if (this.Operation() == SYSOperation.New)
        {
            if (Request.QueryString["from"] != null
                 && Request.QueryString["from"].ToString().Equals("INAPO")
                 && Request.QueryString["ids"] != null
                 && !Request.QueryString["ids"].ToString().Equals(""))
            {
                // 如果是从入库采购单中链接过来
                var ids = Request.QueryString["ids"].ToString();
                IGenericRepository<V_INPO> con = new GenericRepository<V_INPO>(context);
                var caseList = from p in con.Get()
                               where p.ids == ids
                               select p;
                V_INPO ind = caseList.ToList().FirstOrDefault();
             
                txtCinvcode16.Text = ind.cinvcode;
                txtCinvName16.Text = ind.cinvname;
                this.trSumNum.Visible = true;
             //   this.txtSumNum.Enabled = false;
                

                if (ind.iquantity != null)
                {
                    var qty = Convert.ToDecimal(ind.iquantity);

                    if (this.IsPostBack == false)
                    {
                        this.txtQty16.Text = qty != null ? qty.ToString("f2") : string.Empty;
                        this.txtSumNum.Text = qty != null ? qty.ToString("f2") : string.Empty;
                        this.txtPo16.Text = ind.pono;
                    }

                    //if (!string.IsNullOrEmpty(this.txtSumNum.Text) && Convert.ToDecimal(txtSumNum.Text) > qty)
                    //{
                    //    this.Alert("总数量不能大采购单数量！");
                    //    this.SetFocus(txtSumNum);
                    //    IsEnabled = false;
                    //}
                }

                this.txtVendor16.Text = ind.vendorid;
                this.txtVendorName16.Text = ind.vendorname;
               
                //txtDateCode16.Text = string.Format(DateTime.Now.ToString("yyMMdd"));
               
            }
            else if (Request.QueryString["from"] != null
             && Request.QueryString["from"].ToString().Equals("OUTORDER")
             && Request.QueryString["ids"] != null
             && !Request.QueryString["ids"].ToString().Equals(""))
            {
                // 如果是从出库订单中链接过来
                var ids = Request.QueryString["ids"].ToString();
                var modOrder_d = context.OUTORDER_D.Where(x => x.ids == ids).FirstOrDefault();
                if (modOrder_d != null) {
                    txtCinvcode16.Text = modOrder_d.CinvCode;
                    txtCinvName16.Text = modOrder_d.CinvName;
                    var modOrder = context.OUTORDER.Where(x => x.id == modOrder_d.id).FirstOrDefault();


                    this.txtVendor16.Text = modOrder.CustomId;
                    this.txtVendorName16.Text = modOrder.CustomName;
                   
                    var qty = Convert.ToDecimal(modOrder_d.Iquantity);
                    
                    //this.txtSumNum.Text = qty.ToString();
                    trSumNum.Visible = true;
                   // this.txtSumNum.Enabled = false;
                    //if (!string.IsNullOrEmpty(this.txtSumNum.Text) && Convert.ToDecimal(txtSumNum.Text) > qty)
                    //{
                    //    this.Alert("总数量不能大于订单数量！");
                    //    this.SetFocus(txtSumNum);
                    //    IsEnabled = false;
                    //}

                    if (this.IsPostBack == false)
                    {
                        this.txtPo16.Text = modOrder.OrderNo;
                        this.txtSumNum.Text = qty != null ?  qty.ToString("f2"):string.Empty;
                        this.txtQty16.Text =  qty != null ? qty.ToString("f2"):string.Empty;
                    }
                }
            }
            else
            {
                showcinvcode16.GetComp = true;
                showcinvcode16.SetCINVCODE = this.txtCinvcode16.ClientID;
                showcinvcode16.SetCINVNAME = this.txtCinvName16.ClientID;
                showcinvcode16.SetBoxNum = this.txtPkNumn.ClientID;
                showvendor16.GetComp = false;
                showvendor16.SetORGCode = this.txtVendor16.ClientID;
                showvendor16.SetCompName = this.txtVendorName16.ClientID;
                txtCinvcode16.Attributes["onclick"] = "Show('" + showcinvcode16.GetDivName + "');";
                txtVendor16.Attributes["onclick"] = "Show('" + showvendor16.GetDivName + "');";
               // this.txtDateCode16.Text = string.Format(DateTime.Now.ToString("yyMMdd"));
                this.txtSNCode.BackColor = Color.LightGray;
                this.txtBegNum16.BackColor = Color.LightGray;
            }
        }
        else
        {
            btnPrint.Visible = true;
        }
       // this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
       
    
        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
    }

    //界面初始化
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('SnCode');return false;";
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<BASE_BAR_SN> con = new GenericRepository<BASE_BAR_SN>(context);
        var caseList = from p in con.Get()
                       where p.id == this.KeyID
                       select p;
        BASE_BAR_SN entity = caseList.ToList().FirstOrDefault();
        //设置SN编码类型
        session.Value = entity.sn_type == "0" ? "1" : "2";
        #region 
        //if (session.Value == "1")//19码
        //{
        //    //设置按钮可见及可修改性
        //    txtCinvcode19.Enabled = false;
        //    txtCinvName19.Enabled = false;
        //    txtDateCode19.Enabled = false;
        //    ddlBonded19.Enabled = false;
        //    txtVendor19.Enabled = false;
        //    txtVendorName19.Enabled = false;
        //    txtOkDate19.Enabled = false;
        //    txtCreateDate19.Enabled = false;
        //    txtMultinum19.Enabled = false;
        //    Label19.Visible = false;
        //    txtBegNum19.Visible = false;
        //    //控件复制
        //    txtCinvcode19.Text = entity.cinvcode;
        //    txtCinvName19.Text = entity.cinvcode_name;
        //    txtDateCode19.Text = entity.datecode;
        //    ddlBonded19.SelectedValue = entity.bonded;
        //    txtVendor19.Text = entity.vendor;
        //    txtVendorName19.Text = entity.vendor_name;
        //    txtOkDate19.Text = entity.validitydate.HasValue ? entity.validitydate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        //    txtCreateDate19.Text = entity.production_date.HasValue ? entity.production_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""; 
        //    txtQty19.Text =entity.quantity.ToString();
        //    txtCpositioncode19.Text = entity.cpositioncode;
        //    txtMultinum19.Text = @"1";
        //    ddlBarType19.SelectedValue = entity.bar_type;
        //}
        //else
        //if (session.Value == "2")//16码
        //{
        #endregion

        //设置按钮可见及可修改性
            txtCinvcode16.Enabled = false;
            txtCinvName16.Enabled = false;
            txtDateCode16.Enabled = false;
            txtVendor16.Enabled = false;
            txtVendorName16.Enabled = false;
            txtMultinum16.Enabled = false;
            Label20.Visible = true;
            txtBegNum16.Visible = true;
            txtSNCode.Visible = true;
            Label27.Visible = true;
            //控件复制
            txtCinvcode16.Text = entity.cinvcode;
            txtCinvName16.Text = entity.cinvcode_name;
            txtDateCode16.Text = entity.datecode;
            txtVendor16.Text = entity.vendor;
            txtVendorName16.Text = entity.vendor_name;
            txtQty16.Text = entity.quantity!=null? Convert.ToDecimal(entity.quantity).ToString("f2"):"0.00";
            txtPo16.Text = entity.po_number;
            txtMultinum16.Text =@"1";
            //ddlBarType16.SelectedValue = entity.bar_type;
            if (entity.boxNum != null && entity.boxNum <= 0)
            {
                IGenericRepository<BASE_PART> basePartCon = new GenericRepository<BASE_PART>(context);
                var basePart = from p in basePartCon.Get()
                               where p.cpartnumber == entity.cinvcode
                               select p;

                BASE_PART bpEntity = basePart.ToList().FirstOrDefault<BASE_PART>();

                txtPkNumn.Text = bpEntity.boxnum.ToString();
            }
            else
            {
                txtPkNumn.Text = entity.boxNum.ToString(); 
            }
            this.txtSNCode.Text = entity.sn_code;
            this.txtSumNum.Text = entity.quantity != null ? Convert.ToDecimal(entity.quantity).ToString("f2") : "0.00";
            if (!string.IsNullOrEmpty(entity.sn_code))
            {
                var index = entity.sn_code.Length - 1;
                txtBegNum16.Text = entity.sn_code.Substring(index, 1);
            }
            else
            {
                txtBegNum16.Text = @"1";
            }
        //}                                                                                                                         
    }


    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        var cinvcode = "";
        var cinvname = "";
        var vendorname = "";
        var datecode = "";
        var bonded = "";
        var vendor = "";
        var okdate = "";
        var createdate = "";
        var qty = "";
        var Cpositioncode = "";
        var po = "";
        var multinum = "";
        var seriesnum = "";
        decimal num;
        var sncode = string.Empty;
        IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
      
        IGenericRepository<BASE_VENDOR> conv = new GenericRepository<BASE_VENDOR>(context);
        var caseListv = from p in conv.Get()
                        where p.cvendorid == vendor
                        select p;
       

        //if (hfTabIndex.Value == "2")//16码新增
        //{
            #region 16位检查
            //变量接收
            cinvcode = txtCinvcode16.Text.Trim();
            cinvname = txtCinvName16.Text.Trim();
            datecode = txtDateCode16.Text.Trim();
            vendor = txtVendor16.Text.Trim();
            vendorname = txtVendorName16.Text.Trim();
            qty = txtQty16.Text.Trim();
            po = txtPo16.Text.Trim();
            multinum = txtMultinum16.Text.Trim();
            seriesnum = txtBegNum16.Text.Trim();
            var caseList = from p in con.Get()
                           where p.cpartnumber == txtCinvcode16.Text.Trim().ToUpper()
                           select p;
            //判断必输字段是否输入
            //料号
            if (cinvcode.Equals(""))
            {
                this.Alert("料号不允许为空！");
                this.SetFocus(txtCinvcode16);
                return false;
            }
            else
            {
                     //判断料号是否正确
                if (caseList.Count()==0)
                {
                    this.Alert("料号不存在！");
                    this.SetFocus(txtCinvcode16);
                    return false;
                }

            }
            if (cinvname.Equals(""))
            {
                txtCinvName16.Text = caseList.ToList().FirstOrDefault().cpartname;  
                cinvname = txtCinvName16.Text.Trim();
            }
            //品名
            if (cinvname.Equals(""))
            {
                this.Alert("品名不允许为空！");
                this.SetFocus(txtCinvName16);
                return false;
            }
            //datecode
            if (datecode.Equals(""))
            {
                this.Alert("DateCode不允许为空！");
                this.SetFocus(txtDateCode16);
                return false;
            }
            //datecode数字验证
            //校验数字
            try
            {
                num = decimal.Parse(datecode);
            }
            catch (Exception ex)
            {
                this.Alert("DateCode[" + datecode + "]不为有效数字！");
                this.SetFocus(txtDateCode16);
                return false;
            }
            if (datecode.Trim().Length < 6)
            {
                this.Alert("DateCode[" + datecode + "]格式不正确！");
                this.SetFocus(txtDateCode16);
                return false;
            }
            //datecode校验
            var msg = InBill.CheckSNFormate(datecode + "0000000001").Trim();
            if (msg.Length != 6)
            {
                Alert("[" + datecode + "]" + msg);
                return false;
            }

            //DateCode
            if (datecode.Length != 6)
            {
                this.Alert("DateCode输入不正确！");
                this.SetFocus(txtDateCode16);
                return false;
            }
            //ch cancle 
            ////供应商编码
            //if (vendor.Equals(""))
            //{
            //    this.Alert("供应商编码不允许为空！");
            //    this.SetFocus(txtVendor16);
            //    return false;
            //}
            //ch cancle 
            // var listQuery = new BASE_FrmBASE_VENDORListQuery();
            // var dtRowCount = listQuery.GetList("", "", vendor, "", "", "", "", "", "", "", false, 0, 15);
            // txtVendorName16.Text = dtRowCount.Rows[0]["CVENDOR"].ToString().Trim();
            if (caseListv.Count() > 0)
            {
                txtVendorName16.Text = caseListv.ToList().FirstOrDefault().cvendor;
            }

            vendorname = txtVendorName16.Text.Trim();
            //ch cancle 
            ////供应商名称
            //if (vendorname.Equals(""))
            //{
            //    this.Alert("供应商编码不存在！");
            //    this.SetFocus(txtVendorName16);
            //    return false;
            //}
            //ch cancle 
           
            //po/SO  
            //if (string.IsNullOrEmpty(po))
            //{
            //    ch cancle 
            //    this.Alert("PO/SO不允许为空！");
            //    this.SetFocus(txtVendorName16);
            //    return false;
            //    ch cancle                
            //}
            //else
            //{
            //    if (Encoding.Default.GetByteCount(po) > 20)
            //    {
            //        this.Alert("po超过长度20！");
            //        this.SetFocus(txtPo16);
            //        return false;
            //    }
            //}
            if (!string.IsNullOrEmpty(po))
            {
                if (Encoding.Default.GetByteCount(po) > 20)
                {
                    this.Alert("po超过长度20！");
                    this.SetFocus(txtPo16);
                    return false;
                }
               // if (Regex.IsMatch(po, "^[A-Za-z0-9]+$") == false)
                if (Regex.IsMatch(po, "^[\u4E00-\u9FFF]+$") == true)
                {
                    this.Alert("po格式不正确！");
                    this.SetFocus(txtPo16);
                    return false;
                }
        
            }
            if (!string.IsNullOrEmpty(this.txtPkNumn.Text))
            {
                string pkNumMSG = string.Empty;
                if (!(Comm_Function.Fun_IsDecimal(this.txtPkNumn.Text, 0, 1, 1, out pkNumMSG)))
                {
                    this.Alert(pkNumMSG);
                    this.SetFocus(txtPkNumn);
                    return false;
                }
            }
            else
            {
                this.txtPkNumn.Text = "0.00";
            }

            //检查数量是否正确
            string barNumMSG = string.Empty;
            if (!(Comm_Function.Fun_IsDecimal(this.txtQty16.Text, 0, 0, 1, out barNumMSG)))
            {
                this.Alert(barNumMSG);
                this.SetFocus(txtSumNum);
                return false;
            }

            if (this.Operation() == SYSOperation.New)
            {
                //检查数量是否正确
                string errmsg = string.Empty;
                if (!(Comm_Function.Fun_IsDecimal(this.txtSumNum.Text, 0, 0, 1, out errmsg)))
                {
                    this.Alert(errmsg);
                    this.SetFocus(txtSumNum);
                    return false;
                }

                

                var iqty = !string.IsNullOrEmpty(this.txtQty16.Text) ? Convert.ToDecimal(this.txtQty16.Text) : 0;
                var sumQty =!string.IsNullOrEmpty(this.txtSumNum.Text)?Convert.ToDecimal(this.txtSumNum.Text):0;

                if (iqty > sumQty)
                {
                    this.Alert("单条码数量不能大于总数量！");
                    this.SetFocus(this.txtQty16.Text);
                    return false;
                }

            }

            #region
            ////批次数量
            //if (multinum.Equals(""))
            //{
            //    this.Alert("批次数量不允许为空！");
            //    this.SetFocus(txtMultinum16);
            //    return false;
            //}

            ////校验数字
            //try
            //{
            //    num = decimal.Parse(multinum);
            //}
            //catch (Exception ex)
            //{
            //    this.Alert("批次数量[" + multinum + "]不为有效数字！");
            //    this.SetFocus(txtMultinum16);
            //    return false;
            //}
            //if (num == 0 || num < 0)
            //{
            //    this.Alert("批次数量[" + multinum + "]必须大于0！");
            //    this.SetFocus(txtMultinum16);
            //    return false;
            //}
            //if ((num - Math.Truncate(num)) > 0)
            //{
            //    this.Alert("批次数量[" + multinum + "]必须为正整数！");
            //    this.SetFocus(txtMultinum16);
            //    return false;
            //}
            //if (num > 100)
            //{
            //    this.Alert("批次数量[" + multinum + "]不能超过100！");
            //    this.SetFocus(txtMultinum19);
            //    return false;
            //}

            //流水码
            //if (!seriesnum.Equals(""))
            //{
            //    //校验数字
            //    try
            //    {
            //        num = decimal.Parse(seriesnum);
            //    }
            //    catch (Exception ex)
            //    {
            //        this.Alert("起始流水号[" + seriesnum + "]不为有效数字！");
            //        this.SetFocus(txtBegNum16);
            //        return false;
            //    }
            //    if (num == 0 || num < 0)
            //    {
            //        this.Alert("起始流水号[" + seriesnum + "]必须大于0！");
            //        this.SetFocus(txtBegNum16);
            //        return false;
            //    }
            //    if (seriesnum.Length > 5)
            //    {
            //        this.Alert("起始流水号[" + seriesnum + "]长度不能大于5位！");
            //        this.SetFocus(txtBegNum16);
            //        return false;
            //    }
            //    if ((num - Math.Truncate(num)) > 0)
            //    {
            //        this.Alert("起始流水号[" + seriesnum + "]必须为正整数！");
            //        this.SetFocus(txtMultinum19);
            //        return false;
            //    }
            //}

            #endregion

       
            #endregion
        //}

        return true;
    }


    //private bool CheckQTY(string qty,string name)
    //{
    //    var num=0;
    //    //数量
    //    if (qty.Equals(""))
    //    {
    //        this.Alert(name+"不允许为空！");
    //        this.SetFocus(txtQty16);
    //        return false;
    //    }
       
    //    if (num == 0 || num < 0)
    //    {
    //        this.Alert(name + "[" + qty + "]必须大于0！");
    //        this.SetFocus(txtQty16);
    //        return false;
    //    }

    //    //var isCorrectDec = Regex.IsMatch(num.ToString(), "^[1-9][0-9]{0,5}.[0-9]{1,4}$");
    //    //if (!isCorrectDec)
    //    //{

    //    //    this.Alert(name + "[" + qty + "]小数点后面最多四位！");
    //    //    this.SetFocus(txtQty16);
    //    //    return false;
    //    //}



    //    //if ((num - Math.Truncate(num)) > 0)
    //    //{
    //    //    this.Alert(name + "[" + qty + "]必须为正整数！");
    //    //    this.SetFocus(txtQty16);
    //    //    return false;
    //    //}

    //    return true;
    //}


    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_BAR_SN SendData()
    {
        BASE_BAR_SN entity =null;
        if (this.Operation() == SYSOperation.Modify)
        {
            IGenericRepository < BASE_BAR_SN > con = new GenericRepository<BASE_BAR_SN>(context);
            var caseList = from p in con.Get()
                           where p.id == this.KeyID
                           select p;
            entity = caseList.ToList().FirstOrDefault(); 
            


        }
        else
        {
           
             entity= new BASE_BAR_SN();
            
            var cinvcode = "";
            var cinvname = "";
            var datecode = "";
            var bonded = "";
            var vendor = "";
            var vendorname = "";
            var okdate = "";
            var createdate = "";
            var qty = "";
            var Cpositioncode = "";
            var po = "";
            var multinum = "";
            var seriesnum = "";
            var type = "";
            var BarType = "";

            //if (hfTabIndex.Value == "1")//19码新增
            //{
            //    //变量接收
            //    cinvcode = txtCinvcode19.Text.Trim();
            //    cinvname = txtCinvName19.Text.Trim();
            //    datecode = txtDateCode19.Text.Trim();
            //    bonded = ddlBonded19.SelectedValue;
            //    vendor = txtVendor19.Text.Trim();
            //    vendorname = txtVendorName19.Text.Trim();
            //    okdate = txtOkDate19.Text.Trim();
            //    createdate = txtCreateDate19.Text.Trim();
            //    qty = txtQty19.Text.Trim();
            //    Cpositioncode = txtCpositioncode19.Text.Trim();
            //    multinum = txtMultinum19.Text.Trim();
            //    BarType = ddlBarType19.SelectedValue;
            //    type = "0";

            //}
            //else 
            //  if (hfTabIndex.Value == "2")//16码新增
            //{
            //变量接收
            cinvcode = this.txtCinvcode16.Text != null ? txtCinvcode16.Text.Trim() : string.Empty;
            cinvname = this.txtCinvName16 != null ? txtCinvName16.Text.Trim() : string.Empty;
            datecode = txtDateCode16.Text != null ? txtDateCode16.Text.Trim() : string.Empty;
            vendor = this.txtVendor16 != null ? txtVendor16.Text.Trim() : string.Empty;
            vendorname = this.txtVendorName16 != null ? txtVendorName16.Text.Trim() : string.Empty;
            qty = this.txtQty16 != null ? txtQty16.Text.Trim() : string.Empty;
            po = txtPo16.Text != null ? txtPo16.Text.Trim() : string.Empty;
            multinum = txtMultinum16.Text.Trim();
            BarType = "1";
            type = "1";


            //}


            //料号
            if (cinvcode.Length > 0)
            {
                entity.cinvcode = cinvcode;
            }
            else
            {

                //entity.SetDBNull("CINVCODE", true);
            }
            //品名
            if (cinvname.Length > 0)
            {
                entity.cinvcode_name = cinvname;
            }
            else
            {
                //entity.SetDBNull("CINVCODE_NAME", true);
            }
            //DateCode
            if (datecode.Length > 0)
            {
                entity.datecode = datecode;
            }
            else
            {
                //entity.SetDBNull("DATECODE", true);
            }
            //是否保税
            if (bonded.Length > 0)
            {
                entity.bonded = bonded;
            }
            else
            {
                //entity.SetDBNull("BONDED", true);
            }
            //供应商编码
            if (vendor.Length > 0)
            {
                entity.vendor = vendor;
            }
            else
            {
                entity.vendor = "";
                //entity.SetDBNull("VENDOR", true);
            }
            //供应商名称
            if (vendorname.Length > 0)
            {
                entity.vendor_name = vendorname;
            }
            else
            {
                entity.vendor_name ="";
                //entity.SetDBNull("VENDOR_NAME", true);
            }
            //有效期限
            if (okdate.Length > 0)
            {
                entity.validitydate = Convert.ToDecimal(okdate);
            }
            else
            {
                //entity.SetDBNull("VALIDITYDATE", true);
            }
            //生产日期
            if (createdate.Length > 0)
            {
                entity.production_date = DateTime.Parse(createdate);
            }
            else
            {
                //entity.SetDBNull("PRODUCTION_DATE", true);
            }
            //数量
            if (qty.Length > 0)
            {
                entity.quantity = Convert.ToDecimal(qty);
            }
            else
            {
                //entity.SetDBNull("QUANTITY", true);
            }
            //类型（16码、19码）
            if (type.Length > 0)
            {
                entity.sn_type = type;
            }
            else
            {
                //entity.SetDBNull("SN_TYPE", true);
            }
            //储位
            if (Cpositioncode.Length > 0)
            {
                entity.cpositioncode = Cpositioncode;
            }
            else
            {
                //entity.SetDBNull("CPOSITIONCODE", true);
            }
            //po
            if (po.Length > 0)
            {
                entity.po_number = po;
            }
            else
            {
                //entity.SetDBNull("PO_NUMBER", true);
            }

            //po
            if (BarType.Length > 0)
            {
                entity.bar_type = BarType;
            }
            else
            {
                //entity.SetDBNull("BAR_TYPE", true);
            }
            var asc = 0;
            entity.batch_number = "0";
            entity.batch_qty = Convert.ToDecimal(asc);
            entity.id = Guid.NewGuid().ToString();
            entity.create_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.create_time = Comm_Fun.GetDBNowTime();
            entity.last_upd_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.last_upd_time = Comm_Fun.GetDBNowTime();

        }
        entity.boxNum = Convert.ToDecimal(txtPkNumn.Text);
        
        return entity;
    }


    //获取SN批次号
   // private string GetSn(out string msg)
     private string GetSn()
    {
       // var type = hfTabIndex.Value;
        var sn_Code = "";
        var datecode = this.txtDateCode16.Text != null ? txtDateCode16.Text.Trim() : string.Empty;
        var vendor = this.txtVendor16.Text != null ? txtVendor16.Text.Trim() : string.Empty;
        var numSN = this.txtQty16.Text != null ? txtQty16.Text.Trim() : string.Empty;
        var po = this.txtPo16.Text != null ? txtPo16.Text.Trim() : string.Empty;
        var civCode = this.txtCinvcode16.Text != null ? txtCinvcode16.Text.Trim() : string.Empty;
        
        //var serialNum = BarQuery.GetSerialNum(civCode, vendor, po, datecode,this.KeyID,out msg);

       // var snd = !string.IsNullOrEmpty(numSN) ? Convert.ToInt32(numSN) : 0;

       // var sn = string.Format("{0:00000}", snd);
        //civCode = string.Format("{0,30}", civCode);
        //vendor = string.Format("{0,10}", vendor);
        //datecode = string.Format("{0,6}", datecode);
        //po = string.Format("{0,25}", po);
        //pc = datecode + civCode + numSN + vendor + po+serialNum;
       // pc = datecode + civCode + po + vendor + serialNum;

        sn_Code = BarQuery.CreateBaseBarSN(datecode, civCode,po, vendor,this.KeyID);

        return sn_Code;


        #region 

        //if (type == "1") //19码
        //{
        //    var datecode = txtDateCode19.Text.Trim();
        //    var bonded = ddlBonded19.SelectedValue;
        //    var vendor = txtVendor19.Text.Trim();
        //    var okdate = txtOkDate19.Text.Trim();
        //    var createdate = txtCreateDate19.Text.Trim();

        //    if (vendor.Length < 5)
        //    {
        //        for (var i = 0; i < 5 - vendor.Length; i++)
        //        {
        //            vendor = "0" + vendor;
        //        }
        //    }
        //    else if (vendor.Length > 5)
        //    {
        //        vendor = vendor.Substring(0, 5);
        //    }

        //    var month = DateTime.Parse(createdate).Month.ToString();
        //    var date = DateTime.Parse(createdate).Day.ToString("00");
        //    switch (month)
        //    {
        //        case "10":
        //            month = "A";
        //            break;
        //        case "11":
        //            month = "B";
        //            break;
        //        case "12":
        //            month = "C";
        //            break;
        //    }

        //    pc = datecode + (bonded == "0" ? "B" : "N") + int.Parse(okdate).ToString("00") + vendor + month + date;
        //}
        //else

        //if(type=="2")
        //{
      



        //if (vendor.Length < 10)
        //{
        //    for (var i = 0; i < 10 - vendor.Length; i++)
        //    {
        //        vendor = " " + vendor;
        //    }
        //}
        //else if (vendor.Length > 10)
        //{
        //    vendor = vendor.Substring(0, 10);
        //}


      //  var snd=string.Format("{0:D2}", numSN);

    

        //if (!string.IsNullOrEmpty(numSN) && numSN.Length < 5)
        //{
        //    for (int i = 0; i < 5 - numSN.Length; i++)
        //    {
        //        numSN = "0" + numSN;
        //    }
        //}
        //else if (numSN.Length > 5)
        //{
        //    numSN = numSN.Substring(0, 5);
        //}

        //var cinvicode = string.Empty;


       

        //if (!string.IsNullOrEmpty(civCode) && civCode.Length < 30)
        //{
        //    for (int i = 0; i < 30-civCode.Length; i++)
        //    {
        //        civCode = " " + civCode;
        //    }
        //}
        //else if (civCode.Length > 30)
        //{
        //    civCode = civCode.Substring(0, 30);
        //}

        //vendor
       

        //if (!string.IsNullOrEmpty(vendor) && vendor.Length < 10)
        //{
        //    for (int i = 0; i < 10 - vendor.Length; i++)
        //    {
        //        vendor = " " + vendor;
        //    }
        //}
        //else if (vendor.Length > 10)
        //{
        //    vendor = vendor.Substring(0, 10);
        //}

        //}

        #endregion

    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsEnabled == true)
        {
            IGenericRepository<BASE_BAR_SN> con = new GenericRepository<BASE_BAR_SN>(context);
            if (this.CheckData())
            {
                var entity = (BASE_BAR_SN)this.SendData();
                try
                {
                    var msg = "";

                    if (this.Operation() == SYSOperation.Modify)
                    {
                        if (txtQty16.Text.Trim() != "")
                        {
                            entity.quantity = Convert.ToDecimal(txtQty16.Text.Trim());
                        }

                        entity.po_number = txtPo16.Text.Trim();
                        entity.sn_code = GetSn();//获取批次码
                        entity.bar_type = "1";
                        entity.last_upd_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
                        entity.last_upd_time = Comm_Fun.GetDBNowTime();
                        con.Update(entity);
                        con.Save();
                        if (msg == "")
                        {
                            msg = "修改成功";
                        }
                        else
                        {
                            Alert("保存失败![" + msg + "]");
                        }
                        this.AlertAndBack("FrmBar_SNManagementEdit.aspx?" + BuildQueryString(SYSOperation.Modify, this.KeyID), msg);

                    }
                    else if (this.Operation() == SYSOperation.New)
                    {
                        var qty = !string.IsNullOrEmpty(this.txtQty16.Text) ? Convert.ToDecimal(this.txtQty16.Text) : 1;
                        var sumQty = !string.IsNullOrEmpty(this.txtSumNum.Text) ? Convert.ToDecimal(this.txtSumNum.Text) : 1;

                        var num = Math.Truncate(Convert.ToDecimal(sumQty) / Convert.ToDecimal(qty));
                        var decNum = string.Format("{0:N2}", Convert.ToDecimal(sumQty) % Convert.ToDecimal(qty)); //sumQty % qty;
                        if (num >= 1 && sumQty != qty)
                        {
                            var inum = num;
                            if (Convert.ToDecimal(decNum) > 0)
                            {
                                inum = num + 1;
                            }
                            for (int i = 1; i <= inum; i++)
                            {
                                var entity2 = (BASE_BAR_SN)this.SendData();
                                decimal iqty = 0;
                                if (i == inum && Convert.ToDecimal(decNum) > 0)
                                {
                                    iqty = sumQty - qty * Convert.ToDecimal(num);
                                }
                                else
                                {
                                    iqty = qty;
                                }

                                entity2.quantity = Convert.ToDecimal(iqty);
                                entity2.sn_code = GetSn();//获取批次码
                                if (msg == "")
                                {
                                    con.Insert(entity2);
                                    con.Save();
                                }

                            }
                        }
                        else
                        {
                            if (sumQty == qty)
                            {
                                var entity2 = (BASE_BAR_SN)this.SendData();
                                entity2.quantity = Convert.ToDecimal(qty);
                                entity2.sn_code = GetSn();//获取批次码
                                if (msg == "")
                                {
                                    con.Insert(entity2);
                                    con.Save();
                                }
                            }
                        }

                        if (msg == "")
                        {
                            msg = "保存成功";
                        }
                        else
                        {
                            Alert("保存失败![" + msg + "]");
                        }

                        if (
                            (Request.QueryString["from"] != null
                                && (Request.QueryString["from"].ToString().Equals("INAPO") || Request.QueryString["from"].ToString().Equals("OUTORDER"))
                                && Request.QueryString["ids"] != null
                                && !Request.QueryString["ids"].ToString().Equals(""))

                           )
                        {
                            this.AlertAndBack("FrmBar_SNManagement.aspx?Action=" +SYSOperation.View+"&POSO="+this.txtPo16.Text , msg);
                        }
                        else
                        {
                            this.AlertAndBack("FrmBar_SNManagementEdit.aspx?" + BuildQueryString(SYSOperation.New, ""), msg);
                        }

                    }

                }
                catch (Exception ex)
                {
                    Alert("保存失败![" + ex.Message + "]");
                }
            }
        }
    }

    //打印
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        var id = "'" + this.KeyID + "'";
        //GetPrintSN
        DataTable dt = BarQuery.GetPrintSn(id);
        dt.TableName = "SNList";

        Session["DT"] = dt;

        Session["SNLength"] = "75";
        this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("/Apps/BAR/Form_SNBarCode_Print.aspx", SYSOperation.New, "") + "','打印SN','BAR_SN',800,600);");
    }
    #endregion
}