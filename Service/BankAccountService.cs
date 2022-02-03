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
    internal class BankAccountService
    {
        private readonly AuthorizeService authorizeService;
        public BankAccountService()
        {
            authorizeService = new AuthorizeService();
        }
        public async Task<Result> GetBankAccount()
        {
            string url = Endpoint.BANK_ACCOUNTS;
            return await GetBankAccountHelper(url);
        }

        public async Task<Result> GetBankAccount(long bankAccountId)
        {
            var validateResult = BankAccountValidator.GetBankAccountById(bankAccountId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                Result result = new Result();
                result.Message = validateResult;
                return result;
            }

            string url = Endpoint.BANK_ACCOUNTS + bankAccountId;
            return await GetBankAccountHelper(url);
        }

        public async Task<Result> GetBankAccountHelper(string url)
        {
            Result result = new Result();

            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            BankAccount bankAccountData = null;
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
                        bankAccountData = JsonConvert.DeserializeObject<BankAccount>(responseBody);
                    }

                    return ResponseService.ResponseResult(response, responseBody, bankAccountData);
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
