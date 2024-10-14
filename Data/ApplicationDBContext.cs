using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext: DbContext
    {

        public ApplicationDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {


            
        }

        public DbSet<Mutasil> Mutasil{get;set;}
        public DbSet<Cst> Cst{get;set;}
        public DbSet<ManssahTech> ManssahTech{get;set;}





        
    }
}