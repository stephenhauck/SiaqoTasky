using System;
using System.Collections.Generic;
using System.Windows.Input;
using SiaqoTasky.Models;
using Xamarin.Forms;

namespace SiaqoTasky.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        public string Title { get; } = "Task details";
        public TaskItem task { get; set; }
        public DetailViewModel(Page page, TaskItem selectedTask) : base(page)
        {
            task = selectedTask;
            task.IsChanged = false;
        }

		public ICommand DisappearingCommand
		{
			get
			{
				return new Command(() =>
				{
                    if (task.IsChanged) new SiaqoDbManager().SaveTaskItems(new List<TaskItem>() { task });

				});
			}
		}

	}
}
