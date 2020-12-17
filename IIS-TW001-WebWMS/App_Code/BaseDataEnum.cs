using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///BaseDataEnum 的摘要说明
/// </summary>
public class BaseDataEnum
{
	public BaseDataEnum()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    
}

/// <summary>
/// 上架指引的规则， 0：数量 1：重量 2：体积
/// </summary>
public enum INAssistType
{
    QTY = 0,
    WEIGHT = 1,
    VOLUME = 2,
}

public static class BaseStatus
{
    //未处理状态：0 
    public static string UnDone = "0";
}


