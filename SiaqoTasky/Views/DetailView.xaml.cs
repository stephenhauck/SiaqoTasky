using System;
using System.Collections.Generic;
using SiaqoTasky.Models;
using SiaqoTasky.ViewModels;
using Xamarin.Forms;

namespace SiaqoTasky.Views
{
    public partial class DetailView : ContentPage
    {
		private DetailViewModel viewModel;

		public DetailView(TaskItem selectedTask)
		{
			InitializeComponent();
			viewModel = new DetailViewModel(this, selectedTask);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (BindingContext == null)
			{
				BindingContext = viewModel;
			}
		}
    }
}
