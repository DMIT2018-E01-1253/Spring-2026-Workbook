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

//1 - Where Clauses
//Question 1: Single Where Clause, All Fields
//Context: "We need to identify all employees hired after January 1, 2022, to ensure they are included in our new training program."
//Question: "How would you filter the Employee table to retrieve these employees?"
Employees
	.Where(x => x.HireDate.Value > (new DateOnly(2022, 01, 01)))
	.Select(x => x)
	.Dump();
	
//Question 2: Single Where Clause, All Fields
//Context: "Our inventory team wants to find all products that have been available for sale since July 1, 2019, to ensure they are properly stocked."
//Question: "How would you filter the Product table to retrieve these products?"
Products
	.Where(x => x.AvailableForSaleDate.Value >= (new DateTime(2019, 07, 01)))
	.Select(x => x)
	.Dump();
	
//Question 3: Multiple Where Clauses
//Context: "To update our customer database, we need to pull the email addresses of all customers with a yearly income between $60,000 & $61,000."
//Question: "How would you filter the Customer table and retrieve only the email addresses of these customers?"	
Customers
	.Where(x => x.YearlyIncome > 60000 && x.YearlyIncome < 61000)
	.Select(x => x.EmailAddress)
	.Dump();
	
//Question 4: Filtering using Contain
//Context: "The marketing department needs a list of all promotions focused on North America to prepare for the upcoming sale."
//Question: "How would you filter the Promotion table to retrieve the promotion information?"
Promotions
	.Where(x => x.PromotionName.ToUpper().Contains("NORTH AMERICA"))
	.Select(x => x)
	.Dump();
Promotions
	.Where(x => x.PromotionName.ToLower().Contains("north america"))
	.Select(x => x)
	.Dump();
Promotions
	.Where(x => x.PromotionName.Contains("north america"))
	.Select(x => x)
	.Dump();
	