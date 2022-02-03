using ERPXTpl.Model;
using ERPXTpl.Validator;
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
    internal class PaymentMethodService
    {
        private readonly AuthorizeService authorizeService;

        public PaymentMethodService()
        {
            authorizeService = new AuthorizeService();
        }
        public async Task<Result> GetPaymentMethod()
        {
            string url = Endpoint.PAYMENT_METHODS;
            return await GetPaymentMethodHelper(url);
        }
        public async Task<Result> GetPaymentMethod(long paymentMethodId)
        {
            var validateResult = PaymentMethodValidator.GetPaymentMethodById(paymentMethodId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                Result result = new Result();
                result.Message = validateResult;
                return result;
            }
            string url = Endpoint.PAYMENT_METHODS + paymentMethodId;
            return await GetPaymentMethodHelper(url);
        }

        private async Task<Result> GetPaymentMethodHelper(string url)
        {
            Result result = new Result();

            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            PaymentMethod paymentMethodData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        paymentMethodData = JsonConvert.DeserializeObject<PaymentMethod>(responseBody);
                    }

                    return ResponseService.ResponseResult(response, responseBody, paymentMethodData);
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
