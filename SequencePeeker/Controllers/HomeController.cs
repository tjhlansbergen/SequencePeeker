/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  ASP Main controller
/// </summary>

using SequencePeeker.Code;
using SequencePeeker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace SequencePeeker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Properties()
        {
            //check if we have a session
            if(Session["id"] == null) return View("Message", new MessageModel("Your session was lost!, please start all over again..."));

            Guid ID = (Guid)Session["id"];

            if (DataStore.HasData(ID))
                return View("Properties", new PropertiesModel(ID));
            else
                return View("Message", new MessageModel("Your session expired!, please start all over again..."));
        }

        public ActionResult Files()
        {
            //check if we have a session
            if (Session["id"] == null) return View("Message", new MessageModel("Your session was lost!, please start all over again..."));

            Guid ID = (Guid)Session["id"];

            if (DataStore.HasData(ID))
                return View("Files", new FilesModel(ID));
            else
                return View("Message", new MessageModel("Your session expired!, please start all over again..."));
        }

        public ActionResult Registry()
        {
            //check if we have a session
            if (Session["id"] == null) return View("Message", new MessageModel("Your session was lost!, please start all over again..."));

            Guid ID = (Guid)Session["id"];

            if (DataStore.HasData(ID))
                return View("Registry", new RegistryModel(ID));
            else
                return View("Message", new MessageModel("Your session expired!, please start all over again..."));
        }

        public ActionResult Services()
        {
            //check if we have a session
            if (Session["id"] == null) return View("Message", new MessageModel("Your session was lost!, please start all over again..."));

            Guid ID = (Guid)Session["id"];

            if (DataStore.HasData(ID))
                return View("Services", new ServicesModel(ID));
            else
                return View("Message", new MessageModel("Your session expired!, please start all over again..."));
        }

        public ActionResult History()
        {
            //check if we have a session
            if (Session["id"] == null) return View("Message", new MessageModel("Your session was lost!, please start all over again..."));

            Guid ID = (Guid)Session["id"];

            if (DataStore.HasData(ID))
                return View("History", new HistoryModel(ID));
            else
                return View("Message", new MessageModel("Your session expired!, please start all over again..."));
        }

        public ActionResult Shortcuts()
        {
            //check if we have a session 
            if (Session["id"] == null) return View("Message", new MessageModel("Your session was lost!, please start all over again..."));

            Guid ID = (Guid)Session["id"];
            if(DataStore.HasData(ID))
                return View("Shortcuts", new ShortcutsModel(ID));
            else
                return View("Message", new MessageModel("Your session expired!, please start all over again..."));
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string passcode)
        {
            //check for passcode
            if(passcode != System.Configuration.ConfigurationManager.AppSettings["passcode"]) return View("Message", new MessageModel(string.Format("Sorry, that passcode is incorrect.")));

            //first, create some space by clearing cache
            DataStore.ClearCache();

            //create instance of packagefile and attach to session
            PackageFile packagefile = new PackageFile(file);
          
            //get result
            if (!packagefile.IsAppVPackage)
            {
                return View("Message", new MessageModel(string.Format("There was an error inspecting this package: {0}", packagefile.Status)));
            }

            //store session info
            Session["name"] = Path.GetFileName(file.FileName);
            Session["id"] = packagefile.ID;

            //return results
            return View("Properties", new PropertiesModel(packagefile.ID));
        }
    }
}