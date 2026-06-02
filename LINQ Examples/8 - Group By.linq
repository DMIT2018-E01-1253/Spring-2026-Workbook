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

//Question 1: Group By (Simple)
//Context: The product management team at Contoso Corporation is working on a project to better organize their product catalog. They need to categorize all product subcategories under their respective categories to streamline their catalog structure, making it easier for customers to find and purchase products. The team requires a comprehensive list that groups product subcategories by their parent category and orders both categories and subcategories alphabetically.
//Objective: "How would you write a query to group product subcategories by their parent category? The query should return the CategoryName and a list of ProductSubcategories that includes the SubCategoryName. Each subcategory should be ordered alphabetically by category name and subcategory name."
ProductSubcategories
	.GroupBy(x => x.ProductCategory.ProductCategoryName)
	//.GroupBy(x => new
	//{
	//	x.ProductCategory.ProductCategoryName
	//})
	//.GroupBy(x => new
	//{
	//	CategoryName = x.ProductCategory.ProductCategoryName
	//})
	.Select(g => new
	{
		CategoryName = g.Key,
		ProductSubcategories = g.Select(ps => new
			{
				SubcategoryName = ps.ProductSubcategoryName
			})
			.OrderBy(ps => ps.SubcategoryName)
	})
	.OrderBy(g => g.CategoryName)
	.ToList()
	.Dump();
	
	

//Question 2: Group By (Complex)
//Context: The product management team at Contoso Corporation is analyzing sales data to understand which products contribute most to each category and subcategory. By grouping invoice lines by product category and subcategory, the team aims to identify trends and patterns in product sales, which will help them make informed decisions about future product development and inventory management.
//Objective: "How would you write a query to group invoice lines by product category and subcategory? The query should return the CategoryName, SubcategoryName, and a list of invoices that include the InvoiceID, Product, and Amount. For each product, order by category name, subcategory name, and finally by product name."
InvoiceLines
	.GroupBy(x => new
	{
		CategoryName = x.Product.ProductSubcategory.ProductCategory.ProductCategoryName,
		SubcategoryName = x.Product.ProductSubcategory.ProductSubcategoryName,
	})
	.Select(g => new
	{
		CategoryName = g.Key.CategoryName,
		SubcategoryName = g.Key.SubcategoryName,
		Invoices = g.Select(i => new
			{
				InvoiceID = i.InvoiceID,
				Product = i.Product.ProductName,
				Qty = i.SalesQuantity
			})
			.OrderBy(i => i.Product)
	})
	.OrderBy(g => g.CategoryName)
	.ThenBy(g => g.SubcategoryName)
	.Dump();