using ERPXTpl.Model;
using ERPXTpl.Validator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace ERPXTpl.Service
{
    internal class CustomerService
    {
        private readonly AuthorizeService authorizeService;
        public CustomerService()
        {
            authorizeService = new AuthorizeService();
        }
        public async Task<Result> GetCustomerById(long customerId)
        {
            var validateResult = CustomerValidator.DeleteAndGetCustomerByIdValidator(customerId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                Result result = new Result();
                result.Message = validateResult;
                return result;
            }
            return await GetCustomerHelper(customerId.ToString(), Endpoint.CUSTOMERS);
        }

        public async Task<Result> GetCustomerByTIN(string TIN)
        {
            var validateResult = CustomerValidator.GetCustomerByTINValidator(TIN);
            if (!string.IsNullOrEmpty(validateResult))
            {
                Result result = new Result();
                result.Message = validateResult;
                return result;
            }
            return await GetCustomerHelper(TIN, Endpoint.CUSTOMERS + "?nip=");
        }

        public async Task<Result> GetCustomersByEmail(string email)
        {
            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            Result result = new Result();
            List<Customer> customerData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(Endpoint.CUSTOMERS + "?$filter=Mail eq '{0}'", email));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        customerData = JsonConvert.DeserializeObject<List<Customer>>(responseBody);
                    }
                    return ResponseService.ResponseResult(response, responseBody, customerData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> AddCustomer(Customer customer)
        {
            Result result = new Result();
            var validateResult = CustomerValidator.PostCustomerValidator(customer);
            if (!string.IsNullOrEmpty(validateResult))
            {
                result.Message = validateResult;
                return result;
            }

            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            Customer customerData = new Customer();
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoint.CUSTOMERS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string customerDataToAdd = JsonConvert.SerializeObject(customer, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(customerDataToAdd, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        customerData.Id = Int64.Parse(responseBody);
                    }

                    return ResponseService.ResponseResult(response, responseBody, customerData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> ModifyCustomer(Customer customer)
        {
            Result result = new Result();
            var validateResult = CustomerValidator.ModifyCustomerValidator(customer);
            if (!string.IsNullOrEmpty(validateResult))
            {
                result.Message = validateResult;
                return result;
            }

            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Endpoint.CUSTOMERS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string customerDataToModify = JsonConvert.SerializeObject(customer, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(customerDataToModify, Encoding.UTF8, "application/json");

                    response = await client.PutAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return ResponseService.ResponseResult(response, responseBody);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> DeleteCustomer(long customerId)
        {
            Result result = new Result();
            var validateResult = ProductValidator.DeleteAndGetProductValidator(customerId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                result.Message = validateResult;
                return result;
            }

            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }
            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, Endpoint.CUSTOMERS + customerId);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return ResponseService.ResponseResult(response, responseBody);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        private async Task<Result> GetCustomerHelper(string value, string url)
        {
            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            Result result = new Result();
            Customer customerData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url + value);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        customerData = JsonConvert.DeserializeObject<Customer>(responseBody);
                    }
                    return ResponseService.ResponseResult(response, responseBody, customerData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }
    }
}
