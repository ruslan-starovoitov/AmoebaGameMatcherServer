using DataLayer;

namespace Services.Services.GoogleApi.AccessTokenUtils
{
    public class GoogleApiProfileStorageService
    {
        public GoogleApiProfile GetCurrentProfile()
        {
            return new GoogleApiProfileNewest();
        }
    }
}    