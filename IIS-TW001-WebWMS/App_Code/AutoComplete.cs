using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Web.Services;
using System.Collections.Generic;



/// <summary>
///AutoComplete 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService
{

    public AutoComplete()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
}
   

