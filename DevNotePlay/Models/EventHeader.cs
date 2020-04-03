using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Models
{
    public class EventHeader: INotifyPropertyChanged
    {

        private string _domain;
        private string _department;
        private string _tag;
        private string _description;
        private int _versionNo;
        private string _fileName;

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
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    RaisePropertyChanged("VersionNo");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}

