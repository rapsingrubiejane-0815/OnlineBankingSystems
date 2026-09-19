using Microsoft.Data.SqlClient;
using OnlineBankingSystemDataService;
using OnlineBankingSystemModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBankingSystem
{
    public class OnlineBankingDBData : IOnlineBankingDataService
    {
        private string connectionString = "Data Source = localhost\\SQLEXPRESS; Initial Catalog = db_OnlineBanking; Integrated Security = True; TrustServerCertificate=True;";

        public OnlineBankingDBData()
        {
            AddSeeds();
        }

        private void AddSeeds()
        {
            var existing = GetBalance();

            if (existing.Count == 0)
            {
                BankAccount onbank = new BankAccount
                {
                    AccountId = Guid.NewGuid(),
                    Username = "rubie",
                    Password = "rubie123",
                    Email = "rubie@gmail.com",
                    Balance = 10.0
                };
                Add(onbank);
            }
        }

        public void Add(BankAccount onbanking)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // FIXED: Added Email to the column list to match the 5 parameters below
                string query = @"
                    INSERT INTO tbl_OnlineBanking (AccountId, Username, Password, Balance, Email)
                    VALUES (@AccountId, @Username, @Password, @Balance, @Email)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountId", onbanking.AccountId);
                    cmd.Parameters.AddWithValue("@Username", onbanking.Username);
                    cmd.Parameters.AddWithValue("@Password", onbanking.Password);
                    cmd.Parameters.AddWithValue("@Balance", onbanking.Balance);
                    cmd.Parameters.AddWithValue("@Email", (object?)onbanking.Email ?? DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<BankAccount> GetBalance()
        {
            var onlinebank = new List<BankAccount>();
            string selectStatement = "SELECT AccountId, Username, Password, Balance, Email FROM tbl_OnlineBanking";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand selectCommand = new SqlCommand(selectStatement, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BankAccount account = new BankAccount();
                            account.AccountId = Guid.Parse(reader["AccountId"].ToString()!);
                            account.Username = reader["Username"].ToString()!;
                            account.Password = reader["Password"].ToString()!;
                            account.Balance = Convert.ToDouble(reader["Balance"].ToString());
                            account.Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString();

                            onlinebank.Add(account);
                        }
                    }
                }
            }
            return onlinebank;
        }

        public void UpdateBalance(BankAccount bankaccount)
        {
            string updateStatement = "UPDATE tbl_OnlineBanking SET Balance = @Balance WHERE AccountId = @AccountId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand updateCommand = new SqlCommand(updateStatement, conn))
                {
                    updateCommand.Parameters.AddWithValue("@AccountId", bankaccount.AccountId);
                    updateCommand.Parameters.AddWithValue("@Balance", bankaccount.Balance);

                    conn.Open();
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        public BankAccount? GetByUsername(string username)
        {
            string query = "SELECT AccountId, Username, Password, Balance, Email FROM tbl_OnlineBanking WHERE Username = @Username";
            BankAccount? account = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account = new BankAccount
                            {
                                AccountId = Guid.Parse(reader["AccountId"].ToString()!),
                                Username = reader["Username"].ToString()!,
                                Password = reader["Password"].ToString()!,
                                Balance = Convert.ToDouble(reader["Balance"].ToString()),
                                Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                            };
                        }
                    }
                }
            }
            return account;
        }

        public BankAccount? GetBalances(double bal)
        {
            return GetBalance().FirstOrDefault(x => x.Balance == bal);
        }

        public BankAccount? GetById(Guid id)
        {
            var selectStatement = "SELECT AccountId, Username, Password, Balance, Email FROM tbl_OnlineBanking WHERE AccountId = @AccountId";
            BankAccount account = new BankAccount();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand selectCommand = new SqlCommand(selectStatement, conn))
                {
                    selectCommand.Parameters.AddWithValue("@AccountId", id);
                    conn.Open();

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account.AccountId = Guid.Parse(reader["AccountId"].ToString()!);
                            account.Username = reader["Username"].ToString()!;
                            account.Password = reader["Password"].ToString()!;
                            account.Balance = Convert.ToDouble(reader["Balance"].ToString());
                            account.Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString();
                        }
                    }
                }
            }
            return account;
        }
    }
}