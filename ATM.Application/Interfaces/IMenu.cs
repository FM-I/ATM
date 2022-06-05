using ATM.Domain.Models;

namespace ATM.Application.Interfaces
{
    public interface IMenu
    {
        void Show();
        void Close();
        void ShowBalance();
        void BalanceAction(Func<AutomatedTellerMachine, Account, decimal, BalanceResult> func);
        void TransferBalance();
        void Authenticate();
    }
}