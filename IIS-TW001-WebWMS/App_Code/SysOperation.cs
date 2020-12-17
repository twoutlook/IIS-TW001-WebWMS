using System;
using System.Collections.Generic;
using System.Web;

///// <summary>
///// SysOperation 的摘要说明
///// </summary>
//public class SysOperation
//{
//    public SysOperation()
//    {
//        //
//        // TODO: 在此处添加构造函数逻辑
//        //
//    }
//}

public enum SYSOperation
{
    /// <summary>
    /// 新增
    /// </summary>
    New,
    /// <summary>
    /// 查看
    /// </summary>
    View,
    /// <summary>
    /// 修改
    /// </summary>
    Modify,
    /// <summary>
    /// 查询
    /// </summary>
    Query,
    /// <summary>
    /// 删除
    /// </summary>
    Delete,
    /// <summary>
    /// 申请
    /// </summary>
    Apply,
    /// <summary>
    /// 审批
    /// </summary>        
    Approve,
    /// <summary>
    /// 驳回
    /// </summary>
    Reject,
    /// <summary>
    /// 更高一级审批
    /// </summary>
    SecondAudit,
    /// <summary>
    /// 更高一级驳回
    /// </summary>
    SecondReject,
    /// <summary>
    /// 预留动作1
    /// </summary>
    Preserved1,
    /// <summary>
    /// 预留动作2
    /// </summary>
    Preserved2,
    /// <summary>
    /// 预留动作3
    /// </summary>
    Preserved3,
    /// <summary>
    /// 预留动作4
    /// </summary>
    Preserved4,
    /// <summary>
    /// 预留动作5
    /// </summary>
    Preserved5,
    /// <summary>
    /// 预留动作6
    /// </summary>
    Preserved6
}