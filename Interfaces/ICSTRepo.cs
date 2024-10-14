using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;

namespace api.Interfaces
{
    public interface ICSTRepo
    {
         Task<List<Cst>> GetAllAsync();
        Task<Cst> GetLastAsync();

        Task<Cst> CreateAsync(Cst CSTItem);
    }
}