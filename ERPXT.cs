using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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

        public async Task<Result> GetProduct(long productId)
        {
            Result result = new Result();
            var validateResult = ProductValidator.DeleteAndGetProductValidator(productId);
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

            Product productData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PRODUCTS + productId);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        productData = JsonConvert.DeserializeObject<Product>(responseBody);
                    }

                    return ResponseResult(response, responseBody, productData);
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
            var tokenResponse = await GetTokenIfNeeded();
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

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string productDataToAdd = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(productDataToAdd, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        productData.Id = Int64.Parse(responseBody);
                    }

                    return ResponseResult(response, responseBody, productData);
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
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Endpoint.PRODUCTS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string productData = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(productData, Encoding.UTF8, "application/json");

                    response = await client.PutAsync(request.RequestUri, stringContent);
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

        public async Task<Result> DeleteProduct(long productId)
        {
            Result result = new Result();
            var validateResult = ProductValidator.DeleteAndGetProductValidator(productId);
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
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, Endpoint.PRODUCTS + productId);

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

        public async Task<Result> GetCustomerById(long customerId)
        {
            var validateResult = CustomerValidator.DeleteAndGetCustomerByIdValidator(customerId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                Result result = new Result();
                result.Message = validateResult;
                return result;
            }
            return await GetCustomer(customerId, Endpoint.CUSTOMERS);
        }

        public async Task<Result> GetCustomerByTIN(long TIN)
        {
            var validateResult = CustomerValidator.GetCustomerByTINValidator(TIN);
            if (!string.IsNullOrEmpty(validateResult))
            {
                Result result = new Result();
                result.Message = validateResult;
                return result;
            }
            return await GetCustomer(TIN, Endpoint.CUSTOMERS + "?nip=");
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

            var tokenResponse = await GetTokenIfNeeded();
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

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string customerDataToAdd = JsonConvert.SerializeObject(customer, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(customerDataToAdd, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        customerData.Id = Int64.Parse(responseBody);
                    }

                    return ResponseResult(response, responseBody, customerData);
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
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Endpoint.CUSTOMERS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string customerDataToModify = JsonConvert.SerializeObject(customer, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(customerDataToModify, Encoding.UTF8, "application/json");

                    response = await client.PutAsync(request.RequestUri, stringContent);
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

        public async Task<Result> DeleteCustomer(int customerId)
        {
            Result result = new Result();
            var validateResult = ProductValidator.DeleteAndGetProductValidator(customerId);
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
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, Endpoint.CUSTOMERS + customerId);

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

        public async Task<Result> GetPaymentMethod()
        {
            var tokenResponse = await GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }
            Result result = new Result();
            List<PaymentMethod> paymentMethodData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PAYMENT_METHODS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        paymentMethodData = JsonConvert.DeserializeObject<List<PaymentMethod>>(responseBody);
                    }

                    return ResponseResult(response, responseBody, paymentMethodData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> GetPaymentMethod(long paymentMethodId)
        {
            Result result = new Result();

            var validateResult = PaymentMethodValidator.GetPaymentMethodById(paymentMethodId);
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

            PaymentMethod paymentMethodData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.PAYMENT_METHODS + paymentMethodId);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        paymentMethodData = JsonConvert.DeserializeObject<PaymentMethod>(responseBody);
                    }

                    return ResponseResult(response, responseBody, paymentMethodData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> GetBankAccounts()
        {
            var tokenResponse = await GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("OK"))
            {
                return tokenResponse;
            }
            Result result = new Result();
            List<BankAccount> bankAccountsData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.BANK_ACCOUNTS);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        bankAccountsData = JsonConvert.DeserializeObject<List<BankAccount>>(responseBody);
                    }

                    return ResponseResult(response, responseBody, bankAccountsData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> GetBankAccount(long bankAccountId)
        {
            Result result = new Result();

            var validateResult = BankAccountValidator.GetBankAccountById(bankAccountId);
            if (!string.IsNullOrEmpty(validateResult))
            {
                result.Message = validateResult;
                return result;
            }

            var tokenResponse = await GetTokenIfNeeded();
            if (!tokenResponse.StatusCode.Contains("200"))
            {
                return tokenResponse;
            }

            BankAccount bankAccountData = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.BANK_ACCOUNTS + bankAccountId);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        bankAccountData = JsonConvert.DeserializeObject<BankAccount>(responseBody);
                    }

                    return ResponseResult(response, responseBody, bankAccountData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> GetPrintTemplates()
        {
            var tokenResponse = await GetTokenIfNeeded();
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

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        printsData = JsonConvert.DeserializeObject<List<PrintTemplate>>(responseBody);
                    }

                    return ResponseResult(response, responseBody, printsData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<Result> GetInvoicePrintByCustomer(int invoiceId)
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

        public async Task<Result> GetVatRates()
        {
            Result result = new Result();

            var tokenResponse = await GetTokenIfNeeded();
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
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        vatRates = JsonConvert.DeserializeObject<List<VatRate>>(responseBody);
                    }
                    return ResponseResult(response, responseBody, vatRates);

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

            var tokenResponse = await GetTokenIfNeeded();
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
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        countries = JsonConvert.DeserializeObject<List<string>>(responseBody);
                    }
                    return ResponseResult(response, responseBody, countries);

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

            var validateResult = SalesInvoiceValidator.GetSalesInvoiceValidator(invoiceId);
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

            SalesInvoice invoice = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.SALES_INVOICES +"/"+ invoiceId);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        invoice = JsonConvert.DeserializeObject<SalesInvoice>(responseBody);
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

        public async Task<Result> GetSalesInvoice(string invoiceNumber)
        {
            Result result = new Result();

            var validateResult = SalesInvoiceValidator.GetSalesInvoiceValidator(invoiceNumber);
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

            SalesInvoice invoice = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoint.SALES_INVOICES +"?number="+invoiceNumber);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        invoice = JsonConvert.DeserializeObject<SalesInvoice>(responseBody);
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

        public async Task<Result> AddSalesInvoice(SalesInvoice salesInvoice)
        {
            Result result = new Result();
            var validateResult = SalesInvoiceValidator.PostSalesInvoiceValidator(salesInvoice);
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

            SalesInvoice salesInvoiceData = new SalesInvoice();
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoint.SALES_INVOICES);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string salesInvoiceDataToAdd = JsonConvert.SerializeObject(salesInvoice, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(salesInvoiceDataToAdd, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(request.RequestUri, stringContent);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        salesInvoiceData.Id = Int64.Parse(responseBody);
                    }

                    return ResponseResult(response, responseBody, salesInvoiceData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
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
            var tokenResponse = await GetTokenIfNeeded();
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

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        printData = JsonConvert.DeserializeObject<string>(responseBody).Substring(28);
                    }
                    return ResponseResult(response, responseBody, printData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        private async Task<Result> GetCustomer(long value, string url)
        {
            var tokenResponse = await GetTokenIfNeeded();
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
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get(CacheData.AccessToken).ToString());
                    var response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        customerData = JsonConvert.DeserializeObject<Customer>(responseBody);
                    }
                    return ResponseResult(response, responseBody, customerData);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        private async Task<Result> GetAuthToken(string clientId, string secretKey)
        {
            Result result = new Result();

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


                        if (response.IsSuccessStatusCode)
                        {
                            var authObject = JsonConvert.DeserializeObject<Models.Authorization>(responseBody);

                            try
                            {
                                var secondsBeforeExpire = authObject.expires - 60;
                                cacheExpire = DateTime.Now.AddSeconds(secondsBeforeExpire);
                                cache.Set(CacheData.Expires, cacheExpire.ToString());
                                cache.Set(CacheData.AccessToken, authObject.access_token);
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

        private async Task<Result> GetTokenIfNeeded()
        {
            Result result = new Result();
            if (DateTime.Now >= GetExpireDate())
            {
                var tokenValidator = AuthorizationValidator.GetTokenValidator(ClientID, SecretKey);
                if (!string.IsNullOrEmpty(tokenValidator))
                {
                    result.Message = tokenValidator;
                    return result;
                }
                return await GetAuthToken(ClientID, SecretKey);
            }
            result.Message = "Valid token is in cache";
            result.StatusCode = "OK";
            return result;
        }

        private DateTime GetExpireDate()
        {
            return DateTime.Parse(cache.Get(CacheData.Expires).ToString());
        }

        private Result ResponseResult(HttpResponseMessage response, string responseBody, object data = null)
        {
            Result result = new Result();
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
            {
                result.Data = data;
                result.StatusCode = response.StatusCode.ToString();
                result.Message = response.ReasonPhrase;
            }
            else
            {
                var errorMessage = JsonConvert.DeserializeObject<Result.Error>(responseBody);
                result.Data = null;
                result.StatusCode = response.StatusCode.ToString();
                result.Message = errorMessage.Message;
            }
            return result;
        }
    }
}

