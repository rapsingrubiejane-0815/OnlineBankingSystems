using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingSystemModels
{
    public class BankAccount
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
    }
}
