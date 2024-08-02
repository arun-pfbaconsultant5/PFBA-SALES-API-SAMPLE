namespace pfba.sales.crm.creation.Common
{
	public static class Utility
	{
	
		/// <summary>
		/// Check the given string is null or empty
		/// </summary>
		/// <param name="value">value</param>
		/// <returns></returns>
		public static bool IsNullOrEmpty(string value)
		{
			return (value == null || value.Trim().Equals(string.Empty));
		}
		// Compare source and target strings is equal or not
		public static bool IsEqual(string sourceValue, string compareValue)
		{
			bool isEqual = ApplicationConstant.False;
			if(!IsNullOrEmpty(sourceValue) && !IsNullOrEmpty(compareValue))
			{
				sourceValue = sourceValue.ToUpper();
				compareValue = compareValue.ToUpper();
				isEqual = sourceValue.Equals(compareValue);
			}
			return isEqual;
		}
		
	}
}
