using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace SiaqoTasky.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		internal readonly Page page;

		public BaseViewModel(Page page)
		{
			this.page = page;
		}
    }
}
