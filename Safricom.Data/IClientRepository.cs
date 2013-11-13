using Safricom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Safricom.Data
{
    public interface IClientRepository
    {
        void CreateClient(Client newClient);
        void UpdateClient(Client newClient);
        bool CheckIfClientExists(Client client);
    }
}
