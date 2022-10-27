using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Infrastructure.Database;
using SimpleWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApi.Tests.Context
{
    internal class ContextFactory
    {
        public static Guid ProvinceId;

        public static DatabaseContext Create()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DatabaseContext(options);
            context.Database.EnsureCreated();
            var Country = new Country("Country 4");
            var Province = new Province("Country 4 Provinve 1", Country.CountryId);
            
            context.Countries.Add(Country);
            context.Provinces.Add(Province);
            context.Users.Add(new Domain.User("login@mail.com", Province.ProvinceId, "pass"));
            context.Users.Add(new Domain.User("login@google.mail.com", Province.ProvinceId, "pass"));
            context.SaveChanges();
            return context;
        }

        public static void Destroy(DatabaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
