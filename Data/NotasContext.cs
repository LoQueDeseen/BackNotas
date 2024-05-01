using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BackNotas.Data
{
    public class NotasContext: DbContext
    {
        public NotasContext(DbContextOptions<NotasContext> options): base(options){}

    }
}