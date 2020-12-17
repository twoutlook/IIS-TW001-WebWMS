using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ClassBuilder
{
    public class GridTextField 
    {
        public string HeaderText
        {
            get;
            set;
        }
        
        public bool IsLink
        {
            get;
            set;
        }

        /// <summary>
        /// 对于链接字段，绑定的操作
        /// </summary>
        public string OperationForLink
        {
            get;
            set;
        }

        /// <summary>
        /// 对于链接字段，是否在新的窗口打开链接
        /// </summary>
        public bool IsShowAtNewWin
        {
            get;
            set;
        }

        public GridTextField()
        {
            this.ShowType = EnumShowTypeInGrid.GeneralText;
            this.ListData = new ColData();
            this.IsLink = false;
            this.ColDef = new ColDefinition();
        }

        public EnumShowTypeInGrid ShowType
        {
            get;
            set;
        }

        public ColDefinition ColDef
        {
            get;
            set;
        }

        public Type CSharpType
        {
            get;
            set;
        }

        public ColData ListData;

        public override string ToString()
        {

            return this.HeaderText;
        }

        /// <summary>
        /// 格式字符串
        /// </summary>
        public string FormatString
        {
            get;
            set;
        }

        /// <summary>
        /// 对齐方式
        /// Left,Center,Right
        /// </summary>
        public string Align
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public static GridTextField TransferFromXML(XmlNode node)
        {
            GridTextField result = new GridTextField();
            result.IsLink = node.SelectSingleNode("IsLink").InnerText.ToBoolean();
            result.Align = node.SelectSingleNode("Align").InnerText;
            result.FormatString = node.SelectSingleNode("FormatString").InnerText;
            result.HeaderText = node.SelectSingleNode("HeaderText").InnerText;
            result.IsShowAtNewWin = node.SelectSingleNode("IsShowAtNewWin").InnerText.ToBoolean();
            result.OperationForLink = node.SelectSingleNode("OperationForLink").InnerText;
            result.ShowType = node.SelectSingleNode("ShowType").InnerText.ToEnum<EnumShowTypeInGrid>();
            result.Width = node.SelectSingleNode("Width").InnerText.ToInt();
            result.CSharpType = Type.GetType(node.SelectSingleNode("CSharpType").InnerText,false,true); 
            XmlNode colDefNode = node.SelectSingleNode("ColDef");
            result.ColDef.Name = colDefNode.SelectSingleNode("Name").InnerText;
            result.ColDef.DataType = colDefNode.SelectSingleNode("DataType").InnerText;
            result.ColDef.DataTypeCatalog = colDefNode.SelectSingleNode("DataTypeCatalog").InnerText.ToInt();
            result.ColDef.DefaultValue = colDefNode.SelectSingleNode("DefaultValue").InnerText;
            result.ColDef.IsAllowNull = colDefNode.SelectSingleNode("IsAllowNull").InnerText.ToBoolean();
            result.ColDef.Length = colDefNode.SelectSingleNode("Length").InnerText.ToInt();
            result.ColDef.Other = colDefNode.SelectSingleNode("Other").InnerText;
            result.ColDef.Prec = colDefNode.SelectSingleNode("Prec").InnerText.ToInt();
            
            XmlNode listDataNode = node.SelectSingleNode("ListData");
            result.ListData.LoadCatalog = listDataNode.SelectSingleNode("LoadCatalog").InnerText.ToEnum<EnumLoadCatalog>();
            result.ListData.ReferencedTable = listDataNode.SelectSingleNode("ReferencedTable").InnerText;
            result.ListData.ReferencedField = listDataNode.SelectSingleNode("ReferencedField").InnerText;
            result.ListData.ReferencedDisplayField = listDataNode.SelectSingleNode("ReferencedDisplayField").InnerText;
            result.ListData.SQL = listDataNode.SelectSingleNode("SQL").InnerText;

            XmlNodeList listNodes = listDataNode.SelectNodes("Lists/Item");
            foreach (XmlNode listNode in listNodes)
            {
                KeyValuePair<string, string> item = new KeyValuePair<string, string>(listNode.Attributes["Key"].Value, listNode.Attributes["Value"].Value);
                result.ListData.Lists.Add(item);
            }

            //result.ListData.LoadCatalog = 
            return result;
        }
    }

    public enum EnumShowTypeInGrid
    {
        /// <summary>
        /// 不做任何转换，文本方式显示
        /// </summary>
        GeneralText,

        /// <summary>
        /// 友好方式，需要借助于第三方数据表或者配置的列表或者配置的查询语句，
        /// 转换一个易于识别的方式显示
        /// </summary>
        Friendly,
    }
}