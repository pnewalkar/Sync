using Maintel.Icon.Portal.Sync.HighlightAPI.Models;
using System;
using System.Collections.Generic;

namespace Maintel.Icon.Portal.Sync.HighlightAPI.Helpers
{
    /// <summary>
    /// Class responsible for dtat transport object conversion
    /// </summary>
    public static class DTOConvert
    {
        /// <summary>
        /// Converts a HighlightAlertDTO object to the icon portal HighlightAlert object
        /// </summary>
        /// <param name="dto">The HighlightAlertDTO object to convert</param>
        /// <returns>A populated HighlightAlert object</returns>
        public static HighlightAlert GetHighlightAlertFromDTO(HighlightAlertDTO dto)
        {
            HighlightAlert lHighlightAlert = new HighlightAlert();
            lHighlightAlert.DateSent = Convert.ToDateTime(dto.timeStampUtc.ToString());
            lHighlightAlert.Folder = dto.folder.ToString().Replace("?", ">>");
            lHighlightAlert.Location = dto.locationName.ToString();
            lHighlightAlert.WatchName = dto.watchName.ToString();
            lHighlightAlert.WatchTypeName = dto.watchTypeName.ToString();
            lHighlightAlert.Problem = dto.problem.ToString();
            lHighlightAlert.AlertSummary = dto.alertSummary.ToString();
            lHighlightAlert.Description = dto.stabilityIssueDescription.ToString();
            lHighlightAlert.ReferenceText = dto.referenceText.ToString();
            lHighlightAlert.LinkUrl = dto.linkUrl.ToString();
            lHighlightAlert.SiteLinksUp = Convert.ToInt32(dto.siteLinksUp);
            lHighlightAlert.HasStabilityIssue = Convert.ToBoolean(dto.hasStabilityIssue);
            lHighlightAlert.IsBroadbandSpeedAlert = Convert.ToBoolean(dto.isBroadbandSpeedAlert);
            lHighlightAlert.IsWirelessAccessPoint = Convert.ToBoolean(dto.isWirelessAccessPoint);

            return lHighlightAlert;
        }
    }
}
