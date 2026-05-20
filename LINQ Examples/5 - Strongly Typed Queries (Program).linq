<Query Kind="Program">
  <Connection>
    <ID>2837cd29-a98c-4fc4-b5d3-5efbababb91f</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>(local)</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <UseMicrosoftDataSqlClient>true</UseMicrosoftDataSqlClient>
    <DisplayName>Contoso</DisplayName>
    <EncryptTraffic>true</EncryptTraffic>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Contoso</Database>
    <MapXmlToString>false</MapXmlToString>
    <DriverData>
      <SkipCertificateCheck>true</SkipCertificateCheck>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	GetEmployeeReview("al", 30).Dump();
	GetProductColorProcess("Music").Dump();
}

// You can define other methods, fields, classes and namespaces here

public List<EmployeeView> GetEmployeeReview(string lastName, decimal baseRate)
{
	return Employees
			.Where(x => x.LastName.Contains("al"))
			.OrderBy(x => x.LastName)
			.Select(x => new EmployeeView
			{
				FullName = $"{x.FirstName} {x.LastName}",
				Department = x.DepartmentName,
				IncomeCategory = x.BaseRate < 30 ?
							"Required Review" : "No Review Required"
			})
			.ToList();
}

public List<ProductColorProcessView> GetProductColorProcess(string categoryName)
{
	return Products
			.Where(x => x.ProductSubcategory.ProductCategory.ProductCategoryName.Contains(categoryName))
			.OrderBy(x => x.StyleName)
			.Select(x => new ProductColorProcessView
			{
				ProductName = x.ProductName,
				Color = x.ColorName,
				ColorProcessNeeded = (x.ColorName == "Black" || x.ColorName == "White") ?
								"No" : "Yes"
			})
			.ToList();
}

public class EmployeeView
{
	public string FullName { get; set; }
	public string Department { get; set; }
	public string IncomeCategory { get; set; }
}

public class ProductColorProcessView
{
	public string ProductName { get; set; }
	public string Color { get; set; }
	public string ColorProcessNeeded { get; set; }
}