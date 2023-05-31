using Microservices.Persistence.Repository.EfCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Stock.Persistence.Repositories.EfCore.Context
{
    public class StockDbContext : CoreDbContext
    {
        public StockDbContext(){
        }
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options){
        }

        public virtual DbSet<Domain.Models.Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
