using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class MutasilRepo: IMutasilRepo
    {


        private readonly ApplicationDBContext _context;


        public MutasilRepo(ApplicationDBContext context)
        {

            _context=context;
            
        }




        public async Task<List<Mutasil>> GetAllAsync(){


        
            return await _context.Mutasil.ToListAsync();


        }
        public async Task<Mutasil> GetLastAsync(){

            var mutasil=_context.Mutasil.AsQueryable();
            mutasil=mutasil.OrderByDescending(m=>m.timestamps);

            var last=await mutasil.ToListAsync();
            
            
            
            return last[0];



        }

        public async Task<Mutasil> CreateAsync(Mutasil MutasilItem){

            await _context.Mutasil.AddAsync(MutasilItem);
            await _context.SaveChangesAsync();

            return MutasilItem;

        }







        
    }
}