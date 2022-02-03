using ERPXTpl.Model;
using ERPXTpl.Validator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Caching.Memory;

namespace ERPXTpl.Service
{
    internal class PrintService
    {
        private readonly AuthorizeService authorizeService;
        public PrintService()
        {
            authorizeService = new AuthorizeService();
        }
        public async Task<Result> GetPrintTemplates()
        {
            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            Result result = new Result();
            List<PrintTemplate> printsData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PRINT_TEMPLATES);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ERPXT.cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        printsData = JsonConvert.DeserializeObject<List<PrintTemplate>>(responseBody);
                    }

                    return ResponseService.ResponseResult(response, responseBody, printsData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> GetInvoicePrintByCustomer(long invoiceId)
        {
            var validateResult = PrintValidator.GetPrintValidator(invoiceId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                Result result = new Result();
                result.Message = validateResult;
                return result;
            }
            string url = Endpoint.INVOICE_PRINT_CUSTOMER + invoiceId;
            return await GetPrint(url);
        }

        public async Task<Result> GetProformaPrintByCustomer(long invoiceId)
        {
            var validateResult = PrintValidator.GetPrintValidator(invoiceId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                Result result = new Result();
                result.Message = validateResult;
                return result;
            }
            string url = Endpoint.PROFORMA_INVOICE_PRINT_CUSTOMER + invoiceId;
            return await GetPrint(url);
        }

        public async Task<Result> GetInvoiceCustomPrint(long invoiceId, long printTemplateId)
        {
            var validateResult = PrintValidator.GetCustomPrintValidator(printTemplateId, invoiceId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                Result result = new Result();
                result.Message = validateResult;
                return result;
            }
            string url = string.Format(Endpoint.INVOICE_PRINT_CUSTOM, invoiceId, printTemplateId);
            return await GetPrint(url);
        }

        public async Task<Result> GetProformaCustomPrint(long invoiceId, long printTemplateId)
        {
            var validateResult = PrintValidator.GetCustomPrintValidator(printTemplateId, invoiceId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                Result result = new Result();
                result.Message = validateResult;
                return result;
            }
            string url = string.Format(Endpoint.PROFORMA_PRINT_CUSTOM, invoiceId, printTemplateId);
            return await GetPrint(url);
        }

        public async Task<Result> SavePrintToFile(string base64Print, string pathToSave)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(base64Print) || string.IsNullOrEmpty(pathToSave))
            {
                result.Message = "Base64 or Path cannot be null or empty";
                return result;
            }
            try
            {
                byte[] PDFDecoded = Convert.FromBase64String(base64Print);
                string fileName = "PDF" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm") + ".pdf";
                string file = pathToSave + fileName;

                await Task.Run(() => File.WriteAllBytes(fileName, PDFDecoded));
                result.Message = fileName;
                result.StatusCode = "OK";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        private async Task<Result> GetPrint(string url)
        {
            var tokenResponse = await authorizeService.GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }

            string printData = null;
            Result result = new Result();
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
                        printData = JsonConvert.DeserializeObject<string>(responseBody).Substring(28);
                    }
                    return ResponseService.ResponseResult(response, responseBody, printData);
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
