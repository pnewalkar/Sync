using System;

namespace Maintel.Icon.Portal.Sync.HighlightAPI.Spec.Models
{
	/// <summary>
	/// The Highlight Authenticate to be used when requesting initial encryption token
	/// </summary>	
	public class HighlightAuthenticate 
	{
		/// <summary>
		/// The highlight partner value
		/// </summary>
		public string partner { get; set; }

		/// <summary>
		/// the email address of the user logging in
		/// </summary>
		public string uid { get; set; }

		/// <summary>
		/// The token value
		/// </summary>
		public string token { get; set; }

		/// <summary>
		/// The cookie value
		/// </summary>
		public string cookie { get; set; }

	}
}