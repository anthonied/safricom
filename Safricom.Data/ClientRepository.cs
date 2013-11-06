using Safricom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Safricom.Data
{
    public class ClientRepository
    {
        Client _selectedClient;

        SqlRepository _sqlRepository = new SqlRepository();
        PastelRepository _pastelRepository = new PastelRepository();

        public void InsertClient(Client newClient)
        {
            _selectedClient = newClient;
            insertToSql();
            insertToPastel();
        }

        private void insertToSql()
        {
            if (CheckIfClientExistsInSQL())
                UpdateSqlClient();
            else
                CreateSqlClient();
        }

        public void CreateSqlClient()
        {
            _sqlRepository.CreateClient(_selectedClient);
        }

        public void UpdateSqlClient()
        {
            _sqlRepository.UpdateClient(_selectedClient);
        }
     
        public bool CheckIfClientExistsInSQL()
        {
            return _sqlRepository.CheckIfClientExists(_selectedClient);
        }

        private bool CheckIfClientExistsInPastel()
        {
            return _pastelRepository.CheckIfClientExists(_selectedClient);
        }

        private void insertToPastel()
        {
            if (CheckIfClientExistsInPastel())
                UpdatePastelClient();
            else
                CreatePastelClient();
        }

        private void CreatePastelClient()
        {
            _pastelRepository.CreateClient(_selectedClient);
        }
        private void UpdatePastelClient()
        {
            _pastelRepository.UpdateClient(_selectedClient);
        }
    }
}
