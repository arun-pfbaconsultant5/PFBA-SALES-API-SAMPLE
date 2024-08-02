namespace pfba.sales.crm.creation.Common
{
	/// <summary>
	/// Represents a collection of stored procedures.
	/// </summary>
	public class StoredProcs
	{
		/// <summary>
		/// Gets or sets the list of stored procedures configurations.
		/// </summary>
        public List<StoredProcConfig> StoredProc { get; set; }
    }
	/// <summary>
	/// Represents a configuration for a stored procedure.
	/// </summary>
	public class StoredProcConfig
	{
		/// <summary>
		/// Gets or sets the key of the stored procedure configuration.
		/// </summary>
        public string Key { get; set; }
		/// <summary>
		/// Gets or sets the value of the stored procedure configuration.
		/// </summary>
		public string Value { get; set; }
    }
}
