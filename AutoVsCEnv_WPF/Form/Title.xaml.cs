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

namespace AutoVsCEnv_WPF.Form
{
    /// <summary>
    /// Title.xaml 的交互逻辑
    /// </summary>
    public partial class Title : UserControl
    {

        public Title()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string MainText { get; set; }
        public string SubText { get; set; }
        public string Description { get; set; }
    }
}
