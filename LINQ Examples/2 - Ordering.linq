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

//2 - Ordering
//Question 1: Single Where Clause, Ordered by Last name
//Context: "We need to identify all employees hired after January 1, 2022, to ensure they are included in our new training program."
//Question: "How would you filter the Employee table to retrieve these employees, ordered by last name?"
Employees
	.Where(x => x.HireDate.Value > (new DateOnly(2022, 01, 01)))
	.Select(x => x)
	.OrderBy(x => x.LastName)
	.Dump();
	
//Question 2: Single Where Clause, Ordered by Product Label Descending
//Context: "Our inventory team wants to find all products that have been available for sale since July 1, 2019, to ensure they are properly stocked."
//Question: "How would you filter the Product table to retrieve these products, ordered by product label descending?"
Products
	.Where(x => x.AvailableForSaleDate.Value >= (new DateTime(2019, 07, 01)))
	.Select(x => x)
	.OrderByDescending(x => x.ProductLabel)
	.Dump();

//Question 3: Multiple Where Clauses, Ordered by Email Address
//Context: "To update our customer database, we need to pull the email addresses of all customers with a yearly income between $60,000 and $61,000." NOTE: Order by must follow the where clause but before the select.
//Question: "How would you filter the Customer table and retrieve only the email addresses of these customers, ordered by email address?"	
Customers
	.Where(x => x.YearlyIncome > 60000 && x.YearlyIncome < 61000)
	.OrderBy(x => x.EmailAddress)
	.Select(x => x.EmailAddress)
	.Dump();
	
Customers
	.Where(x => x.YearlyIncome > 60000 && x.YearlyIncome < 61000)
	.Select(x => x.EmailAddress)
	.OrderBy(x => x)
	.Dump();
	
//Question 4: Filtering using Contains, Ordered by Promotion Name and Start Date
//Context: "The marketing department needs a list of all promotions focused on North America to prepare for the upcoming sale."
//Question: "How would you filter the Promotion table to retrieve the promotion information, ordered by promotion name?"
Promotions
	.Where(x => x.PromotionName.Contains("North America"))
	.Select(x => x)
	.OrderBy(x => x.PromotionName)
	.ThenBy(x => x.StartDate)
	.Dump();