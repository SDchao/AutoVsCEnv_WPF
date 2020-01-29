using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AutoVsCEnv_WPF.Operators
{
    class Logger
    {
        private FileStream fileStream;
        public Logger(string fileName)
        {
            try
            {
                fileStream = File.Open(fileName, FileMode.Append);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
                fileStream = null;
            }
        }

        public void Info(string context)
        {
            if(fileStream != null)
            {
                context = DateTime.Now.ToString("hh:mm:ss") + "[INFO] " + context + "\n";
                byte[] text = Encoding.UTF8.GetBytes(context);
                fileStream.Write(text, 0, text.Length);
                fileStream.Flush();
                Console.WriteLine(context);
            }
        }

        public void Warn(string context)
        {
            if(fileStream != null)
            {
                context = DateTime.Now.ToString("hh:mm:ss") + "[WARN] " + context + "\n";
                byte[] text = Encoding.UTF8.GetBytes(context);
                fileStream.Write(text, 0, text.Length);
                fileStream.Flush();
                Console.WriteLine(context);
            }
        }

        public void Err(string context)
        {
            if (fileStream != null)
            {
                context = DateTime.Now.ToString("hh:mm:ss") + "[ERROR] " + context + "\n";
                byte[] text = Encoding.UTF8.GetBytes(context);
                fileStream.Write(text, 0, text.Length);
                fileStream.Flush();
                Console.WriteLine(context);
            }
        }

        public void Dispose()
        {
            if (fileStream != null)
                fileStream.Close();
        }
    }
}
