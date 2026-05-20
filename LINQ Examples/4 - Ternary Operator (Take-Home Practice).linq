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
//Context: "The inventory management team at Contoso Corporation is focusing on the 'Cell phones' category to ensure that stores maintain adequate stock levels. They need to identify which stores are at risk of running out of inventory and may require a reorder. The team wants a report that lists each store, the specific cell phone products they carry, and whether a reorder is necessary. The reorder is required if the sum of the current on-hand quantity and on-order quantity is less than the safety stock quantity. This report should be organized by store ID to help the team prioritize which locations need immediate attention."
//Question: "How would you filter the Inventories table to retrieve records for products in the 'Cell phones' category, and return the results as an anonymous data set that includes the store ID, store name, product name, and whether a reorder is necessary, ordered by store ID, then by ProductName?"
Inventories 
	.Where(x => x.Product.ProductSubcategory.ProductCategory.ProductCategoryName == "Cell phones")
	.Select(x => new
	{
		StoreID = x.StoreID,
		StoreName = x.Store.StoreName,
		ProductName = x.Product.ProductName,
		Reorder = x.SafetyStockQuantity > x.OnHandQuantity + x.OnOrderQuantity ?
				"Yes" : "No"
	})
	.OrderBy(x => x.StoreID)
	.ThenBy(x => x.ProductName)
	.Dump();

//Question 2: Ternary Operator
//Context: "The finance department at Contoso Corporation is conducting an analysis of customer invoices to prioritize collection efforts. They are particularly interested in identifying high-value invoices—those with amounts exceeding $5,000—as these should be flagged as 'High Priority' for immediate follow-up. The department wants a report that lists all invoices, sorted alphabetically by the customer's last name, to ensure easy navigation. The report should include the invoice number, date, customer name, store name, the manager's location (represented by the city), and whether the invoice is considered high or low priority based on the total amount."
//Question: "How would you filter the Invoices table to retrieve all invoices, and return the results as an anonymous data set that includes the invoice number, date, customer name, store name, manager's city, and priority status, ordered by the customer's last name?"
Invoices
	.OrderBy(x => x.Customer.LastName)
	.Select(x => new
	{
		InvoiceNo = x.InvoiceID,
		InvoiceDate = $"{x.DateKey.Month}/{x.DateKey.Day}/{x.DateKey.Year}",
		InvoiceDateConCat = x.DateKey.Month + "/" + x.DateKey.Day + "/" + x.DateKey.Year,
		Name = $"{x.Customer.FirstName} {x.Customer.LastName}",
		StoreName = x.Store.StoreName,
		Manager = x.Store.Geography.CityName,
		Priority = x.TotalAmount > 5000 ? "High Priority" :
				"Low Priority"
	})
	.Dump();
