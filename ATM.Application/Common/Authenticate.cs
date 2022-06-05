using ATM.Application.Data;
using ATM.Application.Interfaces;
using ATM.Domain.Models;

namespace ATM.Application.Common
{
    public class Authenticate : IAuthenticate
    {
        public event Action<string> OnAuth;

        public Account Auth(string cardNumber, string password)
        {
            var account = Repository.Cards.Find(c => c.Number == cardNumber && c.Password == password);

            if(account == null)
            {
                OnAuth?.Invoke("Введені невірні данні, аутентифікація не пройдена!");
            }
            
            OnAuth?.Invoke("Введені вірні данні, аутентифікація пройдена!");

            return account;
        }
    }
}
