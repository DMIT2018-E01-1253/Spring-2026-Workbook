<Query Kind="Statements">
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

//Question 1: Strongly Typed Queries
//Context: "The HR department at Contoso Corporation is conducting a customized salary review, allowing them to target specific groups of employees based on both their last name and their hourly wage. This approach helps HR quickly identify employees who might be underpaid. For this task, they need to generate a list of employees whose last names match a given search term and whose base rate is below a specified threshold. The HR team will provide these parameters to ensure flexibility in their search. The list should include the employee's full name, department, and an income category indicating whether their salary requires a review. The results should be ordered alphabetically by last name."
//Objective: "Create a method that retrieves employee records based on a search for last names and a base rate threshold. The method should take two parameters—lastName and baseRate—and return a strongly typed list of EmployeeView objects, containing the employee's full name, department, and income category, ordered by last name."
Employees
	.Where(x => x.LastName.Contains("al"))
	.OrderBy(x => x.LastName)
	.Select(x => new EmployeeView
	{
		FullName = $"{x.FirstName} {x.LastName}",
		Department = x.DepartmentName,
		IncomeCategory = x.BaseRate < 30 ?
					"Required Review" : "No Review Required"
	})
	.ToList()
	.Dump();


//Question 2: Strongly Typed Queries
//Context: "The production team at Contoso Corporation needs to review their product line to determine which products require additional color processing. The team is particularly interested in products within specific categories, which they will specify as needed. For this task, the production team will provide a category name to search for products within that category. They need to identify whether each product's color requires additional processing, with black and white colors not needing further processing. The results should be organized by the product's style name to facilitate the review process."
//Objective: "Create a method that retrieves product records based on a category name search. The method should take a categoryName parameter and return a strongly typed list of ProductColorProcessView objects, containing the product name, color, and whether additional color processing is needed, ordered by the product's style name."
Products
	.Where(x => x.ProductSubcategory.ProductCategory.ProductCategoryName.Contains("Music"))
	.OrderBy(x => x.StyleName)
	.Select(x => new ProductColorProcessView
	{
		ProductName = x.ProductName,
		Color = x.ColorName,
		ColorProcessNeeded = (x.ColorName == "Black" || x.ColorName == "White") ?
						"No" : "Yes"
	})
	.ToList()
	.Dump();



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
