using System;
using System.Collections.Generic;
using System.Text;

namespace Ems.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : ICloneable, IComparable<T>
    {
        private List<T> items; //resizing

        public Repository()
        {
            items = new List<T>();
        }

        public int Count => items.Count;

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            items.Add((T)item.Clone());
        }

        public bool Remove(T item)
        {
            if (item == null)
                return false;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].CompareTo(item) == 0)
                {
                    items.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public void Sort()
        {
            items.Sort();
        }

        public List<T> GetAll()
        {
            List<T> result = new List<T>();

            foreach (var item in items)
            {
                result.Add((T)item.Clone());
            }

            return result;
        }

        
    }
}
