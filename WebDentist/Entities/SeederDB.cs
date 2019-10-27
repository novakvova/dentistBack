using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDentist.Entities
{
    public class SeederDB
    {
        private static void SeedCountries(ApplicationDbContext context)
        {
            Country[] countries =
            {
                new Country {
                    Name ="Ukraine",
                    FlagImage ="https://sturm.com.ua/image/cache/catalog/flagi/B9560-620x620.jpg"
                },
                new Country {
                    Name ="Poland",
                    FlagImage ="http://ostarbeiter.vn.ua/img/2011/02/prapor-polska.jpg"
                }
            };
            foreach(var item in countries)
            {
                var country = context.Countries.SingleOrDefault(c => c.Name == item.Name);
                if(country==null)
                {
                    context.Add(item);
                    context.SaveChanges();
                }
            }
        }

        public static void SeedData(IServiceProvider services, IHostingEnvironment env, IConfiguration config)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                SeederDB.SeedCountries(context);
            }
        }

    }
}
