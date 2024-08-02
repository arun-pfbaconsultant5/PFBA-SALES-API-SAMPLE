#region Namespace
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using pfba.sales.crm.creation.Common;
using Microsoft.Data.SqlClient;
#endregion

namespace pfba.sales.crm.creation.Handlers
{
	/// <summary>
	/// Provides a static class for configuring the exception handler for the application.
	/// </summary>
	public static class ExceptionHandler
	{
		/// <summary>
		/// Indicates whether logging is enabled or not.
		/// </summary>
		private static string isLoggingEnabled = ApplicationConstant.N;

		/// <summary>
		/// Configure the exception handler for the application.
		/// </summary>
		/// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
		/// <param name="logger">The <see cref="ILoggerManager"/> instance.</param>
		/// <param name="configuration">The <see cref="IConfiguration"/> instance.</param>
		public static void ConfigureExceptionHandler(this IApplicationBuilder app, IConfiguration configuration)
		{
			try
			{
				app.UseExceptionHandler(appError =>
				{
					appError.Run(async context =>
					{
						context.Response.ContentType = ApplicationConstant.ContentTypeJson;
						var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
						if (contextFeature != null)
						{
							isLoggingEnabled = configuration.GetValue<string>("IsLoggingEnabled");
							
							NLog.LogEventInfo eventInfo = new NLog.LogEventInfo(NLog.LogLevel.Error, "", "");
							eventInfo.Properties["callerId"] = context.Request.Headers["CallerID"];
							
							if(!Utility.IsNullOrEmpty(isLoggingEnabled) && Utility.IsEqual(ApplicationConstant.Y, isLoggingEnabled))
							{
								using var streamReader = new StreamReader(context.Request.Body);
								streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
								var requestBody = await streamReader.ReadToEndAsync();
								var formattedRequestBody = $"{context.Request.Scheme} {context.Request.Host} {context.Request.Path} {context.Request.QueryString} {requestBody}";
								eventInfo.Properties["requestBody"] = formattedRequestBody;
							}
							eventInfo.Exception = contextFeature.Error;
							

							string errorMessage = (string)GetErrorMessage(contextFeature.Error);

							await context.Response.WriteAsync(new ErrorDetails()
							{
								StatusCode = context.Response.StatusCode,
								Message = errorMessage,
								Severity = "E",
								Process = ".NET CORE Provider PayMethod Service"

							}.ToString());
						}
					});
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// Gets the custom error message based on the provided exception
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <returns>The custom error message</returns>
		private static string GetErrorMessage(Exception exception)
		{
			string errorMessage = string.Empty;
			switch(exception)
			{
				case Exception ex when ex is ArgumentNullException:
					errorMessage = "Invalid request input";
					break;
				case Exception ex when ex is ArgumentException:
					errorMessage = "Invalid request input";
					break;
				case Exception ex when ex is UnauthorizedAccessException:
					errorMessage = "Unauthorized request";
					break;
				case Exception ex when ex is IOException:
					errorMessage = "Input/Output failure";
					break;
				case Exception ex when ex is SystemException:
					errorMessage = "System failure";
					break;
				case Exception ex when ex is ApplicationException:
					errorMessage = "Application failure";
					break;
				case Exception ex when ex is Exception:
					errorMessage = "Techincal service failure";
					break;
				case Exception ex when ex is NotImplementedException:
					errorMessage = "Invalid operation";
					break;
				case Exception ex when ex is AggregateException:
					errorMessage = "Invalid aggregation";
					break;
				case Exception ex when ex is NullReferenceException:
					errorMessage = "Techincal service failure";
					break;
				case Exception ex when ex is UriFormatException:
					errorMessage = "Invalid URI format";
					break;
				//formatexception
				case Exception ex when ex is FormatException:
					errorMessage = "Invalid format";
					break;
				//timeoutexception
				case Exception ex when ex is TimeoutException:
					errorMessage = "Operation timeout";
					break;
				//outofmemoryexception
				case Exception ex when ex is OutOfMemoryException:
					errorMessage = "Running out of memory";
					break;
				//IndexOutOfRangeException
				case Exception ex when ex is IndexOutOfRangeException:
					errorMessage = "Invalid access of range";
					break;
				// Sql Exception
				case Exception ex when ex is SqlException:
					errorMessage = "Infrastructure error";
					break;
				default:
					errorMessage = "Operation failed";
					break;
			}	
			return errorMessage;
		}
	}
}
