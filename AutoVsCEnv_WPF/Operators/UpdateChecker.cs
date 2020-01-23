using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace AutoVsCEnv_WPF.Operators
{
    class UpdateChecker
    {
        private const string version = "1.0";
        private const string checkPage = "";
        public static bool HasUpdate()
        {
            
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
    }
}
