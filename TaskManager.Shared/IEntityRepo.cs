using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Shared
{
    public interface IEntityRepo<T> where T : class
    {
        Task<IList<T>> GetAll();
        Task<T>? Get(string Id);
        Task Add(T item);
        void Delete(T item);
        void Update(string Id, T item);
        Task Save();
    }
}