using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackNotas.Models;

namespace BackNotas.Data
{
    public class NotasContext: DbContext
    {
        public NotasContext(DbContextOptions<NotasContext> options): base(options){}

          public DbSet<Category> Categories {get; set;}
          public DbSet<Note> Notes {get; set;}
          public DbSet<User> Users {get; set;}
      
    }

    

    
}