using System;
using System.Collections.Generic;
using SiaqoTasky.ViewModels;

using Xamarin.Forms;

namespace SiaqoTasky.Views
{
    public partial class MainView : ContentPage
    {
		private MainViewModel viewModel;

		public MainView()
		{
			
            InitializeComponent();
			viewModel = new MainViewModel(this);

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
