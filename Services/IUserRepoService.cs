using System.Collections.Generic;
using TaxiApp.Model;

namespace TaxiApp.Services
{
    public interface IUserRepoService<T>
    {
        void Add(T entity);
        T Get(string mail);
        void Update(T entity );
    }
}
