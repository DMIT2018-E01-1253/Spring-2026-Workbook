<Query Kind="Program">
  <Connection>
    <ID>912e6aee-9ddb-44d8-842c-9b600fcce726</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>(local)</Server>
    <Database>Contoso</Database>
    <DisplayName>Contoso</DisplayName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	GetProductCategories().Dump();
	GetInvoicesWithDetails("Torres").Dump();
}

// You can define other methods, fields, classes and namespaces here
public List<ProductCategorySummaryView> GetProductCategories()
{
	return ProductCategories
			.Select(x => new ProductCategorySummaryView
			{
				ProductCategoryName = x.ProductCategoryName,
				SubCategories = x.ProductSubcategories
					.Select(sc => new ProductSubcategorySummaryView
					{
						SubCategoryName = sc.ProductSubcategoryName,
						Description = sc.ProductSubcategoryDescription
					})
					.OrderBy(sc => sc.SubCategoryName)
					.ToList()
			})
			.OrderBy(x => x.ProductCategoryName)
			.ToList();
}

public List<InvoiceView> GetInvoicesWithDetails(string lastName)
{
	//Implicit conversion
	int intNum = 6;
	decimal decNum = intNum; 
	intNum.ToString("N2").Dump();
	double dNum = 6.7d;
	dNum.Dump();

	return Invoices
			.Where(x => x.Customer.LastName.Contains(lastName))
			.Select(x => new InvoiceView
			{
				InvoiceNo = x.InvoiceID,
				InvoiceDate = $"{x.DateKey.Month}/{x.DateKey.Day}/{x.DateKey.Year}",
				Customer = $"{x.Customer.FirstName} {x.Customer.LastName}",
				Amount = x.TotalAmount,
				//Number Format
				//Amount2 = x.TotalAmount.ToString("N6"),
				//Amount2 = $"${x.TotalAmount.ToString("N2")}",
				//Currency Format
				//Amount2 = x.TotalAmount.ToString("C2"),
				//Amount2 = $"{x.TotalAmount.ToString("P2")}%",
				//Percentage Format
				//Amount2 = x.TotalAmount.ToString("P2"),
				//Optional digit
				//Amount3 = x.TotalAmount.ToString("#,###.####"),
				//Required digit
				//Amount4 = x.TotalAmount.ToString("0,000.0000"),
				//Combination of Optional and Required digit
				//Amount4 = x.TotalAmount.ToString("#,##0.0000"),
				//Literal
				//Amount5 = 6.7m,
				//Explicit conversion
				//Amount5 = (decimal)6.7,
				Details = x.InvoiceLines
					.Select(il => new InvoiceLineView
					{
						LineReference = il.InvoiceLineID,
						ProductName = il.Product.ProductName,
						Qty = il.SalesQuantity > 0 ? il.SalesQuantity : il.ReturnQuantity * -1,
						Price = il.SalesQuantity > 0 ? il.UnitPrice : il.UnitPrice * -1,
						Discount = il.SalesQuantity > 0 ? il.DiscountAmount : il.DiscountAmount * -1,
						ExtPrice = il.SalesQuantity > 0 ? il.SalesAmount : il.ReturnAmount * -1
					})
					.OrderBy(il => il.LineReference)
					.ToList()
			})
			.ToList();
}

public class ProductCategorySummaryView
{
	public string ProductCategoryName { get; set; }
	public List<ProductSubcategorySummaryView> SubCategories { get; set; }
}

public class ProductSubcategorySummaryView
{
	public string SubCategoryName { get; set; }
	public string Description { get; set; }
}

public class InvoiceView
{
	public int InvoiceNo { get; set; }
	public string InvoiceDate { get; set; }
	public string Customer { get; set; }
	public decimal Amount { get; set; }
	public string Amount2 { get; set; }
	public string Amount3 { get; set; }
	public string Amount4 { get; set; }
	public decimal Amount5 { get; set; }
	public List<InvoiceLineView> Details { get; set; }
}

public class InvoiceLineView
{
	public int LineReference { get; set; }
	public string ProductName { get; set; }
	public int Qty { get; set; }
	public decimal? Price { get; set; }
	public decimal? Discount { get; set; }
	public decimal? ExtPrice { get; set; }
}