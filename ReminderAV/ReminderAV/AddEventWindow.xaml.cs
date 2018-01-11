using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ReminderAV
{
    /// <summary>
    /// Interaction logic for AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : Window, INotifyPropertyChanged
    {
        ObservableCollection<UserEvent> _events;
        public ObservableCollection<UserEvent> Events
        {
            get
            {
                return _events;
            }
            set
            {
                _events = value;
                NotifyPropertyChanged("Events");
            }
        }
        public AddEventWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
