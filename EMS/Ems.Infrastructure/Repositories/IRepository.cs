using System;
using System.Collections.Generic;
using System.Text;

namespace Ems.Infrastructure.Repositories
{
    public interface IRepository<T> where T : ICloneable, IComparable<T>
    {
        void Add(T item);
        bool Remove(T item);
        void Sort();
        public List<T> GetAll();
        int Count { get; }
    }
}
