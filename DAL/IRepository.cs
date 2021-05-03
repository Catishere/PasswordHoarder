using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PasswordHoarder;

namespace PasswordHoarder.DAL
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);

        IList<User> Get(Expression<Func<T, bool>> @where);
        void Insert(T entity);
        void Delete(int id);
        void Update(T entity);
        void Save();
    }
}