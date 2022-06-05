namespace ATM.Domain.Models
{
    public class Bank
    {
        public string Name { get; set; }
        public List<AutomatedTellerMachine> AutomatedTellerMachines { get; set; } = new();
    }
}
