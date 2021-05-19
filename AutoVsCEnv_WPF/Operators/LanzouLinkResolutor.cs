using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoVsCEnv_WPF.Operators
{
    internal class LanzouLinkResolutor
    {
        /// <summary>
        /// 解析给出的蓝奏云分享链接直链
        /// </summary>
        /// <param name="LanzouLink">欲解析的分享链接</param>
        /// <returns></returns>
        public static string Resolve(string LanzouLink)
        {
            string content = ReadHttpSourceCode(LanzouLink);
            string downloadPageUri = SolveDownloadPageUri(content);
            string downloadUrl = SolveDownloadUrl(downloadPageUri);

            return downloadUrl;
        }

        private static string SolveDownloadUrl(string downloadPageUrl)
        {
            string content = ReadHttpSourceCode(downloadPageUrl);
            string sign = "";
            string signValue = "";
            string data = "";

            // 获取Content中JS部分
            Regex jsRegex = new Regex(@"<script type=""text/javascript"">\n([\S\s]*?)</script>");
            Match jsMatch = jsRegex.Match(content);

            if(jsMatch.Success)
            {
                string jsContent = jsMatch.Groups[1].Value;

                // 注释替换表达式
                Regex desciptionRegex = new Regex(@"//.*?[\n]");
                jsContent = desciptionRegex.Replace(jsContent, "\n");

                // 获取data值
                Regex dataRegex = new Regex(@"data : (.*?,'sign':(.*?),.*?})");
                Match dataMatch = dataRegex.Match(jsContent);

                if (dataMatch.Success)
                {
                    data = dataMatch.Groups[1].Value;
                    sign = dataMatch.Groups[2].Value;

                    //根据Sign获取Sign值
                    Regex signRegex = new Regex("var " + sign + " *?= *?'(.*?)';");
                    Match signMatch = signRegex.Match(jsContent);

                    if (signMatch.Success)
                    {
                        signValue = signMatch.Groups[1].Value;
                        data = data.Replace("'sign':" + sign, "'sign':'" + signValue + "'");
                    }

                    // 获取ajaxdata
                    if (data.Contains("'signs':ajaxdata")) {
                        Regex ajaxdataRegex = new Regex("var ajaxdata = '(.*)';");
                        Match ajaxdataMatch = ajaxdataRegex.Match(jsContent);

                        if (ajaxdataMatch.Success)
                        {
                            string ajaxdataValue = ajaxdataMatch.Groups[1].Value;
                            data = data.Replace("'signs':ajaxdata", "'signs':'" + ajaxdataValue + "'");
                        }
                        else
                        {
                            throw new Exception("无法获取ajaxdata: \ndata:" + data);
                        }
                    }

                }
                else
                {
                    throw new Exception("无法解析下载iFrame Data数据");
                }
            }
            else
            {
                throw new Exception("无法获取页面的JS信息");
            }

            //转化data为键值对
            try
            {
                data = Json2FormData(data);
            }
            catch (Exception)
            {
                throw new Exception("无法转化data为Json:\n" + data + "\n" + sign + ": " + signValue);
            }

            data = Encoding.UTF8.GetString(Encoding.Default.GetBytes(data));
            string phpContent = PostAjax(data, downloadPageUrl);
            string finalUrl = "";
            Regex domRegex = new Regex("\"dom\":\"(.*)\",\"url\"");
            Regex urlRegex = new Regex("\"url\":\"(.*)\",\"inf\"");

            Match domMatch = domRegex.Match(phpContent);
            Match urlMatch = urlRegex.Match(phpContent);

            if (domMatch.Success)
            {
                finalUrl = domMatch.Groups[1].Value.Replace("\\", "");
            }

            finalUrl += "/file/";

            if (urlMatch.Success)
            {
                finalUrl += urlMatch.Groups[1].Value.Replace("\\", "");
            }

            return finalUrl;
        }

        private static string SolveDownloadPageUri(string content)
        {
            Regex regex = new Regex("<iframe class=\"ifr2\" name=\"[0-9]+\" src=\"(.+)\" frameborder=\"0\" scrolling=\"no\"></iframe>\n");
            Match match = regex.Match(content);
            string relativeUrl = string.Empty;
            if (match.Success)
            {
                relativeUrl = match.Groups[1].Value;
            }

            if (relativeUrl != string.Empty)
            {
                return "https://sdchao.lanzoui.com" + relativeUrl;
            }
            else
                return null;
        }

        private static string ReadHttpSourceCode(string url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "GET";
            request.Timeout = 10000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36 Edg/79.0.309.68";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(new BufferedStream(response.GetResponseStream()), Encoding.UTF8);
            string content = reader.ReadToEnd();
            response.Close();
            reader.Close();
            return content;
        }

        private static string PostAjax(string data, string refer)
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://sdchao.lanzoui.com/ajaxm.php");
            request.Method = "POST";
            request.Timeout = 10000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36 Edg/79.0.309.68";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json, text/javascript, */*";
            request.ContentLength = data.Length;
            request.Referer = refer;
            request.Headers.Add("origin", "https://www.lanzoui.com");
            request.Headers.Set(HttpRequestHeader.Cookie, "sec_tc=AQAAAHB5XRdl9Q4AJDhn091ZwaV4BKpX; pc_ad1=1");

            //写入data
            StreamWriter writer = new StreamWriter(new BufferedStream(request.GetRequestStream()));
            writer.Write(data);
            writer.Close();

            //获取响应
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(new BufferedStream(response.GetResponseStream()));
            string content = reader.ReadToEnd();
            reader.Close();

            return content;
        }

        private static string Json2FormData(string jsonString)
        {
            StringBuilder builder = new StringBuilder();

            Dictionary<string, string> keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            foreach (KeyValuePair<string, string> pair in keyValuePairs)
            {
                builder.Append(pair.Key).Append("=").Append(pair.Value).Append("&");
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
    }
}