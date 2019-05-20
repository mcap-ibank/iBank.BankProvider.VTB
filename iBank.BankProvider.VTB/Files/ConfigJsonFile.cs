using System;
using System.Net;
using iBank.Core.Files;
using iBank.Core.Folders;

using PCLExt.FileStorage;

namespace iBank.BankProvider.VTB.Files
{
    public class ConfigJsonFile : BaseConfigJsonFile
    {
        #region Bank Provider

        private string _DSN = "iBank_BankProvider_VTB";
        public string DSN { get => _DSN; set => SetValueIfChangedAndSave(ref _DSN, value); }

        private ushort _Bank_Provider_Port = 15565;
        public ushort Bank_Provider_Port { get => _Bank_Provider_Port; set => SetValueIfChangedAndSave(ref _Bank_Provider_Port, value); }

        #endregion

        public ConfigJsonFile() : base(new ConfigFolder().CreateFile("iBank.BankProvider.VTB.json", CreationCollisionOption.OpenIfExists)) { }

        public string GetBankProviderConnectionString()
        {
            return $"dsn={DSN}";
        }

        public BankProviderServer GetServer() => new BankProviderServer(IPAddress.Any, Bank_Provider_Port);
    }
}
