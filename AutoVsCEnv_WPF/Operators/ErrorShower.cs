using System;
using System.Text;
using System.Threading;
using System.Windows;

namespace AutoVsCEnv_WPF.Operators
{
    internal class ErrorShower
    {
        public static void Show(Exception e)
        {
            StringBuilder messgaeBuilder = new StringBuilder();
            string errorString = e.Message + "\n" + e.StackTrace + "\n";
            messgaeBuilder.Append("在配置中出现了以下异常：\n");
            messgaeBuilder.Append(errorString);
            messgaeBuilder.Append("恳请您向我发送反馈，以此改进配置工具！\n");
            messgaeBuilder.Append("是否愿意前往 Github 提交 Issue ?");
            MessageBoxResult result =
                MessageBox.Show(messgaeBuilder.ToString(), "哎呦被玩坏了",
                MessageBoxButton.YesNo, MessageBoxImage.Error, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("已复制错误信息到剪切板，即将跳转至Github", "谢谢");
                Thread t = new Thread(() =>
                {
                    Clipboard.SetText(errorString);
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();
                System.Diagnostics.Process.Start("https://github.com/SDchao/AutoVsCEnv_WPF/issues/new/choose");
            }
        }
    }
}