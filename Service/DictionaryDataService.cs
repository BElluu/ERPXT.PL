using ERPXTpl.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace ERPXTpl.Service
{
    internal class DictionaryDataService
    {
        private readonly AuthorizeService authorizeService;
        public DictionaryDataService()
        {
            authorizeService = new AuthorizeService();
        }
        public async Task<Result> GetVatRates()
        {
            Result result = new Result();

            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            List<VatRate> vatRates = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.VAT_RATES);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        vatRates = JsonConvert.DeserializeObject<List<VatRate>>(responseBody);
                    }
                    return ResponseService.ResponseResult(response, responseBody, vatRates);

                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                return result;
            }
        }

        public async Task<Result> GetCountries()
        {
            Result result = new Result();

            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            List<string> countries = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.VAT_RATES + "countries");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        countries = JsonConvert.DeserializeObject<List<string>>(responseBody);
                    }
                    return ResponseService.ResponseResult(response, responseBody, countries);

                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                return result;
            }
        }
    }
}
