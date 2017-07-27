using System;
using System.Collections.Generic;
using System.Linq;
using Directory.BL.Models;
using Directory.DAL;
using Directory.DAL.Entities;

namespace Directory.BL.Repositories
{
    public class AddressRepository
    {
        private Mapper _mapper = new Mapper();

        public List<AddressDetailModel> GetAllByPerson(Guid personId)
        {
            using (var directoryDbContext = new DirectoryDbContext())
            {
                return directoryDbContext.Adresses
                    .Where(a => a.Person.Id == personId)
                    .Select(_mapper.MapEntityToDetailModel)
                    .ToList();
            }
        }

        public void Insert(AddressDetailModel model)
        {
            using (var directoryDbContext = new DirectoryDbContext())
            {
                var person = directoryDbContext.Persons.First(p => p.Id == model.PersonId);
                var entity = _mapper.MapDetailModelToEntity(model, person);
                entity.Id = Guid.NewGuid();

                directoryDbContext.Adresses.Add(entity);
                directoryDbContext.SaveChanges();
            }
        }

        public void Remove(Guid addressId, Guid personId)
        {
            using (var directoryDbContext = new DirectoryDbContext())
            {
                var person = directoryDbContext.Persons.First(p => p.Id == personId);
                var entity = new Address() { Id = addressId, Person = person};
                directoryDbContext.Adresses.Attach(entity);

                directoryDbContext.Adresses.Remove(entity);
                directoryDbContext.SaveChanges();
            }
        }

        public AddressDetailModel Find(Guid addressId)
        {
            using (var directoryDbContext = new DirectoryDbContext())
            {
                var address = directoryDbContext.Adresses
                    .FirstOrDefault(p => p.Id == addressId);
                return address == null ? null : _mapper.MapEntityToDetailModel(address);
            }
        }

        public void Update(AddressDetailModel address)
        {
            using (var directoryDbContext = new DirectoryDbContext())
            {
                var entity = directoryDbContext.Adresses.First(p => p.Id == address.Id);

                entity.City = address.City;
                entity.Street = address.Street;
                entity.ZipCode = address.ZipCode;

                directoryDbContext.SaveChanges();
            }
        }
    }
}