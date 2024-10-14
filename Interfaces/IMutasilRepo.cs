using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;

namespace api.Interfaces
{
    public interface IMutasilRepo
    {
        Task<List<Mutasil>> GetAllAsync();
        Task<Mutasil> GetLastAsync();

        Task<Mutasil> CreateAsync(Mutasil MutasilItem);

    }
}