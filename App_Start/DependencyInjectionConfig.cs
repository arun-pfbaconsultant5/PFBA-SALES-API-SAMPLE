#region Namespace
using pfba.sales.crm.creation.Business;
using pfba.sales.crm.creation.Interfaces;
using Microsoft.Extensions.DependencyInjection;
#endregion
namespace pfba.sales.crm.creation.App_Start
{
	public class DependencyInjectionConfig
	{
		public static void AddScope(IServiceCollection services)
		{
			services.AddHealthChecks();
			services.AddHttpClient();
			services.AddMemoryCache();
		}
	}
}
