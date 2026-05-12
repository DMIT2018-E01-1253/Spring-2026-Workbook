<Query Kind="Statements">
  <Connection>
    <ID>6b821400-0acf-4186-ba5f-2174b2f7f2fe</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>(local)</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <UseMicrosoftDataSqlClient>true</UseMicrosoftDataSqlClient>
    <EncryptTraffic>true</EncryptTraffic>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook-2025</Database>
    <MapXmlToString>false</MapXmlToString>
    <DriverData>
      <SkipCertificateCheck>true</SkipCertificateCheck>
    </DriverData>
  </Connection>
</Query>

// Query Syntax
//from x in Albums
//select x

// Method Syntax
Albums
	.Select(x => x.Title)
	.Dump();
	
	
Tracks.Sum(x => x.UnitPrice)
	  .Dump();


int[] numbers = { 1, 2, 3, 4, 5 };

numbers
	.Select(x => x)
	.Where(x => x % 2 == 0)
	.Dump();
	
numbers
	.Select(x => x)
	.Where(x => x % 2 != 0)
	.Dump();
	
numbers
	.Select(x => x)
	.Where(x => x > 3)
	.Dump();
	
	
Albums
	.Where(x => x.ReleaseYear > 2000)
	.Select(x => x.Title)
	.Dump();
	
Albums
	.Where(x => x.ReleaseYear > 2000)
	.Select(x => new
	{
		x.ReleaseYear,
		x.Title
	})
	.Dump();