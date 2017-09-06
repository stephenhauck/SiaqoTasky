using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SiaqoTasky.Models;
using Sqo;
using Sqo.Transactions;
using Xamarin.Forms;

namespace SiaqoTasky
{
    public class SiaqoDbManager
    {

        private ISiaqodb siaqodb = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SiaqodbMVVM.SiaqoDbManager"/> class.
        /// </summary>
        /// <param name="database">Database.</param>
        public SiaqoDbManager(string database = "DEFAULTDB")
        {
            siaqodb = DependencyService.Get<ISiaqo>().GetInstance(database);

            //Getting the count of a type in the database..
            if (siaqodb.LoadAll<TaskItem>().Count() < 1)
            {
                LoadInitialData();
            }
        }

        /// <summary>
        /// Loads the initial data.
        /// </summary>
        private void LoadInitialData()
        {
            //Insert some default data into the system

            var tmp = new List<TaskItem>();
            tmp.Add(new TaskItem() { TaskName = "Example task 1", TaskNotes = "Notes for task 1", TaskComplete = false});
            tmp.Add(new TaskItem() { TaskName = "Example task 2", TaskNotes = "Notes for task 2", TaskComplete = false });
            tmp.Add(new TaskItem() { TaskName = "Example task 3", TaskNotes = "Notes for task 3", TaskComplete = false });
            tmp.Add(new TaskItem() { TaskName = "Example task 4", TaskNotes = "Notes for task 4", TaskComplete = false });
            tmp.Add(new TaskItem() { TaskName = "Example task 5", TaskNotes = "Notes for task 5", TaskComplete = false });
            tmp.Add(new TaskItem() { TaskName = "Example task 6", TaskNotes = "Notes for task 6", TaskComplete = false });
            tmp.Add(new TaskItem() { TaskName = "Example task 7", TaskNotes = "Notes for task 7", TaskComplete = false });
            tmp.Add(new TaskItem() { TaskName = "Example task 8", TaskNotes = "Notes for task 8", TaskComplete = false });
            tmp.Add(new TaskItem() { TaskName = "Example task 9", TaskNotes = "Notes for task 9", TaskComplete = false });
            SaveTaskItems(tmp);

        }

        /// <summary>
        /// Resets the database.
        /// </summary>
        public void ResetDatabase()
        {
            siaqodb.DropType<TaskItem>();
            siaqodb.DropType<ErrorLogEntry>();
        }

        /// <summary>
        /// Retrieving the task by OID
        /// </summary>
        /// <returns>The task.</returns>
        /// <param name="oid">Oid.</param>
        public TaskItem GetTask(int oid)
        {
            try
            {
                return siaqodb.LoadObjectByOID<TaskItem>(oid);
            }
            catch (Exception ex)
            {
                siaqodb.StoreObject(new ErrorLogEntry() { ErrorText = ex.Message });
                return null;
            }

        }

        /// <summary>
        /// Gets all the tasks.
        /// </summary>
        /// <returns>The tasks.</returns>
        public List<TaskItem> GetTasks()
        {

            var tmp = siaqodb.LoadAll<TaskItem>().ToList();
            return tmp;
			
		}

        /// <summary>
        /// Saves one or more task items.
        /// </summary>
        /// <param name="tasks">Tasks.</param>
        public void SaveTaskItems(List<TaskItem> tasks)
        {
            ITransaction transaction = null;

            try
            {
                transaction = siaqodb.BeginTransaction();
                foreach (var item in tasks)
                {
                    siaqodb.StoreObject(item,transaction);
                }
                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                siaqodb.StoreObject(new ErrorLogEntry(){ErrorText = ex.Message});

            }

        }

        /// <summary>
        /// Deletes the task.
        /// </summary>
        /// <param name="task">Task.</param>
        public void DeleteTask(TaskItem task)
        {
            try
            {
                siaqodb.Delete(task);
            }
            catch (Exception ex)
            {
                siaqodb.StoreObject(new ErrorLogEntry() { ErrorText = ex.Message });
            }

        }


		/// <summary>
		/// Wite an error to the database to review later if necessary
		/// </summary>
		/// <param name="when"></param>
		/// <param name="error"></param>
		/// <param name="errorLevel"></param>
		public void WriteError(String error, String errorLevel = "Info")
		{
			try
			{
				siaqodb.StoreObject(new ErrorLogEntry() { TimeOfError = DateTime.Now, ErrorLevel = errorLevel, ErrorText = error });
			}
			catch (Exception ex)
			{
				if (Debugger.IsAttached)
				{
					Debug.WriteLine(ex.Message);
				}
			}
		}

	}
}
