using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Services.Api
{
    public interface IApiService<T>
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<bool> DeleteById(int id);
        
    }
}
