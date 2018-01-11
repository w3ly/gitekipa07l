using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ReminderAV
{
    [XmlRoot("Event")]
    public class EventsList
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
            }
        }
    }

    public class UserEvent : INotifyPropertyChanged
    {
        DateTime _eventDate;
        string _eventTitle;
        string _eventDesc;
        [XmlElement("Date")]
        public DateTime EventDate
        {
            get
            {
                return _eventDate;
            }
            set
            {
                _eventDate = value;
                NotifyPropertyChanged("EventDate");
            }
        }
        [XmlElement("Title")]
        public string EventTitle
        {
            get
            {
                return _eventTitle;
            }
            set
            {
                _eventTitle = value;
                NotifyPropertyChanged("EventTitle");
            }
        }
        [XmlElement("Description")]
        public string EventDesc
        {
            get
            {
                return _eventDesc;
            }
            set
            {
                _eventDesc = value;
                NotifyPropertyChanged("EventDesc");
            }
        }

        public UserEvent()
        {

        }
        public UserEvent(DateTime date, string title, string desc)
        {
            EventDate = date;
            System.Diagnostics.Debug.WriteLine("!---> " + date);
            EventTitle = title;
            EventDesc = desc;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public UserEvent Clone()
        {
            UserEvent ue = new UserEvent(_eventDate, _eventTitle.Clone().ToString(), _eventDesc.Clone().ToString());
            return ue;
        }
    }
}
