<Query Kind="Expression">
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

Albums
	.Select(x => new
	{
		x.Title,
		Label = x.ReleaseLabel == null ? "Unknown" : x.ReleaseLabel,
		Artist = x.Artist.Name,
		Year = x.ReleaseYear,
		DecadeOfReleases = x.ReleaseYear < 1970 ? "Oldies" :
						   x.ReleaseYear < 1980 ? "70s" :
						   x.ReleaseYear < 1990 ? "80s" :
						   x.ReleaseYear < 2000 ? "90s" :
						   "Modern"
	})
	.OrderBy(x => x.Year)