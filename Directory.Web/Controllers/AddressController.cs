using System;
using System.Web.Mvc;
using Directory.BL.Models;
using Directory.BL.Repositories;

namespace Directory.Web.Controllers
{
    public class AddressController : Controller
    {
        private AddressRepository _repository = new AddressRepository();

        public ActionResult Detail(Guid id)
        {
            ViewBag.PersonId = id;
            return PartialView("_Detail", _repository.GetAllByPerson(id));
        }

        public ActionResult Create(Guid? id)
        {
            if (id.HasValue)
            {
                PersonRepository _personRepository = new PersonRepository();
                var personId = (Guid)id;
                var person = _personRepository.Find(personId);

                if (person == null)
                {
                    return HttpNotFound();
                }
                ViewBag.PersonId = personId;
                return View();
            }
            return Redirect("~");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddressDetailModel address)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(address);
                return Redirect("~");
            }
            return View(address);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id.HasValue)
            {
                var addressId = (Guid)id;
                var address = _repository.Find(addressId);
                _repository.Remove(addressId, address.PersonId);

            }
            return Redirect("~");
        }

        public ActionResult Edit(Guid? id)
        {
            if (id.HasValue)
            {
                var addressId = (Guid)id;
                var address = _repository.Find(addressId);

                if (address == null)
                {
                    return HttpNotFound();
                }
                return View(address);
            }
            return Redirect("~");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddressDetailModel address)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(address);
                return Redirect("~");
            }
            return View(address);
        }
    }
}