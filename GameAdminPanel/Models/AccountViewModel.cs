using System.Collections.Generic;
using DataLayer.Tables;

namespace GameAdminPanel.Controllers
{
    public class AccountViewModel
    {
        public AccountDbDto AccountDbDto { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}