#region Namespace
using System.Collections.Generic;
#endregion
namespace pfba.sales.crm.creation.Models.Context
{
	/// <summary>
	/// Represents the database context for the application.
	/// </summary>
	public class DBContext
	{
		/// <summary>
		/// Gets or sets the default connection string.
		/// </summary>
        public string DefaultConnection { get; set; }
    }
}
