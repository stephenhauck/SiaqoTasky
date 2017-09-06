using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SiaqoTasky.Droid;
using Sqo;
using Xamarin.Forms;

[assembly: Dependency(typeof(SiaqodbFactory))]
namespace SiaqoTasky.Droid
{
    public class SiaqodbFactory : ISiaqo
    {

		/// <summary>
		/// The siaqod database variables stord in a dictionary to save time and hopefully memory
		/// </summary>
	    private static readonly Dictionary<string, Siaqodb> SiaqoDatabases = new Dictionary<string, Siaqodb>();


		#region Database management code

		/// <summary>
		/// Gets the database instance or creates one
		/// </summary>
		/// <returns>The instance.</returns>
		/// <param name="database">Database.</param>
		public ISiaqodb GetInstance(string database)
		{
			try
			{
				//Does it exist in the dictionary...
				Siaqodb db;
				if (SiaqoDatabases != null)
				{
					if (SiaqoDatabases.TryGetValue(database, out db)) return db;
				}

				//Did not find it so add it...
				var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), database);
				var config = new Configurator();
				// Siaqodb Starter version allows you to store 100 objects per type
				// To obtain a trial or full license please visit https://www.siaqodb.com
				// If you are using a trial or fully licensed version of Siaqodb, uncomment this line and enter your key
				//Sqo.SiaqodbConfigurator.SetLicense("[Paste your provided license key here...]");
				SiaqodbConfigurator.ApplyConfigurator(config);
				db = new Siaqodb(dbpath);
				//Add the instance to the list
				SiaqoDatabases?.Add(database, db);

				return db;
			}
			catch (Exception ex)
			{
				if (Debugger.IsAttached)
				{
					Debug.WriteLine(ex.Message);
				}
				return null;
			}
		}

		public bool CloseInstance(string database = "DEFAULTDB")
		{
			try
			{

				Siaqodb db;
				if (SiaqoDatabases != null)
				{
					if (SiaqoDatabases.TryGetValue(database, out db))
					{
						db.Close();
						return SiaqoDatabases.Remove(database);
					}
					//Nothing to do 
					return true;
				}
				//Nothing to do
				return true;
			}
			catch (Exception ex)
			{
				if (Debugger.IsAttached)
				{
					Debug.WriteLine(ex.Message);
				}
				return false;
			}
		}


		public void Shutdown()
		{
			try
			{
				if (SiaqoDatabases != null)
				{
					foreach (var database in SiaqoDatabases)
					{
						database.Value.Close();
					}
				}
			}
			catch (Exception ex)
			{
				if (Debugger.IsAttached)
				{
					Debug.WriteLine(ex.Message);
				}
			}
		}

		#endregion

	}

}
