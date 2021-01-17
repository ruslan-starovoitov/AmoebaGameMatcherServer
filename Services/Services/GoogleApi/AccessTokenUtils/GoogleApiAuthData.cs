using System;

namespace Services.Services.GoogleApi.AccessTokenUtils
{
    public class GoogleApiAuthData
    {
        public string AccessToken;
        public string RefreshToken;
        public int? ExpiresInSec;
        public DateTime? AccessTokenCreationTime;
    }
}