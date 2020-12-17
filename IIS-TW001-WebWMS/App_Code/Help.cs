using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Xml;
using System.Text;

/// <summary>
///Help 的摘要说明
/// </summary>
public static class Help
{

    /// <summary>
    /// 将获取到的值帮定到ListBox控件上
    /// </summary>
    /// <param name="list">List集合数据集</param>
    /// <param name="lbx">邦定数据的控件对象</param>
    /// <param name="lbxDefaultText">控件中第一个默认显示文本</param>
    /// <param name="DataTextField">显示字段</param>
    /// <param name="DataValueField">隐藏字段</param>
    /// <param name="showValue">要显示选中的值</param>
    public static void ListBoxDataBind(IList list, ListBox lbx, string lbxDefaultText, string DataTextField, string DataValueField, string showValue)
    {
        //if (lbxDefaultText != "")
        //{

        //}

        lbx.Items.Clear();

        if (list != null && list.Count > 0)
        {
            lbx.DataSource = list;
            lbx.DataTextField = DataTextField;
            lbx.DataValueField = DataValueField;
            lbx.DataBind();
        }

        if (lbxDefaultText != "")
        {
            lbx.Items.Insert(0, new ListItem(lbxDefaultText, ""));
        }

        if (showValue != "")
        {
            lbx.SelectedValue = showValue;
        }
    }

    /// <summary>
    /// 将获取到的值帮定到ListBox控件上
    /// </summary>
    /// <param name="ds">DataSet 数据集</param>
    /// <param name="lbx">邦定数据的控件对象</param>
    /// <param name="lbxDefaultText">控件中第一个默认显示文本</param>
    /// <param name="DataTextField">显示字段</param>
    /// <param name="DataValueField">隐藏字段</param>
    /// <param name="showValue">要显示选中的值</param>
    public static void ListBoxDataBind(DataTable dt, ListBox lbx, string lbxDefaultText, string DataTextField, string DataValueField, string showValue)
    {
        //if (lbxDefaultText != "")
        //{

        //}

        lbx.Items.Clear();

        if (dt != null && dt.Rows.Count > 0)
        {
            lbx.DataSource = dt;
            lbx.DataTextField = DataTextField;
            lbx.DataValueField = DataValueField;
            lbx.DataBind();
        }

        if (lbxDefaultText != "")
        {
            lbx.Items.Insert(0, new ListItem(lbxDefaultText, ""));
        }

        if (showValue != "")
        {
            lbx.SelectedValue = showValue;
        }
    }

    /// <summary>
    /// 将获取到的值帮定到DropDownList控件上
    /// </summary>
    /// <param name="ds">DataSet 数据集</param>
    /// <param name="lbx">邦定数据的控件对象</param>
    /// <param name="lbxDefaultText">控件中第一个默认显示文本</param>
    /// <param name="DataTextField">显示字段</param>
    /// <param name="DataValueField">隐藏字段</param>
    /// <param name="showValue">要显示选中的值</param>
    public static void DropDownListDataBind(DataTable dt, DropDownList ddl, string lbxDefaultText, string DataTextField, string DataValueField, string showValue)
    {
        ddl.Items.Clear();

        if (dt != null && dt.Rows.Count > 0)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            //Note by Qamar 2020-11-11
            //DataBind()前若沒加這些程式碼
            //有些情境會報錯如下
            //xxx has a SelectedValue which is invalid because it does not exist in the list of items.
            ddl.SelectedIndex = -1;
            ddl.SelectedValue = null;
            ddl.ClearSelection();
            ddl.DataBind();
        }

        if (lbxDefaultText != "")
        {
            ddl.Items.Insert(0, new ListItem(lbxDefaultText, ""));
        }

        if (showValue != "")
        {
            ddl.SelectedValue = showValue;
        }
    }


    /// <summary>
    /// 将获取到的值帮定到RadioButtonList控件上
    /// </summary>
    /// <param name="ds">DataSet 数据集</param>
    /// <param name="lbx">邦定数据的控件对象</param>
    /// <param name="lbxDefaultText">控件中第一个默认显示文本</param>
    /// <param name="DataTextField">显示字段</param>
    /// <param name="DataValueField">隐藏字段</param>
    /// <param name="showValue">要显示选中的值</param>
    public static void RadioButtonDataBind(IList list, RadioButtonList ddl, string lbxDefaultText, string DataTextField, string DataValueField, string showValue)
    {
        ddl.Items.Clear();

        if (list != null &&  list.Count > 0)
        {
            ddl.DataSource = list;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
        }

        if (lbxDefaultText != "")
        {
            ddl.Items.Insert(0, new ListItem(lbxDefaultText, ""));
        }

        if (showValue != "")
        {
            ddl.SelectedValue = showValue;
        }
        else
        {
            ddl.SelectedIndex = 1;
        }
    }



    public static void DropDownListDataBind_BOM(DataTable dt, DropDownList ddl, string lbxDefaultText, string DataTextField, string DataValueField, string showValue)
    {
        ddl.Items.Clear();

        if (dt != null && dt.Rows.Count > 0)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
        }

        if (lbxDefaultText != "")
        {
            ddl.Items.Insert(0, new ListItem(lbxDefaultText, ""));
        }

        if (showValue != "")
        {
            ddl.SelectedValue = showValue;
        }
    }
    /// <summary>
    /// 将获取到的值帮定到DropDownList控件上
    /// </summary>
    /// <param name="List">List 数据集</param>
    /// <param name="lbx">邦定数据的控件对象</param>
    /// <param name="lbxDefaultText">控件中第一个默认显示文本</param>
    /// <param name="DataTextField">显示字段</param>
    /// <param name="DataValueField">隐藏字段</param>
    /// <param name="showValue">要显示选中的值</param>
    public static void DropDownListDataBind(IList List, DropDownList ddl, string lbxDefaultText, string DataTextField, string DataValueField, string showValue)
    {
        ddl.Items.Clear();

        if (List != null && List.Count > 0)
        {
            ddl.DataSource = List;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
        }

        if (lbxDefaultText != "")
        {
            ddl.Items.Insert(0, new ListItem(lbxDefaultText, ""));
        }

        if (showValue != "")
        {
            ddl.SelectedValue = showValue;
        }
    }

    /// <summary>
    /// 将获取到的值帮定到DropDownList控件上
    /// </summary>
    /// <param name="ds">DataSet 数据集</param>
    /// <param name="lbx">邦定数据的控件对象</param>
    /// <param name="lbxDefaultText">控件中第一个默认显示文本</param>
    /// <param name="DataTextField">显示字段</param>
    /// <param name="DataValueField">隐藏字段</param>
    /// <param name="showValue">要显示选中的值</param>
    public static void CheckBoxListDataBind(DataTable dt, CheckBoxList cboList, string lbxDefaultText, string DataTextField, string DataValueField, string showValue, RepeatDirection direction)
    {
        cboList.Items.Clear();
        cboList.RepeatDirection = direction;
        if (dt != null && dt.Rows.Count > 0)
        {
            cboList.DataSource = dt;
            cboList.DataTextField = DataTextField;
            cboList.DataValueField = DataValueField;
            cboList.DataBind();
        }

        if (lbxDefaultText != "")
        {
            cboList.Items.Insert(0, new ListItem(lbxDefaultText, ""));
        }

        if (showValue != "")
        {
            cboList.SelectedValue = showValue;
        }
    }

    /// <summary>
    /// 将获取到的值帮定到DropDownList控件上
    /// </summary>
    /// <param name="List">List 数据集</param>
    /// <param name="lbx">邦定数据的控件对象</param>
    /// <param name="lbxDefaultText">控件中第一个默认显示文本</param>
    /// <param name="DataTextField">显示字段</param>
    /// <param name="DataValueField">隐藏字段</param>
    /// <param name="showValue">要显示选中的值</param>
    public static void CheckBoxListDataBind(IList List, CheckBoxList cboList, string lbxDefaultText, string DataTextField, string DataValueField, string showValue, RepeatDirection direction)
    {
        cboList.Items.Clear();
        cboList.RepeatDirection = direction;
        if (List != null && List.Count > 0)
        {
            cboList.DataSource = List;
            cboList.DataTextField = DataTextField;
            cboList.DataValueField = DataValueField;
            cboList.DataBind();
        }

        if (lbxDefaultText != "")
        {
            cboList.Items.Insert(0, new ListItem(lbxDefaultText, ""));
        }

        if (showValue != "")
        {
            cboList.SelectedValue = showValue;
        }
    }

    public static int GetDay(int year, int month)
    {
        int days = 0;

        switch (month)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                days = 31;
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                days = 30;
                break;
            case 2:
                if ((year % 400) == 0 || ((year % 4) == 0 && (year % 100) != 0))
                {
                    days = 29;
                }
                else
                {
                    days = 28;
                }
                break;
        }
        return days;
    }

    /// <summary>
    /// 字符串长度处理方法
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string StringLength(object obj, int length)
    {
        if (obj == null)
        {
            return "";
        }
        string str = obj.ToString();
        if (str != null && str.ToString().Trim() != "" && str.Trim().Length > length)
        {
            return str.Substring(0, length - 3) + "...";
        }
        return str;
    }

    /// <summary>
    /// 读取webconfg文件
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string ReadWebconfig(string key)
    {
       return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
    }

    /// <summary>
    /// 为长品名换行
    /// </summary>
    /// <param name="e"></param>
    /// <param name="cellsIndex"></param>
    public static void PartName(GridViewRowEventArgs e, int cellsIndex)
    {
        try
        {
            int nameLength = e.Row.Cells[cellsIndex].Text.Trim().Length;
            int size = Convert.ToInt32(ReadWebconfig("PartNameLength"));
            int lingth = size;
            if (nameLength > size)
            {

                int count = nameLength / size;
                if (nameLength % size != 0)
                {
                    count++;
                }
                StringBuilder name = new StringBuilder();
                for (int i = 0; i < count; i++)
                {
                    if ((i + 1) == count)
                    {
                        lingth = nameLength - i * size;
                    }
                    name.Append(e.Row.Cells[cellsIndex].Text.Trim().Substring(i * size, lingth) + "<br/>");
                }
                e.Row.Cells[cellsIndex].Text = name.ToString().Substring(0, name.ToString().Length - 5);
            }
        }
        catch (Exception)
        {
            
            //throw;
        }
    }

    public static string DateTime_ToChar(object obj)
    {
        if (obj.ToString().Length == 1)
        {
            return "0" + obj.ToString();
        }
        return obj.ToString();
    }
}