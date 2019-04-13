using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class ShopDBInitializer
    {
        public static void Initialize(ShopDbContext context)
        {
            

            context.Database.EnsureCreated();

            var portal = new Portal[]
            {
             new Portal{ IsActive = true , Language = "en-US" , LocaleId="en-US" , PortalName="en" },
             //new Portal{ IsActive = true , Language = "fa-IR" , LocaleId="fa-IR" , PortalName="fa" }
            };
            if (!context.Portals.Any())
               foreach (Portal s in portal)
               {
                   context.Portals.Add(s);
               }

            context.SaveChanges();

        }
    }
}
