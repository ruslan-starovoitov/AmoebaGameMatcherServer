﻿using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using DataLayer;
using Google.Apis.Upload;
using Newtonsoft.Json;

namespace Experimental
{

    static class Program
    {
        static async Task Main()
        {
            // await GoogleApiFileManager.WriteGoogleApiDataToFile();
            var data = await FileTesting.GetApiDataFromFile();
            if (data != null)
            {
                Console.WriteLine(data.AccessToken);
                Console.WriteLine(data.RefreshToken);
                Console.WriteLine(data.ExpiresInSec);    
            }
            else
            {
                Console.WriteLine("data was null");
            }
        }
    }

    public static class FileTesting
    {
        public static async Task WriteGoogleApiDataToFile()
        {
            MyGoogleApiData data = new MyGoogleApiData
            {
                AccessToken = "mydichAccess",
                RefreshToken = "refreshDich",
                ExpiresInSec = 999
            };
            string text = JsonConvert.SerializeObject(data);
            using (StreamWriter sw = new StreamWriter(GoogleApiGlobals.FileName, false, Encoding.UTF8))
            {
                await sw.WriteLineAsync(text);
            }
        }
        
        public static async Task<MyGoogleApiData> GetApiDataFromFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(GoogleApiGlobals.FileName, Encoding.UTF8))
                {
                    string fileContent = await sr.ReadToEndAsync();
                    if (string.IsNullOrEmpty(fileContent))
                    {
                        return null;
                    }

                    try
                    {
                        var result = JsonConvert.DeserializeObject<MyGoogleApiData>(fileContent);
                        return result;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(
                            "Брошено исключение при демериализации файла с данными для доступа к google api. " +
                            e.Message);
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}