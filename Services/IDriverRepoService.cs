using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApp.Model;

namespace TaxiApp.Services
{
    public interface IDriverRepoService<T>
    {
        void Add(T entity);
        void AddList(List<T> entities);
        void Update(T entity);
        List<T> GetAll();
    }
}
