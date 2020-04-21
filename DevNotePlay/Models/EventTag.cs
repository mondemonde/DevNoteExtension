using System;
using System.ComponentModel;

namespace Player.Models
{
    public class EventTag: INotifyPropertyChanged, IDataErrorInfo
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

        [DisplayName("ID")]
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
        [DisplayName("Event Tag")]
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
                    RaisePropertyChanged("Descriptions");
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged (string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public string Error { get; set; }

        private static readonly string[] ValidatedProperties =
        {
            "Domain",
            "Department",
            "Tag",
            "VersionNo"
        };

        public bool IsValid()
        {
            foreach (string property in ValidatedProperties)
            {
                if (this[property] != "")
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Validates value of a property
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;

                switch (columnName)
                {
                    case "Domain":
                        if (string.IsNullOrEmpty(Domain) || Domain == "")
                        {
                            result = "Domain is required.";
                        }
                        break;
                    case "Department":
                        if (string.IsNullOrEmpty(Department) || Department == "")
                        {
                            result = "Department is required.";
                        }
                        break;
                    case "Tag":
                        if (string.IsNullOrEmpty(Tag) || Tag == "")
                        {
                            result = "Tag is required.";
                        }
                        break;
                    case "VersionNo":
                        if (!int.TryParse(VersionNo.ToString(), out int i))
                        {
                            result = "Version no. cannot be empty.";
                        }
                        break;
                }
                return result;
            }
        }
    }
}
