# # ERPXT.pl - .NET Helper library

A library that is to facilitate the use of the [ERP XT](https://www.erpxt.pl) API without unnecessary implementation.

[Nuget](https://www.nuget.org/packages/ERPXTpl/)

## Requirements

You need to have ClientId and SecretKey to authorize API server. More information about how to get this data you can found on this page [ERPXT-Authorization](https://pomoc.erpxt.pl/dokumentacja/api-autoryzacja/)

## Installation
### Package Manager
```
Install-Package ERPXTpl
```
### .NET CLI
```
dotnet add package ERPXTpl
```
or just find library in Nuget Package Manager in Visual Studio ;)

## Usage
You need to create a main object that takes two parameters - ClientId and SecretKey.

```csharp
ERPXT erpxt = new ERPXT(clientId, secretKey);
```
The token is saved in the memory cache. Prior to each request to the API, its validity is checked. If the token is expired, the server is asked for a new token which is saved to the cache.

Each request is wrapped in a Result object. Result consists of Data, StatusCode, and Message
```
Data = returned object from response.
StatusCode = HttpCode from response or "ERROR" if something went wrong before send request.
Message = Message from server if something went wrong or message from internal validator.
```

# Examples

Methods are really simple and readable but if you need more information, you can ask me by creating issue :)

## Bank Accounts
### Add Bank Account
```csharp
BankAccount bankAccount = new BankAccount();
bankAccount.AccountNumber = "51239841214300002211";
bankAccount.BankName = "Best Bank Ever";
bankAccount.Symbol = "BBE";
bankAccount.ReportingPeriod = ReportingPeriod.MONTHLY;
bankAccount.Primary = false;
var addBankAcc = await erp.AddBankAccount(bankAccount);
```
### Modify Bank Account
```csharp
BankAccount bankAccount = new BankAccount();
bankAccount.Id = 10176471;
bankAccount.AccountNumber = "44991234888800002244";
bankAccount.BankName = "Our Best Bank";
bankAccount.Symbol = "BBE";
bankAccount.ReportingPeriod = ReportingPeriod.MONTHLY;
var modifyBank = await erp.ModifyBankAccount(bankAccount);
```
### Take all Bank Accounts
```csharp
var bankList = await erp.GetBankAccount();
```
### Take a specific Bank Account based on Id
```csharp
var getBank = await erp.GetBankAccount(10176471);
```
### Delete Bank Account
```csharp
var deleteBank = await erp.DeleteBankAccount(10176471);
```

## Payment Methods
### Add Payment Method
```csharp
PaymentMethod paymentMethod = new PaymentMethod();
paymentMethod.Name = "FastPayOnline";
paymentMethod.Primary = false;
paymentMethod.Deadline = 14;
paymentMethod.Type = PaymentMethodType.ACCOUNT_PAYMENT;
var addPayment = await erp.AddPaymentMethod(paymentMethod);
```
### Modify Payment Method
```csharp
PaymentMethod paymentMethod = new PaymentMethod();
paymentMethod.Id = 10331712;
paymentMethod.Name = "2Fast2PayOnline";
paymentMethod.Primary = false;
paymentMethod.Deadline = 1;
paymentMethod.Type = PaymentMethodType.ACCOUNT_PAYMENT;
var modifyPaymentMethod = await erp.ModifyPaymentMethod(paymentMethod);
```
### Take list of Payment Methods
```csharp
var listOfPaymentMethods = await erp.GetPaymentMethod();
```
### Take specific Payment Method based on Id
```csharp
var getPaymentMethod = await erp.GetPaymentMethod(10331712);
```
### Delete Payment Method
```csharp
var deletePaymentMethod = await erp.DeletePaymentMethod(10331712);
```

## Products
### Add Product
```csharp
Product product = new Product();
product.Name = "Epic Premium Product";
product.Description = "Best description ever!";
product.ItemCode = "27.11.22.0";
product.ProductCode = "EPP";
product.UnitOfMeasurment = "piece";
product.SaleNetPrice = 139.99;
product.Rate = Rate.TWENTY_THREE_PERCENT;
var addProduct = await erp.AddProduct(product);
```

### Modify Product
```csharp
Product product = new Product();
product.Id = 11661992;
product.Name = "Epic Premium Product";
product.Description = "Useful information!";
product.ItemCode = "27.11.22.0";
product.ProductCode = "EPP/1";
product.UnitOfMeasurment = "packaging";
product.SaleGrossPrice = 169.99;
product.Rate = Rate.FIVE_PERCENT;
var modifyProduct = await erp.ModifyProduct(product);
```
### Take list of Products
```csharp
var listOfProducts = await erp.GetProduct();
```
### Take specific product based on Id
```csharp
var getProduct = await erp.GetProduct(11661992);
```
### Delete product
```csharp
var deleteProduct = await erp.DeleteProduct(11661992);
```
## Customers
### Add Customer
```csharp
Customer customer = new Customer();
customer.Name = "Comarch S.A";
customer.CountryCode = "PL";
customer.CustomerTaxNumber = "6770065406";
customer.CustomerCode = "Customer/PL/1";
customer.CustomerType = CustomerType.BUSINESS_ENTITY;
customer.CustomerStatus = CustomerStatus.DOMESTIC;
customer.Mail = "erpxt@comarch.pl";
customer.PhoneNumber = "126814300";
customer.Address.BuildingNumber = "39A";
customer.Address.FlatNumber = "1";
customer.Address.Street = "Al.Jana Paw³a II";
customer.Address.PostalCode = "31-864";
customer.Address.City = "Cracow";
var addCustomer = await erp.AddCustomer(customer);
```
### Modify Customer
```csharp
Customer customer = new Customer();
customer.Id = 13632471;
customer.Name = "Comarch Spolka Akcyjna";
customer.CountryCode = "PL";
customer.CustomerTaxNumber = "6770065406";
customer.CustomerCode = "Customer/PL/1";
customer.CustomerType = CustomerType.BUSINESS_ENTITY;
customer.CustomerStatus = CustomerStatus.DOMESTIC;
customer.Mail = "erpxt@comarch.pl";
customer.PhoneNumber = "126814300";
customer.Address.BuildingNumber = "39A";
customer.Address.FlatNumber = "3";
customer.Address.Street = "Al.Jana Paw³a II";
customer.Address.PostalCode = "31-864";
customer.Address.City = "Cracow";
var modifyCustomer = await erp.ModifyCustomer(customer);
```
### Take list of Customers
```csharp
var listOfCustomers = await erp.GetCustomer();
```
### Take specific Customer based on Id
```csharp
var getCustomer = await erp.GetCustomer(13632471);
```
### Take specific Customer based on TIN
```csharp
var getCustomer = await erp.GetCustomer("6770065406");
```
### Take specific Customer based on Email
```csharp
var getCustomer = await erp.GetCustomerByEmail("erpxt@comarch.pl"); // Can return more than one customer if few of them have same email
```
### Delete Customer
```csharp
var deleteCustomer = await erp.DeleteCustomer(13632471);
```
## OSS Procedure
### Take list of EU Country codes
```csharp
var listOfCodes = await erp.GetCountries();
```
### Take list of foreign VAT rates
```csharp
var listOfVatRates = await erp.GetVatRates();
```
## Sales Invoices
### Add Sales Invoice
```csharp
SalesInvoice invoice = new SalesInvoice();
invoice.PaymentStatus = PaymentStatus.PAID;
invoice.OSSProcedureCountryCode = "DE";
invoice.IsOSSProcedure = true;
invoice.PurchasingPartyId = 13632511;
invoice.ReceivingPartyId = 13632511;
invoice.PaymentTypeId = 10199423;
invoice.BankAccountId = 10176453;
invoice.IssueDate = "2022-01-01";
invoice.SalesDate = "2022-01-02";
invoice.InvoiceType = InvoiceType.TOTAL;
invoice.Description = "Invoice for our business partner.";
invoice.Items.AddRange(new List<Item>
{
     new Item() { ProductId = 11626227, Quantity = 99, ProductCurrencyPrice = 15.52M, VatRateId = 75 },
     new Item() { ProductId = 11290439, Quantity = 1, ProductCurrencyPrice = 25.00M, ProductDescription = "Hello2", VatRateId = 75 }
});
var addInvoice = await erp.AddSalesInvoice(invoice);
```
### Modify Sales Invoice
```csharp
SalesInvoice invoice = new SalesInvoice();
invoice.Id = 22386347;
invoice.PaymentStatus = PaymentStatus.UNPAID;
invoice.IsOSSProcedure = false;
invoice.PurchasingPartyId = 12466411;
invoice.ReceivingPartyId = 12466411;
invoice.PaymentTypeId = 10199423;
invoice.BankAccountId = 10176453;
invoice.IssueDate = "2022-01-01";
invoice.SalesDate = "2022-01-05";
invoice.InvoiceType = InvoiceType.TOTAL;
invoice.Description = "Send invoice by email";
invoice.Items.AddRange(new List<Item>
{
     new Item() { ProductId = 11626227, Quantity = 35, ProductCurrencyPrice = 12.21M},
     new Item() { ProductId = 11290439, Quantity = 7, ProductCurrencyPrice = 125.00M, ProductDescription = "Color RED",}
});
var modifyInvoice = await erp.ModifySalesInvoice(invoice);
```
### Take list of Sales Invoices
```csharp
var listOfInvoices = await erp.GetSalesInvoice();
```
### Take Sales Invoice based on Id
```csharp
var invoice = await erp.GetSalesInvoice(22386352);
```
### Take Sales Invoice based on Number
```csharp
var invoice = await erp.GetSalesInvoice("2022/1/FS/5")
```
### Get last X issued Sales Invoices
```csharp
var lastInvoices = await erp.GetLastInvoices(5); // Last 5 invoices
```
### Delete Sales Invoice
```csharp
var deleteInvoice = await erp.DeleteSalesInvoice(22386353);
```

## Proforma Invoices
### Add Proforma Invoice
```csharp
ProformaInvoice proforma = new ProformaInvoice();
proforma.OSSProcedureCountryCode = "DE";
proforma.IsOSSProcedure = true;
proforma.PurchasingPartyId = 13632511;
proforma.ReceivingPartyId = 13632511;
proforma.PaymentTypeId = 10199423;
proforma.BankAccountId = 10176453;
proforma.IssueDate = "2022-01-01";
proforma.InvoiceType = InvoiceType.SUBTOTAL;
proforma.Description = "Proforma with our best offer.";
proforma.Items.AddRange(new List<Item>
{
     new Item() { ProductId = 11626227, Quantity = 12, ProductCurrencyPrice = 88.11M, VatRateId = 75 },
     new Item() { ProductId = 11290439, Quantity = 3, ProductCurrencyPrice = 55.00M, ProductDescription = "Test description", VatRateId = 75 }
});
var addProforma = await erp.AddProformaInvoice(proforma);
```
### Modify Proforma Invoice
```csharp
ProformaInvoice proforma = new ProformaInvoice();
proforma.Id = 22386743;
proforma.OSSProcedureCountryCode = "DE";
proforma.IsOSSProcedure = true;
proforma.PurchasingPartyId = 13632511;
proforma.ReceivingPartyId = 13632511;
proforma.PaymentTypeId = 10199423;
proforma.BankAccountId = 10176453;
proforma.IssueDate = "2022-01-05";
proforma.InvoiceType = InvoiceType.SUBTOTAL;
proforma.Description = "Proforma with our worst offer.";
proforma.Items.AddRange(new List<Item>
{
     new Item() { ProductId = 11626227, Quantity = 12, ProductCurrencyPrice = 300.11M, VatRateId = 75 },
     new Item() { ProductId = 11290439, Quantity = 3, ProductCurrencyPrice = 155.00M, ProductDescription = "Sell with 100 percent margin", VatRateId = 75 }
});
var modifyProforma = await erp.ModifyProformaInvoice(proforma);
```
### Take all Proforma Invoices
```csharp
var listOfProforms = await erp.GetProformaInvoice();
```
### Take Proforma Invoice based on Id
```csharp
var getProforma = await erp.GetProformaInvoice(14745129);
```
### Take Proforma Invoice which are converted to other documents or not
```csharp
var getConvertedProforms = await erp.GetProformaInvoice(true); // if false, you will get unconverted proforms
```
### Delete Proforma Invoice
```csharp
var deleteProforma = await erp.DeleteProformaInvoice(22386743);
```
## Prints
### Take list of Custom Prints
```csharp
var listOfCustomPrints = await erp.GetPrintTemplates();
```
### Take default Sales Invoice print for Customer
```csharp
var defaultSalesPrintForCustomer = await erp.GetInvoicePrintByCustomer(22386352);
```
### Take custom Sales Invoice Print
```csharp
var customInvoicePrint = await erp.GetInvoiceCustomPrint(22386352, 10161077);
```
### Take default Proforma Invoice print for Customer
```csharp
var defaultProformaPrintForCustomer = await erp.GetProformaPrintByCustomer(15339936);
```
### Take custom Proforma Invoice Print
```csharp
var customProformaPrint = await erp.GetProformaCustomPrint(15339936, 10177498);
```
### Save print to PDF
```csharp
string base64Print = customProformaPrint.Data.ToString();
string path = "Z:\\OurCompany\\PrintsForCustomers\\";
var getPrintPdf = await erp.SavePrintToFile(base64Print, path);
```
## License
[MIT](https://choosealicense.com/licenses/mit/)
