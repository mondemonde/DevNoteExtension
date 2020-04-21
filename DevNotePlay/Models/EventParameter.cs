using System;
using System.ComponentModel;

namespace Player.Models
{
    public class EventParameter: INotifyPropertyChanged, IDataErrorInfo
    {
        private string _propertyName;
        private string _mappedToInputX;
        private string _defaultValue;

        public int Id { get; set; }
        public int WFProfileId { get; set; }

        [DisplayName("Property Name")]
        public string PropertyName
        {
            get { return _propertyName; }
            set
            {
                if (_propertyName != value)
                {
                    _propertyName = value;
                    RaisePropertyChanged("PropertyName");
                }
            }
        }
        [DisplayName("Mapped to Input")]
        public string MappedTo_Input_X
        {
            get { return _mappedToInputX; }
            set
            {
                if (_mappedToInputX != value)
                {
                    _mappedToInputX = value;
                    RaisePropertyChanged("MappedTo_Input_X");
                }
            }
        }
        [DisplayName("Default Value")]
        public string DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                if (_defaultValue != value)
                {
                    _defaultValue = value;
                    RaisePropertyChanged("DefaultValue");
                }
            }
        }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public string Error { get; set; }

        private static readonly string[] ValidatedProperties =
        {
            "PropertyName",
            "MappedTo_Input_X",
            "DefaultValue"
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
                    case "PropertyName":
                        if (string.IsNullOrEmpty(PropertyName) || PropertyName == "")
                        {
                            result = "Property name is required.";
                        }
                        break;
                    case "MappedTo_Input_X":
                        if (string.IsNullOrEmpty(MappedTo_Input_X) || MappedTo_Input_X == "")
                        {
                            result = "A selection is required.";
                        }
                        break;
                    case "DefaultValue":
                        if (string.IsNullOrEmpty(DefaultValue) || DefaultValue == "")
                        {
                            result = "Default value is required.";
                        }
                        break;
                }
                return result;
            }
        }
    }
}
