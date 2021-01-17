namespace Services.Services.GoogleApi.AccessTokenUtils
{
    public class PackageNameStorageService
    {
        private readonly string packageName;

        public PackageNameStorageService(GoogleApiProfileStorageService profileStorageService)
        {
            packageName = profileStorageService.GetCurrentProfile().PackageName;
        }
        public string GetPackageName()
        {
            return packageName;
        }
    }
}