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

//Question 1: Where Clause with OrderBy and Anonymous Data Set
//Context: "The pricing team analyzes low-cost items and must identify all products priced below $10. They want to see the product label, name, and unit price for these items, ordered alphabetically by price and product name."
//Question: "How would you filter the Product table to retrieve products with a unit price less than $10, order them by price and product name in ascending order, and return the results as an anonymous data set that includes the product label, product name, and unit price?"
Products
	.Where(x => x.UnitPrice < 10)
	.Select(x => new
	{
		ProductLabel = x.ProductLabel,
		ProductName = x.ProductName,
		UnitPrice = x.UnitPrice
	})
	.OrderBy(x => x.UnitPrice)
	.ThenBy(x => x.ProductName)
	.Dump();
		
//QuestFirstNameion 2: Where Clause with Anonymous Data Set and Navigational Property
//Context: "Our marketing team needs to identify customers who live in British Columbia, Canada, to target them for a new regional promotion. We want to include their first name, last name, and city name in the order of the results by city then by last name."
//Question: "How would you filter the Customer table to retrieve customers in British Columbia, Canada, and include their first name, last name, and associated city name ordered by city then by last name (from the Geography table) as an anonymous data set?"
Customers
	.Where(x => x.Geography.RegionCountryName == "Canada" &&
			x.Geography.StateProvinceName == "British Columbia")
	.Select(x => new
	{
		FirstName = x.FirstName,
		LastName = x.LastName,
		CityName = x.Geography.CityName
	})
	.OrderBy(x => x.CityName)
	.ThenBy(x => x.LastName)
	.Dump();
	
//Question 3: Navigational Properties & Anonymous Data Set
//Context: "The product management team focuses on audio-related products and wants to analyze those that fall under the 'Audio' category. Specifically, they want products within the 'Recording Pen' or 'Bluetooth Headphones' subcategory that are 'Pink.' The list should include the category name, subcategory name, and product name."
//Question: "How would you filter the Products table to retrieve products that are 'Pink' and belong to the 'Audio' category and any of the subcategories 'Recording Pen' or 'Bluetooth Headphones' and return the results as an anonymous data set with the product name, subcategory name, and category name?"
Products
	.Where(x => x.ProductSubcategory.ProductCategory.ProductCategoryName == "Audio" &&
		x.ColorName == "Pink" &&
		(x.ProductSubcategory.ProductSubcategoryName == "Recording Pen" ||
		x.ProductSubcategory.ProductSubcategoryName == "Bluetooth Headphones"))
	.Select(x => new
	{
		CategoryName = x.ProductSubcategory.ProductCategory.ProductCategoryName,
		SubcategoryName = x.ProductSubcategory.ProductSubcategoryName,
		ProductName = x.ProductName
	})
	.OrderBy(x => x.SubcategoryName)
	.ThenBy(x => x.ProductName)
	.Dump();
	
//Question 4: Navigational Properties & Anonymous Data Set
//Context: "The sales department is analyzing invoices from European customers. They need a list of all invoices for European customers, along with the invoice number, date, customer name, city, and country. The results should be ordered alphabetically by city."
//Question: "How would you filter the Invoices table to retrieve invoices for customers located in Europe and return the results as an anonymous data set that includes the invoice number, invoice date, customer name, city, and country, ordered by city?"
Invoices
	.Where(x => x.Customer.Geography.ContinentName == "Europe")
	.Select(x => new
	{
		InvoiceNo = x.InvoiceID,
		InvoiceDate = x.DateKey.Date.ToString(),
		InvoiceDate2 = $"{x.DateKey.Year}/{(x.DateKey.Month < 10 ? "0" + x.DateKey.Month : x.DateKey.Month.ToString())}/{(x.DateKey.Day < 10 ? "0" + x.DateKey.Day : x.DateKey.Day.ToString())}",
		CustomerName = x.Customer.FirstName + " " + x.Customer.LastName,
		CustomerName2 = $"{x.Customer.FirstName} {x.Customer.LastName}",
		City = x.Customer.Geography.CityName,
		Country = x.Customer.Geography.RegionCountryName
	})
	.OrderBy(x => x.City)
	.Dump();
	