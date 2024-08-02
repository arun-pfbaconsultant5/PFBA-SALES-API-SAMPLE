namespace pfba.sales.crm.creation.Common
{
	public static class ApplicationConstant
	{
		//Content Types
		public const string ContentTypeJson = "application/json";
		public const string ContentTypeXml = "application/xml";
		public const string ContentTypeFormUrlEncoded = "application/x-www-form-urlencoded";
		public const string ContentTypeTextPlain = "text/plain";
		public const string ContentTypeTextHtml = "text/html";
		public const string ContentTypeTextXml = "text/xml";
		public const string ContentTypeTextJson = "text/json";

		//Security Header names
		public const string XFrameOptions = "X-Frame-Options";
		public const string XFrameOptionsValue = "DENY";
		public const string XContentTypeOptions = "X-Content-Type-Options";
		public const string XContentTypeOptionsValue = "nosniff";
		public const string XContentSecurityPolicy = "Content-Security-Policy";
		public const string XContentSecurityPolicyValue = "default-src 'self'";
		public const string ReferrerPolicy = "Referrer-Policy";
		public const string ReferrerPolicyValue = "no-referrer";
		public const string FeaturePolicy = "Feature-Policy";
		public const string DefaultFeaturePolicy = "Default";
		//XSS protection header
		public const string XXssProtection = "X-XSS-Protection";
		public const string One = "1";

		// Yes(Y) or No(N) in char
		public const string Y = "Y";
		public const string N = "N";
		// Yes(YES) or No(NO) in string
		public const string Yes = "YES";
		public const string No = "NO";

		// True and false in string
		public const bool True = true;
		public const bool False = false;

		public const string XSRFHeaderName = "X-XSRF-TOKEN";
		public const string XSRFCookieName = "XSRF-TOKEN";
		
		// Default connection string 
		public const string ConnectionStringConfigName = "ConnectionStrings";
		public const string DefaultConnectionConfigName = "DefaultConnection";
		public const string StoredProcsConfigName = "StoredProcs";
		public const string IsLoggingEnabledConfigName = "IsLoggingEnabled";

	}
}
