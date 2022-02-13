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
    internal class SalesInvoiceService
    {
        private readonly AuthorizeService authorizeService;

        public SalesInvoiceService()
        {
            authorizeService = new AuthorizeService();
        }
        public async Task<Result> GetSalesInvoice()
        {
            Result result = new Result();

            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            List<SalesInvoice> invoices = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.SALES_INVOICES);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        invoices = JsonConvert.DeserializeObject<List<SalesInvoice>>(responseBody);
                    }
                    return ResponseService.TakeResult(response, responseBody, invoices);

                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                return result;
            }
        }

        public async Task<Result> GetSalesInvoice(long invoiceId)
        {
            Result result = new Result();

            var validateResult = InvoiceValidator.GetInvoiceValidator(invoiceId);
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

            SalesInvoice invoice = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.SALES_INVOICES + "/" + invoiceId);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        invoice = JsonConvert.DeserializeObject<SalesInvoice>(responseBody);
                    }
                    return ResponseService.TakeResult(response, responseBody, invoice);

                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                return result;
            }
        }

        public async Task<Result> GetSalesInvoice(string invoiceNumber)
        {
            Result result = new Result();

            var validateResult = InvoiceValidator.GetInvoiceValidator(invoiceNumber);
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

            SalesInvoice invoice = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.SALES_INVOICES + "?number=" + invoiceNumber);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        invoice = JsonConvert.DeserializeObject<SalesInvoice>(responseBody);
                    }
                    return ResponseService.TakeResult(response, responseBody, invoice);

                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                return result;
            }
        }

        public async Task<Result> GetLastInvoices(int numberOfInvoices)
        {
            Result result = new Result();

            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            List<SalesInvoice> invoices = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(Endpoint.SALES_INVOICES + "?&$orderby=IssueDate desc, Id desc &$skip=0 &$top={0}", numberOfInvoices));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        invoices = JsonConvert.DeserializeObject<List<SalesInvoice>>(responseBody);
                    }
                    return ResponseService.TakeResult(response, responseBody, invoices);

                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                return result;
            }
        }

        public async Task<Result> AddSalesInvoice(SalesInvoice salesInvoice)
        {
            Result result = new Result();
            var validateResult = InvoiceValidator.PostInvoiceValidator(salesInvoice);
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

            SalesInvoice salesInvoiceData = new SalesInvoice();
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoint.SALES_INVOICES);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string salesInvoiceDataToAdd = JsonConvert.SerializeObject(salesInvoice, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(salesInvoiceDataToAdd, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        salesInvoiceData.Id = Int64.Parse(responseBody);
                    }

                    return ResponseService.TakeResult(response, responseBody, salesInvoiceData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> ModifySalesInvoice(SalesInvoice salesInvoice)
        {
            Result result = new Result();
            var validateResult = InvoiceValidator.PutSalesInvoiceValidator(salesInvoice);
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

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Endpoint.SALES_INVOICES);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string salesInvoiceDataToAdd = JsonConvert.SerializeObject(salesInvoice, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(salesInvoiceDataToAdd, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return ResponseService.TakeResult(response, responseBody);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> DeleteSalesInvoice(long invoiceId)
        {
            Result result = new Result();
            var validateResult = InvoiceValidator.GetInvoiceValidator(invoiceId);
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
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, Endpoint.SALES_INVOICES + invoiceId);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return ResponseService.TakeResult(response, responseBody);
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
