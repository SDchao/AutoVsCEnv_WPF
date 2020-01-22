using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVsCEnv_WPF.Operators
{
    class Installer
    {
        private string gccPath;
        private string projectPath;

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

        private void UpdateProgress(double percent)
        {
            string percentStr = String.Format("f2", percent * 100);
            OnProgressChangeEvent(operation + " (" + percent + "%)");
        }
    }
}
