using System;

namespace Directory.DAL.Entities.Base.Interface
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}