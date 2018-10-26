using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ku.Common
{
    /// <summary>
    /// C# 操作正则表达式的公共类  
    /// </summary>
    public class RegexHelper
    {
        #region 验证输入字符串是否与模式字符串匹配
        /// <summary>  
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true  
        /// </summary>  
        /// <param name="input">输入字符串</param>  
        /// <param name="pattern">模式字符串</param>          
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>  
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true  
        /// </summary>  
        /// <param name="input">输入的字符串</param>  
        /// <param name="pattern">模式字符串</param>  
        /// <param name="options">筛选条件</param>  
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }
        #endregion

        #region 筛选获取列表
        /// <summary>
        /// 筛选获取列表
        /// </summary> 
        /// <returns></returns>
        public static List<string> GetMatchList(string html, string pattern)
        {
            List<string> list = new List<string>();
            html = html.Replace("\r\n", "").Replace("\n", "");
            MatchCollection matches = Regex.Matches(html, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (Match match in matches)
            {
                list.Add(match.Value);
            }
            return list;
        }
        #endregion

        #region 正则获取val
        /// <summary>
        /// 获取特定的值
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string GetMatchVal(string html, string pattern)
        {
            html = html.Replace("\r\n", "").Replace("\n", "");
            Match math = Regex.Match(html, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return math.Groups["val"].Value;
        }
        #endregion

        #region 正则获取val2

        public static string GetMatchVal2(string html, string pattern)
        {
            html = html.Replace("\r\n", "").Replace("\n", "");
            Match math = Regex.Match(html, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return math.Groups["val2"].Value;
        }
        #endregion


        #region 获取匹配的索引个
        public static string GetMatchValue(string html, string pattern, int index = 0)
        {
            html = html.Replace("\r\n", "").Replace("\n", "");
            Match math = Regex.Match(html, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return math.Groups[index].Value;
        }
        #endregion

        #region 替换
        /// <summary>
        /// 替换
        /// <.*?> 匹配所有 html标签 （用来清除html标签）
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string Replace(string html, string pattern)
        {
            html = html.Replace("\r\n", "").Replace("\n", "");
            string vhtml = Regex.Replace(html, pattern, "");
            return vhtml;
        }
        public static string Replace(string html, string pattern, string replacement)
        {
            html = html.Replace("\r\n", "").Replace("\n", "");
            string vhtml = Regex.Replace(html, pattern, replacement);
            return vhtml;
        }
        #endregion

        #region 常用的正则匹配
        /// <summary>
        /// 匹配任意闭合HTML标签的正则表达式
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<string> GetHtmlTagList(string html, string tag)
        {
            string pattern = @"<(?<HtmlTag>" + tag + @")[^>]*?>((?<Nested><\k<HtmlTag>[^>]*>)|</\k<HtmlTag>>(?<-Nested>)|.*?)*</\k<HtmlTag>>";
            List<string> list = GetMatchList(html, pattern);
            return list;
        }
        /// <summary>
        /// 匹配任意闭合HTML标签的正则表达式
        /// 标签上额外的 如样式
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tag"></param>
        /// <param name="other">标签上额外的 如样式</param>
        /// <returns></returns>
        public static List<string> GetHtmlTagList(string html, string tag, string other)
        {
            string pattern = @"<(?<HtmlTag>" + tag + ")[^>]*?" + other + @"[^>]*?>((?<Nested><\k<HtmlTag>[^>]*>)|</\k<HtmlTag>>(?<-Nested>)|.*?)*</\k<HtmlTag>>";
            List<string> list = GetMatchList(html, pattern);
            return list;
        }
        /// <summary>
        /// 匹配任意闭合HTML标签的正则表达式 返回第一个
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tag"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static string GetHtmlTagFirst(string html, string tag, string other)
        {
            if (!string.IsNullOrEmpty(other))
            {
                other = @"(\s*)" + other;
            }
            string pattern = @"<(?<HtmlTag>" + tag + ")[^>]*?" + other + @"[^>]*?>((?<Nested><\k<HtmlTag>[^>]*>)|</\k<HtmlTag>>(?<-Nested>)|.*?)*</\k<HtmlTag>>";
            List<string> list = GetMatchList(html, pattern);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return "";
            }

        }
        public static string GetHtmlTagFirst_ID(string html, string tag, string idName)
        {
            string pattern = @"<(?<HtmlTag>" + tag + @")[^>]*\s[iI][dD]=(?<Quote>[""']?)([\\w]+(\\s*))?" + idName + "((\\s*)[\\w]+)?(?(Quote)\\k<Quote>)[^>]*?(/>|>((?<Nested><\\k<HtmlTag>[^>]*>)|</\\k<HtmlTag>>(?<-Nested>)|.*?)*</\\k<HtmlTag>>)";
            List<string> list = GetMatchList(html, pattern);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取 包含某个class的标签 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tag">标签</param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static string GetHtmlTagFirst_Class(string html, string tag, string className)
        {
            string pattern = @"<(?<HtmlTag>" + tag + @")[^>]*\sclass=(?<Quote>[""']?)([\\w]+(\\s*))?" + className + "((\\s*)[\\w]+)?(?(Quote)\\k<Quote>)[^>]*?(/>|>((?<Nested><\\k<HtmlTag>[^>]*>)|</\\k<HtmlTag>>(?<-Nested>)|.*?)*</\\k<HtmlTag>>)";
            List<string> list = GetMatchList(html, pattern);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return "";
            }
        }

        public static List<string> GetHtmlTag_Class(string html, string tag, string className)
        {
            string pattern = @"<(?<HtmlTag>" + tag + @")[^>]*\sclass=(?<Quote>[""']?)([\\w]+(\\s*))?" + className + "((\\s*)[\\w]+)?(?(Quote)\\k<Quote>)[^>]*?(/>|>((?<Nested><\\k<HtmlTag>[^>]*>)|</\\k<HtmlTag>>(?<-Nested>)|.*?)*</\\k<HtmlTag>>)";
            List<string> list = GetMatchList(html, pattern);
            return list;
        }

        /// <summary>
        /// 获取标签上属性值
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tag">div|a|li</param>
        /// <param name="attr">title|href</param>
        /// <returns></returns>
        public static string GetTagAttributes(string html, string tag, string attr)
        {
            string pattern = string.Format(@"<{0}[^><]*{1}=""(?<val>[^""]*?)""[^><]*>", tag, attr);
            string value = GetMatchVal(html, pattern);
            return value;
        }

        #endregion

    }
}
