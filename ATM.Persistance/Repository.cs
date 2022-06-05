using ATM.Application.Interfaces;
using ATM.Domain.Models;

namespace ATM.Persistance
{
    public class Repository : IRepository
    {
        public List<Card> Cards { get; } = new()
            {
                new Card { Number = "4149-4991-1234-3421", Password = "1234", Balance = 2000 },
                new Card { Number = "4149-4991-3561-5421", Password = "4321", Balance = 21.51m },
                new Card { Number = "4149-4991-1236-6521", Password = "2341", Balance = 95.2m },
                new Card { Number = "4149-4991-1572-1363", Password = "4123", Balance = 21002.2m },
                new Card { Number = "4149-4991-5412-6421", Password = "1324", Balance = 184.1m },
                new Card { Number = "4149-4991-4321-1234", Password = "3141", Balance = 17535.28m },
            };
    }
}
