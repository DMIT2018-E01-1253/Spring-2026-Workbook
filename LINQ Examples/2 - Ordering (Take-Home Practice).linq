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

//Question 1: Single Where Clause, Ordered by Invoice Date
//Context: "We need to review all invoices created after November 2023 to ensure they were processed correctly."
//Question: "How would you filter the Invoice table to retrieve these invoices, ordered by invoice date?"
Invoices
  .Where(x => x.DateKey > new DateTime(2023, 11, 30))
  .Select(x => x)
  .OrderBy(x => x.DateKey)
  .Dump();

//Question 2: Single Where Clause, Ordered by Geography
//Context: "Our regional analysis team needs to focus on all sales territories located in Canada for a new market expansion project."
//Question: "How would you filter the Geography table to retrieve all records where the country is Canada, ordered by geography?"
Geographies
  .Where(x => x.RegionCountryName == "Canada")
  .OrderBy(x => x.GeographyType)
  .Dump();

//Question 3: Multiple Field Selection, Ordered by City
//Context: "After reviewing the previous data output, we noticed records with GeographyType labeled as 'Country/Region.' For our detailed analysis, we only want to focus on cities located in Canada."
//Question: "How would you filter the Geography table to retrieve records where the Type 'City' is ordered by province and then city?"
Geographies
  .Where(x => x.RegionCountryName == "Canada" &&
	x.GeographyType == "City")
  .OrderBy(x => x.StateProvinceName)
  .ThenBy(x => x.CityName)
  .Dump();

//Question 4: Filtering using Contains, Ordered by Selling Area Size, Employee Count from largest to smallest and finally Store Name
//Context: "There has been some confusion with store names that include the term 'No.' in them, which might indicate store numbers or branches. We need to identify all stores with 'No.' in their names to review their details and address any inconsistencies."
//Question: "How would you filter the Store table to retrieve all records where the StoreName contains 'No.', ordered by selling area size and then employee count descending and then store name?"
Stores
  .Where(x => x.StoreName.Contains("No."))
  .Select(x => x)
  .OrderBy(x => x.SellingAreaSize)
  .ThenByDescending(x => x.EmployeeCount)
  .ThenBy(x => x.StoreName)
  .Dump();

