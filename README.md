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

Methods are really simple and readable. I will write some examples below but if you need more information, you can ask me by creating issue :)

### Get list of products
```csharp
var getProducts = await erpxt.GetProduct()
```
### Get product by ID
```csharp
long productId = 12345678;
var getProduct = await erpxt.GetProduct(productId)
```
### Add product
```csharp
Product product = new Product()
product.Name = "Test Product";
product.Description = "Best description ever!";
product.UnitOfMeasure = "piece";
product.Rate = RateEnum.TWENTY_THREE_PERCENT;
var addProduct = await erpxt.AddProduct(product);
```
### Get customer by ID
```csharp
long customerId = 12349876;
var getCustomer = await erpxt.GetCustomer(customerId);
```

### Get customer by Email
```csharp
string email = "test@mail.com";
var getCustomer = await erpxt.GetCustomerByEmail(email);
```
### Add customer
```csharp
Customer customer = new Customer();
customer.Name = "Comarch S.A";
customer.CountryCode = "PL";
customer.CustomerStatus = CustomerStatusEnum.DOMESTIC;
customer.Mail = "erpxt@comarch.pl";
customer.Address.BuildingNumber = "17";
customer.Address.Street = "Dworcowa";
customer.Address.PostalCode = "222-232";
customer.Address.City = "Warsaw";
var addCustomer = await erpxt.AddCustomer(customer);
```

### Modify customer
```csharp
Customer customer = new Customer();
customer.Id = 334121;
customer.Name = "Allegro S.A";
customer.Phone = "123-456-789";
var modifyCustomer = await erpxt.ModifyCustomer(customer);
```
### Delete customer
```csharp
long customerId = 777666;
var deleteCustomer = await erpxt.DeleteCustomer(customerId);
```
### Get list of payment methods
```csharp
var getPaymentMethods = await erpxt.GetPaymentMethod();
```

### Get bank accounts
```csharp
var getBankAccounts = await erpxt.GetBankAccount();
```
### Get specific bank account
```csharp
long bankId = 551551;
var getBankAccount = await erpxt.GetBankAccount(bankId);
```
### Get sales invoice by number
```csharp
string number = "FA/1/2022";
var getInvoice = await erpxt.GetSalesInvoice(number);
```

### Add sales invoice
```csharp
SalesInvoice invoice = new SalesInvoice();
invoice.PaymentStatus = PaymentStatusEnum.PAID;
invoice.PurchasingPartyId = 13604916;
invoice.PaymentTypeId = 10199422;
invoice.Items.AddRange(new List<Item>
{
new Item() { ProductId = 11430127, Quantity = 99, ProductCurrencyPrice = 15.52M },
new Item() { ProductId = 10578173, Quantity = 1, ProductCurrencyPrice = 25.00M, ProductDescription = "Hello2" }
});
var addInvoice = await erpxt.AddSalesInvoice(invoice);
```
### Save print to file
```csharp
long invoiceId = 555111;
long printTemplateId = 5;
string path = "C:/prints/";
var printData= await erpxt.GetInvoiceCustomPrint(invoiceId, printTemplateId);
erpxt.SavePrintToFile(printData, path);
```
### Get converted ProForma Invoices
```csharp
var proformas = await erpxt.GetProformasFiltered(true) // if false, you will get non converted
```
### Get last Sales Invoices depends on issue date 
```csharp
var invoices = await erpxt.GetLastInvoices(10); // You will get last 10 invoices;
```
and more more more ... :)
## License
[MIT](https://choosealicense.com/licenses/mit/)
