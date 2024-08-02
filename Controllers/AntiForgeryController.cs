#region Namespace
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
#endregion
namespace pfba.sales.crm.creation.Controllers
{
	/// <summary>
	/// Represents a controller for handling anti-forgery tokens for protection.
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AntiForgeryController : ControllerBase
	{
		
		private readonly IAntiforgery _antiforgery;
		public AntiForgeryController(IAntiforgery antiforgery)
		{
			_antiforgery = antiforgery;
			
		}
		/// <summary>
		/// IsAlive - Check API is Up or Down
		/// </summary>
		/// <returns>true if the API is up and running</returns> 
		[HttpGet]
		[ActionName("IsAlive")]
		public ActionResult<bool> IsAlive()
		{
			return true;
		}

		/// <summary>
		/// Retrieves an anti-forgery token.
		/// </summary>
		/// <returns>The anti-forgery token as a string.</returns>
		[HttpGet]
		[ActionName("GetToken")]
		public ActionResult<string> GetToken()
		{
			
			try
			{
				var token = _antiforgery.GetAndStoreTokens(HttpContext);
				var response =  JsonSerializer.Serialize(token.RequestToken);
				return Content(response);
			}
			catch (Exception ex)
			{
				
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}
	
	}
}
