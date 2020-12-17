using System;

namespace ClassBuilder
{
    /// <summary>
    ///EnumDataType 的摘要说明
    /// </summary>
    public enum EnumDataType
    {
        Int16 = 100,
        Int32 = 101,
        Int64 = 102,
        //Single = 110,
        //Double = 111,
        Decimal = 112,
        Char = 200,
        String = 201,
        DataTime = 300,
        TimeStamp = 301,
        Boolean = 400,
        Blob = 500,
    }
    
    public enum EnumShowType
    {
        Listbox = 7,
        RadioButtonList = 4,
        CheckBoxList = 5,
        DropDownList = 6,
        OpenWin = 8,
        MultiLine = 2,
        Password = 1,
        GeneralText = 3,
        CheckBox = 9,
        DateTimePicker = 10,
        /// <summary>
        /// 上载文件字段，但数据库类型是blob字段时
        /// </summary>
        UploadFile = 11 
    }

    public enum EnumLoadCatalog
    {
        SQL = 3,
        /// <summary>
        /// 用户配置的，从FIELDDATA表获取
        /// </summary>
        FieldData = 2,

        AnotherTable = 1,

        None = 0
    }
    public enum EnumCompType
    {
        Custom = 2,
        CustomsBroker = 6,
        Inspection = 3,
        Logistic = 7,
        Manufacturing = 1,
        MofCom = 5,
        Reserve1 = 8,
        Reserve2 = 9,
        Reserve3 = 10,
        Tax = 4
    }
}
