using Microsoft.EntityFrameworkCore;
using Repositories;
using System;

namespace TestsProject
{
    public class DBFixture : IDisposable
    {
        public ShopApiContext Context { get; private set; }

        public DBFixture()
        {
            var options = new DbContextOptionsBuilder<ShopApiContext>()
                .UseSqlServer("Server=SRV2\\PUPILS;Database=TestTest;Trusted_Connection=True;TrustServerCertificate=True")
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