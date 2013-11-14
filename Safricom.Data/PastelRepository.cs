using log4net;
using Pervasive.Data.SqlClient;
using Safricom.Data.Domain;
using Safricom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Safricom.Data
{
    public class PastelRepository: IClientRepository
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void CreateOrUpdateClient(Client newClient)
        {
            if (CheckIfClientExists(newClient))
                UpdateClient(newClient);
            else
                CreateClient(newClient);
        }

        public void CreateClient(Client newClient)
        {
            using (PsqlConnection pastelConnection = new PsqlConnection(Connect.sPastelConnStr))
            {
                string sqlPastel = createCustomerMasterSQL(newClient);
                executePastelQuery(pastelConnection, sqlPastel);
            }

            using (PsqlConnection liquidConnection = new PsqlConnection(Connect.sConnStr))
            {
                var sqlLiquid = createLiquidCNSQL(newClient);
                executeLiquidQuery(liquidConnection, sqlLiquid);
            }
        }

        private string createLiquidCNSQL(Client newClient)
        {
            var sqlLiquid = @"
                INSERT into SOLCN
                    (
                        IDNumber,
                        CustomerCode
                    )
                VALUES
                    (
                        '" + newClient.IdPassport + @"',
                        '" + newClient.UserName + @"'
                    )
                ";
            return sqlLiquid;
        }

        private string updateLiquidCNSQL(Client newClient)
        {
            var sqlLiquid = @"
                UPDATE SOLCN
                SET
                        IDNumber = '" + newClient.IdPassport + @"'
                WHERE 
                        CustomerCode = '" + newClient.UserName + @"'
                ";
            return sqlLiquid;
        }
        private string createCustomerMasterSQL(Client newClient)
        {
            var sql = @"
                INSERT into CustomerMaster
                    (
                        CustomerCode,
                        CustomerDesc,
                        PostAddress01,
                        PostAddress02,
                        PostAddress03,
                        PostAddress04,
                        PostAddress05,
                        TaxCode,
                        CreateDate
                    )
                VALUES
                    (
                        '" + newClient.UserName + @"',
                        '" + newClient.Name + " " + newClient.Surname + @"',
                        '" + newClient.PostalAddress.StreetNumber + @"',
                        '" + newClient.PostalAddress.Street + @"',
                        '" + newClient.PostalAddress.PoBox + @"',
                        '" + newClient.PostalAddress.PostalCode + @"',
                        '" + newClient.PostalAddress.City + @"',
                        " + newClient.VatNumber + @",
                        '" + DateTime.Now.ToString("yyyy-MM-dd") + @"'
                    )
            ";
            return sql;
        }

        public void UpdateClient(Client newClient)
        {
            using (PsqlConnection pastelConnection = new PsqlConnection(Connect.sPastelConnStr))
            {
                string sqlPastel = updateCustomerMasterSQL(newClient);
                executePastelQuery(pastelConnection, sqlPastel);
            }

            using (PsqlConnection liquidConnection = new PsqlConnection(Connect.sConnStr))
            {
                var sqlLiquid = updateLiquidCNSQL(newClient);
                executeLiquidQuery(liquidConnection, sqlLiquid);
            }

        }

        private string updateCustomerMasterSQL(Client newClient)
        {
            var sql = @"
                    UPDATE CustomerMaster 
                    SET
                            CustomerDesc = '" + newClient.Name + " " + newClient.Surname + @"',
                            PostAddress01 = '" + newClient.PostalAddress.StreetNumber + @"',
                            PostAddress02 = '" + newClient.PostalAddress.Street + @"',
                            PostAddress03 = '" + newClient.PostalAddress.PoBox + @"',
                            PostAddress04 = '" + newClient.PostalAddress.PostalCode + @"',
                            PostAddress05 = '" + newClient.PostalAddress.City + @"',
                            TaxCode = " + newClient.VatNumber + @",
                            CreateDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + @"'
                    WHERE 
                            CustomerCode = '" + newClient.UserName + @"'
                ";
            return sql;
        }
        public bool CheckIfClientExists(Client client)
        {
            using (PsqlConnection pastelConnection = new PsqlConnection(Connect.sPastelConnStr))
            {
                string sqlPastel = String.Format("SELECT COUNT(*) FROM CustomerMaster WHERE CustomerMaster.CustomerCode = '{0}'", client.UserName);
                int count = (int)Connect.getDataCommand(sqlPastel, pastelConnection).ExecuteScalar();
                
                return count > 0;
            }
        }
        private void executePastelQuery(PsqlConnection pastelConnection, string sqlPastel)
        {
            try
            {
                Connect.getDataCommand(sqlPastel, pastelConnection).ExecuteNonQuery();
                _log.Debug(String.Format("Successfully executed query '{0}'", sqlPastel));
            }
            catch (Exception ex)
            {
                _log.Error(String.Format("Could not execute pastel query '{0}'", sqlPastel), ex);
            }
        }
        private void executeLiquidQuery(PsqlConnection liquidConnection, string sqlLiquid)
        {
            try
            {
                Connect.getDataCommand(sqlLiquid, liquidConnection).ExecuteNonQuery();
                _log.Debug(String.Format("Successfully executed query '{0}'", sqlLiquid));
            }
            catch (Exception ex)
            {
                _log.Error(String.Format("Could not execute liquid query '{0}'", sqlLiquid), ex);
            }
        }
    }
}
