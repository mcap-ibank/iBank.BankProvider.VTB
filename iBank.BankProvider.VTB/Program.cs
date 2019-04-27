using iBank.BankProvider.VTB.Files;

namespace iBank.BankProvider.VTB
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Запуск iBank.BankProvider.Console.");
            new ConfigJsonFile().GetServer().Start();
        }
    }
}