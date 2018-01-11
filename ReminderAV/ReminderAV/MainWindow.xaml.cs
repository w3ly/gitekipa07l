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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;
namespace ReminderAV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        ObservableCollection<UserEvent> _events = new ObservableCollection<UserEvent>();
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
        UserEvent _selectedEvent;
        public UserEvent SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                _selectedEvent = value;
                IsSelectedItem = (_selectedEvent != null) ? true : false;
                if (IsSelectedItem)
                {
                    EditEventDate = SelectedEvent.EventDate;
                    EditEventDesc = SelectedEvent.EventDesc;
                    EditEventTitle = SelectedEvent.EventTitle;
                    EditEventHour = SelectedEvent.EventDate.Hour;
                    EditEventMinute = SelectedEvent.EventDate.Minute;
                }
                NotifyPropertyChanged("SelectedEvent");
            }
        }
        bool _isSelectedItem = false;
        public bool IsSelectedItem
        {
            get
            {
                return _isSelectedItem;
            }
            set
            {
                _isSelectedItem = value;
                NotifyPropertyChanged("IsSelectedItem");
            }
        }
        #region New event region
        DateTime _newEventDate;
        public DateTime NewEventDate
        {
            get
            {
                return _newEventDate;
            }
            set
            {
                System.Diagnostics.Debug.WriteLine("CHANGE! " + value);
                _newEventDate = value;
                NotifyPropertyChanged("NewEventDate");
            }
        }
        string _newEventTitle;
        public string NewEventTitle
        {
            get
            {
                return _newEventTitle;
            }
            set
            {
                _newEventTitle = value;
                NotifyPropertyChanged("NewEventTitle");
            }
        }
        string _newEventDesc;
        public string NewEventDesc
        {
            get
            {
                return _newEventDesc;
            }
            set
            {
                _newEventDesc = value;
                NotifyPropertyChanged("NewEventDesc");
            }
        }
        int _newEventHour;
        public int NewEventHour
        {
            get
            {
                return _newEventHour;
            }
            set
            {
                _newEventHour = Int32.Parse(value.ToString());
                NotifyPropertyChanged("NewEventHour");
            }
        }
        int _newEventMinute;
        public int NewEventMinute
        {
            get
            {
                return _newEventMinute;
            }
            set
            {
                _newEventMinute = value;
                NotifyPropertyChanged("NewEventMinute");
            }
        }
        #endregion
        #region Edit event region
        DateTime _editEventDate;
        public DateTime EditEventDate
        {
            get
            {
                return _editEventDate;
            }
            set
            {
                _editEventDate = value;
                NotifyPropertyChanged("EditEventDate");
            }
        }
        string _editEventTitle;
        public string EditEventTitle
        {
            get
            {
                return _editEventTitle;
            }
            set
            {
                _editEventTitle = value;
                NotifyPropertyChanged("EditEventTitle");
            }
        }
        string _editEventDesc;
        public string EditEventDesc
        {
            get
            {
                return _editEventDesc;
            }
            set
            {
                _editEventDesc = value;
                NotifyPropertyChanged("EditEventDesc");
            }
        }
        int _editEventHour;
        public int EditEventHour
        {
            get
            {
                return _editEventHour;
            }
            set
            {
                _editEventHour = Int32.Parse(value.ToString());
                NotifyPropertyChanged("EditEventHour");
            }
        }
        int _editEventMinute;
        public int EditEventMinute
        {
            get
            {
                return _editEventMinute;
            }
            set
            {
                _editEventMinute = Int32.Parse(value.ToString());
                NotifyPropertyChanged("EditEventMinute");
            }
        }
        #endregion

        string xmlDirectoryPath;
        public MainWindow()
        {
            //Take path from config
            xmlDirectoryPath = ConfigurationManager.AppSettings.Get("EventsPath");
            if (string.IsNullOrEmpty(xmlDirectoryPath))
            {
                xmlDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + @"\"+ConfigurationManager.AppSettings.Get("EventsName"); ;
                ConfigurationManager.AppSettings.Set("EventsPath", xmlDirectoryPath);
            }

            //For debug purposes
            System.Diagnostics.Debug.WriteLine("---> " + xmlDirectoryPath);

            InitializeComponent();

            //Hack that simplifies picking date
            NewEventDate = DateTime.Now;

            LoadEvents();
       
            this.DataContext = this;
        }


        #region Interface impelentation
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        //Add event btn
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //To make sure Date is not affected by SOMETHING
            DateTime dt = new DateTime(NewEventDate.Year, NewEventDate.Month, NewEventDate.Day);
            TimeSpan ts = new TimeSpan(NewEventHour, NewEventMinute, 00);
            dt += ts;

            if (dt >= DateTime.Now && !string.IsNullOrEmpty(NewEventTitle))
            {
                if (string.IsNullOrEmpty(NewEventDesc))
                    NewEventDesc = "No description. You can fill description later in edit module.";
                Events.Add(new UserEvent(dt, NewEventTitle, NewEventDesc));

                //Reload view properties
                NewEventDate = DateTime.Today;
                NewEventHour = 0;
                NewEventMinute = 0;
                NewEventTitle = "";
                NewEventDesc = "";
                MessageBox.Show("New event has been added to the list");
                ReloadList();
            }
            //Called when requirements were not met
            else
                MessageBox.Show("OMG SOMETHING GONE WRONG WITH ADDING NEW EVENT, LOL");
        }
        //Remove event btn
        private void RemoveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEvent != null)
            {
                MessageBox.Show(string.Format("Event {0} has been removed", SelectedEvent.EventTitle));
                Events.Remove(SelectedEvent);

                ReloadList();

                if (Events.Count > 0)
                    SelectedEvent = Events[0];
                else
                {
                    SelectedEvent = null;
                    MessageBox.Show("No more events on the list.");
                }
            }
            else
                MessageBox.Show("Select event first.");
        }

        //Called when window is being closed
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SaveEvents();
        }

        //Edit event btn
        private void Edit_Click_1(object sender, RoutedEventArgs e)
        {
            UserEvent newUser = SelectedEvent.Clone();
            DateTime dt = new DateTime(EditEventDate.Year, EditEventDate.Month, EditEventDate.Day);
            TimeSpan ts = new TimeSpan(EditEventHour, EditEventMinute, 0);
            dt += ts;
            newUser.EventDate = dt;
            newUser.EventTitle = EditEventTitle;
            newUser.EventDesc = EditEventDesc;
            Events.Remove(SelectedEvent);
            Events.Add(newUser);
            ReloadList();
        }
        //Save to file, clear list, load from file
        void ReloadList()
        {
            SaveEvents();
            Events.Clear();
            LoadEvents();
        }
        //Load from file in .exe directory
        void LoadEvents()
        {
            EventsList el = new EventsList();

            XmlSerializer serializer = new XmlSerializer(typeof(EventsList));
            try
            {
                using (var x = new FileStream(xmlDirectoryPath, FileMode.Open))
                {
                    var xml = new XmlSerializer(typeof(EventsList));
                    el = (EventsList)xml.Deserialize(x);

                    bool changes = false;
                    foreach (var item in el.Events)
                    {
                        if (item.EventDate < DateTime.Now)
                            changes = true;
                        else
                            Events.Add(item);
                    }
                    if (changes)
                        MessageBox.Show("Some outdated events have been removed.");
                }
            }
            catch (Exception e)
            { 
                MessageBox.Show(e.Message, "Problem with opening events. Created new, empty file.");
                File.Create(xmlDirectoryPath);
            }
            if (Events.Count > 0 && Events != null)
                SelectedEvent = Events[0];
        }
        //Save to file in .exe file directory
        void SaveEvents()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(EventsList));
            try
            {
                using (var x = new FileStream(xmlDirectoryPath, FileMode.Create))
                {
                    EventsList el = new EventsList();
                    foreach (var item in Events)
                    {

                        el.Events.Add(item);
                    }

                    var xml = new XmlSerializer(typeof(EventsList));
                    xml.Serialize(x, el);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Problem with saving data");
            }
        }

        private void DirBTN_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml";

            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                //ConfigurationManager.AppSettings.Set("EventsPath", dlg.FileName.)
                string newPath = dlg.FileName;

                xmlDirectoryPath = ConfigurationManager.AppSettings.Get("EventsPath");
                if(MessageBox.Show("Save actual data in file and load new file now? Note that new file will be loaded with new appliaction startup", "Path has been changed", MessageBoxButton.YesNo, MessageBoxImage.Question ) == MessageBoxResult.Yes)
                {
                    SaveEvents();
                    ConfigurationManager.AppSettings.Set("EventsPath", newPath);
                    xmlDirectoryPath = newPath;
                    Events.Clear();
                    LoadEvents();
                }
                else
                {
                    ConfigurationManager.AppSettings.Set("EventsPath", newPath);
                }
                System.Diagnostics.Debug.WriteLine("!!! " + xmlDirectoryPath);
            }
        }
    }
}
