using Pervasive.Data.SqlClient;
using Safricom.Data.Domain;
using Safricom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Safricom.Data
{
    public class PastelRepository: IClientRepository
    {
        public void CreateClient(Client newClient)
        {
            using (PsqlConnection pastelConnection = new PsqlConnection(Connect.sPastelConnStr))
            {
                string sqlPastel = customerMasterSQL(newClient);
                executePastelQuery(pastelConnection, sqlPastel);
                
            }

            using (PsqlConnection liquidConnection = new PsqlConnection(Connect.sConnStr))
            {
                var sqlLiquid = liquidCnSQL(newClient);
                executeLiquidQuery(liquidConnection, sqlLiquid);
                
            }
        }

        private string liquidCnSQL(Client newClient)
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
                        '" + newClient.CustomerCode + @"'
                    )
                ";
            return sqlLiquid;
        }
        private string customerMasterSQL(Client newClient)
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
                        '" + newClient.CustomerCode + @"',
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
            throw new NotImplementedException();
        }

        public bool CheckIfClientExists(Client client)
        {
            return false;
        }
        private void executePastelQuery(PsqlConnection pastelConnection, string sqlPastel)
        {
            try
            {
                Connect.getDataCommand(sqlPastel, pastelConnection).ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
        }
        private void executeLiquidQuery(PsqlConnection liquidConnection, string sqlLiquid)
        {
            try
            {
                Connect.getDataCommand(sqlLiquid, liquidConnection).ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
        }
    }
}
