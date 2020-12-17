using mshtml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using ThoughtWorks.QRCode.Codec;

public static class StringExtension
{
    /// <summary>
    /// 缺省的可以表示数字0的字符串
    /// </summary>
    public static string ZeroString = "0";
    public static string DefaultDateFormat = "yyyy-MM-dd";
    public static string DefaultTimeFormat = "HH:mm:ss";
    /// <summary>
    /// 获取一个html元素的InnerText
    /// </summary>
    /// <param name="htmlStr">HTML字符串</param>
    /// <returns></returns>
    public static string GetInnerText(this string htmlStr)
    {
        HTMLDocumentClass hTMLDocumentClass = new HTMLDocumentClass();
        IHTMLDocument2 iHTMLDocument = hTMLDocumentClass;
        iHTMLDocument.designMode = "On";
        iHTMLDocument.write(new object[]
			{
				htmlStr
			});
        iHTMLDocument.close();
        hTMLDocumentClass.close();
        string innerText = iHTMLDocument.body.innerText;
        if (true && 255 == 0)
        {
        }
        return innerText;
    }

    /// <summary>
    /// 获取第一个英文单词
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="startIndex">从何处开始搜索</param>
    /// <returns></returns>
    public static string GetFirstWord(this string str, int startIndex)
    {
        Regex regex = new Regex("[a-zA-Z]{1,}", RegexOptions.Multiline);
        Match match = regex.Match(str, startIndex);
        if (!match.Success)
        {
            if (255 != 0)
            {
            }
            return "";
        }
        return match.Value;
    }

    /// <summary>
    /// 得到字符串中的第一个英文单词
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns></returns>
    public static string GetFirstWord(this string str)
    {
        return "";// str.GetFirstWord(0);
    }

    /// <summary>
    /// 在输入的字符串尾部添加给定数量的空格字符串
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="count">空格数量</param>
    /// <returns></returns>
    public static string Space(this string str, int count)
    {
        return str + "".PadLeft(count, ' ');
    }

  
    /// 判断是否是数字类型
    public static bool IsValidNum(string str)
    {
        if (!Regex.IsMatch(str.Replace(".", ""), "^[0-9]+$"))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 将字符串的第一个单词的首字母变为小写(如果字符串只有两个字符且都为大写，则两个字符都变为小写)
    /// </summary>
    /// <param name="inputValue">输入的字符串</param>
    /// <returns></returns>
    public static string FirstToLower(this string inputValue)
    {
        if (inputValue.Length == 2 && inputValue == inputValue.ToUpper())
        {
            return inputValue.ToLower();
        }
        string text = inputValue.Substring(0, 1);
        if (inputValue.Length < 2)
        {
            return text.ToLower();
        }
        return text.ToLower() + inputValue.Substring(1, inputValue.Length - 1);
    }

    /// <summary>
    ///             从字符串第i位开始取n位字符(左--&gt;右)
    /// </summary>
    /// <param name="i">开始取字符串的位置</param>
    /// <param name="n">要取的长度</param> 
    /// <param name="str">被取的字符串</param>
    /// <returns>返回截取后的字符串</returns>
    ///             <remarks>ADD By Cl</remarks>
    public static string Left(this string str, int i, int n)
    {
        if (i < str.Length)
        {
            while (n < str.Length)
            {
                if (i + n < str.Length)
                {
                    return str.Substring(i, n);
                }
                if (2 != 0)
                {
                    break;
                }
            }
        }
        return "";
    }

    /// <summary>
    /// 从字符串第i位开始取n位字符(右--&gt;左)
    /// </summary>
    /// <param name="i">开始取字符串的位置</param>
    /// <param name="n">要取的长度</param> 
    /// <param name="str">被取的字符串</param>
    /// <returns>返回截取后的字符串</returns>
    ///             <remarks>ADD By Cl</remarks>
    public static string Right(this string str, int i, int n)
    {
        if (i < str.Length)
        {
            if (n >= str.Length)
            {
                if (8 != 0)
                {
                }
            }
            else if (i - n >= -1)
            {
                string result = str.Substring(i - n + 1, n);
                bool flag = (uint)i + (uint)i > 4294967295u;
                if (!flag)
                {
                    return result;
                }
            }
        }
        return "";
    }

    /// <summary>
    /// 反转一个字符串
    /// </summary>
    /// <param name="str">要反转的字符串</param>
    /// <returns>返回反转后的字符串</returns>
    /// <remarks>ADD By Cl</remarks>
    //public static string Reverse(this string str)
    //{
    //    string text = "";
    //    while (8 == 0 || (true && str.Length <= 1))
    //    {
    //        text = str;
    //        int i;
    //        bool flag = (uint)i - (uint)i < 0u;
    //        if (!flag)
    //        {
    //            return text.Trim();
    //        }
    //    }
    //    for (int i = str.Length - 1; i >= 0; i--)
    //    {
    //        text += str.Substring(i, 1);
    //    }
    //    return text.Trim();
    //}

    /// <summary>
    /// 查找在一个字符串中子字符串出现的次数
    /// </summary>
    /// <param name="str">执行查找的字符串</param>
    /// <param name="subStr">子字符串</param>
    /// <returns>子字符串出现的次数</returns>
    public static int RepeatTime(this string str, string subStr)
    {
        int num = 0;
        int num2 = 0;
        int i = -1;
        if ((uint)i >= 0u)
        {
            i = str.IndexOf(subStr, num2, str.Length);
        }
        while (i >= 0)
        {
            num++;
            num2 = i + subStr.Length;
            int count = str.Length - num2;
            i = str.IndexOf(subStr, num2, count);
        }
        return num;
    }
    public static int GetLengthByByte(this string inputValue)
    {
        Regex regex;
        if (string.IsNullOrEmpty(inputValue))
        {
            return 0;
        }
    Label_000A:
        regex = new Regex(@"[\u4e00-\u9fa5]", RegexOptions.Multiline);
        MatchCollection matchs = regex.Matches(inputValue);
        if (matchs != null)
        {
            if (4 != 0)
            {
                return (inputValue.Length + matchs.Count);
            }
            goto Label_000A;
        }
        return inputValue.Length;
    }
    /// <summary>
    /// 拆分字符串，返回字符串数组
    /// </summary>
    /// <param name="str">要拆分的字符串</param>
    /// <param name="separator">拆分的分隔字符</param>
    /// <returns>字符串拆分后生成的字符串数组</returns>
    //public static string[] Split(string str, char separator)
    //{
    //    if (str.IsNullOrEmpty())
    //    {
    //        return new string[0];
    //    }
    //    return str.Split(new char[]
    //    {
    //        separator
    //    });
    //}

    /// <summary>
    /// 根据逗号分隔字符串并且转成字符串list
    /// </summary>
    /// <param name="inputValue">以逗号分隔开的字符串</param>
    /// <returns></returns>
    public static List<string> ToList(this string inputValue)
    {
        if (inputValue != null)
        {
            string[] collection = inputValue.Split(new char[]
				{
					','
				});
            List<string> list = new List<string>();
            list.AddRange(collection);
            return list;
        }
        return new List<string>();
    }

    /// <summary>
    /// 将输入的字符串重复累加一定的次数后返回
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="time">重复次数</param>
    /// <returns>重复后累加的字符串</returns>
    public static string Repeat(this string str, int time)
    {
        StringBuilder stringBuilder = new StringBuilder();
        while (true)
        {
        IL_06:
            int i = 0;
            while (i < time)
            {
                stringBuilder.Append(str);
                i++;
                if (4 == 0)
                {
                    goto IL_06;
                }
            }
            break;
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// 根据字符串中的回车换行符将字符串分拆为数组
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns></returns>
    public static string[] ToArray(string str)
    {
        ArrayList arrayList = new ArrayList();
        StringBuilder stringBuilder;
        while (true)
        {
            char[] array = str.ToCharArray();
            stringBuilder = new StringBuilder();
            int num = 0;
            while (true)
            {
                if (num >= array.Length)
                {
                    if (false)
                    {
                        goto Block_3;
                    }
                    if (!(stringBuilder.ToString().Trim() != ""))
                    {
                        goto IL_A5;
                    }
                    goto IL_0B;
                }
                else
                {
                    if (array[num] != '\r')
                    {
                        goto IL_A8;
                    }
                    if (((uint)num & 0u) != 0u)
                    {
                        goto IL_D2;
                    }
                    bool flag = ((uint)num & 0u) == 0u;
                    if (!flag)
                    {
                        break;
                    }
                    if ((uint)num - (uint)num > 4294967295u)
                    {
                        continue;
                    }
                    if (false)
                    {
                        goto IL_A5;
                    }
                    if (array[num + 1] == '\n')
                    {
                        num++;
                    }
                    arrayList.Add(stringBuilder.ToString());
                    stringBuilder.Remove(0, stringBuilder.Length);
                }
            IL_44:
                num++;
                continue;
            IL_A8:
                stringBuilder.Append(array[num]);
                goto IL_44;
            IL_A5:
                if (false)
                {
                    goto IL_A8;
                }
                goto IL_D2;
            }
        }
    IL_0B:
        arrayList.Add(stringBuilder.ToString());
        goto IL_E8;
    Block_3:
        goto IL_0B;
    IL_D2:
    IL_E8:
        return (string[])arrayList.ToArray(typeof(string));
    }

    /// <summary>
    /// 判断输入的字符是否是空白字符
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns></returns>
    public static bool IsSpaceChar(char c)
    {
        if (c != ' ')
        {
            if (false)
            {
                if (3 != 0)
                {
                    goto IL_20;
                }
                if (-1 == 0)
                {
                    goto IL_19;
                }
            }
            else
            {
                if (c != '\f')
                {
                    goto IL_4C;
                }
                return true;
            }
        IL_14:
            if (c == '\t')
            {
                if (!false)
                {
                    return true;
                }
                goto IL_27;
            }
        IL_19:
            if (c == '\v')
            {
                return true;
            }
            if (!false)
            {
                return false;
            }
            goto IL_4C;
        IL_20:
            if (c != '\r')
            {
                goto IL_14;
            }
            return true;
        IL_27:
            if (!false)
            {
                return true;
            }
            goto IL_20;
        IL_4C:
            if (c == '\n')
            {
                goto IL_27;
            }
            goto IL_20;
        }
        return true;
    }

    /// <summary>
    /// 按字节数截取字符串(一个汉字算两个字节）
    /// </summary>
    /// <param name="inputValue">字符串</param>
    /// <param name="len">长度</param>
    /// <returns></returns>
    public static string SubstringByByte(this string inputValue, int len)
    {
        string text;
        return "";//inputValue.SubstringByByte(len, out text);
    }

    /// <summary>
    /// 按字节数截取字符串(一个汉字算两个字节）
    /// </summary>
    /// <param name="inputValue">字符串</param>
    /// <param name="len">长度</param>
    /// <param name="trailString">剩余的尾部字符串</param>
    /// <returns></returns>
    //public static string SubstringByByte(this string inputValue, int len, out string trailString)
    //{
    //    if (!inputValue.IsNullOrEmpty())
    //    {
    //        int i;
    //        while (true)
    //        {
    //        IL_24B:
    //            if (false)
    //            {
    //                goto IL_15B;
    //            }
    //            if (len == 0)
    //            {
    //                goto IL_254;
    //            }
    //            goto IL_1FA;
    //        IL_172:
    //            int num;
    //            bool flag;
    //            if (num == len)
    //            {
    //                if ((uint)len - (uint)i <= 4294967295u)
    //                {
    //                    if (i < inputValue.Length - 1)
    //                    {
    //                        goto Block_14;
    //                    }
    //                    trailString = "";
    //                    flag = ((uint)num - (uint)len < 0u);
    //                    if (flag)
    //                    {
    //                        goto IL_1FA;
    //                    }
    //                    goto IL_234;
    //                }
    //                else
    //                {
    //                    flag = ((uint)len > 4294967295u);
    //                    if (flag)
    //                    {
    //                        goto IL_1DE;
    //                    }
    //                    goto IL_15D;
    //                }
    //            }
    //            else
    //            {
    //                if (num < len + 1)
    //                {
    //                    i++;
    //                    goto IL_35;
    //                }
    //                while (i <= 0)
    //                {
    //                    if ((uint)num - (uint)len > 4294967295u)
    //                    {
    //                        return inputValue;
    //                    }
    //                    if ((uint)i - (uint)len < 0u)
    //                    {
    //                        goto IL_F8;
    //                    }
    //                    if ((uint)num >= 0u)
    //                    {
    //                        trailString = inputValue;
    //                        if ((uint)len < 0u)
    //                        {
    //                            goto IL_1DE;
    //                        }
    //                        flag = ((uint)num > 4294967295u);
    //                        if (flag)
    //                        {
    //                            goto IL_24B;
    //                        }
    //                        goto IL_25D;
    //                    }
    //                }
    //                goto IL_ED;
    //            }
    //        IL_21A:
    //            flag = ((uint)num < 0u);
    //            if (flag)
    //            {
    //                goto IL_22F;
    //            }
    //            goto IL_172;
    //        IL_1DE:
    //            num += 2;
    //            flag = ((uint)i - (uint)len > 4294967295u);
    //            if (flag)
    //            {
    //                goto IL_1FA;
    //            }
    //            goto IL_21A;
    //        IL_16E:
    //            num++;
    //            goto IL_172;
    //        IL_15B:
    //            goto IL_16E;
    //        IL_35:
    //            if (i >= inputValue.Length)
    //            {
    //                flag = ((uint)num < 0u);
    //                if (!flag)
    //                {
    //                    trailString = "";
    //                    flag = (((uint)i | 1u) == 0u);
    //                    if (flag)
    //                    {
    //                        break;
    //                    }
    //                }
    //                flag = ((uint)len - (uint)i > 4294967295u);
    //                if (flag)
    //                {
    //                    continue;
    //                }
    //                return inputValue;
    //            }
    //            else
    //            {
    //                if (inputValue[i] >= '一')
    //                {
    //                    goto IL_15D;
    //                }
    //                flag = ((uint)i < 0u);
    //                if (flag)
    //                {
    //                    goto IL_1C0;
    //                }
    //                goto IL_15B;
    //            }
    //        IL_1FA:
    //            num = 0;
    //            if ((uint)i + (uint)len > 4294967295u)
    //            {
    //                goto IL_115;
    //            }
    //            if (false)
    //            {
    //                goto IL_21A;
    //            }
    //            i = 0;
    //            goto IL_35;
    //        IL_15D:
    //            if (inputValue[i] > '龥')
    //            {
    //                goto IL_16E;
    //            }
    //            goto IL_1DE;
    //        }
    //    IL_43:
    //        return "";
    //    IL_ED:
    //        if (i <= inputValue.Length - 1)
    //        {
    //            trailString = inputValue.Substring(i);
    //            goto IL_FF;
    //        }
    //    IL_F8:
    //        trailString = "";
    //    IL_FF:
    //        return inputValue.Substring(0, i);
    //    IL_108:
    //        return inputValue.Substring(0, i + 1);
    //    IL_115:
    //        goto IL_ED;
    //    Block_14:
    //    IL_1C0:
    //    IL_22F:
    //        trailString = inputValue.Substring(i + 1);
    //    IL_234:
    //        goto IL_108;
    //    IL_25D:
    //        goto IL_43;
    //    }
    //IL_254:
    //    trailString = inputValue;
    //    return "";
    //}

    ///// <summary>
    ///// 按字节数计算长度(一个汉字算两个字节）
    ///// </summary>
    ///// <param name="inputValue">字符串</param>
    ///// <returns></returns>
    //public static int GetLengthByByte(this string inputValue)
    //{
    //    if (inputValue.IsNullOrEmpty())
    //    {
    //        return 0;
    //    }
    //    MatchCollection matchCollection;
    //    do
    //    {
    //        Regex regex = new Regex("[\\u4e00-\\u9fa5]", RegexOptions.Multiline);
    //        matchCollection = regex.Matches(inputValue);
    //        if (matchCollection == null)
    //        {
    //            goto IL_36;
    //        }
    //    }
    //    while (4 == 0);
    //    return inputValue.Length + matchCollection.Count;
    //IL_36:
    //    return inputValue.Length;
    //}

    /// <summary>
    /// 将char数组转换为String类型
    /// </summary>
    /// <param name="inputValue">字符数组</param>
    /// <returns></returns>
    public static string ToString(char[] inputValue)
    {
        string result;
        try
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(inputValue);
            result = stringBuilder.ToString();
        }
        catch
        {
            result = "";
        }
        return result;
    }

    ///// <summary>
    ///// 将字符串用MD5算法加密
    ///// </summary>
    ///// <param name="unEncryptString">代加密的字符串</param>
    ///// <returns>加密后的字符串</returns>
    //public static string MD5Encrypt(this string unEncryptString)
    //{
    //    return FormsAuthentication.HashPasswordForStoringInConfigFile(unEncryptString, "md5");
    //}

    ///// <summary>
    ///// 将字符串用SHA1算法加密
    ///// </summary>
    ///// <param name="unEncryptString">代加密的地字符串</param>
    ///// <returns>加密后的字符串</returns>
    //public static string SHA1Encrypt(this string unEncryptString)
    //{
    //    return FormsAuthentication.HashPasswordForStoringInConfigFile(unEncryptString, "sha1");
    //}

    /// <summary>
    ///  生成二维码图片
    /// </summary>
    /// <param name="content">需要生成的字符串</param>
    public static Image GenerateQRCodeImage(string content)
    {
        return new QRCodeEncoder
        {
            QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
            QRCodeScale = 2,
            QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L,
            QRCodeVersion = 3
        }.Encode(content);
    }

    /// <summary>
    /// 加密/或者不加密保存配置文件
    /// 主要是更改connectionStrings 这一节
    /// </summary>
    /// <param name="file">配置文件全路径，可以是web.config,也可以是app.config</param>
    /// <param name="encrypt">加密还是不加密</param>
    public static void EncryptConfiguration(string file, bool encrypt)
    {
        //    string text = "";
        //    if (false)
        //    {
        //        goto IL_19F;
        //    }
        //    goto IL_243;
        //IL_5D:
        //    while (!file.EndsWith(".exe.config", StringComparison.OrdinalIgnoreCase))
        //    {
        //        File.Delete(file);
        //        File.Copy(text + ".config", file);
        //        if (((encrypt ? 1u : 0u) & 0u) == 0u)
        //        {
        //            goto IL_14;
        //        }
        //        if ((encrypt ? 1u : 0u) - (encrypt ? 1u : 0u) > 4294967295u)
        //        {
        //            goto IL_15D;
        //        }
        //    }
        //    goto IL_F7;
        //IL_19F:
        //    while (-1 != 0)
        //    {
        //        if ((encrypt ? 1u : 0u) - (encrypt ? 1u : 0u) <= 4294967295u)
        //        {
        //            goto IL_13E;
        //        }
        //    }
        //    goto IL_177;
        //IL_14:
        //    if ((encrypt ? 1u : 0u) <= 4294967295u)
        //    {
        //        return;
        //    }
        //    goto IL_5D;
        //IL_6F:
        //    ConnectionStringsSection connectionStringsSection;
        //    if (connectionStringsSection.ElementInformation.IsLocked)
        //    {
        //        goto IL_5D;
        //    }
        //    if (connectionStringsSection.SectionInformation.IsLocked)
        //    {
        //        goto IL_5D;
        //    }
        //    if ((encrypt ? 1u : 0u) + (encrypt ? 1u : 0u) <= 4294967295u)
        //    {
        //        goto IL_177;
        //    }
        //    goto IL_15D;
        //IL_E4:
        //    connectionStringsSection.SectionInformation.ForceSave = true;
        //    Configuration configuration;
        //    if (255 != 0)
        //    {
        //        configuration.Save();
        //        if (((encrypt ? 1u : 0u) | 1u) == 0u)
        //        {
        //            goto IL_106;
        //        }
        //        if (-2147483648 == 0)
        //        {
        //            goto IL_6F;
        //        }
        //        if (!false)
        //        {
        //            goto IL_5D;
        //        }
        //        goto IL_6F;
        //    }
        //IL_F7:
        //    if (false || (encrypt ? 1u : 0u) < 0u)
        //    {
        //        goto IL_14;
        //    }
        //    if (!false)
        //    {
        //        return;
        //    }
        //    bool flag = (encrypt ? 1u : 0u) - (encrypt ? 1u : 0u) > 4294967295u;
        //    if (flag)
        //    {
        //        goto IL_1C7;
        //    }
        //    goto IL_243;
        //IL_106:
        //    goto IL_E4;
        //IL_13E:
        //    if (encrypt)
        //    {
        //        flag = (((encrypt ? 1u : 0u) & 0u) == 0u);
        //        if (flag)
        //        {
        //            if ((encrypt ? 1u : 0u) >= 0u)
        //            {
        //                goto IL_E4;
        //            }
        //        }
        //        else
        //        {
        //            flag = (((encrypt ? 1u : 0u) & 0u) == 0u);
        //            if (flag)
        //            {
        //                goto IL_190;
        //            }
        //            goto IL_177;
        //        }
        //    }
        //    if (!connectionStringsSection.SectionInformation.IsProtected)
        //    {
        //        goto IL_106;
        //    }
        //IL_15D:
        //    connectionStringsSection.SectionInformation.UnprotectSection();
        //    goto IL_E4;
        //IL_177:
        //    if (!encrypt)
        //    {
        //        goto IL_13E;
        //    }
        //IL_190:
        //    if (connectionStringsSection.SectionInformation.IsProtected)
        //    {
        //        goto IL_13E;
        //    }
        //IL_1C7:
        //    connectionStringsSection.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
        //    if (!false)
        //    {
        //        if (false)
        //        {
        //            return;
        //        }
        //        goto IL_19F;
        //    }
        //IL_243:
        //    if (file.EndsWith(".exe.config", StringComparison.OrdinalIgnoreCase))
        //    {
        //        text = file.Substring(0, file.Length - 7);
        //    }
        //    else
        //    {
        //        text = Path.GetTempFileName() + ".exe";
        //        File.Create(text);
        //        File.Copy(file, text + ".config");
        //        flag = ((encrypt ? 1u : 0u) > 4294967295u);
        //        if (flag)
        //        {
        //            goto IL_1C7;
        //        }
        //        File.SetAttributes(text + ".config", FileAttributes.Normal);
        //    }
        //    configuration = ConfigurationManager.OpenExeConfiguration(text);
        //    connectionStringsSection = (configuration.GetSection("connectionStrings") as ConnectionStringsSection);
        //    goto IL_6F;
    }

    /// <summary>
    /// 加密文件
    /// </summary>
    /// <param name="inName">输入文件名</param>
    /// <param name="outName">输出文件名</param>
    /// <param name="desKey">用于对称算法的密钥</param>
    /// <param name="desIV">用于对称算法的初始化向量</param>
    public static void EncryptFile(string inName, string outName, byte[] desKey, byte[] desIV)
    {
        //    FileStream fileStream = new FileStream(inName, FileMode.Open, FileAccess.Read);
        //    FileStream fileStream2;
        //    CryptoStream cryptoStream;
        //    while (true)
        //    {
        //        fileStream2 = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
        //        fileStream2.SetLength(0L);
        //        byte[] buffer = new byte[100];
        //        long num = 0L;
        //        while (true)
        //        {
        //            long length = fileStream.Length;
        //            DES dES = new DESCryptoServiceProvider();
        //            cryptoStream = new CryptoStream(fileStream2, dES.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);
        //            int num2;
        //            bool flag = (uint)num2 < 0u;
        //            if (flag)
        //            {
        //                break;
        //            }
        //            while (num < length)
        //            {
        //                num2 = fileStream.Read(buffer, 0, 100);
        //                do
        //                {
        //                    cryptoStream.Write(buffer, 0, num2);
        //                }
        //                while ((uint)num2 < 0u);
        //                num += (long)num2;
        //            }
        //            if ((uint)num2 <= 4294967295u)
        //            {
        //                goto Block_1;
        //            }
        //        }
        //    }
        //Block_1:
        //    cryptoStream.Close();
        //    fileStream2.Close();
        //    fileStream.Close();
    }

    /// <summary>
    /// 解密文件
    /// </summary>
    /// <param name="inName">输入文件名</param>
    /// <param name="outName">输出文件名</param>
    /// <param name="desKey">用于对称算法的密钥</param>
    /// <param name="desIV">用于对称算法的初始化向量</param>
    public static void DecryptFile(string inName, string outName, byte[] desKey, byte[] desIV)
    {
        //FileStream fileStream = new FileStream(inName, FileMode.Open, FileAccess.Read);
        //long num;
        //if (((uint)num | 4294967295u) != 0u)
        //{
        //}
        //FileStream fileStream2 = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
        //fileStream2.SetLength(0L);
        //byte[] buffer = new byte[100];
        //num = 0L;
        //long length = fileStream.Length;
        //DES dES = new DESCryptoServiceProvider();
        //CryptoStream cryptoStream;
        //int num2;
        //do
        //{
        //    cryptoStream = new CryptoStream(fileStream2, dES.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);
        //}
        //while ((uint)num2 - (uint)num < 0u);
        //while (num < length)
        //{
        //    num2 = fileStream.Read(buffer, 0, 100);
        //    cryptoStream.Write(buffer, 0, num2);
        //    num += (long)num2;
        //}
        //if (!false)
        //{
        //    cryptoStream.Close();
        //    fileStream2.Close();
        //    fileStream.Close();
        //}
    }

    private static string xa7c1fc03f3d90ae1(this string x713cf150ca65b36c)
    {
        byte[] desKey = new byte[]
			{
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8
			};
        byte[] desIV = new byte[]
			{
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8
			};
        string result;
        if (2147483647 != 0)
        {
            string tempFileName = Path.GetTempFileName();
            FileStream fileStream;
            do
            {
                StringExtension.EncryptFile(x713cf150ca65b36c, tempFileName, desKey, desIV);
                fileStream = new FileStream("E", FileMode.Open, FileAccess.Read);
            }
            while (4 == 0);
            UTF8Encoding encoding = new UTF8Encoding();
            StreamReader streamReader = new StreamReader(fileStream, encoding);
            result = streamReader.ReadToEnd();
            fileStream.Close();
        }
        return result;
    }

    /// <summary>
    /// 加密。加密串可以通过调用Decrypt得到原始串
    /// </summary>
    /// <param name="strin">输入文件名</param>
    /// <returns>加密后的文件内容</returns>
    public static string Encrypt(this string strin)
    {
        return "";// strin.xa7c1fc03f3d90ae1();
    }

    private static string xa9f980f4dcadd1ab(this string x713cf150ca65b36c)
    {
        byte[] desKey = new byte[]
			{
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8
			};
        string result;
        do
        {
            byte[] desIV = new byte[]
				{
					1,
					2,
					3,
					4,
					5,
					6,
					7,
					8
				};
            string tempFileName = Path.GetTempFileName();
            StringExtension.DecryptFile(x713cf150ca65b36c, tempFileName, desKey, desIV);
            FileStream fileStream = new FileStream("D", FileMode.Open, FileAccess.Read);
            UTF8Encoding encoding = new UTF8Encoding();
            StreamReader streamReader = new StreamReader(fileStream, encoding);
            result = streamReader.ReadToEnd();
            if (255 != 0)
            {
            }
            fileStream.Close();
        }
        while (false);
        return result;
    }

    /// <summary>
    /// 解密。是Encrypt的反向操作
    /// </summary>
    /// <param name="strin">加过密的文件的名称</param>
    /// <returns>原始文件的内容</returns>
    public static string Decrypt(this string strin)
    {
        return "";// strin.xa9f980f4dcadd1ab();
    }

    //private static string x71c8a890dd8b5ab0(this string xfcad4c0a9c5890c6, string xba08ce632055a1d9)
    //{
    ////    if (xfcad4c0a9c5890c6.IsNullOrEmpty())
    ////    {
    ////        if (!false)
    ////        {
    ////            return "";
    ////        }
    ////    }
    ////    else
    ////    {
    ////        if (xba08ce632055a1d9.IsNullOrEmpty())
    ////        {
    ////            xba08ce632055a1d9 = "secretpassword1!";
    ////            goto IL_67;
    ////        }
    ////        goto IL_67;
    ////    }
    ////IL_0E:
    ////    byte[] bytes = Encoding.Unicode.GetBytes(xfcad4c0a9c5890c6);
    ////    TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider;
    ////    string result = Convert.ToBase64String(tripleDESCryptoServiceProvider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
    ////    tripleDESCryptoServiceProvider = null;
    ////    if (!false)
    ////    {
    ////        return result;
    ////    }
    ////IL_67:
    ////    MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
    ////    if (3 != 0)
    ////    {
    ////        byte[] key = mD5CryptoServiceProvider.ComputeHash(Encoding.Unicode.GetBytes(xba08ce632055a1d9));
    ////        if (!false)
    ////        {
    ////            tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
    ////            tripleDESCryptoServiceProvider.Key = key;
    ////            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
    ////        }
    ////    }
    ////    goto IL_0E;
    //}

    /// <summary>
    /// 加密，加密串可以通过调用DecryptDES3得到原始串
    /// </summary>
    /// <param name="original">加密字符串</param>
    /// <param name="key">加密因子</param>
    /// <returns></returns>
    public static string EncryptDES3(string original, string ReturnKey)
    {
        TripleDESCryptoServiceProvider provider;
        MD5CryptoServiceProvider provider2;
        byte[] buffer2;
        if (!string.IsNullOrEmpty(original)) 
        {
            if (string.IsNullOrEmpty(ReturnKey))
            {
                ReturnKey = "secretpassword1!";
            }
            goto Label_0067;
        }
        if (0 == 0)
        {
            return "";
        }
    Label_000E:
        buffer2 = Encoding.Unicode.GetBytes(original);
        string str = Convert.ToBase64String(provider.CreateEncryptor().TransformFinalBlock(buffer2, 0, buffer2.Length));
        provider = null;
        if (0 == 0)
        {
            return str;
        }
    Label_0067:
        provider2 = new MD5CryptoServiceProvider();
        if (3 != 0)
        {
            byte[] buffer = provider2.ComputeHash(Encoding.Unicode.GetBytes(ReturnKey));
            if (0 == 0)
            {
                provider2 = null;
                provider = new TripleDESCryptoServiceProvider
                {
                    Key = buffer,
                    Mode = CipherMode.ECB
                };
            }
        }
        goto Label_000E;
    }

    private static string x20bc1eeaaf6a7f78(this string xfcad4c0a9c5890c6, string xba08ce632055a1d9)
    {
        if (xfcad4c0a9c5890c6 == null || xfcad4c0a9c5890c6 == "")
        {
            return "";
        }
        string @string;
        if (xba08ce632055a1d9 == null)
        {
            if (-2147483648 == 0)
            {
                return @string;
            }
            goto IL_7D;
        }
        else
        {
            if (!(xba08ce632055a1d9 == ""))
            {
                goto IL_62;
            }
            goto IL_86;
        }
    IL_0B:
        TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider;
        tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
        byte[] array = Convert.FromBase64String(xfcad4c0a9c5890c6);
        @string = Encoding.Unicode.GetString(tripleDESCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array, 0, array.Length));
        tripleDESCryptoServiceProvider = null;
        return @string;
    IL_53:
    IL_62:
        MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
        byte[] key = mD5CryptoServiceProvider.ComputeHash(Encoding.Unicode.GetBytes(xba08ce632055a1d9));
        if (!false)
        {
            if (true)
            {
                tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            }
            tripleDESCryptoServiceProvider.Key = key;
            goto IL_0B;
        }
    IL_7D:
        if (false)
        {
            goto IL_0B;
        }
        if (false)
        {
            goto IL_53;
        }
    IL_86:
        xba08ce632055a1d9 = "secretpassword1!";
        goto IL_53;
    }

    /// <summary>
    /// 解密,是EncryptDES3的反向操作。
    /// </summary>
    /// <param name="original">解密字符串</param>
    /// <param name="key">解密因子</param>
    /// <returns></returns>
    public static string DecryptDES3(this string original, string key)
    {
        return "";// original.x20bc1eeaaf6a7f78(key);
    }

    /// <summary>
    /// 公钥加密
    /// </summary>
    /// <param name="input">明文</param>
    /// <param name="certFile">证书文件</param>
    /// <param name="key">加密Key</param>
    /// <param name="encryptedKey">被加过密的Key</param>
    /// <returns>密文</returns>
    //public static byte[] EncryptMessage(this string input, string certFile, string key, out byte[] encryptedKey)
    //{
    //    //encryptedKey = key.RSAEncrypt(certFile);
    //    return "";// input.x1654f45ad09d5bba(key);
    //}

    /// <summary>
    /// 解密长的消息
    /// </summary>
    /// <param name="encryptedData">密文</param>
    /// <param name="certFile">证书文件</param>
    /// <param name="encryptedKey">被加过密的Key</param>
    /// <param name="certFilePassword">证书文件的密码</param>
    /// <returns>明文</returns>
    public static string DecryptMessage(this byte[] encryptedData, string certFile, byte[] encryptedKey, string certFilePassword)
    {
        //string password = encryptedKey.RSADecrypt(certFile, certFilePassword);
        return "";//StringExtension.DecryptRIJ(encryptedData, password);
    }

    /// <summary>
    /// 将消息解秘
    /// </summary>
    /// <param name="encryptedData">加密的二进制流</param>
    /// <param name="password">秘钥</param>
    /// <returns>解秘的消息内容字符串</returns>
    public static string DecryptRIJ(byte[] encryptedData, string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            password = "secretpassword1!";
        }
        string @string;
        while (true)
        {
            SHA256Managed sHA256Managed = new SHA256Managed();
            byte[] key = sHA256Managed.ComputeHash(Encoding.Unicode.GetBytes(password));
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            if (false)
            {
                goto IL_59;
            }
            rijndaelManaged.Key = key;
            if (!false)
            {
                rijndaelManaged.Mode = CipherMode.ECB;
                @string = Encoding.Unicode.GetString(rijndaelManaged.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length));
                goto IL_59;
            }
        IL_5C:
            if (2 == 0)
            {
                continue;
            }
            break;
        IL_0A:
            goto IL_5C;
        IL_59:
            if (false)
            {
                goto IL_5C;
            }
            goto IL_0A;
        }
        return @string;
    }

    /// <summary>
    /// 使用公钥证书进行加密(不宜用于加密超长明文）
    /// 请使用RSADecrpyt解密本方法产生的密文
    /// </summary>
    /// <param name="input">明文（不能超过40个汉字）</param>
    /// <param name="certFile">公钥证书</param>
    /// <returns>密文</returns>
    //public static byte[] RSAEncrypt(this string input, string certFile)
    //{
    //    X509Certificate2 x509Certificate = new X509Certificate2(certFile);
    //    while (false || !(x509Certificate.PublicKey.Key is RSACryptoServiceProvider))
    //    {
    //        AsymmetricAlgorithm arg_3D_0 = x509Certificate.PublicKey.Key;
    //        if (255 != 0)
    //        {
    //            return null;
    //        }
    //    }
    //    RSACryptoServiceProvider rSACryptoServiceProvider = x509Certificate.PublicKey.Key as RSACryptoServiceProvider;
    //    byte[] bytes = Encoding.UTF8.GetBytes(input);
    //    return rSACryptoServiceProvider.Encrypt(bytes, false);
    //}

    /// <summary>
    /// 使用私钥钥证书进行解密
    /// </summary>
    /// <param name="input">文件流</param>
    /// <param name="certFile">私钥钥证书</param>
    /// <param name="certFilePassword">私钥钥证书密码</param>
    /// <returns></returns>
    //public static string RSADecrypt(this byte[] input, string certFile, string certFilePassword)
    //{
    //    X509Certificate2 x509Certificate = new X509Certificate2(certFile, certFilePassword);
    //    if (!x509Certificate.HasPrivateKey)
    //    {
    //        if (!false)
    //        {
    //            return null;
    //        }
    //    }
    //    else if (x509Certificate.PrivateKey is RSACryptoServiceProvider)
    //    {
    //        RSACryptoServiceProvider rSACryptoServiceProvider = x509Certificate.PrivateKey as RSACryptoServiceProvider;
    //        byte[] bytes = rSACryptoServiceProvider.Decrypt(input, false);
    //        return Encoding.UTF8.GetString(bytes);
    //    }
    //    return null;
    //}

    /// <summary>
    /// 使用包含有私钥的文件进行签名
    /// </summary>
    /// <param name="input">明文</param>
    /// <param name="certFile">证书文件</param>
    /// <param name="certFilePassword">证书文件的密码</param>
    /// <returns>签名数据（注意签名不是加密，签名数据不能代替明文，具体详见http://baike.baidu.com/view/7626.htm）</returns>
    //public static byte[] RSASignData(this string input, string certFile, string certFilePassword)
    //{
    //    X509Certificate2 x509Certificate = new X509Certificate2(certFile, certFilePassword);
    //    if (-1 == 0)
    //    {
    //        if (4 == 0)
    //        {
    //            goto IL_46;
    //        }
    //    }
    //    else if (!x509Certificate.HasPrivateKey)
    //    {
    //        AsymmetricAlgorithm arg_24_0 = x509Certificate.PublicKey.Key;
    //        return null;
    //    }
    //    RSACryptoServiceProvider rSACryptoServiceProvider = x509Certificate.PrivateKey as RSACryptoServiceProvider;
    //    byte[] bytes = Encoding.UTF8.GetBytes(input);
    //IL_46:
    //    return rSACryptoServiceProvider.SignData(bytes, "SHA1");
    //}

    /// <summary>
    /// 使用公钥证书文件校验数据是否被篡改
    /// </summary>
    /// <param name="input">明文</param>
    /// <param name="certFile">证书文件</param>
    /// <param name="signData">签名数据</param>
    /// <returns>True--未被篡改，False--被篡改</returns>
    public static bool RSAVerifySign(this string input, string certFile, byte[] signData)
    {
        //X509Certificate2 x509Certificate = new X509Certificate2(certFile);
        //if (x509Certificate.PublicKey.Key is RSACryptoServiceProvider)
        //{
        //    RSACryptoServiceProvider rSACryptoServiceProvider = x509Certificate.PublicKey.Key as RSACryptoServiceProvider;
        //    byte[] bytes = Encoding.UTF8.GetBytes(input);
        //    return rSACryptoServiceProvider.VerifyData(bytes, "SHA1", signData);
        //}
        return false;
    }

    //private static byte[] x1654f45ad09d5bba(this string xfcad4c0a9c5890c6, string xe8e4b5871d71a79a)
    //{
    //    if (!xe8e4b5871d71a79a.IsNullOrEmpty())
    //    {
    //        goto IL_4B;
    //    }
    //    if (3 != 0)
    //    {
    //        xe8e4b5871d71a79a = "secretpassword1!";
    //        goto IL_4B;
    //    }
    //    RijndaelManaged rijndaelManaged;
    //    byte[] key;
    //    do
    //    {
    //    IL_0F:
    //        rijndaelManaged.Key = key;
    //        rijndaelManaged.Mode = CipherMode.ECB;
    //    }
    //    while (false);
    //    byte[] bytes = Encoding.Unicode.GetBytes(xfcad4c0a9c5890c6);
    //    byte[] result = rijndaelManaged.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
    //    rijndaelManaged = null;
    //    return result;
    //IL_4B:
    //    SHA256Managed sHA256Managed = new SHA256Managed();
    //    key = sHA256Managed.ComputeHash(Encoding.Unicode.GetBytes(xe8e4b5871d71a79a));
    //    rijndaelManaged = new RijndaelManaged();
    //    goto IL_0F;
    //}

    ///// <summary>
    ///// 加密字符串，托管代码。加密串可以通过调用DecryptRIJ得到原始串
    ///// </summary>
    ///// <param name="original">字符串</param>
    ///// <param name="password">加密因子</param>
    ///// <returns></returns>
    //public static byte[] EncryptRIJ(this string original, string password)
    //{
    //    return original.x1654f45ad09d5bba(password);
    //}

    //private static string x93f140cd3671854c(string xfcad4c0a9c5890c6, string xe8e4b5871d71a79a)
    //{
    //    if (xfcad4c0a9c5890c6.IsNullOrEmpty())
    //    {
    //        return "";
    //    }
    //    if (xe8e4b5871d71a79a.IsNullOrEmpty())
    //    {
    //        goto IL_73;
    //    }
    //IL_51:
    //    SHA256Managed sHA256Managed = new SHA256Managed();
    //    byte[] key = sHA256Managed.ComputeHash(Encoding.Unicode.GetBytes(xe8e4b5871d71a79a));
    //    RijndaelManaged rijndaelManaged = new RijndaelManaged();
    //    string @string;
    //    byte[] array;
    //    if (255 == 0)
    //    {
    //        if (-2 != 0)
    //        {
    //            goto IL_73;
    //        }
    //    }
    //    else
    //    {
    //        if (!false && false)
    //        {
    //            return @string;
    //        }
    //        rijndaelManaged.Key = key;
    //        rijndaelManaged.Mode = CipherMode.ECB;
    //        array = Convert.FromBase64String(xfcad4c0a9c5890c6);
    //    }
    //    @string = Encoding.Unicode.GetString(rijndaelManaged.CreateDecryptor().TransformFinalBlock(array, 0, array.Length));
    //    return @string;
    //IL_73:
    //    xe8e4b5871d71a79a = "secretpassword1!";
    //    goto IL_51;
    //}

    /// <summary>
    /// 解密字符串，托管代码。是EncryptRIJ的反向操作
    /// </summary>
    /// <param name="original">字符串</param>
    /// <param name="password">解密因子</param>
    /// <returns></returns>
    //public static string DecryptRIJ(string original, string password)
    //{
    //    return StringExtension.x93f140cd3671854c(original, password);
    //}

    /// <summary>
    /// 转换为Long型
    /// </summary>
    /// <param name="str">长整数格式的字符串</param>
    /// <returns></returns>
    public static long ToInt64(this string str)
    {
        if (str == StringExtension.ZeroString)
        {
            return 0L;
        }
        return Convert.ToInt64(str);
    }

    /// <summary>
    /// 转换为Long型
    /// </summary>
    /// <param name="str">长整数型格式的字符串</param>
    /// <returns></returns>
    //public static long ToLong(this string str)
    //{
    //    return str.ToInt64();
    //}

    /// <summary>
    /// 转换位整数
    /// </summary>
    /// <param name="str">整数格式的字符串</param>
    /// <returns></returns>
    //public static int ToInt(this string str)
    //{
    //    return str.ToInt32();
    //}

    /// <summary>
    /// 转换为其他类型的数据.
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="str">字符串</param>
    /// <returns></returns>
    //public static T To<T>(this string str)
    //{
    //    return (T)((object)str.To(typeof(T)));
    //}

    /// <summary>
    /// 转换位整数
    /// </summary>
    /// <param name="str">整数格式的字符串</param>
    /// <returns></returns>
    public static int ToInt32(this string str)
    {
        if (str == StringExtension.ZeroString)
        {
            return 0;
        }
        return Convert.ToInt32(str);
    }

    ///// <summary>
    ///// 转换为其他类型的数据
    ///// </summary>
    ///// <param name="str">字符串</param>
    ///// <param name="t">类型信息</param>
    ///// <returns></returns>
    public static object To(this string str, Type t)
    {
        if (t == typeof(bool))
        {
            return str.ToBoolean();
        }
        if (t == typeof(decimal))
        {
            return str.ToDecimal();
        }
        if (t != typeof(DateTime))
        {
            TypeConverter converter = TypeDescriptor.GetConverter(t);
            return converter.ConvertTo(str, t);
        }
        return str.ToDateTime();
    }

    ///// <summary>
    ///// 转换为短整型
    ///// </summary>
    ///// <param name="str">短整数型格式的字符串</param>
    ///// <returns></returns>
    //public static short ToShort(this string str)
    //{
    //    return str.ToInt16();
    //}

    /// <summary>
    /// 转换为短整型
    /// </summary>
    /// <param name="str">短整数格式的字符串</param>
    /// <returns></returns>
    public static short ToInt16(this string str)
    {
        if (str == StringExtension.ZeroString)
        {
            return 0;
        }
        return Convert.ToInt16(str);
    }

    /// <summary>
    /// 转换为字节
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns></returns>
    public static byte ToByte(this string str)
    {
        if (str == StringExtension.ZeroString)
        {
            return 0;
        }
        return Convert.ToByte(str);
    }

    /// <summary>
    /// 转换为十进制数字
    /// </summary>
    /// <param name="str">十进制数字格式的字符串</param>
    /// <returns></returns>
    public static decimal ToDecimal(this string str)
    {
        if (str == StringExtension.ZeroString)
        {
            return 0m;
        }
        return Convert.ToDecimal(str);
    }

    /// <summary>
    /// 转换为十进制数字，如果失败返回缺省值
    /// </summary>
    /// <param name="str">十进制数字格式的字符串</param>
    /// <param name="defaultValue">缺省值</param>
    /// <returns></returns>
    //public static decimal ToDecimal(this string str, decimal defaultValue)
    //{
    //    decimal result;
    //    try
    //    {
    //        result = str.ToDecimal();
    //    }
    //    catch
    //    {
    //        result = defaultValue;
    //    }
    //    return result;
    //}

    /// <summary>
    /// 转换为双精度浮点数
    /// </summary>
    /// <param name="str">双精度浮点数格式的字符串</param>
    /// <returns></returns>
    public static double ToDouble(this string str)
    {
        if (str == StringExtension.ZeroString)
        {
            return 0.0;
        }
        return Convert.ToDouble(str);
    }

    /// <summary>
    /// 转换为单精度浮点数
    /// </summary>
    /// <param name="str">单精度浮点数格式的字符串</param>
    /// <returns></returns>
    //public static float ToSingle(this string str)
    //{
    //    return str.ToFloat();
    //}

    /// <summary>
    /// 转换为单精度浮点数
    /// </summary>
    /// <param name="str">单精度浮点数的格式串</param>
    /// <returns></returns>
    public static float ToFloat(this string str)
    {
        if (str == StringExtension.ZeroString)
        {
            return 0f;
        }
        return Convert.ToSingle(str);
    }

    /// <summary>
    /// 四舍五入
    /// </summary>
    /// <param name="inputValue">十进制数字格式的字符串</param>
    /// <param name="digits">四舍五入的小数位数</param>
    /// <returns></returns>
    //public static decimal ToDecimal(this string inputValue, int digits)
    //{
    //    decimal result;
    //    try
    //    {
    //        result = decimal.Round(inputValue.ToDecimal(), digits);
    //    }
    //    catch (Exception innerException)
    //    {
    //        throw new ConvertException(inputValue, "ToDecimal", "Decimal", innerException);
    //    }
    //    return result;
    //}

    /// <summary>
    /// 转换为某种枚举类型
    /// </summary>
    /// <typeparam name="T">类型，必须是枚举类型</typeparam>
    /// <param name="inputVale">字符串</param>
    /// <returns></returns>
    public static T ToEnum<T>(this string inputVale)
    {
        return (T)((object)Enum.Parse(typeof(T), inputVale, true));
    }

    /// <summary>
    /// 转换为布尔型
    /// </summary>
    /// <param name="input">字符串</param>
    /// <returns>如果值是：值对1、T、Y、True、Yes,返回true,否则返回false</returns>
    //public static bool ToBoolean(this string input)
    //{
    //    if (input == null)
    //    {
    //        return false;
    //    }
    //    if (!input.IsDecimal())
    //    {
    //        input = input.ToUpper();
    //        while (true)
    //        {
    //            if (!false)
    //            {
    //                goto IL_21;
    //            }
    //        IL_30:
    //            if (input == "Y")
    //            {
    //                break;
    //            }
    //            if (input == "TRUE")
    //            {
    //                break;
    //            }
    //            if (!(input == "YES"))
    //            {
    //                return false;
    //            }
    //            if (!false)
    //            {
    //                break;
    //            }
    //            if (!true)
    //            {
    //                goto IL_4C;
    //            }
    //            if (2 == 0)
    //            {
    //                return false;
    //            }
    //            if (false)
    //            {
    //                continue;
    //            }
    //        IL_21:
    //            if (input == "T")
    //            {
    //                break;
    //            }
    //        IL_4C:
    //            goto IL_30;
    //        }
    //        return true;
    //    }
    //    decimal d = input.ToDecimal();
    //    return d == 1.0m;
    //}

    ///// <summary>
    ///// 转换为日期时间
    ///// </summary>
    ///// <param name="str">字符串</param>
    ///// <returns></returns>
    public static DateTime ToDateTime(this string str)
    {
        DateTime result;
        try
        {
            //result = str.ToDateTime(DefaultDateFormat + " " + DefaultTimeFormat);
            result = DateTime.ParseExact(str, DefaultDateFormat + " " + DefaultTimeFormat, null);
        }
        catch
        {
            //result = str.ToDateTime(DefaultDateFormat);
            result = DateTime.ParseExact(str, DefaultDateFormat, null);
        }
        return result;
    }

    ///// <summary>
    ///// 转换为日期时间
    ///// </summary>
    ///// <param name="str">字符串</param>
    ///// <returns></returns>
    public static DateTime ToDate(this string str)
    {
        return DateTime.ParseExact(str, DefaultDateFormat, null);
    }

    public static bool ToBoolean(this string str)
    {
        bool bl =false;
        try
        {
            bool.TryParse(str, out bl);
        }
        catch {
            bl = false;
        }
        return bl;
    }

    //public static DateTime ToDateTime(this string str)
    //{
    //    return DateTime.ParseExact(str, DefaultDateFormat + " " + DefaultTimeFormat, null);
    //}

    /// <summary>
    /// 根据日期格式转换为日期时间
    /// </summary>
    /// <param name="str">日期字符串</param>
    /// <param name="format">格式串</param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string str, string format)
    {
        return DateTime.ParseExact(str, format, null);
    }

    /// <summary>
    /// 和 GetName 是一组函数
    /// 返回 "中国(142)" 中的 "142"
    /// </summary>
    /// <param name="value">名称代码组字符串</param>
    public static string GetCode(this string value)
    {
        int num = value.LastIndexOf("(");
        int num2 = value.Length - 1;
        if (!false)
        {
        }
        if (num2 == -1)
        {
            goto IL_3C;
        }
    IL_0E:
        while (!(value.Substring(num2, 1) != ")"))
        {
            if (!false)
            {
                goto IL_2A;
            }
        }
        goto IL_3C;
    IL_2A:
        if (num2 < num + 2)
        {
            return "";
        }
        return value.Substring(num + 1, num2 - num - 1);
    IL_3C:
        num2 = -1;
        bool flag = (uint)num - (uint)num2 < 0u;
        if (flag)
        {
            goto IL_0E;
        }
        if (!false)
        {
            goto IL_2A;
        }
        goto IL_0E;
    }

    /// <summary>
    /// 和 GetCode 是一组函数
    /// 返回 "中国(142)" 中的 "中国"
    /// </summary>
    /// <param name="value">名称代码组</param>
    public static string GetName(this string value)
    {
        int num = value.LastIndexOf("(");
        if (num > 1)
        {
            return value.Substring(0, num);
        }
        return "";
    }

    private static string Replace(this string str)
    {
        str = str.Replace("'", "\\'");
        str = str.Replace("\r", "\\r");
        str = str.Replace("\n", "\\n");
        str = str.Replace("\"", "\\\"");
        return str;
    }

    /// <summary>
    /// 把异常错误信息，转成Javascript 字符串，通常是作为alert的参数
    /// </summary>
    /// <param name="msg">异常错误信息</param>
    /// <returns>Javascript 字符串</returns>       
    public static string ToJsString(this string msg)
    {
        return msg;// msg.xaa184da580508d92();
    }

    private static string IsNullOrEmpty(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }
        do
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace(">", "&gt;");
        }
        while (4 == 0);
        str = str.Replace("<", "&lt;");
        str = str.Replace("\"", "&quot;");
        return str;
    }

    /// <summary>
    /// 转换成XML字符串,用于一个节点的InnerText或者一个结点的属性的值
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns></returns>
    public static string ToXMLString(this string input)
    {
        return "";// input.xfa1bf67ad2460fe0();
    }
    public static bool IsDate(string strDate)
    {
        try
        {
            DateTime.Parse(strDate);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool IsDecimal(string Value)
    {
        try
        {
            Decimal.Parse(Value);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    
    private static string IsNullOrEmpty_sp(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }
        str = str.Replace("\r\n", "<br>");
        return str;
    }

    /// <summary>
    /// 把异常错误信息，转成HTML字符串，通常是输出到页面上的
    /// </summary>
    /// <param name="msg">异常错误信息</param>
    /// <returns>Javascript 字符串</returns>
    public static string ToHtmlString(this string msg)
    {
        return "";//msg.xff58331aa78c1e5a();
    }

    /// <summary>
    /// 转换为数据库操作时所需要的字符串
    /// </summary>
    /// <param name="inputValue">日期字符串</param>
    /// <returns></returns>
    //public static string ToDBString(this string inputValue)
    //{
    //    if (!inputValue.IsNullOrEmpty())
    //    {
    //        return "'" + inputValue.Trim().Replace("'", "''") + "'";
    //    }
    //    return "''";
    //}

    /// <summary>
    ///  返回文件全路径的目录部分
    /// </summary>
    /// <param name="strPath">路径</param>
    public static string GetFileName_Path(this string strPath)
    {
        return Path.GetDirectoryName(strPath);
    }

    /// <summary>
    /// 和 TransStr_toSBC 是一组函数
    /// 返回 全角字符换成半角
    /// </summary>
    /// <param name="value">可能含有全角字符的字符串</param>
    public static string TransStr2DBC(this string value)
    {
        string text = "～｀１２３４５６７８９０－＝！·＃￥％…—＊（）—＋［］｛｝ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ";
        string text2 = "~`1234567890-=!@#$%^&*()-+[]{}abcdefghijklmnopqrstuvwxyz";
        int i;
        do
        {
            for (i = 0; i < text2.Length; i++)
            {
                value = value.Replace(text.Substring(i, 1), text2.Substring(i, 1));
            }
        }
        while ((uint)i + (uint)i > 4294967295u);
        return value;
    }

    /// <summary>
    /// 和 TransStr_toDBC 是一组函数
    /// 返回 半角换成全角字符
    /// </summary>
    /// <param name="value">字符串</param>
    public static string TransStr2SBC(this string value)
    {
        string text = "～｀１２３４５６７８９０－＝！·＃￥％…—＊（）—＋［］｛｝ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ";
        string text2 = "~`1234567890-=!@#$%^&*()-+[]{}abcdefghijklmnopqrstuvwxyz";
        while (true)
        {
        IL_35:
            for (int i = 0; i < text2.Length; i++)
            {
                value = value.Replace(text2.Substring(i, 1), text.Substring(i, 1));
                if ((uint)i - (uint)i > 4294967295u)
                {
                    goto IL_35;
                }
            }
            break;
        }
        return value;
    }

    /// <summary>
    /// 转换为String，如果是"","nbsp;",null==》"",否则源字符串返回
    /// </summary>
    /// <param name="inputValue">字符串</param>
    /// <returns></returns>
    //public static string NullToEmpty(this string inputValue)
    //{
    //    if (inputValue.IsNullOrEmpty() || inputValue.Trim() == "&nbsp;")
    //    {
    //        return "";
    //    }
    //    return inputValue;
    //}

    /// <summary>
    /// 将格式类似为：7D1B6DD6-C1A9-416e-9E7F-31400E3F7249字符串，转换为GUID类型的对象
    /// </summary>
    /// <param name="inputValue">格式类似为：7D1B6DD6-C1A9-416e-9E7F-31400E3F7249的字符串</param>
    /// <returns></returns>
    public static Guid ToGuid(this string inputValue)
    {
        return new Guid(inputValue);
    }

    /// <summary>
    /// 生成一个新的GUID
    /// </summary>
    /// <returns></returns>
    public static string NewGuid()
    {
        return Guid.NewGuid().ToString();
    }
}