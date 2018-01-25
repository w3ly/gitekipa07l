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
        string _projectTitle;
        string _projectCreator;
        string _projectDuty;
        DateTime _projectDeadLine;
        Projects _project;

        [XmlIgnore]
        public Projects Project
        {
            get
            {
                return _project;
            }
            set
            {
                _project = value;
                NotifyPropertyChanged("Project");
            }
        }
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

        [XmlElement("ProjectTitle")]
        public string ProjectTitle
        {
            get
            {
                return _projectTitle;
            }
            set
            {

                _projectTitle = value;
                NotifyPropertyChanged("ProjectTitle");
            }
        }
        [XmlElement("ProjectCreator")]
        public string ProjectCreator
        {
            get
            {
                return _projectCreator;
            }
            set
            {
                _projectCreator = value;
                NotifyPropertyChanged("ProjectCreator");
            }
        }
        [XmlElement("ProjectDuty")]
        public string ProjectDuty
        {
            get
            {
                return _projectDuty;
            }
            set
            {
                _projectDuty = value;
                NotifyPropertyChanged("ProjectDuty");
            }
        }
        [XmlElement("ProjectDeadLine")]
        public DateTime ProjectDeadLine
        {
            get
            {
                return _projectDeadLine;
            }
            set
            {
                _projectDeadLine = value;
                NotifyPropertyChanged("ProjectDeadLine");
            }
        }

        public UserEvent()
        {

        }
        public UserEvent(DateTime date, string title, string desc)
        {
            EventDate = date;
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
