using System;
using System.Net.Http;
using System.Threading.Tasks;
using Services.Services.GoogleApi.AccessTokenUtils;
using Services.Services.GoogleApi.UrlFactories;

namespace Services.Services.GoogleApi
{
    /// <summary>
    /// Уведомляет google о том, что покупка была нормально записана в БД
    /// </summary>
    public class GoogleApiPurchaseAcknowledgeService
    {
        private readonly PurchaseAcknowledgeUrlFactory factory;
        private readonly CustomGoogleApiAccessTokenService accessTokenService;
        
        public GoogleApiPurchaseAcknowledgeService(CustomGoogleApiAccessTokenService accessTokenService,
            PurchaseAcknowledgeUrlFactory factory)
        {
            this.accessTokenService = accessTokenService;
            this.factory = factory;
        }
        
        public async Task Acknowledge(string productId, string token, string developerPayload)
        {
            string accessToken = accessTokenService.GetAccessToken();
            string url = factory.Create(productId, token, accessToken);
            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(developerPayload);
            var result = await httpClient.PostAsync(url, httpContent);
            
            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine(result.StatusCode);
                throw new Exception("Не удалось уведомить о регистрации покупки.");
            }
        }
    }
}