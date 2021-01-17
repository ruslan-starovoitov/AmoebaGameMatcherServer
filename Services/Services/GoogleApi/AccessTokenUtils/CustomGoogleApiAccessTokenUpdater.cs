﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;

namespace Services.Services.GoogleApi.AccessTokenUtils
{
    public static class CustomGoogleApiAccessTokenUpdater
    {
        public static async Task<string> RenewAccessToken(AccessTokenUpdatingArg tokenUpdatingArg)
        {
            Console.WriteLine("старт скачивания данных о новом токене \n\n\n");
            HttpClient httpClient = new HttpClient();
            Dictionary<string, string> requestData = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"client_id", tokenUpdatingArg.ClientId},
                {"client_secret", tokenUpdatingArg.ClientSecret},
                {"refresh_token", tokenUpdatingArg.RefreshToken}
            };
            
            HttpContent  httpContent = new FormUrlEncodedContent(requestData);
            const string url ="https://accounts.google.com/o/oauth2/token";
            
            var responseMessage = await httpClient.PostAsync(url, httpContent);
            string responseContent = await responseMessage.Content.ReadAsStringAsync();
            
            Console.WriteLine($"{nameof(responseMessage.StatusCode)} {responseMessage.StatusCode}");
            Console.WriteLine($"{nameof(responseContent)} {responseContent}");
            
            if (responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine($"responseMessage is ok status");
                return responseContent;
            }

            throw new Exception("1 Не удалось получить токен.");
        }
    }
}