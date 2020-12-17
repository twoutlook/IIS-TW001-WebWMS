//查询条件的一组函数
//枚举类型：输入条件的现实类别
EnumShowType = function() {

}

EnumShowType.prototype =
{
    Listbox: 7,          //列表框
    RadioButtonList: 4,  //收音机按钮列表框
    CheckBoxList: 5,     //检查列表
    DropDownList: 6,     //下拉列表
    OpenWin: 8,          //打开窗体获取
    MultiLine: 2,        //多行文本
    Password: 1,         //密码输入
    GeneralText: 3,     //单行文本
    CheckBox: 9,         //检查框
    DateTimePicker: 10,  //日期选择
    /// <summary>
    /// 上载文件字段，但数据库类型是blob字段时
    /// </summary>
    UploadFile: 11
}

//枚举类型：列表输入项的数据来源分类
EnumLoadCatalog = function() {

}

EnumLoadCatalog.prototype = {
    None: 0,                 //无
    AnotherTable: 1,         //其他表或者试图
    FieldData: 2,            //开发者配置
    SQL: 3                   //SQL查询语句
}

//Key Value 结构体
MyKeyValue = function() {
    this.Key = "";
    this.Value = "";
}

//条件对应的字段定义类
function ColDefinition() {
    this.Name = "";         //字段名
    this.DataType = "";     //数据库内的数据类型
    this.DataTypeCatalog = -1; //数据类型的分类 0--无大小 1--只有长度 2--有精度的数字型            
    this.IsAllowNull = false; //是否允许空
    this.Length = 0;          //长度
    this.Other = "";
    this.Prec = 0;
    this.Scale = 0;
}

//类：列表输入项的数据来源
function ColData() {
    this.LoadCatalog = EnumLoadCatalog.None;
    this.SQL = "";
    this.ReferencedTable = "";
    this.ReferencedField = "";
    this.ReferencedDisplayField = "";
    this.Lists = new Array();
}

//类：输入条件类
function FieldControl() {
    this.Name = "";
    this.CSharpType = "";
    this.LabelText = "";
    this.FormatString = "";
    //            this.CellX = -1;
    //            this.CellY = -1;
    //            this.W = 0;
    //            this.H = 0;
    this.ShowType = EnumShowType.GeneralText;
    this.ReadOnly = false;
    this.RequiredInput = false;
    this.IsRange = false;
    this.IsFromField = false;
    this.IsPrimaryKey = false;
    this.InputControlName = "";
    this.ColDef = new ColDefinition();
    this.HasRelationship = false;
    this.DefaultRelationshipOperator = "";
    this.RelationshipOperators = new Array();
    this.ListData = new ColData();
    this.LogicalOperator = "and";
    this.IsSysCondition = false;
}

var defaultConditions = new Array();
var conditions = new Array();
var reportName = "";

//从XML节点中读出某一个条件的定义
function ReadOneCondition(conditionNode, condition) {
    condition.LogicalOperator = getNodeStringValue(conditionNode, "LogicalOperator");
    if (condition.LogicalOperator == "")
        condition.LogicalOperator = "and";
    condition.Name = getNodeStringValue(conditionNode, "Name");
    condition.CSharpType = getNodeStringValue(conditionNode, "CSharpType");
    condition.LabelText = getNodeStringValue(conditionNode, "LabelText");
    condition.FormatString = getNodeStringValue(conditionNode, "FormatString");
    //            condition.CellX = getNodeIntValue(conditionNode,"CellX");
    //            condition.CellY = getNodeIntValue(conditionNode,"CellY");h
    //            condition.W = getNodeIntValue(conditionNode,"W");
    //            condition.H = getNodeIntValue(conditionNode,"H");
    condition.ShowType = getNodeIntValue(conditionNode, "ShowType");
    condition.ReadOnly = getNodeBoolValue(conditionNode, "ShowType");
    condition.RequiredInput = getNodeBoolValue(conditionNode, "RequiredInput");
    condition.IsRange = getNodeBoolValue(conditionNode, "IsRange");
    condition.IsFromField = getNodeBoolValue(conditionNode, "IsFromField");
    condition.IsPrimaryKey = getNodeBoolValue(conditionNode, "IsPrimaryKey");
    condition.InputControlName = getNodeStringValue(conditionNode, "InputControlName");

    var colDefNode = conditionNode.getElementsByTagName("ColDef");
    if (colDefNode != null && colDefNode.length > 0) {
        colDefNode = colDefNode[0];
        var colDef = condition.ColDef;
        colDef.Name = getNodeStringValue(colDefNode, "Name");
        colDef.DataType = getNodeStringValue(colDefNode, "DataType");
        colDef.DataTypeCatalog = getNodeIntValue(colDefNode, "DataTypeCatalog");
        colDef.IsAllowNull = getNodeBoolValue(colDefNode, "IsAllowNull");
        colDef.Length = getNodeBoolValue(colDefNode, "Length");
        colDef.Other = getNodeBoolValue(colDefNode, "Other");
        colDef.Prec = getNodeIntValue(colDefNode, "Prec");
        colDef.Scale = getNodeIntValue(colDefNode, "Scale");
    }

    condition.HasRelationship = getNodeBoolValue(conditionNode, "HasRelationship");
    var relationshipOperatorsNode = conditionNode.getElementsByTagName("RelationshipOperators");
    if (relationshipOperatorsNode != null && relationshipOperatorsNode.length > 0) {
        relationshipOperatorsNode = relationshipOperatorsNode[0];
        condition.DefaultRelationshipOperator = relationshipOperatorsNode.attributes[0].nodeValue;
        for (var j = 0; j < relationshipOperatorsNode.childNodes.length; j++) {
            var oneRelationshipOperatorNode = relationshipOperatorsNode.childNodes[j];
            var relationshipOperator = new MyKeyValue();
            relationshipOperator.Key = oneRelationshipOperatorNode.attributes[0].nodeValue;
            relationshipOperator.Value = oneRelationshipOperatorNode.attributes[1].nodeValue;
            condition.RelationshipOperators[j] = relationshipOperator;
        }
    }

    var listDataNode = conditionNode.getElementsByTagName("ListData");
    if (listDataNode != null && listDataNode.length > 0) {
        listDataNode = listDataNode[0];
        var loadCatalog = getNodeStringValue(listDataNode, "LoadCatalog");
        if(loadCatalog == "FieldData")
        {
            condition.ListData.LoadCatalog = 2;
        }
        else if(loadCatalog == "SQL")
        {
            condition.ListData.LoadCatalog = 3;
        }
        else if(loadCatalog == "AnotherTable")
        {
            condition.ListData.LoadCatalog = 1;
        }
        else
        {
            condition.ListData.LoadCatalog = 0;
        }
        condition.ListData.SQL = getNodeStringValue(listDataNode, "SQL");
        condition.ListData.ReferencedTable = getNodeStringValue(listDataNode, "ReferencedTable");
        condition.ListData.ReferencedField = getNodeStringValue(listDataNode, "ReferencedField");
        condition.ListData.ReferencedDisplayField = getNodeStringValue(listDataNode, "ReferencedDisplayField");
        var listsNode = listDataNode.getElementsByTagName("Lists");
        if (listsNode != null && listsNode.length > 0) {
            listsNode = listsNode[0];
            for (var j = 0; j < listsNode.childNodes.length; j++) {
                var oneItem = new MyKeyValue();
                oneItem.Key = listsNode.childNodes[j].attributes[0].nodeValue;
                oneItem.Value = listsNode.childNodes[j].attributes[1].nodeValue;
                condition.ListData.Lists[j] = oneItem;
            }
        }
    }
}

//读自定义查询条件的定义
function ReadCondition() {
    var oXMLDoc = new ActiveXObject('Microsoft.XMLDOM');
    oXMLDoc.async = false;
    var customQueryXML = "FrmGetXMLForQuery.aspx?ReportName=" + escape(reportName);
    //alert(customQueryXML);
    oXMLDoc.load(customQueryXML);
    var oRoot = oXMLDoc.lastChild;  //Root Node
    //alert(oRoot.xml);
    var objNodeList = oXMLDoc.selectNodes("root/Conditions/Condition[IsSysCondition=\"False\" and (IsRange=\"False\" or (IsRange=\"True\" and IsFromField=\"True\"))]");
    for (var i = 0; i < objNodeList.length; i++) {
        var conditionNode = objNodeList[i]; //Condition	  
        conditions[i] = new FieldControl();
        var condition = conditions[i];
        ReadOneCondition(conditionNode, condition);
    }

    objNodeList = oXMLDoc.selectNodes("root/DefaultConditions/Condition[IsSysCondition=\"False\" and (IsRange=\"False\" or (IsRange=\"True\" and IsFromField=\"True\"))]");

    for (var i = 0; i < objNodeList.length; i++) {
        var conditionNode = objNodeList[i]; //Condition	  
        defaultConditions[i] = new FieldControl();
        var condition = defaultConditions[i];
        ReadOneCondition(conditionNode, condition);
    }
}

//从节点中获取整数值
function getNodeIntValue(parentNode, nodeName) {
    var v = getNodeStringValue(parentNode, nodeName);
    if (isNaN(v)) {
        return 0;
    }
    else {
        return parseInt(v);
    }
}

//从节点中获取bool值
function getNodeBoolValue(parentNode, nodeName) {
    var v = getNodeStringValue(parentNode, nodeName);
    if (v == "True") {
        return true;
    }
    else {
        return false;
    }
}

//从节点中获取字符串值
function getNodeStringValue(parentNode, nodeName) {
    var nodes = parentNode.getElementsByTagName(nodeName);
    if (nodes != null && nodes.length > 0) {
        return nodes[0].text;
    }
    else {
        return "";
    }
}

///创建字段的下拉框组件
function CreateFieldDropDown(condition, index) {
    var dropDownlist = document.createElement("select");
    dropDownlist.setAttribute("id", "drplstCondition" + index);
    dropDownlist.setAttribute("name", "drplstCondition" + index);
    var oneItem = document.createElement('option');
    oneItem.text = Resources.Lang.Commona_PleaseSelect;//请选择";
    oneItem.value = "";
    dropDownlist.add(oneItem);
    for (var i = 0; i < defaultConditions.length; i++) {
        oneItem = document.createElement('option');
        oneItem.text = defaultConditions[i].LabelText;
        oneItem.value = "1-" + i + "-" + defaultConditions[i].Name;
        if (defaultConditions[i] == condition) {
            oneItem.selected = true;
        }
        dropDownlist.add(oneItem);
    }

    for (var i = 0; i < conditions.length; i++) {
        oneItem = document.createElement('option');
        oneItem.text = conditions[i].LabelText;
        oneItem.value = oneItem.value = "0-" + i + "-" + conditions[i].Name;
        if (conditions[i] == condition) {
            oneItem.selected = true;
        }
        dropDownlist.add(oneItem);
    }
    dropDownlist.onchange = FieldChanged;
    return dropDownlist;
}

//当选中的条件字段发生变化时触发
function FieldChanged() {

    var srcElement = window.event.srcElement;
    
    var trCondition = document.getElementById("trCondition" + srcElement.id.substr(15));
    trCondition.cells(1).innerHTML = "";
    if (trCondition.cells.length == 4)
        trCondition.cells(2).innerHTML = "";
    if (srcElement.selectedIndex > 0) {
        var selectedValue = srcElement.options[srcElement.selectedIndex].value;
        if (selectedValue != "") {
            var _conditions;
            if (selectedValue.charAt(0) == '1') {
                _conditions = defaultConditions;
            }
            else {
                _conditions = conditions;
            }
            var i = selectedValue.indexOf("-", 3);
            if (i > 0) {
                var index = selectedValue.substring(2, i);
                CreateConditionControl(_conditions[index], trCondition);
            }
        }
    }
    else {
        if (trCondition.cells.length == 3) {
            trCondition.cells(1).colSpan = 1;
            trCondition.insertCell(1);
        }
    }
}

//创建关系运算符下拉框
function CreateRelationshipOperatorDropDown(condition, localLineNo) {
    var dropDownlist = document.createElement("select");
    dropDownlist.setAttribute("id", "drplstRS" + localLineNo);
    dropDownlist.setAttribute("name", "drplstRS" + localLineNo);
    for (var i = 0; i < condition.RelationshipOperators.length; i++) {
        var oneItem = document.createElement('option');
        oneItem.text = condition.RelationshipOperators[i].Value;
        oneItem.value = condition.RelationshipOperators[i].Key;
        if (oneItem.value == condition.DefaultRelationshipOperator) {
            oneItem.selected = true;
        }
        dropDownlist.add(oneItem, i);
    }
    return dropDownlist;
}

//创建按范围选择的日期控件
function CreateDateTimeForRange(oneCell, condition, localLineNo,isForConfig) {
    var span = document.createElement("span");
    span.innerText = "从: ";
    span.setAttribute("className", "DynamicNote");
    oneCell.appendChild(span);
    var txtBox = document.createElement("input");
    txtBox.setAttribute("type", "text");
    txtBox.setAttribute("className", "NormalInputText"); //txt
    var inputControlName = GetInputControlName(condition, localLineNo);
    txtBox.setAttribute("id", inputControlName);
    txtBox.setAttribute("name", inputControlName);
    if( isForConfig)
    {
        txtBox.style.width = "200px";
    }
    else
    {
        txtBox.style.width = "90%";
    }
    //txtBox.setAttribute("style.",width);
    if (condition.ColDef.DataTypeCatalog == 1 && condition.ColDef.Length > 0) {
        txtBox.setAttribute("maxLength", condition.ColDef.Length);
    }
    oneCell.appendChild(txtBox);

    var img = document.createElement("img");
    img.onclick = SelectDate;
    img.id = "img" + inputControlName;
    img.src = "../../layout/Calendar/Button.gif";
    img.style.cursor = "hand";
    img.style.position ="relative";
    img.style.left = "-25px";
    img.border = 0;
    img.align = "absmiddle";
    img.alt = condition.FormatString;
    img.setAttribute("alt", "");
    oneCell.appendChild(img);
    oneCell.appendChild(document.createElement("br"));
    span = document.createElement("span");
    span.innerText = " 到: ";
    span.setAttribute("className", "DynamicNote");
    oneCell.appendChild(span);
    txtBox = document.createElement("input");
    txtBox.setAttribute("type", "text");
    txtBox.setAttribute("className", "NormalInputText"); //txt
    var controlName = inputControlName + "To";
    txtBox.setAttribute("id", controlName);
    txtBox.setAttribute("name", controlName);
    if(isForConfig)
    {
        txtBox.style.width = "200px";
    }
    else
    {
        txtBox.style.width = "90%";
    }
    //txtBox.setAttribute("style.",width);
    if (condition.ColDef.DataTypeCatalog == 1 && condition.ColDef.Length > 0) {
        txtBox.setAttribute("maxLength", condition.ColDef.Length);
    }
    oneCell.appendChild(txtBox);
    img = document.createElement("img");
    img.onclick = SelectDate;
    img.id = "img" + controlName;
    img.src = "../../layout/Calendar/Button.gif";
    img.style.cursor = "hand";
    img.style.position ="relative";
    img.style.left = "-25px";
    img.border = 0;
    img.align = "absmiddle";
    img.alt = condition.FormatString;
    img.setAttribute("alt", "");
    oneCell.appendChild(img);

}
//创建按范围选择的文本控件
function CreateTextBoxForRange(oneCell, condition, localLineNo,isForConfig) {
    var span = document.createElement("span");
    span.innerText = "从: ";
    span.setAttribute("className", "DynamicNote");
    oneCell.appendChild(span);
    var txtBox = document.createElement("input");
    txtBox.setAttribute("type", "text");
    txtBox.setAttribute("className", "NormalInputText"); //txt
    var inputControlName = GetInputControlName(condition, localLineNo);
    txtBox.setAttribute("id", inputControlName);
    txtBox.setAttribute("name", inputControlName);
    if(isForConfig)
        txtBox.style.width = "200px";
    else
        txtBox.style.width = "90%";
    //txtBox.setAttribute("style.",width);
    if (condition.ColDef.DataTypeCatalog == 1 && condition.ColDef.Length > 0) {
        txtBox.setAttribute("maxLength", condition.ColDef.Length);
    }
    oneCell.appendChild(txtBox);
    oneCell.appendChild(document.createElement("br"));
    span = document.createElement("span");
    span.innerText = " 到: ";
    span.setAttribute("className", "DynamicNote");
    oneCell.appendChild(span);
    txtBox = document.createElement("input");
    txtBox.setAttribute("type", "text");
    txtBox.setAttribute("className", "NormalInputText"); //txt
    var controlName = inputControlName + "To"
    txtBox.setAttribute("id", controlName);
    txtBox.setAttribute("name", controlName);
    if(isForConfig)
        txtBox.style.width = "200px";
    else
        txtBox.style.width = "90%";
    //txtBox.setAttribute("style.",width);
    if (condition.ColDef.DataTypeCatalog == 1 && condition.ColDef.Length > 0) {
        txtBox.setAttribute("maxLength", condition.ColDef.Length);
    }
    oneCell.appendChild(txtBox);
}

//创建文本框输入项控件
function CreateTextBox(condition, localLineNo, width) {
    var txtBox = document.createElement("input");
    txtBox.setAttribute("type", "text");
    txtBox.setAttribute("className", "NormalInputText"); //txt
    var inputControlName = GetInputControlName(condition, localLineNo);
    txtBox.setAttribute("id", inputControlName);
    txtBox.setAttribute("name", inputControlName);
    txtBox.style.width = width;
    //txtBox.setAttribute("style.",width);
    if (condition.ColDef.DataTypeCatalog == 1 && condition.ColDef.Length > 0) {
        txtBox.setAttribute("maxLength", condition.ColDef.Length);
    }
    return txtBox;
}

//为列表项从后台数据库获取数据源
function GetListDataFromDB(condition) {
    var result = new Array();
    var oXMLDoc = new ActiveXObject('Microsoft.XMLDOM');
    oXMLDoc.async = false;
    var url = "FrmGetXMLForQuery.aspx?ReportName=" + escape(reportName) + "&ConditionName=" + condition.Name;
    oXMLDoc.load(url);
    var objNodeList = oXMLDoc.selectNodes("Lists/Item");
    for (var i = 0; i < objNodeList.length; i++) {
        result[i] = new MyKeyValue();
        result[i].Key = objNodeList[i].attributes[0].nodeValue;
        result[i].Value = objNodeList[i].attributes[1].nodeValue;
    }

    return result;
}

//创建条件字段下拉框
function CreateDropDown(condition, localLineNo) {
    var dropDownlist = document.createElement("select");
    var inputControlName = GetInputControlName(condition, localLineNo);
    dropDownlist.setAttribute("id", inputControlName);
    dropDownlist.setAttribute("name", inputControlName);

    dropDownlist.style.width = "96%";
    var oneItem = document.createElement('option');
    oneItem.text = "::全部::";
    oneItem.value = "";
    dropDownlist.add(oneItem, 0);
    if (condition.ListData.LoadCatalog == EnumLoadCatalog.prototype.FieldData) {

        for (var j = 0; j < condition.ListData.Lists.length; j++) {
            oneItem = document.createElement('option');
            oneItem.text = condition.ListData.Lists[j].Value;
            oneItem.value = condition.ListData.Lists[j].Key;
            dropDownlist.add(oneItem, j);
        }
    }
    else {
        condition.ListData.LoadCatalog = EnumLoadCatalog.prototype.FieldData;
        condition.ListData.Lists = GetListDataFromDB(condition);
        for (var j = 0; j < condition.ListData.Lists.length; j++) {
            oneItem = document.createElement('option');
            oneItem.text = condition.ListData.Lists[j].Value;
            oneItem.value = condition.ListData.Lists[j].Key;
            dropDownlist.add(oneItem, j);
        }
    }
    return dropDownlist;
}

//创建逻辑运算符下拉框
function CreateLogicalDropDown(condition, localLineNo) {
    var dropDownlist = document.createElement("select");

    dropDownlist.setAttribute("id", "drpLstLogical" + localLineNo);
    dropDownlist.setAttribute("name", "drpLstLogical" + localLineNo);

    var oneItem = document.createElement('option');
    oneItem.text = "并且"
    oneItem.value = "and";
    if (condition != null && condition.LogicalOperator == "and") {
        oneItem.selected = true;
    }
    dropDownlist.add(oneItem, 0);
    oneItem = document.createElement('option');
    oneItem.text = "或者"
    oneItem.value = "or";
    if (condition != null && condition.LogicalOperator == "or") {
        oneItem.selected = true;
    }
    dropDownlist.add(oneItem, 1);
    return dropDownlist;
}

//创建检查框输入项
function CreateCheckBox(condition, localLineNo) {
    var chkBox = document.createElement("input");
    var inputControlName = GetInputControlName(condition, localLineNo);
    chkBox.id = inputControlName;
    chkBox.name = inputControlName;
    chkBox.setAttribute("type", "checkbox");
    chkBox.setAttribute("class", "NormalInputText");
    return chkBox;
}

//创建打开窗体选择代码的输入项控件
function CreateSelectCodeImg(condition, localLineNo) {
    var img = document.createElement("img");
    //img.onclick = SelectDate;
    img.id = "img" + GetInputControlName(condition, localLineNo);
    img.src = "../images/btn_find.jpg";
    img.style.cursor = "hand";
    img.border = 0;
    img.align = "absmiddle";
    img.setAttribute("alt", "");
    return img;
}

//获取某一条件的输入项控件的名称
function GetInputControlName(condition, localLineNo) {
    return condition.InputControlName + localLineNo;
}

//日期选择输入
function SelectDate() {
    var srcElement = window.event.srcElement;
    var txtBoxId = srcElement.id.substr(3);
    showCalendar(txtBoxId, 'y-mm-dd');
    return false;
}

//创建日期输入项的img按钮
function CreateDateTimeImg(condition, localLineNo) {
    var img = document.createElement("img");
    img.onclick = SelectDate;
    var inputControlName = GetInputControlName(condition, localLineNo);
    img.id = "img" + inputControlName;
    img.src = "../../layout/Calendar/Button.gif";
    img.style.cursor = "hand";
    img.style.position = "relative";
    img.style.left= "-30px";
    img.style.top= "0px";
    img.border = 0;
    img.align = "absmiddle";
    img.alt = condition.FormatString;
    img.setAttribute("alt", "");
    return img;
}

//创建检查框列表
function CreateCheckBoxList(condition, localLineNo) {
    var tab = document.createElement("table");
    tab.border = "0";
    var inputControlName = GetInputControlName(condition, localLineNo);
    tab.id = inputControlName;
    if (condition.ListData.LoadCatalog != EnumLoadCatalog.prototype.FieldData) {
        condition.ListData.LoadCatalog = EnumLoadCatalog.prototype.FieldData;
        condition.ListData.Lists = GetListDataFromDB(condition);
    }
    if (condition.ListData.LoadCatalog == EnumLoadCatalog.prototype.FieldData) {
        var oneRow = tab.insertRow(j);
        var oneCell = oneRow.insertCell(0);
        oneRow.style.height = 0;
        oneRow.style.display = "none";
        oneCell.style.display = "none";
        var chkBox = document.createElement("input");
        chkBox.value = condition.ListData.Lists.length;
        chkBox.checked = true;
        chkBox.name = inputControlName + "$0";
        oneCell.appendChild(chkBox);
        for (var j = 0; j < condition.ListData.Lists.length; j++) {
            oneRow = tab.insertRow(j);
            oneCell = oneRow.insertCell(0);
            oneCell.align = "left";
            chkBox = document.createElement("input");
            chkBox.id = inputControlName + "_" + (j + 1).toString();
            chkBox.type = "checkbox";
            chkBox.name = inputControlName + "$" + (j + 1).toString();
            chkBox.value = condition.ListData.Lists[j].Key;
            oneCell.appendChild(chkBox);
            var label = document.createElement("label");
            label.setAttribute("for", chkBox.id);
            label.innerText = condition.ListData.Lists[j].Value;
            oneCell.appendChild(label);
        }
    }
    return tab;
}

//创建列表框输入项控件
function CreateListBox(condition, localLineNo) {
    var dropDownlist = document.createElement("select");
    var inputControlName = GetInputControlName(condition, localLineNo);
    dropDownlist.setAttribute("id", inputControlName);
    dropDownlist.setAttribute("name", inputControlName);

    dropDownlist.style.width = "95%";
    if (condition.ListData.LoadCatalog == EnumLoadCatalog.prototype.FieldData) {
        dropDownlist.setAttribute("size", condition.ListData.Lists.length);
        dropDownlist.multiple = "multiple";
        for (var j = 0; j < condition.ListData.Lists.length; j++) {
            var oneItem = document.createElement('option');
            oneItem.text = condition.ListData.Lists[j].Value;
            oneItem.value = condition.ListData.Lists[j].Key;
            dropDownlist.add(oneItem, j);
        }
    }
    return dropDownlist;
}

function CreateRadioButtonList(condition, localLineNo) {
    /*
    <table id="RadioButtonList1" border="0">
    <tr>
    <td><input id="RadioButtonList1_0" type="radio" name="RadioButtonList1" value="a" /><label for="RadioButtonList1_0">21</label></td>
    </tr><tr>
    <td><input id="RadioButtonList1_1" type="radio" name="RadioButtonList1" value="b" /><label for="RadioButtonList1_1">1221</label></td>
    </tr><tr>
    <td><input id="RadioButtonList1_2" type="radio" name="RadioButtonList1" value="c" /><label for="RadioButtonList1_2">dsdsds</label></td>
    </tr><tr>
    <td><input id="RadioButtonList1_3" type="radio" name="RadioButtonList1" value="d" /><label for="RadioButtonList1_3">sadsds</label></td>
    </tr>
    </table>
    */
    var tab = document.createElement("table");
    tab.border = 0;
    var inputControlName = GetInputControlName(condition, localLineNo);
    tab.id = inputControlName;
    var oneRow = tab.insertRow(j);
    var oneCell = oneRow.insertCell(0);
    var rb = document.createElement("input");
    rb.id = inputControlName + "_0";
    rb.type = "radio";
    rb.name = inputControlName;
    rb.value = "";
    oneCell.appendChild(rb);
    var label = document.createElement("label");
    label.setAttribute("for", rb.id);
    label.innerText = "::全部::";
    oneCell.appendChild(label);
    if (condition.ListData.LoadCatalog != EnumLoadCatalog.prototype.FieldData) {
        condition.ListData.LoadCatalog = EnumLoadCatalog.prototype.FieldData;
        condition.ListData.Lists = GetListDataFromDB(condition);
    }
    for (var j = 0; j < condition.ListData.Lists.length; j++) {
        oneRow = tab.insertRow(j);
        oneCell = oneRow.insertCell(0);
        oneCell.align = "left";
        rb = document.createElement("input");
        rb.id = inputControlName + "_" + (j + 1);
        rb.type = "radio";
        rb.name = inputControlName;
        rb.value = condition.ListData.Lists[j].Key;
        oneCell.appendChild(rb);
        var label = document.createElement("label");
        label.setAttribute("for", rb.id);
        label.innerText = condition.ListData.Lists[j].Value;
        oneCell.appendChild(label);
    }
    return tab;
}

//创建空格项
function CreateSpace(numofspace) {
    var span = document.createElement("span");
    var space = "";
    for (var i = 0; i < numofspace; i++) {
        space = space + " ";
    }
    span.innerText = space;
    return span;
}

var lineNo = 0;

function CreateConditionLabel(condition, localLineNo) {
    var span = document.createElement("span");
    span.innerText = condition.LabelText;
    return span;
}

//创建某一行的输入条件,为最终用户使用的
function CreateConditionControlForRun(condition) {
    var tabCondition = document.getElementById("tabCondition");
    var newRow, oneCell
    var localLineNo = 0;
    var startCellIndex = 0;
    localLineNo = lineNo;
    if (localLineNo % 2 == 0) {
        newRow = tabCondition.insertRow(tabCondition.rows.length - 1); //tabCondition.rows.length - 3);      
        while (newRow.cells.length < 4) {
            oneCell = newRow.insertCell(0);
            oneCell.style.whiteSpace = "nowrap";
            //oneCell.style.width = "25%";
        }
        startCellIndex = 0;
    }
    else {
        newRow = tabCondition.rows[localLineNo / 2];
        startCellIndex = 2;
    }
    var index = tabCondition.rows.length - 2;
    oneCell = newRow.cells[startCellIndex];
    var conditionLabel = CreateConditionLabel(condition, localLineNo);
    oneCell.appendChild(conditionLabel);
    oneCell.style.width = "20%";
    oneCell.className = "InputLabel";
    //oneCell.setAttribute("align","right");
    lineNo = lineNo + 1;

    if (condition.IsRange == false) {
        oneCell = newRow.cells(startCellIndex + 1);
        oneCell.style.width = "30%";
        if (condition.ShowType == EnumShowType.prototype.GeneralText) {
            oneCell.appendChild(CreateTextBox(condition, localLineNo, "95%"));
        }
        else if (condition.ShowType == EnumShowType.prototype.DropDownList) {
            oneCell.appendChild(CreateDropDown(condition, localLineNo));
        }
        else if (condition.ShowType == EnumShowType.prototype.CheckBox) {
            oneCell.appendChild(CreateCheckBox(condition, localLineNo));
        }
        else if (condition.ShowType == EnumShowType.prototype.DateTimePicker) {
            oneCell.appendChild(CreateTextBox(condition, localLineNo, "95%"));
            //oneCell.appendChild(CreateSpace(1));
            oneCell.appendChild(CreateDateTimeImg(condition, localLineNo));
        }
        else if (condition.ShowType == EnumShowType.prototype.CheckBoxList) {
            oneCell.appendChild(CreateCheckBoxList(condition, localLineNo));
        }
        else if (condition.ShowType == EnumShowType.prototype.Listbox) {
            oneCell.appendChild(CreateListBox(condition, localLineNo));
        }
        else if (condition.ShowType == EnumShowType.prototype.RadioButtonList) {
            oneCell.appendChild(CreateRadioButtonList(condition, localLineNo));
        }
        else if (condition.ShowType == EnumShowType.prototype.OpenWin) {
            //2012-06-19
            _CreateCodeNameSelectControl(oneCell,"CNS_"+condition.Name + localLineNo,condition.ListData.Lists[0].Value,condition.ListData.Lists[1].Value);
            //oneCell.appendChild(CreateTextBox(condition, localLineNo, "80%"));
            //oneCell.appendChild(CreateSpace(1));
            //oneCell.appendChild(CreateSelectCodeImg(condition));
        }
    }
    else {
        oneCell = newRow.cells(startCellIndex + 1);
        if (condition.ShowType == EnumShowType.prototype.GeneralText) {
            CreateTextBoxForRange(oneCell, condition, localLineNo,false);
        }
        else if (condition.ShowType == EnumShowType.prototype.DateTimePicker) {
            CreateDateTimeForRange(oneCell, condition, localLineNo,false);
        }
    }
}

function _CreateCodeNameSelectControl(ctrHolder, crlID, servicePath, webMethod) {

    if(ctrHolder === null) {
        alert("创建CodeNameSelecte控件失败："+crlID);
        return;
    
    }
    
    var cnsDIV =document.createElement("DIV");
    cnsDIV.id=crlID;
    
    var txtCode = document.createElement("INPUT");
    txtCode.setAttribute("type", "text");
    txtCode.setAttribute("id",crlID +"_txtCode");
    txtCode.setAttribute("name",crlID +"_txtCode");
    txtCode.style.width="95%";
    
    var txtSpan =document.createElement("SPAN");    
    txtSpan.setAttribute("id", crlID + "_Span");
    
    var cnsClientState =document.createElement("INPUT");
    cnsClientState.setAttribute("type", "hidden");
    cnsClientState.setAttribute("id", crlID + "_ClientState");
    cnsClientState.value = "[{Code:'',Name:''}]";
    
    
    cnsDIV.appendChild(txtCode);
    ctrHolder.appendChild(cnsClientState);
    ctrHolder.appendChild(txtSpan);
    ctrHolder.appendChild(cnsDIV);

    $create(DCIS.Web.CodeNameSelect, { "CallbackID": crlID, "CodeNameWindowForm_TitleText": "神州数码（苏州）国信有限公司-代码选择窗口", "DialogCssClass": "dialog", "DialogDataListCssClass": "List", "ElementWidth": "95%", "autoComplete": true, "clientStateField": $get(crlID + "_ClientState"), "completionInterval": 200, "completionListCssClass": "completionListElement", "completionListItemCssClass": "listItem", "highlightedItemCssClass": "highlightedListItem", "minimumPrefixLength": 2, "mutiSelecte": false, "nameControlID": "", "onHide": "", "onSelectedComplete": "", "onShow": "", "onWindowHide": "", "onWindowShow": "", "serviceMethod": webMethod, "servicePath": servicePath }, null, null, $get(crlID));

}

//创建某一行的输入条件，为Web配置界面使用的
function CreateConditionControl(condition, trCondition) {
    var tabCondition = document.getElementById("tabCondition");
    var newRow, oneCell
    var localLineNo = 0;
    if (trCondition != null) {
        newRow = trCondition;
        localLineNo = trCondition.id.substr(11);
    }
    else {
        localLineNo = lineNo;
        //Modify By ChenKai 为适应界面调整的需要,将以下原为"2"的两行改成了"1"
        var index = tabCondition.rows.length - 1;
        newRow = tabCondition.insertRow(tabCondition.rows.length - 1);
        newRow.setAttribute("id", "trCondition" + localLineNo);
        oneCell = newRow.insertCell(0);
        var dropDownlist = CreateFieldDropDown(condition, localLineNo);

        oneCell.appendChild(dropDownlist);
        oneCell.align = "center";
        lineNo = lineNo + 1;
    }

    //重设单元格数
    if (condition != null) {
        if (condition.IsRange) {
            if (newRow.cells.length > 3) {
                newRow.deleteCell(1);
                newRow.cells[1].colSpan = 2;
            }
            else {
                if (newRow.cells.length > 1)
                    newRow.cells[1].colSpan = 1;
                while (newRow.cells.length < 3) {

                    oneCell = newRow.insertCell(1);
                    oneCell.align = "center";
                }
                newRow.cells[1].colSpan = 2;
            }
        }
        else {
            if (newRow.cells.length > 1)
                newRow.cells[1].colSpan = 1;
            while (newRow.cells.length < 4) {
                oneCell = newRow.insertCell(1);
                oneCell.align = "center";
            }
        }
    }
    else {
        if (newRow.cells.length > 1)
            newRow.cells[1].colSpan = 1;
        while (newRow.cells.length < 4) {
            oneCell = newRow.insertCell(1);
            oneCell.align = "center";
        }
    }

    ///关系运算符
    if (condition != null) {
        if (condition.IsRange == false) {
            if (condition.HasRelationship) {
                dropDownlist = CreateRelationshipOperatorDropDown(condition, localLineNo);
                newRow.cells(1).appendChild(dropDownlist);
            }
            else {
                newRow.cells(1).innerText = "等于";
                newRow.cells(1).setAttribute("className", "DynamicNote");
            }
        }

    }



    if (condition != null) {
        if (condition.IsRange == false) {
            oneCell = newRow.cells(2);
            if (condition.ShowType == EnumShowType.prototype.GeneralText) {
                oneCell.appendChild(CreateTextBox(condition, localLineNo, "95%"));
            }
            else if (condition.ShowType == EnumShowType.prototype.DropDownList) {
                oneCell.appendChild(CreateDropDown(condition, localLineNo));
            }
            else if (condition.ShowType == EnumShowType.prototype.CheckBox) {
                oneCell.appendChild(CreateCheckBox(condition, localLineNo));
            }
            else if (condition.ShowType == EnumShowType.prototype.DateTimePicker) {
                oneCell.appendChild(CreateTextBox(condition, localLineNo, "80%"));
                oneCell.appendChild(CreateSpace(1));
                oneCell.appendChild(CreateDateTimeImg(condition, localLineNo));
            }
            else if (condition.ShowType == EnumShowType.prototype.CheckBoxList) {
                oneCell.appendChild(CreateCheckBoxList(condition, localLineNo));
            }
            else if (condition.ShowType == EnumShowType.prototype.Listbox) {
                oneCell.appendChild(CreateListBox(condition, localLineNo));
            }
            else if (condition.ShowType == EnumShowType.prototype.RadioButtonList) {
                oneCell.appendChild(CreateRadioButtonList(condition, localLineNo));
            }
            else if (condition.ShowType == EnumShowType.prototype.OpenWin) {
                // 2012-06-19
                //condition.ListData.Lists
                _CreateCodeNameSelectControl(oneCell,"CNS_"+condition.Name,condition.ListData.Lists[0].Value,condition.ListData.Lists[1].Value);
                //oneCell.appendChild(CreateTextBox(condition, localLineNo, "80%"));
                //oneCell.appendChild(CreateSpace(1));
                //oneCell.appendChild(CreateSelectCodeImg(condition));
            }
        }
        else {
            oneCell = newRow.cells(1);
            if (condition.ShowType == EnumShowType.prototype.GeneralText) {
                CreateTextBoxForRange(oneCell, condition, localLineNo,true);
            }
            else if (condition.ShowType == EnumShowType.prototype.DateTimePicker) {
                CreateDateTimeForRange(oneCell, condition, localLineNo,true);
            }
        }
    }
    if (trCondition == null) {

        oneCell = newRow.cells(newRow.cells.length - 1);
        oneCell.appendChild(CreateLogicalDropDown(condition, localLineNo));
        oneCell.setAttribute("align", "center");
    }
}

//删除无效的条件行
function DeleteInvalidRows() {
    var tabCondition = document.getElementById("tabCondition");
    var i = 1;

    while (i < tabCondition.rows.length) {
        var trCondition = tabCondition.rows[i];
        if (trCondition.cells[0].children.length > 0) {
            var drpList = trCondition.cells[0].children[0];
            if (drpList.selectedIndex <= 0) {
                tabCondition.deleteRow(i);
            }
            else {
                i++;
            }
        }
        else {
            i++;
        }
    }
}



//配置显示栏位的一组函数
//显示或者隐藏显示栏位的配置控件
function ShowOrHideColumnConfig() {
    document.body.offsetWidth
    window.screen.width
    //QueryOnChang();
    if (Table4.style.display == "none") {
        Table4.style.display = "";
        Table4.style.zIndex = 99;
        Table4.style.position = "absolute";
        //Table4.style.left = 100;
        Table4.style.width = 600;
        Table4.style.left = (document.body.offsetWidth - 600) / 2;
        Table4.style.top = 20;
        Table4.style.backgroundColor = "#ffffff";

    }
    else {
        Table4.style.display = "none";
    }
}

//隐藏显示栏位的配置控件
function HideColumnConfig() {
    Table4.style.display = "none";
}

//装载选中的栏位
function LoadSelectedColumn() {
    var oXMLDoc = new ActiveXObject('Microsoft.XMLDOM');
    oXMLDoc.async = false;
    var customQueryXML = "FrmGetXMLForQuery.aspx?ReportName=" + escape(reportName);
    oXMLDoc.load(customQueryXML);
    var objNodeList = oXMLDoc.selectNodes("root/Grid/DisplayColumns/Column");
    for (var i = 0; i < objNodeList.length; i++) {
        var displayColNode = objNodeList[i];
        var oneItem = document.createElement("option");
        oneItem.value = getNodeStringValue(displayColNode, "Name");
        oneItem.text = getNodeStringValue(displayColNode, "HeaderText");
        document.getElementById("DisplayColumns").add(oneItem);
    }

}

//装载没有选中的栏位
function LoadNonSelectedColumn() {
    var oXMLDoc = new ActiveXObject('Microsoft.XMLDOM');
    oXMLDoc.async = false;
    var customQueryXML = "FrmGetXMLForQuery.aspx?ReportName=" + escape(reportName);
    oXMLDoc.load(customQueryXML);
    var objNodeList = oXMLDoc.selectNodes("root/Grid/Columns/Column");
    for (var i = 0; i < objNodeList.length; i++) {
        var displayColNode = objNodeList[i];
        var oneItem = document.createElement("option");
        oneItem.value = getNodeStringValue(displayColNode, "Name");
        oneItem.text = getNodeStringValue(displayColNode, "HeaderText");
        document.getElementById("NonDisplayColumns").add(oneItem);
    }
}

//将需要显示的栏位调整为不需要显示的
function DisplayColumnToNon() {
    var DisplayColumns = document.getElementById("DisplayColumns");
    var NonDisplayColumns = document.getElementById("NonDisplayColumns");
    var i = 0;
    while (i < DisplayColumns.options.length) {
        if (DisplayColumns.options[i].selected) {
            var oneItem = DisplayColumns.options[i];
            DisplayColumns.options.remove(i);
            NonDisplayColumns.options.add(oneItem);
        }
        else {
            i++;
        }
    }
}


function DisplayAllColumnToNon() {
    var DisplayColumns = document.getElementById("DisplayColumns");
    var NonDisplayColumns = document.getElementById("NonDisplayColumns");
    var i = 0;
    while (i < DisplayColumns.options.length) {
        //if (DisplayColumns.options[i].selected) {
            var oneItem = DisplayColumns.options[i];
            DisplayColumns.options.remove(i);
            NonDisplayColumns.options.add(oneItem);
        //}
       
    }
}

//将不需要显示的栏位调整为需要显示的
function NonDisplayColumnToDisplay() {
    //
    var DisplayColumns = document.getElementById("DisplayColumns");
    var NonDisplayColumns = document.getElementById("NonDisplayColumns");
    var i = 0;
    while (i < NonDisplayColumns.options.length) {
        if (NonDisplayColumns.options[i].selected) {
            var oneItem = NonDisplayColumns.options[i];
            NonDisplayColumns.options.remove(i);
            DisplayColumns.options.add(oneItem);
        }
        else {
            i++;
        }
    }
}

//将不需要显示的栏位调整为需要显示的
function NonDisplayAllColumnToDisplay() {
    //
    var DisplayColumns = document.getElementById("DisplayColumns");
    var NonDisplayColumns = document.getElementById("NonDisplayColumns");
    var i = 0;
    while (i < NonDisplayColumns.options.length) {
        //if (NonDisplayColumns.options[i].selected) {
            var oneItem = NonDisplayColumns.options[i];
            NonDisplayColumns.options.remove(i);
            DisplayColumns.options.add(oneItem);
//        }
//        else {
//            i++;
//        }
    }
}

//将栏位移到后面
function SelectedColumnToDown() {
    //document.getElementById("NonDisplayColumns")
    var DisplayColumns = document.getElementById("DisplayColumns");
    if (DisplayColumns.options.length >= 1 && DisplayColumns.options[DisplayColumns.options.length - 1].selected) {
        alert("不能往下移了");
        return;
    }
    for (var i = DisplayColumns.options.length - 2; i >= 0; i--) {
        if (DisplayColumns.options[i].selected) {
            var oneItem = DisplayColumns.options[i + 1];
            DisplayColumns.options.remove(i + 1); //先移调下一个元素
            DisplayColumns.options.add(oneItem, i); //然后将其插入到当前元素之前                
        }
    }
}

//将栏位移到前面
function SelectedColumnToUp() {
    //document.getElementById("NonDisplayColumns")
    var DisplayColumns = document.getElementById("DisplayColumns");
    if (DisplayColumns.options.length >= 1 && DisplayColumns.options[0].selected) {
        alert("不能往上移了");
        return;
    }
    for (var i = 1; i < DisplayColumns.options.length; i++) {
        if (DisplayColumns.options[i].selected) {
            var oneItem = DisplayColumns.options[i];

            DisplayColumns.options.remove(i); //先移调自己
            DisplayColumns.options.add(oneItem, i - 1); //然后将其插入到前一个元素之前   
        }
    }
}

//选中所有栏位，以便后台服务器能够得到这些值
function SelectAllColumns() {
    var DisplayColumns = document.getElementById("DisplayColumns");
    for (var i = 0; i < DisplayColumns.options.length; i++) {
        DisplayColumns.options[i].selected = true;
    }

    var NonDisplayColumns = document.getElementById("NonDisplayColumns");
    for (var i = 0; i < NonDisplayColumns.options.length; i++) {
        NonDisplayColumns.options[i].selected = true;
    }
}