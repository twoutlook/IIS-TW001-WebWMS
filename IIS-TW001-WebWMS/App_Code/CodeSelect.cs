using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Web.Script.Serialization;
/// <summary>
///CodeSelect 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class CodeSelect : System.Web.Services.WebService
{    
   
    private string _codeField;
    private string _nameField;

    private void SetFieldName(string tableName)
    {
        switch (tableName.ToUpper())
        {
            case "PUB_PARA.UNIT":
                {
                    _codeField = "unit_CODE";
                    _nameField = "UNIT_NAME";
                    break;
                }
            case "PUB_PARA.CURR":
                {
                    _codeField = "curr_code";
                    _nameField = "CURR_NAME";
                    break;
                }
            default:
                {
                    _codeField = "CODE";
                    _nameField = "NAME";
                    break;
                }
        }

    }

}

