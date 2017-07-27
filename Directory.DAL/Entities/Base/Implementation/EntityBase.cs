using System;
using Directory.DAL.Entities.Base.Interface;

namespace Directory.DAL.Entities.Base.Implementation
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}