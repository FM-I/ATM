using ATM.Domain.Models;

namespace ATM.Application.Interfaces
{
    public interface IAuthenticate
    {
        event Action<string> OnAuth;
        Account Auth(string cardNumber, string password); 
    }
}
