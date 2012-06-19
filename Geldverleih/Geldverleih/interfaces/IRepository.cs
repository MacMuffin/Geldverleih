using System;
using System.Collections.Generic;

namespace Geldverleih.Repository.interfaces
{
    public interface IRepository<T>
    {
        T GetById(Guid id);
        void Save(T objekt);
        void Delete(T objekt);
        IList<T> GetAll();
    }
}