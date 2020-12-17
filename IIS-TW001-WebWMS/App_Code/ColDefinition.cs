using System;

namespace ClassBuilder
{
	/// <summary>
	/// 列的定义结构体
	/// </summary>
	public class ColDefinition
	{
		/// <summary>
		/// 初始化各成员分量
		/// </summary>
		public void Initialize()
		{
			this.Name = "";
			this.DataType = "";
			this.DataTypeCatalog = 0;
			this.DefaultValue = "";
			this.IsAllowNull = true;
			this.Length = 0;
			this.Prec = 0;
			this.Scale = 0;			
			this.Other = "";
			this.Comment = "";
		}

		/// <summary>
		/// 列名称
		/// </summary>
		public string Name;
		/// <summary>
		/// 数据类型
		/// </summary>
		public string DataType;
		/// <summary>
		/// 长度
		/// </summary>
		public int Length;
		/// <summary>
		/// 精度
		/// </summary>
		public int Prec;
		/// <summary>
		/// 数字型字段的小数位数
		/// </summary>
		public int Scale;
		/// <summary>
		/// 0--无长短定义  1--有一个，如字符串的  2--- 有精度和小数位数
		/// </summary>
		public int DataTypeCatalog;
		/// <summary>
		/// 是否允许为空
		/// </summary>
		public bool IsAllowNull;
		/// <summary>
		/// 却省值
		/// </summary>
		public string DefaultValue;
		/// <summary>
		/// 注释
		/// </summary>
		public string Comment;
		/// <summary>
		/// 其他
		/// </summary>
		public string Other;
		
		/// <summary>
		/// 数据类型的全称
		/// </summary>
		public string FullNameOfType
		{
			get
			{
				string fullName = this.DataType;
				if(this.DataTypeCatalog == 1)
				{
					if(this.Prec != 0)
						fullName += "(" + this.Prec.ToString() + ")";
					else
						fullName += "(" + this.Length.ToString() + ")";
				}
				else if(this.DataTypeCatalog == 2)
				{
					if(this.Prec == 0 && this.Scale == 0)
					{
						fullName += "(" + this.Length.ToString() + ")";
						this.DataTypeCatalog = 1;
					}
					else
					{
						fullName += "(" + this.Prec + "," + this.Scale + ")";
					}
				}
				return fullName;
			}
		}		
	}
}
 