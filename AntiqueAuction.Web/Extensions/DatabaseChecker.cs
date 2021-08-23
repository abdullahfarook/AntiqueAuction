using System;
using System.Threading.Tasks;
using AntiqueAuction.Application.Extensions;
using AntiqueAuction.Core.Models;
using AntiqueAuction.Infrastructure;
using AntiqueAuction.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AntiqueAuction.Web.Extensions
{
    public class DatabaseChecker
    {
        public static async Task EnsureDatabaseIsReady(IServiceScope serviceScope)
        {
            var services = serviceScope.ServiceProvider;
            var ssoContext = services.GetRequiredService<AntiqueAuctionDbContext>();

            await ssoContext.Database.MigrateAsync();
            await SeedData(ssoContext);

        }

        public static async Task SeedData(AntiqueAuctionDbContext context)
        {
            var users = GetSeedUsers();
            foreach (var user in users)
            {
                var dbUser = await context.User.FirstOrDefaultAsync(x => x.Id == user.Id);
                if (dbUser is null)
                {
                    context.User.Add(user);
                }
            }
            var items = GetSeedItems();
            for (var i = 0; i < Items.Length-1; i++)
            {
                var dbItem = await context.Items.FirstOrDefaultAsync(x => x.Id == items[i].Id);
                if (dbItem is null)
                    context.Items.Add(items[i]);
            }

            await context.SaveChangesAsync();
        }
        public static User[] GetSeedUsers()
            => new[]
            {
                new User("Abdullah Farooq", "user1", "abc123", 1000,500,
                        new Address("123 Revenue", "NewYork", "Texas", "US", "75888"))
                    .SetProperty("Id", DummyUser.Id),
                new User("Oksana", "user2", "oks123", 1000,500,
                        new Address("123 Revenue", "NewYork", "Texas", "US", "75888"))
                    .SetProperty("Id", Guid.Parse("6A73190F-2440-4B45-AEA8-7792C09969A1"))
            };

        private static readonly Guid[] Items =
        {
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757AA2"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757AA3"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757AA4"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757AA5"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757AA6"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757AA7"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757AA8"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757AA9"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757A10"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757A11"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757A12"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757A13"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757A14"),
            new Guid("0C65E2BA-D974-49D9-98B4-A80036757A15"),
        };
        public static Item[] GetSeedItems()
            => new[]
            {
                new Item("Jug", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(1),
                    "1.png","This is very good Jug and have beautiful design ")
                    .SetProperty("Id",Items[0]),
                new Item("Glass", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(2),
                        "2.png","Good Glass and have beautiful design ")
                    .SetProperty("Id",Items[1]),
                new Item("Cup", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(3),
                        "3.png","Good Cup and have beautiful design ")
                    .SetProperty("Id",Items[2]),
                new Item("Place", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(4),
                        "4.png","Good Place and have beautiful design ")
                    .SetProperty("Id",Items[3]),
                new Item("Table", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(5),
                        "5.png","Good Table and have beautiful design ")
                    .SetProperty("Id",Items[4]),
                new Item("Door", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(6),
                        "6.png","Good Door and have beautiful design ")
                    .SetProperty("Id",Items[5]),
                new Item("Ceiling", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(7),
                        "7.png","Good Ceiling and have beautiful design ")
                    .SetProperty("Id",Items[6]),
                new Item("Laptop", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(8),
                        "8.png","Good laptop and have beautiful design ")
                    .SetProperty("Id",Items[7]),
                new Item("Processor", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(9),
                        "9.png","Good Processor and have beautiful design ")
                    .SetProperty("Id",Items[8]),
                new Item("Chair", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(10),
                        "10.png","Good Chair and have beautiful design ")
                    .SetProperty("Id",Items[9]),
                new Item("AC", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(11),
                        "11.png","Good AC and have beautiful design ")
                    .SetProperty("Id",Items[10]),
                new Item("Hand Free", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(12),
                        "12.png","Good Hand Free and have beautiful design ")
                    .SetProperty("Id",Items[11]),
                new Item("Window", 100, true, DummyUser.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(13),
                        "13.png","Good Window and have beautiful design ")
                    .SetProperty("Id",Items[12]),

            };
    }
}
