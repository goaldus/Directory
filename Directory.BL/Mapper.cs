using Directory.BL.Models;
using Directory.DAL.Entities;

namespace Directory.BL
{
    public class Mapper
    {
        public PersonDetailModel MapEntityToDetailModel(Person entity)
        {
            return new PersonDetailModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IN = entity.IN,
                TIN = entity.TIN
            };
        }

        public Person MapDetailModelToEntity(PersonDetailModel model)
        {
            return new Person
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IN = model.IN,
                TIN = model.TIN
            };
        }

        public AddressDetailModel MapEntityToDetailModel(Address entity)
        {
            return new AddressDetailModel
            {
                Id = entity.Id,
                City = entity.City,
                Street = entity.Street,
                ZipCode = entity.ZipCode,
                PersonId = entity.Person.Id
            };
        }

        public Address MapDetailModelToEntity(AddressDetailModel model, Person person)
        {
            return new Address
            {
                Id = model.Id,
                City = model.City,
                Street = model.Street,
                ZipCode = model.ZipCode,
                Person = person
            };
        }
    }
}