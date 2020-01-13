using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoedasCrypt.Models.Context
{
    public class MoedasContext : DbContext
    {
        public DbSet<Moedas> Moedas { get; set; }

        public MoedasContext(DbContextOptions<MoedasContext> opt ) :  base(opt)
        {

        }
    }
}
