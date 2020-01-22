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
using System.ComponentModel;

namespace AutoVsCEnv_WPF.UserControls
{
    /// <summary>
    /// Title.xaml 的交互逻辑
    /// </summary>
    public partial class UcTitle : UserControl, INotifyPropertyChanged
    {

        private string _MainText;
        private string _SubText;
        private string _Description;

        public UcTitle()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string MainText {
            get
            {
                return _MainText;
            }
            set
            {
                _MainText = value;
                NotifyPropertyChanged("MainText");
            }
        }
        public string SubText {
            get
            {
                return _SubText;
            }
            set
            {
                _SubText = value;
                NotifyPropertyChanged("SubText");
            }
        }
        public string Description {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
