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
                banking.Add(new BankAccount { Balance = 0 });

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
            banking.Add(bankaccount);
            SaveDataToJsonFile();
        }

        public List<BankAccount> GetBalance()
        {
            RetrieveDataFromJsonFile();
            return banking;
        }

        public BankAccount? GetBalances(double balances)
        {
            RetrieveDataFromJsonFile();
            return banking.FirstOrDefault(t => t.Balance == balances);
        }

        public void UpdateBalance(BankAccount account)
        {
            RetrieveDataFromJsonFile();

            if (banking.Count > 0)
            {
                banking[0].Balance = account.Balance;
            }

            SaveDataToJsonFile();
        }
    }
}
