using Newtonsoft.Json;
using Safricom.Data;
using Safricom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Safricom.Controllers
{
    public class ClientsController : Controller
    {
        public ActionResult ContactDetails()
        {
            return View();
        }

        public JsonResult SaveClient(string clientInfo)
        {
            var client = JsonConvert.DeserializeObject<Client>(clientInfo);

            var repository = new ClientRepository();
            repository.InsertClient(client);

            return new JsonResult { Data = true };
        }
    }
}
