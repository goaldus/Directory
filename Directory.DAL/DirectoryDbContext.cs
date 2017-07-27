using System.Data.Entity;
using Directory.DAL.Entities;

namespace Directory.DAL
{
    public class DirectoryDbContext : DbContext
    {
        public IDbSet<Person> Persons { get; set; }
        public IDbSet<Address> Adresses { get; set; }

        public DirectoryDbContext() : base("DirectoryContext")
        {

        }
        
    }
}