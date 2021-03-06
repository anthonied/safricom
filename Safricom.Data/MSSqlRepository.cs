﻿using log4net;
using Safricom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Safricom.Data
{
    public class MSSqlRepository : IClientRepository
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
            using (var model = new safricomEntities())
            {
                var thisClient = new client();
                createClientBasicInformation(newClient, model, thisClient, PersistanceMethod.Create);
                var clientId = thisClient.Id;

                var installationAddress = new clientaddress();
                createInstallationAddress(newClient, clientId, model, installationAddress, PersistanceMethod.Create);

                var physicalAddress = new clientaddress();
                createPhysicalAddress(newClient, model, clientId, physicalAddress, PersistanceMethod.Create);

                var postalAddress = new clientaddress();
                createPostalAddress(newClient, clientId, model, postalAddress, PersistanceMethod.Create);
            }
        }

        public void UpdateClient(Client newClient)
        {
            using (var model = new safricomEntities())
            {
                var thisClient = updateClientBasicInformation(newClient, model);
                var clientId = thisClient.Id;
                updateInstallationAddress(newClient, model, clientId);
                updatePhysicalAddress(newClient, model, clientId);
                updatePostalAddress(newClient, model, clientId);
            }
        }

        private client updateClientBasicInformation(Client newClient, safricomEntities model)
        {
            var thisClient = (from clt in model.clients
                              where clt.name == newClient.Name
                               && clt.surname == newClient.Surname
                               && clt.id_passport == newClient.IdPassport
                              select clt).FirstOrDefault();

            createClientBasicInformation(newClient, model, thisClient, PersistanceMethod.Update);
            return thisClient;
        }
        private void createClientBasicInformation(Client newClient, safricomEntities model, client thisClient, PersistanceMethod method)
        {
            thisClient.name = newClient.Name;
            thisClient.surname = newClient.Surname;
            thisClient.business_name = newClient.BusinessName;
            thisClient.cell_phone_one = newClient.CellPhoneOne;
            thisClient.email = newClient.Email;
            thisClient.telephone_work = newClient.Telephone;
            thisClient.id_passport = newClient.IdPassport;
            thisClient.vat_registration = newClient.VatNumber;

            if (method == PersistanceMethod.Create)
                model.clients.Add(thisClient);

            if (model.SaveChanges() > 0)
                _log.Debug(String.Format("Successfully created client basic information form '{0} {1}'", thisClient.name, thisClient.surname));
            else
                _log.Warn(String.Format("No changes were made for client basic information of '{0} {1}'", thisClient.name, thisClient.surname));
        }

        private void updateInstallationAddress(Client newClient, safricomEntities model, int clientId)
        {
            var installationAddress = (from add in model.clientaddresses
                                       where add.client_id == clientId
                                       && add.type == "Installation"
                                       select add).FirstOrDefault();

            if (installationAddress != null)
                createInstallationAddress(newClient, clientId, model, installationAddress, PersistanceMethod.Update);
        }
        private void createInstallationAddress(Client newClient, int clientId, safricomEntities model, clientaddress installationAddress, PersistanceMethod method)
        {
            installationAddress.street_number = newClient.InstallationAddress.StreetNumber;
            installationAddress.street = newClient.InstallationAddress.Street;
            installationAddress.city = newClient.InstallationAddress.City;
            installationAddress.suburb = newClient.InstallationAddress.Suburb;
            installationAddress.gps_coordinates = newClient.InstallationAddress.GPS;
            installationAddress.client_id = clientId;
            installationAddress.type = AddressType.Installation.ToString();

            if (method == PersistanceMethod.Create)
                model.clientaddresses.Add(installationAddress);

            if (model.SaveChanges() > 0)
                _log.Debug(String.Format("Successfully created client innstallation address of '{0} {1}'", newClient.Name, newClient.Surname));
            else
                _log.Warn(String.Format("No changes were made for client installation address of '{0} {1}'", newClient.Name, newClient.Surname));
        }

        private void updatePhysicalAddress(Client newClient, safricomEntities model, int clientId)
        {
            var physicalAddress = (from add in model.clientaddresses
                                   where add.client_id == clientId
                                   && add.type == "Physical"
                                   select add).FirstOrDefault();

            if (physicalAddress != null)
                createPhysicalAddress(newClient, model, clientId, physicalAddress, PersistanceMethod.Update);
        }
        private void createPhysicalAddress(Client newClient, safricomEntities model, int clientId, clientaddress physicalAddress, PersistanceMethod method)
        {
            physicalAddress.street_number = newClient.PhysicalAddress.StreetNumber;
            physicalAddress.street = newClient.PhysicalAddress.Street;
            physicalAddress.city = newClient.PhysicalAddress.City;
            physicalAddress.suburb = newClient.PhysicalAddress.Suburb;
            physicalAddress.client_id = clientId;
            physicalAddress.type = AddressType.Physical.ToString();

            if (method == PersistanceMethod.Create)
                model.clientaddresses.Add(physicalAddress);

            if (model.SaveChanges() > 0)
                _log.Debug(String.Format("Successfully created client physical address of '{0} {1}'", newClient.Name, newClient.Surname));
            else
                _log.Warn(String.Format("No changes were made for client physical address of '{0} {1}'", newClient.Name, newClient.Surname));
        }

        private void updatePostalAddress(Client newClient, safricomEntities model, int clientId)
        {
            var postalAddress = (from add in model.clientaddresses
                                 where add.client_id == clientId
                                 && add.type == "Postal"
                                 select add).FirstOrDefault();

            if (postalAddress != null)
                createPostalAddress(newClient, clientId, model, postalAddress, PersistanceMethod.Update);

        }
        private void createPostalAddress(Client newClient, int clientId, safricomEntities model, clientaddress postalAddress, PersistanceMethod method)
        {
            postalAddress.street_number = newClient.PostalAddress.StreetNumber;
            postalAddress.street = newClient.PostalAddress.Street;
            postalAddress.post_box = newClient.PostalAddress.PoBox;
            postalAddress.city = newClient.PostalAddress.City;
            postalAddress.suburb = newClient.PostalAddress.Suburb;
            postalAddress.postal_code = newClient.PostalAddress.PostalCode;
            postalAddress.client_id = clientId;
            postalAddress.type = AddressType.Postal.ToString();

            if (method == PersistanceMethod.Create)
                model.clientaddresses.Add(postalAddress);

            if (model.SaveChanges() > 0)
                _log.Debug(String.Format("Successfully created client postal address of '{0} {1}'", newClient.Name, newClient.Surname));
            else
                _log.Warn(String.Format("No changes were made for client postal address of '{0} {1}'", newClient.Name, newClient.Surname));
        }

        public bool CheckIfClientExists(Client client)
        {
            using (var model = new safricomEntities())
            {
                var clients = (from clt in model.clients
                               where clt.name == client.Name
                               && clt.surname == client.Surname
                               && clt.id_passport == client.IdPassport
                               select clt).ToList();

                return clients.Count > 0;
            }
        }


    }
}
