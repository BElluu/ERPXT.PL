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
        public async Task<Result> GetBankAccount()
        {
            BankAccountService bankAccountService = new BankAccountService();
            return await bankAccountService.GetBankAccount();
        }
        public async Task<Result> GetBankAccount(long bankAccountId)
        {
            BankAccountService bankAccountService = new BankAccountService();
            return await bankAccountService.GetBankAccount(bankAccountId);
        }
        public async Task<Result> GetCustomer()
        {
            CustomerService customerService = new CustomerService();
            return await customerService.GetCustomers();
        }
        public async Task<Result> GetCustomer(long customerId)
        {
            CustomerService customerService = new CustomerService();
            return await customerService.GetCustomerById(customerId);
        }
        public async Task<Result> GetCustomer(string TIN)
        {
            CustomerService customerService = new CustomerService();
            return await customerService.GetCustomerByTIN(TIN);
        }
        public async Task<Result> GetCustomerByEmail(string email)
        {
            CustomerService customerService = new CustomerService();
            return await customerService.GetCustomersByEmail(email);
        }
        public async Task<Result> AddCustomer(Customer customer)
        {
            CustomerService customerService = new CustomerService();
            return await customerService.AddCustomer(customer);
        }
        public async Task<Result> ModifyCustomer(Customer customer)
        {
            CustomerService customerService = new CustomerService();
            return await customerService.ModifyCustomer(customer);
        }
        public async Task<Result> DeleteCustomer(long customerId)
        {
            CustomerService customerService = new CustomerService();
            return await customerService.DeleteCustomer(customerId);
        }

        public async Task<Result> GetVatRates()
        {
            DictionaryDataService dictionaryDataService = new DictionaryDataService();
            return await dictionaryDataService.GetVatRates();
        }
        public async Task<Result> GetCountries()
        {
            DictionaryDataService dictionaryDataService = new DictionaryDataService();
            return await dictionaryDataService.GetCountries();
        }

        public async Task<Result> GetPaymentMethod()
        {
            PaymentMethodService paymentMethodService = new PaymentMethodService();
            return await paymentMethodService.GetPaymentMethod();
        }
        public async Task<Result> GetPaymentMethod(long paymentMethodId)
        {
            PaymentMethodService paymentMethodService = new PaymentMethodService();
            return await paymentMethodService.GetPaymentMethod(paymentMethodId);
        }
        public async Task<Result> GetPrintTemplates()
        {
            PrintService printService = new PrintService();
            return await printService.GetPrintTemplates();
        }
        public async Task<Result> GetInvoicePrintByCustomer(long invoiceId)
        {
            PrintService printService = new PrintService();
            return await printService.GetInvoicePrintByCustomer(invoiceId);
        }
        public async Task<Result> GetProformaPrintByCustomer(long invoiceId)
        {
            PrintService printService = new PrintService();
            return await printService.GetProformaPrintByCustomer(invoiceId);
        }
        public async Task<Result> GetInvoiceCustomPrint(long invoiceId, long printTemplateId)
        {
            PrintService printService = new PrintService();
            return await printService.GetInvoiceCustomPrint(invoiceId, printTemplateId);
        }
        public async Task<Result> GetProformaCustomPrint(long invoiceId, long printTemplateId)
        {
            PrintService printService = new PrintService();
            return await printService.GetProformaCustomPrint(invoiceId, printTemplateId);
        }
        public async Task<Result> SavePrintToFile(string base64Print, string pathToSave)
        {
            PrintService printService = new PrintService();
            return await printService.SavePrintToFile(base64Print, pathToSave);
        }
        public async Task<Result> GetProduct()
        {
            ProductService productService = new ProductService();
            return await productService.GetProduct();
        }
        public async Task<Result> GetProduct(long productId)
        {
            ProductService productService = new ProductService();
            return await productService.GetProduct(productId);
        }
        public async Task<Result> AddProduct(Product product)
        {
            ProductService productService = new ProductService();
            return await productService.AddProduct(product);
        }
        public async Task<Result> ModifyProduct(Product product)
        {
            ProductService productService = new ProductService();
            return await productService.ModifyProduct(product);
        }
        public async Task<Result> DeleteProduct(long productId)
        {
            ProductService productService = new ProductService();
            return await productService.DeleteProduct(productId);
        }
        public async Task<Result> GetProformaInvoice()
        {
            ProformaInvoiceService proformaInvoiceService = new ProformaInvoiceService();
            return await proformaInvoiceService.GetProformaInvoice();
        }
        public async Task<Result> GetProformaInvoice(long invoiceId)
        {
            ProformaInvoiceService proformaInvoiceService = new ProformaInvoiceService();
            return await proformaInvoiceService.GetProformaInvoice(invoiceId);
        }

        public async Task<Result> GetProformaInvoice(bool converted)
        {
            ProformaInvoiceService proformaInvoiceService = new ProformaInvoiceService();
            return await proformaInvoiceService.GetProformasFiltered(converted);
        }
        public async Task<Result> AddProformaInvoice(ProformaInvoice proformaInvoice)
        {
            ProformaInvoiceService proformaInvoiceService = new ProformaInvoiceService();
            return await proformaInvoiceService.AddProformaInvoice(proformaInvoice);
        }
        public async Task<Result> ModifyProformaInvoice(ProformaInvoice proformaInvoice)
        {
            ProformaInvoiceService proformaInvoiceService = new ProformaInvoiceService();
            return await proformaInvoiceService.ModifyProformaInvoice(proformaInvoice);
        }
        public async Task<Result> DeleteProformaInvoice(long invoiceId)
        {
            ProformaInvoiceService proformaInvoiceService = new ProformaInvoiceService();
            return await proformaInvoiceService.DeleteProformaInvoice(invoiceId);
        }
        public async Task<Result> GetSalesInvoice()
        {
            SalesInvoiceService salesInvoiceService = new SalesInvoiceService();
            return await salesInvoiceService.GetSalesInvoice();
        }
        public async Task<Result> GetSalesInvoice(long invoiceId)
        {
            SalesInvoiceService salesInvoiceService = new SalesInvoiceService();
            return await salesInvoiceService.GetSalesInvoice(invoiceId);
        }
        public async Task<Result> GetSalesInvoice(string invoiceNumber)
        {
            SalesInvoiceService salesInvoiceService = new SalesInvoiceService();
            return await salesInvoiceService.GetSalesInvoice(invoiceNumber);
        }
        public async Task<Result> GetLastInvoices(int numberOfInvoices)
        {
            SalesInvoiceService salesInvoiceService = new SalesInvoiceService();
            return await salesInvoiceService.GetLastInvoices(numberOfInvoices);
        }
        public async Task<Result> AddSalesInvoice(SalesInvoice salesInvoice)
        {
            SalesInvoiceService salesInvoiceService = new SalesInvoiceService();
            return await salesInvoiceService.AddSalesInvoice(salesInvoice);
        }
        public async Task<Result> ModifySalesInvoice(SalesInvoice salesInvoice)
        {
            SalesInvoiceService salesInvoiceService = new SalesInvoiceService();
            return await salesInvoiceService.ModifySalesInvoice(salesInvoice);
        }
        public async Task<Result> DeleteSalesInvoice(long invoiceId)
        {
            SalesInvoiceService salesInvoiceService = new SalesInvoiceService();
            return await salesInvoiceService.DeleteSalesInvoice(invoiceId);
        }
    }
}

