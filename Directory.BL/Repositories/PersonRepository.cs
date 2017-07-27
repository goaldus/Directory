using System;
using System.Collections.Generic;
using System.Linq;
using Directory.BL.Models;
using Directory.DAL;
using Directory.DAL.Entities;

namespace Directory.BL.Repositories
{
    public class PersonRepository
    {
        private Mapper _mapper = new Mapper();

        public List<PersonDetailModel> GetAll()
        {
            using (var directoryDbContext = new DirectoryDbContext())
            {
                return directoryDbContext.Persons
                    .Select(_mapper.MapEntityToDetailModel)
                    .OrderBy(p => p.FirstName)
                    .ToList();
            }
        }

        public void Insert(PersonDetailModel person)
        {
            using (var directoryDbContext = new DirectoryDbContext())
            {
                var entity = _mapper.MapDetailModelToEntity(person);
                entity.Id = Guid.NewGuid();

                directoryDbContext.Persons.Add(entity);
                directoryDbContext.SaveChanges();
            }
        }

        public void Remove(Guid personId)
        {
            using (var directoryDbContext = new DirectoryDbContext())
            {
                var addresses = directoryDbContext.Adresses.Where(a => a.Person.Id == personId).ToList();
                var entity = new Person() {Id = personId, Adresses = addresses};
                directoryDbContext.Persons.Attach(entity);

                directoryDbContext.Persons.Remove(entity);
                directoryDbContext.SaveChanges();
            }
        }

        public PersonDetailModel Find(Guid personId)
        {
            using (var directoryDbContext = new DirectoryDbContext())
            {
                var person = directoryDbContext.Persons
                    .FirstOrDefault(p => p.Id == personId);

                return person == null ? null : _mapper.MapEntityToDetailModel(person);
            }
        }

        public void Update(PersonDetailModel person)
        {
            using (var directoryDbContext = new DirectoryDbContext())
            {
                var entity = directoryDbContext.Persons.First(p => p.Id == person.Id);

                entity.FirstName = person.FirstName;
                entity.LastName = person.LastName;
                entity.IN = person.IN;
                entity.TIN = person.TIN;

                directoryDbContext.SaveChanges();
            }
        }
    }
}