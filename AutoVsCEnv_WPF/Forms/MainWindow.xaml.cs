using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AutoVsCEnv_WPF.Operators;

namespace AutoVsCEnv_WPF.Forms
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task task = new Task(() =>
            {
                bool hasUpdate = UpdateChecker.HasUpdate();
                if (hasUpdate)
                {
                    MessageBoxResult r = MessageBox.Show("发现更新，这个版本可能已经不可以用了喵\n是否前往更新？", "更新提示", MessageBoxButton.YesNo);
                    if (r == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start("https://github.com/SDchao/AutoVsCEnv_WPF/releases/latest");
                    }
                }
            });
            this.Content = new Welcome();
        }
    }
}
