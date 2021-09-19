using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendContext.Domain.Entities;

namespace TrendContext.Domain.Data
{
    public class InitialData
    {
        protected InitialData()
        {
        }

        public  static void AddDefaultData(InMemoryAppContext context)
        {
            var rebeca = new User
            {
                Id = Guid.Parse("4786FC4D-1B0C-4959-8459-0E871B104D3C"),
                CheckingAccountAmount = 234M,
                CPF = "69686332804",
                Name = "Rebeca Carolina Fabiana Peixoto",
            };

            var priscila = new User
            {
                Id = Guid.Parse("08211958-1DB0-48B4-886E-125079D98836"),
                CheckingAccountAmount = 451,
                CPF = "73060084122",
                Name = "Priscila Vanessa Vitória Ferreira",
            };

            context.Users.Add(rebeca);
            context.Users.Add(priscila);

            var petr4 = new Trend
            {
                Id = Guid.Parse("08211958-1DB0-48B4-886E-125079D98836"),
                Symbol = "PETR4",
                CurrentPrice = 28.44M,
            };

            var mglu3 = new Trend
            {
                Id = Guid.Parse("CB1F9520-9DEE-41B4-849F-FACD6D046BF3"),
                Symbol = "MGLU3",
                CurrentPrice = 25.91M,
            };

            var vvar3 = new Trend
            {
                Id = Guid.Parse("3D97A2F8-1D6B-4CA8-96E8-969DA6B64E38"),
                Symbol = "VVAR3",
                CurrentPrice = 25.91M,
            };

            var sanb11 = new Trend
            {
                Id = Guid.Parse("0A490622-F0BC-41B4-9EDB-1F638B76AD11"),
                Symbol = "SANB11",
                CurrentPrice = 40.77M,
            };

            var toro4 = new Trend
            {
                Id = Guid.Parse("D8912C3A-5780-4EF8-A15D-6A2E291B92E7"),
                Symbol = "TORO4",
                CurrentPrice = 115.98M,
            };

            context.Trends.Add(petr4);
            context.Trends.Add(mglu3);
            context.Trends.Add(vvar3);
            context.Trends.Add(sanb11);
            context.Trends.Add(toro4);

            var order1 = new Order
            {
                Id = Guid.Parse("C16D2DB5-A50A-40F0-BD77-F9FBB77D819B"),
                UserId = rebeca.Id,
                TrendId = petr4.Id,
                Amount = 2,
                Total = Order.CalculateTotalOrder(petr4.CurrentPrice, 2),
            };

            var order2 = new Order
            {
                Id = Guid.Parse("DAEC7DD6-E4B2-4034-A297-95D4C5D2A87C"),
                UserId = rebeca.Id,
                TrendId = sanb11.Id,
                Amount = 3,
                Total = Order.CalculateTotalOrder(sanb11.CurrentPrice, 3),
            };

            context.Orders.Add(order1);
            context.Orders.Add(order2);

            context.SaveChanges();
        }
    }
}
