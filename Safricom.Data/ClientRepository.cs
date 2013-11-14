using log4net;
using Safricom.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Safricom.Data
{
    public class ClientRepository
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        List<IClientRepository> _repositories = new List<IClientRepository>();
        private bool MsSqlRepoShouldRun
        {
            get
            {
                return ConfigurationManager.AppSettings.AllKeys.Contains("UseMSSqlRepo")
                                && ConfigurationManager.AppSettings["UseMSSqlRepo"] == "true";
            }
        }
        private bool PastelRepoShouldRun
        {
            get
            {
                return ConfigurationManager.AppSettings.AllKeys.Contains("UsePastelRepo")
                                && ConfigurationManager.AppSettings["UsePastelRepo"] == "true";
            }
        }
        private bool RadiusRepoShouldRun
        {
            get
            {
                return ConfigurationManager.AppSettings.AllKeys.Contains("UseRadiusRepo")
                                && ConfigurationManager.AppSettings["UseRadiusRepo"] == "true";
            }
        }

        public ClientRepository()
        {
            if (RadiusRepoShouldRun)
                _repositories.Add(new RadiusRepository());
            if (MsSqlRepoShouldRun)
                _repositories.Add(new MSSqlRepository());
            if (PastelRepoShouldRun)
                _repositories.Add(new PastelRepository());
        }

        public void InsertClient(Client newClient)
        {
            _log.Info(String.Format("Managing client: {0} {1}", newClient.Name, newClient.Surname));
            foreach (var repo in _repositories)
                repo.CreateOrUpdateClient(newClient);
        }
    }
}
