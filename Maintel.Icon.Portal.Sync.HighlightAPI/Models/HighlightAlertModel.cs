using System;

namespace Maintel.Icon.Portal.Sync.HighlightAPI.Models
{
	/// <summary>
	/// The Highlight Alert object returned from the webhook
	/// </summary>	
	/// <remarks>For details see: https://hlm4.highlighter.net/help/Status/alerting_webhooks</remarks>
	public class HighlightAlertDTO {

		/// <summary>
		/// The broadband downstream speed in Kbps
		/// </summary>
		public int? broadbandDownstreamSpeedKbps { get; set; }

		/// <summary>
		/// The broadband alert threshold in Kbps
		/// </summary>
		public int? broadbandAlertThresholdKbps { get; set; }

		/// <summary>
		/// The device address
		/// </summary>
		public string deviceAddress { get; set; }

		/// <summary>
		/// The alert folder
		/// </summary>
		public string folder { get; set; }

		/// <summary>
		/// If the alert has a stability issue
		/// </summary>
		public Boolean hasStabilityIssue { get; set; }

		/// <summary>
		/// If the alert is a broadband speed alert
		/// </summary>
		public Boolean isBroadbandSpeedAlert { get; set; }

		/// <summary>
		/// If the alert is from a wireless access point
		/// </summary>
		public Boolean isWirelessAccessPoint { get; set; }

		/// <summary>
		/// The link to the highlight alert
		/// </summary>
		public string linkUrl { get; set; }

		/// <summary>
		/// The location name
		/// </summary>
		public string locationName { get; set; }

		/// <summary>
		/// The name of the watch created
		/// </summary>
		public string watchName { get; set; }

		/// <summary>
		/// The alert problem created
		/// </summary>
		public string problem { get; set; }

		/// <summary>
		/// The reference text assoicated with the alert
		/// </summary>
		public string referenceText { get; set; }

		/// <summary>
		/// The numer of site links up
		/// </summary>
		public int? siteLinksUp { get; set; }

		/// <summary>
		/// The stability issue code
		/// </summary>
		public string stabilityIssueCode { get; set; }

		/// <summary>
		/// The description of the stability code 
		/// </summary>
		public string stabilityIssueDescription { get; set; }

		/// <summary>
		/// The alert summary
		/// </summary>
		public string alertSummary { get; set; }

		/// <summary>
		/// The alert wap location
		/// </summary>
		public string wapLocation { get; set; }

		/// <summary>
		/// The alert wap serial number
		/// </summary>
		public string wapSerialNumber { get; set; }

		/// <summary>
		/// The type of the watch
		/// </summary>
		public string watchTypeName { get; set; }

		/// <summary>
		/// The date and time of the alert being sent
		/// </summary>
		public DateTime timeStampUtc { get; set; }

	}


	/// <summary>
	/// The Highlight Alert object stored in the portal database
	/// </summary>	
		public class HighlightAlert {
		

		/// <summary>
		/// The date and time that the alert was sent
		/// </summary>	
	    public DateTime DateSent{ get; set; }
		/// <summary>
		/// The summary information of the alert
		/// </summary>
        public string AlertSummary { get; set; }
		/// <summary>
		/// The watch name associated with the alert
		/// </summary>
        public string WatchName { get; set; }
		/// <summary>
		/// The type of the watch associated with the alert
		/// </summary>
        public string WatchTypeName { get; set; }
		/// <summary>
		/// The folder associated with the alert
		/// </summary>
        public string Folder { get; set; }
		/// <summary>
		/// The location associated with the alert
		/// </summary>
        public string Location { get; set; }
		/// <summary>
		/// The Reference Text associated with the alert
		/// </summary>
        public string ReferenceText { get; set; }
		/// <summary>
		/// The link to the highlight alert
		/// </summary>
		public string LinkUrl { get; set; }
		/// <summary>
		/// If the alert has a stability issue
		/// </summary>
		public Boolean HasStabilityIssue { get; set; }

		/// <summary>
		/// If the alert is a broadband speed alert
		/// </summary>
		public Boolean IsBroadbandSpeedAlert { get; set; }

		/// <summary>
		/// If the alert is from a wireless access point
		/// </summary>
		public Boolean IsWirelessAccessPoint { get; set; }

		/// <summary>
		/// The link to the highlight alert
		/// </summary>
		public string linkUrl { get; set; }
		/// <summary>
		/// The alert problem
		/// </summary>
        public string Problem { get; set; }
		/// <summary>
		/// The description of the stability issue
		/// </summary>
        public string Description { get; set; }
		/// <summary>
		/// The number of site links up
		/// </summary>
        public int SiteLinksUp { get; set; }
	}

}