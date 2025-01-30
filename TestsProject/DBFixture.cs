using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsProject
{
    public class DBFixture
    {
        public ShopApiContext Context { get; set; }  
        public DBFixture() 
        {
            var options = new DbContextOptionsBuilder<ShopApiContext>()
                .UseSqlServer("Server=TZIPPY53\\SQLEXPRESS; Database=Tests; Trusted_Connection=True; TrustServerCertificate=True")
                .Options;
            Context = new ShopApiContext(options);
            Context.Database.EnsureCreated();

        }  

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
