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
                    Username = "rubie",
                    Password = "rubie123",
                    Balance = 10.0
                };
                Add(onbank);
            }
        }
        public void Add(BankAccount onbanking)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        INSERT INTO tbl_OnlineBanking (AccountId, Username, Password, Balance)
        VALUES (@AccountId, @Username, @Password, @Balance)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@AccountId", onbanking.AccountId);
                cmd.Parameters.AddWithValue("@Username", onbanking.Username);
                cmd.Parameters.AddWithValue("@Password", onbanking.Password);
                cmd.Parameters.AddWithValue("@Balance", onbanking.Balance);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public List<BankAccount> GetBalance()
        {
            string selectStatement = "SELECT AccountId, Username, Password, Balance FROM tbl_OnlineBanking";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var onlinebank = new List<BankAccount>();

            while (reader.Read())
            {
                BankAccount account = new BankAccount();
                account.AccountId = Guid.Parse(reader["AccountId"].ToString());
                account.Username = reader["Username"].ToString();
                account.Password = reader["Password"].ToString();
                account.Balance = Convert.ToDouble(reader["Balance"].ToString());

                onlinebank.Add(account);
            }

            sqlConnection.Close();
            return onlinebank;
        }

        public void UpdateBalance(BankAccount bankaccount)
        {
            string updateStatement = "UPDATE tbl_OnlineBanking SET Balance = @Balance WHERE AccountId = @AccountId";

            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            updateCommand.Parameters.AddWithValue("@AccountId", bankaccount.AccountId);
            updateCommand.Parameters.AddWithValue("@Balance", bankaccount.Balance);

            sqlConnection.Open();
            updateCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public BankAccount? GetByUsername(string username)
        {
            string query = "SELECT AccountId, Username, Password, Balance FROM tbl_OnlineBanking WHERE Username = @Username";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@Username", username);

            sqlConnection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            BankAccount? account = null;

            if (reader.Read())
            {
                account = new BankAccount
                {
                    AccountId = Guid.Parse(reader["AccountId"].ToString()),
                    Username = reader["Username"].ToString(),
                    Password = reader["Password"].ToString(),
                    Balance = Convert.ToDouble(reader["Balance"].ToString())
                };
            }

            sqlConnection.Close();
            return account;
        }

        public BankAccount? GetBalances(double bal)
        {
            return GetBalance().FirstOrDefault(x => x.Balance == bal);
        }

        public BankAccount? GetById(Guid id)
        {
            var selectStatement = "SELECT AccountId, Username, Password FROM tbl_OnlineBanking WHERE AccountId = @AccountId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@AccountId", id.ToString());
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var account = new BankAccount();

            while (reader.Read())
            {
                account.AccountId = Guid.Parse(reader["AccountId"].ToString());
                account.Username = reader["Username"].ToString();
                account.Password = reader["Password"].ToString();
            }

            sqlConnection.Close();
            return account;
        }
    }


}