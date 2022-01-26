﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ERPXTpl.Models;
using ERPXTpl.Validators;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace ERPXTpl
{
    public class ERPXT
    {
        DateTime cacheExpire;
        string ClientID = string.Empty;
        string SecretKey = string.Empty;
        MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public ERPXT(string clientID, string secretKey)
        {
            ClientID = clientID;
            SecretKey = secretKey;
            cache.Set(CacheData.Expires, new DateTime());
        }

        public async Task<Product> GetProduct(int productId)
        {
            ProductValidator.DeleteAndGetProductValidator(productId);

            if (DateTime.Now >= GetExpireDate())
            {
                await GetAuthToken(ClientID, SecretKey);
            }

            Product productData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://app.erpxt.pl/api2/public/products/" + productId);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    productData = JsonConvert.DeserializeObject<Product>(responseBody);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return productData;
        }

        public async Task<int> AddProduct(Product product)
        {
            ProductValidator.AddProductValidator(product);

            if (DateTime.Now >= GetExpireDate())
            {
                await GetAuthToken(ClientID, SecretKey);
            }

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://app.erpxt.pl/api2/public/products");

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string productData = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(productData, Encoding.UTF8, "application/json");
                    
                    var response = await client.PostAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return Int32.Parse(responseBody);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return 0;
        }

        public async Task<string> ModifyProduct(Product product)
        {
            ProductValidator.ModifyProductValidator(product);

            if (DateTime.Now >= GetExpireDate())
            {
                await GetAuthToken(ClientID, SecretKey);
            }
            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "https://app.erpxt.pl/api2/public/products");

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string productData = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(productData, Encoding.UTF8, "application/json");

                    response = await client.PutAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return response.StatusCode.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return response.StatusCode.ToString();
        }

        public async Task<string> DeleteProduct(int productId)
        {
            ProductValidator.DeleteAndGetProductValidator(productId);

            if (DateTime.Now >= GetExpireDate())
            {
                await GetAuthToken(ClientID, SecretKey);
            }

            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "https://app.erpxt.pl/api2/public/products/" + productId);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return response.StatusCode.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return response.StatusCode.ToString();
        }


        private async Task GetAuthToken(string clientId, string secretKey)
        {
            AuthorizationValidator.GetTokenValidator(clientId, secretKey);

            if (!cache.TryGetValue(CacheData.AccessToken, out cacheExpire))
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://app.erpxt.pl/api2/public/token");

                        var authData = new UTF8Encoding().GetBytes(clientId + ":" + secretKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authData));

                        var formData = new List<KeyValuePair<string, string>>();
                        formData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                        request.Content = new FormUrlEncodedContent(formData);

                        var response = await client.SendAsync(request);
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var authObject = JsonConvert.DeserializeObject<Authorization>(responseBody);

                        try
                        {
                            var secondsBeforeExpire = authObject.expires - 60;
                            cacheExpire = DateTime.Now.AddSeconds(secondsBeforeExpire);
                            cache.Set(CacheData.Expires, cacheExpire.ToString());
                            cache.Set(CacheData.AccessToken, authObject.access_token);

                            Console.WriteLine(cache.Get(CacheData.AccessToken));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        }

        private DateTime GetExpireDate()
        {
            return DateTime.Parse(cache.Get(CacheData.Expires).ToString());
        }
    }
}

