using Sqo;

namespace SiaqoTasky
{
	public interface ISiaqo
	{
        /// <summary>
        /// Gets the database connection
        /// </summary>
        /// <returns>The instance.</returns>
        /// <param name="database">Database.</param>
		ISiaqodb GetInstance(string database= "DEFAULTDB");

        /// <summary>
        /// Closes the instance.
        /// </summary>
        /// <returns><c>true</c>, if instance was closed, <c>false</c> otherwise.</returns>
        /// <param name="database">Database.</param>
        bool CloseInstance(string database = "DEFAULTDB");

        //Close all instances
        void Shutdown();
	}

}