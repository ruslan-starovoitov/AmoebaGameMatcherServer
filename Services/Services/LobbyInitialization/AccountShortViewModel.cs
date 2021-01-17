using System;

namespace Services.Services.LobbyInitialization
{
    public class AccountShortViewModel
    {
        public string ServiceId { get; set; }
        public int AccountId { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}