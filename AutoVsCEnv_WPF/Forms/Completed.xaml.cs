using System.Windows;
using System.Windows.Controls;

namespace AutoVsCEnv_WPF.Forms
{
    /// <summary>
    /// Completed.xaml 的交互逻辑
    /// </summary>
    public partial class Completed : Page
    {
        public Completed()
        {
            InitializeComponent();
        }

        private void CheckDoc(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.bilibili.com/video/av52434248");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}