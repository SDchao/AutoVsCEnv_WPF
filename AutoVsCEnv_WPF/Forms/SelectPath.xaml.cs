using Ookii.Dialogs.Wpf;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace AutoVsCEnv_WPF.Forms
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class SelectPath : Page
    {
        public static string SelectedGccPath { get; set; }

        public static string SelectedProjectPath { get; set; }

        private static int NowStep = 0;

        public SelectPath()
        {
            InitializeComponent();
            this.DataContext = this;
            UpdateText();
        }

        private void UpdateText()
        {
            if (NowStep == 0)
            {
                UcTitle.MainText = "我们需要您提供一些路径";
                UcTitle.SubText = "请选择MinGW编译器安装位置";
                UcTitle.Description = "MinGW编译器是将源代码(.c 或 .cpp)文件\n编译为可执行(.exe)文件的工具。";
                PrevButton.Content = "取消";

                PathInput.SetBinding(TextBox.TextProperty, "SelectedGccPath");
            }
            else
            {
                UcTitle.MainText = "还需要选择最后一个路径";
                UcTitle.SubText = "请选择 VScode 项目文件夹位置";
                UcTitle.Description = "项目文件夹是存放源代码(.c 或 .cpp)的位置\n您今后需要调试的代码都需要存放在此文件夹内";
                PrevButton.Content = "上一步";
                PathInput.SetBinding(TextBox.TextProperty, "SelectedProjectPath");
            }
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.SelectedPath = SelectedGccPath;
            dialog.ShowDialog();
            PathInput.Text = dialog.SelectedPath;
            PathInput.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void PathInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PathCheck(PathInput.Text))
            {
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private bool InculdeIllegal(string text)
        {
            Regex regex = new Regex(@"[^a-zA-Z0-9:_\\]");
            if (regex.Match(text).Success)
                return true;
            return false;
        }

        private bool PathCheck(string path)
        {
            if (!Directory.Exists(path))
            {
                PathError.Text = "路径不存在";
                return false;
            }

            if (InculdeIllegal(path))
            {
                PathError.Text = "路径包含空格或特殊符号";
                return false;
            }

            PathError.Text = "";
            return true;
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (NowStep == 0)
            {
                Application.Current.MainWindow.Content = new Welcome();
            }
            else
            {
                NowStep = 0;
                UpdateText();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (NowStep == 0)
            {
                NowStep = 1;
                UpdateText();
            }
            else
            {
                Application.Current.MainWindow.Content = new Installing(SelectedGccPath, SelectedProjectPath);
            }
        }
    }
}