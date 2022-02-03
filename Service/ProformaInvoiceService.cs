/*using ERPXTpl.Model;
using ERPXTpl.Validator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ERPXTpl.Service
{
    internal class ProformaInvoiceService
    {
        public async Task<Result> GetProformaInvoice()
        {
            Result result = new Result();

            var tokenResponse = await GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            List<ProformaInvoice> invoice = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PROFORMA_INVOICES);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        invoice = JsonConvert.DeserializeObject<List<ProformaInvoice>>(responseBody);
                    }
                    return ResponseResult(response, responseBody, invoice);

                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                return result;
            }
        }

        public async Task<Result> GetProformaInvoice(long invoiceId)
        {
            Result result = new Result();

            var validateResult = InvoiceValidator.GetInvoiceValidator(invoiceId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                result.Message = validateResult;
                return result;
            }

            var tokenResponse = await GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            ProformaInvoice invoice = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PROFORMA_INVOICES + "/" + invoiceId);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        invoice = JsonConvert.DeserializeObject<ProformaInvoice>(responseBody);
                    }
                    return ResponseResult(response, responseBody, invoice);

                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                return result;
            }
        }

        public async Task<Result> GetProformasFiltered(bool converted)
        {
            string filter;
            if (converted == true)
            {
                filter = "/?$filter=InvoiceDocIdNum ne null";
            }
            else
            {
                filter = "/?$filter=InvoiceDocIdNum eq null";
            }
            return await GetFilteredProformas(filter);
        }
        public async Task<Result> AddProformaInvoice(ProformaInvoice proformaInvoice)
        {
            Result result = new Result();
            var validateResult = InvoiceValidator.PostInvoiceValidator(proformaInvoice);
            if (!string.IsNullOrEmpty(validateResult))
            {
                result.Message = validateResult;
                return result;
            }

            var tokenResponse = await GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            ProformaInvoice proformaInvoiceData = new ProformaInvoice();
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoint.PROFORMA_INVOICES);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string proformaInvoiceDataToAdd = JsonConvert.SerializeObject(proformaInvoice, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(proformaInvoiceDataToAdd, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        proformaInvoiceData.Id = Int64.Parse(responseBody);
                    }

                    return ResponseResult(response, responseBody, proformaInvoiceData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> ModifyProformaInvoice(ProformaInvoice proformaInvoice)
        {
            Result result = new Result();
            var validateResult = InvoiceValidator.PutSalesInvoiceValidator(proformaInvoice);
            if (!string.IsNullOrEmpty(validateResult))
            {
                result.Message = validateResult;
                return result;
            }

            var tokenResponse = await GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Endpoint.PROFORMA_INVOICES);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string proformaInvoiceDataToAdd = JsonConvert.SerializeObject(proformaInvoice, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(proformaInvoiceDataToAdd, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return ResponseResult(response, responseBody);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        private async Task<Result> DeleteProformaInvoice(long invoiceId)
        {
            Result result = new Result();
            var validateResult = InvoiceValidator.GetInvoiceValidator(invoiceId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                result.Message = validateResult;
                return result;
            }

            var tokenResponse = await GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, Endpoint.PROFORMA_INVOICES);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return ResponseResult(response, responseBody);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        private async Task<Result> GetFilteredProformas(string filter)
        {
            Result result = new Result();

            var tokenResponse = await GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            List<ProformaInvoice> invoices = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PROFORMA_INVOICES + filter);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        invoices = JsonConvert.DeserializeObject<List<ProformaInvoice>>(responseBody);
                    }
                    return ResponseResult(response, responseBody, invoices);

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
*/