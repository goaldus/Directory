using Directory.BL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Directory.BL.Models;

namespace Directory.Web.Controllers
{
    public class PersonController : Controller
    {
        private PersonRepository _repository = new PersonRepository();

        // GET: Home
        public ActionResult Index()
        {
            return View(_repository.GetAll());
        }

        public ActionResult Delete(Guid? id)
        {
            if (id.HasValue)
            {
                var person = (Guid) id;
                _repository.Remove(person);
                
            }
            return Redirect("~");

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonDetailModel person)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(person);
                return Redirect("~");
            }

            return View(person);
        }


        public ActionResult Edit(Guid? id)
        {
            if (id.HasValue)
            {
                var personId = (Guid) id;
                var person = _repository.Find(personId);

                if (person == null)
                {
                    return HttpNotFound();
                }
                return View(person);
            }
            return Redirect("~");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonDetailModel person)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(person);
                return Redirect("~");
            }

            return View(person);
        }

        public ActionResult Detail(int id)
        {
            var xmldoc = new XmlDocument();
            xmldoc.Load("http://wwwinfo.mfcr.cz/cgi-bin/ares/darv_std.cgi?ico=" + id);


            XmlNodeList nodeList = xmldoc.GetElementsByTagName("are:Obchodni_firma");
            foreach (XmlNode node in nodeList)
            {
                ViewBag.CompanyName = node.InnerText;
            }

            nodeList = xmldoc.GetElementsByTagName("are:Datum_vzniku");
            string s = "";
            foreach (XmlNode node in nodeList)
            {
                s = node.InnerText;
            }
            string[] date = s.Split('-');
            ViewBag.CreatedOn = date[2] + "." + date[1] + "." + date[0];

            nodeList = xmldoc.GetElementsByTagName("dtt:Nazev_obce");
            foreach (XmlNode node in nodeList)
            {
                ViewBag.City = node.InnerText;
            }

            nodeList = xmldoc.GetElementsByTagName("dtt:Nazev_ulice");
            foreach (XmlNode node in nodeList)
            {
                ViewBag.Street = node.InnerText;
            }

            nodeList = xmldoc.GetElementsByTagName("dtt:Cislo_domovni");
            foreach (XmlNode node in nodeList)
            {
                ViewBag.HouseNumber = node.InnerText;
            }

            nodeList = xmldoc.GetElementsByTagName("dtt:PSC");
            foreach (XmlNode node in nodeList)
            {
                ViewBag.ZipCode = node.InnerText;
            }

            return PartialView("_INDetail");
        }
    }
}