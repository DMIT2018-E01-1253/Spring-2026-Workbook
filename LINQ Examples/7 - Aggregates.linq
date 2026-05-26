<Query Kind="Statements">
  <Connection>
    <ID>912e6aee-9ddb-44d8-842c-9b600fcce726</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>(local)</Server>
    <Database>Contoso</Database>
    <DisplayName>Contoso</DisplayName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

//Question 1: Count()
//Context: The marketing department at Contoso Corporation is planning a new campaign targeted at families. To effectively tailor their messaging and offers, they need to understand the size of their customer base that has children. Specifically, they want to count how many of their customers have at least one child. This data will help them focus their efforts on a segment of customers who are most likely to be interested in family-oriented products and services.
//Objective: How would you write a query to count the number of customers who have one or more children? The query should filter the Customers table to find those with a TotalChildren value greater than 0 and return the total count.
Customers
	.Where(x => x.TotalChildren > 0)
	//.Select(x => x.CustomerID).Count()
	.Count()
	.Dump();
	
//Question 2: Count()
//Context: The HR department is conducting a salary review to identify the number of employees earning a high base rate. They are particularly interested in understanding how many employees have a base rate higher than $70 per hour. This analysis will help determine if the company needs to adjust its compensation strategy.
//Objective: How would you write a query to count the number of employees whose base rate is greater than $70 per hour? The query should filter the Employees table to find those with a BaseRate value greater than 70 and return the total count.
Employees
	.Where(x => x.BaseRate > 70)
	.Count()
	//.Select(x => x).Count()
	.Dump();
//Employees
//	.Where(x => x.BaseRate > 70)
//	.Select(x => new
//	{
//		EmployeeID = x.EmployeeID,
//		MemberCount = x.Children.Count()
//	})InvoiceLines
//	.Dump();

//Question 3: Sum()
//Context: The inventory management team at Contoso Corporation is assessing the stock levels across all product lines. They need to generate a report that lists each product along with the total quantity on hand across all store locations ordered by name. This information will be critical for planning inventory restocking and managing product availability across different regions.
//Objective: How would you write a query to retrieve the name of each product and the total quantity on hand? The query should calculate the sum of the OnHandQuantity for each product, and if a product has no inventory records, it should return a quantity of 0, ordered by name. NOTE: You will have to use the Ternary Operator to check if there is any inventory for the product.
Products
	.Select(x => new
	{
		Name = x.ProductName,
		TotalOnHand = x.Inventories.Count() == 0 ? 0 : x.Inventories.Sum(i => i.OnHandQuantity),
		TotalOnHand2 = x.Inventories.Count() == 0 ? 0 : x.Inventories.Select(i => i.OnHandQuantity).Sum()
	})
	.OrderBy(x => x.Name)
	.Dump();

//Question 4: Sum()
//Context: The marketing department needs to calculate the total discount amount applied under each promotion to assess its effectiveness, ordered by promotion name.
//Objective: How would you use LINQ to sum the total DiscountAmount for each promotion, ordered by promotion name using navigation properties in the Promotion table?
Promotions
	.Select(x => new
	{
		PromotionID = x.PromotionID,
		PromotionName = x.PromotionName,
		TotalDiscountGiven = x.InvoiceLines.Sum(il => il.DiscountAmount),
		TotalDiscountGiven2 = x.InvoiceLines.Select(il => il.DiscountAmount).Sum()
	})
	.OrderBy(x => x.PromotionName)
	.ToList()
	.Dump();

//Question 5: Min()
//Context: The product pricing team at Contoso Corporation is reviewing the pricing structure of their various product lines. They need to identify the lowest product cost and price within each subcategory to ensure competitive pricing and profitability. This analysis will help them adjust prices where necessary and identify opportunities for cost reduction.
//Objective: How would you write a query to retrieve the lowest unit cost and price for each product subcategory? The query should select the ProductCategory and ProductSubcategory names, along with the minimum UnitCost and UnitPrice for products in each subcategory. The results should only include subcategories where both the lowest cost and price are available and should be ordered by product category name.
ProductSubcategories
	.Select(x => new
	{
		Category = x.ProductCategory.ProductCategoryName,
		Subcategory = x.ProductSubcategoryName,
		LowestCost = x.Products.Min(p => p.UnitCost),
		LowestPrice = x.Products.Min(p => p.UnitPrice)
	})
	.Where(x => x.LowestCost != null && x.LowestPrice != null)
	.OrderBy(x => x.Category)
	.Dump();

//Question 6: Min()
//Context: The finance department at Contoso Corporation is conducting an audit of their store operations and needs to identify the oldest invoice date for each store. This information will help them review the longevity of business transactions and ensure that records are maintained appropriately across all stores.
//Objective: How would you write a query to retrieve the oldest invoice date for each store? The query should return the StoreID, StoreName, and the oldest invoice date formatted as a string. If no invoices exist for a store, the result should display 'N/A' for the date. The results should be ordered alphabetically by the store name.
Stores
	.Select(x => new
	{
		StoreID = x.StoreID,
		Name = x.StoreName,
		OldestInVoice = x.Invoices.Min(i => i.DateKey) == null ? "N/A" :
			$"{x.Invoices.Min(i => i.DateKey).Month}/{x.Invoices.Min(i => i.DateKey).Day}/{x.Invoices.Min(i => i.DateKey).Year}"
	})
	.OrderBy(x => x.Name)
	.ToList()
	.Dump();

//Question 7: Max()
//Context: The product pricing team at Contoso Corporation is conducting a detailed analysis of product costs and prices across various subcategories. They aim to understand the pricing range within each subcategory by identifying the minimum and maximum unit costs and prices of products. This information is crucial for assessing pricing strategies and identifying opportunities for margin improvements.
//Objective: How would you write a query to retrieve the lowest and highest unit costs and prices for each product subcategory? The query should return the ProductCategory name, SubCategory name, LowestCost, LowestPrice, MaxCost, and MaxPrice. The results should be filtered to exclude subcategories without cost or price information and should be ordered by category name.
ProductSubcategories
	.Select(x => new
	{
		Category = x.ProductCategory.ProductCategoryName,
		Subcategory = x.ProductSubcategoryName,
		LowestCost = x.Products.Min(p => p.UnitCost),
		LowestPrice = x.Products.Min(p => p.UnitPrice),
		MaxCost = x.Products.Max(p => p.UnitCost),
		MaxPrice = x.Products.Max(p => p.UnitPrice)
	})
	.Where(x => x.LowestCost != null && x.LowestPrice != null)
	.OrderBy(x => x.Category)
	.Dump();

//Question 8: Max()
//Context: The finance team at Contoso Corporation is conducting a review of sales performance across their stores. They are particularly interested in identifying the highest invoice amounts recorded at each store. This data will help the team understand the potential revenue from top transactions and assess store-level performance.
//Objective: How would you write a query to retrieve the largest invoice amount for each store? The query should return the StoreID, StoreName, and the LargestInvoiceAmount. The results should be ordered alphabetically by store name.
Stores
	.Select(x => new
	{
		StoreID = x.StoreID,
		Name = x.StoreName,
		//LargestInvoiceAmount = x.Invoices.Select(i => (decimal?)i.TotalAmount).Max() ?? 0,
		LargestInvoiceAmount = x.Invoices.Max(i => (decimal?)i.TotalAmount) ?? 0
	})
	.OrderBy(x => x.Name)
	.ToList()
	.Dump();

//Question 9: Average()
//Context: The sales analysis team at Contoso Corporation is interested in understanding the average quantity of items sold per invoice. This data will help them assess sales patterns and identify opportunities for improving sales strategies. By calculating the average quantity of items sold across all invoices, the team can better understand customer purchasing behaviors.
//Objective: How would you write a query to calculate the average quantity of items sold for each invoice? The query should return the InvoiceNo, InvoiceDate, and the AverageQty of items sold per invoice.
Invoices
	//.AsEnumerable()
	.Select(x => new
	{
		InvoiceNo = x.InvoiceID,
		//InvoiceDate = x.DateKey.DateOnly().ToString("MM/dd/yyyy"),
		InvoiceDate = $"{x.DateKey.Month}/{x.DateKey.Day}/{x.DateKey.Year}",
		AverageQty = x.InvoiceLines.Average(i => i.SalesQuantity),
		AverageQty2 = x.InvoiceLines.Select(i => i.SalesQuantity).Average()
	})
	.ToList()
	.Dump();

//Question 10: Average()
//Context: The financial team at Contoso Corporation is evaluating the performance of their stores by analyzing average sales per store. This information will be used to identify which stores are performing well and which may need additional support or resources. By calculating the average sales amount for each store, the team can gain valuable insights into the overall financial health of the company’s retail operations.
//Objective: How would you write a query to calculate the average sales amount for each store? The query should return the StoreID, Name, and the AverageSales for each store, ordered by store name.
Stores
	.Select(x => new
	{
		StoreID = x.StoreID,
		Name = x.StoreName,
		//LargestInvoiceAmount = x.Invoices.Select(i => (decimal?)i.TotalAmount).Average() ?? 0,
		LargestInvoiceAmount = x.Invoices.Average(i => (decimal?)i.TotalAmount) ?? 0
	})
	.OrderBy(x => x.Name)
	.ToList()
	.Dump();




