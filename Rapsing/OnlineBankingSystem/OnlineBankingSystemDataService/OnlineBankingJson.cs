using OnlineBankingSystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineBankingSystemDataService
{
    public class OnlineBankingJson : IOnlineBankingDataService
    {
        private List<BankAccount> banking = new List<BankAccount>();

        private string _jsonFileName;

        public OnlineBankingJson()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/OnlineBankingData.json";
            PopulateJsonFile();
        }
        private void PopulateJsonFile()
        {
            RetrieveDataFromJsonFile();

            if (banking.Count <= 0)
            {
                banking.Add(new BankAccount { AccountId = Guid.NewGuid(), Username = "rubie", Password = "rubie1234", Balance = 0 });

                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            using (var outputStream = File.OpenWrite(_jsonFileName))
            {
                JsonSerializer.Serialize<List<BankAccount>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true }), banking);
            }
        }
        private void RetrieveDataFromJsonFile()
        {
            using (var jsonFileReader = File.OpenText(this._jsonFileName))
            {
                this.banking = JsonSerializer.Deserialize<List<BankAccount>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }
        
        public void Add(BankAccount bankaccount)
        {
            RetrieveDataFromJsonFile();

            if (bankaccount.AccountId == Guid.Empty)
                bankaccount.AccountId = Guid.NewGuid();

            banking.Add(bankaccount);
            SaveDataToJsonFile();
        }

        
        public List<BankAccount> GetAllAccounts()
        {
            RetrieveDataFromJsonFile();
            return banking;
        }

        
        public BankAccount? GetByBalance(double balance)
        {
            RetrieveDataFromJsonFile();
            return banking.FirstOrDefault(t => t.Balance == balance);
        }

        
        public void UpdateBalance(BankAccount account)
        {
            RetrieveDataFromJsonFile();

            
            var existing = banking.FirstOrDefault(x => x.AccountId == account.AccountId);

            if (existing != null)
            {
                existing.Balance = account.Balance;
                SaveDataToJsonFile();
            }
            else
            {
                throw new Exception("Account not found in JSON data!");
            }
        }

        
        public BankAccount? GetById(Guid id)
        {
            RetrieveDataFromJsonFile();
            return banking.FirstOrDefault(x => x.AccountId == id);
        }

        public BankAccount? GetByUsername(string username)
        {
            RetrieveDataFromJsonFile();
            return banking.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public BankAccount? GetBalances(double balance)
        {
            return banking.FirstOrDefault(a => a.Balance == balance);
        }

        public List<BankAccount> GetBalance()
        {
            RetrieveDataFromJsonFile();
            return banking;
        }

    }
}

