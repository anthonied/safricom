using Pervasive.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Safricom.Data.Domain
{
    public class Connect
    {
        public static string sConnStr = "Server Name=Phillip-Laptop;Database Name=LSAFRICOM;Uid=;Pwd=;";
        public static string sPastelConnStr = "Server Name=Phillip-Laptop;Database Name=PSAFRICOM;Uid=;Pwd=;";
        public static PsqlCommand getDataCommand(string sSQL, PsqlConnection conn)
        {
            try
            {
                if (conn == null)
                {
                    try
                    {
                        conn = new PsqlConnection(sConnStr);
                        conn.Open();
                    }
                    catch (PsqlException ex)
                    {
                        throw ex;
                    }
                }
                else if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                PsqlCommand cmdSQL = new PsqlCommand(sSQL, conn);
                return cmdSQL;
            }
            catch
            {
                return null;
            }
        }
    }
}
