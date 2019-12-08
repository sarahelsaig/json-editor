using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace JsonEditor
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged<T>(ref T variable, T value, [CallerMemberName] String propertyName = "")
        {
            variable = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string _JsonString = "{}";
        public string JsonString { get => _JsonString; set { if (_JsonString != value) NotifyPropertyChanged(ref _JsonString, value); } }

        JObject _Json = JObject.FromObject(new Dictionary<string, object>());
        public JObject Json { get => _Json; set { if (_Json != value) NotifyPropertyChanged(ref _Json, value); } }

        string _FileName = null;
        public string FileName { get => _FileName; set { if (_FileName != value) NotifyPropertyChanged(ref _FileName, value); } }

        public bool IsChanged() => Json.ToString() != JsonString;
    }
}
