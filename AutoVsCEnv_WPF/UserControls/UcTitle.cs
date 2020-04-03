using System.ComponentModel;
using System.Windows.Controls;

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

        public string MainText
        {
            get
            {
                return _MainText;
            }
            set
            {
                _MainText = value;
                NotifyPropertyChanged(nameof(MainText));
            }
        }

        public string SubText
        {
            get
            {
                return _SubText;
            }
            set
            {
                _SubText = value;
                NotifyPropertyChanged(nameof(SubText));
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}