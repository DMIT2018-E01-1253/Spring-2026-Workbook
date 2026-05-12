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

//Standard Query
Albums
	//.Where(x => x.ReleaseYear >= 1990 && x.ReleaseYear <= 1999)
	.Where(x => x.ReleaseYear > 1989 && x.ReleaseYear < 2000)
	.Select(x => x)
	.OrderBy(x => x.ReleaseYear)
	.ThenBy(x => x.Title)
	.Dump();

Albums
	.Where(x => x.ReleaseYear >= 1990 && x.ReleaseYear <= 1999)
	.OrderBy(x => x.ReleaseYear)
	.ThenBy(x => x.Title)
	.Dump();
	
Albums
	.OrderBy(x => x.ReleaseYear)
	.ThenBy(x => x.Title)
	.Where(x => x.ReleaseYear >= 1990 && x.ReleaseYear <= 1999)
	.Dump();

Albums
	.Where(x => x.ReleaseYear > 1989 && x.ReleaseYear < 2000)
	.Select(x => x)
	.OrderBy(x => x.ReleaseYear)
	.ThenByDescending(x => x.Title)
	.Dump();
