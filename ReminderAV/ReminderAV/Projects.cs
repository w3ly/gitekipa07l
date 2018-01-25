using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ReminderAV
{
    [XmlRoot("Projects")]
    public class ProjectList
    {
        ObservableCollection<Projects> _projectsList = new ObservableCollection<Projects>();
        public ObservableCollection<Projects> ProjectsList
        {
            get
            {
                return _projectsList;
            }
            set
            {
                _projectsList = value;
            }
        }
    }
    public class Projects : INotifyPropertyChanged
    {
        string _title;
        DateTime _deadline;
        string _creator;
        string _duty;

        [XmlElement("Title")]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        [XmlElement("DeadLine")]
        public DateTime DeadLine
        {
            get
            {
                return _deadline;
            }
            set
            {
                _deadline = value;
                NotifyPropertyChanged("DeadLine");
            }
        }
        [XmlElement("Creator")]
        public string Creator
        {
            get
            {
                return _creator;
            }
            set
            {
                _creator = value;
                NotifyPropertyChanged("Creator");
            }
        }
        [XmlElement("Duty")]
        public string Duty
        {
            get
            {
                return _duty;
            }
            set
            {
                _duty = value;
                NotifyPropertyChanged("Duty");
            }
        }

        public Projects() { }
        public Projects(string title, string creator, string duty, DateTime deadline)
        {
            Title = title;
            Creator = creator;
            Duty = duty;
            DeadLine = deadline;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
