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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new SelectPath();
        }

        private void CheckVideo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.baidu.com");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
