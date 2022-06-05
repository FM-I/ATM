using ATM.Application.Interfaces;
using ATM.Domain.Models;

namespace ATM.Application.Data
{
    public static class Repository
    {
        public static List<Account> Cards { get; } = new()
            {
                new Account { Number = "4149-4991-1234-3421", Password = "1234", Balance = 2000 },
                new Account { Number = "4149-4991-3561-5421", Password = "4321", Balance = 21.51m },
                new Account { Number = "4149-4991-1236-6521", Password = "2341", Balance = 95.2m },
                new Account { Number = "4149-4991-1572-1363", Password = "4123", Balance = 21002.2m },
                new Account { Number = "4149-4991-5412-6421", Password = "1324", Balance = 184.1m },
                new Account { Number = "4149-4991-4321-1234", Password = "3141", Balance = 17535.28m },
            };

        public static List<Bank> Banks => new()
        {
            new() { 
                Name = "Банк 1", 
                AutomatedTellerMachines = new()
                {
                    new() { Name= "Банкомат 1", Cash = 5000, Id = Guid.NewGuid()},
                    new() { Name= "Банкомат 2", Cash = 3000, Id = Guid.NewGuid()},
                    new() { Name= "Банкомат 3", Cash = 7000, Id = Guid.NewGuid()},
                    new() { Name= "Банкомат 4", Cash = 15000, Id = Guid.NewGuid()}
                } 
            },
            new() {
                Name = "Банк 2",
                AutomatedTellerMachines = new()
                {
                    new() { Name= "Банкомат 1", Cash = 1000, Id = Guid.NewGuid()},
                    new() { Name= "Банкомат 2", Cash = 2500, Id = Guid.NewGuid()},
                    new() { Name= "Банкомат 3", Cash = 6000, Id = Guid.NewGuid()},
                    new() { Name= "Банкомат 4", Cash = 5000, Id = Guid.NewGuid()}
                }
            },
            new() {
                Name = "Банк 3",
                AutomatedTellerMachines = new()
                {
                    new() { Name= "Банкомат 1", Cash = 9000, Id = Guid.NewGuid()},
                    new() { Name= "Банкомат 2", Cash = 1000, Id = Guid.NewGuid()},
                    new() { Name= "Банкомат 3", Cash = 3000, Id = Guid.NewGuid()},
                    new() { Name= "Банкомат 4", Cash = 75000, Id = Guid.NewGuid()}
                }
            }
        };
    }
}
