using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml;

namespace ClassBuilder
{
    /// <summary>
    ///FieldLightControl 的摘要说明
    /// </summary>
    public class FieldLightControl
    {
        public FieldLightControl()
        {
            this.Name = "";
            this.CSharpType = typeof(string);
            this.LabelText = "";
            this.FormatString = "";
            this.CellX = -1;
            this.CellY = -1;
            this.W = 0;
            this.H = 0;
            this.ShowType = ClassBuilder.EnumShowType.GeneralText;
            this.ReadOnly = false;
            this.RequiredInput = false;
            this.IsRange = false;
            this.IsFromField = false;
            this.IsPrimaryKey = false;
            this.InputControlName = "";
            this.ColDef = new ClassBuilder.ColDefinition();
            this.HasRelationship = false;
            this.RelationshipOperators = new List<KeyValuePair<string, string>>();
            this.ListData = new ClassBuilder.ColData();

        }
        public string Name
        {
            get;
            set;
        }

        public Type CSharpType
        {
            get;
            set;
        }

        public bool IsNumberType()
        {
            if (this.CSharpType == typeof(int) || this.CSharpType == typeof(decimal) || this.CSharpType == typeof(float)
                       || this.CSharpType == typeof(double) || this.CSharpType == typeof(Int16) || this.CSharpType == typeof(Int64)
                       || this.CSharpType == typeof(byte) || this.CSharpType == typeof(uint) || this.CSharpType == typeof(UInt16)
                       || this.CSharpType == typeof(UInt64))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public string LabelText
        {
            get;
            set;
        }

        public string FormatString
        {
            get;
            set;
        }

        public int CellX
        {
            get;
            set;
        }

        public int CellY
        {
            get;
            set;
        }

        public int W
        {
            get;
            set;
        }

        public int H
        {
            get;
            set;
        }

        public ClassBuilder.EnumShowType ShowType
        {
            get;
            set;
        }

        public bool ReadOnly
        {
            get;
            set;
        }

        public bool RequiredInput
        {
            get;
            set;
        }

        public bool IsRange
        {
            get;
            set;
        }

        public bool IsFromField
        {
            get;
            set;
        }
        public bool IsPrimaryKey
        {
            get;
            set;
        }

        public string InputControlName
        {
            get;
            set;
        }

        public ClassBuilder.ColDefinition ColDef;

        public bool HasRelationship
        {
            get;
            set;
        }

        public string LogicalOperator
        {
            get;
            set;
        }

        public string DefaultRelationshipOperator
        {
            get;
            set;
        }

        public List<System.Collections.Generic.KeyValuePair<string, string>> RelationshipOperators;

        public ClassBuilder.ColData ListData;

        public static FieldLightControl TransferFromXML(XmlNode node)
        {
            FieldLightControl result = new FieldLightControl();
            result.LogicalOperator = node.SelectSingleNode("LogicalOperator").InnerText;
            result.Name = node.SelectSingleNode("Name").InnerText;
            result.CSharpType = Type.GetType(node.SelectSingleNode("CSharpType").InnerText);
            result.LabelText = node.SelectSingleNode("LabelText").InnerText;
            result.FormatString = node.SelectSingleNode("FormatString").InnerText;

            result.ShowType = node.SelectSingleNode("ShowType").InnerText.ToEnum<EnumShowType>();
            result.ReadOnly = node.SelectSingleNode("ReadOnly").InnerText.ToBoolean();
            result.RequiredInput = node.SelectSingleNode("RequiredInput").InnerText.ToBoolean();
            result.IsRange = node.SelectSingleNode("IsRange").InnerText.ToBoolean();
            result.IsFromField = node.SelectSingleNode("IsFromField").InnerText.ToBoolean();
            result.IsPrimaryKey = node.SelectSingleNode("IsPrimaryKey").InnerText.ToBoolean();
            result.InputControlName = node.SelectSingleNode("InputControlName").InnerText;
            XmlNode colDefNode = node.SelectSingleNode("ColDef");
            result.ColDef.Name = colDefNode.SelectSingleNode("Name").InnerText;
            //result.ColDef.Comment = colDefNode.SelectSingleNode("Comment").InnerText;
            result.ColDef.DataType = colDefNode.SelectSingleNode("DataType").InnerText;
            result.ColDef.DataTypeCatalog = colDefNode.SelectSingleNode("DataTypeCatalog").InnerText.ToInt();
            result.ColDef.DefaultValue = colDefNode.SelectSingleNode("DefaultValue").InnerText;
            result.ColDef.IsAllowNull = colDefNode.SelectSingleNode("IsAllowNull").InnerText.ToBoolean();
            result.ColDef.Length = colDefNode.SelectSingleNode("Length").InnerText.ToInt();
            result.ColDef.Other = colDefNode.SelectSingleNode("Other").InnerText;
            result.ColDef.Prec = colDefNode.SelectSingleNode("Prec").InnerText.ToInt();
            result.HasRelationship = node.SelectSingleNode("HasRelationship").InnerText.ToBoolean();
            result.DefaultRelationshipOperator = node.SelectSingleNode("RelationshipOperators").Attributes["DefaultOperator"].Value;
            XmlNodeList relationshipOperatorNodes = node.SelectNodes("RelationshipOperators/RelationshipOperator");
            foreach (XmlNode relationshipOperatorNode in relationshipOperatorNodes)
            {
                KeyValuePair<string, string> relationshipOperator = new KeyValuePair<string, string>(relationshipOperatorNode.Attributes["Operator"].Value, relationshipOperatorNode.Attributes["Name"].Value);

                result.RelationshipOperators.Add(relationshipOperator);
            }

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

            /*
             * this.Name = "";
            this.CSharpType = "";
            this.LabelText = "";
            this.FormatString = "";
            this.CellX = -1;
            this.CellY = -1;
            this.W = 0;
            this.H = 0;
            this.ShowType = ClassBuilder.EnumShowType.GeneralText;
            this.ReadOnly = false;
            this.RequiredInput = false;
            this.IsRange = false;
            this.IsFromField = false;
            this.IsPrimaryKey = false;
            this.InputControlName = "";
            this.ColDef = new ClassBuilder.ColDefinition();
            this.HasRelationship = false;
            this.RelationshipOperators = new List<KeyValuePair<string, string>>(); 
            this.ListData = new ClassBuilder.ColData();
             */
            return result;
        }
    }
}