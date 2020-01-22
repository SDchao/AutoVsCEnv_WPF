using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoVsCEnv_WPF.Operators
{
    class Installer
    {
        private string gccPath;
        private string projectPath;

        const string lanzouUrl = "https://www.lanzous.com/i7iwn2h?p";

        /// <summary>
        /// 显示进度委托
        /// </summary>
        /// <param name="progressText">进度信息字符串</param>
        public delegate void OnProgressChangeHandler(String progressText);
        /// <summary>
        /// 当进度变化时的事件操作
        /// </summary>
        public event OnProgressChangeHandler OnProgressChangeEvent;

        private string operation;

        public Installer(string gccPath, string projectPath)
        {
            this.gccPath = gccPath;
            this.projectPath = projectPath;
        }
        /// <summary>
        /// 开始安装操作
        /// </summary>
        public void StartInstall()
        {
            ChangeProgress("正在检查gcc环境");
            bool hasGcc = EnvChecker.CheckGcc();

            ChangeProgress("正在检查VScode环境");
            string codePath = EnvChecker.GetCodePath();
            if(codePath == EnvChecker.NOTFOUND)
            {
                MessageBox.Show("没有找到 VScode，将不会为您配置 C/C++ 插件\n若您已经安装了VScode，请在 VScode 插件列表中搜索安装。",
                    "找不到喵",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }

            if (hasGcc) // 发布前记得修改啊！！！！！！！！！！！！！！！！！！！！！
            {
                ChangeProgress("正在解析 MinGW 下载链接");
                string downloadUrl = LanzouLinkResolutor.Resolve(lanzouUrl);

                ChangeProgress("正在下载 MinGW");
                DownloadHelper downloadHelper = new DownloadHelper();
                downloadHelper.OnProgressChanged += UpdateDownloadProgress;
                downloadHelper.Download(downloadUrl, "data");

                ChangeProgress("正在解压 MinGW");
                ExtractHelper.Extract(@"data\MinGW.7z", gccPath);

                ChangeProgress("正在配置工作区");
                ExtractHelper.Extract(@"data\config.7z", projectPath);

                ChangeProgress("正在修改用户Path路径");
                
            }

            if (codePath != EnvChecker.NOTFOUND)
            {
                ChangeProgress("正在安装 C/C++插件");
                CmdResult result = CmdRunner.CmdRun(codePath + "code --install-extension ms-vscode.cpptools");
                if(!result.result.Contains("is already installed") && !result.result.Contains("was successfully installed"))
                {
                    MessageBox.Show("未能成功安装 C/C++ 插件。请手动安装哦",
                    "安装失败了喵", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void ChangeProgress(string newOperation)
        {
            operation = newOperation;
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            OnProgressChangeEvent(operation);
        }

        private void UpdateDownloadProgress(string percent, string speed, string eta)
        {
            string showString = operation + " (" + percent + ") " + speed + "/s 预计剩余: " + eta;
            OnProgressChangeEvent(showString);
        }

        /*
        private void UpdateProgress(double percent)
        {
            string percentStr = String.Format("f2", percent * 100);
            OnProgressChangeEvent(operation + " (" + percent + "%)");
        }
        */
    }
}
