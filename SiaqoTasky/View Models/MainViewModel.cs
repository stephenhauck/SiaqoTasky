using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using SiaqoTasky.Models;
using Xamarin.Forms;

namespace SiaqoTasky.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string Title { get; } = "Tasks";
        public List<TaskItem> tasks { get; set; }
        public TaskItem SelectedTask { get; set; }
        public MainViewModel(Page page) : base(page)
        {

        }


		public ICommand RowTappedCommand
		{
			get
			{
				return new Command(async () =>
				{
					try
					{

						await page.Navigation.PushAsync(new Views.DetailView(SelectedTask));

					}
					catch (Exception ex)
					{
						if (Debugger.IsAttached) Debug.WriteLine(ex.Message);
						new SiaqoDbManager().WriteError(ex.Message, "Error");
					}



				});
			}
		}

		public ICommand AppearingCommand
		{
			get
			{
				return new Command(() =>
				{
					tasks = new SiaqoDbManager().GetTasks();
					if (Debugger.IsAttached) Debug.WriteLine(string.Format("{0} tasks retrieved", tasks.Count));

				});
			}
		}

		public ICommand CreateTaskCommand
		{
			get
			{
				return new Command(async () =>
				{
					try
					{

                        await page.Navigation.PushAsync(new Views.DetailView(new TaskItem()));

					}
					catch (Exception ex)
					{
						if (Debugger.IsAttached) Debug.WriteLine(ex.Message);
						new SiaqoDbManager().WriteError(ex.Message, "Error");
					}

				});
			}
		}
    }
}
