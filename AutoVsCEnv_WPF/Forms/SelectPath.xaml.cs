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
using Ookii.Dialogs.Wpf;
using System.IO;

namespace AutoVsCEnv_WPF.Forms
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class SelectPath : Page
    {
        public string SelectedGccPath { get; set; }

        public string SelectedProjectPath { get; set; }

        private static int NowStep = 0;


        public SelectPath()
        {
            InitializeComponent();
            this.DataContext = this;
            UpdateText();
        }

        private void UpdateText()
        {
            if(NowStep == 0)
            {
                Title.MainText = "我们需要您提供一些路径";
                Title.SubText = "请选择MinGW编译器安装位置";
                Title.Description = "MinGW编译器是将源代码(.c 或 .cpp)文件\n编译为可执行(.exe)文件的工具。";
                PrevButton.Content = "取消";
            }
            else
            {
                Title.MainText = "还需要选择最后一个路径";
                Title.SubText = "请选择 VScode 项目文件夹位置";
                Title.Description = "项目文件夹是存放源代码(.c 或 .cpp)的位置\n您今后需要调试的代码都需要存放在此文件夹内";
                PrevButton.Content = "上一步";
            }
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            Console.WriteLine(SelectedGccPath);
            dialog.SelectedPath = SelectedGccPath;
            dialog.ShowDialog();
            if(Directory.Exists(dialog.SelectedPath))
            {
                if (InculdeChinese(dialog.SelectedPath))
                {
                    MessageBox.Show("路径包含中文", "路径错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    SelectedGccPath = dialog.SelectedPath;
                    PathInput.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                }
            }
            else
            {
                MessageBox.Show("路径不存在", "路径错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool InculdeChinese(string text)
        {
            char[] textArr = text.ToCharArray();
            for (int i = 0; i < textArr.Length; i++)
            {
                if (textArr[i] >= 0x4e00 && textArr[i] <= 0x9fbb)
                {
                    return true;
                }
            }
            return false;
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if(NowStep == 0)
            {
                
            }
        }
    }
}
