#region Namespace
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using pfba.sales.crm.creation.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
#endregion
namespace pfba.sales.crm.creation.Middlewares
{	
	/// <summary>
	/// Middleware for handling incoming HTTP requests.
	/// </summary>
	public class RequestMiddleware
	{
		/// <summary>
		/// Represents a middleware that handles incoming requests.
		/// </summary>
		private readonly RequestDelegate _next;
		/// <summary>
		/// Indicates whether logging is enabled or not.
		/// </summary>
		private string isLoggingEnabled = ApplicationConstant.N;
		/// <summary>
		/// Initializes a new instance of the <see cref="RequestMiddleware"/> class.
		/// </summary>
		/// <param name="next">The request delegate.</param>
		/// <param name="logger">The logger.</param>
		/// <param name="configuration">The configuration.</param>
		public RequestMiddleware(RequestDelegate next, IConfiguration configuration)
		{
			_next = next;
			isLoggingEnabled = configuration.GetValue<string>(ApplicationConstant.IsLoggingEnabledConfigName);
		}
		/// <summary>
		/// Represents an asynchronous operation that handles the incoming HTTP request.
		/// </summary>
		/// <param name="context">The HttpContext representing the current HTTP request.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task Invoke(HttpContext context)
		{
			try
			{
				if (!Utility.IsNullOrEmpty(isLoggingEnabled) && Utility.IsEqual(ApplicationConstant.Y, isLoggingEnabled))
				{
					context.Request.EnableBuffering();
					var builder = new StringBuilder();
					using var reader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: ApplicationConstant.False, leaveOpen: ApplicationConstant.True);
					context.Request.Body.Position = 0;
				}
				await _next(context);
			}
			catch (Exception ex)
			{
				LogEventInfo eventInfo = new LogEventInfo(NLog.LogLevel.Error, "CreationLog", ex.Message);
				eventInfo.Properties["callerId"] = context.Request.Headers["CallerID"];
				throw;
			}
		}

	}
}
