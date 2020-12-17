using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// CommonService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class CommonService : System.Web.Services.WebService {

    [WebMethod]
    public WebServiceResult SaveParameter(WMSSYS_Parameter parameter)
    {
        WebServiceResult result = new WebServiceResult("0", "保存成功！");
        if (parameter == null)
        {
            result.code = "1";
            result.msg = "参数错误，保存失败！";
            return result;
        }

        #region 部分规则再验证
        if (parameter.FlagNames == null || !parameter.FlagNames.Any())
        {
            result.code = "1";
            result.msg = "名称不能为空!";
            return result;
        }

        #endregion

        DBContext context = new DBContext();
        using (var modContext = context)
        {
            using (var dbContextTransaction = modContext.Database.BeginTransaction())
            {
                try
                {
                    var modGroup = modContext.SYS_PARAMETERGROUP.Where(x => x.ID == parameter.GroupID).FirstOrDefault();
                    if (modGroup == null) {
                        result.code = "1";
                        result.msg = "数据错误，保存失败!";
                        return result;
                    }

                    //默认语言
                    var modConfig = modContext.SYS_CONFIG.Where(x => x.code == "130003").FirstOrDefault();
                    var langList = modContext.SYS_PARAMETER.Where(x=>x.flag_type=="Language").OrderBy(x=>x.flag_id).ToList();

                    string defaultLang = modConfig != null ? modConfig.config_value : "";
                    if (string.IsNullOrEmpty(defaultLang)) {
                        defaultLang = langList != null ? langList.FirstOrDefault().flag_id : "";
                    }
                    string defaultName = "";

                    if (parameter.Operation == "Modify")
                    { 
                        //修改
                        var modParameter = modContext.SYS_PARAMETER.Where(x => x.ID == parameter.ID && x.flag_type == modGroup.FLAG_TYPE).FirstOrDefault();
                        if (modParameter == null) {
                            result.code = "1";
                            result.msg = "数据错误，保存失败!";
                            return result;
                        }

                        //判断相同组下是否已存在对应code值
                        if (modContext.SYS_PARAMETER.Any(x => x.flag_type == modGroup.FLAG_TYPE && x.flag_id == parameter.FlagId && x.ID != modParameter.ID)) {
                            result.code = "1";
                            result.msg = "子项编号已存在!";
                            return result;
                        }

                        modParameter.flag_id = parameter.FlagId;
                        modParameter.sortid = Convert.ToDecimal(parameter.SortId);

                        foreach (var item in parameter.FlagNames) {
                            var modName =  modContext.SYS_PARAMETERNAME.Where(x => x.FLAG_GUID == modParameter.ID && x.LANGUAGEID.ToUpper() == item.Lan.ToUpper()).FirstOrDefault();
                            if (modName == null)
                            {
                                modName = new SYS_PARAMETERNAME();
                                modName.ID = Guid.NewGuid().ToString();
                                modName.FLAG_GUID = modParameter.ID;
                                modName.FLAG_ID = modParameter.flag_id;
                                modName.FLAG_NAME = item.Name;
                                modName.LANGUAGEID = item.Lan;
                                modName.REMARK = "";
                                modName.CREATETIME = DateTime.Now;
                                modContext.SYS_PARAMETERNAME.Add(modName);
                            }
                            else {
                                if (modName.FLAG_NAME != item.Name) {
                                    modName.FLAG_NAME = item.Name;
                                    modContext.Entry(modName).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                            if (defaultLang.ToUpper() == item.Lan.ToUpper()) {
                                defaultName = item.Name;
                            }
                        }

                        modParameter.flag_name = defaultName;
                        modContext.Entry(modParameter).State = System.Data.Entity.EntityState.Modified;
                        modContext.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    else if (parameter.Operation == "New")
                    {
                        //新增
                        string keyId = Guid.NewGuid().ToString();

                        //判断相同组下是否已存在对应code值
                        if (modContext.SYS_PARAMETER.Any(x => x.flag_type == modGroup.FLAG_TYPE && x.flag_id == parameter.FlagId))
                        {
                            result.code = "1";
                            result.msg = "子项编号已存在!";
                            return result;
                        }

                        SYS_PARAMETER modParameter = new SYS_PARAMETER();
                        modParameter.ID = keyId;
                        modParameter.flag_type = modGroup.FLAG_TYPE;
                        modParameter.memo = modGroup.REMARK;
                        modParameter.flag_id = parameter.FlagId;
                        modParameter.sortid = Convert.ToDecimal(parameter.SortId);

                        foreach (var item in parameter.FlagNames)
                        {
                            SYS_PARAMETERNAME modName = new SYS_PARAMETERNAME();
                            modName.ID = Guid.NewGuid().ToString();
                            modName.FLAG_GUID = keyId;
                            modName.FLAG_ID = modParameter.flag_id;
                            modName.FLAG_NAME = item.Name;
                            modName.LANGUAGEID = item.Lan;
                            modName.REMARK = "";
                            modName.CREATETIME = DateTime.Now;
                            modContext.SYS_PARAMETERNAME.Add(modName);

                            if (defaultLang.ToUpper() == item.Lan.ToUpper())
                            {
                                defaultName = item.Name;
                            }
                        }


                        modParameter.flag_name = defaultName;
                        modContext.SYS_PARAMETER.Add(modParameter);
                        modContext.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    else {
                        result.code = "1";
                        result.msg = "错误的操作方式!";
                        return result;
                    }
                    CacheHelper.RemoveAllCache();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    result.code = "1";
                    result.msg = "保存失败，错误信息：" + ex.Message;
                }
            }
        }

        return result;
    }
    



}


public class WMSSYS_Parameter {
    public string ID { get; set; }
    public string GroupID { get; set; }
    public string Operation { get; set; }
    public string FlagId { get; set; }
    public string SortId { get; set; }
    public List<WMS_FlagName> FlagNames { get; set; }
}

public class WMS_FlagName {
    public string Name { get; set; }
    public string Lan { get; set; }
}