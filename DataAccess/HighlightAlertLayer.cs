using System;  
using System.Collections.Generic;  
using System.Data;  
using System.Data.SqlClient;  
using Maintel.Icon.Portal.Sync.HighlightAPI.Models;
  
namespace Maintel.Icon.Portal.Sync.HighlightAPI.DataAccess  
{  
    /// <summary>
    /// Database connection layer for Highlight Alert items.
    /// </summary>
    public class HighlightAlertLayer  
    {  
        string connectionString = Startup.GetHighlightConnectionString();

        /// <summary>
        /// Writes the supplied HighlightAlert object to the database
        /// </summary>
        /// <param name="highlightAlert" type="HighlightAlert">The populated HighlightAlert object to create</param>
        /// <returns>True if added correctly</returns>
        public int CreateEmailAlert(HighlightAlert highlightAlert) {
            var rtn = -1;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {  
                    SqlCommand cmd = new SqlCommand("Highlight.WebhookAlerts_INSERT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DateSent", highlightAlert.DateSent);
                    cmd.Parameters.AddWithValue("@AlertSummary", highlightAlert.AlertSummary);
                    cmd.Parameters.AddWithValue("@LinkUrl", highlightAlert.LinkUrl);
                    cmd.Parameters.AddWithValue("@Description", highlightAlert.Description);
                    cmd.Parameters.AddWithValue("@Location", highlightAlert.Location.Replace("?", ">>"));
                    cmd.Parameters.AddWithValue("@Folder", highlightAlert.Folder);
                    cmd.Parameters.AddWithValue("@Problem", highlightAlert.Problem);
                    cmd.Parameters.AddWithValue("@ReferenceText", highlightAlert.ReferenceText);
                    cmd.Parameters.AddWithValue("@SiteLinksUp", highlightAlert.SiteLinksUp);
                    cmd.Parameters.AddWithValue("@WatchName", highlightAlert.WatchName);
                    cmd.Parameters.AddWithValue("@WatchTypeName", highlightAlert.WatchTypeName);
                    cmd.Parameters.AddWithValue("@HasStabilityIssue", highlightAlert.HasStabilityIssue);
                    cmd.Parameters.AddWithValue("@IsBroadbandSpeedAlert", highlightAlert.IsBroadbandSpeedAlert);
                    cmd.Parameters.AddWithValue("@IsWirelessAccessPoint", highlightAlert.IsWirelessAccessPoint);
                    cmd.Parameters.AddWithValue("@Id", 0);
                    cmd.Parameters[14].Direction = ParameterDirection.InputOutput;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    rtn = Convert.ToInt32(cmd.Parameters[14].Value);
                    con.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return rtn;  
        }
    }
}