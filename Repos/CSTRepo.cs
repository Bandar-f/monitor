using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;
using api.Data;


namespace api.Repos
{
    public class CSTRepo: ICSTRepo
    {

         private readonly ApplicationDBContext _context;

         public CSTRepo(ApplicationDBContext context)
         {

            _context=context;
            
         }

        public async Task<List<Cst>> GetAllAsync(){


            return await _context.Cst.ToListAsync();

        }
        public async Task<Cst> GetLastAsync(){

            var Cst=_context.Cst.AsQueryable();
            Cst=Cst.OrderByDescending(m=>m.timestamps);

            var last=await Cst.ToListAsync();
            
            
            
            return last[0];

        }

        public async Task<Cst> CreateAsync(Cst CSTItem){

            await _context.Cst.AddAsync(CSTItem);
            await _context.SaveChangesAsync();

            return CSTItem;



        }
        
    }
}