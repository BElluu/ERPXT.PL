using ERPXTpl.Model;
using ERPXTpl.Validator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace ERPXTpl.Service
{
    internal class AuthorizeService
    {
        DateTime cacheExpire;
        internal async Task<Result> GetAuthToken(string clientId, string secretKey)
        {
            Result result = new Result();

            if (!ERPXT.cache.TryGetValue(CacheData.AccessToken, out cacheExpire))
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


                        if (response.IsSuccessStatusCode)
                        {
                            var authObject = JsonConvert.DeserializeObject<Authorization>(responseBody);

                            try
                            {
                                var secondsBeforeExpire = authObject.expires - 60;
                                cacheExpire = DateTime.Now.AddSeconds(secondsBeforeExpire);
                                ERPXT.cache.Set(CacheData.Expires, cacheExpire.ToString());
                                ERPXT.cache.Set(CacheData.AccessToken, authObject.access_token);
                                result.Data = "Token saved in cache";
                                result.StatusCode = response.StatusCode.ToString();
                                result.Message = response.ReasonPhrase;

                            }
                            catch (Exception ex)
                            {
                                result.Message = ex.Message;
                            }
                        }
                        else
                        {
                            var errorMessage = JsonConvert.DeserializeObject<Result.Error>(responseBody);
                            result.Data = null;
                            result.StatusCode = response.StatusCode.ToString();
                            result.Message = errorMessage.Message;
                        }
                    }

                    catch (Exception ex)
                    {
                        result.Message = ex.Message;
                    }
                }
            }
            return result;
        }

        internal async Task<Result> GetTokenIfNeeded()
        {
            Result result = new Result();
            if (DateTime.Now >= GetExpireDate())
            {
                var tokenValidator = AuthorizationValidator.GetTokenValidator(ERPXT.ClientID, ERPXT.SecretKey);
                if (!string.IsNullOrEmpty(tokenValidator))
                {
                    result.Message = tokenValidator;
                    return result;
                }
                return await GetAuthToken(ERPXT.ClientID, ERPXT.SecretKey);
            }
            result.Message = "Valid token is in cache";
            result.StatusCode = "OK";
            return result;
        }

        private DateTime GetExpireDate()
        {
            return DateTime.Parse(ERPXT.cache.Get(CacheData.Expires).ToString());
        }
    }
}
