using AutoVsCEnv_WPF.Operators;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AutoVsCEnv_WPF.Forms
{
    /// <summary>
    /// Installing.xaml 的交互逻辑
    /// </summary>
    public partial class Installing : Page, IDisposable
    {
        private string gccPath;
        private string projectPath;
        private BackgroundWorker worker;

        public Installing(string gccPath, string projectPath)
        {
            InitializeComponent();
            this.gccPath = gccPath;
            this.projectPath = projectPath;
            worker = new BackgroundWorker();
        }

        private void CheckBiliSpace(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://space.bilibili.com/12263994");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += StartInstall;
            worker.ProgressChanged += ProgressChanged;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void StartInstall(object sender, DoWorkEventArgs args)
        {
            try
            {
                Installer installer = new Installer(gccPath, projectPath);
                installer.OnProgressChangeEvent += ProgressChangeSend;
                installer.StartInstall();
            }
            catch (Exception e)
            {
                ErrorShower.Show(e);
            }
        }

        // 收到Installer的事件调用，将内容发送给worker
        private void ProgressChangeSend(string text)
        {
            worker.ReportProgress(0, text);
        }

        //worker处理进度
        private void ProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            WorkingWith.Text = args.UserState as string;
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            Application.Current.MainWindow.Content = new Completed();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool b)
        {
            worker.Dispose();
            this.Dispose(b);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
            }
        }
    }
}