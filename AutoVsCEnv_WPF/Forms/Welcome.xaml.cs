using System.Windows;
using System.Windows.Controls;

namespace AutoVsCEnv_WPF.Forms
{
    /// <summary>
    /// Welcome.xaml 的交互逻辑
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new SelectPath();
        }

        private void CheckVideo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.bilibili.com/video/av52434248");
        }
    }
}