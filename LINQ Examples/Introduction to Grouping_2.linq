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

Products
	.GroupBy(x => x.ProductSubcategory.ProductCategory.ProductCategoryID)
	.Select(x => new
	{
		Category = ProductCategories
					.Where(p => p.ProductCategoryID == x.Key)
					.Select(p => p.ProductCategoryName)
					.FirstOrDefault(),
		Product = x.ToList()
	})
	.Dump();
	
Products
	.GroupBy(x => x.ProductSubcategory.ProductCategory.ProductCategoryID)
	.Select(x => new
	{
		Category = ProductCategories
					.Where(p => p.ProductCategoryID == x.Key)
					.Select(p => p.ProductCategoryName)
					.FirstOrDefault(),
		Category2 = x.Select(p => p.ProductSubcategory.ProductCategory.ProductCategoryName).FirstOrDefault(),
		Product = x.Select(p => new
		{
			ProductID = p.ProductID,
			ProdyctName = p.ProductName				
		})
	})
	.Dump();
	