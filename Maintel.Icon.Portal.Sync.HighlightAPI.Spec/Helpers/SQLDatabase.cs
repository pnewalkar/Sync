using System;  
using System.Collections.Generic;  
using System.Data;  
using System.Data.SqlClient;  
using System.Linq;  

namespace Maintel.Icon.Portal.Sync.HighlightAPI.Spec.Helpers
{
    public static class SQLDatabase {

        public static bool TestConnection(string connectionString) {
            bool rtn = false;
            using ( SqlConnection con = new SqlConnection(connectionString))  
            {  
                SqlCommand cmd = new SqlCommand("SELECT 'OK'", con);  
                cmd.CommandType = CommandType.Text; 

                con.Open();  
                SqlDataReader rdr = cmd.ExecuteReader(); 
                while (rdr.Read()) {  
                    if(rdr[0].ToString() == "OK") {
                        rtn = true;
                    }
                }
                con.Close();
            }
            return rtn;
        }
        
        public static string ReturnQuery(string connectionString, string sql) {
            var rtn = "";

            using ( SqlConnection con = new SqlConnection(connectionString))  
            {  
                SqlCommand cmd = new SqlCommand(sql, con);  
                cmd.CommandType = CommandType.Text; 

                con.Open();  
                SqlDataReader rdr = cmd.ExecuteReader(); 
                while (rdr.Read())  
                {  
                    rtn += rdr[0].ToString();
                }
                con.Close();
            }
            return rtn;
        }
    }

}

