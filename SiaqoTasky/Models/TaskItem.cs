using Sqo.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiaqoTasky.Models
{
    
    public class TaskItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string TaskName { get; set; }
        public string TaskNotes { get; set; }
        public bool TaskComplete { get; set; }
        [Ignore]
        public bool IsChanged { get; set; }

    }
}
