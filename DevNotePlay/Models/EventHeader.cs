using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;

namespace Player.Models
{
    public class EventHeader: INotifyPropertyChanged, IDataErrorInfo
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
                    RaisePropertyChanged("FileName");
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
                    RaisePropertyChanged("FileName");
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
            //get { return _fileName; }
            //set
            //{
            //    if (_fileName != value)
            //    {
            //        _fileName = value;
            //        RaisePropertyChanged("FileName");
            //    }
            //}
            get { return Domain + "_" + Tag; }
        }
        public string TenantId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        [JsonIgnore]
        public string Error { get; set; }

        private static readonly string[] ValidatedProperties =
        {
            "Domain",
            "Department",
            "Tag",
            "VersionNo",
            //"FileName",
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
                    //case "FileName":
                    //    if (string.IsNullOrEmpty(FileName) || FileName == "")
                    //    {
                    //        result = "File name is required.";
                    //    }
                    //    else if (Path.GetExtension(FileName) != String.Empty)
                    //    {
                    //        result = "Extension cannot be included in File name.";
                    //    }
                    //    break;
                }
                return result;
            }
        }
    }
}

