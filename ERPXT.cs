using System;
using System.Threading.Tasks;
using ERPXTpl.Model;
using ERPXTpl.Service;
using Microsoft.Extensions.Caching.Memory;

namespace ERPXTpl
{
    public class ERPXT
    {
        public static string ClientID = string.Empty;
        public static string SecretKey = string.Empty;
        public static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public ERPXT(string clientID, string secretKey)
        {
            ClientID = clientID;
            SecretKey = secretKey;
            cache.Set(CacheData.Expires, new DateTime());
        }

        public Task<Result> GetBankAccount()
        {
            BankAccountService bankAccountService = new BankAccountService();
            return bankAccountService.GetBankAccount();
        }

        public Task<Result> GetBankAccount(long bankAccountId)
        {
            BankAccountService bankAccountService = new BankAccountService();
            return bankAccountService.GetBankAccount(bankAccountId);
        }

        public Task<Result> GetCustomer(long customerId)
        {
            CustomerService customerService = new CustomerService();
            return customerService.GetCustomerById(customerId);
        }
        public Task<Result> GetCustomer(string TIN)
        {
            CustomerService customerService = new CustomerService();
            return customerService.GetCustomerByTIN(TIN);
        }
        public Task<Result> GetCustomerByEmail(string email)
        {
            CustomerService customerService = new CustomerService();
            return customerService.GetCustomersByEmail(email);
        }
        public Task<Result> AddCustomer(Customer customer)
        {
            CustomerService customerService = new CustomerService();
            return customerService.AddCustomer(customer);
        }
        public Task<Result> ModifyCustomer(Customer customer)
        {
            CustomerService customerService = new CustomerService();
            return customerService.ModifyCustomer(customer);
        }
        public Task<Result> DeleteCustomer(long customerId)
        {
            CustomerService customerService = new CustomerService();
            return customerService.DeleteCustomer(customerId);
        }

        public Task<Result> GetVatRates()
        {
            DictionaryDataService dictionaryDataService = new DictionaryDataService();
            return dictionaryDataService.GetVatRates();
        }
        public Task<Result> GetCountries()
        {
            DictionaryDataService dictionaryDataService = new DictionaryDataService();
            return dictionaryDataService.GetCountries();
        }

        public Task<Result> GetPaymentMethod()
        {
            PaymentMethodService paymentMethodService = new PaymentMethodService();
            return paymentMethodService.GetPaymentMethod();
        }
        public Task<Result> GetPaymentMethod(long paymentMethodId)
        {
            PaymentMethodService paymentMethodService = new PaymentMethodService();
            return paymentMethodService.GetPaymentMethod(paymentMethodId);
        }
        public Task<Result> GetPrintTemplates()
        {
            PrintService printService = new PrintService();
            return printService.GetPrintTemplates();
        }
        public Task<Result> GetInvoicePrintByCustomer(long invoiceId)
        {
            PrintService printService = new PrintService();
            return printService.GetInvoicePrintByCustomer(invoiceId);
        }
        public Task<Result> GetProformaPrintByCustomer(long invoiceId)
        {
            PrintService printService = new PrintService();
            return printService.GetProformaPrintByCustomer(invoiceId);
        }
        public Task<Result> GetInvoiceCustomPrint(long invoiceId, long printTemplateId)
        {
            PrintService printService = new PrintService();
            return printService.GetInvoiceCustomPrint(invoiceId, printTemplateId);
        }
        public Task<Result> GetProformaCustomPrint(long invoiceId, long printTemplateId)
        {
            PrintService printService = new PrintService();
            return printService.GetProformaCustomPrint(invoiceId, printTemplateId);
        }
        public Task<Result> SavePrintToFile(string base64Print, string pathToSave)
        {
            PrintService printService = new PrintService();
            return printService.SavePrintToFile(base64Print, pathToSave);
        }
        public Task<Result> GetProduct()
        {
            ProductService productService = new ProductService();
            return productService.GetProduct();
        }
        public Task<Result> GetProduct(long productId)
        {
            ProductService productService = new ProductService();
            return productService.GetProduct(productId);
        }
        public Task<Result> AddProduct(Product product)
        {
            ProductService productService = new ProductService();
            return productService.AddProduct(product);
        }
        public Task<Result> ModifyProduct(Product product)
        {
            ProductService productService = new ProductService();
            return productService.ModifyProduct(product);
        }
        public Task<Result> DeleteProduct(long productId)
        {
            ProductService productService = new ProductService();
            return productService.DeleteProduct(productId);
        }
    }
}

