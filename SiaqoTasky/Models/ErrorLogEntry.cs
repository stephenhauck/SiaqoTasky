using Sqo.Attributes;
using System;
using System.ComponentModel;

namespace SiaqoTasky.Models
{
    public class ErrorLogEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ErrorText;
        public DateTime TimeOfError = DateTime.Now;
        public string ErrorLevel = "Info";
		[Ignore]
		public bool IsChanged { get; set; }
      
    }
}
