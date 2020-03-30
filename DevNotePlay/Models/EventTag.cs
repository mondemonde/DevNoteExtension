using System;
using System.ComponentModel;

namespace Player.Models
{
    public class EventTag
    {
        private int _id;
        public string _domain;
        public string _department;
        public string _tag;
        public string _name;
        public string _description;
        public string _sourcePath;
        public bool _inactive;
        public int _versionNo;
        public string _fileName;
        public DateTime _created;
        public DateTime? _modified;

        public int Id { get { return _id; } set { _id = value; } }
        public string Domain
        {
            get { return _domain; }
            set
            {
                if (_domain != value)
                {
                    _domain = value;
                    RaisePropertyChanged("Domain");
                }
            }
        }
        public string Department
        {
            get { return _department; }
            set
            {
                if (_department != value)
                {
                    _department = value;
                    RaisePropertyChanged("Department");
                }
            }
        }
        public string Tag
        {
            get { return _tag; }
            set
            {
                if (_tag != value)
                {
                    _tag = value;
                    RaisePropertyChanged("Tag");
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        public string Descriptions
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }
        public bool InActive
        {
            get { return _inactive; }
            set
            {
                if (_inactive != value)
                {
                    _inactive = value;
                    RaisePropertyChanged("InActive");
                }
            }
        }
        public int VersionNo
        {
            get { return _versionNo; }
            set
            {
                if (_versionNo != value)
                {
                    _versionNo = value;
                    RaisePropertyChanged("VersionNo");
                }
            }
        }
        public string SourcePath { get; set; }
        public string FileName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged (string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
