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

Artists
	.Where(x => x.ArtistId < 6)
	.Dump();
	
	
Albums
	.Where(x => x.AlbumId < 6)
	.Select(x => new
	{
		Album = x.Title,
		Label = x.ReleaseLabel,
		Year = x. ReleaseYear,
		Artist = x.Artist.Name
	})
	.OrderBy(x => x.Album)
	.Dump();
	
	
//SQL Aggregates
//Count, Sum, Average, Min, Max
Albums
	.Where(a => a.ArtistId < 6)
	.Select(a => new
	{
		Artist = a.Artist.Name,
		Album = a.Title,
		TotalPrice = a.Tracks.Sum(t => t.UnitPrice)
	})
	.Dump("Albums and Full Price");