using System.Collections.Generic;
using DataLayer.Tables;

namespace AdminPanel.Models
{
    public class AccountViewModel
    {
        public AccountDbDto AccountDbDto { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}