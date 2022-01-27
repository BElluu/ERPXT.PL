using System;
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
            await GetTokenIfNeeded();

            Product productData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PRODUCTS + productId);

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
            await GetTokenIfNeeded();

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoint.PRODUCTS);

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
            await GetTokenIfNeeded();

            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Endpoint.PRODUCTS);

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
            await GetTokenIfNeeded();

            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, Endpoint.PRODUCTS + productId);

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

        public async Task<Customer> GetCustomerById(long customerId)
        {
            CustomerValidator.DeleteAndGetCustomerByIdValidator(customerId);
            return await GetCustomer(customerId, Endpoint.CUSTOMERS);
        }

        public async Task<Customer> GetCustomerByTIN(long TIN)
        {
            CustomerValidator.GetCustomerByTINValidator(TIN);
            return await GetCustomer(TIN, Endpoint.CUSTOMERS + "?nip=");
        }

        public async Task<int> AddCustomer(Customer customer)
        {
            CustomerValidator.PostCustomerValidator(customer);
            await GetTokenIfNeeded();

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoint.CUSTOMERS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string customerData = JsonConvert.SerializeObject(customer, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(customerData, Encoding.UTF8, "application/json");

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

        public async Task<string> ModifyCustomer(Customer customer)
        {
            CustomerValidator.ModifyCustomerValidator(customer);
            await GetTokenIfNeeded();

            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Endpoint.CUSTOMERS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string customerData = JsonConvert.SerializeObject(customer, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(customerData, Encoding.UTF8, "application/json");

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

        public async Task<string> DeleteCustomer(int customerId)
        {
            ProductValidator.DeleteAndGetProductValidator(customerId);
            await GetTokenIfNeeded();

            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, Endpoint.CUSTOMERS + customerId);

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

        public async Task<List<PaymentMethod>> GetPaymentMethod()
        {
            await GetTokenIfNeeded();

            List<PaymentMethod> paymentMethodData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PAYMENT_METHODS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    paymentMethodData = JsonConvert.DeserializeObject<List<PaymentMethod>>(responseBody);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return paymentMethodData;
        }

        public async Task<PaymentMethod> GetPaymentMethod(int paymentMethodId)
        {
            PaymentMethodValidator.GetPaymentMethodById(paymentMethodId);
            await GetTokenIfNeeded();

            PaymentMethod paymentMethodData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PAYMENT_METHODS + paymentMethodId);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    paymentMethodData = JsonConvert.DeserializeObject<PaymentMethod>(responseBody);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return paymentMethodData;
        }

        public async Task<List<BankAccount>> GetBankAccount()
        {
            await GetTokenIfNeeded();

            List<BankAccount> bankAccountData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.BANK_ACCOUNTS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    bankAccountData = JsonConvert.DeserializeObject<List<BankAccount>>(responseBody);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return bankAccountData;
        }

        public async Task<BankAccount> GetBankAccount(int bankAccountId)
        {
            BankAccountValidator.GetBankAccountById(bankAccountId);
            await GetTokenIfNeeded();

            BankAccount bankAccountData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.BANK_ACCOUNTS + bankAccountId);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    bankAccountData = JsonConvert.DeserializeObject<BankAccount>(responseBody);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return bankAccountData;
        }

        private async Task<Customer> GetCustomer(long value, string url)
        {
            await GetTokenIfNeeded();

            Customer customerData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url + value);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    customerData = JsonConvert.DeserializeObject<Customer>(responseBody);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return customerData;
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
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoint.AUTHORIZATION);

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

        private async Task GetTokenIfNeeded()
        {
            if (DateTime.Now >= GetExpireDate())
            {
                await GetAuthToken(ClientID, SecretKey);
            }
        }

        private DateTime GetExpireDate()
        {
            return DateTime.Parse(cache.Get(CacheData.Expires).ToString());
        }
    }
}

