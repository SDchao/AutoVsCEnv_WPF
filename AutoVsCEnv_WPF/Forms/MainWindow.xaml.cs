using AutoVsCEnv_WPF.Operators;
using System.Threading.Tasks;
using System.Windows;

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
            task.Start();
            this.Content = new Welcome();
        }
    }
}