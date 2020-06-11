using System;

namespace Maintel.Icon.Portal.Sync.HighlightAPI.Models
{
    /// <summary>
    /// The error object model
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// The request identifier
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Shown request identifier if not empty
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}