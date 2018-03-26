using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using Domain.Abstract;
using Domain.Entities;

namespace WebUI.Controllers
{
    public class ContactController : Controller
    {
		IShopRepository repository;
		static object fileSync = new object();

		public ContactController(IShopRepository repo)
		{
			repository = repo;
		}

		public ActionResult Index()
        {
			return View(repository.Shops);
        }
    }
}