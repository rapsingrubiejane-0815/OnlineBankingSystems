using Microsoft.Data.SqlClient;
using OnlineBankingSystemDataService;
using OnlineBankingSystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingSystem
{
    public class OnlineBankingDBData : IOnlineBankingDataService
    {
        private string connectionString
            = "Data Source = localhost\\SQLEXPRESS; Initial Catalog = db_OnlineBanking; Integrated Security = True; TrustServerCertificate=True;";

        private SqlConnection sqlConnection;

        public OnlineBankingDBData()
        {
            sqlConnection = new SqlConnection(connectionString);
            AddSeeds();
        }

        private void AddSeeds()
        {
            var existing = GetBalance();

            if (existing.Count == 0)
            {
                BankAccount onbank = new BankAccount
                {

                    Balance = 1000
                };
                Add(onbank);
            }
        }
        public void Add(BankAccount onbanking)
        {
            string insertStatement = "INSERT INTO tbl_OnlineBanking (Balance) VALUES (@Balance)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@Balance", onbanking.Balance);

            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        
        public List<BankAccount> GetBalance()
        {
            string selectStatement = "SELECT Balance FROM tbl_OnlineBanking";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var onlinebank = new List<BankAccount>();

            while (reader.Read())
            {
                BankAccount account = new BankAccount();
                account.Balance = Convert.ToDouble(reader["Balance"]);

                onlinebank.Add(account);
            }

            sqlConnection.Close();
            return onlinebank;
        }

     
        public void UpdateBalance(BankAccount bankaccount)
        {
            string updateStatement = "UPDATE tbl_OnlineBanking SET Balance = @Balance";

            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            updateCommand.Parameters.AddWithValue("@Balance", bankaccount.Balance);

            sqlConnection.Open();
            updateCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

     
        public BankAccount? GetBalances(double bal)
        {
            return GetBalance().FirstOrDefault(x => x.Balance == bal);
        }
    }


}