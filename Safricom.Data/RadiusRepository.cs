using System;
using System.Collections.Generic;
using System.Linq;
using Safricom.Domain;
using log4net;
using System.Reflection;
using System.Data.Entity.Validation;

namespace Safricom.Data
{
    public class RadiusRepository : IClientRepository
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
            using (var model = new radiusEntities())
            {
                var client = new radcheck()
                {
                    username = newClient.UserName,
                    attribute = "Password",
                    op = ":=",
                    value = newClient.Password
                };
                try
                {
                    model.radchecks.Add(client);
                    if (model.SaveChanges() > 0)
                        _log.Debug(String.Format("Client using username '{0}' was added to the radcheck", newClient.UserName));
                    else
                        _log.Error(String.Format("Client using username '{0}' was not added to the radcheck", newClient.UserName));
                }
                catch (DbEntityValidationException ex)
                {
                    _log.Error("Critical exception creating new Client", ex);
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        _log.Error(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            _log.Error(String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                }
            }   
        }

        public void UpdateClient(Client newClient)
        {
            using (var model = new radiusEntities())
            {
                var client = (from user in model.radchecks
                              where user.username == newClient.UserName
                              && user.attribute == "Password"
                              select user).FirstOrDefault();
                if (client == null)
                {
                    _log.Error(String.Format("Could not find client with username '{0}' to update in radius", newClient.UserName));
                    return;
                }
                client.value = newClient.Password;
                model.SaveChanges();
                _log.Debug(String.Format("Client using username '{0}' was updated in radcheck", newClient.UserName));
            }
        }

        public bool CheckIfClientExists(Client client)
        {
            using (var model = new radiusEntities())
            {
                var clients = (from user in model.radchecks
                               where user.username == client.UserName
                               select user).ToList();
                if (clients.Count == 0)
                {
                    _log.Debug(String.Format("The user '{0}' '{1}' using username '{2}' does not exist in radius", client.Name, client.Surname, client.UserName));
                    return false;
                }
                if (clients.Count > 1)
                    _log.Warn(String.Format("More than one user using username '{0}' was found in radius. Check for data corruption", client.UserName));
                return true;
            }
        }
    }
}
