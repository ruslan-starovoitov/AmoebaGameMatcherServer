using System;
using System.Collections.Generic;

namespace NebulaWarsAdminPanel.Models
{
    public class AccountsViewModel
    {
        public List<AccountShortViewModel> Acccounts { get; set; }
    }

    public class AccountShortViewModel
    {
        public string Username { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}