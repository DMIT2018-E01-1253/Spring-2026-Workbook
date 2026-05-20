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
//Context: "The marketing department is planning a special promotion for Secretary Week to show appreciation to customers who work in clerical roles. They want to target single customers with less than $51,000 annually. To personalize the promotion, they need a list of these customers, including their full names, yearly income, and the number of children they have. The results should be ordered alphabetically by the customers' last names."
//Question: "How would you filter the Customers table to retrieve the clerical staff who are single and earn less than $51,000, and return the results as an anonymous data set that includes their full name, yearly income, and number of children, ordered by last name?"
Customers
	.Where(x => x.Occupation == "Clerical" &&
			x.MaritalStatus == "S" &&
			x.YearlyIncome < 51000)
	.OrderBy(x => x.LastName)
	.Select(x => new
	{
		Name = $"{x.FirstName} {x.LastName}",
		Income = x.YearlyIncome,
		Childrens = x.TotalChildren
	})
	.Dump();
	
//Question 2: Where Clause with OrderBy and Anonymous Data Set
//Context: "As part of their Secretary Week campaign, the marketing department wants to understand where their clerical customers are to tailor regional promotions. They specifically target single customers who work in clerical roles and earn less than $51,000 annually. The marketing team must gather customer data, including their full name, yearly income, number of children, city, and country. The results should be ordered first by country and then by city."
//Question: "How would you filter the Customers table to retrieve clerical staff who are single and earn less than $51,000, and return the results as an anonymous data set that includes their full name, yearly income, number of children, city, and country, ordered by country and then by city?"
Customers
	.Where(x => x.Occupation == "Clerical" &&
			x.MaritalStatus == "S" &&
			x.YearlyIncome < 51000)
	.Select(x => new
	{
		Name = $"{x.FirstName} {x.LastName}",
		Income = x.YearlyIncome,
		Childrens = x.TotalChildren,
		City = x.Geography.CityName,
		Country = x.Geography.RegionCountryName
	})
	.OrderBy(x => x.Country)
	.OrderBy(x => x.City)
	.Dump();

//Question 3: Where Clause with OrderBy and Anonymous Data Set
//Context: "The retail chain is concerned about the high number of product returns that occurred in February 2023. They want to analyze all return transactions where more than six items were returned. The company is particularly interested in understanding which products were involved, which stores processed these returns, and which customers were making these returns. The results should be organized by the date of the original transaction and further sorted by the invoice ID."
//Question: "How would you filter the InvoiceLines table to retrieve data on returns where more than six items were returned in February 2023, and return the results as an anonymous data set that includes the invoice date, customer name, invoice ID, store name, product ID, product name, quantity returned, and total return amount, ordered by invoice date and then by invoice ID?"
InvoiceLines
	.Where(x => x.ReturnQuantity > 6 &&
			x.Invoice.DateKey.Year == 2023 &&
			x.Invoice.DateKey.Month == 2)
	.Select(x => new
	{
		InvoiceDate = x.Invoice.DateKey.Month + "/" + x.Invoice.DateKey.Day + "/" + x.Invoice.DateKey.Year,
		Customer = $"{x.Invoice.Customer.FirstName} {x.Invoice.Customer.LastName}",
		InvoiceID = x.InvoiceID,
		Store = x.Invoice.Store.StoreName,
		ProducID = x.ProductID,
		ProductName = x.Product.ProductName,
		Qty = x.ReturnQuantity,
		TotalReturn = x.ReturnAmount
	})
	.OrderBy(x => x.InvoiceDate)
	.ThenBy(x => x.InvoiceID)
	.Dump();

