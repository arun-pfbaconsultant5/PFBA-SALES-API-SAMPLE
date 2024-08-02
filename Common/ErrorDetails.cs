#region Namespace
using System.Text.Json;
#endregion
namespace pfba.sales.crm.creation.Common
{
	public class ErrorDetails
	{
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Severity { get; set; }
        public string? Process { get; set; }
		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}

	}
    
}
