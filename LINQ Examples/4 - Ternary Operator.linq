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

//Question 1: Ternary Operator
//Context: "The HR department at Contoso Corporation is in the middle of their annual salary review process. This year, they are focusing on employees who might be earning below industry standards, particularly those with a base rate of less than $30 per hour. The department aims to ensure fair compensation and recognize areas where pay adjustments may be necessary. To start the review, HR wants to compile a list of employees who might require a salary review. The list should include each employee's full name and department, and categorize them based on whether they require a review or not. The results should be sorted alphabetically by last name to make it easier for the HR team to navigate through the data."
//Question: "How would you filter the Employees table to retrieve those with a base rate of less than $30, and return the results as an anonymous data set that includes their full name, department, and whether they require a salary review, ordered by last name?"
Employees
	.OrderBy(x => x.LastName)
	.Select(x => new
	{
		FullName = $"{x.FirstName} {x.LastName}",
		Department = x.DepartmentName,
		IncomeCategory = x.BaseRate < 30 ? "Required Review" : "No Review Required"
	})
	.Dump();
	
//Question 2: Ternary Operator
//Context: "The product development team at Contoso Corporation is preparing to optimize the production process for their 'Music, Movies, and Audio Books' category. As part of this effort, they need to identify which products in this category require additional color processing. Products in black or white are typically ready for packaging without further processing, while other colors may need additional steps. To streamline their workflow, the team wants to compile a list of all products in this category, sorted by their style name. This list should include the product name, color, and an indicator of whether additional color processing is required."
//Question: "How would you filter the Products table to retrieve items in the 'Music, Movies, and Audio Books' category, and return the results as an anonymous data set that includes the product name, color, and whether color processing is needed, ordered by style name?"
Products
	.Where(x => x.ProductSubcategory.ProductCategory.ProductCategoryName == "Music, Movies and Audio Books")
	//.Where(x => x.ProductSubcategory.ProductCategory.ProductCategoryID == 6)
	.OrderBy(x => x.StyleName)
	.Select(x => new
	{
		ProductName = x.ProductName,
		Color = x.ColorName,
		ColorProcessNeeded = x.ColorName == "Black" || x.ColorName == "White" ?
							"No" : "Yes"
	})
	.Dump();

