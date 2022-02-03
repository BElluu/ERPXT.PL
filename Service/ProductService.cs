using ERPXTpl.Model;
using ERPXTpl.Validator;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ERPXTpl.Service
{
    internal class ProductService
    {
        private readonly AuthorizeService authorizeService;
        public ProductService()
        {
            authorizeService = new AuthorizeService();
        }

        public async Task<Result> GetProduct()
        {
            Result result = new Result();
            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            List<Product> productData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PRODUCTS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        productData = JsonConvert.DeserializeObject<List<Product>>(responseBody);
                    }

                    return ResponseService.ResponseResult(response, responseBody, productData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        public async Task<Result> GetProduct(long productId)
        {
            Result result = new Result();
            var validateResult = ProductValidator.DeleteAndGetProductValidator(productId);
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

            Product productData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PRODUCTS + productId);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        productData = JsonConvert.DeserializeObject<Product>(responseBody);
                    }

                    return ResponseService.ResponseResult(response, responseBody, productData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> AddProduct(Product product)
        {
            Result result = new Result();
            var validateResult = ProductValidator.AddProductValidator(product);
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
            Product productData = new Product();
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoint.PRODUCTS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string productDataToAdd = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(productDataToAdd, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        productData.Id = Int64.Parse(responseBody);
                    }

                    return ResponseService.ResponseResult(response, responseBody, productData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> ModifyProduct(Product product)
        {
            Result result = new Result();
            var validateResult = ProductValidator.ModifyProductValidator(product);
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
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Endpoint.PRODUCTS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string productData = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(productData, Encoding.UTF8, "application/json");

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

        public async Task<Result> DeleteProduct(long productId)
        {
            Result result = new Result();
            var validateResult = ProductValidator.DeleteAndGetProductValidator(productId);
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
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, Endpoint.PRODUCTS + productId);

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
    }
}
