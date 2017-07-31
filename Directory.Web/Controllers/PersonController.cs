using Directory.BL.Repositories;
using System;
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


            ViewBag.CompanyName = xmldoc.GetElementsByTagName("are:Obchodni_firma").Item(0).InnerText;
         
            string str = xmldoc.GetElementsByTagName("are:Datum_vzniku").Item(0).InnerText;
            string[] date = str.Split('-');
            ViewBag.CreatedOn = date[2] + "." + date[1] + "." + date[0];

            ViewBag.City = xmldoc.GetElementsByTagName("dtt:Nazev_obce").Item(0).InnerText;
            ViewBag.Street = xmldoc.GetElementsByTagName("dtt:Nazev_ulice").Item(0).InnerText;
            ViewBag.HouseNumber = xmldoc.GetElementsByTagName("dtt:Cislo_domovni").Item(0).InnerText;
            ViewBag.ZipCode = xmldoc.GetElementsByTagName("dtt:PSC").Item(0).InnerText;

            return PartialView("_INDetail");
        }
    }
}